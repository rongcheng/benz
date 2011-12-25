using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using QJVRMS.Business;
using WebUI.UIBiz;
using QJVRMS.Common;
using System.Web.Security;
using System.Configuration;

namespace WebUI
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class loginFromBoss : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            //context.Response.Write("Hello World");

            HttpRequest Request = context.Request;

            string defaultUserName = ConfigurationManager.AppSettings["fromBossUserName"];
            string defaultPassword = ConfigurationManager.AppSettings["fromBossPassword"];

            if (string.IsNullOrEmpty(defaultUserName))
            {
                defaultUserName = "dmadmin";
            }

            if (string.IsNullOrEmpty(defaultPassword))
            {
                defaultPassword = "111";
            }

            string loginName = defaultUserName;
            string password = defaultPassword;

            #region 登陆使用方法
            MemberShipManager msm = new MemberShipManager();
            object temp = null;


            bool isValidate = false;

            //Form 验证 
            if (!CommonInfo.AuthByAD)
            {
                isValidate = msm.AuthUserByForm(loginName,
                    password,
                    Request.UserHostAddress,
                    ref temp);
            }//AD 验证
            else
            {
                isValidate = msm.AuthUserByAD(CommonInfo.DomainName,
                    CommonInfo.DomainNamePrefix + @"\" + loginName,
                    loginName, password,
                    ref temp);
            }


            if (isValidate)
            {
                //转换为业务对象
                QJVRMS.Business.User user = temp as QJVRMS.Business.User;


                //AD验证 获取用户在系统中的特有信息 
                if (CommonInfo.AuthByAD)
                {
                    User userInfo = msm.GetUser(loginName);

                    user.GroupName = userInfo.GroupName;
                    user.IsDownLoad = userInfo.IsDownLoad;
                    user.UserId = userInfo.UserId;//用户表中的ID替换AD中的ID。
                    user.GroupId = userInfo.GroupId;
                }

                //用户Session信息
                string userData = string.Empty;
                userData = user.UserId + "," + user.GroupId + "," + loginName + "," + user.UserName + "," + user.GroupName + "," + user.IsDownLoad;


                FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1,
                    loginName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(WebUI.UIBiz.CommonInfo.CookieTimeout),
                    false,
                    userData,
                    "/"); //建立身份验证票对象
                string HashTicket = FormsAuthentication.Encrypt(Ticket); //加密序列化验证票为字符串
                HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
                //生成Cookie
                context.Response.Cookies.Add(UserCookie); //输出Cookie

                context.Response.Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                string str = "登录失败!请确认用户名密码输入是否正确！";
                context.Response.Write("<script>alert('"+str+"');window.close();</script>");
            }
            #endregion

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
