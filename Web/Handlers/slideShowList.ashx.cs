using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;

namespace WebUI.Handlers
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class slideShowList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            
            context.Response.ContentType = "text/plain";
            string strReturn = "";
            try
            {
                //当前收藏夹ID
                string FavoriteID = context.Request.QueryString["FavoriteID"] == null ? "" : context.Request.QueryString["FavoriteID"];
                //当前页码 if(strPageIndex == 0) 为首次打开幻灯片，还需要获取所有的收藏夹，其他情况只获取图片信息。
                string strPageIndex = context.Request.QueryString["PageIndex"] == null ? "0" : context.Request.QueryString["PageIndex"];
                int pageIndex = int.Parse(strPageIndex);

                bool blFirst = false;
                if (pageIndex == 0)
                {
                    blFirst = true;
                    pageIndex = 1;
                }


                if (context.Session["slideShowList"] == null)
                {
                    return;

                }
                DataTable dt = context.Session["slideShowList"] as DataTable;
                

                if (blFirst)
                {
                    string imgCount = dt.Rows.Count.ToString();
                    pageIndex = 1;
                    strReturn = "AllowFullScreen=1&ImgCount=" + imgCount;
                    strReturn += "&";
                }
                //处理图片信息
                strReturn += "ImgValue=";

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        string serverFileName = dr["ServerFileName"].ToString().ToLower();
                        //string serverFolderName = dr["ServerFolderName"].ToString().ToLower();
                        string serverFolderName = dr["ItemID"].ToString().ToLower();
                        strReturn += string.Format("{0}/{1}|", serverFolderName, serverFileName);
                    }
                    strReturn = strReturn.Trim('|');
                }

                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write(strReturn);


            }
            catch (Exception ee)
            {
                throw ee;
            }



        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
