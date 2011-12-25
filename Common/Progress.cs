using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Common
{
    /// <summary>
    /// �ϴ�״̬
    /// </summary>
    public enum UploadState
    {
        /// <summary>
        /// ���ڽ�������
        /// </summary>
        ReceivingData,
        /// <summary>
        /// �����
        /// </summary>
        Complete,
        /// <summary>
        /// �ϴ�����.
        /// </summary>
        Error
    }

    /// <summary>
    /// �ϴ�������Ϣ
    /// </summary>
    public class Progress
    {
        long contentLength = 0;
        long bytesRead;
        DateTime start;
        string uploadId = "";
        UploadState state;

        public Progress(long contentLength, string uploadId)
        {
            this.contentLength = contentLength;
            start = DateTime.Now;
            this.uploadId = uploadId;
        }

        public void SetBytesRead(long bytesRead)
        {
            lock (this)
            {
                this.bytesRead = bytesRead;
            }
        }


        public void SetState(UploadState state)
        {
            lock (this)
            {
                this.state = state;
            }
        }

        /// <summary>
        /// �ܴ�С
        /// </summary>
        public long ContentLength
        {
            get
            {
                return contentLength;
            }
        }

        /// <summary>
        /// ���ϴ���С
        /// </summary>
        public long BytesRead
        {
            get
            {
                return bytesRead;
            }
        }

        /// <summary>
        /// �ϴ�״̬
        /// </summary>
        public UploadState State
        {
            get
            {
                return state;
            }
        }

        /// <summary>
        /// �ϴ���ʼʱ��
        /// </summary>
        public DateTime Start
        {
            get
            {
                return start;
            }
        }

        /// <summary>
        /// Ψһ��־��ǰ�ϴ���UploadID
        /// </summary>
        string UploadId
        {
            get
            {
                return uploadId;
            }
        }
    }
}
