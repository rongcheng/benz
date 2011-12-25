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
using AjaxControlToolkit;
namespace WebUI.UserControls
{
    public partial class CatalogMenu : BaseUserControl
    {

        /// <summary>
        /// 缓存分类数据
        /// </summary>
        public static DataTable cataTable = null;

        protected System.Text.StringBuilder html = null;

        protected void GetCataTable()
        {
            if (cataTable == null)
            {
                cataTable = QJVRMS.Business.Catalog.GetAllCatalog();
            }
        }


        /// <summary>
        /// 用户特有信息写入ViewState
        /// </summary>
        /// <returns></returns>
        protected DataTable GetDenyTable()
        {
            if (this.ViewState["denyTable"] == null)
            {
                DataTable t = QJVRMS.Business.Catalog.GetCatalogByMethod(CurrentUser.UserId, QJVRMS.Business.SecurityControl.OperatorMethod.Deny);
                this.ViewState["denyTable"] = t;
            }

            return this.ViewState["denyTable"] as DataTable;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //  if (!this.IsPostBack)
            {
                GetCataTable();
                if (cataTable.Rows.Count == 0) return;


                #region 权限判定


                //    denyTable = QJVRMS.Business.Catalog.GetCatalogByMethod(CurrentUser.UserId, QJVRMS.Business.SecurityControl.OperatorMethod.Deny);


                #endregion

                DataRow[] firstNodes = cataTable.Select("parentid is null", "CatalogOrder");

                Int16 menuIndex = 0;

                html = new System.Text.StringBuilder();
                foreach (DataRow dr in firstNodes)
                {
                    string cataId = dr["CatalogId"].ToString();//ItemArray[0].ToString();
                   
                    if (this.GetDenyTable().Select("ObjectId='" + cataId + "'").Length == 0)
                    {
                        if( menuIndex == 0 )
                            html.Append("<li class='on'><strong style='background-color:" + WebUI.UIBiz.CommonInfo.RepeatColor[menuIndex].Split(';')[0] + "'><a href='/ResourceList.aspx?mi=" + menuIndex.ToString() + "&showCata=1&CatalogID=" + cataId + "&showtype=" + dr["showtype"].ToString() + "&cname=" + Server.UrlEncode(dr["catalogName"].ToString()) + "'>");
                        else
                            html.Append("<li><strong style='background-color:" + WebUI.UIBiz.CommonInfo.RepeatColor[menuIndex].Split(';')[0] + "'><a href='/ResourceList.aspx?mi=" + menuIndex.ToString() + "&showCata=1&CatalogID=" + cataId + "&showtype=" + dr["showtype"].ToString() + "&cname=" + Server.UrlEncode(dr["catalogName"].ToString()) + "'>");

                        html.Append(dr["catalogName"].ToString());
                        html.Append("</a></strong>");
                        html.Append("<ul>");

                        GenerateMenu(cataTable, cataId, html, menuIndex);
                        html.Append("</ul>");
                        html.Append("</li>");

                        //AccordionPane topCata = new AccordionPane();
                        //HyperLink hl = new HyperLink();
                        //hl.Text = dr.ItemArray[1].ToString();
                        //hl.NavigateUrl = "~/PicList.aspx?mi=" + menuIndex.ToString() + "&showCata=1&CatalogID=" + cataId;
                        //topCata.HeaderContainer.Controls.Add(hl);
                        //this.cataMenu.Panes.Add(topCata);

                        //GenerateMenu(cataTable, dr, topCata, menuIndex);

                        menuIndex++;
                    }


                }

                //  GetCatalog(cataTable, firstNodes, null,0);

            }

          
        }

        protected string MIndex
        {
            get
            {
                int temp = 0;
                if (!string.IsNullOrEmpty(Request["mi"]))
                {
                    
                    int.TryParse(Request["mi"], out temp);
                    // this.cataMenu.SelectedIndex = temp;
                }

                return temp.ToString();
            }
        }

