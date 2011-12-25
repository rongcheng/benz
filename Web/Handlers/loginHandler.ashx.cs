using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using QJVRMS.Business;
using WebUI.UIBiz;
using QJVRMS.Common;
using System.Web.Security;
using System.Configuration;
using System.Web.SessionState;

namespace WebUI.Handlers
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class loginHandler : IHttpHandler, IRequiresSessionState 
    {

        /// <summary>
        /// 输出值 0 用户名密码为空 1 成功 2 密码错误
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            
            context.Response.ContentType = "text/html";
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;

            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1441;

            string _userName = Request["userName"];
            string _password = Request["password"];
            string _ret = "0";

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                Response.Write(_ret);
                return;
            }

            #region 登陆使用方法
            MemberShipManager msm = new MemberShipManager();
            object temp = null;
            bool isValidate = false;

            //Form 验证 
            if (!CommonInfo.AuthByAD)
            {
                isValidate = msm.AuthUserByForm(_userName,
                    _password,
                    Request.UserHostAddress,
                    ref temp);
            }//AD 验证
            else
            {
                isValidate = msm.AuthUserByAD(CommonInfo.DomainName,
                    CommonInfo.DomainNamePrefix + @"\" + _userName,
                    _userName, _password,
                    ref temp);
            }


            if (isValidate)
            {
                //转换为业务对象
                QJVRMS.Business.User user = temp as QJVRMS.Business.User;


                //AD验证 获取用户在系统中的特有信息 
                if (CommonInfo.AuthByAD)
                {
                    User userInfo = msm.GetUser(_userName);

                    user.GroupName = userInfo.GroupName;
                    user.IsDownLoad = userInfo.IsDownLoad;
                    user.UserId = userInfo.UserId;//用户表中的ID替换AD中的ID。
                    user.GroupId = userInfo.GroupId;
                }

                //用户Session信息
                string userData = string.Empty;
                userData = user.UserId + "," + user.GroupId + "," + _userName + "," + user.UserName + "," + user.GroupName + "," + user.IsDownLoad;


                FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1,
                    _userName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(WebUI.UIBiz.CommonInfo.CookieTimeout),
                    false,
                    userData,
                    "/"); //建立身份验证票对象
                string HashTicket = FormsAuthentication.Encrypt(Ticket); //加密序列化验证票为字符串
                HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
                UserCookie.Expires = DateTime.Now.AddMonths(1);
                //生成Cookie
                Response.Cookies.Add(UserCookie); //输出Cookie
                //Response.Redirect(FormsAuthentication.DefaultUrl);


                //这里为该用户创立一个默认的收藏夹，用户必须要有一个收藏夹，即使都删除了，下次登录还是会有一个
                Resource r = new Resource();
                r.CreateDefaultLightbox(user.UserId);
                
                _ret = "1";

                LogEntity model = new LogEntity();
                model.id = Guid.NewGuid();
                model.userId = user.UserId;
                model.userName = _userName;
                model.EventType = ((int)LogType.Login).ToString();
                model.EventResult = "成功";
                model.EventContent = "";
                model.IP = HttpContext.Current.Request.UserHostAddress;
                model.AddDate = DateTime.Now;
                new Logs().Add(model);
            }
            else
            {
                _ret = "2"; //用户名密码不正确，或者没有权限

                LogEntity model = new LogEntity();
                model.id = Guid.NewGuid();
                model.userId = new Guid();
                model.userName = _userName;
                model.EventType = ((int)LogType.Login).ToString();
                model.EventResult = "失败";
                model.EventContent = "错误的密码："+_password;
                model.IP = HttpContext.Current.Request.UserHostAddress;
                model.AddDate = DateTime.Now;
                new Logs().Add(model);
            }

            #endregion

            //Response.Write(_ret+":"+context.Session.SessionID);
            Response.Write(_ret);
            Response.End();
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
