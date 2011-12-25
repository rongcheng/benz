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
    public partial class DeptDDL : System.Web.UI.UserControl
    {
        public event EventHandler GroupSel;


        public DataTable GroupList
        {
            get
            {
                return DeptTree.groupList;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindDeptDDl();
            }

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Guid SelDeptId
        {
            get { return new Guid(this.deptDDLList.SelectedValue); }
        }

        protected void BindDeptDDl()
        {
            DataRow[] rows = GroupList.Select("parentId is null");

            if (rows.Length > 0)
            {
                DataRow rootRow = rows[0];

                string rootId = rootRow["groupId"].ToString();
                string rootName = rootRow["groupName"].ToString();

                ListItem rootItem = new ListItem();
                rootItem.Text = rootName;
                rootItem.Value = rootId;

                this.deptDDLList.Items.Add(rootItem);

                int index = 0;

                DataRow[] childRows = GroupList.Select("parentId='" + rootId + "'");
                index++;
                foreach (DataRow row in childRows)
                {

                    string id = row["groupId"].ToString();
                    string name = row["groupName"].ToString();

                    ListItem item = new ListItem();
                    item.Text = GetSplit(index) + name;
                    item.Value = id;

                    this.deptDDLList.Items.Add(item);


                    GenChildNodes(id, index);
                }
            }

        }

        public void GenChildNodes(string parentId, int index)
        {
            DataRow[] childRows = GroupList.Select("parentId='" + parentId + "'");
            index++;
            foreach (DataRow row in childRows)
            {

                string id = row["groupId"].ToString();
                string name = row["groupName"].ToString();

                ListItem item = new ListItem();
                item.Text = GetSplit(index) + name;
                item.Value = id;


                this.deptDDLList.Items.Add(item);

                GenChildNodes(id, index);
            }
        }

        protected string GetSplit(int index)
        {
            string split = string.Empty;

            for (int i = 0; i < index; i++)
            {
                split += "¡¡";
            }

            split += "+";

            return split;
        }

        protected void deptDDLList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GroupSel != null)
            {
                GroupSel(sender, e);
            }
        }
    }
}