        protected void GenerateMenu(DataTable cataTable, string parentId, System.Text.StringBuilder html, Int16 menuIndex)
        {
            DataRow[] childNodes = cataTable.Select("parentid='" + parentId + "'", "CatalogOrder");

            foreach (DataRow dr in childNodes)
            {
                string cataId = dr.ItemArray[0].ToString();

                if (this.GetDenyTable().Select("ObjectId='" + cataId + "'").Length == 0)
                {
                    html.Append("<li style='background-color:" + WebUI.UIBiz.CommonInfo.RepeatColor[menuIndex].Split(';')[1] + "'><a href='/ResourceList.aspx?mi=" + menuIndex.ToString() + "&showCata=1&CatalogID=" + cataId + "&showtype=" + dr["showtype"].ToString() + "&cname=" + Server.UrlEncode(dr["catalogName"].ToString()) + "'>");
                    html.Append(dr.ItemArray[1].ToString());
                    html.Append("</a></li>");
                   // menuIndex++;
                }
            }
        }

        //private void GenerateMenu(DataTable cataTable, DataRow parentNode, AccordionPane topPane, Int16 menuIndex)
        //{
        //    string parentId = parentNode.ItemArray[0].ToString();

        //    DataRow[] childNodes = cataTable.Select("parentid='" + parentId + "'", "CatalogOrder");


        //    foreach (DataRow dr in childNodes)
        //    {
        //        string cataId = dr.ItemArray[0].ToString();

        //        if (this.GetDenyTable().Select("ObjectId='" + cataId + "'").Length == 0)
        //        {


        //            HyperLink hl = new HyperLink();
        //            hl.Text = dr.ItemArray[1].ToString();
        //            hl.NavigateUrl = "~/PicList.aspx?mi=" + menuIndex.ToString() + "&showCata=1&CatalogID=" + cataId;
        //            HtmlGenericControl newLine = new HtmlGenericControl("BR");

        //            topPane.ContentContainer.Controls.Add(hl);
        //            topPane.ContentContainer.Controls.Add(newLine);

        //        //    GenerateMenu(cataTable, dr, topPane, menuIndex);

        //            //menuIndex++;
        //        }


        //        //就产生2级 GenerateMenu(cataTable, parentNode, topPane, menuIndex);

        //    }


        //}


        #region  Delete

        /*
        private void GetCatalog(DataTable CTable, DataRow[] Nodes, AccordionPane topPane, Int16 menuIndex)
        {
            bool ShowFlag = false;

           

            for (int i = 0; i < Nodes.Length; i++)
            {

                ShowFlag = this.GetDenyTable().Select("ObjectId='" + Nodes[i].ItemArray[0].ToString() + "'").Length == 0;
                AccordionPane topCata = null;
                HyperLink hl = null;

                if (Nodes[i].ItemArray[2].ToString() == string.Empty)
                {
                    if (ShowFlag)
                    {
                        topCata = new AccordionPane();
                        topCata.ID = i.ToString() + "Cata";
                      
                        hl = new HyperLink();
                        hl.Text = Nodes[i].ItemArray[1].ToString();
                        hl.NavigateUrl = "~/PicList.aspx?mi="+menuIndex.ToString()+"&showCata=1&CatalogID=" + Nodes[i].ItemArray[0].ToString();
                        
                      

                        topCata.HeaderContainer.Controls.Add(hl);

                        this.cataMenu.Panes.Add(topCata);

                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (ShowFlag)
                    {
                        hl = new HyperLink();

                        hl.Text = Nodes[i].ItemArray[1].ToString();
                        hl.NavigateUrl = "~/PicList.aspx?mi=" + menuIndex.ToString() + "&showCata=1&CatalogID=" + Nodes[i].ItemArray[0].ToString();
                        HtmlGenericControl newLine = new HtmlGenericControl("BR");
                        topPane.ContentContainer.Controls.Add(hl);
                        topPane.ContentContainer.Controls.Add(newLine);
                      
                    }

                }

                DataRow[] firstNodesChild = CTable.Select("parentid='" + Nodes[i].ItemArray[0].ToString() + "'", "CatalogOrder");
                GetCatalog(CTable, firstNodesChild, topCata, menuIndex);
                menuIndex++;
            }
        }*/

        #endregion
    }
}