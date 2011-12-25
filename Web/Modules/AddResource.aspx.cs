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
using QJVRMS.Business.ResourceType;

namespace WebUI.Modules
{
    public partial class AddResource :   AuthPage 
    {
        public static DataTable cataTable = null;
        protected string newSn = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsSuperAdmin)
                {
                    DataTable dtBig = this.GetDenyTable();
                    if (dtBig.Rows.Count == 0)
                    {
                        Response.Write("��û��Ȩ���ϴ�");
                        Response.End();
                    }
                }

                initCalendar();
                initCopyright();
                bindKeywordCat();
                bindRptKeywordDetail();

                bindBigCat();
                bindRptCatDetail();
            }
        }

        private void initCopyright()
        {
            this.rblCopyright.Attributes.Add("onclick", "rblCopyrightChanged()");
        }

        private void bindKeywordCat()
        {
            KeyWords obj=new KeyWords();
            DataSet ds=obj.GetKeywordsByParentid(0);
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



        /// <summary>
        /// �û�������Ϣд��ViewState
        /// </summary>
        /// <returns></returns>
        protected DataTable GetDenyTable()
        {
            if (this.ViewState["denyTable"] == null)
            {
                DataTable t = QJVRMS.Business.Catalog.GetCatalogByMethod(CurrentUser.UserId, QJVRMS.Business.SecurityControl.OperatorMethod.Write);
                this.ViewState["denyTable"] = t;
            }

            return this.ViewState["denyTable"] as DataTable;
        }


        protected void GetCataTable()
        {
            if (cataTable == null)
            {
                cataTable = QJVRMS.Business.Catalog.GetAllCatalog();
            }
            cataTable = QJVRMS.Business.Catalog.GetAllCatalog();
        }

        private void bindBigCat()
        {
            GetCataTable();
            if (cataTable.Rows.Count == 0) return;

            DataRow[] firstNodes = cataTable.Select("parentid is null", "CatalogOrder");

            DataTable newDataTable = cataTable.Clone();
            for (int i = 0; i < firstNodes.Length; i++) 
            {
                //newDataTable.ImportRow(firstNodes[i]);
                string cataId = firstNodes[i]["CatalogId"].ToString();
                if (this.GetDenyTable().Select("ObjectId='" + cataId + "'").Length > 0 ||IsSuperAdmin)
                {
                    newDataTable.ImportRow(firstNodes[i]);
                }
            }

            this.rptBigCat.DataSource = newDataTable.DefaultView;
            this.rptBigCat.DataBind();
            


            //KeyWords obj = new KeyWords();
            //DataSet ds = obj.GetKeywordsByParentid(0);
            //this.rptKeywordCat.DataSource = ds.Tables[0].DefaultView;
            //this.rptKeywordCat.DataBind();
        }

        private void bindRptCatDetail()
        {

            GetCataTable();
            if (cataTable.Rows.Count == 0) return;

            DataRow[] firstNodes = cataTable.Select("parentid is null", "CatalogOrder");

            DataTable newDataTable = cataTable.Clone();
            for (int i = 0; i < firstNodes.Length; i++)
            {
                //newDataTable.ImportRow(firstNodes[i]);
                string cataId = firstNodes[i]["CatalogId"].ToString();
                if (this.GetDenyTable().Select("ObjectId='" + cataId + "'").Length > 0 || IsSuperAdmin)
                {
                    newDataTable.ImportRow(firstNodes[i]);
                }
            }

            this.rptBigCat1.DataSource = newDataTable.DefaultView;
            this.rptBigCat1.DataBind();
            
            
            //KeyWords obj = new KeyWords();
            //DataSet ds = obj.GetKeywordsByParentid(0);
            //this.rptKeywordDetail1.DataSource = ds.Tables[0].DefaultView;
            //this.rptKeywordDetail1.DataBind();

        }


        public void rptBigCat1_ItemDataBoud(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {

            GetCataTable();            
            

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptSmallCat = (Repeater)e.Item.FindControl("rptSmallCat");
                DataRowView rowv = (DataRowView)e.Item.DataItem;
               
                
                //int CategorieId = Convert.ToInt32(rowv["ID"]);
                //���ݷ���ID��ѯ�÷����µĲ�Ʒ�����󶨲�ƷRepeater



                string parentId = rowv["CatalogId"].ToString();
                DataRow[] childNodes = cataTable.Select("parentid='" + parentId + "'", "CatalogOrder");
                DataTable newDataTable = cataTable.Clone(); 
                for (int i = 0; i < childNodes.Length; i++)
                {
                    //newDataTable.ImportRow(childNodes[i]);
                    string cataId = childNodes[i]["CatalogId"].ToString();
                    if (this.GetDenyTable().Select("ObjectId='" + cataId + "'").Length > 0 || IsSuperAdmin)
                    {
                        newDataTable.ImportRow(childNodes[i]);
                    }
                }


                rptSmallCat.DataSource = newDataTable.DefaultView;
                rptSmallCat.DataBind();

            }




        }

        protected void btnUpload_ServerClick(object sender, EventArgs e)
        {
            //�Ƿ��а�Ȩ����Դ
            int hasCopyRight = 0;
            try
            {
                hasCopyRight = Convert.ToInt32(this.rblCopyright.SelectedValue);
            }
            catch (Exception ex)
            { }


            #region ��֤���ڲ���
            //if (this.shotDate1.Text.Trim() == string.Empty)
            //{
            //    this.ShowMessage(this, "��ѡ��ʱ��");
            //    return;
            //}
            //else
            //{
            //    if (Convert.ToDateTime(this.shotDate1.Text) > DateTime.Now)
            //    {
            //        this.ShowMessage(this, "����ʱ��Ӧ��������");
            //        return;
            //    }
            //}
            //��֤����
            DateTime sDate = new DateTime();
            DateTime eDate = new DateTime();
            if (this.startDate.Text != "")
            {
                //��Ч��ʼ���ھͲ�����֤�ˣ�ֻҪ�������ڱ�������Ϳ����ˡ�

                sDate = Convert.ToDateTime(this.startDate.Text);
                //if (sDate <= DateTime.Now)
                //{
                //    this.ShowMessage(this, "��Ч��ʼ����Ӧ������������");
                //    return;
                //}
            }
            else
            {
                sDate = Convert.ToDateTime("1900-01-01");
            }
            if (this.endDate.Text != "")
            {
                eDate = Convert.ToDateTime(this.endDate.Text);

                if (sDate == Convert.ToDateTime("1900-01-01") && eDate < DateTime.Now)
                {
                    this.ShowMessage(this, "��Ч��������Ӧ���ٱ�����������");
                    return;
                }
                else if (eDate < sDate)
                {
                    this.ShowMessage(this, "��Ч��������Ӧ����Ч��ʼ������");
                    return;
                }
            }
            else
            {
                eDate = Convert.ToDateTime("1900-01-01");
            }

            #endregion 


            #region ��֤�Ƿ�ѡ��Ŀ¼�ڵ�
            ////���ڵ�
            //TreeNode parentNode = catalogTree.RootNode;
            ////��ȡchecked�Ľڵ�List
            //ArrayList nodeList = new ArrayList();
            //this.catalogTree.ArrCheckbox(nodeList, parentNode);

            //ArrayList catalogIds = new ArrayList(nodeList.Count);
            //foreach (TreeNode node in nodeList)
            //{

            //    catalogIds.Add(new Guid(node.Value));
            //}

            //if (catalogIds.Count == 0)
            //{
            //    this.ShowMessage(this, "û��ѡ�����,�ϴ�ʧ��!");
            //    return;
            //}


            string catIds = this.hidCatIds.Value.Trim().Trim(new char[] { ',' });
            string[] arrCatIds = catIds.Split(",".ToCharArray());
            if (arrCatIds.Length == 0)
            {
                this.ShowMessage(this, "û��ѡ�����,�ϴ�ʧ��!");
                return;
            }
            ArrayList catalogIds = new ArrayList(arrCatIds.Length);
            foreach(string _s in arrCatIds)
            {
                catalogIds.Add(new Guid(_s));
            }
            #endregion 


            string fileName = "";   //ԭʼ�ļ���
            if (!string.IsNullOrEmpty(Request["selectedFile"]))
            {
                fileName = Request["selectedFile"].ToString().ToLower();
            }
            else
            {
                return;
            }

            string uploadFileName = ""; //�ϴ��Ժ����·�����ļ��� prefix+yymmdd+00001.extention
            if (!string.IsNullOrEmpty(Request["uploadFileName"]))
            {
                uploadFileName = Request["uploadFileName"].ToString();
            }
            else
            {
                return;
            }

            string[] arrFiles = uploadFileName.Split(',');


            foreach (string singleFiles in arrFiles) {
                if (!singleFiles.Contains(":")) {
                    continue;
                }

                string[] _arr = singleFiles.Split(':');
                string strClientFileName = _arr[1];
                string strServerFileName = _arr[0];



                fileName = strClientFileName;
                uploadFileName = strServerFileName;

                /** start **/
                Resource objResource = new Resource();
                ResourceEntity model = new ResourceEntity();

                //����Ա�ϴ�ֱ�����ͨ��
                if (IsSuperAdmin) {
                    model.Status = (int)ResourceEntity.ResourceStatus.IsPass;
                }
                else {
                    model.Status = (int)ResourceEntity.ResourceStatus.NewUpload;
                }

                //�ĳ��������ϴ��Ķ�����ֱ��ͨ�� �� ��ʱֱ��ע������һ�仰�Ϳ���
                model.Status = (int)ResourceEntity.ResourceStatus.IsPass;
                model.Status = 0;

                if (this.txt_Caption.Value.Trim().Length > 0) {
                    model.Caption = this.txt_Caption.Value;
                }
                else {
                    model.Caption = Path.GetFileNameWithoutExtension(strClientFileName);
                }

                model.Description = this.description.Value;
                model.EndDate = eDate;
                model.FileName = fileName;
                model.FolderName = CurrentUser.UserLoginName;
                if (uploadFileName.ToLower().IndexOf(".cr2") != -1 || uploadFileName.ToLower().IndexOf(".nef") != -1 || uploadFileName.ToLower().IndexOf(".psd") != -1) {
                    model.ServerFileName = uploadFileName.Replace("cr2", "jpg").Replace("nef", "jpg").Replace("psd", "jpg");
                }
                else {
                    model.ServerFileName = uploadFileName;
                }
                model.GroupId = CurrentUser.UserGroupId;
                model.ItemId = Guid.NewGuid();
                model.ItemSerialNum = Path.GetFileNameWithoutExtension(uploadFileName);
                //model.Keyword = this.keyWord.Value;
                model.Keyword = this.txtKeyWords.Text.Trim().Trim(",".ToCharArray());
                //model.shotDate = Convert.ToDateTime(this.shotDate1.Text);
                model.StartDate = sDate;
                model.uploadDate = DateTime.Now;
                model.userId = CurrentUser.UserId;
                model.updateDate = DateTime.Now;
                model.Author = this.txt_Author.Value.Trim();

                //ȡ���ļ�����չ����������.��
                string fileExtName = string.Empty;
                fileExtName = Path.GetExtension(fileName);
                if (fileExtName.IndexOf(".") > -1)
                {
                    fileExtName = fileExtName.Substring(1);
                }

                model.ResourceType = ResourceTypeFactory.getResourceType(fileExtName).ResourceType;
                model.FileSize = Resource.GetResourceFileSize(uploadFileName, model.FolderName, fileExtName, "");
                model.HasCopyright = hasCopyRight;

                model.shotDate = DateTime.Now;
                DateTime shotDateTime = Resource.GetResourceShotDateTime(uploadFileName, model.FolderName, model.ResourceType, "");
                if (shotDateTime != DateTime.MinValue) {
                    model.shotDate = shotDateTime;
                }

                objResource.Add(model);
                objResource.CreateRelationshipResourceAndCatalog(model.ItemId, (Guid[])catalogIds.ToArray(typeof(Guid)));

                //ͬʱ��������
                string[] SNs = new string[] { model.ItemSerialNum };
                ResourceIndex.updateIndex(SNs);


            }
            /** end  **/



            #region ע�Ͳ���

            //VideoStorageClass vsc = new VideoStorageClass();
            //VideoStorage v = new VideoStorage();
            //v.Caption = this.txt_Caption.Value;
            //v.Description = this.description.Value;
            //v.EndDate = eDate;
            //v.FileName = fileName;
            //v.FolderName = CurrentUser.UserLoginName;
            //v.ServerFileName = uploadFileName;
            //v.GroupId = CurrentUser.UserGroupId;
            //v.ItemId = Guid.NewGuid();
            //v.ItemSerialNum = Path.GetFileNameWithoutExtension(uploadFileName);
            //v.Keyword = this.keyWord.Value;
            //v.shotDate = Convert.ToDateTime(this.shotDate.Value);
            //v.StartDate = sDate;
            //v.uploadDate = DateTime.Now;
            //v.userId = CurrentUser.UserId;
            //v.updateDate = DateTime.Now;

            ////�洢���ݿ��¼
            //// img.ItemSerialNum = ImageStorageClass.AddImageStorage(img);

            //if (!vsc.Add(v))
            //{
            //    this.ShowMessage(this, "�ϴ�ʧ��");
            //    return;
            //}

            //vsc.CreateRelationshipVideoAndCatalog(v.ItemId, (Guid[])catalogIds.ToArray(typeof(Guid)));
            #endregion


            //this.shotDate1.Text = "";
            this.keyWord.Value = "";
            this.description.Value = "";
            this.txt_Caption.Value = "";
            this.startDate.Text = "";
            this.endDate.Text = "";
            initCalendar();
            
            //this.ShowMessage(this, "�ϴ��ɹ�");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>myUploadSuccess2()</script>");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>myUploadSuccess2()</script>");
        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }
        private void initCalendar()
        {
            //shotDate1.Text  = DateTime.Now.ToString("yyyy-MM-dd");
            //startDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
           // endDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            //this.sh
        }


        private new void ShowMessage(Control container, String message)
        {
            
            string script = string.Format("alert('{0}');", message);

            ScriptManager.RegisterStartupScript(container,
              typeof(Page),
              "myAlert",
              script, true);

        }

        
        
    }
}
