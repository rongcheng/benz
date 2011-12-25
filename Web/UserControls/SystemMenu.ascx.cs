using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using QJVRMS.Business.SecurityControl;

namespace WebUI.UserControls
{
    public partial class SystemMenu :BaseUserControl
    {
        /// <summary>
        /// 缓存分类数据
        /// </summary>
        public static DataTable cataTable = null;
        protected System.Text.StringBuilder html = null;

        protected void GetCataTable()
        {
            if (cataTable == null)
            {
                cataTable = QJVRMS.Business.Function.GetFunctionTableList();
            }
            cataTable = QJVRMS.Business.Function.GetFunctionTableList();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //  if (!this.IsPostBack)
            {
                GetCataTable();
                if (cataTable.Rows.Count == 0) return;






                DataRow[] firstNodes = cataTable.Select("parentid is null", "OrderFlag");

                Int16 menuIndex = 0;

                html = new System.Text.StringBuilder();
                foreach (DataRow dr in firstNodes)
                {
                    string cataId = dr["FunctionID"].ToString();//ItemArray[0].ToString();

                    if (true)
                    {
                      
                        html.Append("<div class=\"AccordionPanel\"><div class=\"AccordionPanelTab\">");

                        html.Append(dr["Functionname"].ToString());
                        html.Append("</div>");

                        GenerateMenu(cataTable, cataId, html, menuIndex);
                        html.Append("</div>");
                        
                        menuIndex++;
                    }


                }

 
            }


        }

        protected string MIndex
        {
            get
            {
                int temp = 0;
                if (!string.IsNullOrEmpty(Request["mi"]))
                {

                    int.TryParse(Request["mi"], out temp);
                    // this.cataMenu.SelectedIndex = temp;
                }

                return temp.ToString();
            }
        }

        protected void GenerateMenu(DataTable cataTable, string parentId, System.Text.StringBuilder html, Int16 menuIndex)
        {

            DataRow[] childNodes = cataTable.Select("parentid='" + parentId + "'", "OrderFlag");




            Hashtable rowRule = null;
            if (!IsSuperAdmin)
            {
                List<ObjectRule> rules = new List<ObjectRule>(childNodes.Length);
                rowRule = new Hashtable(childNodes.Length);
                foreach (DataRow row in childNodes)
                {
                    ISecurityObject secObj = new SecurityObject(new Guid(row["FunctionId"].ToString()), SecurityObjectType.Function);
                    ObjectRule rule = new ObjectRule(secObj, CurrentUser, OperatorMethod.Access);
                    rules.Add(rule);
                    rowRule.Add(row, rule);
                }

                ObjectRule.CheckRules(rules);
            }





            html.Append("<div class=\"AccordionPanelContent\">");
            foreach (DataRow dr in childNodes)
            {
                string cataId = dr.ItemArray[0].ToString();

                if (!IsSuperAdmin)
                {
                    IRule rule = rowRule[dr] as IRule;

                    if (!rule.IsValidate)
                    {
                        continue;
                    }
                }


                if (true)
                {
                    string _appPath = Request.ApplicationPath;
                    if (_appPath.Equals("/")) _appPath = "";

                    html.Append("<ul><li><a href='");
                    html.Append(dr["UrlPath"].ToString().Replace("~", _appPath));
                    html.Append("?mi=" + menuIndex.ToString() + "&funId=" + dr["FunctionId"].ToString() + "'>");
                    html.Append(dr.ItemArray[1].ToString());
                    html.Append("</a></li></ul>");

                    //html.Append("<li style='background-color:" + WebUI.UIBiz.CommonInfo.RepeatColor[menuIndex].Split(';')[1] + "'><a href='/ResourceList.aspx?mi=" + menuIndex.ToString() + "&showCata=1&CatalogID=" + cataId + "&showtype=" + dr["showtype"].ToString() + "&cname=" + Server.UrlEncode(dr["catalogName"].ToString()) + "'>");
                    //html.Append(dr.ItemArray[1].ToString());
                    //html.Append("</a></li>");
                    // menuIndex++;
                }
            }
            html.Append("</div>");
        }

    }
}