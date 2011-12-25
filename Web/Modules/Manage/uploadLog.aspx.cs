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
using QJVRMS.Business.Interface;
using QJVRMS.Business.ResourceType;

namespace WebUI.Modules.Manage
{
    public partial class uploadLog : AuthPage
    {
        private int pageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                this.t_Date.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
                this.e_Date.Text = DateTime.Now.ToShortDateString();
            }

        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }

        protected void BindLog(int pageSize,int pageIndex)
        {   
            string username = txtLoginName.Text.Trim();
            string userId = "";

            if (!string.IsNullOrEmpty(username))
            {
               
                MemberShipManager msm = new MemberShipManager();

                if (msm.IsUserExist(username))
                {
                    User _user = msm.GetUser(username);

                    if (_user != null)
                    {
                        userId = _user.UserId.ToString();
                    }
                }
                else
                {
                    GridView1.EmptyDataText = "没有该用户";
                    this.AspNetPager1.RecordCount = 0;
                    GridView1.DataSource = null;//ds;
                    GridView1.DataBind();
                    return;
                }
            }

            if (t_Date.Text != string.Empty
                && e_Date.Text != string.Empty)
            {
                DateTime begin = Convert.ToDateTime(t_Date.Text);
                DateTime end = Convert.ToDateTime(e_Date.Text).AddDays(1);

                DataSet ds;
                
                Resource obj = new Resource();
                ds = obj.GetResourcesUploadLog(begin, end, pageSize, pageIndex, userId);

                if (ds != null && ds.Tables[1].Rows.Count > 0)
                {
                    this.AspNetPager1.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    GridView1.DataSource = ds.Tables[1];
                    GridView1.DataBind();
                }
                else
                {
                    this.AspNetPager1.RecordCount = 0;
                    GridView1.DataSource = null;//ds;
                    GridView1.DataBind();
                }
            }
        }



        protected void searchDate_Click(object sender, EventArgs e)
        {
            BindLog(this.AspNetPager1.PageSize, pageIndex);
        }

        //protected string GetImgUrl(string ItemSerialNum, string ImageType, string folder)
        //{
        //    return UIBiz.CommonInfo.GetImageUrl(170, folder, ItemSerialNum, ImageType);

        //}


        protected string GetImgUrl(string ItemSerialNum, string serverFileName, string folder,string resourceType)
        {

            IResourceType obj = ResourceTypeFactory.getResourceTypeByString(resourceType);

            if (resourceType.ToLower().Equals("image"))
            {

                return obj.GetPreviewPathRead(folder, serverFileName, "170");
            }
            else if (resourceType.ToLower().Equals("video"))
            {

                return obj.GetPreviewPathRead(folder, ItemSerialNum + ".jpg", "image");
            }

            return "";
           //return obj.GetPreviewPathRead(folder, ItemSerialNum, ImageType);
        
        }


        protected string GetImgUrl(string serverFileName, string folder)
        {
            QJVRMS.Business.ResourceType.ImageType obj = new QJVRMS.Business.ResourceType.ImageType();
            //yangguang
            //return obj.PreviewPath_170_Read + "/" + folder + "/" + serverFileName;
            return obj.GetPreviewPathRead(folder, serverFileName, "170");

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            BindLog(this.AspNetPager1.PageSize, pageIndex);
        }


        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.pageIndex = e.NewPageIndex;
            
            BindLog(this.AspNetPager1.PageSize, pageIndex);
        }


    }
}
