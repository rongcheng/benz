using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using QJVRMS.DataAccess;
using System.IO;



namespace QJVRMS.Business
{
    [Serializable]
    public class ImageStorage : IImageStorage
    {
        private Guid m_ItemId;
        private Guid m_userId;
        private Guid groupId;
        private string m_ItemSerialNum;
        private string m_FileName;
        private string m_FolderName;
        private string m_Caption;
        private string m_Address;
        private string m_Character;
        private DateTime m_StartDate;
        private DateTime m_EndDate;
        private DateTime m_uploadDate;
        private DateTime m_shotDate;
        private string m_Keyword;
        private string m_Description;
        private string m_ImageType;

        private string m_Hvsp;//add by dtf 08-05-30
        private long _filesize;

        #region IImageStorage ��Ա

        /// <summary>
        /// ͼƬ��״��ʶ
        /// </summary>
        public string Hvsp
        {
            get { return m_Hvsp; }
            set { m_Hvsp = value; }
        }

        public Guid ItemId
        {
            get
            {
                return m_ItemId;
            }
            set
            {
                m_ItemId = value;
            }

        }

        public Guid GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        public Guid userId
        {
            get
            {

                return m_userId;
            }
            set
            {

                m_userId = value;
            }
        }

        public string ItemSerialNum
        {
            get
            {

                return m_ItemSerialNum;
            }
            set
            {

                m_ItemSerialNum = value;
            }
        }
        public string FileName
        {
            get
            {

                return m_FileName;
            }
            set
            {

                m_FileName = value;
            }
        }


        public string FolderName
        {
            get
            {

                return m_FolderName;
            }
            set
            {

                m_FolderName = value;
            }
        }
        public string Caption
        {
            get
            {

                return m_Caption;
            }
            set
            {

                m_Caption = value;
            }
        }
        public string Address
        {
            get
            {

                return m_Address;
            }
            set
            {

                m_Address = value;
            }
        }
        public string Character
        {
            get
            {

                return m_Character;
            }
            set
            {

                m_Character = value;
            }
        }
        public DateTime StartDate
        {
            get
            {

                return m_StartDate;
            }
            set
            {

                m_StartDate = value;
            }
        }
        public DateTime EndDate
        {
            get
            {

                return m_EndDate;
            }
            set
            {

                m_EndDate = value;
            }
        }

        public DateTime uploadDate
        {
            get
            {

                return m_uploadDate;
            }
            set
            {

                m_uploadDate = value;
            }
        }

        public DateTime shotDate
        {
            get
            {

                return m_shotDate;
            }
            set
            {

                m_shotDate = value;
            }
        }
        public string Keyword
        {
            get
            {

                return m_Keyword;
            }
            set
            {

                m_Keyword = value;
            }
        }
        public string Description
        {
            get
            {

                return m_Description;
            }
            set
            {

                m_Description = value;
            }
        }
        public string ImageType
        {
            get
            {

                return m_ImageType;
            }
            set
            {

                m_ImageType = value;
            }
        }

        public long FileSize
        {
            set { _filesize = value; }
            get { return _filesize; }
        }

        /// <summary>
        /// ɾ��ͼƬ��Ϣ
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        //public void Delete()
        //{
        //    string sql = "DELETE   FROM ImageStorage   WHERE  ItemId ='" + this.ItemId + "'";
        //    SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql);
        //}

        /// <summary>
        /// �޸�ͼƬ��Ϣ
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        //public void Store()
        //{
        //    string sql = "UPDATE ImageStorage  ";
        //    sql = sql + "  SET FileName='" + this.FileName + "'";
        //    sql = sql + "  ,shotDate='" + this.shotDate + "'";
        //    sql = sql + " ,Keyword='" + this.Keyword + "'";
        //    sql = sql + " ,Description='" + this.Description + "'";
        //    sql = sql + "  WHERE  ItemId ='" + this.ItemId + "'";

        //    SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql);

        //    ImageStorageClass oImageStorageClass = new ImageStorageClass();
        //    oImageStorageClass.DeleteRelationshipImageAndCatalog(this.ItemId);
        //}

