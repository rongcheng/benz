using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using System.IO;
using QJVRMS.Business;
using QJVRMS.Business.ResourceType;
using QJVRMS.Business.Interface;

namespace WebUI {
    public partial class DownHigh :AuthPage{
        delegate void WriteLog(string fileName, string fileType, string username, string usage, string enduser, string folder, bool Errflag, string resourceType);
        public string strFile = string.Empty;
        string strUserName = string.Empty;
        protected void Page_Load(object sender, EventArgs e) {

            try {
                string strFileName;
                string strFileType;
                strUserName = Request["folder"];
                string strUsage;
                string strEndUser;
                string attType;
                string folder = string.Empty;
                string resourceType = string.Empty;

                if (Request.QueryString["FileName"] == null || Request.QueryString["FileName"] == "") {
                    Response.Write("参数错误！");
                    Response.End();
                }
                strFileName = Request.QueryString["FileName"].ToString();
                //使用范围
                strUsage = Request["Usage"];
                //文件类型
                strFileType = Request["FileType"];
                //最终使用用户
                strEndUser = Request["EndUser"];
                attType = Request["attType"];

                resourceType = Request["resourceType"] == null ? "" : Request["resourceType"].ToString();

                //resourceType = new Resource().GetResourceTypeByFileExtention(strFileType);
                // resourceType = ResourceTypeFactory.getResourceType(strFileType.Substring(1)).ResourceType;

                if (!string.IsNullOrEmpty(attType)
                    && attType != "source")
                    folder = UIBiz.CommonInfo.AttachFolder;

                ProcessRequest(strFileName, folder, strFileType, strUserName, strUsage, strEndUser, resourceType, attType);
                
                Response.End();
            }
            catch {
                //Response.Write("参数错误！");
                Response.End();
            }
        }

