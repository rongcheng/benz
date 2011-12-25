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

namespace WebUI.UserControls
{
    public partial class UserInfo : BaseUserControl
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (WebUI.UIBiz.CommonInfo.AuthByAD)
                {
                    this.ReqToPwd.Enabled = false;
                    this.ReqToRePwd.Enabled = false;
                    this.CompareValidator1.Enabled = false;
                }
            }

        }


        // private OperatorMethod method = OperatorMethod.Read;
        private Guid groupId;
        private string groupName;


        public Guid GroupId
        {
            get { return this.groupId; }
        }

        public string GroupName
        {
            get { return this.groupName; }
        }

        public string LoginName
        {
            get { return this.txtLoginName.Text.Trim(); }
        }

        public string UserName
        {
            get { return this.txtUserName.Text.Trim(); }
        }

        public string Password
        {
            get { return this.txtPwd.Text.Trim(); }
        }

        public string RePassword
        {
            get { return this.txtRePwd.Text.Trim(); }
        }

        public string Tel
        {
            get { return this.txtTel.Text.Trim(); }
        }

        public string Email
        {
            get { return this.txtEmail.Text.Trim(); }
        }


        public bool IsLocked
        {
            get { return !radTrue.Checked; }
        }
        public bool IsIPValidate
        {
            get { return this.RadioIPTrue.Checked; }
        }

        public string IsDownLoad
        {
            //get { return radDownTrue.Checked.ToString(); }
            get { return "false"; }
        }

        public OperatorMethod MethodParam
        {
            set
            {
                //OperatorMethod method = value;
                ////this.hiMethodParam.Value = value.ToString();

                //switch (method)
                //{
                //    case OperatorMethod.Read:

                //        break;

                //    case OperatorMethod.Write:
                //        this.btnSaveNewUser.Visible = true;
                //        break;

                //    case OperatorMethod.Modify:
                //        this.oldPwd.Visible = true;
                //        this.btnSaveUserInfo.Visible = true;

                //        LoadUserInfo();
                //        break;

                //    default:
                //        break;
                //}


            }
        }


        public void LoadUserInfo(Guid userId)
        {
            this.pwdPan.Visible = false;
            this.repwdPan.Visible = false;

            MemberShipManager msm = new MemberShipManager();
            QJVRMS.Business.User user = (QJVRMS.Business.User)msm.GetUser(userId);

            this.txtLoginName.Text = user.UserLoginName;
            this.txtUserName.Text = user.UserName;
            this.txtPwd.Text = "abcd1234";
            this.txtRePwd.Text = "abcd1234";

            this.groupId = user.GroupId;
            this.groupName = user.GroupName;
            //this.txtPwd.Enabled = false;
            //this.txtPwd.BackColor = System.Drawing.Color.Gray;
            //this.txtRePwd.BackColor = System.Drawing.Color.Gray;
            //this.txtRePwd.Enabled = false;

            if (CurrentUser.UserId == userId)
            {
                this.radFalse.Enabled = false;
                this.radTrue.Enabled = false;
            }
            else
            {
                this.radFalse.Enabled = true;
                this.radTrue.Enabled = true;
            }


            this.txtEmail.Text = user.Email;
            this.txtTel.Text = user.Telphone;

            if (user.IsLocked)
            {
                this.radTrue.Checked = false;
                this.radFalse.Checked = true;
            }
            else
            {
                this.radTrue.Checked = true;
                this.radFalse.Checked = false;
            }
            if (user.IsIPValidate)
            {
                this.RadioIPTrue.Checked = true;
                this.RadioIPFalse.Checked = false;
            }
            else
            {
                this.RadioIPTrue.Checked = false;
                this.RadioIPFalse.Checked = true;
            }

            //if (bool.Parse(user.IsDownLoad))
            //{
            //    this.radDownTrue.Checked = true;
            //    this.radDownFalse.Checked = false;
            //}
            //else
            //{
            //    this.radDownTrue.Checked = false;
            //    this.radDownFalse.Checked = true;
            //}
        }

       

        public void InitUserInfo()
        {
            this.txtLoginName.Text = "";
            this.txtUserName.Text = "";
            this.txtPwd.Text = "";
            this.txtRePwd.Text = "";
            this.txtTel.Text = "";
            this.txtEmail.Text = "";

            this.radTrue.Enabled = true;
            this.radFalse.Enabled = true;

            this.RadioIPTrue.Enabled = true;
            this.RadioIPFalse.Enabled = true;

            //this.radDownTrue.Enabled = true;
            //this.radDownFalse.Enabled = true;            

            this.pwdPan.Visible = true;
            this.repwdPan.Visible = true;

            //this.radDownTrue.Checked = true;
            this.radTrue.Checked = true;
        }
    }
}