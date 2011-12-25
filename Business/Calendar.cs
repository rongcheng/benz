using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace QJVRMS.Business {
    class Calendar:ICalendar {
        #region ICalendar 成员
        //显示周信息
        public string ShowHead(int year, int month, string creator) {
            DateTime nowTime = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"f14\" style=\" height:25px;\">");
            sb.Append("<DIV class=\"l\" style=\"MARGIN-TOP: 4px\">");
            if (month == 1) {
                sb.Append("<IMG title=\"前一月\" onclick=\"Show('" + (year - 1).ToString() + "', '12', '"+creator+"')\" style=\"CURSOR: pointer\" src=\"../images/butt_ago.gif\" align=\"absMiddle\" />");
                sb.Append("&nbsp;&nbsp;");
                sb.Append("<IMG title=\"后一月\" onclick=\"Show('" + year.ToString() + "', '" + (month + 1).ToString() + "', '" + creator + "')\" style=\"CURSOR: pointer\" src=\"../images/butt_after.gif\" align=\"absMiddle\" />");
            }
            else if (month == 12) {
                sb.Append("<IMG title=\"前一月\" onclick=\"Show('" + year.ToString() + "', '" + (month - 1).ToString() + "', '" + creator + "')\" style=\"CURSOR: pointer\" src=\"../images/butt_ago.gif\" align=\"absMiddle\" />");
                sb.Append("&nbsp;&nbsp;");
                sb.Append("<IMG title=\"后一月\" onclick=\"Show('" + (year + 1).ToString() + "', '1', '" + creator + "')\" style=\"CURSOR: pointer\" src=\"../images/butt_after.gif\" align=\"absMiddle\" />");
            }
            else {
                sb.Append("<IMG title=\"前一月\" onclick=\"Show('" + year.ToString() + "', '" + (month - 1).ToString() + "', '" + creator + "')\" style=\"CURSOR: pointer\" src=\"../images/butt_ago.gif\" align=\"absMiddle\" />");
                sb.Append("&nbsp;&nbsp;");
                sb.Append("<IMG title=\"后一月\" onclick=\"Show('" + year.ToString() + "', '" + (month + 1).ToString() + "', '" + creator + "')\" style=\"CURSOR: pointer\" src=\"../images/butt_after.gif\" align=\"absMiddle\" />");
            }
            sb.Append("</div>");
            sb.Append("<DIV class=\"l\" style=\"MARGIN-TOP: 2px; MARGIN-LEFT: 3px\">");
            if (nowTime.Year != year || nowTime.Month != month)
                sb.Append("<INPUT style=\"WIDTH: 40px; HEIGHT: 22px\" onclick=\"Show('" + nowTime.Year.ToString() + "', '" + nowTime.Month.ToString() + "', '" + creator + "')\" type=\"button\" value=\"今天\"  />");
            else
                sb.Append("<INPUT style=\"WIDTH: 40px; HEIGHT: 22px\" disabled type=\"button\" value=\"今天\" />");
            sb.Append("</div>");
            sb.Append("<DIV class=\"l\" style=\"MARGIN-TOP: 4px; MARGIN-LEFT: 3px\">" + year.ToString() + " 年 " + month.ToString() + " 月</DIV>");
            sb.Append("<DIV class=\"c\"></DIV>");
            sb.Append("</div>");

            return sb.ToString();
        }

        public string ShowContent(int year, int month, string creator) {
            int total = DateTime.DaysInMonth(year, month);
            int nowDay = DateTime.Now.Day;
            string sTime = year.ToString() + "-" + month.ToString() + "-1";
            string eTime = year.ToString() + "-" + month.ToString() + "-" + total.ToString();

            QJVRMS.Business.CalendarWS.CalendarService calendar = new QJVRMS.Business.CalendarWS.CalendarService();
            DataTable dt = calendar.GetCalendarsMonth(sTime, eTime, creator);

            StringBuilder sb = new StringBuilder();
            string week = Convert.ToDateTime(sTime).DayOfWeek.ToString();
            int iWeek = GetWeek(week);
            int day = 1;
            string head = string.Empty;
            string content = string.Empty;
            for (int i = 0; i < 7; i++) {
                if (i >= iWeek) {
                    head += "<DIV class=\"title1\"><SPAN style=\"CURSOR: pointer\">" + day.ToString() + "</SPAN></DIV>";
                    if(nowDay == day)
                        content += "<DIV class=\"cont1\" onclick=\"this.className = 'cont2';\" onmouseout=\"this.className = 'cont1';\" ondblclick=\"openEdit('" + year.ToString() + "-" + month.ToString() + "-" + day.ToString() + "')\" id=\"" + year.ToString() + month.ToString() + day.ToString() + "\">" + GetContent(year, month, day, dt) + "</DIV>";
                    else
                        content += "<DIV class=\"cont\" onclick=\"this.className = 'cont2;'\" onmouseout=\"this.className = 'cont';\" ondblclick=\"openEdit('" + year.ToString() + "-" + month.ToString() + "-" + day.ToString() + "')\" id=\"" + year.ToString() + month.ToString() + day.ToString() + "\">"+ GetContent(year, month, day, dt) + "</DIV>";
                    day++;
                }
                else {
                    head += "<DIV class=\"title1\"><SPAN style=\"CURSOR: pointer\">&nbsp;</SPAN></DIV>";
                    content += "<DIV class=\"cont\"></DIV>";
                }
            }
            int num = total - day + 7;
            for (int i = 7; i <= num; i++) {
                if (i % 7 == 0) {
                    head += "<DIV class=\"c\"></DIV>";
                    content += "<DIV class=\"c\"></DIV>";
                    sb.Append(head + content);
                    head = string.Empty;
                    content = string.Empty;
                    head += "<DIV class=\"title1\"><SPAN style=\"CURSOR: pointer\">" + day.ToString() + "</SPAN></DIV>";
                    if(nowDay == day)
                        content += "<DIV class=\"cont1\" onclick=\"this.className = 'cont2';\" onmouseout=\"this.className = 'cont1';\" ondblclick=\"openEdit('" + year.ToString() + "-" + month.ToString() + "-" + day.ToString() + "')\" id=\"" + year.ToString() + month.ToString() + day.ToString() + "\">" + GetContent(year, month, day, dt) + "</DIV>";
                    else
                        content += "<DIV class=\"cont\" onclick=\"this.className = 'cont2';\" onmouseout=\"this.className = 'cont';\" ondblclick=\"openEdit('" + year.ToString() + "-" + month.ToString() + "-" + day.ToString() + "')\" id=\"" + year.ToString() + month.ToString() + day.ToString() + "\">" + GetContent(year, month, day, dt) + "</DIV>";
                    day++;
                    if (i == num) {
                        head += "<DIV class=\"c\"></DIV>";
                        content += "<DIV class=\"c\"></DIV>";
                        sb.Append(head + content);
                        head = string.Empty;
                        content = string.Empty;
                    }
                }
                else {
                    head += "<DIV class=\"title1\"><SPAN style=\"CURSOR: pointer\">" + day.ToString() + "</SPAN></DIV>";
                    if(nowDay == day)
                        content += "<DIV class=\"cont1\" onclick=\"this.className = 'cont2';\" onmouseout=\"this.className = 'cont1';\" ondblclick=\"openEdit('" + year.ToString() + "-" + month.ToString() + "-" + day.ToString() + "')\" id=\"" + year.ToString() + month.ToString() + day.ToString() + "\">" + GetContent(year, month, day, dt) + "</DIV>";
                    else
                        content += "<DIV class=\"cont\" onclick=\"this.className = 'cont2';\" onmouseout=\"this.className = 'cont';\" ondblclick=\"openEdit('" + year.ToString() + "-" + month.ToString() + "-" + day.ToString() + "')\" id=\"" + year.ToString() + month.ToString() + day.ToString() + "\">" + GetContent(year, month, day, dt) + "</DIV>";
                    day++;
                }
                if (i == num && i % 7 != 0) {
                    head += "<DIV class=\"c\"></DIV>";
                    content += "<DIV class=\"c\"></DIV>";
                    sb.Append(head + content);
                    head = string.Empty;
                    content = string.Empty;
                }
            }
            return sb.ToString();
        }

        public string ShowSingle(string nowTime, string creator) {
            QJVRMS.Business.CalendarWS.CalendarService calendar = new QJVRMS.Business.CalendarWS.CalendarService();
            DataTable dt = calendar.GetCalendars(nowTime, creator);
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            string theme = string.Empty;
            int total = dt.Rows.Count >= 5 ? 5 : dt.Rows.Count;
            sb.Append("<ul>");
            for (int i = 0; i < total; i++) {
                theme = dt.Rows[i]["Theme"].ToString();
                theme = theme.Length > 6 ? theme.Substring(0, 6) + "..." : theme;
                sb.Append("<li><a href=\"javascript:openManager('" + dt.Rows[i]["CalendarId"].ToString() + "', '"+nowTime+"')\">" + theme + "</a></li>");
            }
            sb.Append("</ul>");
            if (dt.Rows.Count >= 5) {
                sb.Append("<div class=\"tar\" style=\"MARGIN: -3px 5px 0px 0px\">");
                sb.Append("<A class=\"c9\" href=\"javascript:openFull('" + nowTime + "');\">全部</A>");
                sb.Append("</div>");
            }
            return sb.ToString();
        }

        private string GetContent(int year, int month, int day, DataTable dt) {
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            string time = year.ToString() + "-" + month.ToString() + "-" + day.ToString();
            DateTime eTime = Convert.ToDateTime(time).AddDays(1);
            string where = " StartTime < '" + eTime.ToShortDateString() + "' and EndTime > '" + time + "'";
            DataRow[] rows = dt.Select(where);
            if (rows.Length == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            string theme = string.Empty;
            int total = rows.Length >= 5 ? 5 : rows.Length;
            sb.Append("<ul>");
            for (int i = 0; i < total; i++) {
                theme = rows[i]["Theme"].ToString();
                theme = theme.Length > 6 ? theme.Substring(0, 6) + "..." : theme;
                sb.Append("<li><a href=\"javascript:openManager('" + rows[i]["CalendarId"].ToString() + "', '" + year.ToString()+"-" + month.ToString()+"-" + day.ToString() + "')\">" + theme + "</a></li>");
            }
            sb.Append("</ul>");
            if (rows.Length >= 5) {
                sb.Append("<div class=\"tar\" style=\"MARGIN: -3px 5px 0px 0px\">");
                sb.Append("<A class=\"c9\" href=\"javascript:openFull('" + time + "');\">全部</A>");
                sb.Append("</div>");
            }
            return sb.ToString();
        }

        private int GetWeek(string week) {
            int iWeek = 0;
            switch (week) { 
                case "Monday":
                    iWeek = 0;
                    break;
                case "Tuesday":
                    iWeek = 1;
                    break;
                case "Wednesday":
                    iWeek = 2;
                    break;
                case "Thursday":
                    iWeek = 3;
                    break;
                case "Friday":
                    iWeek = 4;
                    break;
                case "Saturday":
                    iWeek = 5;
                    break;
                case "Sunday":
                    iWeek = 6;
                    break;
            }

            return iWeek;
        }
        //显示当天的全部信息
        public string ShowNowCalendars(string nowTime, string creator) {
            QJVRMS.Business.CalendarWS.CalendarService calendar = new QJVRMS.Business.CalendarWS.CalendarService();
            DataTable dt = calendar.GetCalendars(nowTime, creator);
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            for (int i = 0; i < dt.Rows.Count; i++) {
                DateTime sTime = Convert.ToDateTime(dt.Rows[i]["StartTime"].ToString());
                DateTime eTime = Convert.ToDateTime(dt.Rows[i]["EndTime"].ToString());
                sb.Append("<li>");
                sb.Append("<table width=\"95%\" id=\"" + dt.Rows[i]["CalendarId"].ToString() + nowTime + "table\">");
                sb.Append("<tr><td width=\"10px\">"+(i+1).ToString()+".</td><td width=\"300px\">");
                sb.Append("主题：" + dt.Rows[i]["Theme"].ToString());
                sb.Append("</td><td>地点：" + dt.Rows[i]["Site"].ToString());
                sb.Append("</td></tr>");
                sb.Append("<tr><td></td>");
                sb.Append("<td>标签：" + dt.Rows[i]["Label"].ToString()+"</td>");     
                if (IsDiffTime(DateTime.Now, sTime))
                    sb.Append("<td>状态：距开始" + GetDiffTime(DateTime.Now, sTime) + "</td>");
                else
                    if (IsDiffTime(DateTime.Now, eTime))
                        sb.Append("<td>状态：正在进行</td>");
                    else
                        sb.Append("<td>状态：已过期" + GetDiffTime(DateTime.Now, eTime) + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr><td></td><td colspan=\"2\">");
                sb.Append("开始时间：" + Convert.ToDateTime(sTime).ToString("yyyy-MM-dd hh:mm"));
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                sb.Append("结束时间：" + Convert.ToDateTime(eTime).ToString("yyyy-MM-dd hh:mm"));
                sb.Append("</td></tr>");
                sb.Append("<tr><td></td><td colspan=\"2\">");
                sb.Append("<span style=\"float:left;\">内容：</span><div style=\"float:left;\">" + dt.Rows[i]["DContent"].ToString() + "</div>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
                sb.Append("</li>");
            }
            sb.Append("</ul>");

            return sb.ToString();
        }
        //首页显示的信息
        public string ShowCalendars(string nowTime, string creator) {
            QJVRMS.Business.CalendarWS.CalendarService calendar = new QJVRMS.Business.CalendarWS.CalendarService();
            DataTable dt = calendar.ShowCalendars(nowTime, creator);
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            string theme = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++) { 
                DateTime sTime = Convert.ToDateTime(dt.Rows[i]["StartTime"].ToString());
                DateTime eTime = Convert.ToDateTime(dt.Rows[i]["EndTime"].ToString());
                sb.Append("<li>");
                sb.Append("<div class=\"title1\">");
                theme = dt.Rows[i]["Theme"].ToString();
                theme = theme.Length > 14 ? theme.Substring(0, 14) + "..." : theme;
                string title = theme;
                if (IsDiffTime(DateTime.Now, sTime)) {
                    title = theme+ " ---- 距开始" + GetDiffTime(DateTime.Now, sTime);
                }
                else {
                    if (IsDiffTime(DateTime.Now, eTime))
                        title = theme + " ---- 正在进行";
                    else
                        title = theme + " ---- 已过期" + GetDiffTime(DateTime.Now, eTime);
                }
                sb.Append("<a title=\"" + title + "\" href=\"Calendar.aspx?calendarId=" + dt.Rows[i]["calendarId"].ToString() + "\" target=\"_blank\">" + theme + "</a>");
                sb.Append("</div>");
                //if (IsDiffTime(DateTime.Now, sTime)) {
                //    sb.Append("<div class=\"date1\">距开始" + GetDiffTime(DateTime.Now, sTime) + "</div>");
                //}
                //else {
                //    if (IsDiffTime(DateTime.Now, eTime))
                //        sb.Append("<div class=\"date1\">正在进行</div>");
                //    else
                //        sb.Append("<div class=\"date1\">已过期" + GetDiffTime(DateTime.Now, eTime) + "</div>");
                //}

                sb.Append("</li>");
            }

            return sb.ToString();
        }

        private bool IsDiffTime(DateTime beginTime, DateTime endTime) {
            TimeSpan span = endTime - beginTime;
            if (Convert.ToInt32(span.TotalSeconds) >= 0)
                return true;
            return false;
        }
        protected string GetDiffTime(DateTime beginTime, DateTime endTime) {
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
        //显示单个信息
        public string ShowCalendar(string calendarId, ref string theme) {
            QJVRMS.Business.CalendarWS.CalendarService calendar = new QJVRMS.Business.CalendarWS.CalendarService();
            DataTable dt = calendar.GetCalendar(new Guid(calendarId));

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            theme = dt.Rows[0]["Theme"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"issue_top\">");
            sb.Append("<strong style=\"font-size:14px;\"><span>" + dt.Rows[0]["Theme"].ToString() + "</span></strong>");
            sb.Append("</div>");
            //sb.Append("<div class=\"newclass\">");
            //sb.Append("开始时间：<span>" + Convert.ToDateTime(dt.Rows[0]["StartTime"].ToString()).ToString("yyyy-MM-dd hh:mm") + "</span>");
            //sb.Append("&nbsp;&nbsp;");
            //sb.Append("结束时间：<span>" + Convert.ToDateTime(dt.Rows[0]["EndTime"].ToString()).ToString("yyyy-MM-dd hh:mm") + "</span>");
            //sb.Append("</div>");
            sb.Append("<div><ul>");
            sb.Append("<li></li>");
            sb.Append("<li>开始时间：<span>" + Convert.ToDateTime(dt.Rows[0]["StartTime"].ToString()).ToString("yyyy-MM-dd hh:mm") + "</span>");
            sb.Append("&nbsp;&nbsp;");
            sb.Append("结束时间：<span>" + Convert.ToDateTime(dt.Rows[0]["EndTime"].ToString()).ToString("yyyy-MM-dd hh:mm") + "</span></li>");
            sb.Append("<li>地点：<span>" + dt.Rows[0]["Site"].ToString() + "</span>");
            sb.Append("&nbsp;&nbsp;");
            sb.Append("标签：<span>" + dt.Rows[0]["Label"].ToString() + "</span></li>");
            
            sb.Append("</ul></div>");
            sb.Append("<div id=\"context\">");
            sb.Append("<span>" + dt.Rows[0]["DContent"].ToString() + "</span>");
            sb.Append("</div>");

            return sb.ToString();
        }
        //编辑
        public bool EditCalendar(string calendarId, string theme, string site, string label, 
            string sDate, string sTime, string eDate, string eTime, string content, 
            string creator) {
            QJVRMS.Business.CalendarWS.CalendarService calendar = new QJVRMS.Business.CalendarWS.CalendarService();
            return calendar.EditCalendar(new Guid(calendarId), theme, site, label, sDate,
                sTime, eDate, eTime, content, creator);
        }

        public DataTable GetCalendar(string calendarId) {
            QJVRMS.Business.CalendarWS.CalendarService calendar = new QJVRMS.Business.CalendarWS.CalendarService();
            return calendar.GetCalendar(new Guid(calendarId));
        }
        //删除
        public bool DeleteCalendar(string calendarId) {
            QJVRMS.Business.CalendarWS.CalendarService calendar = new QJVRMS.Business.CalendarWS.CalendarService();
            return calendar.DeleteCalendar(new Guid(calendarId));
        }

        public string SearchCalendarsContent(string monthTime, string stime, string etime, 
            string state, string creator,
            int pageSize, int pageIndex, int type) {
            int pageCount = 0;
            QJVRMS.Business.CalendarWS.CalendarService calendar = new QJVRMS.Business.CalendarWS.CalendarService();
            DataSet ds = calendar.SearchCalendars(monthTime,stime,etime,state,creator,pageSize,pageIndex,type);
            if (ds == null && ds.Tables.Count == 0)
                return string.Empty;
            DataTable dt = ds.Tables[1];
            pageCount = string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) ? 0 : int.Parse(ds.Tables[0].Rows[0][0].ToString());
            StringBuilder sb = new StringBuilder();
            if (pageCount > pageSize)
                sb.Append(SearchCalendarsPage(pageSize, pageIndex, pageCount, monthTime, stime, etime, state, type));
            sb.Append("<ul>");
            for (int i = 0; i < dt.Rows.Count; i++) {
                DateTime sTime = Convert.ToDateTime(dt.Rows[i]["StartTime"].ToString());
                DateTime eTime = Convert.ToDateTime(dt.Rows[i]["EndTime"].ToString());
                int no = pageSize * (pageIndex - 1) + i + 1;
                sb.Append("<li>");
                sb.Append("<table width=\"95%\" style=\"border-bottom:#ddd 1px solid;\">");
                sb.Append("<tr><td width=\"10px\">" + no.ToString() + ".</td><td width=\"300px\">");
                sb.Append("主题：" + dt.Rows[i]["Theme"].ToString());
                sb.Append("</td><td>地点：" + dt.Rows[i]["Site"].ToString());
                sb.Append("</td></tr>");
                sb.Append("<tr><td></td>");
                sb.Append("<td>标签：" + dt.Rows[i]["Label"].ToString() + "</td>");
                if (IsDiffTime(DateTime.Now, sTime))
                    sb.Append("<td>状态：距开始" + GetDiffTime(DateTime.Now, sTime) + "</td>");
                else
                    if (IsDiffTime(DateTime.Now, eTime))
                        sb.Append("<td>状态：正在进行</td>");
                    else
                        sb.Append("<td>状态：已过期" + GetDiffTime(DateTime.Now, eTime) + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr><td></td><td colspan=\"2\">");
                sb.Append("开始时间：" + Convert.ToDateTime(sTime).ToString("yyyy-MM-dd hh:mm"));
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                sb.Append("结束时间：" + Convert.ToDateTime(eTime).ToString("yyyy-MM-dd hh:mm"));
                sb.Append("</td></tr>");
                sb.Append("<tr><td></td><td colspan=\"2\">");
                sb.Append("<span style=\"float:left;\">内容：</span><div style=\"float:left;\">" + dt.Rows[i]["DContent"].ToString() + "</div>");
                sb.Append("</td></tr>");
                sb.Append("</table>");
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            if (pageCount > pageSize)
                sb.Append(SearchCalendarsPage(pageSize,pageIndex,pageCount,monthTime,stime,etime,state,type));
            return sb.ToString();
        }

        private string SearchCalendarsPage(int pageSize, int curpage, int total,
            string monthTime, string stime, string etime, string state, int type) {
            int totalpage = 0;
            int startcount = 0;
            int endcount = 0;

            if (pageSize != 0) {
                totalpage = total / pageSize;
                totalpage = (total % pageSize) != 0 ? totalpage + 1 : totalpage;
                totalpage = totalpage == 0 ? 1 : totalpage;
            }

            startcount = (curpage + 5) > totalpage ? totalpage - 9 : curpage - 4;
            endcount = curpage < 5 ? 10 : curpage + 5;

            if (startcount < 1)
                startcount = 1;

            if (totalpage < endcount)
                endcount = totalpage;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            for (int i = startcount; i <= endcount; i++) {
                if (i == curpage)
                    sb.Append("<a style='color:red;padding:2 2 2 2;' >" + i.ToString() + "</a>");
                else
                    sb.Append("<a style='padding:2 2 2 2;' href=\"javascript:SearchPage('" + monthTime + "','" + stime + "','" + etime + "','" + state + "','" + type.ToString() + "','" + pageSize.ToString() + "','" + i.ToString() + "')\">" + i.ToString() + "</a>");
            }
            sb.Append("</div>");

            return sb.ToString();
        }

        #endregion
    }

    public class CalendarFactory {
        ICalendar iCalendar = null;
        public CalendarFactory() {
            iCalendar = new Calendar();
        }

        public string ShowNowCalendars(string nowTime, string creator) {
            return iCalendar.ShowNowCalendars(nowTime, creator);
        }

        public string ShowCalendar(string calendarId, ref string theme) {
            return iCalendar.ShowCalendar(calendarId, ref theme);
        }

        public bool EditCalendar(string calendarId, string theme, string site, string label, 
            string sDate, string sTime, string eDate, string eTime, string content, string creator) {
            return iCalendar.EditCalendar(calendarId, theme, site, label, sDate, sTime, eDate,
                eTime, content, creator);
        }

        public DataTable GetCalendar(string calendarId) {
            return iCalendar.GetCalendar(calendarId);
        }

        public bool DeleteCalendar(string calendarId) {
            return iCalendar.DeleteCalendar(calendarId);
        }

        public string ShowCalendars(string nowTime, string creator) {
            return iCalendar.ShowCalendars(nowTime, creator);
        }

        public string ShowHead(int year, int month, string creator) {
            return iCalendar.ShowHead(year, month, creator);
        }

        public string ShowContent(int year, int month, string creator) {
            return iCalendar.ShowContent(year, month, creator);
        }

        public string ShowSingle(string nowTime, string creator) {
            return iCalendar.ShowSingle(nowTime, creator);
        }

        public string SearchCalendarsContent(string monthTime, string stime, string etime,
            string state, string creator,
            int pageSize, int pageIndex, int type) {
            return iCalendar.SearchCalendarsContent(monthTime, stime, etime, state,
                creator, pageSize, pageIndex, type);
        }
    }
    public class Thumbnail {
        public Thumbnail(string id, byte[] data) {
            this.ID = id;
            this.Data = data;
        }


        private string id;
        public string ID {
            get {
                return this.id;
            }
            set {
                this.id = value;
            }
        }

        private byte[] thumbnail_data;
        public byte[] Data {
            get {
                return this.thumbnail_data;
            }
            set {
                this.thumbnail_data = value;
            }
        }


    }
}