        /// <summary>
        /// 进行请求处理，返回图像或数据
        /// </summary>
        private void ProcessRequest(string fileName, string subfolder, string fileType, string userName, string usage, string endUser, string resourceType, string attType) {
            //string strResourceRootPath = UIBiz.CommonInfo.ResourceRootPath;

            strUserName = CurrentUser.UserLoginName;
            string strFullFilePath = string.Empty;
            IResourceType rt = null; 
            if (string.IsNullOrEmpty(subfolder)) {
                rt = ResourceTypeFactory.getResourceTypeByString(resourceType.ToLower());
                //yangguang
                //strFullFilePath = Path.Combine(rt.SourcePath, userName);
                //strFullFilePath = Path.Combine(strFullFilePath, fileName + fileType);
                strFullFilePath = rt.GetSourcePath(userName, fileName + fileType);
                strFile = resourceType.ToLower() + "_" + strFullFilePath;
            }
            else {
                if (resourceType.ToLower().Equals("image")) {
                    rt = ResourceTypeFactory.getResourceTypeByString(resourceType.ToLower());
                    //yangguang
                    //strFullFilePath = Path.Combine(rt.SourcePath, userName);
                    //strFullFilePath = Path.Combine(strFullFilePath, subfolder + "/" + fileName + fileType);
                    strFullFilePath = rt.GetSourcePath(userName, subfolder + "/" + fileName + fileType);
                    strFile = resourceType.ToLower() + "_" + strFullFilePath;
                }
                else if (resourceType.ToLower().Equals("video")) {
                    rt = ResourceTypeFactory.getResourceTypeByString(resourceType.ToLower());
                    //yangguang
                    //strFullFilePath = Path.Combine(rt.SourcePath, userName);
                    //strFullFilePath = Path.Combine(strFullFilePath, subfolder + "/" + fileName + fileType);
                    strFullFilePath = rt.GetSourcePath(userName, subfolder + "/" + fileName + fileType);
                    strFile = resourceType.ToLower() + "_" + strFullFilePath;
                }
                else if (resourceType.ToLower().Equals("other"))
                {
                    rt = ResourceTypeFactory.getResourceTypeByString(resourceType.ToLower());
                    //yangguang
                    //strFullFilePath = Path.Combine(rt.SourcePath, userName);
                    //strFullFilePath = Path.Combine(strFullFilePath, subfolder + "/" + fileName + fileType);
                    strFullFilePath = rt.GetSourcePath(userName, subfolder + "/" + fileName + fileType);
                    strFile = resourceType.ToLower() + "_" + strFullFilePath;
                }
            }
            #region
            //string resUrl = string.Empty;
            //resUrl = UIBiz.CommonInfo.FileDownPath + "/" + userName + "{0}" + fileName + fileType;

            //if (resourceType.ToLower().Equals("video"))
            //{
            //    strFullFilePath = Path.Combine(Path.Combine(Path.Combine(strResourceRootPath, "video"), "Source"), userName);
            //    if (attType.ToLower().Equals("attachment"))
            //    {
            //        resUrl = UIBiz.CommonInfo.FileDownPath + "/../video/source/" + userName + "{0}/attachments/" + fileName + fileType;
            //        strFullFilePath = Path.Combine(strFullFilePath, UIBiz.CommonInfo.AttachFolder);

            //    }

            //}
            //else if (resourceType.ToLower().Equals("image"))
            //{
            //    //strFullFilePath = Path.Combine(Path.Combine(Path.Combine(strResourceRootPath,"images"), "Source") , userName); //91的结构
            //    strFullFilePath = Path.Combine(UIBiz.CommonInfo.ImageRootPath, userName);
            //    if (attType.ToLower().Equals("attachment"))
            //    {
            //        resUrl = UIBiz.CommonInfo.FileDownPath + "/../video/source/" + userName + "{0}/attachments/" + fileName + fileType;
            //        strFullFilePath = Path.Combine(strFullFilePath, UIBiz.CommonInfo.AttachFolder);

            //    }

            //}

            //else if (resourceType.ToLower().Equals("document"))
            //{
            //    strFullFilePath = Path.Combine(Path.Combine(strResourceRootPath, "document"), userName);

            //}

            //else if (resourceType.ToLower().Equals("other"))
            //{
            //    strFullFilePath = Path.Combine(Path.Combine(strResourceRootPath, "other"), userName);

            //}
            #endregion
            if (File.Exists(strFullFilePath)) {
                WriteLog wl = new WriteLog(this.WriteLogDB);
                wl.BeginInvoke(fileName, fileType, strUserName, usage, endUser, userName, true, resourceType, null, null);

                System.IO.Stream iStream = null;
                try {

                    byte[] buffer = new Byte[10000];
                    int length;
                    long dataToRead;
                    string filepath = strFullFilePath;
                    string filename = System.IO.Path.GetFileName(filepath);

                    iStream = new System.IO.FileStream(strFullFilePath, System.IO.FileMode.Open,
                    System.IO.FileAccess.Read, System.IO.FileShare.Read);
                    dataToRead = iStream.Length;

                    Response.Buffer = false;
                    Response.AddHeader("Connection", "Keep-Alive");   
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + encodeChineseFileName(filename));
                    Response.AddHeader("Content-Length", iStream.Length.ToString()); 

                    while (dataToRead > 0) {
                        if (Response.IsClientConnected) {
                            length = iStream.Read(buffer, 0, 10000);
                            Response.OutputStream.Write(buffer, 0, length);
                            Response.Flush();

                            buffer = new Byte[10000];
                            dataToRead = dataToRead - length;
                        }
                        else {
                            dataToRead = -1;
                        }
                    }
                }
                catch (Exception ex) {
                    // Trap the error, if any.
                    Response.Write("Error : " + ex.Message);
                }
                finally {
                    if (iStream != null) {
                        //Close the file.
                        iStream.Close();
                    }
                }
                #region
                //Response.WriteFile(strFullFilePath);
                //Response.TransmitFile(strFullFilePath);
                //Response.Clear();
                //Response.Buffer = true;
                //Response.ContentType = "application/octet-stream";
                //Response.AddHeader("Content-Disposition", "attachment;filename=" + encodeChineseFileName(fileName + fileType));
                //Response.BinaryWrite(rawImg);
                //Response.End();
                #endregion
            }
            #region
            //if (!string.IsNullOrEmpty(subfolder))
            //{
            //    resUrl = string.Format(resUrl, "/" + subfolder + "/");
            //}
            //else
            //{
            //    resUrl = string.Format(resUrl, "/");
            //}
            //if (resUrl != null && resUrl != "")
            //{
            //   // wl.BeginInvoke(fileName, fileType, CurrentUser.UserLoginName, usage, endUser, userName, true , resourceType, null, null);

            //    //Response.Redirect(resUrl, true);

            //    //这里不采用 response.binarywrite方法往客户端发送数据，主要是大文件引起的问题，而且也没有这个必要
            //    //by ciqq 2010-4-1
            //}
            #endregion
        }

        /// <summary>
        /// 写日志数据库
        /// </summary>
        private void WriteLogDB(string fileName, string fileType, string downusername, string usage, string enduser, string folder, bool Errflag, string resourceType) {
            //QJVRMS.Business.ImageStorageClass.Production_Hires_Down_Log(fileName, fileType, downusername, usage, enduser, folder, Errflag);
            QJVRMS.Business.Resource.Production_Hires_Down_Log(fileName, fileType, downusername, usage, enduser, folder, Errflag, resourceType);
        }

        /// <summary>
        /// 中文文件名编码
        /// by ciqq 2010-3-18
        /// </summary>
        /// <param name="s">文件名</param>
        /// <returns></returns>
        private string encodeChineseFileName(string s) {
            string _ret = s;
            string _userAgent = Request.UserAgent.ToUpper();

            if (_userAgent.Contains("MSIE")) {
                _ret = HttpUtility.UrlEncode(s);
                if (_ret.Contains("+")) {
                    _ret = _ret.Replace('+', ' ');
                }
            }
            else if (_userAgent.Contains("FIREFOX")) {
                _ret = "\"" + s + "\"";
            }
            return _ret;
        }
    }
}