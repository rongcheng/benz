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
using WebUI.UIBiz;
using QJVRMS.Business;

namespace WebUI.UserControls
{
    public partial class QJ_Header_DefaultPage : BaseUserControl
    {
        public static DataTable cataTable = null;
        public string[] currentCss;
        private string shownav = "";
        public string CEFlag = "";
        public string loginName = "";
        public string ShowNav//如果是创意和媒体公用的页面 可以设为0 然后通过CEFlag 来区别哪个专区
        {
            set
            {
                this.shownav = value;
            }
            get
            {
                return this.shownav;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            currentCss = new string[7] {"", "", "", "", "", "",""};
            string _url = Request.RawUrl.ToLower();

            if (_url.Contains("default.aspx"))
            {
                currentCss[0] = " class=\"current\"";
            }
            else if (_url.Contains("searchresource.aspx"))
            {
                currentCss[1] = " class=\"current\"";
            }
            else if (_url.Contains("addresource.aspx"))
            {
                currentCss[2] = " class=\"current\"";
            }
            else if (_url.Contains("sysmanager.aspx"))
            {
                currentCss[3] = " class=\"current\"";
            }
            else if (_url.Contains("userprofile.aspx"))
            {
                currentCss[4] = " class=\"current\"";
            }
            else if (_url.Contains("feature.aspx")) {
                currentCss[5] = " class=\"current\"";
            }

            if (Request.IsAuthenticated)
            {
                //loginName = AccountController.UserName;
            }

            if (Request.QueryString["CEFlag"] != null && Request.QueryString["CEFlag"].ToString() == "2")
            {
                this.CEFlag = "2";
            }
            else if (Request.QueryString["CEFlag"] != null && Request.QueryString["CEFlag"].ToString() == "1")
            {
                this.CEFlag = "1";
            }
            else
            {
                if (this.shownav == "1") this.CEFlag = "1";
                else if (this.shownav == "2") this.CEFlag = "2";
                else CEFlag = "0";//既不是创意也不是编辑频道 如gallery  and so on.

            }


            this.ManageLink.Text = "<a href=\"/Modules/Manage/Sysmanager.aspx\">平台管理</a>";
            this.ManageLink.Visible = false;

            if (Request.IsAuthenticated)
            {
                this.lblLoginName.Text = CurrentUser.UserName;//获取用户登录名称
                if (QJVRMS.Business.Function.GetUserFunctionRight(CurrentUser.UserId) || CurrentUser.UserId == CommonInfo.SuperAdminId)
                {
                    this.ManageLink.Visible = true;
                }
            }

            if (WebUI.UIBiz.CommonInfo.AuthByAD)
            {
                this.btnModifyPwd.Visible = false;
            }

            bindBigCat();
        }

        /// <summary>
        /// 用户特有信息写入ViewState
        /// </summary>
        /// <returns></returns>
        protected DataTable GetDenyTable()
        {
            if (this.ViewState["denyTable"] == null)
            {
                DataTable t = QJVRMS.Business.Catalog.GetCatalogByMethod(CurrentUser.UserId, QJVRMS.Business.SecurityControl.OperatorMethod.Deny);
                this.ViewState["denyTable"] = t;
            }

            return this.ViewState["denyTable"] as DataTable;
        }


        private void bindBigCat()
        { 
            
            GetCataTable();
            if (cataTable.Rows.Count == 0) return;

            DataRow[] firstNodes = cataTable.Select("parentid is null", "CatalogOrder");

            DataTable newDataTable = cataTable.Clone();
            for (int i = 0; i < firstNodes.Length; i++) 
            {
                //newDataTable.ImportRow(firstNodes[i]);
                string cataId = firstNodes[i]["CatalogId"].ToString();
                if (this.GetDenyTable().Select("ObjectId='" + cataId + "'").Length == 0)
                {
                    newDataTable.ImportRow(firstNodes[i]);
                }
            }

            this.rptBigCat.DataSource = newDataTable.DefaultView;
            this.rptBigCat.DataBind();
            
        }
        public void rptBigCat_ItemDataBoud(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            GetCataTable();


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptSmallCat = (Repeater)e.Item.FindControl("rptSmallCat");
                DataRowView rowv = (DataRowView)e.Item.DataItem;


                //int CategorieId = Convert.ToInt32(rowv["ID"]);
                //根据分类ID查询该分类下的产品，并绑定产品Repeater



                string parentId = rowv["CatalogId"].ToString();
                DataRow[] childNodes = cataTable.Select("parentid='" + parentId + "'", "CatalogOrder");
                DataTable newDataTable = cataTable.Clone();
                for (int i = 0; i < childNodes.Length; i++)
                {

                    //newDataTable.ImportRow(childNodes[i]);
                    string cataId = childNodes[i]["CatalogId"].ToString();
                    if (this.GetDenyTable().Select("ObjectId='" + cataId + "'").Length == 0)
                    {
                        newDataTable.ImportRow(childNodes[i]);
                    }
                }


                rptSmallCat.DataSource = newDataTable.DefaultView;
                rptSmallCat.DataBind();

            }



        }


        protected void GetCataTable()
        {
            if (cataTable == null)
            {
                cataTable = QJVRMS.Business.Catalog.GetAllCatalog();
            }
            cataTable = QJVRMS.Business.Catalog.GetAllCatalog();
        }


        protected void logStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            LogEntity model = new LogEntity();
            model.id = Guid.NewGuid();
            model.userId = CurrentUser.UserId;
            model.userName = CurrentUser.UserLoginName;
            model.EventType = ((int)LogType.Logout).ToString();
            model.EventResult = "成功";
            model.EventContent = "";
            model.IP = HttpContext.Current.Request.UserHostAddress;
            model.AddDate = DateTime.Now;
            new Logs().Add(model);
        }
    }
}