        /// <summary>
        /// ���ݹؼ�������ͼƬ add by dtf 5-30 
        /// ��ȡ ItemSerialNum,Hvsp ����ֵ
        /// </summary>
        /// <returns></returns>
        //public static DataTable SearchImageByKeyword(string keyword, int PageSize, int PageNum, ref int rowCount)
        //{
        //    //return QJVRMS.DataAccess.ImageStorage.SearchImageByKeyword(keyword, PageSize, PageNum, ref rowCount);
        //    QJVRMS.Business.SearchWS.SearchService ss = new QJVRMS.Business.SearchWS.SearchService();
        //    return ss.SearchImageByKeyword(keyword, PageSize, PageNum, ref rowCount);
        //}
        /// <summary>
        ///���ؼ��ֻ�ͼƬ��ţ��ϴ�ʱ�䣬��������ͼƬ
        /// dailing
        /// </summary>
        /// <returns></returns>
        public static DataTable SearchImage(string keyword, string beginDate, string endDate, string Catalogid, string Userid, int PageSize, int PageNum, ref int rowCount)
        {
            //return QJVRMS.DataAccess.ImageStorage.SearchImage(keyword, uploadDate, shotDate, Catalogid,Userid, PageSize, PageNum, ref rowCount);
            QJVRMS.Business.SearchWS.SearchService ss = new QJVRMS.Business.SearchWS.SearchService();
            return ss.SearchImage(keyword, beginDate, endDate, Catalogid, Userid, PageSize, PageNum, ref rowCount);
        }

