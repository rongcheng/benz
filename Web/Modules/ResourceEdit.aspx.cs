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
using QJVRMS.Common;
using System.IO;
using System.Net;
using System.Runtime;
using System.Text;
using QJVRMS.Business.SecurityControl;
using WebUI.UIBiz;
using QJVRMS.Business.ResourceType;


namespace WebUI.Modules {
    public partial class ResourceEdit : AuthPage {
        string yRootPath = "";
        string SlImageRootPath;
        StringBuilder OutString = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e) {
            if (!this.IsPostBack && !string.IsNullOrEmpty(Request["itemId"])) {
                SearchItemSerialNum(Request["itemId"]);
                LoadAttach();


                bindKeywordCat();
                bindRptKeywordDetail();
            }
        }

        protected override void OnInit(EventArgs e) {
            this.IsInControl = false;
            base.OnInit(e);
        }

        //���ļ���Ų�ѯ,��ȡ��Դ��ϸ��Ϣ
        public void SearchItemSerialNum(string ItemID) {
            Resource objResource = new Resource();
            ResourceEntity model = null;

            try {
                //oIImageStorage = ImageStorageClass.GetImageInfoByNum(imgItemSerialNum);
                //oIImageStorage = ImageStorageClass.GetImageInfoByItemId(new Guid(ItemID), CurrentUser.UserId);

                model = objResource.GetResourceInfoByItemId(ItemID);
                if (ViewState["model"] != null) {
                    ViewState["model"] = model;
                }

                if (model != null) {
                    //this.lb_ImageType.Text = "";
                    this.lb_ItemSerialNum.Text = model.ItemSerialNum;
                    this.TxtKeyword.Text = model.Keyword;
                    this.lb_uploadDate.Text = model.uploadDate.ToString("yyyy-MM-dd");
                    this.txt_Caption.Text = model.Caption;
                    //this.txt_Address.Text = oIImageStorage.Address;
                    //this.txt_Character.Text = oIImageStorage.Character;
                    this.hiFolder.Value = model.FolderName;
                    this.Hidden_ItemId.Value = ItemID;
                    this.Hidden_FolderName.Value = model.FolderName;
                    this.hiNum.Value = model.ItemSerialNum;
                    this.TxtDescription.Text = model.Description;
                    this.lb_FileName.Text = model.FileName;

                    this.Calendar_ShotDate.Text = model.shotDate.ToString("yyyy-MM-dd");
                    this.Calendar_StartDate.Text = model.StartDate.ToString("yyyy-MM-dd");
                    this.Calendar_EndDate.Text = model.EndDate.ToString("yyyy-MM-dd");
                    
                    this.resourceImage.Text = showImage(model.ServerFileName, model.FolderName, model.ResourceType, model.Status, ItemID);

                    
                    this.resourceType.Value = model.ResourceType;
                    this.serverFolder.Value = model.FolderName;

                    using (DataSet ds = GetResourceCatalog(ItemID)) {
                        if (ds != null && ds.Tables[0].Rows.Count != 0) {
                            DataTable cataTable = ds.Tables[0];

                            for (int i = 0; i < cataTable.Rows.Count; i++) {
                                OutString.Append(cataTable.Rows[i]["CatalogName"].ToString() + "   ");
                            }

                            lb_catalogs.Text = OutString.ToString();

                        }
                    }
                }
                #region
                //this.yRootPath = UIBiz.CommonInfo.GetImageUrl(400, oIImageStorage);

                //string imgSuffixStr = ",JPG,JPEG,GIF,BMP,TIFF,PCX,TGA,EXIF,FPX,";
                //if (imgSuffixStr.IndexOf("," + oIImageStorage.ImageType.ToUpper() + ",") < 0)
                //{
                //    this.tr_Hvsp.Visible = false;
                //    this.tr_shotDate.Visible = false;
                //}

                //this.imgsrc.Src = yRootPath;

                //switch (oIImageStorage.Hvsp.ToUpper())
                //{

                //    case "H": this.lb_Hvsp.Text = "��ͼ"; break;
                //    case "V": this.lb_Hvsp.Text = "��ͼ"; break;
                //    case "S": this.lb_Hvsp.Text = "��ͼ"; break;
                //    case "P": this.lb_Hvsp.Text = "ȫ��ͼ"; break;
                //    default: this.lb_Hvsp.Text = "��ͼ"; break;
                //}

                //if (oIImageStorage.StartDate.ToString("yyyy-MM-dd") != "1900-01-01")
                //{
                //    this.Calendar_StartDate.Text = oIImageStorage.StartDate.ToString("yyyy-MM-dd");
                //}
                //else
                //{
                //    this.Calendar_StartDate.Text = "";
                //}
                //if (oIImageStorage.EndDate.ToString("yyyy-MM-dd") != "1900-01-01")
                //{
                //    this.Calendar_EndDate.Text = oIImageStorage.EndDate.ToString("yyyy-MM-dd");
                //}
                //else
                //{
                //    this.Calendar_EndDate.Text = "";
                //}
                //if (oIImageStorage.shotDate.ToString("yyyy-MM-dd") != "1900-01-01")
                //{
                //    this.Calendar_ShotDate.Text = oIImageStorage.shotDate.ToString("yyyy-MM-dd");
                //}
                //else
                //{
                //    this.Calendar_ShotDate.Text = "";
                //}
                #endregion
            }
            catch (Exception e2) {
                this.imagePanel.Visible = false;
                Response.Write("<script language='javascript'>alert('�����ڴ���Դ!');window.close();</script>");
                Response.End();
            }
        }

