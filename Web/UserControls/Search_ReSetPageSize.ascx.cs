using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WebUI.UserControls
{
    public partial class Search_ReSetPageSize : System.Web.UI.UserControl
    {

        public string Search_ResourceType
        {
            get { return this.SelectResourceType.Value; }

        }


        public string isChangePageSize
        {
            get
            {
                return this.hidden_isChangePageSize.Value;
            }
            set
            {
                this.hidden_isChangePageSize.Value = value;
            }
        }

        /// <summary>
        /// Ñ¡ÔñµÄÒ³Êý
        /// </summary>
        public int SelectedPageCount
        {
            get
            {
                return int.Parse(this.SelectPageSize.Items[this.SelectPageSize.SelectedIndex].Value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GetPageCookie();
        }

        protected void GetPageCookie()
        {
            this.SelectPageSize.SelectedIndex = -1;
            HttpCookie pageCountCookie = Request.Cookies["QJpageCount"];
            int defaultCount = UIBiz.CommonInfo.PageCount;

            if (pageCountCookie == null)
            {
                this.SelectPageSize.SelectedIndex = 0;
            }
            else
            {
                int.TryParse(pageCountCookie.Value, out defaultCount);


                this.SelectPageSize.Items.FindByValue(defaultCount.ToString()).Selected = true;
            }
        }


    }
}