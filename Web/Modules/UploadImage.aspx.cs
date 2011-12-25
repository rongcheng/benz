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
using System.Drawing;
using QJVRMS.Business;
using QJVRMS.Common;
using System.IO;
using System.Net;
using System.Runtime;
using System.Text;

namespace WebUI.Modules
{
    public partial class UploadImage : AuthPage //, IHttpModule //继承IHttpModule接口实现大文件上传功能
    {
        string ImageRootPath;//WebUI.UIBiz.CommonInfo.ImageRootPath;
        string SlImageRootPath; //WebUI.UIBiz.CommonInfo.SlImageRootPath;
        public string hvsp;
        //  static string imgSuffixStr = "jpg,png,psd,ai,jpeg,gif,bmp,tiff,pcx,tga,exif,fpx";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                initCalendar();
                

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

        /// <summary>
        /// 保存原图片与压缩图片 到磁盘
        /// </summary>
        /// <param name="path">上传图片绝对路径</param>
        /// <param name="oImageStorage">上传图片对象</param>
        /// <param name="imagetype">上传图片类型</param>
        /// <returns></returns>
        //private void saveImage(System.Drawing.Image image, IImageStorage oImageStorage)
        //{
        //    bool slImage;

        //    System.Drawing.Image objImg = image;//System.Drawing.Image.FromFile(path);

        //    ImageRootPath = WebUI.UIBiz.CommonInfo.ImageRootPath;

        //    //原图存储路径
        //    string sourcePath = ImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + oImageStorage.ImageType;
        //    try
        //    {
        //        string sourceFolder = Path.Combine(ImageRootPath, CurrentUser.UserLoginName);
        //        if (!Directory.Exists(sourceFolder))
        //        {
        //            // Directory.CreateDirectory(ImageRootPath + "\\" + CurrentUser.UserLoginName);
        //            Directory.CreateDirectory(sourceFolder);
        //        }

        //        //保存原图

        //        this.AttachFile.PostedFile.SaveAs(sourcePath);

        //        SlImageRootPath = WebUI.UIBiz.CommonInfo.SlImageRootPath170;
        //        if (!Directory.Exists(SlImageRootPath + "\\" + CurrentUser.UserLoginName))
        //        {
        //            Directory.CreateDirectory(SlImageRootPath + "\\" + CurrentUser.UserLoginName);
        //        }

        //        //   slImage = ImageController.ConvertImageToScale(sourcePath, 170, SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum  + oImageStorage.ImageType);
        //        ArrayList sarray = new ArrayList();
        //        sarray.Add(sourcePath);
        //        ArrayList aarray = new ArrayList();
        //        aarray.Add(SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + oImageStorage.ImageType);
        //        ImageController.ToZipImage(sarray, aarray, 170);

        //        SlImageRootPath = WebUI.UIBiz.CommonInfo.SlImageRootPath400;
        //        if (!Directory.Exists(SlImageRootPath + "\\" + CurrentUser.UserLoginName))
        //        {
        //            Directory.CreateDirectory(SlImageRootPath + "\\" + CurrentUser.UserLoginName);
        //        }

        //        sarray = new ArrayList();
        //        sarray.Add(sourcePath);
        //        aarray = new ArrayList();
        //        aarray.Add(SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + oImageStorage.ImageType);
        //        ImageController.ToZipImage(sarray, aarray, 400);
        //        //   slImage = ImageController.ConvertImageToScale(sourcePath, 400, SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + oImageStorage.ImageType);

        //    }
        //    catch
        //    {
        //        Response.Write("保存图片出现错误");
        //    }
        //    finally
        //    {

        //    }

        //}


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

            //string uploadvalue = this.ImageUpload.Value.Trim();
            //string fileName = this.AttachFile.FileName; //获取上传文件的全路径 //uploadvalue.Substring(uploadvalue.Trim().LastIndexOf(@"\") + 1);//得到文件
            string fileName="";
            if(!string.IsNullOrEmpty(Request["selectedFile"]))
            {
                fileName=Request["selectedFile"].ToString();
            }
            string uploadFileName = "";
            if (!string.IsNullOrEmpty(Request["uploadFileName"]))
            {
                uploadFileName=Request["uploadFileName"].ToString();
            }
            //Response.Write("selectedFile:" + fileName);
            //Response.Write("uploadFileName:" + uploadFileName);
            //Response.End();
            #region 组织数据

            img.ImageType = System.IO.Path.GetExtension(fileName).ToLower();//获取扩展名 