        public static DataSet SearchImage(int pageSize, int pageIndex, out int pageCount, string keyword, DateTime beg, DateTime end, Guid catalogId, Guid userId)
        {
            pageCount = 0;
          

            string sqlWhere = string.Empty;

            if (!string.IsNullOrEmpty(keyword))
            {
                sqlWhere += " And (Keyword like '%" + keyword + "%' or Caption like '%" + keyword + "%' or ItemSerialNum like '%"+keyword+"%')";
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

        public static DataSet SearchByDept(Guid catalogId, Guid userId, Guid deptID, int pageSize, int pageIndex)
        {
            QJVRMS.Business.SearchWS.SearchService ss = new QJVRMS.Business.SearchWS.SearchService();
            return ss.SearchByDept(catalogId, userId, deptID, pageSize, pageIndex);
        }

        /// <summary>
        /// ���ݹؼ�������ͼƬ 
        /// ��ȡ ItemSerialNum,Hvsp ����ֵ
        /// </summary>
        /// <returns></returns>
        //public static DataTable SearchImageByCataID(string CatalogID, string UserID, int PageSize, int PageNum, ref int rowCount)
        //{
        //   // return QJVRMS.DataAccess.ImageStorage.SearchImageByCataID(CatalogID, UserID, PageSize, PageNum, ref rowCount);
        //    QJVRMS.Business.SearchWS.SearchService ss = new QJVRMS.Business.SearchWS.SearchService();
        //    return ss.SearchImageByCataID(CatalogID, UserID, PageSize, PageNum, ref rowCount);
        //}

        /// <summary>
        /// add by dtf 08-06-03 ��ȡ�����û�������Ϣ
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserByAll()
        {
            // return QJVRMS.DataAccess.ImageStorage.GetUserByAll();
            QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
            return iss.GetUserByAll();
        }

        /// <summary>
        /// add by dtf 08-06-03 ���ݿ�ʼ�ͽ������ڼ��û�����ѯ�����¼
        /// </summary>
        /// <param name="username"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataSet SearchDownloadManagerByLoginName(string username)
        {
            //return QJVRMS.DataAccess.ImageStorage.SearchDownloadManagerByLoginNameAndDate(username);
            QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
            return iss.GetDownloadMessageByLoginName(username);
        }

        /// <summary>
        /// add by dtf 08-06-04 ��ȡ�ض��û����ڵ����ؼ�¼��Ϣ
        /// </summary>
        /// <param name="username"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataSet GetDownLoadMessage(string username, DateTime beginDate, DateTime endDate)
        {
            // return QJVRMS.DataAccess.ImageStorage.GetDownLoadMessage(username, beginDate, endDate);
            QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
            return iss.GetDownLoadMessage(username, beginDate, endDate);
        }


        public static bool DeleteDownLog(int logId)
        {
            QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
            return iss.DeleteDownMessage(logId);
        }

        public static DataSet GetImageCatalog(string imageID)
        {
            // return QJVRMS.DataAccess.ImageStorage.GetImageCatalog(imageID);
            QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
            return iss.GetImageCatalog(imageID);
        }

        public static ImageStorage GetImageStorageModel(Guid itemId)
        {
            QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
            DataTable dt = iss.GetImageInfoById(itemId);
            if (dt.Rows.Count > 0)
            {
                return ParseImageStorage(dt.Rows[0]);
            }
            return null;
        }

        #endregion

        public static QJVRMS.Business.ImageStorage ParseImageStorage(DataRow rdr)
        {
            QJVRMS.Business.ImageStorage oIImageStorage = new QJVRMS.Business.ImageStorage();


            oIImageStorage.ItemId = new Guid(Convert.ToString(rdr["ItemId"]));
            if (!Convert.IsDBNull(rdr["userId"]))
            {
                oIImageStorage.userId = new Guid(Convert.ToString(rdr["userId"]));
            }

            if (!Convert.IsDBNull(rdr["ItemSerialNum"]))
            {
                oIImageStorage.ItemSerialNum = Convert.ToString(rdr["ItemSerialNum"]);
            }

            if (!Convert.IsDBNull(rdr["FileName"]))
            {
                oIImageStorage.FileName = Convert.ToString(rdr["FileName"]);
            }

            if (!Convert.IsDBNull(rdr["FolderName"]))
            {
                oIImageStorage.FolderName = Convert.ToString(rdr["FolderName"]);
            }
            if (!Convert.IsDBNull(rdr["uploadDate"]))
            {
                oIImageStorage.uploadDate = Convert.ToDateTime(rdr["uploadDate"]);
            }
            if (!Convert.IsDBNull(rdr["shotDate"]))
            {
                oIImageStorage.shotDate = Convert.ToDateTime(rdr["shotDate"]);
            }

            if (!Convert.IsDBNull(rdr["Keyword"]))
            {
                oIImageStorage.Keyword = Convert.ToString(rdr["Keyword"]);
            }

            if (!Convert.IsDBNull(rdr["Description"]))
            {
                oIImageStorage.Description = Convert.ToString(rdr["Description"]);
            }
            if (!Convert.IsDBNull(rdr["ImageType"]))
            {
                oIImageStorage.ImageType = Convert.ToString(rdr["ImageType"]);
            }
            if (!Convert.IsDBNull(rdr["Hvsp"]))
            {
                oIImageStorage.Hvsp = Convert.ToString(rdr["Hvsp"]);
            }
            if (!Convert.IsDBNull(rdr["Caption"]))
            {
                oIImageStorage.Caption = Convert.ToString(rdr["Caption"]);
            }
            if (!Convert.IsDBNull(rdr["Address"]))
            {
                oIImageStorage.Address = Convert.ToString(rdr["Address"]);
            }
            if (!Convert.IsDBNull(rdr["Character"]))
            {
                oIImageStorage.Character = Convert.ToString(rdr["Character"]);
            }
            if (!Convert.IsDBNull(rdr["StartDate"]))
            {
                oIImageStorage.StartDate = Convert.ToDateTime(rdr["StartDate"]);
            }
            if (!Convert.IsDBNull(rdr["EndDate"]))
            {
                oIImageStorage.EndDate = Convert.ToDateTime(rdr["EndDate"]);
            }

            if (!Convert.IsDBNull(rdr["FileLength"]))
            {
                oIImageStorage.FileSize = Convert.ToInt64(rdr["FileLength"]);
            }
            return oIImageStorage;
        }
        #region

        /// <summary>
        /// �޸�ͼƬ��Ϣ���ļ����ƣ��ؼ��֣������� ---dailing
        /// </summary>
        /// <returns></returns>
        //public static bool UpdateImageStorage(string ItemSerialNum, string FileName, string Keyword, string Description)
        //{
        //    return QJVRMS.DataAccess.ImageStorage.UpdateImageStorage(ItemSerialNum, FileName, Keyword, Description);
        //}

        /// <summary>
        /// ɾ��ͼƬ��Ϣ
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        //public static bool DeleteImageStorage(string ItemSerialNum)
        //{
        //    return QJVRMS.DataAccess.ImageStorage.DeleteImageStorage(ItemSerialNum);
        //}

        /// <summary>
        /// add by dtf 08-06-05 �û���ͳ��
        /// </summary>
        /// <returns></returns>
        //public static DataSet GetUserGrouptj()
        //{
        //    QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
        //    return iss.GetUserGrouptj();
        //    //return QJVRMS.DataAccess.ImageStorage.GetUserGrouptj();
        //}

        /// <summary>
        ///  add by dtf 08-06-04 ��ȡ����ͼƬ����
        /// </summary>
        /// <returns></returns>
        //public static DataSet GetCategoryPicCount()
        //{
        //    //return QJVRMS.DataAccess.ImageStorage.GetCategoryPicCount();
        //    QJVRMS.Business.ImageStorageWS.ImageStorageService iss = new QJVRMS.Business.ImageStorageWS.ImageStorageService();
        //    return iss.GetCategoryPicCount();
        //}

        /// <summary>
        /// ��ȡ��;��Ϣ dtf
        /// </summary>
        /// <returns></returns>
        //public static DataSet getUsage()
        //{
        //    return QJVRMS.DataAccess.ImageStorage.getUsage();
        //}

        /// <summary>
        /// ��ȡ��½�û�����Ϣ add by dtf 08-06-06
        /// </summary>
        /// <param name="lname"></param>
        /// <returns></returns>
        //public static DataSet GetUserProfile(string lname, string userid)
        //{
        //    return QJVRMS.DataAccess.ImageStorage.GetUserProfile(lname, userid);
        //}

        #endregion
    }
}
