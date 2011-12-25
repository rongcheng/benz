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
using QJVRMS.Business;
using QJVRMS.Business.SecurityControl;
namespace WebUI.Secure
{
    public partial class ChangePWD : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Label1.Text = "";
        }

        protected void ChangePasswordImageButton_Click(object sender, EventArgs e)
        {
            string oldpwd = this.txtOldPwd.Text.Trim();
            string newpwd = this.NewPassword.Text.Trim();
            string repwd = this.ConfirmNewPassword.Text.Trim();

            if (newpwd != repwd)
            {
                ShowMessage("������������벻һ��!");
                return;
            }

            if (newpwd.Length > 10)
            {
                ShowMessage("���볤�Ȳ��ܴ���10!");
                return;
            }

            MemberShipManager msm = new MemberShipManager();
            System.Guid UserIdGuid = CurrentUser.UserId;
            try
            {
                if (msm.ChangePassword(UserIdGuid, oldpwd, newpwd))
                {
                    this.Label1.Text = "�޸�����ɹ�!";
                }
                else
                {
                    this.Label1.Text = "�޸�����ʧ��!";
                }
            }
            catch
            {
                this.Label1.Text = "�޸�����ʧ��!";
            }
        }

      
    }
}
