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

namespace WebUI.UserControls
{
    public partial class DeptTree :  BaseUserControl
    {
        public event EventHandler GroupSel;

        public static DataTable groupList = QJVRMS.Business.Group.GetGroupList();


        public static DataTable GroupList
        {
            get
            {
                if( groupList == null )
                    groupList = QJVRMS.Business.Group.GetGroupList();

                return groupList;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindDeptTree();
            }
        }

        public int ExpandDepth
        {
            set { this.deptTreeView.ExpandDepth = value; }
        }


        protected void BindDeptTree()
        {
            DataRow[] rows = GroupList.Select("parentId is null");

            if (rows.Length > 0)
            {
                DataRow rootRow = rows[0];

                string rootId = rootRow["groupId"].ToString();
                string rootName = rootRow["groupName"].ToString();

                TreeNode rootNode = new TreeNode();
                rootNode.Text = rootName;
                rootNode.Value = rootId;
                
            

                this.deptTreeView.Nodes.Add(rootNode);

                DataRow[] childRows = GroupList.Select("parentId='" + rootId + "'");

                foreach (DataRow row in childRows)
                {
                    string id = row["groupId"].ToString();
                    string name = row["groupName"].ToString();

                    TreeNode newNode = new TreeNode();
                    newNode.Text = name;
                    newNode.Value = id;

                    rootNode.ChildNodes.Add(newNode);


                    GenChildNodes(newNode);
                }
            }

        }

        public void GenChildNodes(TreeNode parentNode)
        {
            DataRow[] childRows = GroupList.Select("parentId='" + parentNode.Value + "'");

            foreach (DataRow  row in childRows)
            {
                string id = row["groupId"].ToString();
                string name = row["groupName"].ToString();

                TreeNode newNode = new TreeNode();
                newNode.Text = name;
                newNode.Value = id;


                parentNode.ChildNodes.Add(newNode);

                GenChildNodes(newNode);
            }
        }

        public TreeNode CurrentSelNode
        {
            get { return this.deptTreeView.SelectedNode; }
        }

        public TreeNode RootNode
        {
            get { return this.deptTreeView.Nodes[0]; }
        }

        protected void deptTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeView catalogTree = sender as TreeView;
            TreeNode node = catalogTree.SelectedNode;
            //if (node.ChildNodes.Count > 0
            //  || node.Value == string.Empty) return;

            if (GroupSel != null)
            {
                GroupSel(sender, e);
            }
        }
    }
}