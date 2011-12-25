using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.Business.SecurityControl;
using System.Data;
using QJVRMS.DataAccess;
using System.Data.SqlClient;
using QJVRMS.Business.FunctionWS;

namespace QJVRMS.Business
{
    /// <summary>
    /// 系统功能
    /// </summary>
   
    public partial class Function : ISecurityObject
    {
        private Guid objectId;


        /// <summary>
        /// 获取所有Function列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetFunctionTableList()
        {
            //string sql = " select * from functionList order by orderFlag asc";
            //return SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql).Tables[0];

            FunctionService fs = new FunctionService();
            return fs.GetFunctionTableList();
        }

      
        public static bool  GetUserFunctionRight(Guid userID)
        {
            //SqlParameter[] Parameters = new SqlParameter[1];
            //Parameters[0] = new SqlParameter("@UserId", SqlDbType.VarChar);
            //Parameters[0].Value = userID;

            //string sql = "select count(*) from dbo.AccessControlList where OperatorId in (select RoleId from dbo.Users_InRoles where UserId=@UserId) and ObjectId in (select FunctionId from  FunctionList)";
            //string strCount = SqlHelper.ExecuteScalar(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters).ToString();
            //return strCount == "0" ? false : true;
            FunctionService fs = new FunctionService();
            return fs.GetUserFunctionRight(userID);
        }

        /// <summary>
        /// 获取拥有的function
        /// </summary>
        /// <param name="operId"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static DataTable GetOwnFunction(IOperator oper, OperatorMethod method)
        {
//            SecurityObjectType objType = SecurityObjectType.Function;

//            string sql = @"select f.functionId,f.FunctionName from accessControlList a,FunctionList f where
//                            a.operatorId=@operId and a.OperatorMethod=@operMethod and a.ObjectType=@objType and a.ObjectId=f.FunctionId";

//            SqlParameter[] Parameters = new SqlParameter[3];


//            Parameters[0] = new SqlParameter("@operId", SqlDbType.UniqueIdentifier);
//            Parameters[1] = new SqlParameter("@operMethod", SqlDbType.TinyInt);
//            Parameters[2] = new SqlParameter("@objType", SqlDbType.TinyInt);


//            Parameters[0].Value = oper.OperatorId;
//            Parameters[1].Value = (int)method;
//            Parameters[2].Value = (int)objType;

//            return SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters).Tables[0];

            FunctionService fs = new FunctionService();
            return fs.GetOwnFunction(oper.OperatorId, (int)method);
        }

        public Guid ObjectId
        {
            get
            {
                return this.objectId;
            }
            set
            {
                this.objectId = value;
            }
        }

        public SecurityObjectType ObjectType
        {
            get
            {
                return SecurityObjectType.Function;
            }
            set
            {
                 
            }
            
        }

      
    }
}
