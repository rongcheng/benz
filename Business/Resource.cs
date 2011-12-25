using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.Common;
using QJVRMS.Business.BizData;
using System.Data;
using QJVRMS.Business.ResourceWS;

using System.IO;
using QJVRMS.Business.Interface;
using QJVRMS.Business.ResourceType;
using QJVRMS.Business;
using QJVRMS.Business.SecurityControl;


namespace QJVRMS.Business
{
    public class Resource
    {

        /// <summary>
        /// ȡ�����µ���ˮ��
        /// </summary>
        /// <param name="Prefix">IMG V</param>
        /// <returns></returns>
        public string GetSN(string Prefix)
        {
            BizService bs = new BizService();
            
            return bs.GetSN(Prefix); 
        }


        /// <summary>
        /// ͳ����Ϣ
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStatResources()
        {
            QJVRMS.Business.ResourceWS.ResourceService rs = new ResourceService();
            return rs.GetResourceStatic();
        }

        /// <summary>
        /// ���ش���ͳ��
        /// </summary>
        /// <param name="resourceType">��Դ���� image,video,document,other</param>
        /// <param name="startDate">��ʼ����</param>
        /// <param name="endDate">��������</param>
        /// <returns></returns>
        public DataTable GetDownloadStatic(string resourceType, DateTime startDate, DateTime endDate)
        {
            ResourceService rs = new ResourceService();
            return rs.GetDownloadStatic(resourceType, startDate, endDate);
        }

        /// <summary>
        /// �õ�Դ�ļ��Ĵ�С��ͨ�� FileInfo�����ȡ
        /// </summary>
        /// <param name="serverFileName">�ļ���</param>
        /// <param name="serverFolderName">�ļ�����</param>
        /// <param name="fileType">����fileType�õ�������ʲô���͵���Դ</param>
        /// <param name="previewType"></param>
        /// <returns></returns>
        public static long GetResourceFileSize(string serverFileName, string serverFolderName, string fileType, string previewType)
        {
            long l = 0;
            string resourceRootPath = string.Empty;
            string filePath = string.Empty;

            IResourceType obj = ResourceTypeFactory.getResourceType(fileType);
            //yangguang
            //filePath = Path.Combine(obj.SourcePath, serverFolderName);
            //filePath = Path.Combine(filePath, serverFileName);
            filePath = obj.GetSourcePath(serverFolderName, serverFileName);

            
            if (File.Exists(filePath))
            {
                try
                {
                    l = new FileInfo(filePath).Length;
                }
                catch (Exception ep)
                {
                    LogWriter.WriteExceptionLog(ep, true);
                }
            }
            return l;
        }


        /// <summary>
        /// ����Դ�ļ����������ڣ�����EXIFEextactor���������
        /// </summary>
        /// <param name="serverFileName"></param>
        /// <param name="serverFolderName"></param>
        /// <param name="resourceType"></param>
        /// <param name="previewType"></param>
        /// <returns></returns>
        public static DateTime GetResourceShotDateTime(string serverFileName,string serverFolderName,string resourceType,string previewType)
        {
            DateTime dt=DateTime.MinValue;
            string filePath = string.Empty;
            string folder = string.Empty;
            
            if (resourceType.ToLower().Equals("image"))
            {
                IResourceType obj = new ImageType();
                //yangguang
                //folder = obj.SourcePath;
                //folder = Path.Combine(folder, serverFolderName);
                //filePath = Path.Combine(folder, serverFileName);
                filePath = obj.GetSourcePath(serverFolderName, serverFileName);

                try
                {

                    Goheer.EXIF.EXIFextractor er2 = new Goheer.EXIF.EXIFextractor(filePath, "", "");
                    string s = string.Empty;
                    if (er2["DTDigitized"] != null)
                    {
                        s = er2["DTDigitized"].ToString();
                    }
                    else if (er2["DTOrig"] != null)
                    {
                        s = er2["DTOrig"].ToString();
                    }
                    else if (er2["Date Time"] != null)
                    {
                        s = er2["Date Time"].ToString();

                    }
                    
                    if (!string.IsNullOrEmpty(s))
                    {
                        
                            string[] arr = s.Split(" ".ToCharArray());
                        
                            string s1 = arr[0].Replace(":", "-") + " " + arr[1];
                            dt = Convert.ToDateTime(s1.ToString());
                        
                    }
                 }
                catch (Exception ex)
                {
                    LogWriter.WriteExceptionLog(ex);
                }
            }
            else if (resourceType.ToLower().Equals("video"))
            {
                
            }

            return dt;        
        }

