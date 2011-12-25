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
using AjaxControlToolkit;

namespace WebUI
{

    
    public partial class Default : AuthPage
    {

        public string imgString = "";
        private string xmlFile = "";

        //static DataTable cataTable = null;
        //DataTable denyTable = null;

        //protected void GetCataTable()
        //{
        //    if (cataTable == null)
        //    {
        //        cataTable = QJVRMS.Business.Catalog.GetAllCatalog();
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {



            xmlFile = Server.MapPath("/xml/IndexFlashImages.xml");
            this.imgString = this.GetImageString();

            //   Guid groupId = CurrentUser.UserGroupId;
            if (!this.IsPostBack)
            {
                //最新公告
                QJVRMS.Business.NoticeFactory noticeFactory = new QJVRMS.Business.NoticeFactory();
                this.NoticesUL.InnerHtml = noticeFactory.ShowNoticesContent();
                
                //个人事务
                //QJVRMS.Business.CalendarFactory calendarFactory = new QJVRMS.Business.CalendarFactory();
                //this.CalendarUL.InnerHtml = calendarFactory.ShowCalendars(DateTime.Now.ToString(), CurrentUser.UserLoginName);
                
                //订单管理
                QJVRMS.Business.Orders orders = new QJVRMS.Business.Orders();
                this.CalendarUL.InnerHtml = orders.ShowDefaultOrders(CurrentUser.UserLoginName);
               // if (Request.IsAuthenticated)
             //   {
         //           this.lblLoginName.Text = CurrentUser.UserName;
                  //  this.notLogin.Visible = false;
           //         this.logged.Visible = true;
           //     }
            //    else
        //        {
             //       this.notLogin.Visible = true;
          //          this.logged.Visible = false;
          //      }
                //GetCataTable();
                //if (cataTable.Rows.Count == 0) return;


                //#region 权限判定

                //// QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
                //denyTable = QJVRMS.Business.Catalog.GetCatalogByMethod(CurrentUser.UserId, QJVRMS.Business.SecurityControl.OperatorMethod.Deny);


                //#endregion

                //DataRow[] firstNodes = cataTable.Select("parentid is null", "CatalogOrder");
                //GetCatalog(cataTable, firstNodes, null);

            }
        }

        //private void GetCatalog(DataTable CTable, DataRow[] Nodes,AccordionPane topPane)
        //{
        //    bool ShowFlag = false;


        //    for (int i = 0; i < Nodes.Length; i++)
        //    {


        //        ShowFlag = denyTable.Select("ObjectId='" + Nodes[i].ItemArray[0].ToString() + "'").Length == 0;
        //        AccordionPane topCata = null;
        //        HyperLink hl = null;

        //        if (Nodes[i].ItemArray[2].ToString() == string.Empty)
        //        {
        //            if (ShowFlag)
        //            {
        //                topCata = new AccordionPane();
        //                topCata.ID = i.ToString() + "Cata";
                      
        //                hl = new HyperLink();
        //                hl.Text = Nodes[i].ItemArray[1].ToString();
        //                hl.NavigateUrl = "~/PicList.aspx?showCata=1&CatalogID=" + Nodes[i].ItemArray[0].ToString();

        //                topCata.HeaderContainer.Controls.Add(hl);
                         
        //                this.cataMenu.Panes.Add(topCata);

        //            }
        //            else
        //            {
        //                continue;
        //            }
        //        }
        //        else
        //        {
        //            if (ShowFlag)
        //            {
        //                hl = new HyperLink();

        //                hl.Text = Nodes[i].ItemArray[1].ToString();
        //                hl.NavigateUrl = "~/PicList.aspx?showCata=1&CatalogID=" + Nodes[i].ItemArray[0].ToString();
        //                HtmlGenericControl newLine = new HtmlGenericControl("BR");
        //                topPane.ContentContainer.Controls.Add(hl);
        //                topPane.ContentContainer.Controls.Add(newLine);
        //                //OutString.Append("<li><a href=\"/PicList.aspx?showCata=1&CatalogID=" + Nodes[i].ItemArray[0].ToString() + "\">" + Nodes[i].ItemArray[1].ToString() + "</a></li>");
        //            }

        //        }


        //        DataRow[] firstNodesChild = CTable.Select("parentid='" + Nodes[i].ItemArray[0].ToString() + "'", "CatalogOrder");
        //        GetCatalog(CTable, firstNodesChild, topCata);
        //    }

         
        //}

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = false;
          
            base.OnInit(e);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


           // string keyword = this.Kwords.Text.ToString().Trim().Replace("'", "''");
            string keyword = "";

            Response.Redirect("/ResourceList.aspx?keyword=" + Server.UrlEncode(keyword) + "&BeginDate=&EndDate=&Catalogid=" + "00000000-0000-0000-0000-000000000000");//以后加开始和结束日期
        }



        private string GetImageString()
        {
            string _all = "";
            string _img = "";
            string _links = "";
            string _title = "";


            string imagePath = GetImagePath();
            DataTable dtUpload = GetUploadImages();
            DataTable dtDefault = GetDefaultImages();
            DataTable dtAll = new DataTable();

            if (dtUpload == null)
            {
                dtUpload = new DataTable("UploadImage");
                foreach (DataColumn columm in dtDefault.Columns)
                {
                    dtUpload.Columns.Add(columm.ColumnName, columm.DataType);
                    dtAll.Columns.Add(columm.ColumnName, columm.DataType);

                }
            }

          


            dtAll.Merge(dtUpload);
            dtAll.Merge(dtDefault);

            DataRow[] drs = dtAll.Select("status='1'","Order");

            foreach (DataRow dr in drs)
            {
                //dr["src"] = imagePath + dr["src"] + ".jpg";      
                _img+= dr["src"].ToString() + "|";
                _links+= dr["link"].ToString() + "|";
                _title+= dr["description"].ToString() + "|";

            }

            if (drs.Length > 0)
            {
                _img = _img.Substring(0, _img.Length - 1);
                _links = _links.Substring(0, _links.Length - 1);
                _title = _title.Substring(0, _title.Length - 1);


                _all = "imgs=" + _img + "&imgPath=" + imagePath.TrimEnd("/".ToCharArray()) + "&links=" + _links + "&titles=" + _title;

            }
            return _all;

            //imgs=1|2|3|4|5&imgPath=/ui/flash/images&links=||||&titles=三一图片管理系统|三一图片管理系统|三一图片管理系统|三一图片管理系统|三一图片管理系统
        
        }


        /// <summary>
        /// 默认的图片信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetDefaultImages()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);
            return ds.Tables["DefaultImage"];
        }


        /// <summary>
        /// 默认的图片信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetUploadImages()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);
            return ds.Tables["UploadImage"];
        }

        private string GetImagePath()
        {
            string tmp = "";
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);
            DataTable dt = ds.Tables["Config"];
            if (dt != null && dt.Rows.Count > 0)
            {

                tmp = dt.Rows[0][0].ToString();
                if (!tmp.EndsWith("/"))
                {
                    tmp = tmp + "/";
                }
            }
            return tmp;
        }


    }
}