        protected string showImage(string ServerFileName, string ServerFolderName, string ResourceType, int status, string ItemId) {
            StringBuilder sb = new StringBuilder("");
            string imageUrl = string.Empty;
            /*string image = @"<a href='../PicDetail.aspx?ItemID={0}' target='_blank'>
                             <img id='Img1' alt='' onmousemove='showPic(this.src,event);' onmouseout='hiddenPic();' src='{1}'/></a>";
             */
            string image = @"<img id='Img1' alt='' src='/CreateImage.aspx?f={0}&s={1}'/>";
            string videoText = @"<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0' width='400' height='300' id='QuanJingFilm' align='middle'>
                        <param name='allowScriptAccess' value='always' />
                        <param name='allowFullScreen' value='false' />
                        <param name='movie' value='../../UI/flash/QJFilm.swf' />

                        <param name='quality' value='autohigh' />
                        <param name='bgcolor' value='#ffffff' />
                        <param name='wmode' value='opaque' />
                        <param name='FlashVars' value='videoUrl={0}&imgUrl={1}&SerailNumber={2}' />
                        <embed src='../../UI/flash/qjFilm.swf' quality='autohigh' bgcolor='#ffffff' width='400' height='300' wmode='opaque' flashvars='videoUrl={0}&imgUrl={1}&SerailNumber={2}'
                            name='QuanJingFilm' align='middle' allowscriptaccess='always' allowfullscreen='false' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' />
                    </object>";
            if (ResourceType.Equals("image")) {
                //imageUrl = UIBiz.CommonInfo.GetResourceImageUrl(400, ServerFileName, ServerFolderName);
                ImageType objImg = new ImageType();
                //yangguang
                //imageUrl = objImg.PreviewPath_400_Read + "/" + ServerFolderName + "/" + ServerFileName;
                imageUrl = objImg.GetPreviewPath(ServerFolderName, ServerFileName, "400");
                //sb.Append(string.Format(image, ItemId, imageUrl));
                sb.Append(string.Format(image, ServerFolderName, ServerFileName));
            }
            else if (ResourceType.Equals("video")) {
                imageUrl = GetVideoImgUrl(ServerFolderName, Path.GetFileNameWithoutExtension(ServerFileName), status);

                string videoUrl = string.Empty;
                videoUrl = GetSmallFlvUrl(ServerFolderName, Path.GetFileNameWithoutExtension(ServerFileName), status);
                sb.Append(string.Format(videoText, videoUrl, imageUrl, ItemId));
            }

            return sb.ToString();
        }
        //ͼƬ·��
        protected string GetVideoImgUrl(string FolderName, string ItemSerialNum, int status) {
            string _ret = string.Empty;
            if (status == 2) {
                _ret = "/images/videoconverterror.gif";
            }
            else if (status == 0) {
                _ret = "/images/videoconverting.gif";
            }
            else {

                VideoType objV = new VideoType();
                string videoPreviewPath = objV.PreviewPathRead;
                if (!string.IsNullOrEmpty(videoPreviewPath)) {
                    _ret = videoPreviewPath + "/image/" + FolderName + "/" + ItemSerialNum + ".jpg";
                }
            }
            //return UIBiz.CommonInfo.GetImageUrl(170, FolderName, ItemSerialNum, ImageType);

            return _ret;
        }

