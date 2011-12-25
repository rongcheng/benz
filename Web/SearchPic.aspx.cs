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
using System.Text;
using QJVRMS.Business;
using System.Data.SqlClient;
namespace WebUI
{
    public partial class SearchPic : AuthPage
    {
        DataTable dt = new DataTable();
        bool ShowFlag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ///创dt的列
                ///需要列的名称和数据类型
                dt.Columns.Add(new DataColumn("Catalogid", typeof(string)));
                dt.Columns.Add(new DataColumn("CatalogName", typeof(string)));

                DataRow dr = dt.NewRow();
                dr["Catalogid"] = "00000000-0000-0000-0000-000000000000";
                dr["CatalogName"] = "--所有目录--";
                dt.Rows.Add(dr);

                Guid groupId = CurrentUser.UserGroupId;
                DataTable cataTable = QJVRMS.Business.Catalog.GetAllCatalog();
                if (cataTable.Rows.Count == 0) return;
                DataRow[] firstNodes = cataTable.Select("parentid is null", "CatalogOrder");
                GetCatalog(cataTable, firstNodes, "");

                this.DdlCatalogid.DataSource = dt;
                this.DdlCatalogid.DataTextField = "CatalogName";
                this.DdlCatalogid.DataValueField = "Catalogid";
                this.DdlCatalogid.DataBind();

                this.BeginDate.Text = DateTime.Now.ToShortDateString();
                this.EndDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void GetCatalog(DataTable CTable, DataRow[] Nodes, string strLine)
        {
            ShowFlag = false;
            for (int i = 0; i < Nodes.Length; i++)
            {
                ShowFlag = QJVRMS.Business.Catalog.GetCataRight(CurrentUser.UserId, new Guid(Nodes[i].ItemArray[0].ToString()));
                DataRow row = dt.NewRow();
                row["Catalogid"] = Nodes[i].ItemArray[0].ToString();
                row["CatalogName"] = strLine + Nodes[i].ItemArray[1].ToString();
                if (ShowFlag == true)
                {
                    dt.Rows.Add(row);
                }
                DataRow[] NodesChild = CTable.Select("parentid='" + Nodes[i].ItemArray[0].ToString() + "'", "CatalogOrder");

                if (NodesChild.Length > 0)
                {
                    //if (ShowFlag == true) //二级类别，如果ShowFlag == true 就显示，等于false;就不显示
                    GetCatalog(CTable, NodesChild, strLine + "--");
                }
                else
                {
                    continue;
                }
            }
        }


        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            //BindData(PageBar1.PageSize, _curpage + 1); 
            string keyword = this.Kwords.Text.ToString().Trim().Replace("'", "''");
            string BeginDate = this.BeginDate.Text.ToString();
            string EndDate = this.EndDate.Text.ToString();
            string ddlcatelog = this.DdlCatalogid.SelectedValue.ToString();


            //在此判断
            if (keyword == string.Empty
                && BeginDate == string.Empty
                && EndDate == string.Empty
                && ddlcatelog == "00000000-0000-0000-0000-000000000000")
            {
                this.ShowMessage("请至少选择一个条件进行查询");
                return;
            }

            if (this.chkUpDate.Checked)
            {
                if (BeginDate != string.Empty
                    && EndDate == string.Empty)
                {
                    EndDate = "9999-12-31";
                }
                else if (EndDate != string.Empty
                    && BeginDate == string.Empty)
                {
                    BeginDate = "1900-01-01";
                }
                else if (EndDate == string.Empty
                    && BeginDate == string.Empty)
                {
                    //BeginDate = "1900-01-01";
                    //EndDate = "9999-12-31";
                }
                else if (Convert.ToDateTime(BeginDate) > Convert.ToDateTime(EndDate))
                {
                    this.ShowMessage("起始时间应早于结束时间");
                    return;

                }
            }
            else
            {
                BeginDate = string.Empty;
                EndDate = string.Empty;
            }

            if (ddlcatelog == "00000000-0000-0000-0000-000000000000")
            {
                ShowFlag = true;
            }
            else
            {
                ShowFlag = QJVRMS.Business.Catalog.GetCataRight(CurrentUser.UserId, new Guid(DdlCatalogid.SelectedValue.ToString()));
            }

            if (ShowFlag != true)
            {
                Response.Write("<script language='javascript' type='text/javascript'>alert('您没有权限进行当前分类搜索！请重新选择！');window.location.href='/SearchPic.aspx';</script>");
            }
            else
            {
                Response.Redirect("/PicList.aspx?keyword=" + keyword + "&BeginDate=" + BeginDate + "&EndDate=" + EndDate + "&Catalogid=" + ddlcatelog);
            }
        }
    }
}
