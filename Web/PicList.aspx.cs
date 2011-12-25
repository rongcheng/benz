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
using QJVRMS.Business.SecurityControl;
using System.Collections.Generic;

namespace WebUI
{
    public partial class PicList : AuthPage
    {
        private int _curpage;
        string isChangePageSize = "0";
        private string keyword = "";
        private string catalogID = "";
        private string beginDate = "";
        private string endDate = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            PageBar1.PageSize = NowPageCount();// UIBiz.CommonInfo.PageCount;
            PageBar2.PageSize = NowPageCount(); //UIBiz.CommonInfo.PageCount;

            isChangePageSize = this.Search_ReSetPageSize1.isChangePageSize;

            string showCata = Request.QueryString["showCata"] == null ? "" : Request.QueryString["showCata"];
            this.catalogID = Request.QueryString["CatalogID"] == null ? "00000000-0000-0000-0000-000000000000" : Request.QueryString["CatalogID"].ToString();




            //分类检索
            if (showCata == "1")
            {

                List<ObjectRule> rules = new List<ObjectRule>(1);
                ISecurityObject securityObj = new SecurityObject(new Guid(this.catalogID), SecurityObjectType.Items);
                ObjectRule or = new ObjectRule(securityObj, new User(CurrentUser.UserId), OperatorMethod.Deny);
                rules.Add(or);
                ObjectRule.CheckRules(rules);

                // if (!Catalog.GetCataRight(CurrentUser.UserId, new Guid(this.catalogID)))
                if (rules[0].IsValidate)
                {
                    ShowMessage("您没有权限浏览此分类!");
                    Response.Redirect(FormsAuthentication.DefaultUrl, true);
                }

                //    this.cataNav.Visible = true;

                this.BindCataNav();

            }
            //关键字或高级搜索
            else
            {
                this.keyword = Request.QueryString["keyword"] == null ? "" : Request.QueryString["keyword"].ToString();
                this.keyword = Server.UrlDecode(this.keyword);
                this.beginDate = Request.QueryString["BeginDate"].ToString();//上传时间起始日期
                this.endDate = Request.QueryString["EndDate"].ToString();//上传时间结束日期   
                //     this.cataNav.Visible = false; ;
            }

            if (isChangePageSize == "1")
            {
                this.Search_ReSetPageSize1.isChangePageSize = string.Empty;
                _curpage = 0;

            }
            if (!Page.IsPostBack || isChangePageSize == "1")
            {
                BindData(PageBar1.PageSize, _curpage);
            }

        }


        private void BindCataNav()
        {
            this.lb_CatalogNav.Text = Request["cname"];

        }

        public Guid SelDeptId
        {
            get
            {
                //if (this.deptTree.CurrentSelNode != null)
                //{
                //    return new Guid(this.deptTree.CurrentSelNode.Value);
                //}
                //return Guid.Empty;
                // return new Guid(this.DeptTree1.CurrentSelNode.Value);


                if (this.hiSelDeptId.Value != string.Empty)
                {
                    return new Guid(this.hiSelDeptId.Value);
                }

                return Guid.Empty;
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        public void BindData(Int32 pageSize, Int32 pageIndex)
        {

            int rowCount = 0, pageCount = 0;
            //DataTable dtSource = QJVRMS.Business.ImageStorage.SearchImage(keyword, this.beginDate, this.endDate, catalogID, CurrentUser.UserId.ToString(), PageBar1.PageSize, pageIndex, ref rowCount);

            DateTime beg, end;

            try
            {
                beg = DateTime.Parse(this.beginDate);
                end = DateTime.Parse(this.endDate);
            }
            catch
            {
                beg = DateTime.MaxValue;
                end = DateTime.MaxValue;
            }

            Guid cataId;

            try
            {
                cataId = new Guid(catalogID);
            }
            catch
            {
                cataId = Guid.Empty;
            }

            DataSet ds = null;
            DataTable dtSource = null;

            if (CheckViewByDept(catalogID))
            {
                //    this.deptDiv.Visible = true; 

                this.DeptTrigger.Visible = true;
                if (chkDept.Checked)
                    ds = QJVRMS.Business.ImageStorage.SearchByDept(new Guid(this.catalogID), CurrentUser.UserId, SelDeptId, pageSize, pageIndex);
                else
                    ds = QJVRMS.Business.ImageStorage.SearchByDept(new Guid(this.catalogID), CurrentUser.UserId, Guid.Empty, pageSize, pageIndex);
                //   ds = QJVRMS.Business.ImageStorage.SearchByDept(new Guid(this.catalogID), CurrentUser.UserId, SelDeptId, pageSize, pageIndex);


            }
            else
            {
                this.DeptTrigger.Visible = false;
                //this.deptDiv.Visible = false; 
                ds = QJVRMS.Business.ImageStorage.SearchImage(this.PageBar1.PageSize, pageIndex, out pageCount, keyword, beg, end, cataId, CurrentUser.UserId);
            }

            dtSource = ds.Tables[0];
            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out rowCount);





            this.lb_ResultCount.Text = "共检索到" + rowCount.ToString() + "张图片";

            this.PageBar1.RecordCount = rowCount;//sp.recordCount;
            this.PageBar2.RecordCount = rowCount;

            //this.PageBar1.CurrentPageIndex = pageIndex;
            //this.PageBar2.CurrentPageIndex = pageIndex;

            this.DataPic1.DataSource = dtSource;
            this.DataPic1.DataBind();


        }


        //void deptTree_GroupSel(object sender, EventArgs e)
        //{
        //    BindData(PageBar1.PageSize, 0);
        //    this.lbDeptName.Text = this.deptTree.CurrentSelNode.Text;
        //}


        protected void chkDept_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.DeptTree1.CurrentSelNode == null)
            //{
            //    this.DeptTree1.RootNode.Selected = true;
            //}
            BindData(PageBar1.PageSize, 0);

            if (!this.chkDept.Checked) this.lbDeptName.Text = string.Empty;
        }

        //void DeptTree1_GroupSel(object sender, EventArgs e)
        //{

        //    BindData(PageBar1.PageSize, 0);
        //}

        //分页
        protected void PageBar1_PageChanged(object src, QJ.WebControls.PageChangedEventArgs e)
        {
            _curpage = e.NewPageIndex;

            if (_curpage != 0) _curpage--;

            this.PageBar1.CurrentPageIndex = e.NewPageIndex;
            this.PageBar2.CurrentPageIndex = e.NewPageIndex;

            BindData(PageBar1.PageSize, _curpage);
        }


        protected int NowPageCount()
        {

            HttpCookie pageCountCookie = Request.Cookies["QJpageCount"];
            int defaultCount = UIBiz.CommonInfo.PageCount;
            if (pageCountCookie == null)
            {
                return UIBiz.CommonInfo.PageCount;
            }
            else
            {
                int.TryParse(pageCountCookie.Value, out defaultCount);
                return defaultCount;
            }
        }

        protected bool CheckViewByDept(string cataId)
        {
            for (int i = 0; i < UIBiz.CommonInfo.ViewByDept.Length; i++)
            {
                if (string.Compare(cataId, UIBiz.CommonInfo.ViewByDept[i], true) == 0)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Post deptId with Ajax
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPostDeptId_ServerClick(object sender, EventArgs e)
        {
            BindData(PageBar1.PageSize, 0);
            if (this.chkDept.Checked) this.lbDeptName.Text = this.hiDeptName.Value;

            if (this.hiIsSelLeft.Value == "1")
                this.DeptGridShow2.GenRightDeptGrid(this.hiSelDeptId.Value);

        }


    }
}
