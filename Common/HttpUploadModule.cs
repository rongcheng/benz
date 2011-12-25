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

            int bytesRead = 0; // �Ѷ����ݴ�С
            int read; // ��ǰ��ȡ�Ŀ�Ĵ�С
            int count = 8192; // �ֿ��С
            byte[] buffer; // ���������ϴ�������
            string uploadId; // Ψһ��־��ǰ�ϴ���ID
            Progress progress; // ��¼��ǰ�ϴ��Ľ�����Ϣ

            if (request != null)
            {
                // ���� HTTP ���������ѱ���ȡ�Ĳ��֡�
                //
                byte[] tempBuff = request.GetPreloadedEntityBody();

                // ����Ǹ����ϴ�
                //
                if (
                    tempBuff != null
                    && IsUploadRequest(app.Request)
                    )
                {
                    // ��ȡ�ϴ���С
                    // 
                    long length = long.Parse(request.GetKnownRequestHeader(HttpWorkerRequest.HeaderContentLength));
                    // ��ǰ�ϴ���ID������Ψһ��־��ǰ���ϴ�
                    // �ô�UploadID������ͨ������ҳ���ȡ��ǰ�ϴ��Ľ���
                    //
                    uploadId = app.Context.Request.QueryString["UploadID"];

                    // ��ʼ��¼��ǰ�ϴ�״̬
                    //
                    progress = new Progress(length, uploadId);
                    progress.SetState(UploadState.ReceivingData);

                    buffer = new byte[length];
                    count = tempBuff.Length; // �ֿ��С

                    // �����ϴ����ݸ��ƹ�ȥ
                    //
                    Buffer.BlockCopy(tempBuff, 0, buffer, bytesRead, count);

                    // ��ʼ��¼���ϴ���С
                    //
                    bytesRead = tempBuff.Length;
                    progress.SetBytesRead(bytesRead);
                    SetProgress(uploadId, progress, app.Application);

                    // ѭ���ֿ��ȡ��ֱ���������ݶ�ȡ����
                    //
                    while (request.IsClientConnected() &&
                        !request.IsEntireEntityBodyIsPreloaded() &&
                        bytesRead < length
                        )
                    {
                        // ������һ���СС�ڷֿ��С�������·ֿ�
                        //
                        if (bytesRead + count > length)
                        {
                            count = (int)(length - bytesRead);
                            tempBuff = new byte[count];
                        }

                        // �ֿ��ȡ
                        //
                        read = request.ReadEntityBody(tempBuff, count);

                        // �����Ѷ����ݿ�
                        //
                        Buffer.BlockCopy(tempBuff, 0, buffer, bytesRead, read);

                        // ��¼���ϴ���С
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

                        // �������ϴ��������
                        //
                        InjectTextParts(request, buffer);

                        // ��ʾ�ϴ��ѽ���
                        //
                        progress.SetBytesRead(bytesRead);
                        progress.SetState(UploadState.Complete);
                        SetProgress(uploadId, progress, app.Application);

                    }
                }
            }
        }

        /// <summary>
        /// ����������Ƴ�������Ϣ
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
        /// ������������ý�����Ϣ��״̬Ϊ��Error��
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
        /// ���õ�ǰ�ϴ�������Ϣ��״̬
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
        /// ���õ�ǰ�ϴ��Ľ�����Ϣ
        /// ����UploadID��¼��Application��
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
        /// ��Application���Ƴ�������Ϣ
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
        /// ����UploadID��ȡ�ϴ�������Ϣ
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
        /// �������ϴ��������
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
        /// �Ƿ�Ϊ�����ϴ�
        /// �жϵĸ�����ContentType������multipart/form-data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool IsUploadRequest(HttpRequest request)
        {
            return StringStartsWithAnotherIgnoreCase(request.ContentType, "multipart/form-data");
        }
    }
}
