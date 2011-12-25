using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WebUI.UserControls
{
    public partial class DataPicFolder : System.Web.UI.UserControl
    { /// <summary>
        /// 图片状态
        /// </summary>
        private string strState = string.Empty;

        /// <summary>
        /// 收藏夹编号
        /// </summary>
        private int intFolder = 0;

        /// <summary>
        /// 小样图下载/高分辨下载的权限
        /// </summary>
        private int intPermit = 0;

        /// <summary>
        /// 高分辨图片查看的权限
        /// </summary>
        private int intRightRead = 0;

      private StringBuilder sbFolderList = new StringBuilder("");

        public int IntRightRead
        {
            get { return intRightRead; }
            set { intRightRead = value; }
        }

        public int IntPermit
        {
            get { return intPermit; }
            set { intPermit = value; }
        }

        public int IntFolder
        {
            get { return intFolder; }
            set { intFolder = value; }
        }

        public string StrState
        {
            get { return strState; }
            set { strState = value; }
        }

        public object DataSource
        {
            get { return this.DataList1.DataSource; }
            set
            {
                this.DataList1.DataSource = value;
            }
        }

        public object DtFolder
        {
            set
            {
                DataTable dt = (DataTable)value;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sbFolderList.Append("<option value='" + dt.Rows[i][0].ToString() + "'");
                    if (dt.Rows[i][0].ToString() == intFolder.ToString())
                        sbFolderList.Append(" selected");
                    sbFolderList.Append(">" + dt.Rows[i][1].ToString());
                }
            }
        }

        public override void DataBind()
        {
            this.DataList1.DataBind();
            DataList1.Dispose();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected string strImageServer = System.Configuration.ConfigurationSettings.AppSettings.Get("imageServer");
        protected string strImageLink = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //图片路径
        protected string GetImgUrl(string FolderName, string ItemSerialNum, string ImageType)
        {
            string yRootPath = "";
            switch (ImageType.ToUpper())
            {
                case "JPG":
                case "JPEG":
                case "GIF":
                    yRootPath += WebUI.UIBiz.CommonInfo.SlImageRootPath170 + FolderName + @"\" + ItemSerialNum + "." + ImageType; break;
                case "TXT":
                    yRootPath += @"..\images\txt.jpg"; break;
                case "DOC":
                case "DOCX":
                    yRootPath += @"..\images\doc.jpg"; break;
                case "WMF":
                    yRootPath += @"..\images\wmf.jpg"; break;
                case "XLS":
                case "XLSX":
                    yRootPath += @"..\images\xls.jpg"; break;
                case "PSD":
                    yRootPath += @"..\images\psd.jpg"; break;
                case "PPT":
                    yRootPath += @"..\images\ppt.jpg"; break;
                case "PPS":
                    yRootPath += @"..\images\pps.jpg"; break;
                case "PDF":
                    yRootPath += @"..\images\pdf.jpg"; break;
                case "MDB":
                case "ACCDB":
                    yRootPath += @"..\images\mdb.jpg"; break;
                case "RAR":
                    yRootPath += @"..\images\rar.jpg"; break;
                case "ZIP":
                    yRootPath += @"..\images\zip.jpg"; break;
                case "HTM":
                case "HTML":
                    yRootPath += @"..\images\htm.jpg"; break;
                default:
                    yRootPath += @"..\images\other.jpg"; break;
            }
            return yRootPath;
        }
        protected string GetKind(object kind)
        {
            string strKind = kind.ToString();

            if (strKind.Substring(1, 1) == "M")
                return " [<font color=#9e1111>RM</font>]";
            else
                return " [<font color=#207e0c>RF</font>]";
        }

        /// <summary>
        /// 图片下载
        /// </summary>
        /// <param name="picId"></param>
        /// <returns></returns>
        protected string GetCmd(string picId, string ptype)
        {
            string strPicid = picId.ToString();

            if (Request.IsAuthenticated)
            {

                return "<a target='_self' href=\"javascript:downhigh('" + strPicid + "','" + ptype + "')\">图片下载</a>";
            }

            else
            {
                return "";
            }

        }

        protected string SetFolderList()
        {
            return sbFolderList.ToString();
        }
    
    }
}