using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business
{
    /// <summary>
    /// ��Ƶ�ļ���ʵ����
    /// </summary>
    [Serializable]
    public class VideoStorage:ResourceEntity
    {
        private string m_flvFileName;
        private string m_flvFilePath;
        
        private int m_status;

        private string m_ClipLength;
        private string m_ClipSize;
        private string m_Bitrate;




        public string ClipLength
        {
            get { return m_ClipLength; }
            set { m_ClipLength = value; }
        }

        public string ClipSize
        {
            get { return m_ClipSize; }
            set { m_ClipSize = value; }
        }

        public string Bitrate
        {
            get { return m_Bitrate; }
            set { m_Bitrate = value; }
        }

        


        /// <summary>
        /// flv�ļ���
        /// </summary>
        public string FlvFilename
        {
            get { return m_flvFileName; }
            set { m_flvFileName=value; }
        }

        /// <summary>
        /// flv�ļ�·��
        /// </summary>
        public string FlvFilePath
        {
            set { m_flvFilePath=value; }
            get { return m_flvFilePath; }
        }

        /// <summary>
        /// ��Ƶ�ļ���״̬
        /// </summary>
        public int Status
        {
            set { m_status = value; }
            get { return m_status; }
        }
    }
}