        /// <summary>
        /// ���һ���µ���Դ��¼
        /// </summary>
        /// <param name="r">��Դʵ������</param>
        /// <returns></returns>
        public void Add(ResourceEntity r)
        {
            ResourceService rs = new ResourceService();
            rs.Add(r.ItemId, r.ItemSerialNum, r.FileName,
                r.FolderName,
                r.FileSize,
                r.ServerFileName, 
                r.Caption,
                r.StartDate,
                r.EndDate,
                r.uploadDate,
                r.shotDate,
                r.Keyword,
                r.Description,
                r.updateDate,
                r.userId,
                r.Status,
                r.ResourceType,
                r.Author,
                r.HasCopyright
                );

            //ͬʱ��������
            string[] SNs = new string[] { r.ItemSerialNum };
            ResourceIndex.updateIndex(SNs);
        }


        /// <summary>
        /// ��Դ���
        /// </summary>
        /// <param name="ids">itemid�ַ�������</param>
        /// <param name="validateStatus">Ҫת�������״̬</param>
        /// <param name="reason">��ͨ��ʱ��ԭ��</param>
        public void ValidateResourceByIDs(string[] ids, int validateStatus, string reason)
        {
            ResourceService rs = new ResourceService();
            rs.ValidateResourceByIDs(ids, validateStatus, reason);


            //��������
            DataSet ds = rs.GetResourceSNByIDs(ids);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                string[] _sns = new string[dt.Rows.Count];
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    _sns[i] = dt.Rows[i][0].ToString();

                }
                ResourceIndex.updateIndex(_sns);
            }

        }

        /// <summary>
        /// ��Դ������ˣ�ͬʱҲҪ��������
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="validateStatus"></param>
        public void ValidateResourceByUserDate(Guid userId, DateTime startDate, DateTime endDate, int validateStatus)
        {
            ResourceService rs = new ResourceService();
            rs.ValidateResourceByUserDate(userId, startDate, endDate, validateStatus);

            //��������
            DataSet ds = rs.GetResourceSNByUserDate(userId, startDate, endDate);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                string[] _sns = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _sns[i] = dt.Rows[i][0].ToString();

                }
                ResourceIndex.updateIndex(_sns);
            }
        }


        /// <summary>
        /// ��Դ������ˣ�ͬʱҲҪ��������
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="validateStatus"></param>
        public void ValidateResourceByUserDate1(Guid userId, DateTime startDate, DateTime endDate, int validateStatusFrom,int validateStatusTo)
        {
            ResourceService rs = new ResourceService();

            
            rs.ValidateResourceByUserDate1(userId, startDate, endDate, validateStatusFrom,validateStatusTo);

            //��������
            DataSet ds = rs.GetResourceSNByUserDate(userId, startDate, endDate);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                string[] _sns = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _sns[i] = dt.Rows[i][0].ToString();

                }
                ResourceIndex.updateIndex(_sns);
            }
        }




        /// <summary>
        /// ��˲����ϵ�ʱ�򣬷���ԭ��
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public string GetNotPassReason(string itemId)
        {
            ResourceService rs = new ResourceService();
            return rs.GetNotPassReason(itemId);
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(ResourceEntity r)
        {
            ResourceService rs = new ResourceService();
            rs.Update(r.ItemId.ToString(),
                r.Caption,
                r.Keyword,
                r.Description,
                r.shotDate,
                r.StartDate,
                r.EndDate
            );

            //ͬʱ��������
            string[] SNs = new string[] { r.ItemSerialNum };
            ResourceIndex.updateIndex(SNs);
        }


        /// <summary>
        /// ����Դ������������
        /// </summary>
        /// <param name="Itemid"></param>
        /// <param name="catalogid"></param>
        public void CreateRelationshipResourceAndCatalog(Guid Itemid, Guid[] catalogid)
        {
            ResourceService rs = new ResourceService();

            rs.AddResourceToCatalog(catalogid, Itemid);
        }


        /// <summary>
        ///���ؼ��ֻ��ţ��ϴ�ʱ�䣬��Ч�ڣ�����������Դ
        /// </summary>
        /// <returns></returns>
        public static DataSet Search(string keyword, string beginDate, string endDate, string Catalogid, string Userid, int PageSize, int PageNum, ref int pageCount,string resourceType,string groupid)
        {
            /** ʹ������������ʼ *****************************/
            if (ResourceIndex.IsUsingIndex)
            {
                if (Catalogid.Equals(new Guid().ToString()))
                {
                    Catalogid = "";
                }
                else
                {
                    Catalogid = " " + Catalogid;
                }

                if (!string.IsNullOrEmpty(groupid.Trim()))
                {
                    groupid = " " + groupid;
                }
                else
                {
                    groupid = "";
                }

                PageNum++;
                return ResourceIndex.Search(keyword+Catalogid+groupid, beginDate, endDate,  Userid, PageSize, PageNum, ref pageCount, resourceType);
            }
            /** ʹ�������������� *******************************/




            QJVRMS.Business.ResourceWS.ResourceService rs = new ResourceService();
            return rs.SearchResource(keyword, beginDate, endDate, Catalogid, Userid, PageSize, PageNum, ref pageCount,resourceType,groupid,2);
            

        }



        /// <summary>
        ///���ؼ��ֻ��ţ��ϴ�ʱ�䣬��Ч�ڣ�����������Դ�� ͼƬ�߳� �ļ���չ��
        /// </summary>
        /// <returns></returns>
        public static DataSet Search(string keyword, string beginDate, string endDate, string Catalogid, string Userid, int PageSize, int PageNum, ref int pageCount, string resourceType, string groupid,string fileExt,string fileWH)
        {
            /** ʹ������������ʼ *****************************/
            if (ResourceIndex.IsUsingIndex)
            {
                if (Catalogid.Equals(new Guid().ToString()))
                {
                    Catalogid = "";
                }
                else
                {
                    Catalogid = " " + Catalogid;
                }

                if (!string.IsNullOrEmpty(groupid.Trim()))
                {
                    groupid = " " + groupid;
                }
                else
                {
                    groupid = "";
                }

                PageNum++;
                return ResourceIndex.Search(keyword + Catalogid + groupid, beginDate, endDate, Userid, PageSize, PageNum, ref pageCount, resourceType, fileExt, fileWH);
            }
            /** ʹ�������������� *******************************/




            QJVRMS.Business.ResourceWS.ResourceService rs = new ResourceService();
            return rs.SearchResource(keyword, beginDate, endDate, Catalogid, Userid, PageSize, PageNum, ref pageCount, resourceType, groupid, 2);


        }





        /// <summary>
        ///���ؼ��ֻ��ţ��ϴ�ʱ�䣬��Ч�ڣ����࣬���״̬������Դ��
        /// </summary>
        /// <returns></returns>
        public static DataSet Search(string keyword, string beginDate, string endDate, string Catalogid, string Userid, int PageSize, int PageNum, ref int pageCount, string resourceType, string groupid,int validateStatus)
        {
            /** ʹ������������ʼ *****************************/
            if (ResourceIndex.IsUsingIndex)
            {
                if (Catalogid.Equals(new Guid().ToString()))
                {
                    Catalogid = "";
                }
                else
                {
                    Catalogid = " " + Catalogid;
                }

                if (!string.IsNullOrEmpty(groupid.Trim()))
                {
                    groupid = " " + groupid;
                }
                else
                {
                    groupid = "";
                }

                PageNum++;
                return ResourceIndex.Search(keyword + Catalogid + groupid, beginDate, endDate, Userid, PageSize, PageNum, ref pageCount, resourceType);
            }
            /** ʹ�������������� *******************************/




            QJVRMS.Business.ResourceWS.ResourceService rs = new ResourceService();
            return rs.SearchResource(keyword, beginDate, endDate, Catalogid, Userid, PageSize, PageNum, ref pageCount, resourceType, groupid,validateStatus);


        }



        public static DataSet Search(int pageSize, int pageIndex, out int pageCount, string keyword, DateTime beg, DateTime end, Guid catalogId, Guid userId)
        {
            
            /** ʹ������������ʼ *****************************/
            if (ResourceIndex.IsUsingIndex)
            { 
                return ResourceIndex.Search(pageSize,pageIndex, out pageCount,keyword+" "+catalogId, beg, end);
            }
            /** ʹ�������������� *******************************/
            
            pageCount = 0;


            string sqlWhere = string.Empty;

            if (!string.IsNullOrEmpty(keyword))
            {
                sqlWhere += " And (Keyword like '%" + keyword + "%' or Caption like '%" + keyword + "%' or ItemSerialNumber like '%" + keyword + "%')";
            }

            if (catalogId != Guid.Empty)
            {
                sqlWhere += " And ic.catalogId='" + catalogId.ToString() + "'";
            }

            if (beg != DateTime.MaxValue
                && end != DateTime.MaxValue)
            {
                sqlWhere += " And datediff(d,'" + beg.ToShortDateString() + "',i.uploadDate)>=0 And datediff(d,'" + end.ToShortDateString() + "',i.uploadDate)<=0";
            }

            QJVRMS.Business.SearchWS.SearchService ss = new QJVRMS.Business.SearchWS.SearchService();
            return ss.Search(sqlWhere, userId, pageIndex, pageSize);
        }

        /// <summary>
        /// ����ͼƬ��100�ţ������ݿ���ֱ��ȡ
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public DataSet GetResourcesByViewCount(DateTime startDate, DateTime endDate, int pageSize, int pageNum, string resourceType)
        {
            ResourceService rs = new ResourceService();
            return rs.GetResourcesByViewCount(startDate, endDate, pageSize, pageNum, resourceType);
        }

        /// <summary>
        /// �������У�100�ţ������ݿ���ֱ��ȡ
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public DataSet GetResourcesByDownloadCount(DateTime startDate, DateTime endDate, int pageSize, int pageNum, string resourceType)
        {
            ResourceService rs = new ResourceService();
            return rs.GetResourcesByDownloadCount(startDate, endDate, pageSize, pageNum, resourceType);
        }

        /// <summary>
        /// �ϴ���־�� ����Դ����ֱ��ȡ��
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataSet GetResourcesUploadLog(DateTime startDate, DateTime endDate, int pageSize, int pageNum, string userId)
        {
            ResourceService rs = new ResourceService();
            return rs.GetResourceUploadLog(startDate, endDate, pageSize, pageNum, userId);
        }


        /// <summary>
        ///��ȡĳ���ϴ�����Դ
        /// </summary>
        /// <returns></returns>    
        public DataSet GetResourceByUserID(string beginDate, string endDate, string Userid, int PageSize, int PageNum, ref int rowCount, string resourceType,int validateStatus)
        {
            ResourceService rs = new ResourceService();
            return rs.GetResourceByUserID(beginDate, endDate, Userid, PageSize, PageNum, ref rowCount, resourceType,validateStatus);
        }

        public ResourceEntity GetResourceInfoByItemId(string itemid)
        {
            ResourceEntity r = new ResourceEntity();
            ResourceService rs = new ResourceService();
            DataSet ds = rs.GetResourceInfoByItemId(itemid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                r.Caption = dr["caption"].ToString();

                r.ItemSerialNum = dr["itemserialnumber"].ToString();
                r.FileName = dr["clientfilename"].ToString();
                r.FolderName = dr["ServerFolderName"].ToString();
                r.ServerFileName = dr["serverfilename"].ToString();
                //v.FlvFilename= dr["flvfilename"].ToString();
                //v.FlvFilePath= dr["flvfilepath"].ToString();
                r.StartDate = Convert.ToDateTime(dr["startdate"]);
                r.EndDate = Convert.ToDateTime(dr["EndDate"]);
                r.uploadDate = Convert.ToDateTime(dr["uploadDate"]);
                r.shotDate = Convert.ToDateTime(dr["shotDate"]);
                r.Keyword = dr["Keywords"].ToString();
                r.Description = dr["Description"].ToString();
                r.updateDate = Convert.ToDateTime(dr["updatedate"]);
                r.userId = new Guid(dr["userid"].ToString());
                r.Status = Convert.ToInt32(dr["status"].ToString());

                r.ItemId = new Guid(dr["id"].ToString());
                r.ResourceType = dr["resourceType"].ToString();
                r.FileSize = Convert.ToInt32(dr["filesize"].ToString());
                r.Author = dr["author"].ToString();
                r.ViewCount = Convert.ToInt32(dr["viewCount"].ToString());

                if (dr["hasCopyright"] == DBNull.Value)
                {
                    r.HasCopyright = 1;
                }
                else
                {
                    r.HasCopyright = Convert.ToInt32(dr["hasCopyright"].ToString());
                }
                
            }
            return r;
        }



        public ResourceEntity GetResourceInfoBySN(string sn)
        {
            ResourceEntity r = new ResourceEntity();
            ResourceService rs = new ResourceService();
            DataSet ds = rs.GetResourceInfoBySN(sn);
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                DataRow dr = ds.Tables[0].Rows[0];
                r.Caption = dr["caption"].ToString();

                r.ItemSerialNum = dr["itemserialnumber"].ToString();
                r.FileName = dr["clientfilename"].ToString();
                r.FolderName = dr["ServerFolderName"].ToString();
                r.ServerFileName = dr["serverfilename"].ToString();
                //v.FlvFilename= dr["flvfilename"].ToString();
                //v.FlvFilePath= dr["flvfilepath"].ToString();
                r.StartDate = Convert.ToDateTime(dr["startdate"]);
                r.EndDate = Convert.ToDateTime(dr["EndDate"]);
                r.uploadDate = Convert.ToDateTime(dr["uploadDate"]);
                r.shotDate = Convert.ToDateTime(dr["shotDate"]);
                r.Keyword = dr["Keywords"].ToString();
                r.Description = dr["Description"].ToString();
                r.updateDate = Convert.ToDateTime(dr["updatedate"]);
                r.userId = new Guid(dr["userid"].ToString());
                r.Status = Convert.ToInt32(dr["status"].ToString());
                r.ItemId = new Guid(dr["id"].ToString());
                r.ResourceType=dr["resourceType"].ToString();
                r.FileSize = Convert.ToInt32(dr["filesize"].ToString());
                r.Author = dr["author"].ToString();

                
            }
            return r;
        }

        public VideoStorage GetVideoInfoBySN(string sn)
        {
            ResourceService rs = new ResourceService();
            VideoStorage v = null;
            DataSet ds = rs.GetResourceInfoBySN(sn);
            if (ds.Tables[0].Rows.Count > 0)
            {
                v = new VideoStorage();
                DataRow dr = ds.Tables[0].Rows[0];
                v.Bitrate = string.Empty;
                v.ClipLength = string.Empty;
                v.ClipSize = string.Empty;

                if (ds.Tables[0].Columns.Contains("bitrate"))
                {
                    v.Bitrate = dr["bitrate"].ToString();
                }
                if (ds.Tables[0].Columns.Contains("ClipLength"))
                {
                    v.ClipLength = dr["ClipLength"].ToString();
                }
                if (ds.Tables[0].Columns.Contains("ClipSize"))
                {
                    v.ClipSize = dr["ClipSize"].ToString();
                }
            }
            return v;
        }


        public ImageInfo GetImageInfoBySN(string sn)
        {
            ResourceService rs = new ResourceService();
            ImageInfo o = null;
            DataSet ds = rs.GetResourceInfoBySN(sn);
            if (ds.Tables[0].Rows.Count > 0)
            {
                o = new ImageInfo();
                DataRow dr = ds.Tables[0].Rows[0];
                o.Width = 0;
                o.Height = 0;
                o.Hvsp = string.Empty;

                if (ds.Tables[0].Columns.Contains("Width"))
                {
                    o.Width = Convert.ToInt32(dr["Width"].ToString());
                }
                if (ds.Tables[0].Columns.Contains("Height"))
                {
                    o.Height = Convert.ToInt32(dr["Height"].ToString());
                }
                if (ds.Tables[0].Columns.Contains("Hvsp"))
                {
                    o.Hvsp = dr["Hvsp"].ToString();
                }
            }
            return o;
        }





        /// <summary>
        /// ���ݿ�ʼ�ͽ������ڼ��û�����ѯ�����¼
        /// </summary>
        /// <param name="username"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataSet SearchDownloadManagerByLoginName(string username)
        {
            //return QJVRMS.DataAccess.ImageStorage.SearchDownloadManagerByLoginNameAndDate(username);
            //QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
            ResourceService rs = new ResourceService();
            return rs.GetDownloadMessageByLoginName(username);
        }

        /// <summary>
        /// ��ȡ�ض��û����ڵ����ؼ�¼��Ϣ
        /// </summary>
        /// <param name="username"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataSet GetDownLoadMessage(string username, DateTime beginDate, DateTime endDate)
        {
            // return QJVRMS.DataAccess.ImageStorage.GetDownLoadMessage(username, beginDate, endDate);
           // QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
            ResourceService rs = new ResourceService();
            return rs.GetDownLoadMessage(username, beginDate, endDate);
        }


        /// <summary>
        /// ɾ�����ؼ�¼
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public static bool DeleteDownLog(int logId)
        {
            //QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
            ResourceService rs = new ResourceService();
            return rs.DeleteDownMessage(logId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filaName"></param>
        /// <param name="fileType"></param>
        /// <param name="downusername"></param>
        /// <param name="usage"></param>
        /// <param name="enduser"></param>
        /// <param name="folder"></param>
        /// <param name="Errflag"></param>
        /// <param name="resourceType"></param>
        public static void Production_Hires_Down_Log(string filaName, string fileType, string downusername, string usage, string enduser, string folder, bool Errflag,string resourceType)
        {

            //ImageStorageService iss = new ImageStorageService();
            ResourceService rs = new ResourceService();
            rs.Production_Hires_Down_Log(filaName, fileType, downusername, usage, enduser, folder, Errflag,resourceType);
        }


        /// <summary>
        /// ������Դ�Ķ������ԣ� ���ͼƬ����Ƶ����
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="de"></param>
        public void insertResourceAttributes(string serialNumber, DictionaryEntry[] de)
        {
            ResourceService rs = new ResourceService();
            rs.insertResourceAttributes(serialNumber, de);   
        
        }

        /// <summary>
        /// ����ĳ����Դ�����ķ��࣬1����Դ�������ڶ������
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public DataSet GetResourceCatalogByItemId(string itemId)
        {
            ResourceService rs = new ResourceService();
            return rs.GetResourceCatalogByItemId(itemId);
        }


        /// <summary>
        /// ��Ӹ���
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="fileName"></param>
        /// <param name="fileLength"></param>
        /// <returns></returns>
        public static bool AddAttach(string itemId, string fileName,long fileLength)
        {
            ResourceService rs = new ResourceService();
            return rs.AddAttach( itemId,fileName,fileLength);
        }

        /// <summary>
        /// ����ĳ����Դ�����и����б�
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static DataTable GetAttachList(Guid itemId)
        {
            ResourceService rs = new ResourceService();
            return rs.GetAttachList(itemId);
        }

        public static void DeleteAttach(Guid attId)
        {
            ResourceService rs = new ResourceService();
            rs.DeleteAttach(attId);
        }


        /// <summary>
        /// ɾ��һ����Դ
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static bool DeleteResource(Guid itemId)
        {
            ResourceService rs = new ResourceService();
            
            //ͬʱ��������
            DataSet ds = rs.GetResourceInfoByItemId(itemId.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                string[] SNs = new string[] { ds.Tables[0].Rows[0]["ItemSerialNumber"].ToString() };
                ResourceIndex.deleteIndex(SNs);
            }
            
            return rs.DeleteResource(itemId);
        }


        /// <summary>
        /// ƽ��ʱ�õ�
        /// </summary>
        /// <param name="catalogId"></param>
        /// <param name="userId"></param>
        /// <param name="deptID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static DataSet SearchByDept(Guid catalogId, Guid userId, Guid deptID, int pageSize, int pageIndex)
        {

            /** ʹ������������ʼ *****************************/
            if (ResourceIndex.IsUsingIndex)
            {
                string Catalogid = catalogId.ToString();
                string groupid = deptID.ToString();
                string keyword = string.Empty;
                string beginDate=string.Empty;
                string endDate=string.Empty;
                string Userid = userId.ToString();
                int PageSize = pageSize;
                int PageNum = pageIndex;
                string resourceType = "video,image,document,other";
                int pageCount=0;

                if (Catalogid.Equals(new Guid().ToString()))
                {
                    Catalogid = "";
                }
                else
                {
                    Catalogid = " " + Catalogid;
                }

                if (!string.IsNullOrEmpty(groupid.Trim()))
                {
                    groupid = " " + groupid;
                }
                else
                {
                    groupid = "";
                }

                PageNum++;

                DataSet ds= ResourceIndex.Search(keyword+Catalogid+groupid, beginDate, endDate,  Userid, PageSize, PageNum, ref pageCount, resourceType);
                DataSet newDS=new DataSet();
                newDS.Tables.Add(ds.Tables[1].DefaultView.ToTable());
                newDS.Tables.Add(ds.Tables[0].DefaultView.ToTable());

                return newDS;

            }
            /** ʹ�������������� *******************************/


            QJVRMS.Business.SearchWS.SearchService ss = new QJVRMS.Business.SearchWS.SearchService();
            return ss.SearchByDept(catalogId, userId, deptID, pageSize, pageIndex);
        }



        /// <summary>
        /// ��ȡ��˲�ͨ������Դ
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public DataSet GetNotPassResources(string userName, DateTime startDate, DateTime endDate, int pageSize, int pageIndex)
        {
            ResourceService rs = new ResourceService();
            return rs.GetNotPassResources(userName, startDate, endDate, pageSize, pageIndex);        
        }


        /// <summary>
        /// �ж�ĳ���û���ĳ����Դ�Ƿ����ĳ������Ȩ��
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public bool IsUserResource(Guid userId, Guid resourceId,int method)
        {
            bool _b = false;
            DataSet ds = this.GetResourceCatalogByItemId(resourceId.ToString());
            int icount = ds.Tables[0].Rows.Count;
            List<ObjectRule> rules = new List<ObjectRule>(icount);
      
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ISecurityObject securityObj = new SecurityObject(new Guid(dr["CatalogId"].ToString()), SecurityObjectType.Items);
                ObjectRule or = new ObjectRule(securityObj, new User(userId), (OperatorMethod)method);
                rules.Add(or);                
            }
            ObjectRule.CheckRules(rules);
            foreach (ObjectRule obj in rules)
            {
                _b = _b || obj.IsValidate;
            }
            return _b;
            
        }


        /// <summary>
        /// ��Դ״̬��id name ��0 ���ϴ�
        /// </summary>
        /// <returns></returns>
        public static DataTable GetResourceStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID",typeof(int));
            dt.Columns.Add("CnName",typeof(string));

            DataRow dr ;

            dr = dt.NewRow();
            dr["ID"] = (int)ResourceEntity.ResourceStatus.NewUpload;
            dr["CnName"] = "���ϴ�";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)ResourceEntity.ResourceStatus.IsProcessing;
            dr["CnName"] = "�����";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)ResourceEntity.ResourceStatus.IsPass;
            dr["CnName"] = "������";

            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = (int)ResourceEntity.ResourceStatus.NotPass;
            dr["CnName"] = "δͨ��";

            dt.Rows.Add(dr);

            return dt;
        }


        /// <summary>
        /// ����ĳ����������������Դ�б�
        /// </summary>
        /// <param name="orderId">����ID</param>
        /// <returns></returns>
        public static DataSet GetResourcesByOrderId(string orderId)
        {
            ResourceService rs = new ResourceService();
            return rs.GetResourcesByOrderId(orderId);

        }

        /// <summary>
        /// ������Դ���������
        /// </summary>
        /// <param name="id">��Դ ID</param>
        public void UpdateResourceViewCount(string id)
        {
            ResourceService rs = new ResourceService();
            rs.UpdateResourceViewCount(id);

        }

        public DataSet GetResourceImagesDetail(string id) {
            FeatureWS.FeatureService fs = new QJVRMS.Business.FeatureWS.FeatureService();
            return fs.GetResourceImagesDetail(id);
        }

        public DataSet GetRecourcesImages(string id) {
            FeatureWS.FeatureService fs = new QJVRMS.Business.FeatureWS.FeatureService();
            return fs.GetResourcesImages(id);
        }

        /// <summary>
        /// �ҵ��ϴ���ͳ�Ʋ��ֵ�
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataSet GetMyUploadStatus(DateTime startDate, DateTime endDate, Guid userid)
        {
            ResourceService rs = new ResourceService();
            return rs.GetMyUploadStatus(startDate, endDate, userid);
        }

        /// <summary>
        /// ���û����ܵ�
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataSet GetNewResourceStatByUser(Guid userid)
        {
            ResourceService rs = new ResourceService();
            return rs.GetNewResourceStatByUser(userid);
        }


        /// <summary>
        /// ����Ա���Ƿ�����ͼƬ ����
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsAlertAdmin(Guid userId,string isSuperAdmin)
        {
            ResourceService rs = new ResourceService();
            return rs.IsAlertAdmin(userId, isSuperAdmin);
        }


        /// <summary>
        /// ���ÿ�˵��ղؼ�
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>
        public DataSet GetMyLightBox(Guid userId)
        {
            ResourceService rs = new ResourceService();
            return rs.GetMyLightBox(userId);
        }

         /// <summary>
        /// ���ĳ���ղؼ��������ͼƬ
        /// </summary>
        /// <param name="id">�ղؼ�id</param>
        /// <returns></returns>
        public DataSet GetResourcesByLightBoxID(Guid id,int pageSize,int pageIndex)
        {
            ResourceService rs = new ResourceService();
            return rs.GetResourcesByLightBoxID(id,pageSize,pageIndex);
        }

         /// <summary>
        /// ���һ���ղؼУ����û�����
        /// </summary>
        /// <param name="title"></param>
        /// <param name="userId"></param>
        /// <param name="zipFilePath"></param>
        /// <param name="zipFileExpireDate"></param>
        /// <returns></returns>

        public bool AddLightBox(string title, Guid userId, string zipFilePath, DateTime zipFileExpireDate)
        {
            ResourceService rs = new ResourceService();
            return rs.AddLightBox(title, userId, zipFilePath, zipFileExpireDate);
        }


        /// <summary>
        /// ����һ���ղؼУ���Ҫ���Ǹ�����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="zipFilePath"></param>
        /// <param name="zipFileExpireDate"></param>
        /// <returns></returns>
        public bool EditLightBox(Guid id, string title, string zipFilePath, DateTime zipFileExpireDate)
        {
            ResourceService rs = new ResourceService();
            return rs.EditLightBox(id, title, zipFilePath, zipFileExpireDate);
        }


         /// <summary>
        /// ɾ��һ���ղؼУ���ͬ�����ղص�ͼƬ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelLightBox(Guid id)
        {
            ResourceService rs = new ResourceService();
            return rs.DelLightBox(id);
        }



         /// <summary>
        /// ���ղؼ������ͼƬ
        /// </summary>
        /// <param name="resourceId">ͼƬid</param>
        /// <param name="lightboxId">�ղؼ�id</param>
        /// <returns></returns>
        public bool AddToLightBox(Guid resourceId, Guid lightboxId)
        {
            ResourceService rs = new ResourceService();
            return rs.AddToLightBox(resourceId, lightboxId);
        }


        /// <summary>
        /// ���ղؼ����Ƴ�ͼƬ
        /// </summary>
        /// <param name="resourceId">ͼƬid</param>
        /// <param name="lightboxId">�ղؼ�id</param>
        /// <returns></returns>
        public bool DelFromLightBox(Guid resourceId, Guid lightboxId)
        {
            ResourceService rs = new ResourceService();
            return rs.DelFromLightBox(resourceId, lightboxId);
        }

        
        /// <summary>
        /// ����ղؼ�
        /// </summary>
        /// <param name="lightboxId">�ղؼ�Id</param>
        /// <returns></returns>
        public bool ClearLightBox(Guid lightboxId)
        {
            ResourceService rs = new ResourceService();
            return rs.ClearLightBox(lightboxId);
        }


        /// <summary>
        /// Ϊĳ�˴���Ĭ���ղؼ�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CreateDefaultLightbox(Guid userId)
        {
            ResourceService rs = new ResourceService();
            return rs.CreateDefaultLightbox(userId);
        }



        public bool SaveDeletedImage(Guid logId, byte[] imageData)
        {
            ResourceService rs = new ResourceService();
            return rs.SaveDeletedImage(logId, imageData);

        }
        public byte[] GetDeletedImage(Guid logId)
        {
            ResourceService rs = new ResourceService();
            return rs.GetDeletedImage(logId);
        }
    }
}
