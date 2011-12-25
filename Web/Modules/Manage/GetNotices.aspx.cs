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

namespace WebUI.Modules.Manage {
    public partial class GetNotices : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            string result = string.Empty;

            string type = get_LinkParam("type");
            string name = string.Empty;
            string noticeId = string.Empty;

            QJVRMS.Business.NoticeFactory noticeFactory = new QJVRMS.Business.NoticeFactory();

            switch (type) { 
                case "Single":
                    name = get_LinkParam("name");
                    noticeId = get_LinkParam("noticeid");
                    result = noticeFactory.GetNoticeContent(noticeId, name);
                    break;
                case "Page":
                    name = get_LinkParam("name");
                    int pageSize = int.Parse(get_LinkParam("size"));
                    int pageIndex = int.Parse(get_LinkParam("index"));
                    result = noticeFactory.GetNoticesContent(name, pageSize, pageIndex);
                    break;
                case "Update":
                    name = get_LinkParam("name");
                    noticeId = get_LinkParam("noticeId");
                    string noticeName = get_LinkParam("noticeName");
                    string noticeContent = get_LinkParam("noticeContent");
                    if (noticeFactory.EditNotice(noticeId, noticeName, noticeContent, name, type))
                        result = "更新成功";
                    else
                        result = "更新失败";
                    break;
                case "Add":
                    name = get_LinkParam("name");
                    noticeId = Guid.NewGuid().ToString();
                    string nName = get_LinkParam("noticeName");
                    string nContent = get_LinkParam("noticeContent");
                    if (noticeFactory.EditNotice(noticeId, nName, nContent, name, type))
                        result = "更新成功";
                    else
                        result = "更新失败";
                    break;
                case "Delete":
                    string nid = get_LinkParam("id");
                    if (noticeFactory.DeleteNotice(nid))
                        result = nid;
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
