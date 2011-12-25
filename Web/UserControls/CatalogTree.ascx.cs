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
using System.Collections.Generic;

namespace WebUI.UserControls
{
    public partial class CatalogTree : BaseUserControl
    {
     //   private Guid groupId;
        private TreeNode m_RootNode;
        public event EventHandler CatalogSel;

        private bool setUploadRight = false;
        private DataTable uploadCatas = null;
        List<string> catalogList = new List<string>();

        public bool UploadRight
        {
            set { this.setUploadRight = value; }
            get { return this.setUploadRight; }
        }

        public string ImagesItemId
        {
            set
            {
                ViewState["ItemId"] = value;
                catalogList = GetCatalog(ViewState["ItemId"].ToString());
            }
        }

        public DataTable CatasOfRight
        {
            get
            {

                if (IsSuperAdmin) return null;

                if (this.ViewState["cataRight"] == null)
                {
                    DataTable dt = QJVRMS.Business.Catalog.GetCatalogByMethod(CurrentUser.UserId,
                     QJVRMS.Business.SecurityControl.OperatorMethod.Write);

                    this.ViewState["cataRight"] = dt;
                }

                return this.ViewState["cataRight"] as DataTable;
            }
        }

        private List<string> GetCatalog(string itemId)
        {

            //DataSet ds = ImageStorageClass.GetCatalogByItemId(itemId);


            DataSet ds = new Resource().GetResourceCatalogByItemId(itemId);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                catalogList.Add(row["CatalogId"].ToString());
            }
            return catalogList;
        }

        public TreeNodeTypes TreeNodeType
        {
            set { this.cataTreeView.ShowCheckBoxes = value; }
        }

        public string RootNodeName
        {
            set
            {
                this.ViewState["RootName"] = value;
            }
        }

        public TreeNode RootNode
        {
            set
            {
                this.m_RootNode = value;
            }
            get
            {
                return this.cataTreeView.Nodes[0];
            }
        }


        public TreeNode CurrentSelNode
        {
            get { return this.cataTreeView.SelectedNode; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindCataTree();
            }
        }

    
        protected void BindCataTree()
        {
         
            #region 绑定类的根节点

            TreeNode rootNode = new TreeNode();

            if (this.ViewState["RootName"] != null)
                rootNode.Text = this.ViewState["RootName"].ToString();
            else
                rootNode.Text = "分类目录";

            rootNode.Value = string.Empty;
            rootNode.NavigateUrl = string.Empty;
            
            this.cataTreeView.Nodes.Add(rootNode);
            #endregion

            DataTable cataTable = Catalog.GetTopCatalog(); 

        

            if (cataTable.Rows.Count == 0) return;

            DataRow[] firstNodes = cataTable.Select("parentid is null");

            


            foreach (DataRow dr in firstNodes)
            {
                string catalogName = dr["CatalogName"].ToString();
                string catalogId = dr["CatalogId"].ToString();
                string catalogOrder = dr["CatalogOrder"].ToString();

                if (CatasOfRight != null
                    && UploadRight
                    && CatasOfRight.Select("ObjectId='" + catalogId + "'").Length == 0)
                {
                    continue;
                }

                TreeNode node = new TreeNode();
                node.Text = catalogName;
                node.Value = catalogId;
                node.ToolTip = catalogOrder;
                rootNode.ChildNodes.Add(node);
                GetChildNodes(node);
            }   
 
        }

        protected void cataTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeView catalogTree = sender as TreeView;
            TreeNode node = catalogTree.SelectedNode;


            if (CatalogSel != null)
            {
                CatalogSel(sender, e);
            }

            if (node.ChildNodes.Count > 0
              || node.Value == string.Empty) return;
             
        
          

            GetChildNodes(node);
        }

        /// <summary>
        /// 子节点绑定
        /// </summary>
        /// <param name="parent"></param>
        protected void GetChildNodes(TreeNode parent)
        {
            Guid parenId = Guid.Empty;
            if (parent.Value != string.Empty)
                parenId = new Guid(parent.Value.ToString());


            using (DataTable dt = Catalog.GetCatalogTableByParentId(parenId))
            {
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["CatalogName"].ToString();
                    string id = row["CatalogId"].ToString().ToLower();
                    string order = row["CatalogOrder"].ToString();

                    if (CatasOfRight != null
                        && UploadRight
                        && CatasOfRight.Select("ObjectId='" + id + "'").Length == 0)
                    {
                        continue;
                    }


                    TreeNode node = new TreeNode();
                    node.Text = name;
                    node.Value = id;
                    node.ToolTip = order;

                    
                    if (ArrSelectedCheckBoxValue.Contains(id))
                    {
                        node.Checked = true;                        
                    }

                    if (catalogList.Contains(id))
                    {
                        node.Checked = true;
                    }
                
                    parent.Expanded = true;
                    parent.ChildNodes.Add(node);
 
                }
                
            }
        }


        //08-5-26  王玉伟
        public ArrayList ArrCheckbox(ArrayList Arrchk,TreeNode  parentNode)
        {
            int childcount=parentNode.ChildNodes.Count;

    
            for (int i = 0; i < childcount; i++)
            {
                 if (parentNode.ChildNodes[i].Checked)
                     Arrchk.Add(parentNode.ChildNodes[i]);

                 if (parentNode.ChildNodes[i].ChildNodes.Count > 0)
                 {
                     ArrCheckbox(Arrchk,parentNode.ChildNodes[i]);
                 }
            }

            return Arrchk;
        }


        /// <summary>
        /// 定义了默认的选中的分类
        /// </summary>
        public  ArrayList ArrSelectedCheckBoxValue=new ArrayList();
    }
}