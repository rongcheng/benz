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
    public partial class CalendarManager : AuthPage {
        string type = string.Empty;
        string calendarId = string.Empty;
        string time = string.Empty;
        protected void Page_Load(object sender, EventArgs e) {
            type = get_LinkParam("type");
            if (string.IsNullOrEmpty(type))
                type = ViewState["CALENDARTYPE"].ToString();
            else
                ViewState["CALENDARTYPE"] = type;

            if (!IsPostBack) {
                switch (type) {
                    case "Add":
                        time = get_LinkParam("time");
                        if (string.IsNullOrEmpty(type))
                            time = ViewState["CALENDARTIME"].ToString();
                        else
                            ViewState["CALENDARTIME"] = time;
                        this.AjaxCalendarE.Text = time;
                        this.AjaxCalendarS.Text = time;
                        this.ddlDTime.Items.FindByValue(DateTime.Now.Hour.ToString() + ":00").Selected = true;
                        this.ddlETime.Items.FindByValue((DateTime.Now.Hour + 1).ToString() + ":00").Selected = true;
                        break;
                    case "Update":
                        calendarId = get_LinkParam("calendarId");
                        if (string.IsNullOrEmpty(calendarId))
                            calendarId = ViewState["CALENDARID"].ToString();
                        else
                            ViewState["CALENDARID"] = calendarId;

                        Update(calendarId);
                        break;
                }
            }
        }

        private void Update(string calendarId) {
            if (string.IsNullOrEmpty(calendarId))
                this.Label1.Text = "";

            QJVRMS.Business.CalendarFactory calendarFactory = new QJVRMS.Business.CalendarFactory();
            DataTable dt = calendarFactory.GetCalendar(calendarId);

            if (dt == null || dt.Rows.Count == 0)
                this.Label1.Text = "";

            this.txtTheme.Text = dt.Rows[0]["Theme"].ToString();
            this.txtSite.Text = dt.Rows[0]["Site"].ToString();
            this.ddlLabel.Items.FindByValue(dt.Rows[0]["Label"].ToString()).Selected = true;
            DateTime sDate = Convert.ToDateTime(dt.Rows[0]["StartTime"].ToString());
            DateTime eDate = Convert.ToDateTime(dt.Rows[0]["EndTime"].ToString());
            this.AjaxCalendarS.Text = sDate.ToShortDateString();
            this.AjaxCalendarE.Text = eDate.ToShortDateString();
            this.ddlDTime.Items.FindByValue(sDate.ToShortTimeString()).Selected = true;
            this.ddlETime.Items.FindByValue(eDate.ToShortTimeString()).Selected = true;
            this.txtContent.Text = dt.Rows[0]["DContent"].ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            string theme = this.txtTheme.Text;
            string site = this.txtSite.Text;
            string label = this.ddlLabel.SelectedValue;
            string sDate = this.AjaxCalendarS.Text;
            string eDate = this.AjaxCalendarE.Text;
            string sTime = this.ddlDTime.SelectedValue;
            string eTime = this.ddlETime.SelectedValue;
            string content = this.txtContent.Text;

            if (type == "Add")
                calendarId = Guid.NewGuid().ToString();
            else
                calendarId = ViewState["CALENDARID"].ToString();
            QJVRMS.Business.CalendarFactory calendar = new QJVRMS.Business.CalendarFactory();
            if (calendar.EditCalendar(calendarId, theme, site, label, sDate, sTime, eDate, eTime, content, CurrentUser.UserLoginName)) {
                this.Label1.Text = "成功";
                string t = ViewState["CALENDARTIME"].ToString();
                DateTime sDateTime = Convert.ToDateTime(sDate);
                DateTime eDateTime = Convert.ToDateTime(eDate);
                string param = GetString(sDateTime, eDateTime);
                //Page.RegisterClientScriptBlock("calendar", "<script>add('" + t + "', '" + CurrentUser.UserLoginName + "', '" + t.Replace("-", string.Empty) + "')</script>");
                Page.RegisterClientScriptBlock("calendar", "<script>adds('" + param + "', '" + CurrentUser.UserLoginName + "')</script>");
            }
            else
                this.Label1.Text = "失败";
        }
        private string GetString(DateTime beginTime, DateTime endTime) {
            string result = string.Empty;
            TimeSpan span = endTime - beginTime;
            int iTotal = Convert.ToInt32(span.TotalSeconds);
            int iDay = 1 * 60 * 60 * 24;
            int days = iTotal / iDay + 1;
            for (int i = 0; i < days; i++) {
                result += beginTime.AddDays(Convert.ToDouble(i.ToString())).ToShortDateString() + ";";
            }

            return result;
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
