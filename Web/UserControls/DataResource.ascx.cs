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
using QJVRMS.Business;


using System.IO;
using System.Text;
using System.Collections.Generic;
using QJVRMS.Business.SecurityControl;
using QJVRMS.Business.ResourceType;
using QJVRMS.Business.Interface;

namespace WebUI.UserControls
{
    public partial class DataResource : BaseUserControl
    {
        public string strNotPass = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            bindLightBox();
        }

        //功能菜单
        private void initFunction()
        {
            if (this.ShowDownload)
            { 
                
            }
        }
        
        //增加
        private bool _isAllEdit;
        public bool IsAllEdit
        {
            get { return this._isAllEdit; }
            set { this._isAllEdit = value; }
        }

        private bool _isAllDownload;
        public bool IsAllDownload
        {
            get { return this._isAllDownload; }
            set { this._isAllDownload = value; }
        }

        private bool _showImageInfo;
        public bool ShowImageInfo
        {
            get { return this._showImageInfo; }
            set { this._showImageInfo = value; }
        }


        private bool _showCheckBox;
        public bool ShowCheckBox
        {
            get { return this._showCheckBox; }
            set { this._showCheckBox = value; }
        }

        private bool _showFavDelete;
        public bool ShowFavDelete
        {
            get
            {
                return this._showFavDelete;
            }
            set
            {
                this._showFavDelete = value;
            }
        }

        private bool _showPreview;
        public bool ShowPreview
        {
            get
            {
                return this._showPreview;
            }
            set
            {
                this._showPreview = value;
            }
        }

        private bool _showFavor;
        public bool ShowFavor
        {
            get
            {
                return this._showFavor;
            }
            set
            {
                this._showFavor = value;
            }
        }

        private bool _showDownload;
        public bool ShowDownload
        {
            get
            {
                return this._showDownload;
            }
            set
            {
                this._showDownload = value;
            }
        }

        private bool _showEdit;
        public bool ShowEdit
        {
            get
            {
                return this._showEdit;
            }
            set
            {
                this._showEdit = value;
            }
        }

        private bool _showValidate;
        public bool ShowValidate
        {
            get
            {
                return this._showValidate;
            }
            set
            {
                this._showValidate = value;
            }
        }

        private bool _showTiJiao;
        public bool ShowTiJiao
        {
            get
            {
                return this._showTiJiao;
            }
            set
            {
                this._showTiJiao = value;
            }
        }


        private bool _showStatus;
        public bool ShowStatus
        {
            get { return this._showStatus; }
            set { this._showStatus = value; }
        }


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
                Session["DataResource"] = this.dataSource;

                this.SearchCondition = null;

