using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using QJVRMS.Business;
using QJVRMS.Common;
using System.IO;
using System.Net;
using System.Runtime;
using System.Text;
using QJVRMS.Business.SecurityControl;
using WebUI.UIBiz;
using QJVRMS.Business.ResourceType;

namespace WebUI.Modules
{
    public partial class ResourceEditBatch : AuthPage
    {
        string yRootPath = "";
        string SlImageRootPath;
        StringBuilder OutString = new StringBuilder();

        public string itemIds = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.IsPostBack && !string.IsNullOrEmpty(Request["itemId"]))
            //{
            //    SearchItemSerialNum(Request["itemId"]);
            //    LoadAttach();


            //    bindKeywordCat();
            //    bindRptKeywordDetail();
            //}

            if (!this.IsPostBack  && !string.IsNullOrEmpty(Request["ids"]))
            {
                bindKeywordCat();
                bindRptKeywordDetail();

                itemIds = Request.QueryString["ids"].ToString(); ;
            }

        }



        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = false;
            base.OnInit(e);
        }

        

        //修改文件信息
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            string ids = Request.QueryString["ids"];
            if (string.IsNullOrEmpty(ids))
            {
                return;
            }

            ids = ids.TrimEnd(";".ToCharArray());
            string[] arrIds = ids.Split(";".ToCharArray());



            ResourceEntity re = null;
            Resource r = new Resource();



            foreach (string id in arrIds)
            {

                re = r.GetResourceInfoByItemId(id);
                re.Caption = this.txt_Caption.Text;
                re.Description = this.TxtDescription.Text;
                re.Keyword = this.TxtKeyword.Text;
                try
                {
                    r.Update(re);

                  
                    LogEntity model = new LogEntity();
                    model.id = Guid.NewGuid();
                    model.userId = CurrentUser.UserId;
                    model.userName = CurrentUser.UserLoginName;
                    model.EventType = ((int)LogType.EditResource).ToString();
                    model.EventResult = "成功";
                    model.EventContent = "图片序号："+re.ItemSerialNum ;
                    model.IP = HttpContext.Current.Request.UserHostAddress;
                    model.AddDate = DateTime.Now;
                    new Logs().Add(model);


                    ShowMessage("修改文件信息成功!");


                }
                catch (Exception e1)
                {
                    ShowMessage("修改文件信息失败!" + e1.Message);
                }
            }





            //ResourceEntity re = null;
            //Resource r = new Resource();
            //if (ViewState["model"] != null)
            //{
            //    re = ViewState["model"] as ResourceEntity;
            //}
            //else
            //{
            //    re = r.GetResourceInfoByItemId(this.Hidden_ItemId.Value);
            //}

            //re.Caption = this.txt_Caption.Text;
            //re.Description = this.TxtDescription.Text;
            //re.Keyword = this.TxtKeyword.Text;
            //re.shotDate = shotDate;
            //re.StartDate = sDate;
            //re.EndDate = eDate;

            //string editResult = "";
            //try
            //{
            //    r.Update(re);

            //    ShowMessage("修改文件信息成功!");
            //    editResult = "成功";
            //}
            //catch (Exception e1)
            //{
            //    ShowMessage("修改文件信息失败!" + e1.Message);
            //    editResult = "失败";
            //}


            //LogEntity model = new LogEntity();
            //model.id = Guid.NewGuid();
            //model.userId = CurrentUser.UserId;
            //model.userName = CurrentUser.UserLoginName;
            //model.EventType = ((int)LogType.EditResource).ToString();
            //model.EventResult = editResult;
            //model.EventContent = "图片序号：" + re.ItemSerialNum;
            //model.IP = HttpContext.Current.Request.UserHostAddress;
            //model.AddDate = DateTime.Now;
            //new Logs().Add(model);



        }

        
        private DataSet GetResourceCatalog(string itemid)
        {
            return new Resource().GetResourceCatalogByItemId(itemid);
        }

        private void GetCatalog(DataTable CTable, DataRow[] Nodes)
        {
            for (int i = 0; i < Nodes.Length; i++)
            {
                if (Nodes[i].ItemArray[2].ToString() == "")
                    OutString.Append(Nodes[i].ItemArray[1].ToString() + "<br /><br />");
                else
                    OutString.Append(Nodes[i].ItemArray[1].ToString() + "   <br/>");

                DataRow[] firstNodesChild = CTable.Select("parentid='" + Nodes[i].ItemArray[0].ToString() + "'", "CatalogOrder");
                GetCatalog(CTable, firstNodesChild);
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
                //根据分类ID查询该分类下的产品，并绑定产品Repeater
                rptKeywordDetail.DataSource = obj.GetKeywordsByParentid(CategorieId);
                rptKeywordDetail.DataBind();
            }


        }
    }
}
