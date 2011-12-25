using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Reflection;

namespace QJVRMS.Common
{
    public class HttpUploadModule : IHttpModule
    {
        public HttpUploadModule()
        {

        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += new EventHandler(this.Application_BeginRequest);
            application.EndRequest += new EventHandler(this.Application_EndRequest);
            application.Error += new EventHandler(this.Application_Error);
        }

        public void Dispose()
        {
        }

        private void Application_BeginRequest(Object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            HttpWorkerRequest request = GetWorkerRequest(app.Context);
            Encoding encoding = app.Context.Request.ContentEncoding;

            int bytesRead = 0; // 已读数据大小
            int read; // 当前读取的块的大小
            int count = 8192; // 分块大小
            byte[] buffer; // 保存所有上传的数据
            string uploadId; // 唯一标志当前上传的ID
            Progress progress; // 记录当前上传的进度信息

            if (request != null)
            {
                // 返回 HTTP 请求正文已被读取的部分。
                //
                byte[] tempBuff = request.GetPreloadedEntityBody();

                // 如果是附件上传
                //
                if (
                    tempBuff != null
                    && IsUploadRequest(app.Request)
                    )
                {
                    // 获取上传大小
                    // 
                    long length = long.Parse(request.GetKnownRequestHeader(HttpWorkerRequest.HeaderContentLength));
                    // 当前上传的ID，用来唯一标志当前的上传
                    // 用此UploadID，可以通过其他页面获取当前上传的进度
                    //
                    uploadId = app.Context.Request.QueryString["UploadID"];

                    // 开始记录当前上传状态
                    //
                    progress = new Progress(length, uploadId);
                    progress.SetState(UploadState.ReceivingData);

                    buffer = new byte[length];
                    count = tempBuff.Length; // 分块大小

                    // 将已上传数据复制过去
                    //
                    Buffer.BlockCopy(tempBuff, 0, buffer, bytesRead, count);

                    // 开始记录已上传大小
                    //
                    bytesRead = tempBuff.Length;
                    progress.SetBytesRead(bytesRead);
                    SetProgress(uploadId, progress, app.Application);

                    // 循环分块读取，直到所有数据读取结束
                    //
                    while (request.IsClientConnected() &&
                        !request.IsEntireEntityBodyIsPreloaded() &&
                        bytesRead < length
                        )
                    {
                        // 如果最后一块大小小于分块大小，则重新分块
                        //
                        if (bytesRead + count > length)
                        {
                            count = (int)(length - bytesRead);
                            tempBuff = new byte[count];
                        }

                        // 分块读取
                        //
                        read = request.ReadEntityBody(tempBuff, count);

                        // 复制已读数据块
                        //
                        Buffer.BlockCopy(tempBuff, 0, buffer, bytesRead, read);

                        // 记录已上传大小
                        //
                        bytesRead += read;
                        progress.SetBytesRead(bytesRead);
                        SetProgress(uploadId, progress, app.Application);

                    }
                    if (
                        request.IsClientConnected() &&
                        !request.IsEntireEntityBodyIsPreloaded()
                        )
                    {

                        // 传入已上传完的数据
                        //
                        InjectTextParts(request, buffer);

                        // 表示上传已结束
                        //
                        progress.SetBytesRead(bytesRead);
                        progress.SetState(UploadState.Complete);
                        SetProgress(uploadId, progress, app.Application);

                    }
                }
            }
        }

        /// <summary>
        /// 结束请求后移除进度信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_EndRequest(Object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;

            if (IsUploadRequest(app.Request))
            {
                SetUploadState(app, UploadState.Complete);
                RemoveFrom(app);
            }

        }

        /// <summary>
        /// 如果出错了设置进度信息中状态为“Error”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Error(Object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;

            if (IsUploadRequest(app.Request))
            {
                SetUploadState(app, UploadState.Error);
            }

        }

        /// <summary>
        /// 设置当前上传进度信息的状态
        /// </summary>
        /// <param name="app"></param>
        /// <param name="state"></param>
        void SetUploadState(HttpApplication app, UploadState state)
        {
            string uploadId = app.Request.QueryString["UploadID"];
            if (uploadId != null && uploadId.Length > 0)
            {
                Progress progress = GetProgress(uploadId, app.Application);
                if (progress != null)
                {
                    progress.SetState(state);
                    SetProgress(uploadId, progress, app.Application);
                }
            }
        }

        /// <summary>
        /// 设置当前上传的进度信息
        /// 根据UploadID记录在Application中
        /// </summary>
        /// <param name="uploadId"></param>
        /// <param name="progress"></param>
        /// <param name="application"></param>
        void SetProgress(string uploadId, Progress progress, HttpApplicationState application)
        {
            if (uploadId == null || uploadId == string.Empty || progress == null)
                return;
            application.Lock();
            application["OpenlabUpload_" + uploadId] = progress;
            application.UnLock();
        }

        /// <summary>
        /// 从Application中移出进度信息
        /// </summary>
        /// <param name="app"></param>
        void RemoveFrom(HttpApplication app)
        {
            string uploadId = app.Request.QueryString["UploadID"];
            HttpApplicationState application = app.Application;
            if (uploadId != null && uploadId.Length > 0)
            {
                application.Remove("OpenlabUpload_" + uploadId);
            }
        }

        /// <summary>
        /// 根据UploadID获取上传进度信息
        /// </summary>
        /// <param name="uploadId"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        public static Progress GetProgress(string uploadId, HttpApplicationState application)
        {
            Progress progress = application["OpenlabUpload_" + uploadId] as Progress;
            return progress;
        }

        HttpWorkerRequest GetWorkerRequest(HttpContext context)
        {

            IServiceProvider provider = (IServiceProvider)HttpContext.Current;
            return (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));
        }

        /// <summary>
        /// 传入已上传完的数据
        /// </summary>
        /// <param name="request"></param>
        /// <param name="textParts"></param>
        void InjectTextParts(HttpWorkerRequest request, byte[] textParts)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;

            Type type = request.GetType();

            while ((type != null) && (type.FullName != "System.Web.Hosting.ISAPIWorkerRequest"))
            {
                type = type.BaseType;
            }

            if (type != null)
            {
                type.GetField("_contentAvailLength", bindingFlags).SetValue(request, textParts.Length);
                type.GetField("_contentTotalLength", bindingFlags).SetValue(request, textParts.Length);
                type.GetField("_preloadedContent", bindingFlags).SetValue(request, textParts);
                type.GetField("_preloadedContentRead", bindingFlags).SetValue(request, true);
            }
        }

        private static bool StringStartsWithAnotherIgnoreCase(string s1, string s2)
        {
            return (string.Compare(s1, 0, s2, 0, s2.Length, true, CultureInfo.InvariantCulture) == 0);
        }

        /// <summary>
        /// 是否为附件上传
        /// 判断的根据是ContentType中有无multipart/form-data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool IsUploadRequest(HttpRequest request)
        {
            return StringStartsWithAnotherIgnoreCase(request.ContentType, "multipart/form-data");
        }
    }
}
