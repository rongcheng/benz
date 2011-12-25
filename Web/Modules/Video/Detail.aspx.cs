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
using QJVRMS.Business;
using QJVRMS.Common;
using System.IO;
using QJVRMS.Business.ResourceType;

namespace WebUI.Modules.Video
{
    public partial class Detail : AuthPage
    {
        protected string folder = string.Empty;
        protected string flvFilePath = string.Empty;
        protected string imageFilePath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                GetVideoInfo(Request["ItemId"].ToString());
            }
            catch
            {
                Response.Write("<script language='javascript'>alert('不存在此视频或您没有权限浏览!');window.close();</script>");
                Response.End();
            }
        }


        protected void GetVideoInfo(string itemId)
        {
             
            Resource r=new Resource();
            ResourceEntity vs = r.GetResourceInfoByItemId(itemId);

            VideoType obj = new VideoType();
            //yangguang
            //string previewPath = obj.PreviewPathRead;
            //if(string.IsNullOrEmpty(previewPath))
            //{
            //    return;
            //}

            if (vs != null)
            {
                folder = vs.FolderName;
                //yangguang
                //flvFilePath = previewPath + "/flv/" + vs.FolderName + "/" + vs.ItemSerialNum + ".flv";
                //imageFilePath = previewPath + "/image/" + vs.FolderName + "/" + vs.ItemSerialNum + ".jpg";
                //flvFilePath = obj.GetPreviewPath(vs.FolderName, vs.ItemSerialNum + ".flv", "flv");
                flvFilePath = obj.GetPreviewPathRead(vs.FolderName, vs.ItemSerialNum + ".flv", "flv");
                imageFilePath = obj.GetPreviewPathRead(vs.FolderName, vs.ItemSerialNum + ".jpg", "image");// + "/image/" + vs.FolderName + "/" + vs.ItemSerialNum + ".jpg";
                this.lb_Caption.Text = vs.Caption;
                this.lb_Description.Text = vs.Description;
                this.lb_enableDate.Text = string.Format("{0} -- {1}", vs.StartDate.ToShortDateString(), vs.EndDate.ToShortDateString());
                this.lb_FileName.Text = vs.FileName;
                this.lb_ItemSerialNum.Text = vs.ItemSerialNum;
                this.lb_Keyword.Text = vs.Keyword;
                this.lb_shotDate.Text = vs.shotDate.ToShortDateString();
                this.lb_uploadDate.Text = vs.uploadDate.ToShortDateString();
                this.lb_FileType.Text = Path.GetExtension(vs.FileName);
                this.lb_FileLength.Text = Tool.toFileSize( vs.FileSize);
                this.pageTitle.Text = vs.Caption;

                if (vs.ResourceType.Equals("video"))
                { 
                    VideoStorage v=r.GetVideoInfoBySN(vs.ItemSerialNum);
                    if (v != null)
                    {
                        if (v.ClipLength.Length > 8)
                        {
                            this.lb_duration.Text = v.ClipLength.Substring(0, 8);
                        }
                        this.lb_wh.Text = v.ClipSize;
                        this.lb_bitrate.Text = v.Bitrate;
                    }                    

                }
            
            }

            
        }
    }
}
