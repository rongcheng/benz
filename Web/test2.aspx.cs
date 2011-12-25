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

namespace WebUI
{
    public partial class test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("/xml/IndexFlashImages.xml"));


            //修改
            DataTable dt = ds.Tables["Config"];
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Rows[0][0] = "/ui/flash/images/";
            }



            //删除

            DataTable dt1 = ds.Tables["DefaultImage"];
            //DataRow[] drs = dt1.Select("src='d'");
            //if (dt1 != null && dt1.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in drs)
            //    {
            //        dr.Delete();
            //    }
            //}


            //加入一行
            DataRow newDr = dt1.NewRow();
            newDr["id"] = Guid.NewGuid().ToString();
            newDr["src"] = "1";
            newDr["link"] = "";
            newDr["description"] = "三一图片管理系统";
            newDr["order"] = "1";
            newDr["status"] = "1";

            dt1.Rows.Add(newDr);


            newDr = dt1.NewRow();
            newDr["id"] = Guid.NewGuid().ToString();
            newDr["src"] = "2";
            newDr["link"] = "";
            newDr["description"] = "三一图片管理系统";
            newDr["order"] = "2";
            newDr["status"] = "1";

            dt1.Rows.Add(newDr);


            newDr = dt1.NewRow();
            newDr["id"] = Guid.NewGuid().ToString();
            newDr["src"] = "3";
            newDr["link"] = "";
            newDr["description"] = "三一图片管理系统";
            newDr["order"] = "3";
            newDr["status"] = "1";

            dt1.Rows.Add(newDr);


            newDr = dt1.NewRow();
            newDr["id"] = Guid.NewGuid().ToString();
            newDr["src"] = "4";
            newDr["link"] = "";
            newDr["description"] = "三一图片管理系统";
            newDr["order"] = "4";
            newDr["status"] = "1";

            dt1.Rows.Add(newDr);


            newDr = dt1.NewRow();
            newDr["id"] = Guid.NewGuid().ToString();
            newDr["src"] = "5";
            newDr["link"] = "";
            newDr["description"] = "三一图片管理系统";
            newDr["order"] = "5";
            newDr["status"] = "1";

            dt1.Rows.Add(newDr);

            ds.WriteXml(Server.MapPath("/xml/IndexFlashImages.xml"));

        }
    }
}
