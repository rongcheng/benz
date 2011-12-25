using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using QJVRMS.DataAccess;

namespace QJVRMS.Business
{
    public class Usage : IUsage
    {
        #region 成员变量
        private int usageID;
        private string usageName;
        private string usageDesc;
        private Guid groupID;
        #endregion
        #region 属性
        public int UsageID
        {
            get
            {
                return this.usageID;
            }
            set
            {
                this.usageID = value;
            }
        }
        public string UsageName
        {
            get
            {
                return this.usageName;
            }
            set
            {
                this.usageName = value;
            }
        }
        public string UsageDesc
        {
            get
            {
                return this.usageDesc;
            }
            set
            {
                this.usageDesc = value;
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


         public  static DataTable usageTable = null;
        /// <summary>
        /// 根据groupID得到所有用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static List<Usage> GetUsageList()
        {

            List<Usage> usageList = new List<Usage>();

            foreach (DataRow row in UsageTable.Rows)
            {
                Usage u = new Usage();

                u.usageID = int.Parse(row["UsageID"].ToString());
                u.usageName = row["UsageName"].ToString();
                u.usageDesc = row["UsageDesc"].ToString();
                usageList.Add(u);
            }


            return usageList;
        }

        public static DataTable UsageTable
        {
            get
            {
                if (usageTable == null)
                {
                    QJVRMS.Business.BaseInfoWS.BaseInfoService bis = new QJVRMS.Business.BaseInfoWS.BaseInfoService();
                    usageTable = bis.GetUsageTable();
                }

                return usageTable;

            }
        }
        /// <summary>
        /// 根据UsageID删除某项用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static bool DeleteUsageByUsageID(int UsageID)
        {

            //SqlParameter[] Parameters = new SqlParameter[1];

            //Parameters[0] = new SqlParameter("@UsageID", SqlDbType.Int);
            //Parameters[0].Value = UsageID;

            //int result = SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Usage_DeleteUsage", Parameters);
            //return result == 1;
            QJVRMS.Business.BaseInfoWS.BaseInfoService bis = new QJVRMS.Business.BaseInfoWS.BaseInfoService();
            return bis.DeleteUsageByUsageID(UsageID);
        }

        /// <summary>
        /// 更新某项用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static bool UpdateUsage(Usage us)
        {

            //SqlParameter[] Parameters = new SqlParameter[3];

            //Parameters[0] = new SqlParameter("@UsageID", SqlDbType.Int);
            //Parameters[1] = new SqlParameter("@UsageName", SqlDbType.NVarChar);
            //Parameters[2] = new SqlParameter("@UsageDesc", SqlDbType.NVarChar);

            //Parameters[0].Value = us.UsageID;
            //Parameters[1].Value = us.UsageName;
            //Parameters[2].Value = us.UsageDesc;

            //int result = SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Usage_UpdateUsage", Parameters);
            //return result == 1;
            QJVRMS.Business.BaseInfoWS.BaseInfoService bis = new QJVRMS.Business.BaseInfoWS.BaseInfoService();
            return bis.UpdateUsage(us.UsageID, us.UsageName, us.UsageDesc);

        }

        /// <summary>
        /// 添加某项用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static bool AddUsage(Usage us)
        {

            //SqlParameter[] Parameters = new SqlParameter[3];


            //Parameters[0] = new SqlParameter("@UsageName", SqlDbType.NVarChar);
            //Parameters[1] = new SqlParameter("@UsageDesc", SqlDbType.NVarChar);
            //Parameters[2] = new SqlParameter("@GroupID", SqlDbType.UniqueIdentifier);

            //Parameters[0].Value = us.UsageName;
            //Parameters[1].Value = us.UsageDesc;
            //Parameters[2].Value = us.GroupID;

            //int result = SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Usage_AddUsage", Parameters);
            //return result == 1;

            QJVRMS.Business.BaseInfoWS.BaseInfoService bis = new QJVRMS.Business.BaseInfoWS.BaseInfoService();
            return bis.AddUsage(us.UsageName, us.UsageDesc);
        }
        #endregion
    }
}
