using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WebUI.Modules.Manage
{
    public partial class DeptManager : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DeptTree.GroupSel += new EventHandler(DeptTree_GroupSel);


        }

        void DeptTree_GroupSel(object sender, EventArgs e)
        {
            TreeView deptTree = sender as TreeView;
            TreeNode node = deptTree.SelectedNode;
            labCurDeptName.Text = node.Text;
            this.txtCurDeptName.Text = node.Text;
            this.hiSelGroupId.Value = node.Value;
            deptInfo.Visible = true;

        }


        protected void btnModify_Click(object sender, EventArgs e)
        {
            string deptName = this.txtCurDeptName.Text.Trim();
            string orderFlagStr = this.txtOrderFlag.Text.Trim();

            int orderFlag = 0;

            int.TryParse(orderFlagStr, out orderFlag);

            if (orderFlag < 0 || orderFlag > 999) orderFlag = 0;

            if (deptName == string.Empty) return;

            if (QJVRMS.Business.Group.ModifyGroup(new Guid(this.hiSelGroupId.Value), deptName,orderFlag))
            {
                this.DeptTree.CurrentSelNode.Text = deptName;
                WebUI.UserControls.DeptTree.groupList = null;
                ShowMessage("修改成功!");
            }
            else
            {
                ShowMessage("修改失败!");
            }

        }

        protected void btnDelCata_Click(object sender, EventArgs e)
        {
            if (QJVRMS.Business.Group.DeleteGroup(new Guid(this.hiSelGroupId.Value)))
            {
                this.DeptTree.CurrentSelNode.Parent.ChildNodes.Remove(this.DeptTree.CurrentSelNode);
                ShowMessage("部门删除成功!");
                this.deptInfo.Visible = false;
                this.labCurDeptName.Text = string.Empty;
                WebUI.UserControls.DeptTree.groupList = null;
            }
            else
            {
                ShowMessage("部门删除失败!");
            }

        }

        protected void btnAddChildDept_Click(object sender, EventArgs e)
        {
            string groupName = this.txtChildDeptName.Text.Trim();
            string orderFlagStr = this.txtOrderFlag.Text.Trim();

            int orderFlag = 0;

            int.TryParse(orderFlagStr, out orderFlag);

            if (orderFlag < 0 || orderFlag > 999) orderFlag = 0;

            Guid groupId = QJVRMS.Business.Group.CreateChildGroup(new Guid(this.hiSelGroupId.Value), groupName, orderFlag);


            TreeNode node = new TreeNode();
            node.Text = groupName;
            node.Value = groupId.ToString();
            this.DeptTree.CurrentSelNode.ChildNodes.Add(node);
            WebUI.UserControls.DeptTree.groupList = null;
            ShowMessage("部门建立成功!");
            //}
            //else
            //{
            //    ShowMessage("部门建立失败!");
            //}
        }
    }
}
