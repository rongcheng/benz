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

namespace WebUI.Modules {
    public partial class CalendarFull : AuthPage {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                string time = get_LinkParam("time");
                string name = CurrentUser.UserLoginName;
                QJVRMS.Business.CalendarFactory calendarFactory = new QJVRMS.Business.CalendarFactory();
                this.Content.InnerHtml = calendarFactory.ShowNowCalendars(time, name);
            }
        }

        private string get_LinkParam(string paramname) {
            string paramcontent = string.Empty;

            switch (Request.RequestType) {
                case "POST":
                    if (Request.Form[paramname] != null && Request.Form[paramname].ToString() != string.Empty) {
                        paramcontent = Request.Form[paramname].ToString();
                    }
                    break;
                case "GET":
                    if (Request.QueryString[paramname] != null && Request.QueryString[paramname].ToString() != string.Empty) {
                        paramcontent = HttpUtility.UrlDecode(Request.QueryString[paramname].ToString());
                    }
                    break;
            }

            return paramcontent.Trim();
        }
    }
}
