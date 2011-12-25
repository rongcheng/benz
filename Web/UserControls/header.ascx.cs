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
using WebUI.UIBiz;

namespace WebUI.UserControls
{
    public partial class header : BaseUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        

            //if (Request.IsAuthenticated)
            //{
            //    
            //}


            if (!this.IsPostBack)
            {
                this.ManageLink.Text = "<li><a href=\"/Modules/Manage/Sysmanager.aspx\">管理</a></li>";

                if (Request.IsAuthenticated)
                {

                    if (QJVRMS.Business.Function.GetUserFunctionRight(CurrentUser.UserId) || CurrentUser.UserId == CommonInfo.SuperAdminId)
                        this.ManageLink.Visible = true;
                    else
                        this.ManageLink.Visible = false;

                    this.lblLoginName.Text = CurrentUser.UserName;//获取用户登录名称
                }
                else
                {
                    this.ManageLink.Visible = false;
                }

                if (WebUI.UIBiz.CommonInfo.AuthByAD)
                {
                    this.btnModifyPwd.Visible = false;
                }
            }
            
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
           

        //    string keyword = this.Kwords.Text.ToString().Trim().Replace("'","''");
           
        //    Response.Redirect("/PicList.aspx?keyword=" + Server.UrlEncode(keyword) + "&BeginDate=&EndDate=&Catalogid=" + "00000000-0000-0000-0000-000000000000");//以后加开始和结束日期
        //}
 
             
     
    }
}