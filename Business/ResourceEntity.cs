using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business
{
    /// <summary>
    /// 资源实体类
    /// </summary>
    public class ResourceEntity
    {
        private Guid m_ItemId;
        private Guid m_userId;
        private Guid groupId;
        private string m_ItemSerialNum;
        private string m_FileName;
        private string m_FolderName;
        private string m_Caption;
        private DateTime m_StartDate;
        private DateTime m_EndDate;
        private DateTime m_uploadDate;
        private DateTime m_shotDate;
        private string m_Keyword;
        private string m_Description;
        private DateTime m_updateDate;

        private string m_ServerFileName;
        private Int32 m_status;
        private long _filesize;

        private string m_resourceType;
        private string m_author;
        private int m_hasCopyright;

        private int m_viewCount;

        public int ViewCount
        {
            get { return m_viewCount; }
            set { m_viewCount = value; }
        }

        public int HasCopyright
        {
            get { return m_hasCopyright; }
            set { m_hasCopyright = value; }
        }

        public string Author
        {
            get { return m_author; }
            set { m_author = value; }
        }

        public string ResourceType
        {
            get { return m_resourceType; }
            set { m_resourceType = value; }
        }

        public long FileSize
        {
            set { _filesize = value; }
            get { return _filesize; }
        }

        public Int32 Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        public Guid ItemId
        {
            get{ return m_ItemId; }
            set{m_ItemId = value;}
        }

        /// <summary>
        /// 所属机构的guid
        /// </summary>
        public Guid GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        /// <summary>
        /// 上传该资源的用户guid
        /// </summary>
        public Guid userId
        {
            get{return m_userId;}
            set{m_userId = value;}
        }

        /// <summary>
        /// 资源的内部编号
        /// </summary>
        public string ItemSerialNum
        {
            get{return m_ItemSerialNum;}
            set{m_ItemSerialNum = value;}
        }

        /// <summary>
        ///保存在客户端上的原始文件名
        /// </summary>
        public string FileName
        {
            get{return m_FileName;}
            set{m_FileName = value;}
        }

        /// <summary>
        /// 保存在服务器上的路径，这里是用户名
        /// </summary>
        public string FolderName
        {
            get{return m_FolderName;}
            set{m_FolderName = value;}
        }

        /// <summary>
        ///保存在服务器上的文件名
        /// </summary>
        public string ServerFileName
        {
            get { return m_ServerFileName; }
            set { m_ServerFileName = value; }
        }

        /// <summary>
        /// 资源标题
        /// </summary>
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
       
        
        /// <summary>
        /// 有效期开始日期
        /// </summary>
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

        /// <summary>
        /// 有效期结束日期
        /// </summary>
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

        /// <summary>
        /// 上传日期
        /// </summary>
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

        /// <summary>
        /// 拍摄日期
        /// </summary>
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


        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime updateDate
        {
            get { return m_updateDate; }
            set { m_updateDate = value; }
        }

        /// <summary>
        /// 关键字
        /// </summary>
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

        /// <summary>
        /// 详细描述
        /// </summary>
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

        [Serializable]
        /// <summary>
        /// 资源状态，一共四个，0:新上传，1:已提交，2:通过审核，3:未通过审核
        /// </summary>
        public enum ResourceStatus
        { 
            NewUpload=0,
            IsProcessing=1,
            IsPass=2,
            NotPass=3
        }


        [Serializable]
        /// <summary>
        /// 缓存的搜索条件
        /// </summary>
        public class SearchCondition
        {
            public SearchContidionType Type
            {
                get;
                set;
            }

            public IList<KeyValuePair<string, string>> Params
            {
                get;
                set;
            }

            public string GetValue(string key)
            {
                string _tmp=string.Empty;
                foreach (KeyValuePair<string, string> kv in Params)
                {
                    if (kv.Key.ToLower().Equals(key.ToLower()))
                    {
                        return kv.Value;
                    }
                }

                return _tmp;
            
            }
        }


        /// <summary>
        /// 搜索条件类型
        /// </summary>
        public enum SearchContidionType
        { 
            ByOrder=0,
            ByIsHot=1,
            ByIsDownloadHot=2,
            BySearch=3
        }
    }
}
