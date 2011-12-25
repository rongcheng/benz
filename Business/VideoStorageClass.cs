using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.Business.VideoStorageWS;
using System.Data;

namespace QJVRMS.Business
{
    public class VideoStorageClass
    {
        public static string GetVideoSeq(DateTime dt)
        {
            QJVRMS.Business.BizData.BizService bs = new QJVRMS.Business.BizData.BizService();
            return bs.GetVideoSeq(dt);
        }

        public VideoStorage GetVideoInfo(string itemid)
        {
            VideoStorage v = new VideoStorage();
            VideoStorageService vss = new VideoStorageService();
            DataSet ds = vss.GetVideoInfo(itemid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                v.Caption = dr["caption"].ToString();
                
                v.ItemSerialNum= dr["itemserialnumber"].ToString();
                v.FileName= dr["clientfilename"].ToString();
                v.FolderName= dr["ServerFolderName"].ToString();
                v.ServerFileName= dr["serverfilename"].ToString();
                //v.FlvFilename= dr["flvfilename"].ToString();
                //v.FlvFilePath= dr["flvfilepath"].ToString();
                v.StartDate= Convert.ToDateTime(dr["startdate"]);
                v.EndDate = Convert.ToDateTime(dr["EndDate"]);
                v.uploadDate = Convert.ToDateTime(dr["uploadDate"]);
                v.shotDate = Convert.ToDateTime(dr["shotDate"]);
                v.Keyword = dr["Keywords"].ToString();
                v.Description = dr["Description"].ToString();
                v.updateDate = Convert.ToDateTime(dr["updatedate"]);
                v.userId= new Guid(dr["userid"].ToString());
                v.Status = Convert.ToInt32(dr["status"].ToString());

                v.ItemId = new Guid(dr["id"].ToString());
                v.FileSize = Convert.ToInt64(dr["FileSize"]);
            }
            return v;
        }



        /// <summary>
        /// 根据关键字搜索图片 
        /// 获取 ItemSerialNum,Hvsp 属性值
        /// </summary>
        /// <returns></returns>
        public bool Add(VideoStorage v)
        {
            VideoStorageService vss = new VideoStorageService();
            return vss.AddVideo(v.ItemId,v.ItemSerialNum,v.FileName,
                v.FolderName,v.ServerFileName,v.FlvFilename,
                v.FlvFilePath,
                v.Caption,
                v.StartDate,
                v.EndDate,
                v.uploadDate,
                v.shotDate,
                v.Keyword,
                v.Description,
                v.updateDate,
                v.userId,
                v.Status
                );
        }


        /// <summary>
        /// 将资源与分类关联起来
        /// </summary>
        /// <param name="Itemid"></param>
        /// <param name="catalogid"></param>
        public void CreateRelationshipVideoAndCatalog(Guid Itemid, Guid[] catalogid)
        {
            VideoStorageService vss = new VideoStorageService();

            vss.AddVideoToCatalog(catalogid, Itemid);
        }


        /// <summary>
        ///按关键字或视频编号，上传时间，分类搜索视频
        /// </summary>
        /// <returns></returns>
        public static DataTable SearchVideo(string keyword, string beginDate, string endDate, string Catalogid, string Userid, int PageSize, int PageNum, ref int rowCount)
        {
            QJVRMS.Business.SearchWS.SearchService ss = new QJVRMS.Business.SearchWS.SearchService();
            return ss.SearchVideo(keyword, beginDate, endDate, Catalogid, Userid, PageSize, PageNum, ref rowCount);
        }


    }
}
