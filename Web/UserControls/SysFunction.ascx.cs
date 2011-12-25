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

namespace WebUI.UserControls
{
    /// <summary>
    /// 系统功能导航.显示所有“管理”功能的导航控件
    /// </summary>
    public partial class SysFunction : BaseUserControl
    {
        private static DataTable functionList;

        public event EventHandler menuClick;

        //public int FunctionIndex
        //{
        //    get
        //    {
        //        MenuItem selItem = this.functionMenu.SelectedItem;

        //        if (selItem != null)
        //            return int.Parse(selItem.Value.Split(',')[1]);
        //        else
        //            return -1;
        //    }
        //}




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                functionList = QJVRMS.Business.Function.GetFunctionTableList();
                BindFunction();
            }
        }

        protected void BindFunction()
        {
            Hashtable rowRule = null;
            if (!IsSuperAdmin)
            {
                List<ObjectRule> rules = new List<ObjectRule>(functionList.Rows.Count);
                rowRule = new Hashtable(functionList.Rows.Count);
                foreach (DataRow row in functionList.Rows)
                {
                    ISecurityObject secObj = new SecurityObject(new Guid(row["FunctionId"].ToString()), SecurityObjectType.Function);
                    ObjectRule rule = new ObjectRule(secObj,CurrentUser, OperatorMethod.Access);
                    rules.Add(rule);

                    rowRule.Add(row, rule);
                }

                ObjectRule.CheckRules(rules);
            }


            foreach (DataRow row in functionList.Rows)
            {

                if (!IsSuperAdmin)
                {
                    IRule rule = rowRule[row] as IRule;

                    if (!rule.IsValidate)
                    {
                        continue;
                    }
                }


                MenuItem item = new MenuItem();

                string funName = row["FunctionName"].ToString();
                string funId = row["FunctionId"].ToString();
                string url = row["UrlPath"].ToString();
                string funValue = funId;


                //funName = funName.Replace("资源", "图片");
                item.Text = funName;
                item.Value = funValue;
                item.NavigateUrl = url + "?funId=" + funValue;
                //item.Target = "funContainer";

                if (funId.ToLower().Equals("3190958f-c721-4ece-b7bb-2d807a728848"))
                {
                     //屏蔽来源和用途功能项
                }
                else
                {

                    functionMenu.Items.Add(item);
                }

            }

            if (functionMenu.Items.Count > 0)
                functionMenu.Items[0].Selected = true;
            
        }



        protected void functionMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (menuClick != null)
            {
                menuClick(sender, e);
            }
        }

    }
}