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
    public partial class CalendarAll : AuthPage{
        public string name = string.Empty;
        protected void Page_Load(object sender, EventArgs e) {
            this.Title = "您的日程安排";
            name = CurrentUser.UserLoginName;
            if (!IsPostBack) {
                BindDropDownList();
                DateTime now = DateTime.Now;
                QJVRMS.Business.CalendarFactory calendarFactory = new QJVRMS.Business.CalendarFactory();
                this.Content.InnerHtml = calendarFactory.SearchCalendarsContent(now.ToShortDateString(), string.Empty,
                    string.Empty, string.Empty, name, 20, 1, 0);
            }
        }

        private void BindDropDownList() {
            int year = DateTime.Now.Year;
            this.ddlYear.Items.Clear();
            for (int i = year - 20; i <= year + 20; i++) {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                this.ddlYear.Items.Add(item);
            }
            this.ddlYear.Items.FindByValue(year.ToString()).Selected = true;
            int month = DateTime.Now.Month;
            this.ddlMonth.Items.Clear();
            for (int i = 1; i <= 12; i++) {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                this.ddlMonth.Items.Add(item);
            }
            this.ddlMonth.Items.FindByValue(month.ToString()).Selected = true;
        }
    }
}
