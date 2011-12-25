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
using System.Collections.Generic;

namespace WebUI.Modules.Manage
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public partial class RoleGroupManager :  AuthPage
    {

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (IsSuperAdmin)  
                {
                    BindGroupList();
                }

                BindRoleList();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }

        ///// <summary>
        ///// 当前生成用户时，所属组 
        ///// </summary>
        protected Guid CurrentGroupId
        {
            get
            {
                return new Guid(this.groupDDL.SelectedValue);
                //if (IsSuperAdmin)  
                //{
                //    return new Guid(this.groupDDL.SelectedValue);
                //}

                //return CurrentUser.UserGroupId;
           }
        }

        //绑定角色列表
        protected void BindRoleList()
        {
            RoleCollection rc = Role.GetRoleCollection(CurrentGroupId);

            this.roleList.DataSource = rc;
            this.roleList.DataBind();
        }

        /// <summary>
        /// 绑定组
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

        /// <summary>
        /// 绑定用户角色功能
        /// </summary>
        protected void BindRoleFunction()
        {
            this.rolePan.Visible = true;
            DataTable dt =  Function.GetFunctionTableList();

            dt.Columns.Add("roleChk", typeof(bool));

            foreach (DataRow row in dt.Rows)
            {
                row["roleChk"] = false;
            }

            if (this.hiRoleId.Value != string.Empty)
            {
                Role role = new Role();
                role.RoleId = new Guid(this.hiRoleId.Value);
                DataTable roleFun = Function.GetOwnFunction(role, QJVRMS.Business.SecurityControl.OperatorMethod.Access);


                foreach (DataRow row in  dt.Rows)
                {
                    string funId = row["functionId"].ToString();

                    if (roleFun.Select("functionId='" + funId + "'").Length > 0)
                    {
                        row["roleChk"] = true;
                    }
                    
                }
            }
            


            this.functionList.DataSource = dt;
            this.functionList.DataBind();



            //绑定分类 2010-11-22 ciqq
            DataTable dtTopCategory = Catalog.GetTopCatalog();
            this.rptCategoryTop.DataSource = dtTopCategory;
            this.rptCategoryTop.DataBind();


        }

        /// <summary>
        /// 编辑用户角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void roleList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            this.hiRoleId.Value = this.roleList.DataKeys[e.NewSelectedIndex].Value.ToString();

            GridViewRow role = this.roleList.Rows[e.NewSelectedIndex];

            string roleName = role.Cells[0].Text;
            string roleDes = role.Cells[1].Text == "&nbsp;" ? string.Empty : role.Cells[1].Text;

            this.txtRoleName.Text = roleName;
            this.txtRoleDescri.Text = roleDes;
            
            BindRoleFunction();
        }


        //删除用户角色
        protected void roleList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Guid roleId = new Guid(roleList.DataKeys[e.RowIndex].Value.ToString());

            if (Role.DeleteRole(roleId))
            {
                BindRoleList();
                rolePan.Visible = false;
                this.roleList.SelectedIndex = -1;
                ShowMessage("角色删除成功");  
            }
            else
            {
                ShowMessage("角色删除失败");
            }
        }


        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveRoleInfo_Click(object sender, EventArgs e)
        {
            string roleName = this.txtRoleName.Text.Trim().Replace("'", "''");
            string roleDesc = this.txtRoleDescri.Text.Trim().Replace("'", "''");

            ArrayList secObjs = new ArrayList(20);
            foreach (DataListItem  item in this.functionList.Items)
            {
                CheckBox chk = item.FindControl("roleChk") as CheckBox;

                if (chk.Checked)
                {
                    Guid funid = new Guid(this.functionList.DataKeys[item.ItemIndex].ToString());
                    ISecurityObject secObj = new SecurityObject(funid, SecurityObjectType.Function);
                    secObjs.Add(secObj);
                }

            }

            //New Role
            if (this.hiRoleId.Value == string.Empty)
            {
                IRole role = Role.NewRole(CurrentGroupId,
                    roleName,
                    roleDesc,
                    (SecurityObject[])secObjs.ToArray(typeof(SecurityObject)),
                    OperatorMethod.Access);

                if (role != null)
                {
                    rolePan.Visible = false;
                    BindRoleList();
                    ShowMessage( "新建角色成功");
                }
                else
                {
                    ShowMessage("新建角色失败");
                }
            }
            else//Modify Role
            {
                if (Role.ModifyRole(roleName,
                    roleDesc,
                    new Guid(this.hiRoleId.Value),
                    (SecurityObject[])secObjs.ToArray(typeof(SecurityObject)),
                    OperatorMethod.Access))
                {
                    rolePan.Visible = false;
                    BindRoleList();
                    ShowMessage("修改用户角色成功");
                }
                else
                {
                    ShowMessage("修改用户角色失败");
                }
            }
        }

        /// <summary>
        /// 新建用户组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNewRole_Click(object sender, EventArgs e)
        {
            rolePan.Visible = true;
            this.txtRoleDescri.Text = string.Empty;
            this.txtRoleName.Text = string.Empty;
            this.hiRoleId.Value = string.Empty;
            BindRoleFunction();
        }

        /// <summary>
        /// 更换用户组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void groupDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.roleList.SelectedIndex = -1;
            rolePan.Visible = false;
           
            BindRoleList();
        }

        protected void rptCategoryTop_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptCatalogChild = (Repeater)e.Item.FindControl("rptCategoryChild");
                
               
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                //提取分类ID 
                string CategorieId = Convert.ToString(rowv["CatalogID"]);
                //根据分类ID查询该分类下的产品，并绑定产品Repeater 
                rptCatalogChild.DataSource = Catalog.GetCatalogTableByParentId(new Guid(CategorieId));
                rptCatalogChild.DataBind();

                HiddenField hf = (HiddenField)e.Item.FindControl("topCatId");
                hf.Value = CategorieId;



                Role role = new Role(new Guid(this.hiRoleId.Value));
                
                Guid catId = new Guid(CategorieId);                
                ISecurityObject securityObj = new SecurityObject(catId, SecurityObjectType.Items);
    
                CheckBox chb = (CheckBox)e.Item.FindControl("funTopReadChk");
                OperatorMethod method = OperatorMethod.Deny;
                ObjectRule rule = new ObjectRule(securityObj, role, method);
                rule.CheckValidate();
                chb.Checked = rule.IsValidate;

                chb = (CheckBox)e.Item.FindControl("funTopUpChk");
                method = OperatorMethod.Write;
                rule = new ObjectRule(securityObj, role, method);
                rule.CheckValidate();
                chb.Checked = rule.IsValidate;

                chb = (CheckBox)e.Item.FindControl("funTopEditChk");
                method = OperatorMethod.Modify;
                rule = new ObjectRule(securityObj, role, method);
                rule.CheckValidate();
                chb.Checked = rule.IsValidate;

                chb = (CheckBox)e.Item.FindControl("funTopDownChk");
                method = OperatorMethod.Download;
                rule = new ObjectRule(securityObj, role, method);
                rule.CheckValidate();
                chb.Checked = rule.IsValidate;

            } 
        }


        protected void rptCategoryChild_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DataRowView rowv = (DataRowView)e.Item.DataItem;
                string CategorieId = Convert.ToString(rowv["CatalogID"]);
                Role role = new Role(new Guid(this.hiRoleId.Value));
                
                HiddenField hf = (HiddenField)e.Item.FindControl("childCatId");
                hf.Value = CategorieId;
                
                Guid catId = new Guid(CategorieId);
                ISecurityObject securityObj = new SecurityObject(catId, SecurityObjectType.Items);

                CheckBox chb = (CheckBox)e.Item.FindControl("funChildReadChk");
                OperatorMethod method = OperatorMethod.Deny;
                ObjectRule rule = new ObjectRule(securityObj, role, method);
                rule.CheckValidate();
                chb.Checked = rule.IsValidate;

                chb = (CheckBox)e.Item.FindControl("funChildUpChk");
                method = OperatorMethod.Write;
                rule = new ObjectRule(securityObj, role, method);
                rule.CheckValidate();
                chb.Checked = rule.IsValidate;

                chb = (CheckBox)e.Item.FindControl("funChildEditChk");
                method = OperatorMethod.Modify;
                rule = new ObjectRule(securityObj, role, method);
                rule.CheckValidate();
                chb.Checked = rule.IsValidate;

                chb = (CheckBox)e.Item.FindControl("funChildDownChk");
                method = OperatorMethod.Download;
                rule = new ObjectRule(securityObj, role, method);
                rule.CheckValidate();
                chb.Checked = rule.IsValidate;
            }
        }

        protected void btnSetRoles_Click(object sender, EventArgs e)
        {


            List<ObjectRule> rules = new List<ObjectRule>();
            Role role = null;
            SecurityObject secObj = null;
            ObjectRule newRule;



            //设置大分类的角色权限
            foreach (RepeaterItem item in rptCategoryTop.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {

                    
                    HiddenField hf = (HiddenField)item.FindControl("topCatId");
                    string CategorieId=hf.Value ;

                    Guid objId = new Guid(CategorieId);
                    secObj = new SecurityObject(objId, SecurityObjectType.Items);


                    role = new Role(new Guid(this.hiRoleId.Value));

                    CheckBox chkRead = item.FindControl("funTopReadChk") as CheckBox;
                    newRule = new ObjectRule(secObj, role, OperatorMethod.Deny);                    
                    newRule.IsValidate = chkRead.Checked;
                    rules.Add(newRule);

                    CheckBox chkUp = item.FindControl("funTopUpChk") as CheckBox;
                    newRule = new ObjectRule(secObj, role, OperatorMethod.Write);
                    newRule.IsValidate = chkUp.Checked;
                    rules.Add(newRule);


                    CheckBox chkEdit = item.FindControl("funTopEditChk") as CheckBox;
                    newRule = new ObjectRule(secObj, role, OperatorMethod.Modify);
                    newRule.IsValidate = chkEdit.Checked;
                    rules.Add(newRule);


                    CheckBox chkDown = item.FindControl("funTopDownChk") as CheckBox;
                    newRule = new ObjectRule(secObj, role, OperatorMethod.Download);
                    newRule.IsValidate = chkDown.Checked;
                    rules.Add(newRule);


                    //寻找小类

                    Repeater rptCatalogChild = (Repeater)item.FindControl("rptCategoryChild");
                    foreach (RepeaterItem itemChild in rptCatalogChild.Items)
                    {
                        HiddenField hfChild = (HiddenField)itemChild.FindControl("childCatId");
                        string childCategorieId = hfChild.Value;

                        Guid objChildId = new Guid(childCategorieId);
                        secObj = new SecurityObject(objChildId, SecurityObjectType.Items);


                        CheckBox chkReadChild = itemChild.FindControl("funChildReadChk") as CheckBox;
                        newRule = new ObjectRule(secObj, role, OperatorMethod.Deny);
                        newRule.IsValidate = chkReadChild.Checked;
                        rules.Add(newRule);

                        CheckBox chkUpChild = itemChild.FindControl("funChildUpChk") as CheckBox;
                        newRule = new ObjectRule(secObj, role, OperatorMethod.Write);
                        newRule.IsValidate = chkUpChild.Checked;
                        rules.Add(newRule);


                        CheckBox chkEditChild = itemChild.FindControl("funChildEditChk") as CheckBox;
                        newRule = new ObjectRule(secObj, role, OperatorMethod.Modify);
                        newRule.IsValidate = chkEditChild.Checked;
                        rules.Add(newRule);


                        CheckBox chkDownChild = itemChild.FindControl("funChildDownChk") as CheckBox;
                        newRule = new ObjectRule(secObj, role, OperatorMethod.Download);
                        newRule.IsValidate = chkDownChild.Checked;
                        rules.Add(newRule);
                    
                    }
                    

                }                

            }


            




            ArrayList opers = new ArrayList(100);
            if (ObjectRule.SetRules(rules, secObj, opers))
            {
                ShowMessage("角色权限设置成功");
            }
            else
            {
                ShowMessage("角色权限设置失败");
            }
            
            

            
        }

       
    }
}
