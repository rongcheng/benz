using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QJVRMS.Business {
    interface ICalendar {
        string ShowNowCalendars(string nowTime, string creator);
        string ShowCalendars(string nowTime, string creator);
        string ShowCalendar(string calendarId, ref string theme);
        DataTable GetCalendar(string calendarId);
        bool DeleteCalendar(string calendarId);
        bool EditCalendar(string calendarId, string theme, string site, string label,
        string sDate, string sTime, string eDate, string eTime, string content, string creator);
        string ShowHead(int year, int month, string creator);
        string ShowContent(int year, int month, string creator);
        string ShowSingle(string nowTime, string creator);

        string SearchCalendarsContent(string monthTime, string stime, string etime,
            string state, string creator,
            int pageSize, int pageIndex, int type);
    }
}
