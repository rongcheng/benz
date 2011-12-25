using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using QJVRMS.Business;
using System.IO;
using QJVRMS.Business.ResourceType;
using QJVRMS.Common;
using QJVRMS.Business.SecurityControl;

namespace WebUI {
    public partial class PicDetail : AuthPage {
        protected string folder = string.Empty;
        public string folderName = string.Empty;
        public string serviceFileName = string.Empty;
        public string preId = string.Empty;
        public string nextId = string.Empty;
        public string id = string.Empty;
        public string guid = string.Empty;

        protected void Page_Load(object sender, EventArgs e) {
            try {
                id = new Guid(Request["ItemId"]).ToString();
                string type = get_LinkParam("type");
                guid = get_LinkParam("guid");
                if (string.IsNullOrEmpty(id)) {
                    Response.Write("<script language='javascript'>alert('不存在此图片或您没有权限浏览!');window.close();</script>");
                    Response.End();
                }
                GetImageInfo(new Guid(id));



                

                //缓存所有的记录试一下

                if (string.IsNullOrEmpty(type)) {







                    


                    if (Session["DataResource"] != null) {
                        guid = Guid.NewGuid().ToString().Replace("-", string.Empty);
                        Session[guid] = (DataTable)Session["DataResource"];

                        //Session[guid+"sc"]





                        if (Session["searchCondition"] != null)
                        {
                            ResourceEntity.SearchCondition sc = (ResourceEntity.SearchCondition)Session["searchCondition"];
                            if (sc.Type == ResourceEntity.SearchContidionType.BySearch)
                            {

                                DateTime beg, end;
                                Guid cataId;
                                try
                                {
                                    beg = DateTime.Parse(sc.GetValue("beg"));
                                    end = DateTime.Parse(sc.GetValue("end"));
                                }
                                catch
                                {
                                    beg = new DateTime(2000, 1, 1, 1, 1, 1);
                                    end = DateTime.MaxValue;
                                }

                                try
                                {
                                    cataId = new Guid(sc.GetValue("cataId"));
                                }
                                catch
                                {
                                    cataId = Guid.Empty;
                                }

                                int pageSize = 1;
                                int pageIndex = 1;
                                int pageCount = 1;
                                string resourceType;
                                string groupId;
                                string fileExt, fileWH;
                                string keyword;

                                int.TryParse(sc.GetValue("pageSize"), out pageSize);
                                int.TryParse(sc.GetValue("pageIndex"), out pageIndex);
                                resourceType = sc.GetValue("resourceType");
                                groupId = sc.GetValue("groupId");
                                fileExt = sc.GetValue("fileExt");
                                fileWH = sc.GetValue("fileWH");
                                keyword = sc.GetValue("keyword");


                                //先得到总的记录数
                                DataSet ds;
                                int rowCount = 0;

                                ds = QJVRMS.Business.Resource.Search(keyword, beg.ToString(), end.ToString(), cataId.ToString(), CurrentUser.UserId.ToString(), 1, 1, ref pageCount, resourceType, groupId, fileExt, fileWH);
                                int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out rowCount);


                                //再把所有的取出来放到session当中
                                ds = QJVRMS.Business.Resource.Search(keyword, beg.ToString(), end.ToString(), cataId.ToString(), CurrentUser.UserId.ToString(), rowCount, 0, ref pageCount, resourceType, groupId, fileExt, fileWH);
                                //ds = QJVRMS.Business.Resource.Search(keyword, beg.ToString(), end.ToString(), cataId.ToString(), CurrentUser.UserId.ToString(), pageSize, pageIndex, ref pageCount, resourceType, groupId, fileExt, fileWH);

                                //Session["DataResource"] = ds.Tables[1];
                                Session[guid] = ds.Tables[1];
                            }
                            else if (sc.Type == ResourceEntity.SearchContidionType.ByOrder)
                            {
                              //这个就是全部
                            }
                            else if (sc.Type == ResourceEntity.SearchContidionType.ByIsHot)
                            {
                                DateTime beg, end;
                                try
                                {
                                    beg = DateTime.Parse(sc.GetValue("beg"));
                                    end = DateTime.Parse(sc.GetValue("end"));
                                }
                                catch
                                {
                                    beg = new DateTime(2000, 1, 1, 1, 1, 1);
                                    end = DateTime.MaxValue;
                                }

                                DataSet ds;
                                int rowCount = 0;
                                int pageSize = 1;
                                int pageIndex = 1;
                                string resourceType;

                                resourceType = sc.GetValue("resourceType");



                                ds = new Resource().GetResourcesByViewCount(beg, end, 1, 1, resourceType);
                                int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out rowCount);


                                ds = new Resource().GetResourcesByViewCount(beg, end,rowCount, 0, resourceType);

                                Session[guid] = ds.Tables[1];
                            
                            }
                            else if (sc.Type == ResourceEntity.SearchContidionType.ByIsDownloadHot)
                            {
                                DateTime beg, end;
                                try
                                {
                                    beg = DateTime.Parse(sc.GetValue("beg"));
                                    end = DateTime.Parse(sc.GetValue("end"));
                                }
                                catch
                                {
                                    beg = new DateTime(2000, 1, 1, 1, 1, 1);
                                    end = DateTime.MaxValue;
                                }

                                DataSet ds;
                                int rowCount = 0;
                                int pageSize = 1;
                                int pageIndex = 1;
                                string resourceType;

                                resourceType = sc.GetValue("resourceType");



                                ds = new Resource().GetResourcesByDownloadCount(beg, end, 1, 1, resourceType);
                                int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out rowCount);


                                ds = new Resource().GetResourcesByDownloadCount(beg, end, rowCount, 0, resourceType);

                                Session[guid] = ds.Tables[1];

                                
                            }

                            Session[guid + "sc"] = Session["searchCondition"];
                        }



                    }

                    
                    
                }