        //flv·��
        protected string GetFlvUrl(string FolderName, string ItemSerialNum, int status) {
            string _ret = string.Empty;
            if (status == 2) {
                //_ret = "/images/videoconverterror.gif";
            }
            else if (status == 0) {
                //_ret = "/images/videoconverting.gif";
            }
            else {
                string videoPreviewPath = ConfigurationManager.AppSettings["videoPreviewPathRead"];
                if (!string.IsNullOrEmpty(videoPreviewPath)) {
                    _ret = videoPreviewPath + "flv/" + FolderName + "/" + ItemSerialNum + ".flv";
                }

            }
            //return UIBiz.CommonInfo.GetImageUrl(170, FolderName, ItemSerialNum, ImageType);

            return Server.UrlEncode(_ret);
        }

        //flv·��
        protected string GetSmallFlvUrl(string FolderName, string ItemSerialNum, int status) {
            string _ret = string.Empty;
            if (status == 2) {
                //_ret = "/images/videoconverterror.gif";
            }
            else if (status == 0) {
                //_ret = "/images/videoconverting.gif";
            }
            else {
                string videoPreviewPath = ConfigurationManager.AppSettings["videoPreviewPathRead"];
                if (!string.IsNullOrEmpty(videoPreviewPath)) {
                    _ret = videoPreviewPath + "smallFlv/" + FolderName + "/" + ItemSerialNum + ".flv";
                }

            }
            //return UIBiz.CommonInfo.GetImageUrl(170, FolderName, ItemSerialNum, ImageType);

            return Server.UrlEncode(_ret);
        }

        //�޸��ļ���Ϣ
        protected void BtnUpdate_Click(object sender, EventArgs e) {
            //��֤����
            DateTime sDate = new DateTime();
            DateTime eDate = new DateTime();
            DateTime shotDate = new DateTime();
            if (this.Calendar_ShotDate.Text == "") {
                this.Label2.Text = "����ʱ�䲻��Ϊ�գ�";
                return;
            }
            else {
                shotDate = Convert.ToDateTime(this.Calendar_ShotDate.Text);
                if (shotDate > Convert.ToDateTime(this.lb_uploadDate.Text)) {
                    this.Label2.Text = "����ʱ��Ӧ��ͼƬ�ϴ������磡";
                    return;
                }
            }
            if (this.Calendar_StartDate.Text != "") {
                sDate = Convert.ToDateTime(this.Calendar_StartDate.Text);
            }
            else {
                sDate = Convert.ToDateTime("1900-01-01");
            }
            if (this.Calendar_EndDate.Text != "") {
                eDate = Convert.ToDateTime(this.Calendar_EndDate.Text);

                if (sDate != Convert.ToDateTime("1900-01-01") && eDate < sDate) {
                    this.Label2.Text = "��Ч��������Ӧ����Ч��ʼ������";
                    return;
                }
            }
            else {
                eDate = Convert.ToDateTime("1900-01-01");
            }

            ResourceEntity re = null;
            Resource r = new Resource();
            if (ViewState["model"] != null) {
                re = ViewState["model"] as ResourceEntity;
            }
            else {
                re = r.GetResourceInfoByItemId(this.Hidden_ItemId.Value);
            }

            re.Caption = this.txt_Caption.Text;
            re.Description = this.TxtDescription.Text;
            re.Keyword = this.TxtKeyword.Text;
            re.shotDate = shotDate;
            re.StartDate = sDate;
            re.EndDate = eDate;

            string editResult = "";
            try {
                r.Update(re);

                ShowMessage("�޸��ļ���Ϣ�ɹ�!");
                editResult = "�ɹ�";
            }
            catch (Exception e1) {
                ShowMessage("�޸��ļ���Ϣʧ��!" + e1.Message);
                editResult = "ʧ��";
            }


            LogEntity model = new LogEntity();
            model.id = Guid.NewGuid();
            model.userId = CurrentUser.UserId;
            model.userName = CurrentUser.UserLoginName;
            model.EventType = ((int)LogType.EditResource).ToString();
            model.EventResult = editResult;
            model.EventContent = "ͼƬ��ţ�" + re.ItemSerialNum;
            model.IP = HttpContext.Current.Request.UserHostAddress;
            model.AddDate = DateTime.Now;
            new Logs().Add(model);



        }

