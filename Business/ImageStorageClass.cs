using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using QJVRMS.DataAccess;
using System.Data.SqlClient;
using System.Collections;
using QJVRMS.Business.ImageStorageWS;

namespace QJVRMS.Business
{
    /// <summary>
    /// Author: wangyw
    /// Date: 2008.05.23
    /// </summary>
    public class ImageStorageClass
    {



        public static string GetImageSeq(DateTime dt)
        {
            QJVRMS.Business.BizData.BizService bs = new QJVRMS.Business.BizData.BizService();
            return bs.GetImageSeq(dt);
        }

        /// <summary>
        /// 根据关键字搜索图片 
        /// 获取 ItemSerialNum,Hvsp 属性值
        /// </summary>
        /// <returns></returns>
        public static string AddImageStorage(IImageStorage imgStorage)
        {
            ImageStorageService iss = new ImageStorageService();
            return iss.AddImageStorage(imgStorage.userId,
                imgStorage.FileName,
                imgStorage.FolderName,
                imgStorage.Caption,
                imgStorage.Address,
                imgStorage.Character,
                imgStorage.StartDate,
                imgStorage.EndDate,
                imgStorage.shotDate,
                imgStorage.Keyword,
                imgStorage.Description,
                imgStorage.ImageType,
                imgStorage.Hvsp,
                imgStorage.ItemId,
                imgStorage.ItemSerialNum,
                imgStorage.GroupId);

        }
        /// <summary>
        /// 修改图片信息
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static bool UpdateImageStorage(IImageStorage img)
        {
            ImageStorageService iss = new ImageStorageService();
            return iss.UpdateImageStorage(img.ItemId,
                img.Caption,
                img.Address,
                img.Character,
                img.StartDate,
                img.EndDate,
                img.shotDate,
                img.Keyword,
                img.Description);

        }



        public static bool DeleteImageStorage(Guid itemId)
        {
            ImageStorageService iss = new ImageStorageService();
            return iss.DeleteImageStorage(itemId);

        }
        /// <summary>
        /// 保存图片id与类id到关联表
        /// </summary>
        /// <param name="Itemid">图片id</param>
        /// <param name="catalogid">类id</param>
        /// <returns></returns>
        public void CreateRelationshipImageAndCatalog(Guid Itemid, Guid catalogid)
        {
            ImageStorageService iss = new ImageStorageService();
            iss.AddImageToCatalog(new Guid[] { catalogid }, Itemid);
        }

        public void CreateRelationshipImageAndCatalog(Guid Itemid, Guid[] catalogid)
        {
            ImageStorageService iss = new ImageStorageService();
            iss.AddImageToCatalog(catalogid, Itemid);
        }


        public static DataTable GetImageByNum(string imageNum, Guid userId)
        {
            ImageStorageService iss = new ImageStorageService();
            DataTable dt = iss.GetImageByAuthAndNum(imageNum, userId);
            return dt;
            //DataRow dr = dt.Rows[0];
            //return QJVRMS.Business.ImageStorage.ParseImageStorage(dr);
        }


        //public static IImageStorage GetImageInfoByNum(string serialNum)
        //{
        //    ImageStorageService iss = new ImageStorageService();
        //    string objStr = iss.GetImageInfoByNum(serialNum);

        //    QJVRMS.Common.SerializeObjectFactory sof = new QJVRMS.Common.SerializeObjectFactory();
        //    object o = sof.DesializeFromBase64(objStr);

        //    QJVRMS.Business.ImageStorage oimage = (QJVRMS.Business.ImageStorage)o;

        //    return oimage;
        //}

        public static IImageStorage GetImageInfoByItemId(Guid itemId, Guid userId)
        {

            ImageStorageService iss = new ImageStorageService();
            // string objStr = iss.GetImageInfoByItemId(itemId);
            DataTable dt = iss.GetImageInfoByAuthAndId(itemId, userId);

            return ImageStorage.ParseImageStorage(dt.Rows[0]);

            //QJVRMS.Common.SerializeObjectFactory sof = new QJVRMS.Common.SerializeObjectFactory();
            //object o = sof.DesializeFromBase64(objStr);

            //QJVRMS.Business.ImageStorage oimage = (QJVRMS.Business.ImageStorage)o;

          
        }


        public static bool AddtoLightBox(Guid userId, Guid imageId, string path, string serNum)
        {
            QJVRMS.Business.CallbackWS.CallbackService cs = new QJVRMS.Business.CallbackWS.CallbackService();
            return cs.AddToLightBox(imageId, userId, path, serNum);
        }


        public static DataTable GetLightBoxItems(Guid userId)
        {
            ImageStorageService iss = new ImageStorageService();
            return iss.GetLightBoxList(userId);
        }

        public static bool DeleteItemOfLightBox(Guid userId, Guid itemId)
        {
            ImageStorageService iss = new ImageStorageService();
            return iss.DelItemFromLightBox(userId, itemId);
        }


        /// <summary>
        /// 获取最新图片
        /// </summary>
        /// <returns></returns>
        public static DataTable GetLatestImages()
        {
            QJVRMS.Business.BizData.BizService bs = new QJVRMS.Business.BizData.BizService();
            return bs.GetTopLatestImage();
        }


        /// <summary>
        /// 统计信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStatImages()
        {
            ImageStorageService iss = new ImageStorageService();
            return iss.GetImageStatic();
        }

        public static DataTable GetTopImagesOfCatalog(Guid parentCataId)
        {
            QJVRMS.Business.BizData.BizService bs = new QJVRMS.Business.BizData.BizService();
            return bs.GetTopImagesOfCatalog(parentCataId);
        }

        public static void Production_Hires_Down_Log(string filaName, string fileType, string downusername, string usage, string enduser,string folder, bool Errflag)
        {

            ImageStorageService iss = new ImageStorageService();
            iss.Production_Hires_Down_Log(filaName, fileType, downusername, usage, enduser, folder, Errflag);
        }


        #region 附件操作

        public static bool AddAttach(Guid itemId,string fileName)
        {
            ImageStorageService iss = new ImageStorageService();
            return iss.AddAttach(itemId, fileName);
        }

        public static void DeleteAttach(Guid attId)
        {
            ImageStorageService iss = new ImageStorageService();
            iss.DeleteAttach(attId);
        }

        public static DataTable GetAttachList(Guid itemId)
        {
            ImageStorageService iss = new ImageStorageService();
            return iss.GetAttachList(itemId);
        }

        public static DataSet GetCatalogByItemId(string itemId)
        {
            ImageStorageService iss = new ImageStorageService();
            return iss.GetImageCatalog(itemId);
        }
        #endregion
    }
}
