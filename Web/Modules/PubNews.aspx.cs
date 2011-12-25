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

namespace WebUI.Modules
{
    public partial class PubNews : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetNewsList();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.IsInControl = true;
            base.OnInit(e);
        }



        protected void btnPub_Click(object sender, EventArgs e)
        {
            string title = this.txtTitle.Text.Trim();
            string content = this.txtContent.Text.Trim().Replace("\r\n", "<br/>").Replace(" ", "&nbsp;"); ;
            char isVal = char.Parse(this.ddlVal.SelectedValue);
            char isTop = this.chkTop.Checked ? '1' : '0';
            char ntype = char.Parse(this.ddlType.SelectedValue);

            if (QJVRMS.Business.News.CreateNews(Guid.NewGuid(),
                title, content,
                DateTime.Now,
                CurrentUser.UserLoginName,
                CurrentUser.UserName,
                CurrentUser.UserId, isVal, isTop,ntype))
            {
                ShowMessage("发布成功");
                GetNewsList();
            }
            else
            {
                ShowMessage("发布失败");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.newsPanel.Visible = false;
            GetNewsList();
        }

        protected void GetNewsList()
        {
            string title = this.txtTitleSea.Text.Trim();

            DataTable dt = QJVRMS.Business.News.GetNewsList("%" + title + "%",char.Parse(this.ddlTypeSea.SelectedValue));
            this.newsList.DataSource = dt;
            this.newsList.DataBind();
        }

       
        /// <summary>
        /// 准备添加新闻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnToAddNews_Click(object sender, EventArgs e)
        {
            this.txtTitle.Text = string.Empty;
            this.txtContent.Text = string.Empty;
            this.newsPanel.Visible = true;
            this.btnPub.Visible = true;
            this.btnUpdate.Visible = false;
        }

        protected void newsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string command = e.CommandName.ToLower();
           
           

            if (command == "modify")
            {
                this.hiSelNewsId.Value = e.CommandArgument.ToString();
                this.newsPanel.Visible = true;
                this.btnPub.Visible = false;
                this.btnUpdate.Visible = true;
                ReadyToUpdate();

            }
            else if (command == "del")
            {
                Guid newsId = new Guid(e.CommandArgument.ToString());
                QJVRMS.Business.News.DeleteNews(newsId);
                GetNewsList();
                this.newsPanel.Visible = false;

            }
        }

        protected void ReadyToUpdate()
        {
            QJVRMS.Business.News news = QJVRMS.Business.News.GetNews(new Guid(this.hiSelNewsId.Value));

            this.txtTitle.Text = news.Title;
            this.txtContent.Text = news.Content.Replace("<br/>","\r\n").Replace("&nbsp;"," ");

            this.ddlVal.SelectedIndex = -1;
            ListItem aimItem = this.ddlVal.Items.FindByValue(news.IsVal.ToString());
            if (aimItem != null) aimItem.Selected = true;

            this.ddlType.SelectedIndex = -1;
            aimItem = this.ddlType.Items.FindByValue(news.NType.ToString());
            if (aimItem != null)  aimItem.Selected = true;

            this.chkTop.Checked = news.IsTop == '1';
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string title = this.txtTitle.Text.Trim();
            string content = this.txtContent.Text.Replace("\r\n", "<br/>").Replace(" ", "&nbsp;");
            char isVal = char.Parse(this.ddlVal.SelectedValue);
            char isTop = this.chkTop.Checked ? '1' : '0';
            char ntype = char.Parse(this.ddlType.SelectedValue);

            if (QJVRMS.Business.News.UpdateNews(new Guid(this.hiSelNewsId.Value), title, content, isVal, isTop,ntype))
            {
                ShowMessage("更新成功");
                this.newsPanel.Visible = false;
            }
            else
            {
                ShowMessage("更新失败");
            }
        }

        protected void newsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.newsList.PageIndex = e.NewPageIndex;
            GetNewsList();
        }


    }
}
