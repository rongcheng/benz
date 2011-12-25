using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebUI.UIBiz;
using QJVRMS.Business;
using QJVRMS.Business.SecurityControl;


namespace WebUI.Modules.Manage
{
    public partial class ADUserManager : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitData();
            }
        }

        protected void InitData()
        {
            this.txtDomainName.Text = CommonInfo.DomainName;
        }


        /// <summary>
        /// check users
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckUser_Click(object sender, EventArgs e)
        {
            string[] userId = this.txtUserIdList.Text.Trim(';').Split(';');


            List<string> checkUser = new List<string>(userId);

            List<User> users = MemberShipManager.CheckUsers(this.txtDomainName.Text,
                this.txtOUName.Text.Trim(),
                this.txtAdmin.Text.Trim(),
                this.txtPwd.Text.Trim(), checkUser);

            //foreach (User var in users)
            //{
            //    Response.Write(var.UserName + "<BR>");
            //}

            btnAddUsers.Visible = users.Count != 0;

            this.userList.DataSource = users;
            this.userList.DataBind();
        }

        protected void btnAddUsers_Click(object sender, EventArgs e)
        {
            QJVRMS.Business.User user = null;
            ArrayList userList = new ArrayList(this.userList.Rows.Count);

            foreach (GridViewRow row in this.userList.Rows)
            {
                string userId = this.userList.DataKeys[row.RowIndex].Value.ToString();
                string userName = row.Cells[0].Text;
                string loginId = row.Cells[1].Text;
                string email = row.Cells[2].Text;

                user = new QJVRMS.Business.User();

                user.UserId = new Guid(userId);
                user.UserName = userName;
                user.UserLoginName = loginId;
                user.Email = email;

              
                userList.Add(user);
            }

            if (QJVRMS.Business.MemberShipManager.AddADUserToDB(userList, new Guid("AE636EC4-1B0F-4BFD-A571-1F4BB66C59F5")))
            {
                ShowMessage("添加AD用户成功!");
            }
            else
            {
                ShowMessage("添加AD用户失败!");
            }
        }
    }



}
