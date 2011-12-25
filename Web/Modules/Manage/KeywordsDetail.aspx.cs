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

namespace WebUI.Modules.Manage
{
    public partial class KeywordsDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    this.hId.Value = id;

                    bind();
                }
                else
                { 
                    //关闭该窗口
                }
            
            }

        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            string txtName = this.txtName.Text.Trim().Replace("'", "''");
            //string txtOrder = this.txtOrder.Text.Trim();

            int sort=1;
            //int.TryParse(txtOrder, out sort);

            int parentId = Convert.ToInt32(this.hId.Value);

            QJVRMS.Business.KeyWords obj = new QJVRMS.Business.KeyWords();
            obj.Add(parentId, txtName, sort);

            bind();

        }

        private void bind()
        {
            int parentId = Convert.ToInt32(this.hId.Value);

            QJVRMS.Business.KeyWords obj = new QJVRMS.Business.KeyWords();
            DataSet ds = obj.GetKeywordsByParentid(parentId);
            
            this.rptKey.DataSource = ds.Tables[0].DefaultView;
            this.rptKey.DataBind();
            
        }

        protected void rptKey_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (e.CommandName.ToLower().Equals("del"))
                {
                    int id;
                    int.TryParse(e.CommandArgument.ToString(), out id);

                    QJVRMS.Business.KeyWords obj = new QJVRMS.Business.KeyWords();
                    obj.Delete(id);

                    bind();
                
                }
            
            }

        }
    }
}
