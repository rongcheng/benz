using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

using QJVRMS.Business;
using QJVRMS.Common;

namespace WebUI.Handlers
{
    /// <summary>
    /// CatalogsHandler 的摘要说明
    /// </summary>
    public class CatalogsHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;

            //获取所有直接子和间接子列表
            if (context.Request["action"] == "allsub")
            {
                if (!string.IsNullOrEmpty(context.Request["rootId"]))
                {
                    result = GetAllSubCatalogs(context.Request["rootId"], context.Request["catalogId"]);
                }
            }

            context.Response.Write(result);
        }

        private string GetAllSubCatalogs(string rootId, string catalogId)
        {
            DataTable dt = Catalog.GetAllSubCatalog(rootId);
            StringBuilder result = new StringBuilder();

            result.Append("[");
            foreach (DataRow row in dt.Rows)
            {
                result.Append("{id:\"");
                result.Append(row["catalogid"].ToString());
                result.Append("\",pid:\"");
                result.Append(row["parentid"].ToString());
                result.Append("\",name:\"");
                result.Append(row["catalogname"].ToString());
                result.Append("\",open:true,target:\"_self\",url:\"");
                result.Append(row["Url"].ToString() + "?rootid=" + rootId + "&catalogid=" + row["CatalogId"].ToString());
                result.Append("\"");
                if (!string.IsNullOrEmpty(catalogId)
                    && row["CatalogId"].ToString() == catalogId)
                {
                    result.Append(",color:\"red\"");
                }
                result.Append("},");
            }
            result.Append("]");
            return result.ToString().Replace(",]", "]");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}