using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;

using QJVRMS.Business;
using QJVRMS.Common;
using WebUI.UIBiz;
using QJVRMS.Business.ResourceType;
using System.IO;
using System.Drawing.Imaging;

namespace WebUI.Handlers
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class resourceHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;

            Response.ContentType = "text/plain";

            string action = Request.QueryString["action"];

            if (string.IsNullOrEmpty(action))
            { 
                
            
            }
            else if (action.Trim().ToLower().Equals("validatealert"))
            {
                //是否提醒
                Resource obj = new Resource();
                string userId = Request.QueryString["userId"];
                string isSuperAdmin=Request.QueryString["isSuperAdmin"];

                if (!string.IsNullOrEmpty(userId))
                {
                    if(string.IsNullOrEmpty(isSuperAdmin))
                    {
                        isSuperAdmin="0";
                    }

                    if (obj.IsAlertAdmin(new Guid(userId),isSuperAdmin))
                    {
                        Response.Write("1"); //有新的图片等待审核
                        Response.End();
                    }
                    else
                    {
                        Response.Write("0"); //没有需要审核的图片
                        Response.End();
                    }



                }
                Response.Write("0"); //不用提醒
                Response.End();
            }
            else if (action.Trim().ToLower().Equals("delbatch"))
            { 
                //批量删除

                string itemIds = Request.Form["itemIds"];
                if (string.IsNullOrEmpty(itemIds))
                {
                    Response.Write("0"); //参数错误
                    Response.End();
                    
                }
                itemIds = itemIds.TrimEnd(";".ToCharArray());

                string userId = Request.QueryString["userId"];

                string[] arrId = itemIds.Split(";".ToCharArray());
                foreach (string id in arrId)
                {
                    //删除的图片要记录一下

                    byte[] buffer;

                    ResourceEntity re = null;
                    Resource r = new Resource();
                    re = r.GetResourceInfoByItemId(id);
                    
                    string ItemSerialNum = "";
                    string ImageType = "";
                    string str = "";//判断170图片或者400图片有没有被删除

                    ItemSerialNum = re.ItemSerialNum;
                    //ItemSerialNum = lb_ItemSerialNum.Text;
                    //ImageType = lb_ImageType.Text;
                    //bool isValidate = QJVRMS.Business.ImageStorageClass.DeleteImageStorage(new Guid(this.Hidden_ItemId.Value));

                    bool isValidate = Resource.DeleteResource(re.ItemId);
                    string attachmentFolder = string.Empty;

                    string sourceFolder = string.Empty;
                    string attachmentsFolder = string.Empty;
                    if (re.ResourceType.ToLower().Equals("image"))
                    {
                        string _170Folder;
                        string _400Folder;

                        ImageType obj = new ImageType();
                        
                        try
                        {
                            
                            //记录图片
                            
                            string img = obj.GetPreviewPath(re.FolderName, re.ServerFileName, "170");
                            ImageHelper objImgHelper = new ImageHelper(img);
                            objImgHelper.Resize(80);
                            using (MemoryStream ms = new MemoryStream())
                            {
                                objImgHelper.GetImage().Save(ms, ImageFormat.Jpeg);
                                buffer = ms.ToArray();
                            }
                            objImgHelper.Dispose();



                            //记录日志
                            if (!string.IsNullOrEmpty(userId))
                            {
                                User objUser = new MemberShipManager().GetUser(new Guid(userId));
                                if (objUser != null)
                                {

                                    //日志，所有的删除，只记录一次
                                    LogEntity model = new LogEntity();
                                    model.id = Guid.NewGuid();
                                    model.userId = objUser.UserId;
                                    model.userName = objUser.UserLoginName;
                                    model.EventType = ((int)LogType.DeleteResource).ToString();
                                    model.EventResult = "成功";
                                    model.EventContent = "图片序号："+re.ItemSerialNum;
                                    model.IP = HttpContext.Current.Request.UserHostAddress;
                                    model.AddDate = DateTime.Now;
                                    new Logs().Add(model);

                                    r.SaveDeletedImage(model.id, buffer);
                                }

                            }


                            File.Delete(obj.GetSourcePath(re.FolderName, re.ServerFileName));
                            attachmentsFolder = obj.SourcePaths[obj.PathNumber].Trim();
                            File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "170"));
                            File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "400"));



                        }
                        catch { }
                    }
                    else if (re.ResourceType.ToLower().Equals("video"))
                    {
                        string _previewPolder = CommonInfo.VideoPreviewPath;

                        VideoType obj = new VideoType();
                        
                        try
                        {
                            File.Delete(obj.GetSourcePath(string.Empty, re.ServerFileName));
                            attachmentsFolder = obj.SourcePaths[obj.PathNumber].Trim();
                            File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "flv"));
                            File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "image"));
                            File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "smallflv"));
                        }
                        catch
                        {

                        }
                    }

                    //sourceFolder = Path.Combine(sourceFolder, CurrentUser.UserLoginName);
                    //sourceFolder = Path.Combine(sourceFolder, WebUI.UIBiz.CommonInfo.AttachFolder);

                    #region 删除物理文件 by ciqq 2010-4-2
                   
                    //删除所有的附件
                    //string sourceFolder = Path.Combine(WebUI.UIBiz.CommonInfo.ImageRootPath, this.hiFolder.Value);

                    //根据资源ID获得所有的附件

                    DataTable dt = Resource.GetAttachList(new Guid(id));

                    //dt.Columns.Add("fileNamefileLength");

                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    dr["fileNamefileLength"] = dr["filename"].ToString() + " ( " + Tool.toFileSize(Convert.ToInt64(dr["fileLength"].ToString())) + " ) ";

                    //}

                    //this.attList.DataSource = dt;
                    //this.attList.DataBind();


                    string fileName = "";
                    attachmentsFolder = Path.Combine(attachmentsFolder, re.FolderName);
                    attachmentsFolder = Path.Combine(attachmentsFolder, UIBiz.CommonInfo.AttachFolder);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //fileName = this.attList.DataKeys[i].Values[1].ToString();
                        fileName = dt.Rows[i]["filename"].ToString();
                        fileName = Path.Combine(attachmentsFolder, fileName);
                        try
                        {
                            File.Delete(fileName);
                        }
                        catch (Exception ex)
                        {
                            LogWriter.WriteExceptionLog(ex);
                        }
                    }                   
                    #endregion
                }


                

                Response.Write("0"); //不用提醒
                Response.End();


                
              
                
            }


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
