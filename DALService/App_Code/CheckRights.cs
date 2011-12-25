using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.DataAccess;

/// <summary>
/// CheckRights 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CheckRights : System.Web.Services.WebService
{

    public CheckRights()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 获取用户不可访问的类别
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable GetAccessCatalog(Guid userId)
    {
        string sql = "select ObjectId from AccessControlList a where (OperatorId in("
                    + " select RoleId from  Users_InRoles u where userid=@UserID)"
                    + " or OperatorId=@UserID) and a.operatorMethod=6";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = userId;

        try
        {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];
        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return null;
        }

    }
}

