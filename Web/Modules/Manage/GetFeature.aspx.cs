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
using System.IO;
using System.Text;
using System.Xml;

namespace WebUI.Modules.Manage {
    public partial class GetFeature : System.Web.UI.Page {
        const string TEMPPATH = "../../xml/temp.gif";
        const string SAVEPATH = "../../xml/water.gif";
        const string XMLPATH = "../../xml/mark.xml";
        const string SYSTEMPATH = "../../xml/System.xml";
        const string DEFAULTPATH = "../../xml/default.gif";

        protected void Page_Load(object sender, EventArgs e) {
            string result = string.Empty;
            
            string type = get_LinkParam("type");
            string name = string.Empty;
            string featureId = string.Empty;
            
            QJVRMS.Business.FeatureFactory featureFactory = new QJVRMS.Business.FeatureFactory();
            
            switch (type) {
                case "Single"://获取一个专题信息
                    featureId = get_LinkParam("featureId");
                    name = get_LinkParam("name");
                    result = featureFactory.GetFeatureContent(featureId, name);
                    break;
                case "Page"://分页
                    name = get_LinkParam("name");
                    int pageSize = int.Parse(get_LinkParam("size"));
                    int pageIndex = int.Parse(get_LinkParam("index"));
                    result = featureFactory.GetFeaturesContent(name, pageSize, pageIndex);
                    break;
                case "Show":
                    name = get_LinkParam("name");
                    int sSize = int.Parse(get_LinkParam("size"));
                    int sIndex = int.Parse(get_LinkParam("index"));
                    result = featureFactory.ShowFeaturesContent(name, sSize, sIndex);
                    break;
                case "Update":
                    name = get_LinkParam("name");
                    featureId = get_LinkParam("featureId");
                    string featureName = get_LinkParam("featureName");
                    string featureDes = get_LinkParam("des");
                    string coverImage = get_LinkParam("cover");
                    bool bUpdate = get_LinkParam("state") == "0" ? false : true;
                    if(featureFactory.EditFeature(featureId, featureName, featureDes, name, bUpdate, coverImage, type))
                        result = "更新成功";
                    else
                        result = "更新失败";
                    break;
                case "Add":
                    name = get_LinkParam("name");
                    featureId = Guid.NewGuid().ToString();
                    string fName = get_LinkParam("featureName");
                    string fDes = get_LinkParam("des");
                    string cImage = get_LinkParam("cover");
                    bool bAdd = get_LinkParam("state") == "0" ? false : true;
                    if (featureFactory.EditFeature(featureId, fName, fDes, name, bAdd, cImage, type))
                        result = "增加成功;" + featureId;
                    else
                        result = "增加失败";
                    break;
                case "Image":
                    featureId = get_LinkParam("featureId");
                    int size = int.Parse(get_LinkParam("size"));
                    int index = int.Parse(get_LinkParam("index"));
                    result = featureFactory.GetFeatureImagesContent(featureId, 0, size, index);
                    break;
                case "Detail":
                    featureId = get_LinkParam("featureId");
                    int dSize = int.Parse(get_LinkParam("size"));
                    int dIndex = int.Parse(get_LinkParam("index"));
                    result = featureFactory.ShowFeatureImagesContent(featureId, 0, dSize, dIndex);
                    break;
                case "Search":
                    string keyword = get_LinkParam("search");
                    int ssize = int.Parse(get_LinkParam("size"));
                    int sindex = int.Parse(get_LinkParam("index"));
                    string id = get_LinkParam("id");
                    featureId = get_LinkParam("featureId");
                    string sparam = get_LinkParam("param");
                    string st = get_LinkParam("t");
                    result = featureFactory.SearchImagesContent(keyword, id, featureId, ssize, sindex, sparam, st);
                    break;
                case "Save":
                    featureId = get_LinkParam("featureId");
                    string param = get_LinkParam("param");
                    string t = get_LinkParam("t");
                    if (t == "order") {
                        string[] ss = param.Split(';');
                        QJVRMS.Business.Orders orders = new QJVRMS.Business.Orders();
                        orders.AddResourceToOrders(featureId, ss);
                    }
                    else {
                        foreach (string imageId in param.Split(';')) {
                            if (!string.IsNullOrEmpty(imageId))
                                featureFactory.AddFeatureDetail(featureId, imageId);
                        }
                    }
                    result = "成功";
                    break;
                case "Top":
                    result = featureFactory.GetTopCatalogContent();
                    break;
                case "Child":
                    string parentId = get_LinkParam("parentId");
                    result = featureFactory.GetChildCatalogContent(parentId);
                    break;
                case "Catalog":
                    string catalogId = get_LinkParam("catalogId");
                    int csize = int.Parse(get_LinkParam("size"));
                    int cindex = int.Parse(get_LinkParam("index"));
                    string cid = get_LinkParam("id");
                    featureId = get_LinkParam("featureId");
                    string cparam = get_LinkParam("param");
                    string ct = get_LinkParam("t");
                    result = featureFactory.CatalogImagesContent(catalogId, cid, featureId, csize, cindex, cparam, ct);
                    break;
                case "Delete":
                    string fid = get_LinkParam("id");
                    if (featureFactory.DeleteFeatureDetail(fid))
                        result = fid;
                    break;
                case "NewWater":
                    string showType = get_LinkParam("show");
                    try {
                        string temppath = Server.MapPath(TEMPPATH);
                        string newshow = string.Empty;
                        if (File.Exists(temppath)) {
                            if (UpdateWaterImage(temppath, ref newshow))
                                File.Delete(temppath);
                        }
                        string xmlPath = Server.MapPath(XMLPATH);
                        if (File.Exists(xmlPath))
                            GetXml(showType, xmlPath);
                        result = "保存设置成功|" + newshow;
                    }
                    catch {
                        result = string.Empty;
                    }
                    break;
                case "default":
                    string dType = get_LinkParam("show");
                    try {
                        string dpath = Server.MapPath(DEFAULTPATH);
                        string dshow = string.Empty;
                        if (File.Exists(dpath)) {
                            UpdateWaterImage(dpath, ref dshow);
                        }
                        string xPath = Server.MapPath(XMLPATH);
                        if (File.Exists(xPath))
                            GetXml(dType, xPath);
                        result = "保存默认设置成功|" + dshow;
                    }
                    catch {
                        result = string.Empty;
                    }
                    break;
                case "OldWater":
                    string sType = get_LinkParam("show");
                    try {
                        //string savepath = Server.MapPath(SAVEPATH);
                        string oldshow = string.Empty;
                        //if (File.Exists(savepath)) {
                        //    UpdateWaterImage(savepath, ref oldshow);
                        //}
                        string xPath = Server.MapPath(XMLPATH);
                        if (File.Exists(xPath))
                            GetXml(sType, xPath);
                        result = "保存设置成功";
                    }
                    catch {
                        result = string.Empty;
                    }
                    break;
                case "Cover":
                    featureId = get_LinkParam("featureId");
                    name = get_LinkParam("name");
                    string src = get_LinkParam("src");
                    string folderName = get_LinkParam("foldername");
                    if (featureFactory.UpdateCoverImage(featureId, name, folderName)) {
                        result = src + ";" + name;
                    }
                    break;
                case "System":
                    string msmtp = get_LinkParam("s");
                    string mname = get_LinkParam("n");
                    string mpass = get_LinkParam("p");
                    string mport = get_LinkParam("t");
                    string mfrom = get_LinkParam("from");
                    string mto = get_LinkParam("to");
                    name = get_LinkParam("u");
                    if (SaveXml(Server.MapPath(SYSTEMPATH), msmtp, mname, mpass, mport, mfrom, mto, name)) {
                        result = "保存设置成功";
                    }
                    else {
                        result = "保存设置成功";
                    }
                    break;
                case "history":
                    string c = get_LinkParam("c");
                    string f = get_LinkParam("t");
                    if(string.IsNullOrEmpty(c)){
                        result = BuildString(Server.MapPath(SYSTEMPATH));
                    }
                    else {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(Server.MapPath(SYSTEMPATH));
                        XmlNodeList list = doc.SelectNodes("Root/LogItem");
                        if (list.Count != 0) {
                            if (f == "next") {
                                if (c != list.Count.ToString()) {
                                    c = (int.Parse(c) + 1).ToString();
                                }
                                else {
                                    c = "1";
                                }
                            }
                            else if (f == "pre") {
                                if (c == "1") {
                                    c = list.Count.ToString(); ;
                                }
                                else {
                                    c = (int.Parse(c) - 1).ToString();
                                }
                            }
                            result = BuildString(doc, c);
                        }
                    }
                    if (string.IsNullOrEmpty(result))
                        result = "<span style='color:red;'>没有历史记录</span>";
                    break;
            }

            Response.Write(result);
            Response.End();
        }
        private string BuildString(string path) {
            XmlDocument doc = null;
            try {
                doc = new XmlDocument();
                doc.Load(path);
                StringBuilder sb = new StringBuilder();
                if (doc != null) {
                    XmlNodeList list = doc.SelectNodes("Root/LogItem");
                    if (list.Count == 0)
                        return string.Empty;
                    int i = 1;
                    int num = 1;
                    foreach (XmlNode n in list) {
                        int t = int.Parse(n.Attributes["id"].Value);
                        if (t > num) {
                            num = t;
                        }
                        if (i == list.Count) {
                            XmlNode xn = list[num - 1];
                            sb.Append("<table width=\"550px\" style=\"border:solid 1px #d4d0c8;padding:5px 5px 5px 5px\">");
                            sb.Append("<tr style=\"background-color:#fbfbfb;\">");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\" width=\"110px\">发送邮件服务器：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\" width=\"130px\">");
                            sb.Append("<div id=\"dSmtpId\">" + xn.Attributes["host"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">用户名：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\" >");
                            sb.Append("<div id=\"dUsernameId\">" + xn.Attributes["userName"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr style=\"background-color:#f3f3f3;\">");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">密码：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\"  width=\"130px\">");
                            sb.Append("<div id=\"dPassId\">" + xn.Attributes["password"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">端口：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\" >");
                            sb.Append("<div id=\"dPortId\">" + xn.Attributes["port"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr style=\"background-color:#fbfbfb;\">");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">发件人邮箱：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\"  width=\"130px\">");
                            sb.Append("<div id=\"dFromId\">" + xn.Attributes["from"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">订单处理人邮箱：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\" >");
                            sb.Append("    <div id=\"dToId\">" + xn.Attributes["to"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr style=\"background-color:#f3f3f3;\">");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">修改时间：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\"  width=\"130px\">");
                            sb.Append("    <div id=\"dMarkId\">" + xn.Attributes["date"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\" >修改人：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\" >");
                            sb.Append("<div id=\"dPersonId\">" + xn.Attributes["name"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr style=\"background-color:#fbfbfb;\">");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" colspan=\"4\" align=\"center\">");
                            sb.Append("<a href=\"javascript:OnPre('" + xn.Attributes["id"].Value + "');\" >上一条</a>");
                            sb.Append("&nbsp;&nbsp;");
                            sb.Append("<a href=\"javascript:OnNext('" + xn.Attributes["id"].Value + "');\">下一条</a>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                            break;
                        }
                        i++;
                    }
                }
                return sb.ToString();
            }
            catch {
                return string.Empty;
            }
        }
        private string BuildString(XmlDocument doc, string id) {
            try {
                StringBuilder sb = new StringBuilder();
                if (doc != null) {
                    XmlNodeList list = doc.SelectNodes("Root/LogItem");
                    if (list.Count == 0)
                        return string.Empty;
                    int i = 1;
                    int num = 1;
                    foreach (XmlNode n in list) {
                        if (n.Attributes["id"].Value == id) {
                            sb.Append("<table width=\"550px\" style=\"border:solid 1px #d4d0c8;padding:5px 5px 5px 5px\">");
                            sb.Append("<tr style=\"background-color:#fbfbfb;\">");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\" width=\"110px\">发送邮件服务器：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\" width=\"130px\">");
                            sb.Append("<div id=\"dSmtpId\">" + n.Attributes["host"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">用户名：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\" >");
                            sb.Append("<div id=\"dUsernameId\">" + n.Attributes["userName"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr style=\"background-color:#f3f3f3;\">");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">密码：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\"  width=\"130px\">");
                            sb.Append("<div id=\"dPassId\">" + n.Attributes["password"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">端口：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\" >");
                            sb.Append("<div id=\"dPortId\">" + n.Attributes["port"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr style=\"background-color:#fbfbfb;\">");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">发件人邮箱：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\"  width=\"130px\">");
                            sb.Append("<div id=\"dFromId\">" + n.Attributes["from"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">订单处理人邮箱：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\" >");
                            sb.Append("    <div id=\"dToId\">" + n.Attributes["to"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr style=\"background-color:#f3f3f3;\">");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\"  width=\"110px\">修改时间：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\"  width=\"130px\">");
                            sb.Append("    <div id=\"dMarkId\">" + n.Attributes["date"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"right\" valign=\"middle\" >修改人：</td>");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" align=\"left\" valign=\"middle\" >");
                            sb.Append("<div id=\"dPersonId\">" + n.Attributes["name"].Value + "</div>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr style=\"background-color:#fbfbfb;\">");
                            sb.Append("<td style=\"padding:5px 5px 5px 5px;\" colspan=\"4\" align=\"center\">");
                            sb.Append("<a href=\"javascript:OnPre('" + id + "');\" >上一条</a>");
                            sb.Append("&nbsp;&nbsp;");
                            sb.Append("<a href=\"javascript:OnNext('" + id + "');\">下一条</a>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                        }
                    }
                }
                return sb.ToString();
            }
            catch {
                return string.Empty;
            }
        }
        private bool UpdateWaterImage(string path, ref string show) {
            try {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                int length = int.Parse(fs.Length.ToString());
                byte[] b = new byte[length];

                fs.Read(b, 0, length);
                fs.Dispose();
                fs.Close();

                string savePath = Server.MapPath(SAVEPATH);
                show = "show" + Guid.NewGuid().ToString().Replace("-", string.Empty);
                string showPath = Server.MapPath("../../xml/" + show + ".gif");
                if(File.Exists(savePath))
                    File.SetAttributes(savePath, FileAttributes.Normal);

                FileStream ws = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);
                ws.Write(b, 0, length);
                ws.Dispose();
                ws.Close();

                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("../../xml"));
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files) {
                    if (file.Name.StartsWith("show")) {
                        File.Delete(file.FullName);
                    }
                }
                FileStream s = new FileStream(showPath, FileMode.OpenOrCreate, FileAccess.Write);
                s.Write(b, 0, length);
                s.Dispose();
                s.Close();

                return true;
            }
            catch {
                return false;
            }
        }
        private bool SaveXml(string path, string smtp, string name, string pass, string port, string from, string to, string logname) {
            XmlDocument doc = null;
            try {
                doc = new XmlDocument();
                doc.Load(path);

                if (doc != null) {
                    XmlNode node = doc.SelectSingleNode("Root/Item");
                    
                    if (!string.IsNullOrEmpty(smtp))
                        node.Attributes["host"].Value = smtp;
                    if (!string.IsNullOrEmpty(name))
                        node.Attributes["userName"].Value = name;
                    if (!string.IsNullOrEmpty(pass))
                        node.Attributes["password"].Value = pass;
                    if (!string.IsNullOrEmpty(port))
                        node.Attributes["port"].Value = port;
                    if (!string.IsNullOrEmpty(from))
                        node.Attributes["from"].Value = from;
                    if (!string.IsNullOrEmpty(to))
                        node.Attributes["to"].Value = to;

                    XmlNodeList list = doc.SelectNodes("Root/LogItem");
                    int total = list.Count;
                    if (total == 10) {
                        foreach (XmlNode n in list) {
                            if (n.Attributes["id"].Value == "1") {
                                doc.DocumentElement.RemoveChild(n);
                            }
                            else {
                                n.Attributes["id"].Value = (int.Parse(n.Attributes["id"].Value) -1).ToString();
                            }
                        }
                        XmlElement e = doc.CreateElement("LogItem");
                        e.SetAttribute("id", "10");
                        e.SetAttribute("host", smtp);
                        e.SetAttribute("userName", name);
                        e.SetAttribute("password", pass);
                        e.SetAttribute("port", port);
                        e.SetAttribute("from", from);
                        e.SetAttribute("to", to);
                        e.SetAttribute("date", DateTime.Now.ToString());
                        e.SetAttribute("name", logname);

                        doc.DocumentElement.AppendChild(e);
                    }
                    else {
                        XmlElement n = doc.CreateElement("LogItem");
                        n.SetAttribute("id", (total + 1).ToString());
                        n.SetAttribute("host", smtp);
                        n.SetAttribute("userName", name);
                        n.SetAttribute("password", pass);
                        n.SetAttribute("port", port);
                        n.SetAttribute("from", from);
                        n.SetAttribute("to", to);
                        n.SetAttribute("date", DateTime.Now.ToString());
                        n.SetAttribute("name", logname);

                        doc.DocumentElement.AppendChild(n);
                    }
                    
                    File.SetAttributes(path, FileAttributes.Normal);

                    doc.Save(path);
                }
                return true;
            }
            catch {
                return false;
            }
        }
        private bool GetXml(string type, string path) {
            XmlDocument doc = null;
            try { 
                doc = new XmlDocument();
                doc.Load(path);

                if (doc != null) {
                    XmlNode node = doc.SelectSingleNode("Root/ShowType");
                    node.InnerText = type;

                    File.SetAttributes(path, FileAttributes.Normal);

                    doc.Save(path);
                }
                return true;
            }
            catch{
                return false;
            }
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
}