                if (Session[guid] != null) {
                    DataTable dt = (DataTable)Session[guid];
                    if (dt == null || dt.Rows.Count == 0) {
                        Response.Write("<script language='javascript'>alert('不存在此图片或您没有权限浏览!');window.close();</script>");
                        Response.End();
                    }

                    StringBuilder sb = new StringBuilder("<ul>");
                    int total = dt.Rows.Count;
                    int rowid = 1;
                    List<DetailInfo> list = new List<DetailInfo>();
                    for (int i = 0; i < total; i++) {
                        string itemid = dt.Rows[i]["ID"].ToString();
                        if (id == itemid) {
                            rowid = i + 1;
                            DetailInfo entity = new DetailInfo();
                            entity.ID = i + 1;
                            entity.ItemId = itemid;
                            entity.FolderName = dt.Rows[i]["ServerFolderName"].ToString();
                            entity.ServerFileName = dt.Rows[i]["serverfilename"].ToString();
                            list.Add(entity);
                        }
                        else {
                            DetailInfo entity = new DetailInfo();
                            entity.ID = i + 1;
                            entity.ItemId = itemid;
                            entity.FolderName = dt.Rows[i]["ServerFolderName"].ToString();
                            entity.ServerFileName = dt.Rows[i]["serverfilename"].ToString();
                            list.Add(entity);
                        }
                        
                    }
                    if (total > 1) {
                        if (rowid == 1) {
                            preId = list[total - 1].ItemId;
                            nextId = list[rowid].ItemId;
                        }
                        else if (rowid == total) {



                            preId = list[total - 2].ItemId;
                            nextId = list[0].ItemId;
                        }
                        else {
                            preId = list[rowid - 2].ItemId;
                            nextId = list[rowid].ItemId;
                        }
                    }
                    if (total <= 10) {
                        sb.Append(BuildString(list, id, 1, total, guid));
                    }
                    else {
                        int startcount = (rowid + 5) > total ? total - 9 : rowid - 4;
                        int endcount = rowid < 5 ? 10 : rowid + 5;

                        if (startcount < 1)
                            startcount = 1;

                        if (total < endcount)
                            endcount = total;

                        sb.Append(BuildString(list, id, startcount, endcount, guid));
                    }
                    sb.Append("</ul>");
                    this.Content.InnerHtml = sb.ToString();
                 }   
            }
            catch (Exception ex) {
                LogWriter.WriteExceptionLog(ex);
                Response.Write("<script language='javascript'>alert('不存在此图片或您没有权限浏览!');window.close();</script>");
                Response.End();
            }
        }
        private string BuildString(List<DetailInfo> list, string itemId, int s, int e, string guid) {
            ImageType obj = new ImageType();
            //yangguang
            //string yRootPath = obj.PreviewPath_170_Read; // 400预览图路径@"e:\\DSC_3257.jpg"
            string path = string.Empty;
            string hvsp = string.Empty;
            string id = string.Empty;

            StringBuilder sb = new StringBuilder();
            for (int i = s - 1; i < e; i++) {
                //yangguang
                //path = yRootPath + list[i].FolderName + @"/" + list[i].ServerFileName;
                path = obj.GetPreviewPathRead(list[i].FolderName, list[i].ServerFileName, "170");
                id = list[i].ItemId;
                if (id == itemId) {
                    sb.Append("<li>");
                    sb.Append("<a href=\"PicDetail.aspx?type=1&ItemID=" + id + "&guid=" + guid + "\"><img src=\"" + path + "\" class=\"liimg1\" /></a>");
                    sb.Append("</li>");
                }
                else {
                    sb.Append("<li>");
                    sb.Append("<a href=\"PicDetail.aspx?type=1&ItemID=" + id + "&guid=" + guid + "\"><img src=\"" + path + "\" class=\"liimg\" /></a>");
                    sb.Append("</li>");
                }
            }

            return sb.ToString();
        }

        protected void GetImageInfo(Guid itemId) {
            Resource rs = new Resource();
            ResourceEntity r = rs.GetResourceInfoByItemId(itemId.ToString());

            //更新浏览次数
            rs.UpdateResourceViewCount(itemId.ToString());


            ImageType obj = new ImageType();
            //yangguang
            //string yRootPath = obj.PreviewPath_400_Read; // 400预览图路径
            folderName = r.FolderName;
            serviceFileName = r.ServerFileName;
            //yRootPath = yRootPath + r.FolderName + @"/" + r.ServerFileName;
            string yRootPath = obj.GetPreviewPathRead(r.FolderName, r.ServerFileName, "400");
            folder = r.FolderName;

            //this.imgsrc.Src = yRootPath;
            this.lb_Description.Text = GetString(r.Description, 25);
            this.lb_FileName.Text = r.FileName;
            this.lb_Caption.Text = GetString(r.Caption, 25);
            this.lb_Author.Text = r.Author;
            this.lb_fileLength.Text = QJVRMS.Common.Tool.toFileSize(r.FileSize);
            this.lb_ImageType.Text = Path.GetExtension(r.FileName);
            this.lb_ItemSerialNum.Text = r.ItemSerialNum;
            this.lb_Keyword.Text = r.Keyword;
            this.lb_shotDate.Text = r.shotDate.ToString("yyyy-MM-dd");
            this.lb_uploadDate.Text = r.uploadDate.ToString("yyyy-MM-dd");
            this.pageTitle.Text = r.Caption;
            this.lb_viewCount.Text = r.ViewCount.ToString();
            string enableDate = "";
            if (r.StartDate.ToString("yyyy-MM-dd") != "1900-01-01") {
                enableDate += r.StartDate.ToString("yyyy-MM-dd");
            }
            enableDate += " -- ";
            if (r.EndDate.ToString("yyyy-MM-dd") != "1900-01-01") {
                enableDate += r.EndDate.ToString("yyyy-MM-dd");
            }

            this.lb_SN.Text = r.ItemSerialNum;
            //if (enableDate != " -- ") {
            //    this.lb_enableDate.Text = enableDate;
            //}

            //if (r.HasCopyright == 1) {
            //    this.pSource.Visible = false;
            //}
            //else {
            //    this.pSource.Visible = true;
            //}

            if (r.ResourceType.Equals("image")) {
                ImageInfo o = rs.GetImageInfoBySN(r.ItemSerialNum);
                if (o != null) {
                    switch (o.Hvsp.ToUpper()) {
                        case "H": this.lb_Hvsp.Text = "横图"; break;
                        case "V": this.lb_Hvsp.Text = "竖图"; break;
                        case "S": this.lb_Hvsp.Text = "方图"; break;
                        case "P": this.lb_Hvsp.Text = "全景图"; break;
                        default: this.lb_Hvsp.Text = "横图"; break;
                    }
                    this.lb_Size.Text = o.Width.ToString() + "x" + o.Height.ToString();
                }
            }

            StringBuilder OutString = new StringBuilder("");
            using (DataSet ds = GetResourceCatalog(itemId.ToString()))
            {
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    DataTable cataTable = ds.Tables[0];

                    for (int i = 0; i < cataTable.Rows.Count; i++)
                    {
                        OutString.Append(cataTable.Rows[i]["CatalogName"].ToString() + "   ");
                    }

                    this.lb_Category.Text = OutString.ToString();

                }
            }



            //判断是否具有下载权限
            this.pDownload.Visible = IsSuperAdmin || rs.IsUserResource(CurrentUser.UserId, r.ItemId, (int)OperatorMethod.Download);
            this.pEdit.Visible = IsSuperAdmin || rs.IsUserResource(CurrentUser.UserId, r.ItemId, (int)OperatorMethod.Modify);
        }

        private DataSet GetResourceCatalog(string itemid)
        {
            return new Resource().GetResourceCatalogByItemId(itemid);
        }
        private string GetString(string des, int n) {
            string result = string.Empty;
            int number = des.Length / n;
            if (des.Length % n > 0) {
                number++;
            }
            string temp = des;
            for (int i = 1; i <= number;i++ ) {
                if (i == number) {
                    result += temp;
                }
                else {
                    result += temp.Substring(0, n)+"\r\n";
                    temp = temp.Substring(n, temp.Length - n);
                }
            }

            return result;
        }
        private string get_LinkParam(string paramname) {
            string paramcontent = string.Empty;

            switch (Request.RequestType) {
                case "POST":
                    if (Request.Form[paramname] != null && Request.Form[paramname].ToString() != string.Empty) {
                        paramcontent = Request.Form[paramname].ToString();
                    }
                    break;
                case "GET":
                    if (Request.QueryString[paramname] != null && Request.QueryString[paramname].ToString() != string.Empty) {
                        paramcontent = HttpUtility.UrlDecode(Request.QueryString[paramname].ToString());
                    }
                    break;
            }

            return paramcontent.Trim();
        }
    }

    public class DetailInfo {
        public int ID { get; set; }
        public string ItemId { get; set; }
        public string FolderName { get; set; }
        public string ServerFileName { get; set; }
    }
}
