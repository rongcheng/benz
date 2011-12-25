using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.DataAccess;
using QJVRMS.Business.SecurityControl;
using System.Collections.Generic;
using QJVRMS.Business;

using QJVRMS.Common;

/// <summary>
/// FunctionService 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FunctionService : System.Web.Services.WebService
{

    public FunctionService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataTable GetFunctionTableList()
    {
        string sql = " select * from functionList order by orderFlag asc";
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0];
    }

    [WebMethod]
    public string GetTopFunctionList()
    {
        string sql = "SELECT * FROM dbo.FunctionList WHERE parentId IS NULL ORDER BY orderFlag";
        IList<Function> TopFunctionList = new List<Function>();
        using (DataTable table = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0])
        {
            foreach (DataRow row in table.Rows)
            {
                Function f = new Function();
                f.Description = row["Description"].ToString();
                f.FunctionName = row["FunctionName"].ToString();
                f.UrlPath = row["UrlPath"].ToString();
                f.FunctionID = new Guid(row["FunctionId"].ToString());
                f.OrderFlag = int.Parse(row["orderFlag"].ToString());

                if (row["parentid"] == DBNull.Value)
                {
                    f.ParentFunctionId = null;
                }
                else
                {
                    f.ParentFunctionId = new Guid(row["parentId"].ToString());
                }
                
                TopFunctionList.Add(f);
            }
        }
        SerializeObjectFactory sof = new SerializeObjectFactory();
        return sof.SerializeToBase64(TopFunctionList);
    }


    [WebMethod]
    public bool GetUserFunctionRight(Guid userID)
    {
        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = userID;

        string sql = "select count(*) from dbo.AccessControlList where OperatorId in (select RoleId from dbo.Users_InRoles where UserId=@UserId) and ObjectId in (select FunctionId from  FunctionList)";
        string strCount = SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).ToString();

        return strCount != "0";
    }


    [WebMethod]
    public DataTable GetOwnFunction(Guid operatorId, int method)
    {
        SecurityObjectType objType = SecurityObjectType.Function;

        string sql = @"select f.functionId,f.FunctionName from accessControlList a,FunctionList f where
                            a.operatorId=@operId and a.OperatorMethod=@operMethod and a.ObjectType=@objType and a.ObjectId=f.FunctionId";

        SqlParameter[] Parameters = new SqlParameter[3];


        Parameters[0] = new SqlParameter("@operId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@operMethod", SqlDbType.TinyInt);
        Parameters[2] = new SqlParameter("@objType", SqlDbType.TinyInt);


        Parameters[0].Value = operatorId;
        Parameters[1].Value = method;
        Parameters[2].Value = (int)objType;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];
    }


    /// <summary>
    ///  List<Function> 
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string GetFunctionList()
    {

        List<Function> FunctionListAll = new List<Function>();
        using (DataTable table = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Function_GetFunction").Tables[0])
        {
            foreach (DataRow row in table.Rows)
            {
                Function f = new Function();
                f.Description = row["Description"].ToString();
                f.FunctionName = row["FunctionName"].ToString();
                f.UrlPath = row["UrlPath"].ToString();
                f.FunctionID = new Guid(row["FunctionId"].ToString());
                f.OrderFlag = int.Parse(row["orderFlag"].ToString());

                if (row["parentid"] == DBNull.Value)
                {
                    f.ParentFunctionId = null;
                }
                else
                {
                    f.ParentFunctionId = new Guid(row["parentId"].ToString());
                }

                FunctionListAll.Add(f);
            }
        }
        SerializeObjectFactory sof = new SerializeObjectFactory();
        return sof.SerializeToBase64(FunctionListAll);
    }

    [WebMethod]
    public bool DeleteFunctionByFunctionID(Guid FunctionID)
    {

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@FunctionId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = FunctionID;

        int result = SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Function_DeleteFunction", Parameters);
        return result == 1;
    }

    [WebMethod]
    public bool UpdateFunction(Guid funId, string name, string url, string desc, int oflag,Guid? parentFunctionId)
    {

        SqlParameter[] Parameters = new SqlParameter[6];

        Parameters[0] = new SqlParameter("@FunctionId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@FunctionName", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@UrlPath", SqlDbType.VarChar);
        Parameters[3] = new SqlParameter("@Description", SqlDbType.NVarChar);
        Parameters[4] = new SqlParameter("@orderFlag", SqlDbType.TinyInt);
        Parameters[5] = new SqlParameter("@ParentFunctionId", SqlDbType.UniqueIdentifier);


        Parameters[0].Value = funId;
        Parameters[1].Value = name;
        Parameters[2].Value = url;
        Parameters[3].Value = desc;
        Parameters[4].Value = oflag;
        Parameters[5].Value = parentFunctionId;

        int result = SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Function_UpdateFunction", Parameters);
        return result == 1;
    }

    [WebMethod]
    public bool AddFunction(string name,string url,string desc,int orderflag,Guid? parentFunctionId)
    {

        SqlParameter[] Parameters = new SqlParameter[5];

        Parameters[0] = new SqlParameter("@FunctionName", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@UrlPath", SqlDbType.VarChar);
        Parameters[2] = new SqlParameter("@Description", SqlDbType.NVarChar);
        Parameters[3] = new SqlParameter("@orderFlag", SqlDbType.TinyInt);
        Parameters[4] = new SqlParameter("@parentFunctionId", SqlDbType.UniqueIdentifier);



        Parameters[0].Value = name;
        Parameters[1].Value = url;
        Parameters[2].Value = desc;
        Parameters[3].Value = orderflag;
        Parameters[4].Value = parentFunctionId;


        int result = SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Function_AddFunction", Parameters);
        return result == 1;
    }
}

