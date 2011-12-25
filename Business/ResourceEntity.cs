using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business
{
    /// <summary>
    /// ��Դʵ����
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
        /// ����������guid
        /// </summary>
        public Guid GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        /// <summary>
        /// �ϴ�����Դ���û�guid
        /// </summary>
        public Guid userId
        {
            get{return m_userId;}
            set{m_userId = value;}
        }

        /// <summary>
        /// ��Դ���ڲ����
        /// </summary>
        public string ItemSerialNum
        {
            get{return m_ItemSerialNum;}
            set{m_ItemSerialNum = value;}
        }

        /// <summary>
        ///�����ڿͻ����ϵ�ԭʼ�ļ���
        /// </summary>
        public string FileName
        {
            get{return m_FileName;}
            set{m_FileName = value;}
        }

        /// <summary>
        /// �����ڷ������ϵ�·�����������û���
        /// </summary>
        public string FolderName
        {
            get{return m_FolderName;}
            set{m_FolderName = value;}
        }

        /// <summary>
        ///�����ڷ������ϵ��ļ���
        /// </summary>
        public string ServerFileName
        {
            get { return m_ServerFileName; }
            set { m_ServerFileName = value; }
        }

        /// <summary>
        /// ��Դ����
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
        /// ��Ч�ڿ�ʼ����
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
        /// ��Ч�ڽ�������
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
        /// �ϴ�����
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
        /// ��������
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
        /// ��������
        /// </summary>
        public DateTime updateDate
        {
            get { return m_updateDate; }
            set { m_updateDate = value; }
        }

        /// <summary>
        /// �ؼ���
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
        /// ��ϸ����
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
        /// ��Դ״̬��һ���ĸ���0:���ϴ���1:���ύ��2:ͨ����ˣ�3:δͨ�����
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
        /// �������������
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
        /// ������������
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