                // this.DataList1.DataBind();                
            }
        }

        /// <summary>
        /// 保存搜索条件
        /// </summary>
        private Object searchCondition;
        public Object SearchCondition
        {
            set { this.searchCondition = value;
            Session["SearchCondition"] = this.searchCondition;
            }
        }

        public override void DataBind()
        {
            this.DataList1.DataBind();
            DataList1.Dispose();
            dataSource = null;
        }

        private void bindLightBox()
        {
            Resource r = new Resource();
            DataSet ds = r.GetMyLightBox(CurrentUser.UserId);
            this.rptLightBox.DataSource = ds.Tables[0].DefaultView;
            this.rptLightBox.DataBind();
        
        }


        protected string GetDocumentImage(string ServerFileName, string ServerFolderName)
        {
            string imageUrl = string.Empty;

            imageUrl = UIBiz.CommonInfo.GetResourceImageUrl(170, ServerFileName, ServerFolderName);

            return imageUrl;

        }

        //得到显示的图片的路径

        protected string GetImgUrl(string ServerFileName, string ServerFolderName)
        {
            string _ret = string.Empty;

            ImageType objImg = new ImageType();
            //yangguang
            //_ret = objImg.PreviewPath_170_Read + ServerFolderName + "/" + ServerFileName;
            _ret = objImg.GetPreviewPathRead(ServerFolderName, ServerFileName, "170");
            return _ret;
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
                VideoType objV = new VideoType();
                //yangguang
                //string videoPreviewPath =objV.PreviewPathRead;
                //if (!string.IsNullOrEmpty(videoPreviewPath))
                //{
                    //_ret = videoPreviewPath + "/image/" + FolderName + "/" + ItemSerialNum + ".jpg";
                    _ret = objV.GetPreviewPathRead(FolderName, ItemSerialNum+".jpg", "image");
                //}

            }
            return _ret;
        }

       
        //flv路径
        protected string GetSmallFlvUrl(string FolderName, string ItemSerialNum, int status)
        {
            string _ret = string.Empty;
            VideoType objV = new VideoType();
            //yangguang
            //string videoPreviewPath = objV.PreviewPathRead;
            //if (!string.IsNullOrEmpty(videoPreviewPath))
            //{
                //_ret = videoPreviewPath + "/smallFlv/" + FolderName + "/" + ItemSerialNum + ".flv";
            _ret = objV.GetPreviewPathRead(FolderName, ItemSerialNum + ".flv", "smallFlv");
            //}

            return Server.UrlEncode(_ret);
        }



        /// <summary>
        /// 图片下载
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        protected string GetCmd(string picId, string ptype, string itemId, string folder)
        {
            string strPicid = picId.ToString();

            if (Request.IsAuthenticated && bool.Parse(CurrentUser.IsDownLoad))
            {

                return "<a class='small_sample' target='_self' href=\"javascript:downhigh('" + strPicid + "','" + ptype + "','" + itemId + "','" + folder + "')\">下载</a>";
            }

            else
            {
                return "";
            }

        }


        /// <summary>
        /// 取字符串左边几个字符
        /// </summary>
        /// <param name="strInput">输入字符串</param>
        /// <param name="i">取多少个字符</param>
        /// <returns></returns>
        public string GetLeftString(string strInput, int i)
        {
            return string.IsNullOrEmpty(strInput) ? string.Empty : ((i > strInput.Length) ? strInput : strInput.Substring(0, i));
        }

        
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                ((Literal)e.Item.FindControl("lrover")).Text = "";
                DataRowView drv = (DataRowView)e.Item.DataItem;
                string ResourceType = Convert.ToString(drv.Row["ResourceType"]);
                IResourceType objRT = ResourceTypeFactory.getResourceTypeByString(ResourceType);
                Panel p = (Panel)e.Item.FindControl("p" + ResourceType);
                p.Visible = true;

                int validateStatus = 0;

                validateStatus = int.Parse(drv.Row["validateStatus"].ToString());
                 
                string ServerFileName=Convert.ToString(drv.Row["ServerFileName"]);
                string ServerFolderName=Convert.ToString(drv.Row["ServerFolderName"]);
                
                int status=Convert.ToInt32(drv.Row["Status"]);
                string itemId = drv.Row["ID"].ToString();


                bool bEdit = false;
                bool bDownload = false;
                if (IsSuperAdmin)
                {
                    bEdit = true;
                    bDownload = true;
                }
                else
                {
                    //根据resourceId 得到分类ID，一个资源可能属于多个分类 ，是否显示编辑
                    

                    DataSet ds = new Resource().GetResourceCatalogByItemId(itemId);
                    int icount = ds.Tables[0].Rows.Count;
                    List<ObjectRule> rules = new List<ObjectRule>(icount);
                    List<ObjectRule> rulesDownload = new List<ObjectRule>(icount);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ISecurityObject securityObj = new SecurityObject(new Guid(dr["CatalogId"].ToString()), SecurityObjectType.Items);
                        ObjectRule or = new ObjectRule(securityObj, new User(CurrentUser.UserId), OperatorMethod.Modify);

                        rules.Add(or);
                        or = new ObjectRule(securityObj, new User(CurrentUser.UserId), OperatorMethod.Download);
                        rulesDownload.Add(or);
                    }
                    ObjectRule.CheckRules(rules);

                    foreach (ObjectRule obj in rules)
                    {
                        bEdit = bEdit || obj.IsValidate;
                    }


                    ObjectRule.CheckRules(rulesDownload);
                    foreach (ObjectRule or in rulesDownload)
                    {
                        bDownload = bDownload || or.IsValidate;
                    }

                }

                //判断是否有编辑和下载权限，只要有个图片有权限即可
                IsAllEdit = IsAllEdit || bEdit;
                IsAllDownload = IsAllDownload || bDownload;
               

                if (this.ShowDownload)
                {
                    ((Panel)e.Item.FindControl("pDownload")).Visible = bDownload;
                }

                if (this.ShowEdit)
                {

                    ((Panel)e.Item.FindControl("pEdit")).Visible = bEdit;
                }
                if (this.ShowFavDelete)
                {

                    ((Panel)e.Item.FindControl("pFavDelete")).Visible = true;
                }


                if (validateStatus == (int)ResourceEntity.ResourceStatus.NewUpload) //新上传的，显示提交
                {
                    if (this.ShowTiJiao)
                    {
                        ((Panel)e.Item.FindControl("pTiJiao")).Visible = true;

                        if (this.ShowCheckBox)
                        {
                            ((HtmlInputCheckBox)e.Item.FindControl("ckbItemId")).Visible = this.ShowCheckBox;
                        }

                    }
                    if (this.ShowEdit)
                    {
                        if (CurrentUser.UserLoginName.ToLower().Equals(ServerFolderName.ToLower()))
                        {
                            ((Panel)e.Item.FindControl("pEdit")).Visible = true;
                        }
                        else

                        {
                            ((Panel)e.Item.FindControl("pEdit")).Visible = bEdit;
                        }
                    }
                   

                    if (this.ShowStatus)
                    {
                        ((Label)e.Item.FindControl("spanStatus")).Visible = true;
                        ((Label)e.Item.FindControl("spanStatus")).Text = "新上传";
                    }
                    
                    
                }
                else if (validateStatus == (int)ResourceEntity.ResourceStatus.IsProcessing) //已经提交了，显示审核中
                {
                    ((Panel)e.Item.FindControl("pTiJiao")).Visible = false;
                       ((Panel)e.Item.FindControl("pValidate")).Visible = this.ShowValidate;

                    if (this.ShowEdit)
                    {
                        ((Panel)e.Item.FindControl("pEdit")).Visible = false;
                    }

                    if (this.ShowValidate)
                    {
                        if (this.ShowCheckBox)
                        {
                            ((HtmlInputCheckBox)e.Item.FindControl("ckbItemId")).Visible = this.ShowCheckBox;
                        }
                    }

                    if (this.ShowStatus)
                    {
                        ((Label)e.Item.FindControl("spanStatus")).Visible = true;
                        ((Label)e.Item.FindControl("spanStatus")).Text = "审核中";
                    }


                }
                else if (validateStatus == (int)ResourceEntity.ResourceStatus.NotPass) //没有被通过，显示 未通过
                {
                     if (this.ShowEdit)
                    {
                        //((Panel)e.Item.FindControl("pEdit")).Visible = bEdit;
                        if (CurrentUser.UserLoginName.ToLower().Equals(ServerFolderName.ToLower()))
                        {
                            ((Panel)e.Item.FindControl("pEdit")).Visible = true;
                        }
                        else
                        {
                            ((Panel)e.Item.FindControl("pEdit")).Visible = bEdit;
                        }
                    }


                    if (this.ShowTiJiao)
                    {
                        ((Panel)e.Item.FindControl("pTiJiao")).Visible = true;

                        if (this.ShowCheckBox)
                        {
                            ((HtmlInputCheckBox)e.Item.FindControl("ckbItemId")).Visible = this.ShowCheckBox;
                        }

                    }

                    ((Panel)e.Item.FindControl("pValidate")).Visible = this.ShowValidate;

                    if (this.ShowStatus)
                    {
                        ((Label)e.Item.FindControl("spanStatus")).Visible = true;
                        ((Label)e.Item.FindControl("spanStatus")).Text = "未通过";
                        ((Label)e.Item.FindControl("spanStatus")).ToolTip = GetNotPassReason(itemId);

                        strNotPass=" onmouseover=\"notpassover(this,'"+ itemId+"')\" id=\"imgContainer1-"+ itemId+"\"";
                        //strNotPass = "dddd";
                        //lrover
                        ((Literal)e.Item.FindControl("lrover")).Text = strNotPass;


                    }
                    


                }

                else if (validateStatus == (int)ResourceEntity.ResourceStatus.IsPass) //被通过，显示 通过
                {
                    if (this.ShowEdit)
                    {
                        ((Panel)e.Item.FindControl("pEdit")).Visible = bEdit;
                    }


                    if (this.ShowStatus)
                    {
                        ((Label)e.Item.FindControl("spanStatus")).Visible = true;
                        ((Label)e.Item.FindControl("spanStatus")).Text = "已上线";

                    }

                }
                else
                {
                    //if (this.ShowEdit)
                    //{
                    //    ((Panel)e.Item.FindControl("pEdit")).Visible = false;
                    //}
                }
                
                
                ((Panel)e.Item.FindControl("pPreview")).Visible = this.ShowPreview;
                ((Panel)e.Item.FindControl("pFavor")).Visible = this.ShowFavor;


                //是否显示图像详细信息
                if (this.ShowImageInfo)
                {
                    //获取文件详细信息
                    ResourceEntity model = new Resource().GetResourceInfoByItemId(itemId);
                    string imageWH=string.Empty;

                    if (model.ResourceType.Equals("image")) {
                        ImageInfo o = new Resource().GetImageInfoBySN(model.ItemSerialNum);
                        
                            imageWH = o.Width.ToString() + "x" + o.Height.ToString();                        
                    }
                    string imgExt = Path.GetExtension(model.FileName);
                    if (imgExt.IndexOf(".") > -1)
                    {
                        imgExt = imgExt.Substring(1);
                    }
                    string imgInfo = String.Format("<span class='imgInfo'><span class='imgInfoLeft'>{0} | {1}</span><span class='imgInfoRight'>{2}</span></span>",imgExt.ToUpper(),imageWH,QJVRMS.Common.Tool.toFileSize(model.FileSize));                   

                    Literal objImgInfo=(Literal)e.Item.FindControl("imgInfo");
                    objImgInfo.Visible = true;
                    //objImgInfo.Text = "[TIF|1500x4000,32.5M ]";
                    objImgInfo.Text = imgInfo;
                }


            }

           
        }
        protected string GetPreviewUrl(string ResourceType, string itemId)
        {
            IResourceType obj = ResourceTypeFactory.getResourceTypeByString(ResourceType);
            return obj.DetailPage + "?ItemID="+itemId;
            
        
        }

        protected string GetNotPassReason(string itemId)
        {
            Resource obj = new Resource();
            return obj.GetNotPassReason(itemId);

            
        }
    }
}