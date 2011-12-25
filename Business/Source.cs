using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using QJVRMS.DataAccess;


namespace QJVRMS.Business
{
    public class Source:ISource
    {
        #region 成员变量
        private int sourceID;
        private string sourceName;
        private string sourceDesc;
        private Guid groupID;
        #endregion
        #region 属性
        public int SourceID
        {
            get
            {
                return this.sourceID;
            }
            set
            {
                this.sourceID = value;
            }
        }
        public string SourceName
        {
            get
            {
                return this.sourceName;
            }
            set
            {
                this.sourceName = value;
            }
        }
        public string SourceDesc
        {
            get
            {
                return this.sourceDesc;
            }
            set
            {
                this.sourceDesc = value;
            }
        }
        public Guid GroupID
        {
            get
            {
                return this.groupID;
            }
            set
            {
                this.groupID = value;
            }
        }
        #endregion
        #region 方法
        /// <summary>
        /// 根据groupID得到所有用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static List<Source> GetSourceList()
        {

        

            //List<Source> sourceList = new List<Source>();
            //using (DataTable table = SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Source_GetSource").Tables[0])
            //{
            //    foreach (DataRow row in table.Rows)
            //    {
            //        Source u = new Source();
                   
            //        u.sourceID = int.Parse(row["SourceID"].ToString());
            //        u.sourceName = row["SourceName"].ToString();
            //        u.sourceDesc = row["SourceDesc"].ToString();
            //        sourceList.Add(u);
            //    }
            //}

            //return sourceList;
            QJVRMS.Business.BaseInfoWS.BaseInfoService bis = new QJVRMS.Business.BaseInfoWS.BaseInfoService();
            List<Source> sourceList = new List<Source>();
            using (DataTable table = bis.GetSourceTable())
            {
                foreach (DataRow row in table.Rows)
                {
                    Source u = new Source();

                    u.sourceID = int.Parse(row["SourceID"].ToString());
                    u.sourceName = row["SourceName"].ToString();
                    u.sourceDesc = row["SourceDesc"].ToString();
                    sourceList.Add(u);
                }
            }

            return sourceList;
        }





        /// <summary>
        /// 根据SourceID删除某项用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static bool DeleteSourceBySourceID(int SourceID)
        {

            //SqlParameter[] Parameters = new SqlParameter[1];

            //Parameters[0] = new SqlParameter("@SourceID", SqlDbType.Int);
            //Parameters[0].Value = SourceID;

            //int result = SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Source_DeleteSource", Parameters);
            //return result == 1;

            QJVRMS.Business.BaseInfoWS.BaseInfoService bis = new QJVRMS.Business.BaseInfoWS.BaseInfoService();
            return bis.DeleteSourceBySourceID(SourceID);
        }

        /// <summary>
        /// 更新某项用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static bool UpdateSource(Source sc)
        {

            //SqlParameter[] Parameters = new SqlParameter[3];

            //Parameters[0] = new SqlParameter("@SourceID", SqlDbType.Int);
            //Parameters[1] = new SqlParameter("@SourceName", SqlDbType.NVarChar);
            //Parameters[2] = new SqlParameter("@SourceDesc", SqlDbType.NVarChar);

            //Parameters[0].Value = sc.SourceID;
            //Parameters[1].Value = sc.SourceName;
            //Parameters[2].Value = sc.SourceDesc;

            //int result = SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Source_UpdateSource", Parameters);
            //return result == 1;
            QJVRMS.Business.BaseInfoWS.BaseInfoService bis = new QJVRMS.Business.BaseInfoWS.BaseInfoService();
            return bis.UpdateSource(sc.SourceID, sc.SourceName, sc.SourceDesc);

        }

        /// <summary>
        /// 添加某项用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static bool AddSource(Source sc)
        {

            //SqlParameter[] Parameters = new SqlParameter[3];


            //Parameters[0] = new SqlParameter("@SourceName", SqlDbType.NVarChar);
            //Parameters[1] = new SqlParameter("@SourceDesc", SqlDbType.NVarChar);
            //Parameters[2] = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier);

            //Parameters[0].Value = sc.SourceName;
            //Parameters[1].Value = sc.SourceDesc;
            //Parameters[2].Value = sc.GroupID;

            //int result = SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Source_AddSource", Parameters);
            //return result == 1;

            QJVRMS.Business.BaseInfoWS.BaseInfoService bis = new QJVRMS.Business.BaseInfoWS.BaseInfoService();
            return bis.AddSource(sc.SourceName, sc.SourceDesc);
        }
        #endregion
    }
}
