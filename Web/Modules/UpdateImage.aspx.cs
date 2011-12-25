using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

using QJVRMS.Business;
using QJVRMS.Common;
using System.IO;
using System.Net;
using System.Runtime;
using System.Text;
using QJVRMS.Business.SecurityControl;


namespace WebUI.Modules
{
    public partial class UpdateImage : AuthPage
    {

        string yRootPath = "";// WebUI.UIBiz.CommonInfo.SlImageRootPath170;
        string SlImageRootPath; //WebUI.UIBiz.CommonInfo.SlImageRootPath;
        StringBuilder OutString = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack
                && !string.IsNullOrEmpty(Request["itemId"]))
            {
                //List<ObjectRule> rules = new List<ObjectRule>(1);
                //ISecurityObject securityObj = new SecurityObject(new Guid(Request["cataId"]), SecurityObjectType.Items);
                //ObjectRule or = new ObjectRule(securityObj, new User(CurrentUser.UserId), OperatorMethod.Write);
                //rules.Add(or);
                //ObjectRule.CheckRules(rules);
                //if (!rules[0].IsValidate)
                //{
                //    Response.Write("<script language='javascript'>alert('您没有权限修改此图片!');window.close();</script>");
                //    Response.End();
                //}

                SearchItemSerialNum(Request["itemId"]);
                LoadAttach();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = false;
            base.OnInit(e);
        }



        //按文件编号查询,获取图片详细信息
        public void SearchItemSerialNum(string imgItemSerialNum)
        {
            IImageStorage oIImageStorage = null;


            try
            {
                //oIImageStorage = ImageStorageClass.GetImageInfoByNum(imgItemSerialNum);
                oIImageStorage = ImageStorageClass.GetImageInfoByItemId(new Guid(imgItemSerialNum), CurrentUser.UserId);



                this.yRootPath = UIBiz.CommonInfo.GetImageUrl(400, oIImageStorage);

                string imgSuffixStr = ",JPG,JPEG,GIF,BMP,TIFF,PCX,TGA,EXIF,FPX,";
                if (imgSuffixStr.IndexOf("," + oIImageStorage.ImageType.ToUpper() + ",") < 0)
                {
                    this.tr_Hvsp.Visible = false;
                    this.tr_shotDate.Visible = false;
                }

                this.imgsrc.Src = yRootPath;
                this.Hidden_ImgItemId.Value = oIImageStorage.ItemId.ToString();
                hiImgNum.Value = oIImageStorage.ItemSerialNum;
                this.TxtDescription.Text = oIImageStorage.Description;
                this.lb_FileName.Text = oIImageStorage.FileName;
                switch (oIImageStorage.Hvsp.ToUpper())
                {

                    case "H": this.lb_Hvsp.Text = "横图"; break;
                    case "V": this.lb_Hvsp.Text = "竖图"; break;
                    case "S": this.lb_Hvsp.Text = "方图"; break;
                    case "P": this.lb_Hvsp.Text = "全景图"; break;
                    default: this.lb_Hvsp.Text = "横图"; break;
                }

                this.lb_ImageType.Text = oIImageStorage.ImageType.ToUpper();
                this.lb_ItemSerialNum.Text = oIImageStorage.ItemSerialNum;
                this.TxtKeyword.Text = oIImageStorage.Keyword;
                this.lb_uploadDate.Text = oIImageStorage.uploadDate.ToString("yyyy-MM-dd");
                //
                this.txt_Caption.Text = oIImageStorage.Caption;
                this.txt_Address.Text = oIImageStorage.Address;
                this.txt_Character.Text = oIImageStorage.Character;
                this.hiFolder.Value = oIImageStorage.FolderName;

                if (oIImageStorage.StartDate.ToString("yyyy-MM-dd") != "1900-01-01")
                {
                    this.Calendar_StartDate.Text = oIImageStorage.StartDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    this.Calendar_StartDate.Text = "";
                }
                if (oIImageStorage.EndDate.ToString("yyyy-MM-dd") != "1900-01-01")
                {
                    this.Calendar_EndDate.Text = oIImageStorage.EndDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    this.Calendar_EndDate.Text = "";
                }
                if (oIImageStorage.shotDate.ToString("yyyy-MM-dd") != "1900-01-01")
                {
                    this.Calendar_ShotDate.Text = oIImageStorage.shotDate.ToString("yyyy-MM-dd");
                }
                else
                {
                    this.Calendar_ShotDate.Text = "";
                }

                using (DataSet ds = GetImageCatalog(oIImageStorage.ItemId.ToString()))
                {
                    if (ds != null && ds.Tables[0].Rows.Count != 0)
                    {
                        DataTable cataTable = ds.Tables[0];

                        for (int i = 0; i < cataTable.Rows.Count; i++)
                        {
                            OutString.Append(cataTable.Rows[i]["CatalogName"].ToString() + "   ");
                        }

                        lb_catalogs.Text = OutString.ToString();

                    }
                }


            }
            catch
            {
                this.imagePanel.Visible = false;
                Response.Write("<script language='javascript'>alert('不存在此图片!');window.close();</script>");
                Response.End();
            }


        }
        //修改文件信息
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            //验证日期
            DateTime sDate = new DateTime();
            DateTime eDate = new DateTime();
            DateTime shotDate = new DateTime();
            if (this.Calendar_ShotDate.Text == "")
            {
                this.Label2.Text = "拍摄时间不能为空！";
                return;
            }
            else
            {
                shotDate = Convert.ToDateTime(this.Calendar_ShotDate.Text);
                if (shotDate > Convert.ToDateTime(this.lb_uploadDate.Text))
                {
                    this.Label2.Text = "拍摄时间应比图片上传日期早！";
                    return;
                }
            }
            if (this.Calendar_StartDate.Text != "")
            {
                sDate = Convert.ToDateTime(this.Calendar_StartDate.Text);
            }
            else
            {
                sDate = Convert.ToDateTime("1900-01-01");
            }
            if (this.Calendar_EndDate.Text != "")
            {
                eDate = Convert.ToDateTime(this.Calendar_EndDate.Text);

                if (sDate != Convert.ToDateTime("1900-01-01") && eDate < sDate)
                {
                    this.Label2.Text = "有效结束日期应比有效开始日期晚";
                    return;
                }
            }
            else
            {
                eDate = Convert.ToDateTime("1900-01-01");
            }



