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

namespace WebUI.UserControls
{
    public partial class Search : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divIsLogin.Visible = Request.IsAuthenticated;

            if (Request.IsAuthenticated)
            {
                this.lblLoginName.Text =  CurrentUser.UserLoginName;//获取用户登录名称
            }

            this.btnSearch.Attributes.Add("onclick", "return check()");
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
           
            string keyword = this.Kwords.Text.ToString();
            string updatatime= this.TextBox1.Text.ToString();
            string ddlcatelog= this.DropDownList1.SelectedValue.ToString();
            Response.Redirect("/SearchPic.aspx?keyword=" + keyword + "&uploadDate=" + updatatime + "&Catalogid=" + ddlcatelog);//以后加开始和结束日期
        }
    }
}