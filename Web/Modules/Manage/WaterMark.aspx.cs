using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace WebUI.Modules.Manage {
    public partial class WaterMark : AuthPage {
        const string TEMPPATH = "../../xml/{0}.gif";
        const string SAVEPATH = "../../xml/water.gif";
        const string XMLPATH = "../../xml/mark.xml";
        const string SYSTEMPATH = "../../xml/System.xml";
        const string SHOOTPATH = "../../xml/Shoot.xml";

        public string showType = string.Empty;
        public string username = string.Empty;

        public string host = string.Empty;
        public string name = string.Empty;
        public string pass = string.Empty;
        public string port = string.Empty;
        public string from = string.Empty;
        public string to = string.Empty;
        public string content = string.Empty;

        public string time = string.Empty;

        protected void Page_Load(object sender, EventArgs e) {
            this.Title = "系统设置";
            time = DateTime.Now.Millisecond.ToString();
            content = getContent();
            username = CurrentUser.UserLoginName;
            OnLoad();
            XmlDocument mdoc = GetDocument(SYSTEMPATH);
            if (mdoc != null) {
                this.mSmtpId.Value = host = GetValue(mdoc, "host");
                this.mNameId.Value = name = GetValue(mdoc, "userName");
                this.mPassId.Value = pass = GetValue(mdoc, "password");
                this.mPortId.Value = port = GetValue(mdoc, "port");
                this.mFromId.Value = from = GetValue(mdoc, "from");
                this.mToId.Value = to = GetValue(mdoc, "to");
            }
            //Session.Clear();
        }
        private string getContent() {
            string path = Server.MapPath(@"../../xml");
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (FileInfo file in info.GetFiles()) {
                if (File.Exists(file.FullName)) {
                    File.SetAttributes(file.FullName, FileAttributes.Normal);
                    if (file.FullName.IndexOf("file") != -1) {
                        File.Delete(file.FullName);
                    }
                }
            }

            string filepath = Server.MapPath(@"../../xml/water.gif");
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[int.Parse(fs.Length.ToString())];
            fs.Read(b, 0, b.Length);
            fs.Close();
            string src = @"../../xml/file" + Guid.NewGuid().ToString().Replace("-", string.Empty) + ".gif";
            filepath = Server.MapPath(src);
            FileStream f = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            f.Write(b, 0, b.Length);
            f.Close();

            if (!File.Exists(filepath))
                src = @"../../xml/water.gif";
            StringBuilder sb = new StringBuilder();
            sb.Append("<img id=\"oldImgId\" src=\"" + src + "\" />");
            sb.Append("<div><input type=\"radio\" checked name=\"waterimg\" id=\"radoldid\" />Plain Image</div>");

            return sb.ToString();
        }
        private void OnLoad(){
            XmlDocument doc = null;
            doc = GetDocument(XMLPATH);
            if (doc != null) {
                XmlNode node = doc.SelectSingleNode("Root/ShowType");
                showType = node.InnerText;
            }
        }

        private bool UpdateWaterImage(string path) {
            try {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                int length = int.Parse(fs.Length.ToString());
                byte[] b = new byte[length];

                fs.Read(b, 0, length);
                fs.Dispose();
                fs.Close();

                string savePath = Server.MapPath(SAVEPATH);
                File.SetAttributes(savePath, FileAttributes.Normal);

                FileStream ws = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);
                ws.Write(b, 0, length);
                ws.Dispose();
                ws.Close();

                return true;
            }
            catch {
                return false;
            }
        }
        private XmlDocument GetDocument(string path) {
            XmlDocument doc = null;
            try {
                doc = new XmlDocument();
                doc.Load(Server.MapPath(path));

                return doc;
            }
            catch {
                return null;
            }
        }

        private string GetValue(XmlDocument doc, string name) {
            return doc.SelectSingleNode("//Item").Attributes[name].Value;
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
            catch {
                return false;
            }
        }
    }
}
