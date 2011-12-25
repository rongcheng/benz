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
    public partial class searchLeft : System.Web.UI.UserControl
    {
        
        //public string Hvsp;

        //public string LastSearch
        //{
        //    set
        //    {
        //        this.hidLastSearch.Value = value;
        //    }
        //    get
        //    {
        //        return this.hidLastSearch.Value;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.btnSearch.Attributes.Add("onclick", "return check()");

        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            //string mapType = "";

            //string keyword = this.Kwords.Text.ToString();

            //DataTable dt;

            //dt = QJVRMS.Business.ImageStorage.SearchImageByKeyword(keyword);

            //if (dt.Rows.Count > 0)
            //{

            //}

            //if (this.mapType.SelectedValue.ToString() == "p")
            //{
            //    mapType = "p";
            //}
            //else if(this.mapType.SelectedValue.ToString() == "h")
            //{
            //    mapType = "h";
            //}
            //else if(this.mapType.SelectedValue.ToString() == "v")
            //{
            //    mapType = "v";
            //}
            //else if(this.mapType.SelectedValue.ToString() == "s")
            //{
            //    mapType = "s";
            //}

            
        }

        public void SetState()
        {
            
        }
    }
}