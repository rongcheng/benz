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

namespace WebUI.Modules
{
    public partial class CatalogSelBatch : AuthPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["itemid"]))
                {
                    string itemId = Request["itemid"].ToString();
                    DataTable dt = new Resource().GetResourceCatalogByItemId(itemId).Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        this.cataTree.ArrSelectedCheckBoxValue.Add(dr["CatalogId"].ToString().ToLower());
                    }
                }

            }
        }


        protected void btnSetCata_Click(object sender, EventArgs e)
        {

            //Response.Write(this.hfItemId.Value);
            //return;

            string ids = Request.QueryString["ids"];
            if (string.IsNullOrEmpty(ids))
            {
                return;
            }

            //ids = ids.TrimEnd(";".ToCharArray());
            //string[] arrIds = ids.Split(";".ToCharArray());




            string items = ids.Trim().Trim(";".ToCharArray());
            if (string.IsNullOrEmpty(items))
            {
                ShowMessage("设置分类失败");
                return;
            }
            
            ArrayList selNodes = new ArrayList();


            TreeNode parentNode = cataTree.RootNode;
            //获取checked的节点List
            selNodes = this.cataTree.ArrCheckbox(selNodes, parentNode);


            //QJVRMS.Business.ImageStorageClass imageClass = new QJVRMS.Business.ImageStorageClass();
            Resource objResource = new Resource();

            ArrayList catalogIds = new ArrayList(selNodes.Count);
            foreach (TreeNode node in selNodes)
            {
                // chNode = (TreeNode)(nodeList[i]);
                catalogIds.Add(new Guid(node.Value));

                //imageClass.CreateRelationshipImageAndCatalog(img.ItemId, new Guid(chNode.Value));
            }


            try
            {

                string[] _itemarr = items.Split(";".ToCharArray());
                foreach (string itemId in _itemarr)
                {
                    //objResource.CreateRelationshipResourceAndCatalog(new Guid(Request["itemid"]), (Guid[])catalogIds.ToArray(typeof(Guid)));
                    objResource.CreateRelationshipResourceAndCatalog(new Guid(itemId), (Guid[])catalogIds.ToArray(typeof(Guid)));



                    //更新索引

                    //根据itemId获得SN
                    ResourceEntity model = objResource.GetResourceInfoByItemId(itemId);
                    if (model != null)
                    {
                        string[] SNs = new string[] { model.ItemSerialNum };
                        ResourceIndex.updateIndex(SNs);
                    }


                }

                //imageClass.CreateRelationshipImageAndCatalog(new Guid(Request["itemid"]), (Guid[])catalogIds.ToArray(typeof(Guid)));
                ShowMessage("设置分类成功");

                

            }
            catch
            {
                ShowMessage("设置分类失败");
            }
        }
    }
}
