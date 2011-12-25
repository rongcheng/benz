using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using QJVRMS.Business;
using QJVRMS.Business.SecurityControl;

namespace WebUI.Modules.Manage
{
    public partial class CatalogManager : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.catalogTree.CatalogSel += new EventHandler(catalogTree_CatalogSel);
            if (!this.IsPostBack)
            {

                if (IsSuperAdmin)
                {
                    BindGroupList();
                }

            }


        }

        /// <summary>
        /// ����
        /// </summary>
        protected void BindGroupList()
        {
            // labGroupOwn.Visible = true;


            groupDDL.Visible = true;

            QJVRMS.Business.IGroup g = QJVRMS.Business.Group.GetRootGroup();

            ListItem li = new ListItem();
            li.Text = g.GroupName;
            li.Value = g.GroupId.ToString();

            this.groupDDL.Items.Add(li);



            //using (DataTable dt = Group.GetGroupList())
            //{
            //    this.groupDDL.DataSource = dt;
            //    this.groupDDL.DataTextField = "GroupName";
            //    this.groupDDL.DataValueField = "GroupID";
            //    this.groupDDL.DataBind();
            //}


        }


        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }

        protected string CurrentGroupName
        {
            get
            {
                return CurrentUser.GroupName;
            }
        }


        void catalogTree_CatalogSel(object sender, EventArgs e)
        {
            TreeView catalogTree = sender as TreeView;
            TreeNode node = catalogTree.SelectedNode;


            if (node.Value == string.Empty)
            {
                this.labCurrentCataName.Text = node.Text + "���������಻���޸ģ�";

                btnDelCata.Visible = false;
                this.txtNowCataName.Enabled = false;
                this.btnModify.Visible = false;

                this.roleList.Visible = false;
            }
            else
            {
                this.labCurrentCataName.Text = node.Text;
                this.btnDelCata.Visible = true;
                this.btnDelCata.Attributes.Add("onclick", "return window.confirm('��ȷ��ɾ�����ࣺ" + this.labCurrentCataName.Text + "��')");
                this.btnModify.Visible = true;


                this.roleList.Visible = true;//�����๦��

                this.txtNowCataName.Enabled = true;
            }


            this.txtNowCataName.Text = node.Text;
            this.txtOrder.Text = node.ToolTip;
            this.hiCurrentCataId.Value = node.Value;
            cataInfo.Visible = true;


            if (this.hiCurrentCataId.Value != string.Empty)
            {

                BindRoleControlList();
            }

            if (node.Parent != null
                && node.Parent.Value != string.Empty)
            {
                btnAddChildCata.Enabled = false;
            }

            userList.DataSource = null;
            userList.DataBind();
        }

        ///// <summary>
        ///// ��ǰ�����û�ʱ�������� 
        ///// </summary>
        protected Guid CurrentGroupId
        {
            get
            {
               // return new Guid(this.groupDDL.SelectedValue);
                if (IsSuperAdmin)
                {
                    return new Guid(this.groupDDL.SelectedValue);
                }

                return CurrentUser.UserGroupId;
            }
        }

        /// <summary>
        /// ���û��鶨�幦��
        /// </summary>
        void BindRoleControlList()
        {

            Dictionary<int, string> methodDict = WebUI.UIBiz.CommonInfo.GetMethodDict();
            //ע���޸� ��Ϊsuperadminʱ
            RoleCollection roles = Role.GetRoleCollection(CurrentGroupId);


            Hashtable roleRules = new Hashtable();

            foreach (Role role in roles)
            {
                ISecurityObject securityObj = new SecurityObject(new Guid(this.hiCurrentCataId.Value), SecurityObjectType.Items);
                List<ObjectRule> rules = new List<ObjectRule>();

                foreach (KeyValuePair<int, string> methodEntry in methodDict)
                {
                    OperatorMethod method = (OperatorMethod)((int)methodEntry.Key);
                    ObjectRule rule = new ObjectRule(securityObj, role, method);

                    rules.Add(rule);
                }

                roleRules.Add(role, rules);

                ObjectRule.CheckRules(rules);
            }

            DataTable roleMethod = new DataTable();

            DataColumn dc = new DataColumn("roleName");
            roleMethod.Columns.Add(dc);

            dc = new DataColumn("roleId");
            roleMethod.Columns.Add(dc);

            foreach (KeyValuePair<int, string> methodEntry in methodDict)
            {
                string mIndex = methodEntry.Key.ToString();
                roleMethod.Columns.Add(mIndex, typeof(bool));

            }


            //foreach (KeyValuePair<int, string> methodEntry in methodDict)
            //{
            //   TemplateField field = new TemplateField();
            //  //  CheckBoxField field = new CheckBoxField();
            //    WebUI.UIBiz.GridViewTempla template = new WebUI.UIBiz.GridViewTempla(ListItemType.Item, string.Empty);

            //    field.HeaderText = methodEntry.Value.ToString();
            //   // field.DataField = methodEntry.Key.ToString();
            //   // field.ReadOnly = false;

            //   field.ItemTemplate = template;
            //    roleGroupList.Columns.Add(field);

            //    DataColumn methodDc = new DataColumn(methodEntry.Key.ToString());
            //    roleMethod.Columns.Add(methodDc);
            //}



            foreach (DictionaryEntry entry in roleRules)
            {
                Role role = entry.Key as Role;
                List<ObjectRule> rules = entry.Value as List<ObjectRule>;

                DataRow dr = roleMethod.NewRow();
                dr["roleName"] = role.RoleName;
                dr["roleId"] = role.RoleId.ToString();

                foreach (IRule rule in rules)
                {
                    string methodKey = ((int)rule.Method).ToString();
                    dr[methodKey] = rule.IsValidate;
                }

                roleMethod.Rows.Add(dr);

            }

            DataView dv = roleMethod.DefaultView;
            dv.Sort = "RoleName";
            roleGroupList.DataSource = dv;
            roleGroupList.DataBind();



            //   TemplateColumn roleColumn = new TemplateColumn();

        }

        protected void roleGroupList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }



        protected void roleGroupList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.roleGroupList.EditIndex = e.NewEditIndex;
        }


        /// <summary>
        /// �޸ķ�������Ϣ(�������಻���޸�)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModify_Click(object sender, EventArgs e)
        {
            if (this.hiCurrentCataId.Value == string.Empty)
            {
                ShowMessage("�޸ķ���ʧ��");
                return;
            }

            Guid currentCataId = new Guid(this.hiCurrentCataId.Value);
            string newcataName = this.txtNowCataName.Text.Trim();
            if (newcataName == string.Empty)
            {
                ShowMessage("������Ʋ���Ϊ��");
                return;
            }
            string newcataOrder = this.txtOrder.Text.Trim();
            if (newcataOrder == string.Empty)
            {
                ShowMessage("����Ų���Ϊ��");
                return;
            }
            if (Catalog.ModifyCatalog(currentCataId, newcataName, newcataOrder, string.Empty))
            {
                this.labCurrentCataName.Text = newcataName;
                this.catalogTree.CurrentSelNode.Text = newcataName;
                WebUI.UserControls.CatalogMenu.cataTable = null;
                ShowMessage("�޸ķ���ɹ�");
            }
            else
            {
                ShowMessage("�޸ķ���ʧ��");
            }
        }

        //ɾ����ǰ����
        protected void btnDelCata_Click(object sender, EventArgs e)
        {
            if (this.hiCurrentCataId.Value == string.Empty)
            {
                ShowMessage("ɾ������ʧ��");
                return;
            }

            Guid currentCataId = new Guid(this.hiCurrentCataId.Value);

            if (Catalog.DeleteCatalog(currentCataId))
            {
                this.catalogTree.CurrentSelNode.Parent.ChildNodes.Remove(this.catalogTree.CurrentSelNode);
                this.cataInfo.Visible = false; //�ر����޸Ĺ���
                this.roleList.Visible = false;//�ر��û���Ȩ�޹���

                this.hiCurrentCataId.Value = string.Empty;
                this.labCurrentCataName.Text = string.Empty;
                WebUI.UserControls.CatalogMenu.cataTable = null;


                ShowMessage("ɾ������ɹ�");
            }
            else
            {
                ShowMessage("ɾ������ʧ��");
            }
        }


        /// <summary>
        /// ����ӷ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddChildCata_Click(object sender, EventArgs e)
        {
            Guid parentId = Guid.Empty;

            if (this.hiCurrentCataId.Value != string.Empty)
            {
                parentId = new Guid(this.hiCurrentCataId.Value);
            }

            string catalogName = this.txtChildCataName.Text.Trim();
            if (catalogName == string.Empty)
            {
                ShowMessage("������Ʋ���Ϊ��");
                return;
            }


            Catalog catalog = Catalog.CreateCatalog(catalogName, parentId, string.Empty);
            if (catalog != null)
            {
                this.catalogTree.CurrentSelNode.Expanded = true;
                TreeNode node = new TreeNode();
                node.Text = catalogName;
                node.Value = catalog.CatalogId.ToString();
                this.catalogTree.CurrentSelNode.ChildNodes.Add(node);

                this.txtChildCataName.Text = string.Empty;
                WebUI.UserControls.CatalogMenu.cataTable = null;
                ShowMessage("�������ɹ�");
            }

        }


        protected void groupDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.txtNowCataName.Text = string.Empty;

            //cataInfo.Visible = false;
            //this.btnDelCata.Visible = false;


            BindRoleControlList();
        }


        //�趨Ȩ��
        protected void btnSetRoleFun_Click(object sender, EventArgs e)
        {
            List<ObjectRule> rules = new List<ObjectRule>(100);
            Role role = null;
            SecurityObject secObj = null;

            Guid objId = new Guid(this.hiCurrentCataId.Value);
            secObj = new SecurityObject(objId, SecurityObjectType.Items);
            ArrayList opers = new ArrayList(100);

            foreach (GridViewRow row in roleGroupList.Rows)
            {
                Guid roleId = new Guid(roleGroupList.DataKeys[row.RowIndex].Value.ToString());
                role = new Role();
                role.RoleId = roleId;
                opers.Add(role);

                ObjectRule newRule;

                CheckBox chkRead = row.FindControl("funReadChk") as CheckBox;
                newRule = new ObjectRule(secObj, role, OperatorMethod.Deny);
                rules.Add(newRule);
                newRule.IsValidate = chkRead.Checked;



                CheckBox chkWrite = row.FindControl("funUpChk") as CheckBox;
                newRule = new ObjectRule(secObj, role, OperatorMethod.Write);
                rules.Add(newRule);
                newRule.IsValidate = chkWrite.Checked;


                CheckBox chkEdit = row.FindControl("funEditChk") as CheckBox;
                newRule = new ObjectRule(secObj, role, OperatorMethod.Modify);
                rules.Add(newRule);
                newRule.IsValidate = chkEdit.Checked;

                CheckBox chkDownload = row.FindControl("funDownChk") as CheckBox;
                newRule = new ObjectRule(secObj, role, OperatorMethod.Download);
                rules.Add(newRule);
                newRule.IsValidate = chkDownload.Checked;

                //��Ե�ǰ�����������Ȩ��(����Ӧ�Զ��̳и���Ȩ��)
                DataTable childCatalog = Catalog.GetCatalogTableByParentId(objId);
                foreach (DataRow cata in childCatalog.Rows)
                {
                    SecurityObject cSecObj = new SecurityObject(new Guid(cata["catalogId"].ToString()),
                        SecurityObjectType.Items);

                    ObjectRule cOrRead = new ObjectRule(cSecObj, role, OperatorMethod.Deny);
                    cOrRead.IsValidate = chkRead.Checked;
                    ObjectRule cOrWrite = new ObjectRule(cSecObj, role, OperatorMethod.Write);
                    cOrWrite.IsValidate = chkWrite.Checked;
                    ObjectRule cOrEdit = new ObjectRule(cSecObj, role, OperatorMethod.Modify);
                    cOrEdit.IsValidate = chkEdit.Checked;
                    ObjectRule cOrDown = new ObjectRule(cSecObj, role, OperatorMethod.Download);
                    cOrDown.IsValidate = chkDownload.Checked;

                    rules.Add(cOrRead);
                    rules.Add(cOrWrite);
                    rules.Add(cOrEdit);
                    rules.Add(cOrDown);
                }
              
            }

            if (ObjectRule.SetRules(rules, secObj, opers))
            {
                ShowMessage("��ɫȨ�����óɹ�");
            }
            else
            {
                ShowMessage("��ɫȨ������ʧ��");
            }
        }


        protected void btnSearchUser_Click(object sender, EventArgs e)
        {
            QJVRMS.Business.Group userGroup = new QJVRMS.Business.Group(CurrentGroupId);
            DataTable dt = userGroup.SelectUsers(this.txtloginName.Text.Trim(), this.txtUserName.Text.Trim());



            Hashtable userRules = new Hashtable();
            Dictionary<int, string> methodDict = WebUI.UIBiz.CommonInfo.GetMethodDict();
            foreach (DataRow row in dt.Rows)
            {
                ISecurityObject securityObj = new SecurityObject(new Guid(this.hiCurrentCataId.Value), SecurityObjectType.Items);
                List<ObjectRule> rules = new List<ObjectRule>();
                User user = new User(new Guid(row["userId"].ToString()));

                foreach (KeyValuePair<int, string> methodEntry in methodDict)
                {
                    OperatorMethod method = (OperatorMethod)((int)methodEntry.Key);
                    ObjectRule rule = new ObjectRule(securityObj, user, method);

                    rules.Add(rule);
                }

                userRules.Add(user, rules);

                ObjectRule.CheckRules(rules);
            }


            foreach (KeyValuePair<int, string> methodEntry in methodDict)
            {
                string mIndex = methodEntry.Key.ToString();
                dt.Columns.Add(mIndex, typeof(bool));
            }

            foreach (DictionaryEntry entry in userRules)
            {
                User user = entry.Key as User;
                List<ObjectRule> rules = entry.Value as List<ObjectRule>;

                DataRow[] users = dt.Select("userId='" + user.UserId.ToString() + "'");

                foreach (IRule rule in rules)
                {
                    string methodKey = ((int)rule.Method).ToString();
                    users[0][methodKey] = rule.IsValidate;
                }



            }

            this.userList.DataSource = dt;
            this.userList.DataBind();
        }

        protected void btnSetUserFun_Click(object sender, EventArgs e)
        {
            List<ObjectRule> rules = new List<ObjectRule>(100);
            User user = null;
            SecurityObject secObj = null;

            Guid objId = new Guid(this.hiCurrentCataId.Value);
            secObj = new SecurityObject(objId, SecurityObjectType.Items);
            ArrayList opers = new ArrayList(100);

            foreach (GridViewRow row in userList.Rows)
            {
                Guid userId = new Guid(userList.DataKeys[row.RowIndex].Value.ToString());
                user = new User(userId);
                opers.Add(user);

                ObjectRule newRule;


                CheckBox chk = row.FindControl("funUpChk") as CheckBox;
                newRule = new ObjectRule(secObj, user, OperatorMethod.Write);
                rules.Add(newRule);
                if (chk.Checked)
                {
                    newRule.IsValidate = true;
                }
                else
                {
                    newRule.IsValidate = false;
                }

                CheckBox echk = row.FindControl("funEditChk") as CheckBox;
                newRule = new ObjectRule(secObj, user, OperatorMethod.Modify);
                rules.Add(newRule);
                if (echk.Checked)
                {
                    newRule.IsValidate = true;
                }
                else
                {
                    newRule.IsValidate = false;
                }

                CheckBox dchk = row.FindControl("funReadChk") as CheckBox;
                newRule = new ObjectRule(secObj, user, OperatorMethod.Deny);
                rules.Add(newRule);
                if (dchk.Checked)
                {
                    newRule.IsValidate = true;
                }
                else
                {
                    newRule.IsValidate = false;
                }

                CheckBox downChk = row.FindControl("funDownChk") as CheckBox;
                newRule = new ObjectRule(secObj, user, OperatorMethod.Download);
                rules.Add(newRule);
                newRule.IsValidate = downChk.Checked;

                //��Ե�ǰ�����������Ȩ��(����Ӧ�Զ��̳и���Ȩ��)
                DataTable childCatalog = Catalog.GetCatalogTableByParentId(objId);
                foreach (DataRow cata in childCatalog.Rows)
                {
                    SecurityObject cSecObj = new SecurityObject(new Guid(cata["catalogId"].ToString()),
                        SecurityObjectType.Items);

                    ObjectRule cOrUp = new ObjectRule(cSecObj, user, OperatorMethod.Write);
                    cOrUp.IsValidate = chk.Checked;


                    rules.Add(cOrUp);

                    ObjectRule cOrEdit = new ObjectRule(cSecObj, user, OperatorMethod.Modify);
                    cOrEdit.IsValidate = echk.Checked;

                    rules.Add(cOrEdit);


                    ObjectRule cOrDeny = new ObjectRule(cSecObj, user, OperatorMethod.Deny);
                    cOrDeny.IsValidate = dchk.Checked;
                    rules.Add(cOrDeny);


                    ObjectRule cOrDown = new ObjectRule(cSecObj, user, OperatorMethod.Download);
                    cOrDown.IsValidate = downChk.Checked;
                    rules.Add(cOrDown);

                    
                }

            }

            if (ObjectRule.SetRules(rules, secObj, opers))
            {
                ShowMessage("�û�Ȩ�����óɹ�");
            }
            else
            {
                ShowMessage("�û�Ȩ������ʧ��");
            }
        }


    }
}
