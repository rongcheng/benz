using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QJVRMS.Business;
using WebUI.UIBiz;
using QJVRMS.Common;

namespace WebUI.UserControls
{
    public partial class UserLogin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Response.End();
            }
        }



        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            string loginName = this.txtloginName.Text.Trim();
            string password = this.txtPassword.Text.Trim();

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
                Context.Response.Cookies.Add(UserCookie); //输出Cookie

                Response.Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                string str = "登录失败!请确认用户名密码输入是否正确！";
                Label2.Text = str;
            }
            #endregion
        }
    }
}