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
    public partial class DeptGridShow : BaseUserControl
    {

        //protected string LeftList = string.Empty;
        //protected string RightGrid = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                GenLeftDeptList();
            }
        }

        protected void GenLeftDeptList()
        {
            DataTable dt = WebUI.UserControls.DeptTree.GroupList;
            DataRow[] rows = dt.Select("parentId is null");


            if (rows.Length > 0)
            {
                DataRow rootRow = rows[0];

                string rootId = rootRow["groupId"].ToString();
                string rootName = rootRow["groupName"].ToString();

                System.Text.StringBuilder left = new System.Text.StringBuilder();

                left.Append("<DIV class='dept_list'><UL><LI class='on'><A href='#' onclick='SelDept(\""+rootId+"\",0,\""+rootName+"\")'>" + rootName + "</A></LI>");


                DataRow[] childRows = dt.Select("parentId='" + rootId + "'");



                foreach (DataRow row in childRows)
                {
                    string id = row["groupId"].ToString();
                    string name = row["groupName"].ToString();

                    left.Append("<LI><a href='#' onclick='SelDept(\"" + id + "\",1,\"" + name + "\")'>" + name + "</a></LI>");

                    if (RightGrid.Text == string.Empty)
                        GenRightDeptGrid(id);
                }

                left.Append("</UL></DIV>");

               
                LeftList.Text = left.ToString();
                 
            }
        }

        public void GenRightDeptGrid(string parentId)
        {
            DataTable dt = WebUI.UserControls.DeptTree.GroupList;

            DataRow[] childRows = dt.Select("parentId='" + parentId + "'");


            System.Text.StringBuilder right = new System.Text.StringBuilder();

            right.Append("<TABLE class='dept_table' cellPadding=5 width=450 border=0>");

            int index = 1;

            foreach (DataRow row in childRows)
            {

                string id = row["groupId"].ToString();
                string name = row["groupName"].ToString();

                bool isNewLine = index % 2 != 0;

                if (isNewLine)
                {
                    right.Append("<TR>");
                }
                DataRow[] deepRows = dt.Select("parentId='" + id + "'");

                right.Append("<TD valign='top' width='275'>");

                if( deepRows.Length > 0 )
                    right.Append("<H3 style='cursor:hand' onclick='SelDept(\"" + id + "\",0,\"" + name + "\")')'>" + name + "</H3>");
                else
                    right.Append("<a style='cursor:hand' onclick='SelDept(\"" + id + "\",0,\"" + name + "\")')'>" + name + "</a>");

               

                right.Append("<UL>");

                foreach (DataRow d in deepRows)
                {
                    string did = d["groupId"].ToString();
                    string dname = d["groupName"].ToString();

                    right.Append("<LI><a href='#' onclick='SelDept(\"" + did + "\",0,\"" + dname + "\")')'>" + dname + "</a></LI>");
                }

                right.Append("</UL>");
                right.Append("</TD>");

                if (!isNewLine)
                {
                    right.Append("</TR>");
                }

                index++;
            }



            right.Append("</TABLE>");

            RightGrid.Text = right.ToString();
        }
    }
}