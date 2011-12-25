using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.DataAccess;


/// <summary>
/// GroupService 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.None)]
public class GroupService : System.Web.Services.WebService
{

    public GroupService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    [WebMethod]
    public DataTable GetGroup(Guid groupId)
    {
        string sql = "select * from [Group] where groupId=@GroupId";
        SqlParameter[] Parameters = new SqlParameter[1];


        Parameters[0] = new SqlParameter("@GroupId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = groupId;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];


    }

    [WebMethod]
    public Guid CreateGroup(string groupName, string description)
    {
        SqlParameter[] Parameters = new SqlParameter[4];

        Parameters[0] = new SqlParameter("@GroupName", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@CreateDate", SqlDbType.DateTime);
        Parameters[2] = new SqlParameter("@Description", SqlDbType.NVarChar);
        Parameters[3] = new SqlParameter("@GroupId", SqlDbType.UniqueIdentifier);

        Parameters[3].Direction = ParameterDirection.Output;
        DateTime now = DateTime.Now;

        Parameters[0].Value = groupName;
        Parameters[1].Value = now;
        Parameters[2].Value = description;

        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "dbo.Group_CreateGroup", Parameters);

            return new Guid(Parameters[3].Value.ToString());
        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            throw ex;
        }

    }

    [WebMethod]
    public Guid CreateChildGroup(Guid parentId, string groupName,int orderFlag)
    {
        string sql = "Insert Into [Group] (GroupId,GroupName,CreateDate,ParentId,GroupOrder) values (@gId,@gName,@cDate,@pId,@groupOrder)";

        SqlParameter[] Parameters = new SqlParameter[5];

        Parameters[0] = new SqlParameter("@gId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@gName", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@cDate", SqlDbType.DateTime);
        Parameters[3] = new SqlParameter("@pId", SqlDbType.UniqueIdentifier);
        Parameters[4] = new SqlParameter("@GroupOrder", SqlDbType.VarChar);

        Guid groupId = Guid.NewGuid();

        Parameters[0].Value = groupId;
        Parameters[1].Value = groupName;
        Parameters[2].Value = DateTime.Now;
        Parameters[3].Value = parentId;
        Parameters[4].Value = orderFlag;

        try
        {
              SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters);
              return groupId;

        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            throw ex;
        }

         
    }


    [WebMethod]
    public bool DeleteGroup(Guid groupId)
    {
        string sql = "Delete from [Group] Where GroupId=@gId";

        SqlParameter[] Parameters = new SqlParameter[1];


        Parameters[0] = new SqlParameter("@gId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = groupId;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
        }

        return false;
    }

    [WebMethod]
    public bool ModifyGroup(Guid groupId, string groupName, int orderFlag)
    {
        string sql = "Update [Group] Set GroupName=@gName,groupOrder=@orderFlag Where GroupId=@gId";

        SqlParameter[] Parameters = new SqlParameter[3];


        Parameters[0] = new SqlParameter("@gId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@gName", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@orderFlag", SqlDbType.VarChar);

        Parameters[0].Value = groupId;
        Parameters[1].Value = groupName;
        Parameters[2].Value = orderFlag;

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
    }

    [WebMethod]
    public DataTable GetTopGroup()
    {
        string sql = "Declare @rootId uniqueidentifier"
                    + " Select @rootId=GroupId from [Group] Where parentId is null"
                    + " Select * from [Group] Where parentId=rootId";


        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0];
    }

    [WebMethod]
    public DataTable GetChildGroup(Guid parentId)
    {
        string sql = "Select * from Group Where ParentId=@parentId";

        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@parentId", SqlDbType.UniqueIdentifier);

        Parameters[0].Value = parentId;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];

    }


    [WebMethod]
    public DataTable GetRootGroup()
    {
        string sql = "select  * from [Group] Where parentId is null";

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0];
    }

    [WebMethod]
    public DataTable SearchGroup(string groupName)
    {
        string sql = "select * from [Group] Where GroupName like @gName";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@gName", SqlDbType.NVarChar);
        Parameters[0].Value = groupName;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];
    }

    [WebMethod]
    public DataTable GetGroupList()
    {
        string sql = "select  * from [Group] Order By groupOrder asc";

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0];
    }

    [WebMethod]
    public DataTable GetUsersByGroupId(Guid groupId)
    {

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = groupId;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Group_GetUsersByGroupId", Parameters).Tables[0];

    }

    [WebMethod(BufferResponse = true, Description = "SearchUsers", CacheDuration = 0, EnableSession = false, MessageName = "SUSQL")]
    public DataTable SearchUsers(string sqlWhere)
    {
        string sql = "select * from users where 0=0";

       
        if (!string.IsNullOrEmpty(sqlWhere))
        {
            sql += " and " + sqlWhere;


        }

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0];
    }

    [WebMethod(BufferResponse = true, Description = "SearchUsers", CacheDuration = 0, EnableSession = false, MessageName = "SUPARM")]
    public DataTable SearchUsers(Guid groupid, string loginName, string userName)
    {
        string sql = "select * from users where loginName=@loginName";

        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@loginName", SqlDbType.NVarChar);
        Parameters[0].Value = loginName;


        if (!string.IsNullOrEmpty(userName))
        {
            sql += " And userName=@userName";
            Array.Resize<SqlParameter>(ref Parameters, Parameters.Length + 1);
            Parameters[Parameters.Length - 1] = new SqlParameter("@userName", SqlDbType.NVarChar);
            Parameters[Parameters.Length - 1].Value = userName;
        }
       
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];

    }

    [WebMethod]
    public DataSet GetGroupUsersStat()
    {

        string sql = "Select GroupId,GroupName From [Group]"
                    + " Select r.roleName,g.groupId,count(*) as Amount from"
                    + " [Group] g,Roles r,Users_InRoles u"
                    + " where g.groupId=r.groupId and r.roleId=u.roleId"
                    + " Group by g.groupId,r.RoleName";


        try
        {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql);
        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return null;
        }
    }


    [WebMethod]
    public DataTable GetAllGroups(string spaceChar)
    {
        string sql = "Group_GetAllGroups";
        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@spaceChar", SqlDbType.NVarChar,20);
        Parameters[0].Value = spaceChar;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, sql, Parameters).Tables[0];
    }

    [WebMethod]
    public string GetGroupIdByGroupName(string groupName)
    {
        string sql = "select * from [Group] where groupName=@groupName";
        SqlParameter[] Parameters = new SqlParameter[1];


        Parameters[0] = new SqlParameter("@groupName", SqlDbType.VarChar);
        Parameters[0].Value = groupName;

        DataTable dt= SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["GroupID"].ToString();   
        }
        return string.Empty;    
    }


}

