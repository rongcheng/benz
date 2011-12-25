using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text;
using System.Xml;
using QJVRMS.Common;
using QJVRMS.Business;

namespace WebUI
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Global()
        {
            InitializeComponent();
        }

        protected void Application_Start(Object sender, EventArgs e)
        {

        }

        protected void Session_Start(Object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

            #region ����swfUpload��flash���IE�����ͨ��ʱ��session��ͬ������
            try
            {
                string session_param_name = "ASPSESSID";
                string session_cookie_name = "ASP.NET_SESSIONID";

                if (HttpContext.Current.Request.Form[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                Response.Write("Error Initializing Session");
            }

            try
            {
                string auth_param_name = "AUTHID";
                string auth_cookie_name = FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request.Form[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }

            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                Response.Write("Error Initializing Forms Authentication");
            }
            #endregion 


        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            //System.Security.Principal.IPrincipal ipr = HttpContext.Current.User;

            //if (ipr == null) return;
            //FormsIdentity id = (FormsIdentity)ipr.Identity;
            //FormsAuthenticationTicket ticket = id.Ticket;

            //// Get the stored user-data, in this case, our roles
            //string userData = ticket.UserData;
            //string[] roles = userData.Split(',');
            //HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
        }

        protected void Session_End(Object sender, EventArgs e)
        {

        }

        protected void Application_End(Object sender, EventArgs e)
        {

        }


        protected void Application_Error(object sender, EventArgs e) {
            // Response.Write("HIApp");

            //#if(!DEBUG) 
            //    Exception exp = Server.GetLastError().GetBaseException();
            //    QJVRMS.Common.LogWriter.WriteExceptionLog(exp, true);//д����־
            //#endif
           
        }

        #region Web ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }
        #endregion


        /// <summary>
        /// ����cookie
        /// </summary>
        /// <param name="cookie_name"></param>
        /// <param name="cookie_value"></param>
        void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            if (cookie == null)
            {
                cookie = new HttpCookie(cookie_name);
                HttpContext.Current.Request.Cookies.Add(cookie);
            }
            cookie.Value = cookie_value;
            HttpContext.Current.Request.Cookies.Set(cookie);
        }


    }
}