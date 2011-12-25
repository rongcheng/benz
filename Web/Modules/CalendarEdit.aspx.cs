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
    public partial class CalendarEdit : AuthPage {
        public string calendarId = string.Empty;
        public string time = string.Empty;
        public string id = string.Empty;
        public string name = string.Empty;
        public string param = string.Empty;

        protected void Page_Load(object sender, EventArgs e) {
            time = get_LinkParam("time");
            if (string.IsNullOrEmpty(time))
                time = ViewState["CALENDAREDITTIME"].ToString();
            else
                ViewState["CALENDAREDITTIME"] = time;
            id = time.Replace("-", string.Empty);
            name = CurrentUser.UserLoginName;
            if (!IsPostBack) {
                calendarId = get_LinkParam("calendarId");
                if (string.IsNullOrEmpty(calendarId))
                    calendarId = ViewState["CALENDARID"].ToString();
                else
                    ViewState["CALENDARID"] = calendarId;
                Look(calendarId);
                Update(calendarId);
            }
        }

        private void Look(string calendarId) {
            if (string.IsNullOrEmpty(calendarId))
                return;

            QJVRMS.Business.CalendarFactory calendarFactory = new QJVRMS.Business.CalendarFactory();
            DataTable dt = calendarFactory.GetCalendar(calendarId);

            if (dt == null || dt.Rows.Count == 0)
                return;

            this.lbTheme.Text = dt.Rows[0]["Theme"].ToString();
            this.lbSite.Text = dt.Rows[0]["Site"].ToString();
            this.lbLabel.Text = dt.Rows[0]["Label"].ToString();
            DateTime sDate = Convert.ToDateTime(dt.Rows[0]["StartTime"].ToString());
            DateTime eDate = Convert.ToDateTime(dt.Rows[0]["EndTime"].ToString());
            param = GetString(sDate, eDate);
            if (IsDiffTime(DateTime.Now, sDate))
                this.lbState.Text = "距开始" + GetDiffTime(DateTime.Now, sDate);
            else
                if (IsDiffTime(DateTime.Now, eDate))
                    this.lbState.Text = "正在进行";
                else
                    this.lbState.Text = "已过期" + GetDiffTime(DateTime.Now, eDate);
            this.lbSdate.Text = sDate.ToShortDateString() + " " + sDate.ToShortTimeString();
            this.lbEdate.Text = eDate.ToShortDateString() + " " + eDate.ToShortTimeString();
            this.lbContent.Text = dt.Rows[0]["DContent"].ToString();
        }
        private void Update(string calendarId) {
            if (string.IsNullOrEmpty(calendarId))
                return;

            QJVRMS.Business.CalendarFactory calendarFactory = new QJVRMS.Business.CalendarFactory();
            DataTable dt = calendarFactory.GetCalendar(calendarId);

            if (dt == null || dt.Rows.Count == 0)
                return;

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

        private void Add() {
            this.lbTheme.Text = this.txtTheme.Text;
            this.lbSite.Text = this.txtSite.Text;
            this.lbLabel.Text = this.ddlLabel.SelectedValue;
            this.lbSdate.Text = this.AjaxCalendarS.Text +" "+ this.ddlDTime.SelectedValue;
            this.lbEdate.Text = this.AjaxCalendarE.Text +" "+ this.ddlETime.SelectedValue;
            DateTime sDate = Convert.ToDateTime(this.AjaxCalendarS.Text + " " + this.ddlDTime.SelectedValue);
            DateTime eDate = Convert.ToDateTime(this.AjaxCalendarE.Text + " " + this.ddlETime.SelectedValue);
            param = GetString(sDate, eDate);
            if (IsDiffTime(DateTime.Now, sDate))
                this.lbState.Text = "距开始" + GetDiffTime(DateTime.Now, sDate);
            else
                if (IsDiffTime(DateTime.Now, eDate))
                    this.lbState.Text = "正在进行";
                else
                    this.lbState.Text = "已过期" + GetDiffTime(DateTime.Now, eDate);
            this.lbContent.Text = this.txtContent.Text;
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
            calendarId = ViewState["CALENDARID"].ToString();
            QJVRMS.Business.CalendarFactory calendar = new QJVRMS.Business.CalendarFactory();
            if (calendar.EditCalendar(calendarId, theme, site, label, sDate, sTime, eDate, eTime, content, CurrentUser.UserLoginName)) {
                this.Label1.Text = "成功";
                Add();
            }
            else
                this.Label1.Text = "失败";
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

        private bool IsDiffTime(DateTime beginTime, DateTime endTime) {
            TimeSpan span = endTime - beginTime;
            if (Convert.ToInt32(span.TotalSeconds) >= 0)
                return true;
            return false;
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
        private string GetDiffTime(DateTime beginTime, DateTime endTime) {
            string result = string.Empty;
            bool b = false;
            //获得2时间的时间间隔秒计算   
            TimeSpan span = endTime - beginTime;
            int iTatol = Convert.ToInt32(span.TotalSeconds);
            if (iTatol < 0) {
                b = true;
                iTatol = int.Parse(iTatol.ToString().Replace("-", string.Empty));
            }
            int iMinutes = 1 * 60;
            int iHours = iMinutes * 60;
            int iDay = iHours * 24;
            int iMonth = iDay * 30;
            int iYear = iMonth * 12;

            if (iTatol > iYear) {
                result += (iTatol / iYear).ToString() + "年";
                iTatol = iTatol % iYear;
            }
            if (iTatol > iMonth) {
                result += (iTatol / iMonth).ToString() + "月";
                iTatol = iTatol % iMonth;
            }
            if (iTatol > iDay) {
                result += (iTatol / iDay).ToString() + "天";
                iTatol = iTatol % iDay;
            }
            if (iTatol > iHours) {
                result += (iTatol / iHours).ToString() + "小时";
                iTatol = iTatol % iHours;
            }
            if (iTatol > iMinutes) {
                result += (iTatol / iMinutes).ToString() + "分钟";
                iTatol = iTatol % iMinutes;
            }
            //if (b)
            //    result = "-" + result;
            return result;
        }
    }
}
