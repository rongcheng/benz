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

            #region ��½ʹ�÷���
            MemberShipManager msm = new MemberShipManager();
            object temp = null;


            bool isValidate = false;

            //Form ��֤ 
            if (!CommonInfo.AuthByAD)
            {
                isValidate = msm.AuthUserByForm(loginName,
                    password,
                    Request.UserHostAddress,
                    ref temp);
            }//AD ��֤
            else
            {
                isValidate = msm.AuthUserByAD(CommonInfo.DomainName,
                    CommonInfo.DomainNamePrefix + @"\" + loginName,
                    loginName, password,
                    ref temp);
            }


            if (isValidate)
            {
                //ת��Ϊҵ�����
                QJVRMS.Business.User user = temp as QJVRMS.Business.User;


                //AD��֤ ��ȡ�û���ϵͳ�е�������Ϣ 
                if (CommonInfo.AuthByAD)
                {
                    User userInfo = msm.GetUser(loginName);

                    user.GroupName = userInfo.GroupName;
                    user.IsDownLoad = userInfo.IsDownLoad;
                    user.UserId = userInfo.UserId;//�û����е�ID�滻AD�е�ID��
                    user.GroupId = userInfo.GroupId;
                }

                //�û�Session��Ϣ
                string userData = string.Empty;
                userData = user.UserId + "," + user.GroupId + "," + loginName + "," + user.UserName + "," + user.GroupName + "," + user.IsDownLoad;


                FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1,
                    loginName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(WebUI.UIBiz.CommonInfo.CookieTimeout),
                    false,
                    userData,
                    "/"); //���������֤Ʊ����
                string HashTicket = FormsAuthentication.Encrypt(Ticket); //�������л���֤ƱΪ�ַ���
                HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
                //����Cookie
                Context.Response.Cookies.Add(UserCookie); //���Cookie

                Response.Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                string str = "��¼ʧ��!��ȷ���û������������Ƿ���ȷ��";
                Label2.Text = str;
            }
            #endregion
        }
    }
}