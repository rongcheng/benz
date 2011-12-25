using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebUI.UserControls
{
    public partial class Data_List : System.Web.UI.UserControl
    {
        /// <summary>
        /// 列表显示的数据类型
        /// </summary>
        public enum DataListType { Feature, Photo, Necos, Video, Docs, Audio }

        /// <summary>
        /// 显示列数
        /// </summary>
        public int ShowColumnCount
        { get; set; }

        /// <summary>
        /// 当前数据页
        /// </summary>
        public int PageIndex
        { get; set; }

        /// <summary>
        /// 当前页数据行
        /// </summary>
        public int PageSize
        { get; set; }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public string LoginName
        { get; set; }

        /// <summary>
        /// 列表类型
        /// </summary>
        public DataListType ListType
        { get; set; }

        public Unit Width
        {
            get { return dlList.Width; }
            set { dlList.Width = value;}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void InitData()
        {
            DataTable dt = new DataTable();
            dlList.RepeatColumns = ShowColumnCount;
            

            switch (ListType)
            {
                case DataListType.Feature:
                    dt = GetFeatureList();
                    break;
                case DataListType.Photo:
                    break;
                case DataListType.Necos:
                    break;
                case DataListType.Video:
                    break;
                case DataListType.Docs:
                    break;
                case DataListType.Audio:
                    break;
                default:
                    break;
            }

            dlList.DataSource = dt;
            dlList.DataBind();
        }

        private DataTable GetFeatureList()
        {
            QJVRMS.Business.FeatureFactory featureFactory = new QJVRMS.Business.FeatureFactory();
            return featureFactory.GetFeatures(LoginName, PageSize, PageIndex);
        }
    }
}