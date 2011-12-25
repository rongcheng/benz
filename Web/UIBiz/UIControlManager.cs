using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using QJVRMS.Business;
using QJVRMS.Business.SecurityControl;

namespace WebUI.UIBiz
{
    public class UIControlManager
    {

        /// <summary>
        /// 检查方法入口
        /// </summary>
        /// <param name="funId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool CheckUIFunctionEntrance(Guid funId, IWebUser user)
        {
            ISecurityObject secObj = new SecurityObject(funId, SecurityObjectType.Function);
            ObjectRule or = new ObjectRule(secObj, user, OperatorMethod.Access);
            or.CheckValidate();

            return or.IsValidate;
        }

        /// <summary>
        /// 检查功能
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="user"></param>
        public static void CheckUIMethod(object [] controls, IWebUser user,ISecurityObject secObj)
        {
            string mIndex = null;
          

            Hashtable controlRule = new Hashtable();
            List<ObjectRule> rules = new List<ObjectRule>();
            foreach (object item in controls)
            {
                switch (item.GetType().BaseType.Namespace)
                {
                    case "System.Web.UI.HtmlControls":
                        mIndex = ((HtmlControl)item).Attributes["MIndex"];

                        if (mIndex != null)
                        {
                            OperatorMethod method = (OperatorMethod)int.Parse(mIndex);
                            ObjectRule or = new ObjectRule(secObj, user, method);

                            HtmlControl htmlc = item as HtmlControl;
                            controlRule.Add(htmlc, or);

                            rules.Add(or);
                        }
                      
                        break;
                    case "System.Web.UI.WebControls":
                        mIndex = ((WebControl)item).Attributes["MIndex"];


                        if (mIndex != null)
                        {
                            OperatorMethod method = (OperatorMethod)int.Parse(mIndex);
                            ObjectRule or = new ObjectRule(secObj, user, method);

                            WebControl webc = item as WebControl;
                            controlRule.Add(webc, or);

                            rules.Add(or);
                        }
                     

                        break;
                
                    default:
                        break;
                }

            }

            ObjectRule.CheckRules(rules);

            foreach (DictionaryEntry entry in controlRule)
            {
                ObjectRule rule = entry.Value as ObjectRule;
                Control control = entry.Key as Control;

                if (!rule.IsValidate) control.Visible = false;
            }
        }
    }
 
}
