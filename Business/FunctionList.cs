using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using QJVRMS.DataAccess;
using QJVRMS.Business.SecurityControl;
using QJVRMS.Business.FunctionWS;

using QJVRMS.Common;

namespace QJVRMS.Business
{
    [Serializable]
    public partial class Function : ISecurityObject
    {
        #region 字段
        Guid functionID;
        Guid? parentFunctionId;
        string functionName;
        string urlPath;
        string description;
        int orderFlag;
        int type;
        string functionImageName;
        #endregion

        #region 属性
        public Guid FunctionID
        {
            get
            {
                return this.functionID;
            }
            set
            {
                this.functionID = value;
            }
        }

        public Guid? ParentFunctionId
        {
            get { return this.parentFunctionId; }
            set { this.parentFunctionId = value; }
        }
        public string FunctionName
        {
            get
            {
                return this.functionName;
            }
            set
            {
                this.functionName = value;
            }
        }
        public string UrlPath
        {
            get
            {
                return this.urlPath;
            }
            set
            {
                this.urlPath = value;
            }
        }
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        public int OrderFlag
        {
            get
            {
                return this.orderFlag;
            }
            set
            {
                this.orderFlag = value;
            }
        }
        public int Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        public string FunctionImageName
        {
            get
            {
                return this.functionImageName;
            }
            set
            {
                this.functionImageName = value;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// Get All Function
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static List<Function> GetFunctionList()
        {
            SerializeObjectFactory sof = new SerializeObjectFactory();
            FunctionService fs = new FunctionService();
            string funListStr = fs.GetFunctionList();

            object o = sof.DesializeFromBase64(funListStr);

            List<Function> list = (List<Function>)o;

            return list;
            //List<Function> FunctionListAll = new List<Function>();

            //using (DataTable table = SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Function_GetFunction").Tables[0])
            //{
            //    foreach (DataRow row in table.Rows)
            //    {
            //        Function f = new Function();
            //        f.description = row["Description"].ToString();                   
            //        f.functionName = row["FunctionName"].ToString();
            //        f.urlPath = row["UrlPath"].ToString();
            //        f.functionID = row["FunctionId"].ToString();
            //        f.orderFlag = int.Parse(row["orderFlag"].ToString());
            //        FunctionListAll.Add(f);
            //    }
            //}

            //return FunctionListAll;
        }


        public IList<Function> GetTopFunctionList()
        {
            SerializeObjectFactory sof = new SerializeObjectFactory();
            FunctionService fs = new FunctionService();
            string topFunctionList = fs.GetTopFunctionList();
            object o=sof.DesializeFromBase64(topFunctionList);
            IList<Function> list = (IList<Function>)o;

            return list;
        }
        /// <summary>
        /// 根据FunctionID删除某项用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static bool DeleteFunctionByFunctionID(Guid FunctionID)
        {

            //SqlParameter[] Parameters = new SqlParameter[1];

            //Parameters[0] = new SqlParameter("@FunctionId", SqlDbType.UniqueIdentifier);
            //Parameters[0].Value = new Guid(FunctionID);

            //int result = SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Function_DeleteFunction", Parameters);
            //return result == 1;
            FunctionService fs = new FunctionService();
            return fs.DeleteFunctionByFunctionID(FunctionID);

        }

        /// <summary>
        /// 更新某项用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static bool UpdateFunction(Function fl)
        {

            //SqlParameter[] Parameters = new SqlParameter[5];

            //Parameters[0] = new SqlParameter("@FunctionId", SqlDbType.UniqueIdentifier);
            //Parameters[1] = new SqlParameter("@FunctionName", SqlDbType.NVarChar);
            //Parameters[2] = new SqlParameter("@UrlPath", SqlDbType.VarChar);
            //Parameters[3] = new SqlParameter("@Description", SqlDbType.NVarChar);
            //Parameters[4] = new SqlParameter("@orderFlag", SqlDbType.TinyInt);


            //Parameters[0].Value = new Guid(fl.functionID);
            //Parameters[1].Value = fl.functionName;
            //Parameters[2].Value = fl.urlPath;
            //Parameters[3].Value = fl.description;
            //Parameters[4].Value = fl.orderFlag;         

            //int result = SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Function_UpdateFunction", Parameters);
            //return result == 1;
            FunctionService fs = new FunctionService();
            return fs.UpdateFunction(fl.FunctionID, fl.FunctionName, fl.UrlPath, fl.Description, fl.OrderFlag,fl.ParentFunctionId);

        }

        /// <summary>
        /// 添加某项用途
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static bool AddFunction(Function fl)
        {

            //SqlParameter[] Parameters = new SqlParameter[4];

            //Parameters[0] = new SqlParameter("@FunctionName", SqlDbType.NVarChar);
            //Parameters[1] = new SqlParameter("@UrlPath", SqlDbType.VarChar);
            //Parameters[2] = new SqlParameter("@Description", SqlDbType.NVarChar);
            //Parameters[3] = new SqlParameter("@orderFlag", SqlDbType.TinyInt);



            //Parameters[0].Value = fl.functionName;
            //Parameters[1].Value = fl.urlPath;
            //Parameters[2].Value = fl.description;
            //Parameters[3].Value = fl.orderFlag;


            //int result = SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Function_AddFunction", Parameters);
            //return result == 1;
            FunctionService fs = new FunctionService();
          
            return fs.AddFunction(fl.FunctionName, fl.UrlPath, fl.Description, fl.OrderFlag,fl.ParentFunctionId);

        }
        #endregion


    }
}