        //�޸�ͼƬ�������ϴ�ͼƬ
        protected void BtnPic_Click(object sender, EventArgs e) {

        }
        //ɾ���ļ�
        protected void btnDel_Click(object sender, EventArgs e) {

            ResourceEntity re = null;
            Resource r = new Resource();
            if (ViewState["model"] != null) {
                re = ViewState["model"] as ResourceEntity;
            }
            else {
                re = r.GetResourceInfoByItemId(this.Hidden_ItemId.Value);
            }

            string ItemSerialNum = "";
            string ImageType = "";
            string str = "";//�ж�170ͼƬ����400ͼƬ��û�б�ɾ��
            ItemSerialNum = lb_ItemSerialNum.Text;
            //ImageType = lb_ImageType.Text;
            //bool isValidate = QJVRMS.Business.ImageStorageClass.DeleteImageStorage(new Guid(this.Hidden_ItemId.Value));

            bool isValidate = Resource.DeleteResource(re.ItemId);
            string attachmentFolder = string.Empty;

            string sourceFolder = string.Empty;
            string attachmentsFolder = string.Empty;
            if (re.ResourceType.ToLower().Equals("image")) {
                string _170Folder;
                string _400Folder;

                ImageType obj = new ImageType();
                //yangguang
                //sourceFolder = obj.SourcePath;
                //attachmentsFolder = obj.SourcePath;
                //_170Folder = obj.PreviewPath_170;
                //_400Folder = obj.PreviewPath_400;

                //sourceFolder = Path.Combine(sourceFolder, re.FolderName);
                //_170Folder = Path.Combine(_170Folder, re.FolderName);
                //_400Folder = Path.Combine(_400Folder, re.FolderName);

                //try {
                //    File.Delete(Path.Combine(sourceFolder, re.ServerFileName));
                //    File.Delete(Path.Combine(_170Folder, re.ServerFileName));
                //    File.Delete(Path.Combine(_400Folder, re.ServerFileName));
                //}
                //catch { }
                try {
                    File.Delete(obj.GetSourcePath(re.FolderName, re.ServerFileName));
                    attachmentsFolder = obj.SourcePaths[obj.PathNumber].Trim();
                    File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "170"));
                    File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "400"));
                }
                catch { }
            }
            else if (re.ResourceType.ToLower().Equals("video")) {
                string _previewPolder = CommonInfo.VideoPreviewPath;

                VideoType obj = new VideoType();
                //sourceFolder = obj.SourcePath;
                //attachmentsFolder = obj.SourcePath;
                //_previewPolder = obj.PreviewPath;

                //try {
                //    File.Delete(Path.Combine(sourceFolder, re.ServerFileName));
                //    File.Delete(Path.Combine(Path.Combine(Path.Combine(_previewPolder, "flv"), re.FolderName), re.ServerFileName));
                //    File.Delete(Path.Combine(Path.Combine(Path.Combine(_previewPolder, "image"), re.FolderName), re.ServerFileName));
                //    File.Delete(Path.Combine(Path.Combine(Path.Combine(_previewPolder, "smallflv"), re.FolderName), re.ServerFileName));
                //}
                //catch {

                //}
                try {
                    File.Delete(obj.GetSourcePath(string.Empty, re.ServerFileName));
                    attachmentsFolder = obj.SourcePaths[obj.PathNumber].Trim();
                    File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "flv"));
                    File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "image"));
                    File.Delete(obj.GetPreviewPath(re.FolderName, re.ServerFileName, "smallflv"));
                }
                catch {

                }
            }

            //sourceFolder = Path.Combine(sourceFolder, CurrentUser.UserLoginName);
            //sourceFolder = Path.Combine(sourceFolder, WebUI.UIBiz.CommonInfo.AttachFolder);

            #region ɾ�������ļ� by ciqq 2010-4-2
            //SlImageRootPath = WebUI.UIBiz.CommonInfo.SlImageRootPath170;
            //if (Directory.Exists(SlImageRootPath + "\\" + CurrentUser.UserLoginName))
            //{
            //    File.Delete(SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + ItemSerialNum + ImageType);
            //    str = "170Ŀ¼�µ�ͼƬ�ѱ�ɾ��!";
            //}
            //SlImageRootPath = WebUI.UIBiz.CommonInfo.SlImageRootPath400;

            //if (Directory.Exists(SlImageRootPath + "\\" + CurrentUser.UserLoginName))
            //{
            //    File.Delete(SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + ItemSerialNum + ImageType);
            //    str += "400Ŀ¼�µ�ͼƬ�ѱ�ɾ��!";
            //}
            //SlImageRootPath = WebUI.UIBiz.CommonInfo.ImageRootPath;
            //if (Directory.Exists(SlImageRootPath + "\\" + CurrentUser.UserLoginName))
            //{
            //    File.Delete(SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + ItemSerialNum + ImageType);
            //    str += "SourceĿ¼�µ�ͼƬ�ѱ�ɾ��!";
            //}

            //ɾ�����еĸ���
            //string sourceFolder = Path.Combine(WebUI.UIBiz.CommonInfo.ImageRootPath, this.hiFolder.Value);
            string fileName = "";
            attachmentsFolder = Path.Combine(attachmentsFolder, re.FolderName);
            attachmentsFolder = Path.Combine(attachmentsFolder, UIBiz.CommonInfo.AttachFolder);
            for (int i = 0; i < this.attList.Rows.Count; i++) {
                fileName = this.attList.DataKeys[i].Values[1].ToString();
                fileName = Path.Combine(attachmentsFolder, fileName);
                try {
                    File.Delete(fileName);
                }
                catch(Exception ex) {
                    LogWriter.WriteExceptionLog(ex);
                }
            }
                //foreach (GridViewRow gvr in this.attList.Rows) {
                //    fileName = Path.Combine(attachmentFolder, gvr.Cells[0].Text);

                //    try {
                //        File.Delete(fileName);
                //    }
                //    catch (Exception ex) {
                //        LogWriter.WriteExceptionLog(ex);
                //    }
                //}
            #endregion


            LogEntity model = new LogEntity();
            model.id = Guid.NewGuid();
            model.userId = CurrentUser.UserId;
            model.userName = CurrentUser.UserLoginName;
            model.EventType = ((int)LogType.DeleteResource).ToString();
            model.EventResult = "�ɹ�";
            model.EventContent = "ͼƬ��ţ�" + re.ItemSerialNum;
            model.IP = HttpContext.Current.Request.UserHostAddress;
            model.AddDate = DateTime.Now;
            new Logs().Add(model);


                Response.Write("<script language='javascript'>window.close();</script>");
            Response.End();
        }

        private DataSet GetResourceCatalog(string itemid) {
            return new Resource().GetResourceCatalogByItemId(itemid);
        }

        private void GetCatalog(DataTable CTable, DataRow[] Nodes) {
            for (int i = 0; i < Nodes.Length; i++) {
                if (Nodes[i].ItemArray[2].ToString() == "")
                    OutString.Append(Nodes[i].ItemArray[1].ToString() + "<br /><br />");
                else
                    OutString.Append(Nodes[i].ItemArray[1].ToString() + "   <br/>");

                DataRow[] firstNodesChild = CTable.Select("parentid='" + Nodes[i].ItemArray[0].ToString() + "'", "CatalogOrder");
                GetCatalog(CTable, firstNodesChild);
            }
        }

        /// <summary>
        /// �ϴ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpAttach_Click(object sender, EventArgs e) {
            string sourceFolder = Path.Combine(WebUI.UIBiz.CommonInfo.ImageRootPath, this.hiFolder.Value);
            sourceFolder = Path.Combine(sourceFolder, UIBiz.CommonInfo.AttachFolder);

            if (!Directory.Exists(sourceFolder)) {
                Directory.CreateDirectory(sourceFolder);
            }

            string fileType = Path.GetExtension(this.attachFile.FileName);
            string fileName = Path.GetFileName(this.attachFile.FileName);
            //string NewfileName = fileName + fileType;

            string fileFullPath = Path.Combine(sourceFolder, fileName);

            if (fileName.Length > 255) {
                ShowMessage("�������ƹ���,��С��255���ַ�!");
                return;
            }
            if (File.Exists(fileFullPath)) {
                ShowMessage("���������ظ�����ɾ��ԭ�и��������ϴ�!");
                return;
            }

            try {
                this.attachFile.SaveAs(fileFullPath);

                if (ImageStorageClass.AddAttach(new Guid(this.Hidden_ItemId.Value), fileName)) {
                    ShowMessage("��Ӹ����ɹ�!");
                    LoadAttach();
                }
                else {
                    ShowMessage("��Ӹ���ʧ��!");
                }
            }
            catch (PathTooLongException pe) {
                ShowMessage("�������ƹ���!");
            }
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        protected void LoadAttach() {
            DataTable dt = Resource.GetAttachList(new Guid(this.Hidden_ItemId.Value));

            dt.Columns.Add("fileNamefileLength");

            foreach (DataRow dr in dt.Rows) {
                dr["fileNamefileLength"] = dr["filename"].ToString() + " ( " + Tool.toFileSize(Convert.ToInt64(dr["fileLength"].ToString())) + " ) ";

            }

            this.attList.DataSource = dt;
            this.attList.DataBind();
        }

        protected void attList_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            ResourceEntity re = null;
            Resource r = new Resource();
            if (ViewState["model"] != null) {
                re = ViewState["model"] as ResourceEntity;
            }
            else {
                re = r.GetResourceInfoByItemId(this.Hidden_ItemId.Value);
            }

            string sourceFolder = string.Empty;
            ImageType imageType = new ImageType();
            if (re.ResourceType.ToLower().Equals("image")) {
                sourceFolder = imageType.SourcePath;//CommonInfo.ImageRootPath;
            }
            else if (re.ResourceType.ToLower().Equals("video")) {
                sourceFolder = imageType.VideoPath;//CommonInfo.VideoRootPath;

            }

            sourceFolder = Path.Combine(sourceFolder, re.FolderName);
            sourceFolder = Path.Combine(sourceFolder, WebUI.UIBiz.CommonInfo.AttachFolder);

            //sourceFolder = Path.Combine(WsourceFolder, this.hiFolder.Value);
            //sourceFolder = Path.Combine(sourceFolder, UIBiz.CommonInfo.AttachFolder);

            string fileName = this.attList.DataKeys[e.RowIndex].Values[1].ToString();//this.attList.Rows[e.RowIndex].Cells[0].Text;
            fileName = Path.Combine(sourceFolder, fileName);

            try {
                File.Delete(fileName);
                string key = this.attList.DataKeys[e.RowIndex].Values[0].ToString();
                Resource.DeleteAttach(new Guid(key));
                LoadAttach();
            }
            catch (Exception ex) {
                LogWriter.WriteExceptionLog(ex);
            }
        }

        protected void attList_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName.ToLower().Equals("downloadfile")) {


                string endUser = string.Empty;
                string usage = string.Empty;
                string attType = "attachment";
                string filetype = string.Empty;
                string downFileName = string.Empty;
                string resourceType = this.resourceType.Value;
                string serverFolder = this.serverFolder.Value;
                if (attType == "0") {
                    downFileName = Request["FileName"];
                    filetype = Request["FileType"];
                }
                else {

                    downFileName = System.IO.Path.GetFileNameWithoutExtension(e.CommandArgument.ToString());
                    filetype = System.IO.Path.GetExtension(e.CommandArgument.ToString());
                }

                Response.Redirect("../downhigh.aspx?filename=" + downFileName + "&filetype=" + filetype + "&usage=" + usage + "&EndUser=" + endUser + "&attType=" + attType + "&folder=" + serverFolder + "&resourceType=" + resourceType);
            }
        }



        private void bindKeywordCat()
        {
            KeyWords obj = new KeyWords();
            DataSet ds = obj.GetKeywordsByParentid(0);
            this.rptKeywordCat.DataSource = ds.Tables[0].DefaultView;
            this.rptKeywordCat.DataBind();
        }

        private void bindRptKeywordDetail()
        {
            KeyWords obj = new KeyWords();
            DataSet ds = obj.GetKeywordsByParentid(0);
            this.rptKeywordDetail1.DataSource = ds.Tables[0].DefaultView;
            this.rptKeywordDetail1.DataBind();

        }
        public void rptKeywordDetail1_ItemDataBoud(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            KeyWords obj = new KeyWords();
            DataSet ds = obj.GetKeywordsByParentid(0);

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptKeywordDetail = (Repeater)e.Item.FindControl("rptKeywordDetail");
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                int CategorieId = Convert.ToInt32(rowv["ID"]);
                //���ݷ���ID��ѯ�÷����µĲ�Ʒ�����󶨲�ƷRepeater
                rptKeywordDetail.DataSource = obj.GetKeywordsByParentid(CategorieId);
                rptKeywordDetail.DataBind();
            }


        }




    }
}