            ImageStorage img = new ImageStorage();
            img.ItemId = new Guid(this.Hidden_ImgItemId.Value);
            img.Address = this.txt_Address.Text;
            img.Caption = this.txt_Caption.Text;
            img.Character = this.txt_Character.Text;
            img.Description = this.TxtDescription.Text;
            img.Keyword = this.TxtKeyword.Text;

            img.StartDate = sDate;
            img.EndDate = eDate;
            img.shotDate = shotDate;

            img.ItemSerialNum = this.lb_ItemSerialNum.Text;

            bool isValidate = QJVRMS.Business.ImageStorageClass.UpdateImageStorage(img);
            if (isValidate)
            {
                ShowMessage("修改文件信息成功!");
            }
            else
            {
                ShowMessage("修改文件信息失败!");
            }

        }

        //修改图片，重新上传图片
        protected void BtnPic_Click(object sender, EventArgs e)
        {

        }
        //删除文件
        protected void btnDel_Click(object sender, EventArgs e)
        {
            string ItemSerialNum = "";
            string ImageType = "";
            string str = "";//判断170图片或者400图片有没有被删除
            ItemSerialNum = lb_ItemSerialNum.Text;
            ImageType = lb_ImageType.Text;
            bool isValidate = QJVRMS.Business.ImageStorageClass.DeleteImageStorage(new Guid(this.Hidden_ImgItemId.Value));



            
            #region 删除物理文件 by ciqq 2010-4-2
            SlImageRootPath = WebUI.UIBiz.CommonInfo.SlImageRootPath170;
            if (Directory.Exists(SlImageRootPath + "\\" + CurrentUser.UserLoginName))
            {
                File.Delete(SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + ItemSerialNum  + ImageType);
                str = "170目录下的图片已被删除!";
            }
            SlImageRootPath = WebUI.UIBiz.CommonInfo.SlImageRootPath400;

            if (Directory.Exists(SlImageRootPath + "\\" + CurrentUser.UserLoginName))
            {
                File.Delete(SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + ItemSerialNum + ImageType);
                str += "400目录下的图片已被删除!";
            }
            SlImageRootPath = WebUI.UIBiz.CommonInfo.ImageRootPath;
            if (Directory.Exists(SlImageRootPath + "\\" + CurrentUser.UserLoginName))
            {
                File.Delete(SlImageRootPath + "\\" + CurrentUser.UserLoginName + "\\" + ItemSerialNum  + ImageType);
                str += "Source目录下的图片已被删除!";
            }

            //删除所有的附件
            string sourceFolder = Path.Combine(WebUI.UIBiz.CommonInfo.ImageRootPath, this.hiFolder.Value);
            string fileName = "";
            sourceFolder = Path.Combine(sourceFolder, UIBiz.CommonInfo.AttachFolder);

            foreach (GridViewRow gvr in this.attList.Rows)
            {
                fileName = Path.Combine(sourceFolder, gvr.Cells[0].Text);

                try
                {
                    File.Delete(fileName);                    
                }
                catch (Exception ex)
                {
                    LogWriter.WriteExceptionLog(ex);
                }
            }
            #endregion

            Response.Write("<script language='javascript'>window.close();</script>");
            Response.End();
        }



        private DataSet GetImageCatalog(string imageGuidID)
        {
            return QJVRMS.Business.ImageStorage.GetImageCatalog(imageGuidID);
        }

        private void GetCatalog(DataTable CTable, DataRow[] Nodes)
        {
            for (int i = 0; i < Nodes.Length; i++)
            {
                if (Nodes[i].ItemArray[2].ToString() == "")
                    OutString.Append(Nodes[i].ItemArray[1].ToString() + "<br /><br />");
                else
                    OutString.Append(Nodes[i].ItemArray[1].ToString() + "   ");

                DataRow[] firstNodesChild = CTable.Select("parentid='" + Nodes[i].ItemArray[0].ToString() + "'", "CatalogOrder");
                GetCatalog(CTable, firstNodesChild);
            }
        }


        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpAttach_Click(object sender, EventArgs e)
        {
            string sourceFolder = Path.Combine(WebUI.UIBiz.CommonInfo.ImageRootPath, this.hiFolder.Value);
            sourceFolder = Path.Combine(sourceFolder, UIBiz.CommonInfo.AttachFolder);

            if (!Directory.Exists(sourceFolder))
            {
                Directory.CreateDirectory(sourceFolder);
            }

            string fileType = Path.GetExtension(this.attachFile.FileName);
            string fileName = Path.GetFileName(this.attachFile.FileName);
            //string NewfileName = fileName + fileType;

            string fileFullPath = Path.Combine(sourceFolder, fileName);

            if (fileName.Length > 255)
            {
                ShowMessage("附件名称过长,需小于255个字符!");
                return;
            }

            if (File.Exists(fileFullPath))
            {
                ShowMessage("附件名称重复，请删除原有附件重新上传!");
                return;
            }

            try
            {
                this.attachFile.SaveAs(fileFullPath);

                if (ImageStorageClass.AddAttach(new Guid(this.Hidden_ImgItemId.Value), fileName))
                {
                    ShowMessage("添加附件成功!");
                    LoadAttach();
                }
                else
                {
                    ShowMessage("添加附件失败!");
                }
            }
            catch (PathTooLongException pe)
            {
                ShowMessage("附件名称过长!");
            }

        }

        /// <summary>
        /// 显示附件信息
        /// </summary>
        protected void LoadAttach()
        {
            DataTable dt = ImageStorageClass.GetAttachList(new Guid(this.Hidden_ImgItemId.Value));

            this.attList.DataSource = dt;
            this.attList.DataBind();
        }

        protected void attList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sourceFolder = Path.Combine(WebUI.UIBiz.CommonInfo.ImageRootPath, this.hiFolder.Value);
            sourceFolder = Path.Combine(sourceFolder, UIBiz.CommonInfo.AttachFolder);


            string fileName = this.attList.Rows[e.RowIndex].Cells[0].Text;
            fileName = Path.Combine(sourceFolder, fileName);

            try
            {
                File.Delete(fileName);
                string key = this.attList.DataKeys[e.RowIndex].Value.ToString();
                ImageStorageClass.DeleteAttach(new Guid(key));
                LoadAttach();
            }
            catch(Exception ex)
            {
                LogWriter.WriteExceptionLog(ex);
            }


        }

    }
}
