using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using QJVRMS.DataAccess;
using System.Data.SqlClient;
 
/// <summary>
/// UserService 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class UserService : System.Web.Services.WebService
{

    public UserService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }
 

    [WebMethod]
    public bool DeleteUser(Guid userId)
    {
        string sql = "Begin Tran Begin try "
                    + " Delete from Users_inRoles where UserId=@userId"
                    + " Update Users Set IsLocked=1 where UserId=@userId"

                    + " Commit End Try"
                    + " Begin Catch  IF @@TRANCOUNT > 0 Rollback "
                    + " DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int "
                    + " SELECT @ErrMsg = ERROR_MESSAGE(),"
                    + " @ErrSeverity = ERROR_SEVERITY() "
                    + " RAISERROR(@ErrMsg, @ErrSeverity, 1)"
                    + " End Catch";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = userId;

        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters);
            return true;
        }
        catch (Exception ex)
        {
            // QJVRMS.Common.LogWriter.WriteExceptionLog(ex, true);
            return false;
        }
    }

    [WebMethod]
    public DataTable GetRolesOfUser(Guid userId)
    {

        string sql = @"select u.roleID,r.groupId, r.roleName from users_inroles u,roles r
                                where u.userId=@userId and u.roleId=r.roleId";

        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = userId;
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];

    }


    [WebMethod]
    public DataTable GetUsersByRoleId(Guid roleId)
    {

        string sql = @"select u.* from users u,users_inroles r
                                where u.userId=r.userId and r.roleId=@roleId";

        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@roleId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value =  roleId;
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];

    }
}

