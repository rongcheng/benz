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

namespace WebUI
{
    public partial class downRedirect : AuthPage
    {
        public string filename;
        public string itemId;
        protected void Page_Load(object sender, EventArgs e)
        {
            filename = Request["FileName"];
            itemId = Request["itemId"];

            if (!Page.IsPostBack)
            {
                this.resourceType.Value = Request["resourceType"] == null ? "" : Request["resourceType"].ToString();

                string _s = "原图";
                if (this.resourceType.Value.ToLower().Equals("video"))
                {
                    _s = "原视频";
                }
                else if (this.resourceType.Value.ToLower().Equals("document"))
                {
                    _s = "原文档";
                }
                else if (this.resourceType.Value.ToLower().Equals("other"))
                {
                    _s = "原资源";
                }


                ResourceEntity re = new Resource().GetResourceInfoByItemId(itemId.ToString());
                
                this.lbDownSource.Text = _s + " ( "+Tool.toFileSize(re.FileSize)+" )";
                this.serverFileName.Value = re.ServerFileName;


                this.selectUsage.DataSource = QJVRMS.Business.Usage.UsageTable;


                this.selectUsage.DataTextField = "UsageName";
                this.selectUsage.DataValueField = "UsageName";

                this.selectUsage.DataBind();

                this.bindRepeater();
            }
        }

        /// <summary>
        /// 绑定附件列表
        /// </summary>
        private void bindRepeater()
        {
            
            DataTable dt = QJVRMS.Business.Resource.GetAttachList(new Guid(this.itemId));
            dt.Columns.Add("fileNamefileLength");

            foreach(DataRow dr in dt.Rows)
            {
                dr["fileNamefileLength"] = dr["filename"].ToString()+" ( "+Tool.toFileSize(Convert.ToInt64(dr["fileLength"].ToString()))+" ) ";
                
            }
            
            this.rptDownloadList.DataSource = dt.DefaultView;
            this.rptDownloadList.DataBind();
            
        }

        

        /// <summary>
        /// 原始文件下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbDownSource_Command(object sender, CommandEventArgs e)
        {

            LinkButton lb = this.lbDownSource;
            string endUser = this.txtenduser.Text;

            string usage = this.selectUsage.SelectedValue.ToString();

            string attType = "source";

            string filetype = string.Empty;

            string downFileName = string.Empty;

            string resourceType = this.resourceType.Value;

            if (attType == "source")
            {
                downFileName = Request["FileName"];
                filetype = Request["FileType"];
            }
            else
            {
                downFileName = System.IO.Path.GetFileNameWithoutExtension(lb.Text);
                filetype = System.IO.Path.GetExtension(lb.Text);
            }
            filetype = System.IO.Path.GetExtension(this.serverFileName.Value);


            Response.Redirect("downhigh.aspx?filename=" + downFileName + "&filetype=" + filetype + "&usage=" + usage + "&EndUser=" + endUser + "&attType=" + attType + "&folder=" + Request["folder"] + "&resourceType=" + resourceType);
       
        }     


        /// <summary>
        /// 附件下载
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptDownloadList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lb = (LinkButton)e.Item.FindControl("lbDown");

            string endUser = this.txtenduser.Text.Replace("'","''");

            string usage = this.selectUsage.SelectedValue.ToString();

            string attType = "attachment";

            string filetype = string.Empty;

            string downFileName = string.Empty;

            string resourceType = this.resourceType.Value;

            if (attType == "0")
            {
                downFileName = Request["FileName"];
                filetype = Request["FileType"];
            }
            else
            {
                downFileName = System.IO.Path.GetFileNameWithoutExtension(e.CommandArgument.ToString());
                filetype = System.IO.Path.GetExtension(e.CommandArgument.ToString());
            }
            Response.Redirect("downhigh.aspx?filename=" + downFileName + "&filetype=" + filetype + "&usage=" + usage + "&EndUser=" + endUser + "&attType=" + attType + "&folder=" + Request["folder"] + "&resourceType=" + resourceType);
       
        }
        
        
    }
}
