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
using WebUI.UIBiz;
using QJVRMS.Business;
using QJVRMS.Business.SecurityControl;
using QJVRMS.Common;

namespace WebUI
{
    public partial class AuthPage : BasePage
    {
        private string funId;
        private bool isInControl = false;
        private ISecurityObject currentSecObj;
    


        protected ISecurityObject CurrentSecurityObj
        {
            set { this.currentSecObj = value; }
        }

        /// <summary>
        /// ����ҳ���Ƿ���Ȩ�޿�����
        /// </summary>
        protected bool IsInControl
        {
            set { this.isInControl = value; }
        }

        protected object[] checkControls = new object[] { };

        protected override void OnInit(EventArgs e)
        {
            //��ǰ�û������ڷ��ص�¼ҳ��
            if ( !Request.IsAuthenticated)
            //|| CurrentUser == null)
            {
                ResponseNotAuthorized();
            }

            funId = Request["funId"];
            //if (isInControl &&
            //    !IsSuperAdmin
            //    && !CheckUIRule(funId))//�˴���Ҫ�޸� sunan
            //{
            //    Response.Write("��û��Ȩ�޷��ʸ�ҳ��");
            //    Response.End();
            //}

            if (!string.IsNullOrEmpty(funId))
            {
                if (!CheckUIRule(funId) && !IsSuperAdmin)
                {
                    Response.Write("��û��Ȩ�޷��ʸ�ҳ��");
                    Response.End();
                }
                
            }



            if (!IsSuperAdmin)
            {
                CheckUIMethodRule();
            }

            base.OnInit(e);
        }

        protected bool CheckUIRule(string funId)
        {
            if (string.IsNullOrEmpty(funId)) return false;
            return UIControlManager.CheckUIFunctionEntrance(new Guid(funId), CurrentUser);
        }


        protected void CheckUIMethodRule()
        {
            UIControlManager.CheckUIMethod(this.checkControls, CurrentUser, currentSecObj);
        }

        /// <summary>
        /// ��ǰ�û�
        /// </summary>
        public IWebUser CurrentUser
        {
            get
            {


                System.Security.Principal.IPrincipal ipr = HttpContext.Current.User;

                if (ipr == null) return null;
                FormsIdentity id = ipr.Identity as FormsIdentity;

                if (id != null)
                {
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Get the stored user-data, in this case, our roles
                    string userData = ticket.UserData;
                    string[] userStr = userData.Split(',');

                    IWebUser webUser = null;

                    webUser = new WebUser(new Guid(userStr[0]), new Guid(userStr[1]), userStr[2], userStr[3], userStr[4], userStr[5]);

                    return webUser;
                }
                else
                {
                    return null;
                }
            }

        }

        protected bool IsSuperAdmin
        {
            get
            {
                if (CurrentUser == null) return false;
                else return CurrentUser.UserId == CommonInfo.SuperAdminId;

            }
        }


        protected Guid SuperAdminLoginId
        {
            get { return CommonInfo.SuperAdminId; }
        }

        protected void ResponseNotAuthorized()
        {

            //FormsAuthentication.RedirectToLoginPage();
            //   string script = "parent.location='" + FormsAuthentication.LoginUrl + "';";
            // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirect", script, true);
            //  Response.Redirect(
           // Response.Redirect(FormsAuthentication.LoginUrl, true);
            // Response.Write("<script language='javascript'>" + script + "</script>");
            //Response.End();

            
            FormsAuthentication.SignOut();
            Response.Redirect(FormsAuthentication.LoginUrl, true);
            
        }
    }
}
