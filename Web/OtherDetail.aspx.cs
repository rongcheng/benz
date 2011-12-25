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


using System.Text;
using QJVRMS.Business;
using System.IO;
using QJVRMS.Business.ResourceType;
using QJVRMS.Common;
using QJVRMS.Business.SecurityControl;
namespace WebUI
{
    public partial class OtherDetail : AuthPage
    {
        protected string folder = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                string id =Request["ItemId"];
                
                if (string.IsNullOrEmpty(id))
                {
                    Response.Write("<script language='javascript'>alert('不存在此图片或您没有权限浏览!');window.close();</script>");
                    Response.End();
                }
                GetImageInfo(new Guid(id));
            }
            catch (Exception ex)
            {
                LogWriter.WriteExceptionLog(ex);
                Response.Write("<script language='javascript'>alert('不存在此图片或您没有权限浏览!');window.close();</script>");
                Response.End();
            }
        }

        protected void GetImageInfo(Guid itemId)
        {
            Resource rs = new Resource();
            ResourceEntity r = rs.GetResourceInfoByItemId(itemId.ToString());

            //更新浏览次数
            rs.UpdateResourceViewCount(itemId.ToString());


            folder = r.FolderName;

            //this.imgsrc.Src = yRootPath;
            this.lb_Description.Text = r.Description;
            this.lb_FileName.Text = r.FileName;
            this.lb_Caption.Text = r.Caption;
            this.lb_Author.Text = r.Author;
            this.lb_fileLength.Text = QJVRMS.Common.Tool.toFileSize(r.FileSize);
            this.lb_ImageType.Text = Path.GetExtension(r.FileName);
            this.lb_ItemSerialNum.Text = r.ItemSerialNum;
            this.lb_Keyword.Text = r.Keyword;
           
            this.lb_uploadDate.Text = r.uploadDate.ToString("yyyy-MM-dd");
          
            this.lb_viewCount.Text = r.ViewCount.ToString();
            string enableDate = "";
            if (r.StartDate.ToString("yyyy-MM-dd") != "1900-01-01")
            {
                enableDate += r.StartDate.ToString("yyyy-MM-dd");
            }
            enableDate += " -- ";
            if (r.EndDate.ToString("yyyy-MM-dd") != "1900-01-01")
            {
                enableDate += r.EndDate.ToString("yyyy-MM-dd");
            }
            if (enableDate != " -- ")
            {
                this.lb_enableDate.Text = enableDate;
            }

            if (r.HasCopyright == 1)
            {
                this.pSource.Visible = false;
            }
            else
            {
                this.pSource.Visible = true;
            }

            

        }

    }
}
