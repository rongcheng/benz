using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using WebUI.UIBiz;
using QJVRMS.Business;
using QJVRMS.Business.SecurityControl;

namespace WebUI.Modules.Manage
{
    public partial class UserManager : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.deptTree.GroupSel += new EventHandler(deptTree_GroupSel);
            if (!this.IsPostBack)
            {
                ////����ǳ�������Ա ��ʾ�����
                if (IsSuperAdmin)
                {
                    //this.groupList.Visible = true;
                    BindGroupList();

                }

                ShowUserManager();

            }            
        }

        void deptTree_GroupSel(object sender, EventArgs e)
        {
            TreeView deptTree = sender as TreeView;
            TreeNode node = deptTree.SelectedNode;

            this.lbDeptName.Text = node.Text;
            this.hiSelDeptId.Value = node.Value;
        }


        protected void BindGroupList()
        {
            // this.groupList.DataSource = QJVRMS.Business.Group.GetRootGroup();//QJVRMS.Business.Group.GetGroupList();
            //this.groupList.DataTextField = "groupname";
            //this.groupList.DataValueField = "groupId";
            //this.groupList.DataBind();

            QJVRMS.Business.IGroup g = QJVRMS.Business.Group.GetRootGroup();

            ListItem li = new ListItem();
            li.Text = g.GroupName;
            li.Value = g.GroupId.ToString();

            this.groupList.Items.Add(li);
        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }

        #region �û��б��

        protected void ShowUserManager()
        {
            QJVRMS.Business.Group userGroup = new QJVRMS.Business.Group(CurrentGroupId);

            string loginName = this.txtSeaLoginName.Text.Trim().Replace("'", "''");
            string userName = this.txtSeaName.Text.Trim().Replace("'", "''");

            string sql = string.Empty;

            if (loginName != string.Empty)
            {
                sql += " loginName like '%" + loginName + "%'";
            }

            if (userName != string.Empty)
            {
                if (sql != string.Empty)
                    sql += " and UserName like '%" + userName + "%'";
                else
                    sql += "  UserName like '%" + userName + "%'";
            }


            if (!IsSuperAdmin)
            {
                if (sql != string.Empty)
                    sql += " and UserId <> '" + CommonInfo.SuperAdminId + "'";
                else
                    sql += "  UserId <> '" + CommonInfo.SuperAdminId + "'";
            }

            if (string.IsNullOrEmpty(sql))
            {
                sql += "  IsLocked="+this.Status.SelectedValue;
            }
            else
            {
                sql += " And IsLocked="+this.Status.SelectedValue;
            }
            DataTable dt = userGroup.SelectUsers(sql);

            dt.DefaultView.Sort = "UserName ASC";
            DataTable dtTemp = dt.DefaultView.ToTable();


            this.userList.DataSource = dtTemp;
            this.userList.DataBind();
        }


        protected void userList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView user = e.Row.DataItem as DataRowView;

                string status = user["IsLocked"].ToString();



                Label lab = e.Row.FindControl("labStatus") as Label;

                if (status.ToLower() != "true")
                {
                    lab.Text = "��Ч";
                }
                else
                {
                    lab.Text = "��Ч";
                    lab.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        #endregion

        /// <summary>
        /// ��ǰ�����û�ʱ��������
        /// </summary>
        protected Guid CurrentGroupId
        {
            get
            {

                //return new Guid(this.groupList.SelectedValue);
                if (IsSuperAdmin)
                {
                    return new Guid(this.groupList.SelectedValue);
                }

                return CurrentUser.UserGroupId;
            }
        }

        protected Guid SelDeptId
        {
            get
            {
                if (this.hiSelDeptId.Value == string.Empty)
                {
                    return CurrentGroupId;
                }

                return new Guid(this.hiSelDeptId.Value);
            }
        }

        ///// <summary>
        ///// ����
        ///// </summary>
        //protected void BindGroupList()
        //{
        //    labGroupOwn.Visible = true;
        //    groupDDL.Visible = true;
        //    using (DataTable dt = Group.GetGroupList())
        //    {
        //        this.groupDDL.DataSource = dt;
        //        this.groupDDL.DataTextField = "GroupName";
        //        this.groupDDL.DataValueField = "GroupID";
        //        this.groupDDL.DataBind();
        //    }


        //}

        /// <summary>
        /// �����е��û���
        /// </summary>
        protected void BindRoleList()
        {
            RoleCollection rc = Role.GetRoleCollection(CurrentGroupId);
            QJVRMS.Business.User user = new QJVRMS.Business.User(new Guid(this.hiUserId.Value));
            RoleCollection rcUser = user.Roles;


            DataTable dt = new DataTable();
            dt.Columns.Add("roleId", typeof(Guid));
            dt.Columns.Add("roleName", typeof(string));
            dt.Columns.Add("chked", typeof(bool));

            foreach (Role role in rc)
            {
                DataRow newRow = dt.NewRow();
                bool chk = false;
                if (rcUser[role.RoleId] != null)
                {
                    chk = true;
                }

                newRow["roleId"] = role.RoleId;
                newRow["roleName"] = role.RoleName;
                newRow["chked"] = chk;

                dt.Rows.Add(newRow);
            }

            this.roleList.DataSource = dt;
            this.roleList.DataBind();
        }
        //ɾ���û�
        protected void userList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Guid userId = new Guid(userList.DataKeys[e.RowIndex].Value.ToString());

            if (userId != CommonInfo.SuperAdminId)
            {
                if (QJVRMS.Business.MemberShipManager.DeleteUser(userId))
                {
                    ShowUserManager();
                    this.userList.SelectedIndex = -1;
                    ShowMessage("�û�ɾ���ɹ�");
                }
                else
                {
                    ShowMessage("�û�ɾ��ʧ��");
                }
            }
            else
                ShowMessage("���û�����ɾ��");
        }

        /// <summary>
        /// ���û����б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void roleList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rule = e.Row.DataItem as DataRowView;

                string status = rule["chked"].ToString();



                CheckBox chk = e.Row.FindControl("chkRole") as CheckBox;

                if (status.ToLower() == "true")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
            }
        }


        #region �û��½�,�༭,����


        /*
            �����AD�û��κ��޸Ķ���Ӱ��AD������Ϣ 
         */

        //�����û���
        protected void btnSetRole_Click(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();

            foreach (GridViewRow row in this.roleList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = row.FindControl("chkRole") as CheckBox;

                    if (chk != null
                        && chk.Checked)
                    {
                        Guid roleId = new Guid(this.roleList.DataKeys[row.RowIndex].Value.ToString());
                        al.Add(roleId);
                    }
                }
            }

            if (Role.CreateRoleUsers((Guid[])al.ToArray(typeof(Guid)), new Guid(this.hiUserId.Value)))
            {
                // this.InfoShow1.InfoText = "�����û���ɫ�ɹ�";
                ShowMessage("�����û���ɫ�ɹ�");
            }
        }

        /// <summary>
        /// ѡ���û����༭�û�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void userList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            userOtherInfo.Visible = true;
            this.btnSaveNewUser.Visible = false;
            this.btnSaveUserInfo.Visible = true;
            roleSetPan.Visible = true;

            if (!WebUI.UIBiz.CommonInfo.AuthByAD)
            {
                this.resetPwdPan.Visible = true;
            }


            Guid userId;
            string userIdStr = this.userList.DataKeys[e.NewSelectedIndex].Value.ToString();
            userId = new Guid(userIdStr);
            this.hiUserId.Value = userIdStr;
            this.UserInfo.LoadUserInfo(userId);
            this.UserInfo.txtLoginName.Enabled = false;

            this.lbDeptName.Text = this.UserInfo.GroupName;
            this.hiSelDeptId.Value = this.UserInfo.GroupId.ToString();

            BindRoleList();
        }


        //׼���½��û�
        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            //if (this.groupList.SelectedItem.Text == "�ڲ��û�")
            //{
            //    ShowMessage(this, "�ڲ��û������ڴ����!");
            //    return;
            //}

            userOtherInfo.Visible = true;
            this.btnSaveNewUser.Visible = true;
            this.btnSaveUserInfo.Visible = false;
            this.resetPwdPan.Visible = false;
            //  this.btnSelGroup.Visible = false;
            roleSetPan.Visible = false;
            //this.UserInfo.txtLoginName.ReadOnly = false;
            this.UserInfo.txtLoginName.Enabled = true;
            this.lbDeptName.Text = string.Empty;
            this.hiSelDeptId.Value = string.Empty;
            UserInfo.InitUserInfo();
        }

        //�½��û� 
        protected void btnSaveNewUser_Click(object sender, EventArgs e)
        {

            EditUserInfo(true);

        }

        //�����û���Ϣ
        protected void btnSaveUserInfo_Click(object sender, EventArgs e)
        {
            EditUserInfo(false);
        }

        protected void EditUserInfo(bool isCreated)
        {
            string loginName = UserInfo.LoginName;
            string userName = UserInfo.UserName;
            string pwd = UserInfo.Password;

            if (pwd == string.Empty) pwd = "abcd1234";

            string email = UserInfo.Email;
            string tel = UserInfo.Tel;
            bool islocked = UserInfo.IsLocked;
            bool isIPValidate = UserInfo.IsIPValidate;
            string isdownload = UserInfo.IsDownLoad;




            //�½��û�
            if (isCreated)
            {
                //
                try
                {
                    MemberShipManager msm = new MemberShipManager();
                    if (msm.IsUserExist(loginName))
                    {
                        ShowMessage("���û����Ѵ���");
                    }
                    else
                    {

                        IUser user = msm.CreateUser(pwd, loginName, userName, SelDeptId, email, tel, islocked, isdownload, isIPValidate);

                        if (user == null) throw new Exception("Error");

                        ShowMessage("�½��û��ɹ�");

                        this.ShowUserManager();

                    }
                }
                catch (Exception ex)
                {
                    QJVRMS.Common.LogWriter.WriteExceptionLog(ex, true);
                    ShowMessage("�½��û�ʧ��");
                }
            }
            else
            {
                MemberShipManager msm = new MemberShipManager();

                if (msm.ModifyUserInfo(new Guid(this.hiUserId.Value), SelDeptId, userName, email, tel, islocked, isdownload, isIPValidate))
                {
                    ShowMessage("�޸��û���Ϣ�ɹ�");
                    this.ShowUserManager();
                }
                else
                {
                    ShowMessage("�޸��û���Ϣʧ��");
                }


            }
        }


        /// <summary>
        /// �û�����
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.userOtherInfo.Visible = false;
            ShowUserManager();
            // BindRoleList();
        }


        protected void userList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.userList.PageIndex = e.NewPageIndex;
            ShowUserManager();
        }



        //��������
        protected void btnResetPwd_Click(object sender, EventArgs e)
        {
            string pwd = this.txtNewPwd.Text.Trim();
            string repwd = this.txtReNewPwd.Text.Trim();

            if (pwd != repwd)
            {
                ShowMessage("���벻һ��");
                return;
            }
            Regex reg = new Regex(@"(^(\w){3,10}$)");
            if (!reg.IsMatch(pwd))
            {
                ShowMessage("����ֻ������3-10����ĸ�����֡��»���");
                return;

            }
            MemberShipManager msm = new MemberShipManager();

            try
            {
                if (msm.ResetPassword(new Guid(this.hiUserId.Value), pwd))
                {
                    ShowMessage("��������ɹ�");
                }
                else
                {
                    ShowMessage("��������ʧ��");
                }

            }
            catch
            {
                ShowMessage("��������ʧ��");
            }

        }


        #endregion


    }
}
