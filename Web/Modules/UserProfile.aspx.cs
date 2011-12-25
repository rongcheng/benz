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
using System.Text;
using System.IO;
using QJVRMS.Business;
using QJVRMS.Common;
using QJVRMS.Business.Interface;
using QJVRMS.Business.ResourceType;
using System.Security.AccessControl;

namespace WebUI.Modules
{
    public partial class UserProfile : AuthPage 
    {
        public string loginnames;
        public string userid;

        private int _curpageMyUpload;
        private int _curpageMyLightBox;

        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!Page.IsPostBack)
            {
                this.dt_Date.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
                this.de_Date.Text = DateTime.Now.ToShortDateString();

                this.myOrder_StartDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.AddMonths(-1).Month.ToString() + "-1";
                this.myOrder_EndDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");


                this.myUpload_StartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                this.myUpload_EndDate.Text = DateTime.Now.AddDays(1).ToShortDateString();

                this.myUploadStat_StartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                this.myUploadStat_EndDate.Text = DateTime.Now.AddDays(1).ToShortDateString();


                
                GetUserInfo();
                //GetItems();
                GetDownloadLog();
                GetResourceStatus();
                //bindMyUpload();
                GetCalendars();
                bindStatus();

                bindMyUploadStat();
                this.pMyUpload1.Visible = false;
                this.pMyUpload2.Visible = true;


                

                //默认显示哪一个 tab

                string tabId = Request["tabId"];
                if (!string.IsNullOrEmpty(tabId))
                {
                    try
                    {
                        this.profileContainer.ActiveTabIndex = Convert.ToInt32(tabId);
                    }
                    catch (Exception ex)
                    {
                        LogWriter.WriteExceptionLog(ex);

                    }

                    //处理订单
                    if (tabId.Equals("2"))
                    {

                        //QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
                        //obj.SetOrderReadStatus(CurrentUser.UserId);

                        //string _orderStatus = Request.QueryString["orderStatus"];
                        //if (!string.IsNullOrEmpty(_orderStatus))
                        //{
                        //    this.ddlStatus.SelectedValue = _orderStatus;
                            
                        //}
                        //bindMyOrders();
                    }
                    else if (tabId.Equals("4"))
                    {

                        string _resourceStatus = Request.QueryString["resourceStatus"];
                        if (!string.IsNullOrEmpty(_resourceStatus))
                        {
                            this.ddlResoureStatus.SelectedValue = _resourceStatus;

                            this._curpageMyUpload = 0;
                            if (_curpageMyUpload != 0) _curpageMyUpload--;

                            this.pbMyUpload.CurrentPageIndex = 1;
                            this.AspNetPagerUpload.CurrentPageIndex = 1;
                            int validateStatus = Convert.ToInt32(this.ddlResoureStatus.SelectedValue);

                            bindMyUpload(this.pbMyUpload.PageSize, this._curpageMyUpload, validateStatus);

                            this.pMyUpload1.Visible = true;
                            this.pMyUpload2.Visible = false;
                        }

                    }

                    else
                    {
                        this._curpageMyLightBox = 1;
                        //if (_curpageMyLightBox != 0) _curpageMyLightBox--;
                        this.AspNetPagerLightBox.CurrentPageIndex = 1;

                    }
                }

                bindLightBox();
                bindMyLightBox(this.AspNetPagerLightBox.PageSize, this.AspNetPagerLightBox.CurrentPageIndex);


            }
        }

        private void bindLightBox()
        {
            Resource r = new Resource();
            DataSet ds = r.GetMyLightBox(CurrentUser.UserId);
           // this.ddlMyLightBox.DataSource = ds.Tables[0].DefaultView;

            //this.ddlMyLightBox.DataBind();
            this.ddlMyLightBox.DataSource = ds.Tables[0].DefaultView;
            this.ddlMyLightBox.DataTextField = "Title";
            this.ddlMyLightBox.DataValueField = "id";
            this.ddlMyLightBox.DataBind();



        }



        private void bindStatus()
        {
            this.ddlStatus.DataSource = QJVRMS.Business.Orders.GetOrderStatus();
            this.ddlStatus.DataTextField = "CnName";
            this.ddlStatus.DataValueField = "ID";
            this.ddlStatus.DataBind();

            ListItem topItem = new ListItem("全部", "-1");
            this.ddlStatus.Items.Insert(0, topItem);
            this.ddlStatus.SelectedIndex = 0;
        }

        private void GetCalendars() {
            QJVRMS.Business.CalendarFactory calendarFactory = new CalendarFactory();
            this.head.InnerHtml = calendarFactory.ShowHead(DateTime.Now.Year, DateTime.Now.Month, CurrentUser.UserLoginName);
            this.content.InnerHtml = calendarFactory.ShowContent(DateTime.Now.Year, DateTime.Now.Month, CurrentUser.UserLoginName);
            //DateTime time = DateTime.Now;
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < 7; i++) {
            //    time = DateTime.Now.AddDays(double.Parse(i.ToString()));
            //    //sb.Append("<div>");
            //    sb.Append("<a class=\"ahead\">");
            //    sb.Append("<table class=\"head\" width=\"100%\" onclick=\"javascript:ShowCalendar('" + time.ToString("yyyy-MM-dd") + "', '" + CurrentUser.UserLoginName + "')\"><tr>");
            //    sb.Append("<td align=\"left\"><strong>" + time.ToString("yyyy-MM-dd") + "<br />" + time.Date.DayOfWeek.ToString() + "</strong></td>"); ;
            //    sb.Append("<td align=\"right\"><a href=\"EditCalendar.aspx?type=Add&time=" + time.ToString("yyyy-MM-dd") + "\" target=\"_blank\">添加</a></td>");
            //    sb.Append("</tr></table>");
            //    sb.Append("</a>");
            //    sb.Append("<div id=\"" + time.ToString("yyyy-MM-dd") + "\" class=\"contentcan\" style=\"display:none;\">");
            //    sb.Append("</div>");
            //    //sb.Append("</div>");
            //}

            //this.Content.InnerHtml = sb.ToString();
        }


        #region User Info

        public void GetUserInfo()
        {
          
             QJVRMS.Business.MemberShipManager msm = new QJVRMS.Business.MemberShipManager();
             QJVRMS.Business.User user = msm.GetUser(CurrentUser.UserId);


            loginname.Text = user.UserLoginName;
            name.Text = user.UserName;
            tel.Text = user.Telphone;
            email.Text = user.Email;

            if (user.IsLocked)
            {
                state.Text = "无效";
            }
            else
            {
                state.Text = "有效";
            }

          

            userGroup.Text = user.GroupName;

           // Response.Write(CurrentUser.UserId.ToString()+" "+CurrentUser.UserGroupId.ToString() + " " + CurrentUser.GroupName);

        }

        #endregion

        #region LightBox

        protected void GetItems()
        {

            using (DataTable dt = QJVRMS.Business.ImageStorageClass.GetLightBoxItems(this.CurrentUser.UserId))
            {
               

                this.drMyFavorite.DataSource = dt;
                this.drMyFavorite.DataBind();
                
            }
        }


        /// <summary>
        /// 绑定收藏夹
        /// </summary>
        protected void bindMyLightBox(int pageSize,int pageIndex)
        {
            string lightboxId = this.ddlMyLightBox.SelectedValue;
            if (!string.IsNullOrEmpty(lightboxId))
            {

                
                Resource r = new Resource();
                DataSet ds=r.GetResourcesByLightBoxID(new Guid(lightboxId),pageSize,pageIndex);
                this.drMyFavorite.DataSource = ds.Tables[0];
                this.drMyFavorite.DataBind();

                int count = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
                this.lightboxRowCount.Text = count.ToString();

                this.MyLightBoxDownload.Visible = (count>0);
                this.MyLightBoxDownload1.Visible = this.MyLightBoxDownload.Visible;
                this.MyLightBoxDownload.NavigateUrl = "MyLightBoxDownload.aspx?id="+lightboxId;
                this.MyLightBoxDownload1.NavigateUrl = this.MyLightBoxDownload.NavigateUrl;


                this.AspNetPagerLightBox.RecordCount = count;
            
            }
        }

        protected string GetUrl(string shortPath)
        {
            return WebUI.UIBiz.CommonInfo.SlImageRootPath170Read + shortPath;
        }

        protected string GetCmd(string picId, string itemId, string itemPath)
        {
            string ptype = string.Empty, folder = string.Empty;
          

            if ( bool.Parse(CurrentUser.IsDownLoad))
            {
                string[] split = itemPath.Split('/');

                if (split.Length == 2)
                {
                    folder = split[0];
                    ptype = System.IO.Path.GetExtension(split[1]);
                }

                return "<a class='small_sample' target='_self' href=\"javascript:downhigh('" + picId + "','" + ptype + "','" + itemId + "','" + folder + "')\">下载</a>";
            }

            else
            {
                return "";
            }

        }

        //protected void pageBar_PageChanged(object src, QJ.WebControls.PageChangedEventArgs e)
        //{
        //    this.pageBar.CurrentPageIndex = e.NewPageIndex;
        
        //    BindData(PageBar1.PageSize, _curpage);
        //}
        #endregion


        #region MyDownload

        protected void GetDownloadLog()
        {
            DateTime begin = Convert.ToDateTime(dt_Date.Text);
            DateTime end = Convert.ToDateTime(de_Date.Text);

            //DataSet ds = QJVRMS.Business.ImageStorage.GetDownLoadMessage(CurrentUser.UserLoginName, begin, end);
            DataSet ds = QJVRMS.Business.Resource.GetDownLoadMessage(CurrentUser.UserLoginName, begin, end);


            if (ds.Tables[0].Rows.Count != 0)
            {
                // PageBar1.RecordCount = ds.Tables[0].Rows.Count;
                downPageBar.RecordCount = ds.Tables[0].Rows.Count;
                PagedDataSource pd = new PagedDataSource();
                pd.DataSource = ds.Tables[0].DefaultView;
                pd.AllowPaging = true;
                pd.PageSize = downPageBar.PageSize;
                pd.CurrentPageIndex = downPageBar.CurrentPageIndex - 1;
                dlogList.DataSource = pd;//ds;
                dlogList.DataBind();
            }
            else
            {
                dlogList.DataSource = null;//ds;
                dlogList.DataBind();
            }

        }

        protected void downPageBar_PageChanged(object src, QJ.WebControls.PageChangedEventArgs e)
        {

            this.downPageBar.CurrentPageIndex = e.NewPageIndex;

            GetDownloadLog();

        }

        //图片路径        
        protected string GetImgUrl(string ItemSerialNum, string fileType,string folder)
        {
            //return UIBiz.CommonInfo.GetImageUrl(170, folder, ItemSerialNum, ImageType);
            ImageType obj=new ImageType();
            //yangguang
            //string _ret = obj.PreviewPath_170_Read + "/" + folder + "/" + ItemSerialNum + ""+fileType;
            if (fileType.ToLower() == ".cr2" || fileType.ToLower() == ".nef" || fileType.ToLower() == ".psd")
                fileType = ".jpg";
            string _ret = obj.GetPreviewPathRead(folder, ItemSerialNum + "" + fileType, "170");
            return _ret;

        }

        protected void searchDate_Click(object sender, EventArgs e)
        {
            GetDownloadLog();
        }

        #endregion

        #region myUpload

        private void GetResourceStatus()
        {
            this.ddlResoureStatus.DataSource = Resource.GetResourceStatus();
            this.ddlResoureStatus.DataTextField = "CnName";
            this.ddlResoureStatus.DataValueField = "ID";
            this.ddlResoureStatus.DataBind();

            ListItem topItem = new ListItem("所有", "-1");
            this.ddlResoureStatus.Items.Insert(0, topItem);
            this.ddlResoureStatus.SelectedIndex = 0;
        }


        protected void btnSearchMyUpload_Click(object sender, EventArgs e)
        {
            this._curpageMyUpload = 0;
            if (_curpageMyUpload != 0) _curpageMyUpload--;

            this.pbMyUpload.CurrentPageIndex = 1;

            int validateStatus = Convert.ToInt32(this.ddlResoureStatus.SelectedValue);

            bindMyUpload(this.pbMyUpload.PageSize, this._curpageMyUpload, validateStatus);

            this.profileContainer.ActiveTab = this.tabMyUpload;
        }


        //分页
        protected void pbMyUpload_PageChanged(object src, QJ.WebControls.PageChangedEventArgs e)
        {
            this._curpageMyUpload = e.NewPageIndex;
            if (_curpageMyUpload != 0) _curpageMyUpload--;

            this.pbMyUpload.CurrentPageIndex = e.NewPageIndex;

            int validateStatus =Convert.ToInt32( this.ddlResoureStatus.SelectedValue) ;
            

            bindMyUpload(this.pbMyUpload.PageSize, this._curpageMyUpload,validateStatus);

            this.profileContainer.ActiveTab = this.tabMyUpload;

        }

        private void bindMyUpload()
        { 
            int pageSize=pbMyUpload.PageSize;
            int pageIndex=_curpageMyUpload;
            bindMyUpload( pageSize,pageIndex,-1);
        }

        private void bindMyUpload(int pageSize, int pageIndex, int validateStatus)
        {
            string beginDate=this.myUpload_StartDate.Text;
            string endDate=this.myUpload_EndDate.Text;
            string userId=CurrentUser.UserId.ToString();
            
            int pageCount = 0;
            int rowCount = 0;


            endDate = Convert.ToDateTime(endDate).AddDays(1).ToString();
            DataSet ds = new Resource().GetResourceByUserID(beginDate, endDate, userId, pageSize, pageIndex, ref pageCount, "", validateStatus);
            DataTable dt1 = ds.Tables[0];
            DataTable dt = ds.Tables[1];

            if (dt1.Rows.Count > 0)
            {
                rowCount = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }
            if (dt.Rows.Count > 0)
            {
                this.drMyUpload.DataSource = dt;
                this.drMyUpload.DataBind();
            }
            else
            {
                this.drMyUpload.DataSource = null;
                this.drMyUpload.DataBind();
            }

            this.pbMyUpload.PageSize = pageSize;
            this.pbMyUpload.RecordCount = rowCount;

            this.AspNetPagerUpload.PageSize = pageSize;
            this.AspNetPagerUpload.RecordCount = rowCount;
        }

        protected void AspNetPagerUpload_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            

            this._curpageMyUpload = e.NewPageIndex;
            if (_curpageMyUpload != 0) _curpageMyUpload--;

            this.AspNetPagerUpload.CurrentPageIndex = e.NewPageIndex;

            int validateStatus = Convert.ToInt32(this.ddlResoureStatus.SelectedValue);


            bindMyUpload(this.AspNetPagerUpload.PageSize, this._curpageMyUpload, validateStatus);

            this.profileContainer.ActiveTab = this.tabMyUpload;


        }


        protected void AspNetPagerLightBox_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {


            this._curpageMyLightBox = e.NewPageIndex;
            //if (_curpageMyLightBox != 0) _curpageMyLightBox--;

            this.AspNetPagerLightBox.CurrentPageIndex = e.NewPageIndex;

            bindMyLightBox(this.AspNetPagerLightBox.PageSize, this.AspNetPagerLightBox.CurrentPageIndex);

            


        }


        private void bindMyUpload(int pageSize, int pageIndex, int validateStatus,DateTime beginDate,DateTime endDate,Guid userId)
        {
    
            int pageCount = 0;
            int rowCount = 0;

            DataSet ds = new Resource().GetResourceByUserID(beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), userId.ToString(), pageSize, pageIndex, ref pageCount, "", validateStatus);
            DataTable dt1 = ds.Tables[0];
            DataTable dt = ds.Tables[1];

            if (dt1.Rows.Count > 0)
            {
                rowCount = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }
            if (dt.Rows.Count > 0)
            {
                this.drMyUpload.DataSource = dt;
                this.drMyUpload.DataBind();
            }

            this.pbMyUpload.PageSize = pageSize;
            this.pbMyUpload.RecordCount = rowCount;

            this.AspNetPagerUpload.PageSize = pageSize;
            this.AspNetPagerUpload.RecordCount = rowCount;
        }


        #endregion

        private void bindMyUploadStat()
        {
            DateTime dtStart = Convert.ToDateTime(this.myUploadStat_StartDate.Text);
            DateTime dtEnd = Convert.ToDateTime(this.myUploadStat_EndDate.Text).AddDays(1);

            DataSet ds = new Resource().GetMyUploadStatus(dtStart, dtEnd, CurrentUser.UserId);
            this.grvMyUploadStat.DataSource = ds.Tables[0].DefaultView;
            this.grvMyUploadStat.DataBind();
        }


        #region 不用的代码
        protected string showImage(string ServerFileName, string ServerFolderName, string ResourceType,int status,string ItemId)
        {
            StringBuilder sb = new StringBuilder("");
            string imageUrl = string.Empty;
            string image = @"<a href='PicDetail.aspx?ItemID={0}' target='_blank'>
                             <img id='Img1'  alt=''  src='{1}'/></a>";

            string videoText = @"<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0' width='170' height='128' id='QuanJingFilm' align='middle'>
                        <param name='allowScriptAccess' value='always' />
                        <param name='allowFullScreen' value='false' />
                        <param name='movie' value='../../UI/flash/QJFilm.swf' />

                        <param name='quality' value='autohigh' />
                        <param name='bgcolor' value='#ffffff' />
                        <param name='wmode' value='opaque' />
                        <param name='FlashVars' value='videoUrl={0}&imgUrl={1}&SerailNumber={2}' />
                        <embed src='../../UI/flash/qjFilm.swf' quality='autohigh' bgcolor='#ffffff' width='170' height='128' wmode='opaque' flashvars='videoUrl={0}&imgUrl={1}&SerailNumber={2}'
                            name='QuanJingFilm' align='middle' allowscriptaccess='always' allowfullscreen='false' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' />
                    </object>";
            

            if (ResourceType.Equals("image"))
            {
                imageUrl = UIBiz.CommonInfo.GetResourceImageUrl(170, ServerFileName, ServerFolderName);

                sb.Append(string.Format(image,ItemId,imageUrl));
                    
            }
            else if (ResourceType.Equals("video"))
            {
                imageUrl = GetVideoImgUrl(ServerFolderName, Path.GetFileNameWithoutExtension(ServerFileName), status);
                string videoUrl=string.Empty;
                videoUrl=GetSmallFlvUrl(ServerFolderName, Path.GetFileNameWithoutExtension(ServerFileName), status);
                sb.Append(string.Format(videoText, videoUrl, imageUrl,ItemId));
                
            }


            return sb.ToString();

        }

        protected string showText(string ServerFileName, string ServerFolderName, string ResourceType, string itemId)
        {
            StringBuilder sb = new StringBuilder("");
            string strText = @"{2} <a class='small_sample' href='{0}?ItemID={1}' target='_blank'>预览</a>
                               <a class='small_sample' href='javascript:void(0)' onclick='DelItem(""{1}"",event)'>删除</a>";
            string videoImage = @"<img src='../images/video.gif' width='20' height='20' title='视频' />";
            if (ResourceType.Equals("image"))
            {
                //strText = UIBiz.CommonInfo.GetResourceImageUrl(170, ServerFileName, ServerFolderName);

                sb.Append(string.Format(strText, "../PicDetail.aspx", itemId,""));

            }
            else if (ResourceType.Equals("video"))
            {

                sb.Append(string.Format(strText, "Video/Detail.aspx", itemId, videoImage));

            }


            return sb.ToString();

        }

        protected void list_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal l = (Literal)e.Item.FindControl("imgText");

                Literal l1 = (Literal)e.Item.FindControl("imageV");

                DataRowView drv = (DataRowView)e.Item.DataItem;
                string ServerFileName = Convert.ToString(drv.Row["ServerFileName"]);
                string ServerFolderName = Convert.ToString(drv.Row["ServerFolderName"]);
                string ResourceType = Convert.ToString(drv.Row["ResourceType"]);

                int status=0;
                if (drv.Row["status"] != DBNull.Value)
                {
                    status = Convert.ToInt32(drv.Row["Status"]);
                }

                string itemId = drv.Row["resourceItemID"].ToString();


                if (l != null)
                {
                    l.Text = showImage(ServerFileName, ServerFolderName, ResourceType, status, itemId);
                }

                if (l1 != null)
                {
                    l1.Text = showText(ServerFileName, ServerFolderName, ResourceType, itemId);
                }


            }

        }

        //图片路径
        protected string GetVideoImgUrl(string FolderName, string ItemSerialNum, int status)
        {
            string _ret = string.Empty;
            if (status == 2)
            {
                _ret = "/images/videoconverterror.gif";
            }
            else if (status == 0)
            {
                _ret = "/images/videoconverting.gif";
            }
            else
            {
                string videoPreviewPath = ConfigurationManager.AppSettings["videoPreviewPathRead"];
                if (!string.IsNullOrEmpty(videoPreviewPath))
                {
                    _ret = videoPreviewPath + "image/" + FolderName + "/" + ItemSerialNum + ".jpg";
                }

            }
            //return UIBiz.CommonInfo.GetImageUrl(170, FolderName, ItemSerialNum, ImageType);

            return _ret;

        }

        //flv路径
        protected string GetFlvUrl(string FolderName, string ItemSerialNum, int status)
        {
            string _ret = string.Empty;
            if (status == 2)
            {
                //_ret = "/images/videoconverterror.gif";
            }
            else if (status == 0)
            {
                //_ret = "/images/videoconverting.gif";
            }
            else
            {
                string videoPreviewPath = ConfigurationManager.AppSettings["videoPreviewPathRead"];
                if (!string.IsNullOrEmpty(videoPreviewPath))
                {
                    _ret = videoPreviewPath + "flv/" + FolderName + "/" + ItemSerialNum + ".flv";
                }

            }
            //return UIBiz.CommonInfo.GetImageUrl(170, FolderName, ItemSerialNum, ImageType);

            return Server.UrlEncode(_ret);

        }

        //flv路径
        protected string GetSmallFlvUrl(string FolderName, string ItemSerialNum, int status)
        {
            string _ret = string.Empty;
            if (status == 2)
            {
                //_ret = "/images/videoconverterror.gif";
            }
            else if (status == 0)
            {
                //_ret = "/images/videoconverting.gif";
            }
            else
            {
                string videoPreviewPath = ConfigurationManager.AppSettings["videoPreviewPathRead"];
                if (!string.IsNullOrEmpty(videoPreviewPath))
                {
                    _ret = videoPreviewPath + "smallFlv/" + FolderName + "/" + ItemSerialNum + ".flv";
                }

            }
            //return UIBiz.CommonInfo.GetImageUrl(170, FolderName, ItemSerialNum, ImageType);

            return Server.UrlEncode(_ret);

        }

        #endregion 不同的代码

        protected void lbMyuploadDays_Click(object sender, EventArgs e)
        {
            this.pMyUpload1.Visible = false;
            this.pMyUpload2.Visible = true;
            this.profileContainer.ActiveTab = this.tabMyUpload;
        }

        protected void grvMyUploadStat_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            
            GridViewRow gvr = e.Row;
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                string _date = gvr.Cells[0].Text;
                string _newUploadCount = gvr.Cells[1].Text;

                int iNewUploadCount = 0;
                if (int.TryParse(_newUploadCount, out iNewUploadCount))
                {
                    if (iNewUploadCount == 0)
                    {
                        LinkButton lb = (LinkButton)gvr.FindControl("lbMyuploadStatTiJiao");
                        lb.Visible = false;
                    }
                }
            }
        
        }

        protected void btnMyUploadStat_Click(object sender, EventArgs e)
        {
            bindMyUploadStat();
        }

        protected void lbMyuploadStatTiJiao_Click(object sender,CommandEventArgs e)
        {
            string date = e.CommandArgument.ToString();
            DateTime dtStart = Convert.ToDateTime(date);
            DateTime dtEnd = dtStart.AddDays(1);

            Resource obj = new Resource();
            obj.ValidateResourceByUserDate1(CurrentUser.UserId, dtStart, dtEnd, (int)ResourceEntity.ResourceStatus.NewUpload ,(int)ResourceEntity.ResourceStatus.IsProcessing);
           


            bindMyUploadStat();
            this.profileContainer.ActiveTab = this.tabMyUpload;

        
        }
        protected void ShowMyUploadStat(object sender, CommandEventArgs e)
        {
            string date = e.CommandArgument.ToString();
            //DateTime dtStart = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(4,2)), Convert.ToInt32(date.Substring(6,2)));

            DateTime dtStart = Convert.ToDateTime(date);
            DateTime dtEnd = dtStart.AddDays(1);

            //int pageSize = pbMyUpload.PageSize;
            int pageSize = this.AspNetPagerUpload.PageSize;

            int pageIndex = _curpageMyUpload;

            this.myUpload_StartDate.Text = dtStart.ToString("yyyy-MM-dd");
            this.myUpload_EndDate.Text = dtEnd.ToString("yyyy-MM-dd");



            bindMyUpload(pageSize, 0, -1, dtStart, dtEnd, CurrentUser.UserId);

            this.pMyUpload1.Visible = true;
            this.pMyUpload2.Visible = false;
            this.profileContainer.ActiveTab = this.tabMyUpload;
         
        }


        protected void btnSearchMyOrder_Click(object sender, EventArgs e)
        {
           
            this.bindMyOrders();

            this.profileContainer.ActiveTab = this.tabMyOrder;
        }

        protected void bindMyOrders()
        {
            DateTime begin = Convert.ToDateTime(this.myOrder_StartDate.Text);
            DateTime end = Convert.ToDateTime(this.myOrder_EndDate.Text).AddDays(1);
            string userId=CurrentUser.UserId.ToString();
            int status = Convert.ToInt32(this.ddlStatus.SelectedValue);
            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            DataSet ds = obj.GetOrdersByUserId(userId, 10000, 1, begin, end,status);
            this.grvOrders.DataSource = ds.Tables[0];
            this.grvOrders.DataBind();

        }
        protected void grvOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvOrders.PageIndex = e.NewPageIndex;
            this.bindMyOrders();

        }

        /// <summary>
        /// 订单列表绑定时，当订单状态为完成时，显示确认订单的按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int state = Convert.ToInt32(e.Row.Cells[0].Text);
                if (state == (int)OrderStatus.Completed)
                {
                    ((LinkButton)e.Row.FindControl("btnConfirmOrder")).Visible = true;
                }
                else {
                    ((LinkButton)e.Row.FindControl("btnConfirmOrder")).Visible = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
           
        }



        protected void grvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower().Equals("page"))
            {
                return;
            }
           
            int toStatus = 0;
            if (e.CommandName.ToLower().Equals("confirmorder"))
            {
                toStatus = (int)OrderStatus.Confirmed;
            }
            string id = e.CommandArgument.ToString();

            QJVRMS.Business.Orders obj = new QJVRMS.Business.Orders();
            if (obj.UpdateStatus(id, toStatus))
            {
                ShowMessage("操作完成");
                bindMyOrders();
            }
        }

        public string GetStatus(string status)
        {
            DataTable dt = QJVRMS.Business.Orders.GetOrderStatus();
            DataRow[] drs = dt.Select("ID=" + status);
            if (drs.Length == 1)
            {
                return drs[0]["CnName"].ToString();
            }

            return "";

        }

        protected void btnMyLightBox_Click(object sender, EventArgs e)
        {
           // bindMyLightBox();
        }

        protected void ddlMyLightBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindMyLightBox(this.AspNetPagerLightBox.PageSize,1);
            this.AspNetPagerLightBox.CurrentPageIndex = 1;

        }

        protected void lbClearMyLightbox_Click(object sender, EventArgs e)
        {
            
            string lightboxId = this.ddlMyLightBox.SelectedValue;
            if (!string.IsNullOrEmpty(lightboxId))
            {
                Resource r = new Resource();
                if (r.ClearLightBox(new Guid(lightboxId)))
                {
                    ShowMessage("清除收藏夹成功过");
                    bindMyLightBox(this.AspNetPagerLightBox.PageSize,1);
                }
                else
                { 
                    
                }
            }
        }



       
    }
}
