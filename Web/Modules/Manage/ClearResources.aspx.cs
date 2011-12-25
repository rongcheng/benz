using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using QJVRMS.Business;
using QJVRMS.Business.ResourceType;
using System.IO;
using QJVRMS.Common;

namespace WebUI.Modules.Manage
{
    public partial class ClearResources : System.Web.UI.Page
    {
        private int pageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.t_Date.Text = DateTime.Now.AddMonths(-10).ToString("yyyy-MM-dd");
                this.e_Date.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                bind();
            }
        }

        private void bind()
        {
            string userName = this.txtLoginName.Text.Replace("'", "''").Trim();
            DateTime startDate = Convert.ToDateTime(this.t_Date.Text);
            DateTime endDate = Convert.ToDateTime(this.e_Date.Text).AddDays(1);

            Resource obj = new Resource();
            DataSet ds=obj.GetNotPassResources(userName,startDate,endDate,this.AspNetPager1.PageSize,pageIndex);
            this.GridView1.DataSource = ds.Tables[1];
            this.GridView1.DataBind();

            this.AspNetPager1.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }

        protected string GetImgUrl(string serverFileName, string folder)
        {
            QJVRMS.Business.ResourceType.ImageType obj = new QJVRMS.Business.ResourceType.ImageType();
            //yangguang
            //return obj.PreviewPath_170_Read + "/" + folder + "/" + serverFileName;
            return obj.GetPreviewPathRead(folder, serverFileName, "170");
        }

        protected void searchDate_Click(object sender, EventArgs e)
        {
            bind();
        }

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.pageIndex = e.NewPageIndex;
            bind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {   
            string[] ids = chbIds.Value.Split(",".ToCharArray());

            ResourceEntity re = null;
            Resource r = new Resource();

            foreach (string id in ids)
            {
                //单个删除
               
                re = r.GetResourceInfoByItemId(id);
               
                string ItemSerialNum = "";
                string ImageType = "";
                string str = "";//判断170图片或者400图片有没有被删除
           
                bool isValidate = Resource.DeleteResource(re.ItemId);
                string attachmentFolder = string.Empty;

                string sourceFolder = string.Empty;
                string attachmentsFolder = string.Empty;
                if (re.ResourceType.ToLower().Equals("image"))
                {
                    string _170Folder;
                    string _400Folder;

                    ImageType obj = new ImageType();
                    //yangguang
                    //sourceFolder = obj.SourcePath;
                    //attachmentsFolder = obj.SourcePath;
                    //_170Folder = obj.PreviewPath_170;
                    //_400Folder = obj.PreviewPath_400;

                    //sourceFolder = Path.Combine(sourceFolder, re.FolderName);
                    //_170Folder = Path.Combine(_170Folder, re.FolderName);
                    //_400Folder = Path.Combine(_400Folder, re.FolderName);

                    //try
                    //{
                    //    File.Delete(Path.Combine(sourceFolder, re.ServerFileName));
                    //    File.Delete(Path.Combine(_170Folder, re.ServerFileName));
                    //    File.Delete(Path.Combine(_400Folder, re.ServerFileName));
                    //}
                    //catch { }

                    try {
                        File.Delete(obj.GetSourcePath(re.FolderName, re.ServerFileName));
                        attachmentsFolder = obj.SourcePaths[obj.PathNumber].Trim();
                        File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "170"));
                        File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "400"));
                    }
                    catch { }


                    //这里还要加上附件的删除
                    DataTable dt = Resource.GetAttachList(new Guid(id));

                    string fileName = "";
                    attachmentsFolder = Path.Combine(attachmentsFolder, re.FolderName);
                    attachmentsFolder = Path.Combine(attachmentsFolder, UIBiz.CommonInfo.AttachFolder);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        fileName = dt.Rows[i]["fileName"].ToString();
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
                }
                
                
            }


            bind();
        }

    }
}
