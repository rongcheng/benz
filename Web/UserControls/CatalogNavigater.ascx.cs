using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using QJVRMS.Business;

namespace WebUI.UserControls
{
    public partial class CatalogNavigater : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = CacheManager.GetItem(CacheManager.CacheType.TopCatalog) as DataTable;

                if (dt != null)
                {
                    StringBuilder ulList = new StringBuilder();
                    ulList.Append("<ul>");
                    foreach (DataRow row in dt.Rows)
                    {
                        ulList.Append("<li>");
                        ulList.Append("<a href=\"");
                        ulList.Append(row["Url"].ToString() + "?rootid=" + row["CatalogId"].ToString());
                        ulList.Append("\">");
                        ulList.Append(row["CatalogName"].ToString());
                        ulList.Append("<br/>");
                        ulList.Append(row["CatalogEnName"].ToString());
                        ulList.Append("</a></li>");
                    }
                    ulList.Append("</ul>");

                    divMenu.InnerHtml = ulList.ToString();
                }
            }
        }
    }
}