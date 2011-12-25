using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WebUI {
    public partial class Calendar : AuthPage {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                string calendarId = get_LinkParam("calendarId");
                QJVRMS.Business.CalendarFactory calendarFactory = new QJVRMS.Business.CalendarFactory();
                string title = string.Empty;
                this.Content.InnerHtml = calendarFactory.ShowCalendar(calendarId, ref title);
                this.Title = title;
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
