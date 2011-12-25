using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QJVRMS.Business;

namespace WebUI.UserControls
{
    public partial class DataPic : BaseUserControl
    {

        public string JarId
        {
            get
            {
                return this.ClientID + "JarId";
            }
        }

        public string JarImageId
        {
            get { return this.ClientID + "JarImId"; }
        }

        private DataTable dataSource;
        public DataTable DataSource
        {
            set 
            {
                this.dataSource = value;
                this.DataList1.DataSource = this.dataSource;
               // this.DataList1.DataBind();                
            }
        }

        public override void DataBind()
        {
            this.DataList1.DataBind();
            DataList1.Dispose();
            dataSource = null;
        }
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
            //    CheckShowType();
            }
        }

        protected void CheckShowType()
        {
            //if (!string.IsNullOrEmpty(Request["showType"]))
            //{
            //    if( Request["showType"] == "1")
            //    this.DataList1.RepeatColumns = 1;
            //}
        }

        //Í¼Æ¬Â·¾¶
        protected string GetImgUrl(string FolderName , string ItemSerialNum, string ImageType)
        {
          //  if (Request["showType"] == "1")
            //{
            //    return UIBiz.CommonInfo.GetImageUrl(400, FolderName, ItemSerialNum, ImageType);
            //}
         //   else
            {
                return UIBiz.CommonInfo.GetImageUrl(170, FolderName, ItemSerialNum, ImageType);
            }
          
        }       

        /// <summary>
        /// Í¼Æ¬ÏÂÔØ
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        protected string GetCmd(string picId, string ptype,string itemId,string folder)
        {
            string strPicid = picId.ToString();

            if (Request.IsAuthenticated && bool.Parse(CurrentUser.IsDownLoad))
            {

                return "<a class='small_sample' target='_self' href=\"javascript:downhigh('" + strPicid + "','" + ptype + "','"+itemId+"','"+folder+"')\">ÏÂÔØ</a>";
            }
  
            else
            {
                return "";
            }

        }


        /// <summary>
        /// È¡×Ö·û´®×ó±ß¼¸¸ö×Ö·û
        /// </summary>
        /// <param name="strInput">ÊäÈë×Ö·û´®</param>
        /// <param name="i">È¡¶àÉÙ¸ö×Ö·û</param>
        /// <returns></returns>
        public string GetLeftString(string strInput,int i)
        {
            return string.IsNullOrEmpty(strInput)?string.Empty:((i > strInput.Length) ? strInput : strInput.Substring(0, i));   
        }
    }
}