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
using System.Data.SqlClient;
using System.Text;

namespace WebUI.Modules
{
    public partial class CallbackService : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["fun"]))
                {

                    if (Request["fun"].ToLower() == "afolder")
                    {
                        if (!string.IsNullOrEmpty(Request["itemid"])
                            && !string.IsNullOrEmpty(Request["userId"])
                            && !string.IsNullOrEmpty(Request["path"])
                            && !string.IsNullOrEmpty(Request["serNum"]))
                        {
                            AddToLightBox(new Guid(Request["itemid"]), new Guid(Request["userId"]), Request["path"],Request["serNum"]);
                        }
                    }
                    else if (Request["fun"].ToLower() == "delilb")
                    {
                        DeleteItemFromLightBox(new Guid(Request["userid"]), new Guid(Request["itemId"]));
                    }
                    else if (Request["fun"].ToLower() == "addfavor")
                    {
                        //DeleteItemFromLightBox(new Guid(Request["userid"]), new Guid(Request["itemId"]));
                    }




                    if (Request["fun"].ToLower() == "addcart")
                    {
                        if (!string.IsNullOrEmpty(Request["giftid"]))
                        {
                            string count = string.IsNullOrEmpty(Request["count"]) ? "1" : Request["count"];
                            AddToShoppingCart(Request["giftid"], count);
                        }
                    }
                    if (Request["fun"].ToLower() == "delcart")
                    {
                        if (!string.IsNullOrEmpty(Request["giftid"]))
                        {
                            DeleteFromShoppingCart(Request["giftid"]);
                        }
                    }



                }
            }


        }

        protected void AddToLightBox(Guid imageId, Guid userId,string path,string serNum)
        {

            try
            {
                if (QJVRMS.Business.ImageStorageClass.AddtoLightBox(userId, imageId,path,serNum))
                {
                    Response.Write("true");
                }
                else
                {
                    Response.Write("false");
                }
            }
            catch
            {
                throw new Exception("Adding wrong");
            }

            Response.End();
           
        }

        protected void DeleteItemFromLightBox(Guid userId, Guid itemId)
        {
            try
            {
                if (QJVRMS.Business.ImageStorageClass.DeleteItemOfLightBox(userId, itemId))
                {
                    Response.Write("true");
                }
                else
                {
                    Response.Write("false");
                }
            }
            catch
            {
                throw new Exception("Adding wrong");
            }

            Response.End();
        }



        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="giftId"></param>
        /// <param name="count"></param>
        protected void AddToShoppingCart(string giftId, string count)
        {
            HttpCookie cookie = Request.Cookies.Get("ShoppingCart");
            if (cookie == null)
            {
                cookie = new HttpCookie("ShoppingCart");
                cookie.Expires = DateTime.Now.AddHours(4);
            }

            string giftIds = cookie["GiftId"] == null ? string.Empty : cookie["GiftId"];
            string giftCount = cookie["GiftCount"] == null ? string.Empty : cookie["GiftCount"];

            //如果不存在进行添加
            if (giftIds.IndexOf(giftId) == -1)
            {
                giftIds += giftId + ",";
                giftCount += count + ",";

                cookie["GiftId"] = giftIds;
                cookie["GiftCount"] = giftCount;

                Response.Cookies.Add(cookie);

                Response.Write("true");
            }
            else
            {
                Response.Write("false");
            }
            Response.End();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="giftId"></param>
        protected void DeleteFromShoppingCart(string giftId)
        {
            HttpCookie cookie = Request.Cookies.Get("ShoppingCart");
            if (cookie == null)
            {
                Response.Write("false");
            }
            else
            {
                if (string.IsNullOrEmpty(cookie["GiftId"]) || string.IsNullOrEmpty(cookie["GiftCount"]))
                {
                    Response.Write("false");
                }
                else
                {
                    string[] giftIds = cookie["GiftId"].Split(new char[] { ',' });
                    string[] giftCounts = cookie["GiftCount"].Split(new char[] { ',' });

                    StringBuilder sbIdsNew = new StringBuilder();
                    StringBuilder sbCountNew = new StringBuilder();

                    for (int i = 0; i < giftIds.Length; i++)
                    {
                        //只要不是删除的重新保存，以方便出现批量删除的情况
                        if (giftId != giftIds[i])
                        {
                            sbIdsNew.Append(giftIds[i] + ",");
                            sbCountNew.Append(giftCounts[i] + ",");
                        }
                    }

                    cookie["GiftId"] = sbIdsNew.ToString();
                    cookie["GiftCount"] = sbCountNew.ToString();

                    Response.Cookies.Add(cookie);
                    Response.Write("true");
                }
            }
            Response.End();
        }

    }
}
