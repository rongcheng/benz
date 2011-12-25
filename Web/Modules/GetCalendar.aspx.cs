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
    public partial class GetCalendar : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            string result = string.Empty;

            string type = get_LinkParam("type");
            string calendarId = string.Empty;
            string name = string.Empty;
            QJVRMS.Business.CalendarFactory calendarFactory = new QJVRMS.Business.CalendarFactory();
            switch (type) { 
                case "Show":
                    string nowTime = get_LinkParam("time");
                    name = get_LinkParam("name");
                    result = calendarFactory.ShowNowCalendars(nowTime, name);
                    break;
                case "Delete":
                    calendarId = get_LinkParam("calendarId");
                    if (calendarFactory.DeleteCalendar(calendarId))
                        result = "删除成功;" + calendarId;
                    break;
                case "Content":
                    int year = int.Parse(get_LinkParam("year"));
                    int month = int.Parse(get_LinkParam("month"));
                    name = get_LinkParam("name");
                    result = calendarFactory.ShowContent(year, month, name);
                    break;
                case "Head":
                    int hyear = int.Parse(get_LinkParam("year"));
                    int hmonth = int.Parse(get_LinkParam("month"));
                    name = get_LinkParam("name");
                    result = calendarFactory.ShowHead(hyear, hmonth, name);
                    break;
                case "Single":
                    string sTime = get_LinkParam("time");
                    name = get_LinkParam("name");
                    result = calendarFactory.ShowSingle(sTime, name);
                    break;
                case "Save":
                    string monthtime = get_LinkParam("time");
                    string stime = get_LinkParam("stime");
                    string etime = get_LinkParam("etime");
                    string state = get_LinkParam("state");
                    name = get_LinkParam("name");
                    string t = get_LinkParam("t");
                    result = calendarFactory.SearchCalendarsContent(monthtime, stime, etime,
                        state, name, 20, 1, int.Parse(t));
                    break;
                case "Page":
                    string pmonthtime = get_LinkParam("time");
                    string pstime = get_LinkParam("stime");
                    string petime = get_LinkParam("etime");
                    string pstate = get_LinkParam("state");
                    name = get_LinkParam("name");
                    string pt = get_LinkParam("t");
                    string size = string.IsNullOrEmpty(get_LinkParam("size")) ? "20" : get_LinkParam("size");
                    string index = get_LinkParam("index");
                    result = calendarFactory.SearchCalendarsContent(pmonthtime, pstime, petime,
                        pstate, name, int.Parse(size), int.Parse(index), int.Parse(pt));
                    break;
            }

            Response.Write(result);
            Response.End();
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
