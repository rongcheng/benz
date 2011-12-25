using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace QJVRMS.Business {
    class Notices:INotice {
        #region INotice 成员

        public string GetNoticesContent(string userName, int pageSize, int pageIndex) {
            int totalRecord = 0;
            QJVRMS.Business.NoticeWS.NoticesService notices = new QJVRMS.Business.NoticeWS.NoticesService();
            DataTable dt = notices.GetNotices(pageSize, pageIndex, ref totalRecord);

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("<table border=\"0\" cellspacing=\"0\" width=\"100%\" class=\"table\">");
            sb.Append("<tr class=\"head\">");
            sb.Append("<td style=\"width:180px\">公告名称</td>");
            sb.Append("<td style=\"width:250px\">公告内容</td>");
            sb.Append("<td style=\"width:100px\">发布时间</td>");
            sb.Append("<td style=\"width:100px\">发布人</td>");
            sb.Append("<td style=\"width:80px\">编辑</td>");
            sb.Append("</tr>");
            string content = string.Empty;
            string noticeName = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++) {
                if(i%2 ==0)
                    sb.Append("<tr id=\"tr" + dt.Rows[i]["NoticeId"].ToString() + "\" class=\"both\">");
                else
                    sb.Append("<tr id=\"tr" + dt.Rows[i]["NoticeId"].ToString() + "\" class=\"cell\">");
                noticeName = dt.Rows[i]["NoticeName"].ToString();
                noticeName = noticeName.Length > 14 ? noticeName.Substring(0, 14) : noticeName;
                sb.Append("<td>" + noticeName + "</td>");
                content = dt.Rows[i]["NoticeContent"].ToString();
                content = content.Length > 15 ? content.Substring(0, 15) + "..." : content;
                sb.Append("<td>" + content + "</td>");
                sb.Append("<td>" + Convert.ToDateTime(dt.Rows[i]["NoticeDate"].ToString()).ToString("yyyy-MM-dd") + "</td>");
                sb.Append("<td>" + dt.Rows[i]["Creator"].ToString() + "</td>");
                sb.Append("<td>");
                sb.Append("<a href=\"javascript:GetNotice('" + dt.Rows[i]["NoticeId"].ToString() + "', '" + userName + "')\" >编辑</a>");
                sb.Append("&nbsp;&nbsp;");
                sb.Append("<a href=\"javascript:DeleteNotice('" + dt.Rows[i]["NoticeId"].ToString() + "')\" >删除</a>");
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            if (totalRecord > pageSize)
                sb.Append(GetPage(pageSize, pageIndex, totalRecord, userName));

            return sb.ToString();
        }

        private string GetPage(int number, int curpage, int total, string userName) {
            int totalpage = 0;
            int startcount = 0;
            int endcount = 0;

            if (number != 0) {
                totalpage = total / number;
                totalpage = (total % number) != 0 ? totalpage + 1 : totalpage;
                totalpage = totalpage == 0 ? 1 : totalpage;
            }

            startcount = (curpage + 5) > totalpage ? totalpage - 9 : curpage - 4;
            endcount = curpage < 5 ? 10 : curpage + 5;

            if (startcount < 1)
                startcount = 1;

            if (totalpage < endcount)
                endcount = totalpage;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"page\">");
            for (int i = startcount; i <= endcount; i++) {
                if (i == curpage)
                    sb.Append("<a class=\"current\">" + i.ToString() + "</a>");
                else
                    sb.Append("<a class=\"number\" href=\"javascript:GetNoticesPage('" + userName + "', '" + number.ToString() + "', '" + i.ToString() + "')\">" + i.ToString() + "</a>");
            }
            sb.Append("</div>");

            return sb.ToString();
        }

        public string ShowNoticesAll(string userName, int pageSize, int pageIndex) {
            int totalRecord = 0;
            QJVRMS.Business.NoticeWS.NoticesService notices = new QJVRMS.Business.NoticeWS.NoticesService();
            DataTable dt = notices.GetNotices(pageSize, pageIndex, ref totalRecord);

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("<table border=\"0\" cellspacing=\"0\" width=\"100%\" class=\"table\">");
            sb.Append("<tr class=\"head\">");
            sb.Append("<td style=\"width:180px\">公告名称</td>");
            sb.Append("<td style=\"width:250px\">公告内容</td>");
            sb.Append("<td style=\"width:100px\">发布时间</td>");
            sb.Append("<td style=\"width:100px\">发布人</td>");
            sb.Append("</tr>");
            string content = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++) {
                sb.Append("<tr id=\"tr" + dt.Rows[i]["NoticeId"].ToString() + "\" class=\"cell\">");
                //sb.Append("<td><a href=\"Notices.aspx?noticeId=" + dt.Rows[i]["NoticeId"].ToString() + "\" >" + dt.Rows[i]["NoticeName"].ToString() + "</a></td>");
                sb.Append("<td><a href=\"javascript:OnNotices('" + dt.Rows[i]["NoticeId"].ToString() + "');\">" + dt.Rows[i]["NoticeName"].ToString() + "</a></td>");
                content = dt.Rows[i]["NoticeContent"].ToString();
                content = content.Length > 30 ? content.Substring(0, 30) + "..." : content;
                sb.Append("<td>" + content + "</td>");
                sb.Append("<td>" + Convert.ToDateTime(dt.Rows[i]["NoticeDate"].ToString()).ToString("yyyy-MM-dd") + "</td>");
                sb.Append("<td>" + dt.Rows[i]["Creator"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            if (totalRecord > pageSize)
                sb.Append(GetPage(pageSize, pageIndex, totalRecord, userName));

            return sb.ToString();
        }
        public string ShowNoticesContent() {
            QJVRMS.Business.NoticeWS.NoticesService notices = new QJVRMS.Business.NoticeWS.NoticesService();
            DataTable dt = notices.ShowNotices();

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            string noticeName = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++) { 
                sb.Append("<li>");
                sb.Append("<div class=\"title\">");
                noticeName = dt.Rows[i]["NoticeName"].ToString();
                string title = noticeName  + " ---- " + Convert.ToDateTime(dt.Rows[i]["NoticeDate"].ToString()).ToString("yyyy-MM-dd");
                noticeName = noticeName.Length > 14 ? noticeName.Substring(0, 14) + "..." : noticeName;
                //sb.Append("<a title=\""+title+"\" href=\"Notices.aspx?noticeId=" + dt.Rows[i]["NoticeId"].ToString() + "\" target=\"_blank\">" + noticeName + "</a>");
                sb.Append("<a title=\"" + title + "\" href=\"javascript:OnNotices('" + dt.Rows[i]["NoticeId"].ToString() + "');\">" + noticeName + "</a>");
                sb.Append("</div>");
                //sb.Append("<div class=\"date\">" + Convert.ToDateTime(dt.Rows[i]["NoticeDate"].ToString()).ToString("yyyy-MM-dd") + "</div>");
                sb.Append("</li>");
            }

            return sb.ToString();
        }

        public string ShowNoticeContent(string noticeId, ref string title) {
            QJVRMS.Business.NoticeWS.NoticesService notices = new QJVRMS.Business.NoticeWS.NoticesService();
            DataTable dt = notices.GetNotice(noticeId);

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            title = dt.Rows[0]["NoticeName"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<H1 id=\"artibodyTitle\">" + dt.Rows[0]["NoticeName"].ToString() + "</H1>");
            sb.Append("<DIV class=\"artInfo\"><SPAN id=\"art_source\">发布人：" + dt.Rows[0]["Creator"].ToString() + "</SPAN>&nbsp;&nbsp;<SPAN id=\"pub_date\">时间：" + Convert.ToDateTime(dt.Rows[0]["NoticeDate"].ToString()).ToString("yyyy-MM-dd") + "</SPAN></DIV>");
            sb.Append("<DIV class=\"blkContainerSblkCon\" id=\"artibody\">" + dt.Rows[0]["NoticeContent"].ToString() + "</div>");
            //sb.Append("<div class=\"issue_top\">");
            //sb.Append("<strong style=\"font-size:14px;\"><span>" + dt.Rows[0]["NoticeName"].ToString() + "</span></strong>");
            //sb.Append("</div>");
            //sb.Append("<div><ul>");
            //sb.Append("<li></li>");
            //sb.Append("<li>发布人：<span>" + dt.Rows[0]["Creator"].ToString() + "</span>");
            //sb.Append("&nbsp;&nbsp;");
            //sb.Append("时间：<span>" + Convert.ToDateTime(dt.Rows[0]["NoticeDate"].ToString()).ToString("yyyy-MM-dd") + "</span></li>");
            //sb.Append("</ul></div>");
            //sb.Append("<div id=\"context\">");
            //sb.Append("<span>" + dt.Rows[0]["NoticeContent"].ToString() + "</span>");
            //sb.Append("</div>");

            return sb.ToString();
        }

        public string GetNoticeContent(string noticeId, string logName) {
            QJVRMS.Business.NoticeWS.NoticesService notices = new QJVRMS.Business.NoticeWS.NoticesService();
            DataTable dt = notices.GetNotice(noticeId);

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            //sb.Append("<table width=\"100%;\">");
            //sb.Append("<tr><td align=\"left\" style=\"width:100px; font-size:13px; font-weight:100;\">编辑公告:</td></tr>");
            //sb.Append("<tr><td align=\"left\" style=\"width:100px;\">公告名称：</td><td align=\"left\"><input type=\"text\" id=\"NoticeName\" value=\"" + dt.Rows[0]["NoticeName"].ToString() + "\" style=\"width:400px;\" maxlength=\"30\"/><font style=\"color:Red;\">*</font></td></tr>");
            //sb.Append("<tr><td align=\"left\" style=\"width:100px;\">公告内容：</td><td align=\"left\"><textarea cols=\"100\" rows=\"25\" id=\"NoticeContent\">" + dt.Rows[0]["NoticeContent"].ToString() + "</textarea>(不能超过1000个字)</td></tr>");
            //sb.Append("<tr><td align=\"left\" style=\"width:100px;\"><input type=\"button\" value=\"保存\"class=\"btn\" style=\" font-size:12px;\" onclick=\"UpdateNotice('" + noticeId + "', '" + logName + "')\" /></td><td align=\"left\"></td></tr>");
            //sb.Append("</table>");
            sb.Append(dt.Rows[0]["NoticeName"].ToString() + "|" + dt.Rows[0]["NoticeContent"].ToString());
            return sb.ToString();
        }

        public bool EditNotice(string noticeId, string noticeName, string noticeContent, string creator, string type) {
            QJVRMS.Business.NoticeWS.NoticesService notices = new QJVRMS.Business.NoticeWS.NoticesService();
            return notices.EditNotice(new Guid(noticeId), noticeName, noticeContent, creator, type);
        }

        public bool DeleteNotice(string noticeId) {
            QJVRMS.Business.NoticeWS.NoticesService notices = new QJVRMS.Business.NoticeWS.NoticesService();
            return notices.DeleteNotice(new Guid(noticeId));
        }

        #endregion
    }

    public class NoticeFactory {
        INotice iNotice = null;
        public NoticeFactory() {
            iNotice = new Notices();
        }

        public string GetNoticesContent(string userName, int pageSize, int pageIndex) {
            return iNotice.GetNoticesContent(userName, pageSize, pageIndex);
        }

        public string GetNoticeContent(string noticeId, string logName) {
            return iNotice.GetNoticeContent(noticeId, logName);
        }

        public bool EditNotice(string noticeId, string noticeName, string noticeContent, string creator, string type) {
            return iNotice.EditNotice(noticeId, noticeName, noticeContent, creator, type);
        }

        public bool DeleteNotice(string noticeId) {
            return iNotice.DeleteNotice(noticeId);
        }

        public string ShowNoticesContent() {
            return iNotice.ShowNoticesContent();
        }

        public string ShowNoticeContent(string noticeId, ref string title) {
            return iNotice.ShowNoticeContent(noticeId, ref title);
        }

        public string ShowNoticesAll(string userName, int pageSize, int pageIndex) {
            return iNotice.ShowNoticesAll(userName, pageSize, pageIndex);
        }
    }
}
