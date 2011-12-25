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
using System.IO;

using QJVRMS.Business;
using QJVRMS.Common;
using System.Collections.Generic;
using QJVRMS.Business.ResourceType;

namespace WebUI.Modules.Gift
{
    public partial class Gift_Edit : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    ViewState["Id"] = Request["id"];
                }

                InitData();
            }
        }

        private void InitData()
        {
            GiftBiz biz = new GiftBiz();
            
            DataTable dt = biz.GetGiftTypeList();

            ddlGiftType.DataSource = dt;
            ddlGiftType.DataTextField = "TypeName";
            ddlGiftType.DataValueField = "TypeID";
            ddlGiftType.DataBind();

            if (ViewState["Id"] != null)
            {
                GiftInfo model = biz.GetModel(ViewState["Id"].ToString());
                if (model != null)
                {
                    txtTitle.Text = model.Title;
                    txtRemark.Text = model.Remark;
                    txtQuantity.Text = model.Quantity.ToString();
                    ddlGiftType.SelectedValue = model.TypeId;


                    Resource objResource = new Resource();
                    ResourceEntity model1 = null;
                    ImageType objImg = new ImageType();

                    model1 = objResource.GetResourceInfoByItemId(model.ImageId.ToString());

                    imgGift.ImageUrl = objImg.GetPreviewPathRead(model1.FolderName, model1.ServerFileName, "170");
                    //objImg.ge
                    /*
                    ImageStorage imageModel = ImageStorage.GetImageStorageModel(new Guid(model.ImageId));
                    imgGift.ImageUrl = UIBiz.CommonInfo.GetImageUrl(170, imageModel.FolderName, imageModel.ItemSerialNum, imageModel.ImageType);
                    */
                    
                    this.catalogTree.ImagesItemId = model.ImageId;
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
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

            GiftBiz biz = new GiftBiz();
            GiftInfo model = new GiftInfo();
            if (ViewState["Id"] != null)
            {
                model = biz.GetModel(ViewState["Id"].ToString());
            }
            else
            {
                model.Id = biz.GetNewId();
            }

            model.Title = txtTitle.Text.Trim();
            model.Quantity = int.Parse(txtQuantity.Text.Trim());
            model.Status = 1;
            model.Remark = txtRemark.Text.Trim();
            model.TypeId = ddlGiftType.SelectedValue;
            if (ViewState["Id"] == null)
            {
                //model.ImageId = UploadImage();
                model.ImageId = newUploadImage();
                if (model.ImageId == null)
                {
                    return;
                }
                biz.AddGift(model);
            }
            else
            {
                if (fuImage.FileName != string.Empty)
                {
                    model.ImageId = UploadImage();
                    if (model.ImageId == null)
                    {
                        return;
                    }
                }
                int i = biz.UpdateGift(model);
            }


            Resource objResource = new Resource();
            objResource.CreateRelationshipResourceAndCatalog(new Guid(model.ImageId), (Guid[])catalogIds.ToArray(typeof(Guid)));


            //ImageStorageClass imageClass = new ImageStorageClass();

            //imageClass.CreateRelationshipImageAndCatalog(new Guid(model.ImageId), (Guid[])catalogIds.ToArray(typeof(Guid)));

            this.ShowMessage(this, "保存成功！");
            Response.Redirect("Gift_List.aspx");
        }


        private string newUploadImage()
        {
            //HttpPostedFile objFile = Request.Files["Filedata"];
            HttpPostedFile objFile = this.fuImage.PostedFile;
            if (objFile != null)
            {


                string filename = objFile.FileName;
                string fileType = Path.GetExtension(filename).ToLower();
                if (fileType.IndexOf(".") == -1)
                {
                    fileType = "." + fileType;
                }
                //string resourceType = new Resource().GetResourceTypeByFileExtention(fileType);
                string resourceType = ResourceTypeFactory.getResourceType(fileType.Substring(1)).ResourceType;

                if (resourceType.Equals("image"))
                {                   
                    

                    string strClientFileName = filename;
                    string strServerFileName = SaveImage();

                    if (string.IsNullOrEmpty(strServerFileName))
                    {
                        ShowMessage("发生错误");
                        return string.Empty;
                    }

                    /** start **/
                    Resource objResource = new Resource();
                    ResourceEntity model = new ResourceEntity();

                    //管理员上传直接审核通过
                    if (IsSuperAdmin)
                    {
                        model.Status = (int)ResourceEntity.ResourceStatus.IsPass;
                    }
                    else
                    {
                        model.Status = (int)ResourceEntity.ResourceStatus.NewUpload;
                    }

                    if (this.txtTitle.Text.Trim().Length > 0)
                    {
                        model.Caption = this.txtTitle.Text.Trim();
                    }
                    else
                    {
                        model.Caption = Path.GetFileNameWithoutExtension(strClientFileName);
                    }

                    model.Description = this.txtRemark.Text.Trim();
                    model.EndDate = DateTime.Now.AddYears(10);
                    model.FileName = strClientFileName;
                    model.FolderName = CurrentUser.UserLoginName;

                    model.ServerFileName = strServerFileName;
                   
                    model.GroupId = CurrentUser.UserGroupId;
                    model.ItemId = Guid.NewGuid();
                    model.ItemSerialNum = Path.GetFileNameWithoutExtension(strServerFileName);
                    //model.Keyword = this.keyWord.Value;
                    model.Keyword = this.txtTitle.Text.Trim();
                    //model.shotDate = Convert.ToDateTime(this.shotDate1.Text);
                    model.StartDate = Convert.ToDateTime("1900-01-01"); 
                    model.uploadDate = DateTime.Now;
                    model.userId = CurrentUser.UserId;
                    model.updateDate = DateTime.Now;
                    model.Author = "";

                    //取得文件的扩展名，不包括.号
                    string fileExtName = string.Empty;
                    fileExtName = Path.GetExtension(filename);
                    if (fileExtName.IndexOf(".") > -1)
                    {
                        fileExtName = fileExtName.Substring(1);
                    }

                    model.ResourceType = ResourceTypeFactory.getResourceType(fileExtName).ResourceType;
                    model.FileSize = Resource.GetResourceFileSize(strServerFileName, model.FolderName, fileExtName, "");
                    model.HasCopyright = 0;

                    model.shotDate = DateTime.Now;
                   

                    objResource.Add(model);
                    //objResource.CreateRelationshipResourceAndCatalog(model.ItemId, (Guid[])catalogIds.ToArray(typeof(Guid)));


                    //return strServerFileName;
                    return model.ItemId.ToString(); 
                }
                
            }
            return string.Empty;
        }
        private string SaveImage()
        {
            //HttpPostedFile f = Request.Files["Filedata"];

            HttpPostedFile f = this.fuImage.PostedFile;
            string filename = f.FileName;
            string filetype = Path.GetExtension(filename).ToLower();
            ImageType obj = new ImageType();
            string savePath = obj.GetSourcePath(obj.SourcePaths);
            savePath = Path.Combine(savePath, CurrentUser.UserLoginName);


            //string retSn = new Resource().GetSN(ResourceTypeFactory.getResourceType(filetype).ResourceSNPrefix) +  filetype;   


            string resourceseq = new Resource().GetSN(ResourceTypeFactory.getResourceType(filetype.Substring(1)).ResourceSNPrefix);
            string fileFullPath = Path.Combine(savePath, resourceseq + filetype);

            bool slImage;
            try
            {
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                //保存原图
                f.SaveAs(fileFullPath);

                string SlImageRootPath;
                SlImageRootPath = Path.Combine(obj.PreviewPath_170, CurrentUser.UserLoginName);
                if (!Directory.Exists(SlImageRootPath))
                {
                    Directory.CreateDirectory(SlImageRootPath);
                }

                ArrayList sarray = new ArrayList();
                sarray.Add(fileFullPath);
                ArrayList aarray = new ArrayList();
                aarray.Add(Path.Combine(SlImageRootPath, resourceseq + filetype));
                ImageController.ToZipImage(sarray, aarray, 170);

                SlImageRootPath = Path.Combine(obj.PreviewPath_400, CurrentUser.UserLoginName);
                if (!Directory.Exists(SlImageRootPath))
                {
                    Directory.CreateDirectory(SlImageRootPath);
                }

                sarray = new ArrayList();
                sarray.Add(fileFullPath);
                aarray = new ArrayList();
                aarray.Add(Path.Combine(SlImageRootPath, resourceseq + filetype));
                ImageController.ToZipImage(sarray, aarray, 400);


                //将原图的长x宽保存起来

                System.Drawing.Image m_Image = System.Drawing.Image.FromFile(fileFullPath);

                Int32 height = Convert.ToInt32(m_Image.Height.ToString());
                Int32 width = Convert.ToInt32(m_Image.Width.ToString());
                string hvsp = string.Empty;


                if (height > width)
                {
                    hvsp = "v";
                }
                else if (width > height)
                {
                    hvsp = "h";
                }
                else
                {
                    hvsp = "s";
                }

                Dictionary<string, string> dct = new Dictionary<string, string>();
                dct.Add("Width", width.ToString());
                dct.Add("Height", height.ToString());
                dct.Add("Hvsp", hvsp);
                List<QJVRMS.Business.ResourceWS.DictionaryEntry> lst = new List<QJVRMS.Business.ResourceWS.DictionaryEntry>();

                foreach (string key in dct.Keys)
                {
                    QJVRMS.Business.ResourceWS.DictionaryEntry de = new QJVRMS.Business.ResourceWS.DictionaryEntry();
                    de.Key = key;
                    de.Value = dct[key];
                    lst.Add(de);
                }

                QJVRMS.Business.ResourceWS.DictionaryEntry[] result = lst.ToArray();

                Resource r = new Resource();
                r.insertResourceAttributes(resourceseq, result);

                //Response.Write(resourceseq + filetype + ":" + f.FileName);

                return resourceseq + filetype;

            }
            catch (Exception e1)
            {
                Response.Write("保存图片出现错误" + e1.Message);
                LogWriter.WriteExceptionLog(e1, true);
            }
            finally
            {

            }
            return string.Empty;
        }


























        private string UploadImage()
        {


            ImageStorage img = new ImageStorage();

            //string uploadvalue = this.ImageUpload.Value.Trim();
            string fileName = fuImage.FileName; //获取上传文件的全路径 //uploadvalue.Substring(uploadvalue.Trim().LastIndexOf(@"\") + 1);//得到文件


            #region 组织数据

            img.ImageType = System.IO.Path.GetExtension(fileName).ToLower();//获取扩展名 

            img.FileName = System.IO.Path.GetFileName(fileName);//实际文件名称
            img.Keyword = this.txtTitle.Text.Trim();
            img.Description = this.txtRemark.Text.Trim();
            img.shotDate = DateTime.Now;
            img.StartDate = DateTime.Now;
            img.EndDate = DateTime.Now.AddYears(5);

            img.Caption = this.txtTitle.Text.Trim();
            img.FolderName = CurrentUser.UserLoginName;
            img.userId = CurrentUser.UserId;
            img.ItemId = Guid.NewGuid();
            img.ItemSerialNum = ImageStorageClass.GetImageSeq(DateTime.Now);
            img.GroupId = CurrentUser.UserGroupId;
            img.uploadDate = DateTime.Now;

            img.FileSize = this.fuImage.PostedFile.ContentLength;

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

                    m_Image = System.Drawing.Image.FromStream(this.fuImage.PostedFile.InputStream);

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

            string s = ImageStorageClass.AddImageStorage(img);
            if (s == null)
            {
                this.ShowMessage(this, "上传失败");
            }

            //准能压缩文件
            try
            {

                if (!this.CheckImageType(img.ImageType)
                    || img.ImageType == "ai")
                {
                    uploadFile(img);//存储普通文件
                }
                else
                {
                    saveImage(m_Image, img);//将imageID与catalogID保存到ImageStorage_Catalog表
                }

            }
            catch (Exception ex)
            {
                LogWriter.WriteExceptionLog(ex);
                this.ShowMessage(this, "上传失败");
                return null;
            }
            finally
            {
                if (m_Image != null) m_Image.Dispose();
            }

            return img.ItemId.ToString();
        }

        string ImageRootPath;//WebUI.UIBiz.CommonInfo.ImageRootPath;
        string SlImageRootPath; //WebUI.UIBiz.CommonInfo.SlImageRootPath;
        public string hvsp;

        /// <summary>
        /// 上传其他格式文件方法
        /// </summary>
        public void uploadFile(IImageStorage oImageStorage)
        {
            //上传图片的程序段

            string strBaseLocation = WebUI.UIBiz.CommonInfo.ImageRootPath;

            //这是文件将上传到的服务器的绝对目录
            //if (this.fuImage.ContentLength > 0) //判断选取对话框选取的文件长度是否为0 ImageUpload.PostedFile.ContentLength != 0

            if (this.fuImage.PostedFile.ContentLength > 0)
            {
                if (!Directory.Exists(strBaseLocation + "\\" + CurrentUser.UserLoginName))
                {
                    Directory.CreateDirectory(strBaseLocation + "\\" + CurrentUser.UserLoginName);
                }

                // this.fuImage.MoveTo(strBaseLocation + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + "." + oImageStorage.ImageType, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
                this.fuImage.SaveAs(strBaseLocation + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + oImageStorage.ImageType);

            }
        }

        /// <summary>
        /// 保存原图片与压缩图片 到磁盘
        /// </summary>
        /// <param name="path">上传图片绝对路径</param>
        /// <param name="oImageStorage">上传图片对象</param>
        /// <param name="imagetype">上传图片类型</param>
        /// <returns></returns>
        private void saveImage(System.Drawing.Image image, IImageStorage oImageStorage)
        {
            bool slImage;

            System.Drawing.Image objImg = image;//System.Drawing.Image.FromFile(path);

            ImageRootPath = WebUI.UIBiz.CommonInfo.ImageRootPath;

            //原图存储路径
            string sourcePath = ImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + oImageStorage.ImageType;
            try
            {
                string sourceFolder = Path.Combine(ImageRootPath, CurrentUser.UserLoginName);
                if (!Directory.Exists(sourceFolder))
                {
                    // Directory.CreateDirectory(ImageRootPath + "\\" + CurrentUser.UserLoginName);
                    Directory.CreateDirectory(sourceFolder);
                }

                //保存原图

                this.fuImage.PostedFile.SaveAs(sourcePath);

                SlImageRootPath = WebUI.UIBiz.CommonInfo.SlImageRootPath170;
                if (!Directory.Exists(SlImageRootPath + "\\" + CurrentUser.UserLoginName))
                {
                    Directory.CreateDirectory(SlImageRootPath + "\\" + CurrentUser.UserLoginName);
                }

                //   slImage = ImageController.ConvertImageToScale(sourcePath, 170, SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum  + oImageStorage.ImageType);
                ArrayList sarray = new ArrayList();
                sarray.Add(sourcePath);
                ArrayList aarray = new ArrayList();
                aarray.Add(SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + oImageStorage.ImageType);
                ImageController.ToZipImage(sarray, aarray, 170);

                SlImageRootPath = WebUI.UIBiz.CommonInfo.SlImageRootPath400;
                if (!Directory.Exists(SlImageRootPath + "\\" + CurrentUser.UserLoginName))
                {
                    Directory.CreateDirectory(SlImageRootPath + "\\" + CurrentUser.UserLoginName);
                }

                sarray = new ArrayList();
                sarray.Add(sourcePath);
                aarray = new ArrayList();
                aarray.Add(SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + oImageStorage.ImageType);
                ImageController.ToZipImage(sarray, aarray, 400);
                //   slImage = ImageController.ConvertImageToScale(sourcePath, 400, SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + oImageStorage.ItemSerialNum + oImageStorage.ImageType);

            }
            catch
            {
                Response.Write("保存图片出现错误");
            }
            finally
            {

            }
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
    }
}
