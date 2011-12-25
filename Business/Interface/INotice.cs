using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business {
    interface INotice {
        string GetNoticesContent(string userName, int pageSize, int pageIndex);
        string GetNoticeContent(string noticeId, string logName);
        bool EditNotice(string noticeId, string noticeName, string noticeContent, string creator, string type);
        bool DeleteNotice(string noticeId);
        string ShowNoticesContent();
        string ShowNoticeContent(string noticeId, ref string title);
        string ShowNoticesAll(string userName, int pageSize, int pageIndex);
    }
}
