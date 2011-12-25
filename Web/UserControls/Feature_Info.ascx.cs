using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using QJVRMS.Business.ResourceType;

namespace WebUI.UserControls
{
    public partial class Feature_Info : System.Web.UI.UserControl
    {
        public DataRowView CurrentItem
        {
            set
            {
                InitData(value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="row"></param>
        public void InitData(DataRowView row)
        {
            if (row != null)
            {
                lblFeatureName.Text = row["FeatureName"].ToString();
                lblFeatureDes.Text = row["FeatureDes"].ToString();

                ImageType imageType = new ImageType();

                imgCoverImage.ImageUrl = imageType.GetPreviewPathRead(
                                            row["Creator"].ToString(),
                                            row["CoverImage"].ToString(), "170");

            }
        }
    }
}