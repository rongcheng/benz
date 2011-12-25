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
    public partial class imageMarquee : System.Web.UI.UserControl
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

        private string holderId = "slide_pic";
      

        public string HolderId
        {
            set { this.holderId = value; }
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
           
            html.Append("<div class='slide_holder' id='" + holderId + "'>");
      
            foreach (DataRow dr in dt.Rows)
            {
                
              
                html.Append(" <a target='_blank' href='/PicDetail.aspx?ItemID=" + dr["itemId"].ToString() + "'><img src='" + UIBiz.CommonInfo.GetImageUrl(170, dr["FolderName"].ToString(), dr["ItemSerialNum"].ToString(), dr["ImageType"].ToString()) + "'/></a>");
               

            }
       
         
            html.Append("</div>");
        }
    }
    
}