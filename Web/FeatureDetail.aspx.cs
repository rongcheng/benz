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
    public partial class FeatureDetail : AuthPage {
        string featureId = string.Empty;
        string featureName = string.Empty;
        private int _curpage;

        protected void Page_Load(object sender, EventArgs e) {
            this.Title = "查看专题详细信息";
        
            featureId = get_LinkParam("featureId");
            if (!string.IsNullOrEmpty(featureId))
                ViewState["FEATUREID"] = featureId;
            else
                featureId = ViewState["FEATUREID"].ToString();
            featureName = get_LinkParam("name");
            if (!string.IsNullOrEmpty(featureName))
                ViewState["FEATURENAME"] = featureName;
            else
                featureName = ViewState["FEATURENAME"].ToString();

            this.AspNetPager1.PageSize = NowPageCount();
            this.msgSearchResult.Text = "专题[" + featureName + "]的详细信息";
            if (!IsPostBack) {

                BindData(this.AspNetPager1.PageSize, 1);
            }
        }

        protected int NowPageCount() {
            HttpCookie pageCountCookie = Request.Cookies["QJpageCount"];
            int defaultCount = UIBiz.CommonInfo.PageCount;
            if (pageCountCookie == null) {
                return UIBiz.CommonInfo.PageCount;
            }
            else {
                int.TryParse(pageCountCookie.Value, out defaultCount);
                return defaultCount;
            }
        }

        public void BindData(Int32 pageSize, Int32 pageIndex) {
            int rowCount = 0;

            QJVRMS.Business.FeatureFactory featureFactory = new QJVRMS.Business.FeatureFactory();

            DataTable dt = featureFactory.ShowFeatureImages(featureId, 0, pageSize, pageIndex, ref rowCount); 
            this.lb_ResultCount.Text = "共检索到 <strong>" + rowCount.ToString() + "</strong> 条数据";

            this.AspNetPager1.RecordCount = rowCount;

            this.DataResource1.DataSource = dt;
            this.DataResource1.DataBind();
        }
        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e) {
            _curpage = e.NewPageIndex;

            //if (_curpage != 0) _curpage--;

            BindData(this.AspNetPager1.PageSize, _curpage);
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
