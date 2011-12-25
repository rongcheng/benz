using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QJVRMS.Business;
using QJVRMS.Business.ResourceType;
using System.IO;
using QJVRMS.Common;

namespace WebUI.Modules
{
    public partial class LogList :AuthPage
    {
        private int pageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.t_Date.Text = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
                this.e_Date.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                
                bindLogType();
                bind();
            }

        }
        private void bindLogType()
        {
            DataTable dt = new Logs().GetLogType();
            this.ddlLogType.DataSource = dt;
            this.ddlLogType.DataValueField = "ID";
            this.ddlLogType.DataTextField = "CnName";
            this.ddlLogType.DataBind();

            ListItem all = new ListItem("所有", "-1");
            this.ddlLogType.Items.Insert(0,all);
            this.ddlLogType.SelectedValue = "-1";
        }
        private void bind()
        {
            string userName = this.txtLoginName.Text.Replace("'", "''").Trim();
            DateTime startDate = Convert.ToDateTime(this.t_Date.Text);
            DateTime endDate = Convert.ToDateTime(this.e_Date.Text).AddDays(1);
            string logType = this.ddlLogType.SelectedValue;

            Logs obj = new Logs();
            DataSet ds = obj.GetLogs(userName,logType, startDate, endDate, this.AspNetPager1.PageSize, pageIndex);
            this.GridView1.DataSource = ds.Tables[1];
            this.GridView1.DataBind();

            this.AspNetPager1.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
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

        protected string GetEventType(string eventType)
        {

            return new Logs().GetLogTypeCnNameByID(eventType);
            //int _eventType = Convert.ToInt32(eventType);
           // return ((LogType)_eventType).ToString();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                GridViewRow gvr = e.Row;
                string opName = gvr.Cells[1].Text;
                gvr.Cells[1].Text = GetEventType(opName);

                if (opName == ((int)LogType.ValidateResource).ToString())
                {
                    //改变结果的值
                    DataTable dt = Resource.GetResourceStatus();
                    DataRow[] drs = dt.Select("ID="+gvr.Cells[2].Text);
                    if (drs.Length > 0)
                    {
                        gvr.Cells[2].Text = drs[0]["CnName"].ToString();
                    }

                    //id转成sn

                    ResourceEntity re = new Resource().GetResourceInfoByItemId(gvr.Cells[3].Text);
                    gvr.Cells[3].Text = "图片序号：" + re.ItemSerialNum;


                    //gvr.Cells[3].Text = "图片序号：" + this.GridView1.DataKeys[e.Row.RowIndex].Values["ID"].ToString();
                    
                }
                else if (opName == ((int)LogType.DeleteResource).ToString())
                {
                    string logid = this.GridView1.DataKeys[e.Row.RowIndex].Values["ID"].ToString();

                    gvr.Cells[3].Text = "<img src='/modules/showdeletedImage.aspx?id="+logid+"'>";
                
                }

            
            
            }
        }
    }
}
