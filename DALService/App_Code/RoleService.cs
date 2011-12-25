using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.DataAccess;
using QJVRMS.Business.SecurityControl;
using System.Xml.Serialization;
using QJVRMS.Common;

/// <summary>
/// RoleService 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class RoleService : System.Web.Services.WebService
{

    public RoleService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    

    [WebMethod]
    public DataTable GetRole(Guid roleId)
    {
        string sql = "select * from Roles where roleId=@roleId";
        SqlParameter[] Parameters = new SqlParameter[1];


        Parameters[0] = new SqlParameter("@roleId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = roleId;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];
    }

    [WebMethod]
    public DataTable GetRolesByGroupId(Guid groupId)
    {
        string sql = "select * from Roles where GroupId=@groupId order by RoleName";

        SqlParameter[] Parameters = new SqlParameter[1];


        Parameters[0] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = groupId;


        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];


    }


    [WebMethod]
    public bool CreateRoleUsers(Guid[] rolesId, Guid userId)
    {
        string formatcreateSql = string.Empty;
        formatcreateSql = "insert into users_inroles (userId,roleId) values ('{0}','{1}')";
        string createSql = string.Empty;


        string sql = string.Empty;

        sql = "Begin Tran Begin try ";
        sql += " delete from users_inroles where UserId='{0}' ";
        sql = string.Format(sql, userId.ToString());
        foreach (Guid roleId in rolesId)
        {
            createSql = string.Format(formatcreateSql, userId.ToString(), roleId.ToString());

            sql += createSql;
        }

        sql += " Commit End try ";
        sql += @"Begin Catch  IF @@TRANCOUNT > 0 Rollback "
               + " DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int "
               + " SELECT @ErrMsg = ERROR_MESSAGE(),"
               + " @ErrSeverity = ERROR_SEVERITY() "
               + " RAISERROR(@ErrMsg, @ErrSeverity, 1)"
               + " End Catch";

        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql);

            return true;
        }
        catch
        {
            return false;
        }

    }

    [WebMethod]
    public bool DeleteRole(Guid roleId)
    {
        string sql = "Begin Tran Begin try "
                     + " Delete from Users_inRoles where RoleId=@roleId"
                     + " Delete from AccessControlLIst where OperatorId=@roleId"
                     + " Delete from Roles where RoleId=@roleId"
                     + " Commit End Try"
                    + " Begin Catch  IF @@TRANCOUNT > 0 Rollback "
                    + " DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int "
                    + " SELECT @ErrMsg = ERROR_MESSAGE(),"
                    + " @ErrSeverity = ERROR_SEVERITY() "
                    + " RAISERROR(@ErrMsg, @ErrSeverity, 1)"
                    + " End Catch";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@roleId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = roleId;

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


    //[WebMethod]
    //public bool Deleteuser(Guid userId)
    //{
    //    string sql = "Begin Tran Begin try "
    //                + " Delete from Users_inRoles where UserId=@userId"
    //                + " Delete from Users where UserId=@userId"

    //                + " Commit End Try"
    //                + " Begin Catch  IF @@TRANCOUNT > 0 Rollback "
    //                + " DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int "
    //                + " SELECT @ErrMsg = ERROR_MESSAGE(),"
    //                + " @ErrSeverity = ERROR_SEVERITY() "
    //                + " RAISERROR(@ErrMsg, @ErrSeverity, 1)"
    //                + " End Catch";

    //    SqlParameter[] Parameters = new SqlParameter[1];

    //    Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
    //    Parameters[0].Value = userId;

    //    try
    //    {
    //        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters);
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        // QJVRMS.Common.LogWriter.WriteExceptionLog(ex, true);
    //        return false;
    //    }
    //}

    [WebMethod]
    public Guid NewRole(Guid groupId, string roleName, string description, string securityObjs, int method)
    {
        SqlParameter[] Parameters = new SqlParameter[4];

        Parameters[0] = new SqlParameter("@RoleName", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@description", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);
        Parameters[3] = new SqlParameter("@roleId", SqlDbType.UniqueIdentifier);

        Parameters[3].Direction = ParameterDirection.Output;


        Parameters[0].Value = roleName;
        Parameters[1].Value = description;
        Parameters[2].Value = groupId;

        SerializeObjectFactory sof = new SerializeObjectFactory();
        SecurityObject[] objs = (SecurityObject [])sof.DesializeFromBase64(securityObjs);


        SqlTransaction trans = null;
        Guid roleId;
        using (SqlConnection con = new SqlConnection(CommonInfo.ConQJVRMS))
        {
            con.Open();
            trans = con.BeginTransaction();

            try
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "Role_CreateRole", Parameters);
                roleId = new Guid(Parameters[3].Value.ToString());


                string formatcreateSql = @"insert into accessControlList (ObjectId,ObjectType,OperatorId,OperatorMethod)
                                values ('{0}',{1},'{2}',{3})";

                string sql = string.Empty;

                foreach (ISecurityObject secobj in objs)
                {
                    string secObjId = secobj.ObjectId.ToString();
                    int oType = (int)secobj.ObjectType;
                    int methodIndex = method;
                    sql += string.Format(formatcreateSql, secObjId, oType.ToString(), roleId.ToString(), methodIndex.ToString());


                }
                if (sql != string.Empty)
                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);


                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                QJVRMS.Common.LogWriter.WriteExceptionLog(e, true);
                throw e;
            }


        }

        QJVRMS.Common.LogWriter.WriteLog("S", new string[] { "Test" });

        return roleId;

    }


    [WebMethod]
    public bool ModifyRole(string roleName, string description, Guid roleId, string securityObjs, int method)
    {
        SerializeObjectFactory sof = new SerializeObjectFactory();
        SecurityObject[] objs = (SecurityObject[])sof.DesializeFromBase64(securityObjs);

        string formatcreateSql = string.Empty;
        formatcreateSql = @"insert into accessControlList (ObjectId,ObjectType,OperatorId,OperatorMethod)
                                values ('{0}',{1},'{2}',{3})";
        string createSql = string.Empty;


        string sql = string.Empty;

        sql = "Begin Tran Begin try ";

        sql += "update Roles set RoleName='{0}',Description='{1}' where roleId='{2}'";
        sql = string.Format(sql, roleName, description, roleId.ToString());

        sql += " delete from accessControlList where OperatorId='{0}' ";
        sql = string.Format(sql, roleId.ToString());

        foreach (ISecurityObject secobj in objs)
        {
            string secObjId = secobj.ObjectId.ToString();
            int oType = (int)secobj.ObjectType;
            int methodIndex = method;
            createSql = string.Format(formatcreateSql, secObjId, oType.ToString(), roleId.ToString(), methodIndex.ToString());

            sql += createSql;
        }

        sql += " Commit End try ";
        sql += "Begin Catch  IF @@TRANCOUNT > 0 Rollback"
                + " DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int"
                + " SELECT @ErrMsg = ERROR_MESSAGE(),"
                + " @ErrSeverity = ERROR_SEVERITY()"
                + "RAISERROR(@ErrMsg, @ErrSeverity, 1)"
                + " End Catch";

        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql);

            return true;
        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }


    [WebMethod]
    public DataTable GetUsersOfRole(Guid roleId)
    {

        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@roleId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = roleId;
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "dbo.Role_GetUsersByRole", Parameters).Tables[0];

    }



    [WebMethod]
    public string GetRoleIdByName(string roleName)
    {
        string sql = "select * from Roles where roleName=@roleName";
        SqlParameter[] Parameters = new SqlParameter[1];


        Parameters[0] = new SqlParameter("@roleName", SqlDbType.NVarChar);
        Parameters[0].Value = roleName;


        DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["roleID"].ToString();
        }
        return string.Empty;    
    }


}

