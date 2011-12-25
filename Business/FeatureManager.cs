using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace QJVRMS.Business
{
    class FeatureManager : IFeature
    {
        #region IFeature 成员
        //每个用户的全部专题
        public string GetFeaturesContent(string userName, int pageSize, int pageIndex)
        {
            DataTable dt = GetFeatures(userName, pageSize, pageIndex);
            return GetContent(dt, pageSize, pageIndex, 0, userName);
        }

        public string ShowFeaturesContent(string userName, int pageSize, int pageIndex)
        {
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            int totalRecord = 0;
            DataTable dt = feature.ShowFeatures(userName, pageSize, pageIndex, ref totalRecord);
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            //if (totalRecord > pageSize)
            //    sb.Append(ShowFeaturesPage(pageSize, pageIndex, totalRecord, userName, "pre"));
            sb.Append("<DIV class=\"dir\"><SPAN><SPAN>共检索到 <STRONG>" + totalRecord + "</STRONG> 条数据</SPAN> </SPAN></DIV>");
            sb.Append("<div><OL>");
            ResourceType.ImageType imageType = new QJVRMS.Business.ResourceType.ImageType();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<LI>");
                sb.Append("<DL>");

                sb.Append("<DD>");
                sb.Append("<a href=\"FeatureDetail.aspx?featureId=" + dt.Rows[i]["FeatureId"].ToString() + "&name=" + HttpUtility.UrlEncode(dt.Rows[i]["FeatureName"].ToString()) + "\" target=\"_blank\">");
                string folderName = dt.Rows[i]["FolderName"].ToString();
                if (string.IsNullOrEmpty(folderName))
                    folderName = dt.Rows[i]["Creator"].ToString();
                //yangguang
                //sb.Append("<img src=\"" + imageType.PreviewPath_170_Read + folderName + "/" + dt.Rows[i]["CoverImage"].ToString() + "\" alt=\"\" border=\"0\" /></a>");
                sb.Append("<img src=\"" + imageType.GetPreviewPathRead(folderName, dt.Rows[i]["CoverImage"].ToString(), "170") + "\" alt=\"\" border=\"0\" /></a>");
                sb.Append("</DD>");
                sb.Append("<DT><EM><STRONG>");
                sb.Append(dt.Rows[i]["FeatureName"].ToString());
                sb.Append("</STRONG></EM></DT>");
                sb.Append("<DT><STRONG>");
                string des = dt.Rows[i]["FeatureDes"].ToString();
                des = des.Length > 30 ? des.Substring(0, 30) + "..." : des;
                sb.Append(des);
                sb.Append("</STRONG></DT>");
                sb.Append("<DT>");
                sb.Append("共 <a class=\"small\" href=\"FeatureDetail.aspx?featureId=" + dt.Rows[i]["FeatureId"].ToString() + "&name=" + HttpUtility.UrlEncode(dt.Rows[i]["FeatureName"].ToString()) + "\" target=\"_blank\">");
                sb.Append(dt.Rows[i]["Total"].ToString());
                sb.Append("</a> 个图片");
                sb.Append("</DT>");
                sb.Append("</DL>");
                sb.Append("</LI>");
            }
            sb.Append("</OL></div>");
            if (totalRecord > pageSize)
                sb.Append(ShowFeaturesPage(pageSize, pageIndex, totalRecord, userName, "next"));
            return sb.ToString();
        }
        private string ShowFeaturesPage(int number, int curpage, int total, string userName, string param)
        {
            int totalpage = 0;
            int startcount = 0;
            int endcount = 0;

            if (number != 0)
            {
                totalpage = total / number;
                totalpage = (total % number) != 0 ? totalpage + 1 : totalpage;
                totalpage = totalpage == 0 ? 1 : totalpage;
            }

            startcount = (curpage + 5) > totalpage ? totalpage - 9 : curpage - 4;
            endcount = curpage < 5 ? 10 : curpage + 5;

            if (startcount < 1)
                startcount = 1;

            if (totalpage < endcount)
                endcount = totalpage;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"Pager_Number\" style=\"TEXT-ALIGN: right\">");
            //sb.Append("<div class=\"dirf\">共检索到" + total.ToString() + "条数据</div>");
            //sb.Append("<div class=\"pref\">");
            if (curpage == 1)
                sb.Append("<A style=\"MARGIN-RIGHT: 5px\" disabled><<</A>");
            else
                sb.Append("<a style=\"MARGIN-RIGHT: 5px\" href=\"javascript:ShowFeaturesPage('" + userName + "', '" + number.ToString() + "', '" + (curpage - 1).ToString() + "')\"><<</a>");
            //sb.Append("&nbsp;<strong  style=\"font-weight:bold;\">" + curpage.ToString() + "/" + totalpage.ToString() + "</strong>&nbsp;");
            for (int i = startcount; i <= endcount; i++)
            {
                if (i == curpage)
                    sb.Append("<span style=\"FONT-WEIGHT: bold; COLOR: red; MARGIN-RIGHT: 5px\">" + i.ToString() + "</span>");
                else
                    sb.Append("<a style=\"MARGIN-RIGHT: 5px\" href=\"javascript:ShowFeaturesPage('" + userName + "', '" + number.ToString() + "', '" + i.ToString() + "')\">" + i.ToString() + "</a>");
            }
            if (curpage == totalpage)
                sb.Append("<A style=\"MARGIN-RIGHT: 5px\" disabled>>></A>");
            else
                sb.Append("<a style=\"MARGIN-RIGHT: 5px\" href=\"javascript:ShowFeaturesPage('" + userName + "', '" + number.ToString() + "', '" + (curpage + 1).ToString() + "')\">>></a>");
            //sb.Append("&nbsp;&nbsp;转到第<input type=\"text\" id=\""+param+"Page\" style=\"width:20px;\" />页&nbsp;");
            //sb.Append("<input type=\"button\" value=\"GO\" id=\"bthGo\" onclick=\"GoFeaturesPage('" + userName + "', '" + number.ToString() + "', '" + param + "')\" />");
            sb.Append("&nbsp;&nbsp;转到:<SELECT id=\"goSelect\" onchange=\"GoFeaturesPage('" + userName + "', '" + number.ToString() + "', '" + param + "')\">");
            for (int i = 1; i <= totalpage; i++)
            {
                if (i == curpage)
                    sb.Append("<OPTION value=\"" + i.ToString() + "\" selected>" + i.ToString() + "</OPTION>");
                else
                    sb.Append("<OPTION value=\"" + i.ToString() + "\">" + i.ToString() + "</OPTION>");
            }
            sb.Append("</select>");
            //sb.Append("</div>");
            sb.Append("</div>");

            return sb.ToString();
        }
        private string GetContent(DataTable dt, int pageSize, int pageIndex, int totalRecord, string userName)
        {
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("<table border=\"0\" cellspacing=\"0\" width=\"100%\" class=\"table\">");
            sb.Append("<tr class=\"head\">");
            sb.Append("<td style=\"width:200px\">专题名称</td>");
            sb.Append("<td style=\"width:100px\">用户名</td>");
            sb.Append("<td style=\"width:100px\">图片数量</td>");
            sb.Append("<td style=\"width:150px\">创建时间</td>");
            sb.Append("<td style=\"width:80px\">状态</td>");
            sb.Append("<td>编辑</td>");
            sb.Append("</tr>");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i % 2 == 0)
                        sb.Append("<tr id=\"tr" + dt.Rows[i]["FeatureId"].ToString() + "\" class=\"both\">");
                    else
                        sb.Append("<tr id=\"tr" + dt.Rows[i]["FeatureId"].ToString() + "\" class=\"cell\">");
                    sb.Append("<td>" + dt.Rows[i]["FeatureName"].ToString() + "</td>");
                    sb.Append("<td>" + dt.Rows[i]["Creator"].ToString() + "</td>");
                    sb.Append("<td>" + dt.Rows[i]["Total"].ToString() + "</td>");
                    sb.Append("<td>" + Convert.ToDateTime(dt.Rows[i]["CreateDate"].ToString()).ToString("yyyy-MM-dd") + "</td>");
                    if (dt.Rows[i]["State"].ToString().ToLower() == "true")
                        sb.Append("<td>上线</td>");
                    else
                        sb.Append("<td>下线</td>");
                    sb.Append("<td><a href=\"javascript:GetEdit('" + dt.Rows[i]["FeatureId"].ToString() + "', '" + userName + "')\" >编辑</a></td>");
                    sb.Append("</tr>");
                }
            }
            sb.Append("</table>");
            if (totalRecord > pageSize)
                sb.Append(GetPage(pageSize, pageIndex, totalRecord, userName));
            return sb.ToString();
        }
        private string GetPage(int number, int curpage, int total, string userName)
        {
            int totalpage = 0;
            int startcount = 0;
            int endcount = 0;

            if (number != 0)
            {
                totalpage = total / number;
                totalpage = (total % number) != 0 ? totalpage + 1 : totalpage;
                totalpage = totalpage == 0 ? 1 : totalpage;
            }

            startcount = (curpage + 5) > totalpage ? totalpage - 9 : curpage - 4;
            endcount = curpage < 5 ? 10 : curpage + 5;

            if (startcount < 1)
                startcount = 1;

            if (totalpage < endcount)
                endcount = totalpage;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"grvpager\">");
            for (int i = startcount; i <= endcount; i++)
            {
                if (i == curpage)
                    sb.Append("<span>" + i.ToString() + "</span>");
                else
                    sb.Append("<a href=\"javascript:GetFeaturesPage('" + userName + "', '" + number.ToString() + "', '" + i.ToString() + "')\">" + i.ToString() + "</a>");
            }
            sb.Append("</div>");

            return sb.ToString();
        }

        //单个专题信息
        public string GetFeatureContent(string featureId, string logName)
        {
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            DataTable dt = feature.GetFeature(featureId);

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            ResourceType.ImageType imageType = new QJVRMS.Business.ResourceType.ImageType();
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width=\"100%;\">");
            sb.Append("<tr><td align=\"left\" style=\"width:100px; font-size:13px; font-weight:100;\">编辑专题:</td></tr>");
            sb.Append("<tr><td align=\"left\" style=\"width:100px;\">专题名称：</td><td align=\"left\"><input type=\"text\" id=\"FeatureName\" value=\"" + dt.Rows[0]["FeatureName"] + "\" style=\"width:300px;\" maxlength=\"20\" /><font style=\"color:Red;\">*</font></td></tr>");
            sb.Append("<tr><td align=\"left\" style=\"width:100px;\">专题描述：</td><td align=\"left\"><input type=\"text\" id=\"FeatureDes\" value=\"" + dt.Rows[0]["FeatureDes"] + "\" style=\"width:550px;\"  maxlength=\"150\" /></td></tr>");
            sb.Append("<tr>");
            sb.Append("<td align=\"left\" style=\"width:100px;\">状态：</td>");
            sb.Append("<td align=\"left\">");
            sb.Append("<select id=\"selectupdateid\">");
            if (dt.Rows[0]["State"].ToString() == "True")
            {
                sb.Append("<option value=\"0\">下线</option><option value=\"1\" selected>上线</option>");
            }
            else
            {
                sb.Append("<option value=\"0\" selected>下线</option><option value=\"1\">上线</option>");
            }
            sb.Append("</select>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr><td align=\"left\" style=\"width:100px;\">封面图片：</td>");
            sb.Append("<td align=\"left\">");
            sb.Append("<input type=\"hidden\" id=\"CoverImage\" value=\"" + dt.Rows[0]["CoverImage"] + "\" />");
            //yangguang
            //sb.Append("<img id=\"editImg\" src=\"" + imageType.PreviewPath_170_Read + dt.Rows[0]["Creator"].ToString() + "/" + dt.Rows[0]["CoverImage"].ToString() + "\" alt=\"封面\" />");
            sb.Append("<img id=\"editImg\" src=\"" + imageType.GetPreviewPathRead(dt.Rows[0]["Creator"].ToString(), dt.Rows[0]["CoverImage"].ToString(), "170") + "\" alt=\"封面\" />");
            sb.Append("(在添加的图片中选择本专题封面)");
            sb.Append("</td></tr>");
            sb.Append("<tr><td align=\"left\" style=\"width:100px;\"><input type=\"button\" value=\"保存\"class=\"btn\" style=\" font-size:12px;\" onclick=\"UpdateFeature('" + featureId + "', '" + logName + "')\" /></td><td align=\"left\"></td></tr>");
            sb.Append("</table>");

            return sb.ToString();
        }

        //专题管理的详细信息
        public string GetFeatureImagesContent(string featureId, int type, int pageSize, int pageIndex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style=\"padding:3px 0px 3px 0px;border-bottom:#d4d0c8 solid 1px;\"><input type=\"button\" class=\"btn\" style=\"font-size:12px;\" onclick=\"AddImage('" + featureId + "')\" value=\"添加图片\" /></div>");
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            int totalRecord = 0;
            DataTable dt = feature.GetFeatureImages(new Guid(featureId), type, pageSize, pageIndex, ref totalRecord);
            if (dt == null || dt.Rows.Count == 0)
                return sb.ToString();
            HttpContext.Current.Session["DataResource"] = dt;
            ResourceType.ImageType imageType = new QJVRMS.Business.ResourceType.ImageType();
            sb.Append("<div>");
            sb.Append("<ul class=\"detailClass\">");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<li>");
                sb.Append("<table id=\"" + dt.Rows[i]["FDId"].ToString() + "\" ><tr><td valign=\"bottom\" style=\"height:185px;\">");
                sb.Append("<div>");
                sb.Append("<a href=\"../../PicDetail.aspx?ItemID=" + dt.Rows[i]["ID"].ToString() + "\" target=\"_blank\">");
                //yangguang
                //sb.Append("<img src=\"" + imageType.PreviewPath_170_Read + dt.Rows[i]["ServerFolderName"].ToString()+"/" + dt.Rows[i]["ServerFileName"].ToString() + "\" alt=\"\"  border=\"0\" /><br />");
                sb.Append("<img src=\"" + imageType.GetPreviewPathRead(dt.Rows[i]["ServerFolderName"].ToString(), dt.Rows[i]["ServerFileName"].ToString(), "170") + "\" alt=\"\"  border=\"0\" /><br />");
                sb.Append("</a>");
                sb.Append("</div>");
                sb.Append("</td></tr>");
                sb.Append("<tr><td valign=\"middle\">");
                sb.Append(dt.Rows[i]["Caption"].ToString() + "<a href=\"javascript:Delete('" + dt.Rows[i]["FDId"].ToString() + "')\" ><img src=\"../../image/common/delete.GIF\" alt=\"删除图片\" /></a>");
                //yangguang
                //sb.Append("&nbsp;<a href=\"javascript:ShowConver('" + imageType.PreviewPath_170_Read + dt.Rows[i]["ServerFolderName"].ToString() + "/" + dt.Rows[i]["ServerFileName"].ToString() + "', '" + featureId + "', '" + dt.Rows[i]["ServerFileName"].ToString() + "', '" + dt.Rows[i]["ServerFolderName"].ToString() + "')\" ><img src=\"../../image/common/action_print.gif\" alt=\"设为封面\" /></a>");
                sb.Append("&nbsp;<a href=\"javascript:ShowConver('" + imageType.GetPreviewPathRead(dt.Rows[i]["ServerFolderName"].ToString(), dt.Rows[i]["ServerFileName"].ToString(), "170") + "', '" + featureId + "', '" + dt.Rows[i]["ServerFileName"].ToString() + "', '" + dt.Rows[i]["ServerFolderName"].ToString() + "')\" ><img src=\"../../image/common/action_print.gif\" alt=\"设为封面\" /></a>");
                sb.Append("</td></tr></table>");
                sb.Append("</li>");
            }
            sb.Append("</ul></div>");
            if (totalRecord > pageSize)
                sb.Append(GetImagePage(pageSize, pageIndex, totalRecord, featureId));
            return sb.ToString();
        }
        private string GetImagePage(int number, int curpage, int total, string featureId)
        {
            int totalpage = 0;
            int startcount = 0;
            int endcount = 0;

            if (number != 0)
            {
                totalpage = total / number;
                totalpage = (total % number) != 0 ? totalpage + 1 : totalpage;
                totalpage = totalpage == 0 ? 1 : totalpage;
            }

            startcount = (curpage + 5) > totalpage ? totalpage - 9 : curpage - 4;
            endcount = curpage < 5 ? 10 : curpage + 5;

            if (startcount < 1)
                startcount = 1;

            if (totalpage < endcount)
                endcount = totalpage;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            for (int i = startcount; i <= endcount; i++)
            {
                if (i == curpage)
                    sb.Append("<a style='color:red;padding:2 2 2 2;' >" + i.ToString() + "</a>");
                else
                    sb.Append("<a style='padding:2 2 2 2;' href=\"javascript:GetImagePage('" + featureId + "', '" + number.ToString() + "', '" + i.ToString() + "')\" >" + i.ToString() + "</a>");
            }
            sb.Append("</div>");

            return sb.ToString();
        }
        public DataTable ShowFeatureImages(string featureId, int type, int pageSize, int pageIndex, ref int totalRecord)
        {
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            DataTable dt = feature.GetFeatureImages(new Guid(featureId), type, pageSize, pageIndex, ref totalRecord);

            return dt;
        }
        public string ShowFeatureImagesContent(string featureId, int type, int pageSize, int pageIndex)
        {
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            int totalRecord = 0;
            DataTable dt = feature.GetFeatureImages(new Guid(featureId), type, pageSize, pageIndex, ref totalRecord);
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;
            ResourceType.ImageType imageType = new QJVRMS.Business.ResourceType.ImageType();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<div class=\"single_cover\">");
                sb.Append("<div class=\"cover_img\">");
                sb.Append("<a href=\"../../PicDetail.aspx?ItemID=" + dt.Rows[i]["ID"].ToString() + "\" target=\"_blank\">");
                //yangguang
                //sb.Append("<img src=\"" + imageType.PreviewPath_170_Read + dt.Rows[i]["ServerFolderName"].ToString() + "/" + dt.Rows[i]["ServerFileName"].ToString() + "\" alt=\"\" border=\"0\" />");
                sb.Append("<img src=\"" + imageType.GetPreviewPathRead(dt.Rows[i]["ServerFolderName"].ToString(), dt.Rows[i]["ServerFileName"].ToString(), "170") + "\" alt=\"\" border=\"0\" />");
                sb.Append("</a>");
                sb.Append("</div>");
                sb.Append("<div class=\"cover_txt\">");
                sb.Append("<h5>" + dt.Rows[i]["Caption"].ToString() + "</h5>");
                sb.Append("<p class=\"cover_intro\">" + dt.Rows[i]["Description"].ToString() + "</p>");
                sb.Append("<p class=\"more\"><a href=\"../../PicDetail.aspx?ItemID=" + dt.Rows[i]["ID"].ToString() + "\" target=\"_blank\">预览>></a></p>");
                sb.Append("</div>");
                sb.Append("</div>");
            }
            if (totalRecord > pageSize)
                sb.Append(ShowFeatureImagePage(pageSize, pageIndex, totalRecord, featureId));
            return sb.ToString();
        }
        private string ShowFeatureImagePage(int number, int curpage, int total, string featureId)
        {
            int totalpage = 0;
            int startcount = 0;
            int endcount = 0;

            if (number != 0)
            {
                totalpage = total / number;
                totalpage = (total % number) != 0 ? totalpage + 1 : totalpage;
                totalpage = totalpage == 0 ? 1 : totalpage;
            }

            startcount = (curpage + 5) > totalpage ? totalpage - 9 : curpage - 4;
            endcount = curpage < 5 ? 10 : curpage + 5;

            if (startcount < 1)
                startcount = 1;

            if (totalpage < endcount)
                endcount = totalpage;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div>");
            for (int i = startcount; i <= endcount; i++)
            {
                if (i == curpage)
                    sb.Append("<a style='color:red;padding:2 2 2 2;' >" + i.ToString() + "</a>");
                else
                    sb.Append("<a style='padding:2 2 2 2;' href=\"javascript:ShowImagePage('" + featureId + "', '" + number.ToString() + "', '" + i.ToString() + "')\" >" + i.ToString() + "</a>");
            }
            sb.Append("</div>");

            return sb.ToString();
        }
        //编辑专题信息
        public bool EditFeature(string featureId, string featureName,
            string featureDes, string creator, bool state, string coverImage, string type)
        {
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            return feature.EditFeature(new Guid(featureId), featureName, featureDes,
                creator, state, coverImage, type);
        }

        #region AddImages.aspx
        public string SearchImagesContent(string keyWord, string catalogId, string featureId, int pageSize, int pageNum, string param, string type)
        {
            int pageCount = 0;
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            DataSet ds = feature.SearchResource(keyWord,
                new DateTime(2000, 1, 1, 1, 1, 1).ToString(),
                DateTime.MaxValue.ToString(),
                "00000000-0000-0000-0000-000000000000",
                catalogId, pageSize, pageNum - 1, ref pageCount, "image,other", string.Empty, 2, new Guid(featureId), type);
            if (ds == null && ds.Tables.Count == 0)
                return string.Empty;
            DataTable dt = ds.Tables[1];
            pageCount = string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) ? 0 : int.Parse(ds.Tables[0].Rows[0][0].ToString());
            StringBuilder sb = new StringBuilder();
            if (pageCount > pageSize)
                sb.Append(GetSearchPage(pageSize, pageNum, pageCount, keyWord));
            sb.Append("<div style=\"text-align:right; padding-right:10px;\"><input type=\"checkbox\" name=\"fullname\" onclick=\"OnCheckBox(this)\"/>全选</div>");
            sb.Append("<table width=\"100%\" border=\"0\">");
            sb.Append("<tr>");
            string id = string.Empty;
            ResourceType.ImageType imageType = new QJVRMS.Business.ResourceType.ImageType();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 4 == 0 && i != 0)
                    sb.Append("</tr><tr>");
                id = dt.Rows[i]["ID"].ToString();
                sb.Append("<td class=\"imagetd\" valign=\"bottom\" id=\"" + id + "td\">");
                sb.Append("<table><tr><td valign=\"bottom\">");
                if (param.ToLower().IndexOf(id.ToLower()) != -1)
                {
                    sb.Append("<img id=\"" + id + "img\" style=\"cursor:pointer;\" onmouseover=\"OnMUp('" + id + "')\" onmouseout=\"OnMOut('" + id + "')\"");
                    sb.Append(" onclick=\"AddImage('" + id + "', '" + dt.Rows[i]["Caption"].ToString() + "')\"");
                    //yangguang
                    //sb.Append(" src=\"" + imageType.PreviewPath_170_Read + dt.Rows[i]["ServerFolderName"].ToString() + "/" + dt.Rows[i]["ServerFileName"].ToString() + "\" alt=\"\"/>");
                    sb.Append(" src=\"" + imageType.GetPreviewPathRead(dt.Rows[i]["ServerFolderName"].ToString(), dt.Rows[i]["ServerFileName"].ToString(), "170") + "\" alt=\"\"/>");
                }
                else
                {
                    sb.Append("<img id=\"" + id + "img\" style=\"cursor:pointer;\" onmouseover=\"OnMUp('" + id + "')\" onmouseout=\"OnMOut('" + id + "')\" ");
                    sb.Append("onclick=\"AddImage('" + id + "', '" + dt.Rows[i]["Caption"].ToString() + "')\" ");
                    //yangguang
                    //sb.Append("src=\"" + imageType.PreviewPath_170_Read + dt.Rows[i]["ServerFolderName"].ToString() + "/" + dt.Rows[i]["ServerFileName"].ToString() + "\" alt=\"\"/>");
                    sb.Append("src=\"" + imageType.GetPreviewPathRead(dt.Rows[i]["ServerFolderName"].ToString(), dt.Rows[i]["ServerFileName"].ToString(), "170") + "\" alt=\"\"/>");
                }
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<div>");
                if (param.ToLower().IndexOf(id.ToLower()) != -1)
                    sb.Append("<input type=\"checkbox\" id=\"" + id + "\" name=\"image\" value=\"" + id + "\" checked style=\"display:none;\" />");
                else
                    sb.Append("<input type=\"checkbox\" id=\"" + id + "\" name=\"image\" value=\"" + id + "\" style=\"display:none;\"/>");
                sb.Append("<table width=\"100%\"><tr><td valign=\"middle\" align=\"left\" width=\"90%\">");
                sb.Append("<span style=\"float:left;\">" + dt.Rows[i]["Caption"].ToString() + "</span>");
                sb.Append("</td><td valign=\"middle\" align=\"right\">");
                if (param.ToLower().IndexOf(id.ToLower()) != -1)
                {
                    sb.Append("<img id=\"" + id + "check\" src=\"../../images/swfuploadEnd.gif\">");
                }
                else
                {
                    sb.Append("<img id=\"" + id + "check\" style=\"display:none;\" src=\"../../images/swfuploadEnd.gif\">");
                }
                sb.Append("</td></tr></table>");
                sb.Append("</div>");
                sb.Append("</td></tr></table>");
                sb.Append("</td>");
            }
            sb.Append("</tr>");
            sb.Append("</table>");
            if (pageCount > pageSize)
                sb.Append(GetSearchPage(pageSize, pageNum, pageCount, keyWord));
            sb.Append("<div style=\"text-align:right; padding-right:10px;\"><input type=\"checkbox\" name=\"fullname\" onclick=\"OnCheckBox(this)\"/>全选</div>");
            return sb.ToString();
        }
        private string GetSearchPage(int number, int curpage, int total, string keyWord)
        {
            int totalpage = 0;
            int startcount = 0;
            int endcount = 0;

            if (number != 0)
            {
                totalpage = total / number;
                totalpage = (total % number) != 0 ? totalpage + 1 : totalpage;
                totalpage = totalpage == 0 ? 1 : totalpage;
            }

            startcount = (curpage + 5) > totalpage ? totalpage - 9 : curpage - 4;
            endcount = curpage < 5 ? 10 : curpage + 5;

            if (startcount < 1)
                startcount = 1;

            if (totalpage < endcount)
                endcount = totalpage;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"grvpager\" style=\"padding-left:5px;\">");
            for (int i = startcount; i <= endcount; i++)
            {
                if (i == curpage)
                    sb.Append("<span>" + i.ToString() + "</span>");
                else
                    sb.Append("<a href=\"javascript:GetSearchPage('" + keyWord + "', '" + number.ToString() + "', '" + i.ToString() + "')\">" + i.ToString() + "</a>");
            }

            sb.Append("</div>");

            return sb.ToString();
        }
        public string GetTopCatalogContent()
        {
            DataTable dt = Catalog.GetTopCatalog();
            if (dt == null && dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr><td align=\"left\" valign=\"middle\">");
                sb.Append("<img id=\"image" + dt.Rows[i]["CatalogId"].ToString() + "\" src=\"../../image/common/plus.gif\" alt=\"\" onclick=\"OnChild('" + dt.Rows[i]["CatalogId"].ToString() + "')\" />");
                sb.Append("<a href=\"javascript:OnChild('" + dt.Rows[i]["CatalogId"].ToString() + "');\" style=\"padding-left:5px\">" + dt.Rows[i]["CatalogName"].ToString() + "</a>");
                sb.Append("<div id=\"" + dt.Rows[i]["CatalogId"].ToString() + "\" style=\"padding:2px 0px 2px 20px; display:none;\"></div>");
                sb.Append("</td></tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }
        public string GetChildCatalogContent(string parentId)
        {
            DataTable dt = Catalog.GetCatalogTableByParentId(new Guid(parentId));
            if (dt == null && dt.Rows.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr><td align=\"left\">");
                sb.Append("<a href=\"javascript:OnShow('" + dt.Rows[i]["CatalogId"].ToString() + "')\" >" + dt.Rows[i]["CatalogName"].ToString() + "</a>");
                sb.Append("</td></tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }

        public string CatalogImagesContent(string catalogId, string userId, string featureId, int pageSize, int pageNum, string param, string type)
        {
            int pageCount = 0;
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            DataSet ds = feature.SearchResource(string.Empty,
                new DateTime(2000, 1, 1, 1, 1, 1).ToString(),
                DateTime.MaxValue.ToString(),
                catalogId,
                userId, pageSize, pageNum - 1, ref pageCount, "image,other", string.Empty, 2, new Guid(featureId), type);
            if (ds == null && ds.Tables.Count == 0)
                return string.Empty;
            pageCount = string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()) ? 0 : int.Parse(ds.Tables[0].Rows[0][0].ToString());
            DataTable dt = ds.Tables[1];

            StringBuilder sb = new StringBuilder();
            if (pageCount > pageSize)
                sb.Append(GetCatalogPage(pageSize, pageNum, pageCount, catalogId));
            sb.Append("<div style=\"text-align:right; padding-right:10px;\"><input type=\"checkbox\" name=\"fullname\" onclick=\"OnCheckBox(this)\"/>全选</div>");
            sb.Append("<table width=\"100%\" border=\"0\">");
            sb.Append("<tr>");
            string id = string.Empty;
            ResourceType.ImageType imageType = new ResourceType.ImageType();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i % 4 == 0 && i != 0)
                    sb.Append("</tr><tr>");
                id = dt.Rows[i]["ID"].ToString();
                sb.Append("<td class=\"imagetd\" valign=\"bottom\" id=\"" + id + "td\">");
                sb.Append("<table><tr><td valign=\"bottom\">");
                if (param.ToLower().IndexOf(id.ToLower()) != -1)
                {
                    sb.Append("<img id=\"" + id + "img\" style=\"cursor:pointer;\" ");
                    sb.Append("onmouseover=\"OnMUp('" + id + "')\" onmouseout=\"OnMOut('" + id + "')\" ");
                    sb.Append("onclick=\"AddImage('" + id + "', '" + dt.Rows[i]["Caption"].ToString() + "')\" ");
                    //yangguang
                    //sb.Append("src=\"" + imageType.PreviewPath_170_Read + dt.Rows[i]["ServerFolderName"].ToString() + "/" + dt.Rows[i]["ServerFileName"].ToString() + "\" alt=\"\"/>");
                    sb.Append("src=\"" + imageType.GetPreviewPathRead(dt.Rows[i]["ServerFolderName"].ToString(), dt.Rows[i]["ServerFileName"].ToString(), "170") + "\" alt=\"\"/>");
                }
                else
                {
                    sb.Append("<img id=\"" + id + "img\" style=\"cursor:pointer;\" ");
                    sb.Append("onmouseover=\"OnMUp('" + id + "')\" onmouseout=\"OnMOut('" + id + "')\" ");
                    sb.Append("onclick=\"AddImage('" + id + "', '" + dt.Rows[i]["Caption"].ToString() + "')\" ");
                    //yangguang
                    //sb.Append("src=\"" + imageType.PreviewPath_170_Read + dt.Rows[i]["ServerFolderName"].ToString() + "/" + dt.Rows[i]["ServerFileName"].ToString() + "\" alt=\"\"/>");
                    sb.Append("src=\"" + imageType.GetPreviewPathRead(dt.Rows[i]["ServerFolderName"].ToString(), dt.Rows[i]["ServerFileName"].ToString(), "170") + "\" alt=\"\"/>");
                }
                sb.Append("</td></tr>");
                sb.Append("<tr><td>");
                sb.Append("<div>");
                if (param.ToLower().IndexOf(id.ToLower()) != -1)
                    sb.Append("<input type=\"checkbox\" id=\"" + id + "\" name=\"image\" value=\"" + dt.Rows[i]["Caption"].ToString() + "\" checked style=\"display:none;\" />");
                else
                    sb.Append("<input type=\"checkbox\" id=\"" + id + "\" name=\"image\" value=\"" + dt.Rows[i]["Caption"].ToString() + "\" style=\"display:none;\"/>");
                sb.Append("<table width=\"100%\"><tr><td valign=\"middle\" align=\"left\" width=\"90%\">");
                sb.Append("<span style=\"float:left;\">" + dt.Rows[i]["Caption"].ToString() + "</span>");
                sb.Append("</td><td valign=\"middle\" align=\"right\">");
                if (param.ToLower().IndexOf(id.ToLower()) != -1)
                {
                    sb.Append("<img id=\"" + id + "check\" src=\"../../images/swfuploadEnd.gif\">");
                }
                else
                {
                    sb.Append("<img id=\"" + id + "check\" style=\"display:none;\" src=\"../../images/swfuploadEnd.gif\">");
                }
                sb.Append("</td></tr></table>");
                sb.Append("</div>");
                sb.Append("</td></tr></table>");
                sb.Append("</td>");
            }
            sb.Append("</tr>");
            sb.Append("</table>");
            if (pageCount > pageSize)
                sb.Append(GetCatalogPage(pageSize, pageNum, pageCount, catalogId));
            sb.Append("<div style=\"text-align:right; padding-right:10px;\"><input type=\"checkbox\" name=\"fullname\" onclick=\"OnCheckBox(this)\"/>全选</div>");
            return sb.ToString();
        }
        private string GetCatalogPage(int number, int curpage, int total, string catalogId)
        {
            int totalpage = 0;
            int startcount = 0;
            int endcount = 0;

            if (number != 0)
            {
                totalpage = total / number;
                totalpage = (total % number) != 0 ? totalpage + 1 : totalpage;
                totalpage = totalpage == 0 ? 1 : totalpage;
            }

            startcount = (curpage + 5) > totalpage ? totalpage - 9 : curpage - 4;
            endcount = curpage < 5 ? 10 : curpage + 5;

            if (startcount < 1)
                startcount = 1;

            if (totalpage < endcount)
                endcount = totalpage;

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\"grvpager\" style=\"padding-left:5px;\">");
            for (int i = startcount; i <= endcount; i++)
            {
                if (i == curpage)
                    sb.Append("<span>" + i.ToString() + "</span>");
                else
                    sb.Append("<a href=\"javascript:GetCatalogPage('" + catalogId + "', '" + number.ToString() + "', '" + i.ToString() + "')\">" + i.ToString() + "</a>");
            }
            sb.Append("</div>");

            return sb.ToString();
        }
        #endregion

        //删除一条专题管理详细信息
        public bool DeleteFeatureDetail(string id)
        {
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            return feature.DeleteFeatureDetail(new Guid(id));
        }
        //添加一条专题管理详细信息
        public bool AddFeatureDetail(string featureId, string imageId)
        {
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            return feature.AddFeatureDetail(new Guid(featureId), new Guid(imageId));
        }

        public bool UpdateCoverImage(string featureId, string fileName, string folderName)
        {
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            return feature.UpdateCoverImage(featureId, fileName, folderName);
        }

        /// <summary>
        /// 获取专题列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public DataTable GetFeatures(string userName, int pageSize, int pageIndex)
        {
            QJVRMS.Business.FeatureWS.FeatureService feature = new QJVRMS.Business.FeatureWS.FeatureService();
            int totalRecord = 0;
            DataTable dt = feature.GetFeatures(userName, pageSize, pageIndex, ref totalRecord);
            return dt;
        }
        #endregion
    }

    public class FeatureFactory
    {
        #region FeatureFactory
        IFeature iFeature = null;
        public FeatureFactory()
        {
            iFeature = new FeatureManager();
        }

        public string GetFeaturesContent(string userName, int pageSize, int pageIndex)
        {
            return iFeature.GetFeaturesContent(userName, pageSize, pageIndex);
        }

        public string ShowFeaturesContent(string userName, int pageSize, int pageIndex)
        {
            return iFeature.ShowFeaturesContent(userName, pageSize, pageIndex);
        }

        public string GetFeatureContent(string featureId, string logName)
        {
            return iFeature.GetFeatureContent(featureId, logName);
        }

        public bool EditFeature(string featureId, string featureName, string featureDes,
            string creator, bool state, string coverImage, string type)
        {
            return iFeature.EditFeature(featureId, featureName, featureDes,
                creator, state, coverImage, type);
        }

        public string GetFeatureImagesContent(string featureId, int type,
            int pageSize, int pageIndex)
        {
            return iFeature.GetFeatureImagesContent(featureId, type, pageSize, pageIndex);
        }

        public string ShowFeatureImagesContent(string featureId, int type, int pageSize, int pageIndex)
        {
            return iFeature.ShowFeatureImagesContent(featureId, type, pageSize, pageIndex);
        }

        public string SearchImagesContent(string keyWord, string catalogId, string featureId, int pageSize, int pageNum, string param, string type)
        {
            return iFeature.SearchImagesContent(keyWord, catalogId, featureId, pageSize, pageNum, param, type);
        }

        public bool AddFeatureDetail(string featureId, string imageId)
        {
            return iFeature.AddFeatureDetail(featureId, imageId);
        }

        public string GetTopCatalogContent()
        {
            return iFeature.GetTopCatalogContent();
        }

        public string GetChildCatalogContent(string parentId)
        {
            return iFeature.GetChildCatalogContent(parentId);
        }

        public string CatalogImagesContent(string catalogId, string userId, string featureId, int pageSize, int pageNum, string param, string type)
        {
            return iFeature.CatalogImagesContent(catalogId, userId, featureId, pageSize, pageNum, param, type);
        }

        public bool DeleteFeatureDetail(string id)
        {
            return iFeature.DeleteFeatureDetail(id);
        }

        public bool UpdateCoverImage(string featureId, string fileName, string folderName)
        {
            return iFeature.UpdateCoverImage(featureId, fileName, folderName);
        }

        public DataTable ShowFeatureImages(string featureId, int type, int pageSize, int pageIndex, ref int totalRecord)
        {
            return iFeature.ShowFeatureImages(featureId, type, pageSize, pageIndex, ref totalRecord);
        }

        public DataTable GetFeatures(string userName, int pageSize, int pageIndex)
        {
            return iFeature.GetFeatures(userName, pageSize, pageIndex);
        }
        #endregion
    }
}
