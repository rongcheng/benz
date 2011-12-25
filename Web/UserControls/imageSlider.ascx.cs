using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
 

namespace WebUI.UserControls
{
    public partial class imageSlider : System.Web.UI.UserControl
    {
        private string title = "Í¼Æ¬»ÃµÆ";
        private Guid cataId = Guid.Empty;
        private string css = string.Empty;



        public string Css
        {
            get { return css; }
            set { this.css = value; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public Guid CataId
        {
            get { return this.cataId; }
            set { this.cataId = value; }
        }

        private string transName = "idTransformView";
        private string sliderName = "idSlider";
        private string numName = "idNum";

        public string TransName
        {
            set { this.transName = value; }
        }

        public string SliderName
        {
            set { this.sliderName = value; }
        }

        public string NumName
        {
            set { this.numName = value; }
        }

        protected System.Text.StringBuilder html = null;

        public DataTable LatestTable
        {
            get
            {
                if (Cache["LatestImage"] == null)
                {
                    Cache.Insert("LatestImage", QJVRMS.Business.ImageStorageClass.GetLatestImages(),
                        null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 5, 0));
                }

                return Cache["LatestImage"] as DataTable;
            }
        }

        public DataTable CataLatestTable
        {
            get
            {
                if (Cache["CataLatest"] == null)
                {
                    Cache.Insert("CataLatest", QJVRMS.Business.ImageStorageClass.GetTopImagesOfCatalog(this.cataId),
                        null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 5, 0));
                }

                return Cache["CataLatest"] as DataTable;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if( cataId == Guid.Empty)
                    GetLatestImage(LatestTable);
                else
                    GetLatestImage(CataLatestTable);
            }
        }

        protected void GetLatestImage(DataTable dt )
        {
            html = new System.Text.StringBuilder();
            System.Text.StringBuilder temp = new System.Text.StringBuilder();
            int index = 0;

            html.Append("<div class='slcontainer' id='" + transName + "'>");
            html.Append("<ul class='slider' id='"+this.sliderName+"'>");
            temp.Append("<ul class='num' id='"+this.numName+"'>");
            foreach (DataRow dr in dt.Rows)
            {
                index++;
              
                html.Append("<li><a target='_blank' href='/PicDetail.aspx?ItemID=" + dr["itemId"].ToString() + "'><img src='" + UIBiz.CommonInfo.GetImageUrl(170, dr["FolderName"].ToString(), dr["ItemSerialNum"].ToString(), dr["ImageType"].ToString()) + "'/></a></li>");
                temp.Append("<li><a href='/PicDetail.aspx?ItemID=" + dr["itemId"].ToString() + "'>" + index.ToString() + "</a></li>");
               

            }
            html.Append("</ul>");
            temp.Append("</ul>");
            
            html.Append(temp.ToString());
            html.Append("</div>");
        }
    }
}