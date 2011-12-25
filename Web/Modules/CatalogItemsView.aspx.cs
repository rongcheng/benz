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
using QJVRMS.Business;
using QJVRMS.Business.SecurityControl;
using System.Text;

namespace WebUI.Modules
{
    public partial class CatalogItemsView :  AuthPage
    {
    //    int flag = 0;
    //    string catalogid = "";

    //    int CurrentPageCount = 1;
    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        CurrentPageCount =  Convert.ToInt32(this.pageCounttxt.Text);
    //        if (!this.IsPostBack)
    //        {
    //            this.catalogTree.RootNodeName = CurrentUser.GroupName;
    //         //   this.catalogTree.GroupId = CurrentGroupId;           
    //        }
    //        else
    //        {
    //            catalogid = this.catalogTree.CurrentSelNode.Value;
    //        }
    //        GetImageList(catalogid, CurrentPageCount);

    //    }


    //    /// <summary>
    //    /// 执行显示图片
    //    /// </summary>
    //    /// <param name="catalogid">根据图片类id查找显示图片</param>
    //    /// <returns></returns>
    //    private void GetImageList(string catalogid,int count)
    //    {
    //        ArrayList imageArr=null;
    //        if(catalogid=="")
    //            imageArr = new ImageStorageClass().All(CurrentUser.UserId,count);
    //        else
    //            imageArr = new ImageStorageClass().SearchByCatalog(new Guid(catalogid));
    //        int imagecoutn=imageArr.Count;
    //        if (imagecoutn > 0)
    //        {
    //            StringBuilder oImageStringBuilder = new StringBuilder("");
    //            BuildImageListHtml(imageArr, 0, oImageStringBuilder);
    //        }
    //        else
    //        {
    //            this.imagelist_tbody.InnerHtml = "<tr valign=top><td>没有你查找的图片</td></tr>";
    //        }
    //    }

    //    //生成图片列表，每行4张图片(暂没翻页)
    //    private void BuildImageListHtml(ArrayList imagelist, int n, StringBuilder oStringBuilder)
    //    {

    //            oStringBuilder.Append("<tr valign=top>");
    //            int i =n;

    //            flag = 0;
    //            for (; i < imagelist.Count; i++)
    //            {
    //                flag++;
    //                IImageStorage oImage = (IImageStorage)imagelist[i];
    //                if (oImage != null)
    //                {
    //                    if(flag!=1 && (flag-1)%4==0)
    //                        break;
    //                    else
    //                        oStringBuilder.AppendFormat("<td  width=25% align=\"left\" bgcolor=\"#ffffff\">{0}</td>", BuildImageHtml(oImage));
    //                }
    //            }
    //            oStringBuilder.Append("</tr>");
    //            this.imagelist_tbody.InnerHtml = oStringBuilder.ToString();

    //            if(i<imagelist.Count)
    //                BuildImageListHtml(imagelist, i, oStringBuilder);           
    //    }

    //    private string BuildImageHtml(IImageStorage oImageStorage)
    //    {
    //        string fileType = oImageStorage.ImageType;


    //        //图片存储路径
    //        string szUrl =WebUI.UIBiz.CommonInfo.SlImageRootPath170+oImageStorage.ItemSerialNum+"."+fileType;


    //        string imgHref = "ImageView.aspx"+"?imgID="+oImageStorage.ItemId;
    //        StringBuilder oStringBuilder = new StringBuilder("");

    //        oStringBuilder.Append("<table>");
    //        oStringBuilder.Append("<tr>");
    //        oStringBuilder.AppendFormat("<td><img  src={0} style=\"cursor:hand\" border=\"0\" onclick=\"GoimageView('{1}')\"></td>", szUrl, imgHref);
    //        oStringBuilder.Append("</tr>");
    //        oStringBuilder.Append("<tr>");
    //        oStringBuilder.AppendFormat("<td align=\"center\">{0}</td>", oImageStorage.FileName);
    //        oStringBuilder.Append("</tr>");
    //        oStringBuilder.Append("<tr>");
    //        oStringBuilder.AppendFormat("<td align=\"center\"><a href=\"{0}\">详细信息</a></td>", imgHref);
    //        oStringBuilder.Append("</tr>");
    //        oStringBuilder.Append("</table>");

    //        return oStringBuilder.ToString();

    //    }

        
        
    //    protected string CurrentGroupName
    //    {
    //        get
    //        {
    //            return CurrentUser.GroupName;
    //        }
    //    }

    //    protected Guid CurrentGroupId
    //    {
    //        get
    //        {
    //            return CurrentUser.UserGroupId;
    //        }
    //    }

      }
}
