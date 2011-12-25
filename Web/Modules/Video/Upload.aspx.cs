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
using System.Diagnostics;
using System.Drawing;
using QJVRMS.Business;
using QJVRMS.Common;
using System.IO;
using System.Net;
using System.Runtime;
using System.Text;

namespace WebUI.Modules.Video
{
    public partial class Upload :  AuthPage 
    {
        string ImageRootPath;//WebUI.UIBiz.CommonInfo.ImageRootPath;
        string SlImageRootPath; //WebUI.UIBiz.CommonInfo.SlImageRootPath;
        public string _videoFormat;
        public string hvsp;
        //  static string imgSuffixStr = "jpg,png,psd,ai,jpeg,gif,bmp,tiff,pcx,tga,exif,fpx";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                initCalendar();
                _videoFormat = new VideoController().GetVideoFormats();
            }
        }

        private void initCalendar()
        {
            t_Date.Text = DateTime.Now.ToShortDateString();
            Calendar_StartDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
            Calendar_EndDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
        }
        protected bool CheckImageType(string type)
        {
            switch (type)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".psd":
                    return true;
                case ".ai":
                    return true;
                case ".jpeg":
                    return true;
                case ".gif":
                    return true;
                case ".bmp":
                    return true;
                case ".tiff":
                    return true;
                case ".pcx":
                    return true;
                case ".tga":
                    return true;
                case ".exif":
                    return true;
                case ".fpx":
                    return true;
                default:
                    return false;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }

        
        protected void btnUpload_ServerClick(object sender, EventArgs e)
        {

            if (this.t_Date.Text.Trim() == string.Empty)
            {
                this.ShowMessage(this, "请选择时间");
                return;
            }
            else
            {
                if (Convert.ToDateTime(this.t_Date.Text) > DateTime.Now)
                {
                    this.ShowMessage(this, "拍摄时间应比现在早");
                    return;
                }
            }
            //验证日期
            DateTime sDate = new DateTime();
            DateTime eDate = new DateTime();
            if (this.Calendar_StartDate.Text != "")
            {
                sDate = Convert.ToDateTime(this.Calendar_StartDate.Text);
                if (sDate <= DateTime.Now)
                {
                    this.ShowMessage(this, "有效开始日期应比现在日期晚");
                    return;
                }
            }
            else
            {
                sDate = Convert.ToDateTime("1900-01-01");
            }
            if (this.Calendar_EndDate.Text != "")
            {
                eDate = Convert.ToDateTime(this.Calendar_EndDate.Text);

                if (sDate == Convert.ToDateTime("1900-01-01") && eDate < DateTime.Now)
                {
                    this.ShowMessage(this, "有效结束日期应至少比现在日期晚");
                    return;
                }
                else if (eDate < sDate)
                {
                    this.ShowMessage(this, "有效结束日期应比有效开始日期晚");
                    return;
                }
            }
            else
            {
                eDate = Convert.ToDateTime("1900-01-01");
            }

            //根节点
            TreeNode parentNode = catalogTree.RootNode;
            //获取checked的节点List
            ArrayList nodeList = new ArrayList();
            this.catalogTree.ArrCheckbox(nodeList, parentNode);

            ArrayList catalogIds = new ArrayList(nodeList.Count);
            foreach (TreeNode node in nodeList)
            {

                catalogIds.Add(new Guid(node.Value));
            }

            if (catalogIds.Count == 0)
            {
                this.ShowMessage(this, "没有选择分类,上传失败!");
                return;
            }

            
            

            ImageStorage img = new ImageStorage();

            string fileName = "";   //原始文件名
            if (!string.IsNullOrEmpty(Request["selectedFile"]))
            {
                fileName = Request["selectedFile"].ToString();
            }
            string uploadFileName = ""; //上传以后重新分配的文件名 prefix+yymmdd+00001.extention
            if (!string.IsNullOrEmpty(Request["uploadFileName"]))
            {
                uploadFileName = Request["uploadFileName"].ToString();
            }

            VideoStorageClass vsc = new VideoStorageClass();
            VideoStorage v = new VideoStorage();
            v.Caption = this.txt_Caption.Value;
            v.Description = this.description.Value;
            v.EndDate = eDate;
            v.FileName = fileName;
            v.FolderName = CurrentUser.UserLoginName;
            v.ServerFileName = uploadFileName;
            v.GroupId = CurrentUser.UserGroupId;
            v.ItemId = Guid.NewGuid();
            v.ItemSerialNum = Path.GetFileNameWithoutExtension(uploadFileName);
            v.Keyword = this.keyWord.Value;
            v.shotDate = Convert.ToDateTime(this.t_Date.Text);
            v.StartDate = sDate;
            v.uploadDate = DateTime.Now;
            v.userId = CurrentUser.UserId;
            v.updateDate = DateTime.Now;

            //存储数据库记录
            // img.ItemSerialNum = ImageStorageClass.AddImageStorage(img);

            if (!vsc.Add(v))
            { 
                this.ShowMessage(this, "上传失败");
                return;
            }

            vsc.CreateRelationshipVideoAndCatalog(v.ItemId, (Guid[])catalogIds.ToArray(typeof(Guid)));

            
            this.t_Date.Text = "";
            //this.imageType.Value = "";
            this.keyWord.Value = "";
            this.description.Value = "";
            //this.txt_Address.Value = "";
            this.txt_Caption.Value = "";
            //this.txt_Character.Value = "";
            this.Calendar_StartDate.Text = "";
            this.Calendar_EndDate.Text = "";

            initCalendar();

            this.ShowMessage(this, "上传成功");

            //调用一下处理程序，不用等待处理结果
            //VideoController vc = new VideoController();
            //vc.runExe(ConfigurationManager.AppSettings["VideoEngineConsolePath"].ToString());
        }      
    }
}