            img.FileName = System.IO.Path.GetFileName(fileName);//实际文件名称
            img.Keyword = this.keyWord.Value;
            img.Description = this.imageDes.Value;
            img.shotDate = Convert.ToDateTime(this.t_Date.Text);
            img.StartDate = sDate;
            img.EndDate = eDate;

            img.Caption = this.txt_Caption.Value;
            img.Address = this.txt_Address.Value;
            img.Character = this.txt_Character.Value;
            img.FolderName = CurrentUser.UserLoginName;
            img.userId = CurrentUser.UserId;
            img.ItemId = Guid.NewGuid();
            //img.ItemSerialNum = ImageStorageClass.GetImageSeq(DateTime.Now);
            img.ItemSerialNum = Path.GetFileNameWithoutExtension(uploadFileName);
            img.GroupId = CurrentUser.UserGroupId;
            #endregion
            //modify by dtf 08-06-16 (png,psd,ai)

            System.Drawing.Image m_Image = null;

            if (this.CheckImageType(img.ImageType))// is image format
            {
                if (img.ImageType == "ai")
                {
                    img.Hvsp = "s";
                }
                else
                {

                    // m_Image = System.Drawing.Image.FromFile(fileName);
                    ImageRootPath = WebUI.UIBiz.CommonInfo.ImageRootPath;
                    string sourcePath = ImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + uploadFileName;
                    //m_Image = System.Drawing.Image.FromStream(this.AttachFile.PostedFile.InputStream);

                    m_Image = System.Drawing.Image.FromFile(sourcePath);

                    Int32 height = Convert.ToInt32(m_Image.Height.ToString());
                    Int32 width = Convert.ToInt32(m_Image.Width.ToString());

                    if (height > width)
                    {
                        img.Hvsp = "v";
                    }
                    else if (width > height)
                    {
                        img.Hvsp = "h";
                    }
                    else
                    {
                        img.Hvsp = "s";
                    }
                }
            }
            else
            {
                img.Hvsp = string.Empty;
            }

            //存储数据库记录
            // img.ItemSerialNum = ImageStorageClass.AddImageStorage(img);


            if (ImageStorageClass.AddImageStorage(img) == null)
            {
                this.ShowMessage(this, "上传失败");
                return;
            }

            //准能压缩文件
            try
            {

                if (!this.CheckImageType(img.ImageType)
                    || img.ImageType == "ai")
                {
                    //uploadFile(img);//存储普通文件
                }
                else
                {
                    //saveImage(m_Image, img);//将imageID与catalogID保存到ImageStorage_Catalog表
                }

            
               
                ImageStorageClass imageClass = new ImageStorageClass();

               

                imageClass.CreateRelationshipImageAndCatalog(img.ItemId, (Guid[])catalogIds.ToArray(typeof(Guid)));

                this.t_Date.Text = "";
                this.imageType.Value = "";
                this.keyWord.Value = "";
                this.imageDes.Value = "";
                this.txt_Address.Value = "";
                this.txt_Caption.Value = "";
                this.txt_Character.Value = "";
                this.Calendar_StartDate.Text = "";
                this.Calendar_EndDate.Text = "";

                initCalendar();

                this.ShowMessage(this, "上传成功");

            }
            catch (Exception ex)
            {
                LogWriter.WriteExceptionLog(ex);
                this.ShowMessage(this, "上传失败");

            }
            finally
            {
                if (m_Image != null) m_Image.Dispose();
            }


        }


        /// <summary>
        /// 上传其他格式文件方法
        /// </summary>
        //public void uploadFile(IImageStorage oImageStorage)
        //{
        //    //上传图片的程序段

        //    string strBaseLocation = WebUI.UIBiz.CommonInfo.ImageRootPath;

        //    //这是文件将上传到的服务器的绝对目录
        //    //if (this.AttachFile.ContentLength > 0) //判断选取对话框选取的文件长度是否为0 ImageUpload.PostedFile.ContentLength != 0

        //    if (this.AttachFile.PostedFile.ContentLength > 0)
        //    {
        //        if (!Directory.Exists(strBaseLocation + "\\" + CurrentUser.UserLoginName))
        //        {
        //            Directory.CreateDirectory(strBaseLocation + "\\" + CurrentUser.UserLoginName);
        //        }

        //        // this.AttachFile.MoveTo(strBaseLocation + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + "." + oImageStorage.ImageType, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
        //        this.AttachFile.SaveAs(strBaseLocation + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + oImageStorage.ImageType);

        //    }
        //}

    }
}
