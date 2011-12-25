using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using QJVRMS.Business;
using QJVRMS.DataAccess;
using System.Xml.Serialization;
using QJVRMS.Common;
using System.Data;

using System.DirectoryServices;
using ActiveDs;
using QJVRMS.Business.SecurityControl;
using System.Collections.Generic;
/// <summary>
/// MemberShipService 的摘要说明
/// 
/// 用户相关操作
/// 
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class MemberShipService : System.Web.Services.WebService
{

    public MemberShipService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    /// <summary>
    /// 添加AD用户到DB
    /// </summary>
    /// <param name="userList"></param>
    /// <param name="groupid"></param>
    /// <returns></returns>
    [WebMethod]
    public bool AddADUsersToDB(string userListStr, Guid groupid)
    {
        DataTable userTable = new DataTable();
        userTable.Columns.Add("UserId", typeof(Guid));
        userTable.Columns.Add("GroupId", typeof(Guid));
        userTable.Columns.Add("loginName", typeof(string));
        userTable.Columns.Add("UserName", typeof(string));
        userTable.Columns.Add("Tel", typeof(string));
        userTable.Columns.Add("Email", typeof(string));
        userTable.Columns.Add("uType", typeof(string));

        userTable.Columns.Add("password", typeof(string));
        userTable.Columns.Add("isLocked", typeof(string));
        userTable.Columns.Add("isDownload", typeof(string));
        userTable.Columns.Add("isIpValidate", typeof(string));
        userTable.Columns.Add("IpAddress", typeof(string));
        userTable.Columns.Add("CreateDate", typeof(DateTime));



        SerializeObjectFactory sof = new SerializeObjectFactory();
        ArrayList userList = (ArrayList)sof.DesializeFromBase64(userListStr);

        foreach (object ouser in userList)
        {
            QJVRMS.Business.User user = ouser as QJVRMS.Business.User;

            DataRow userRow = userTable.NewRow();

            userRow["UserId"] = user.UserId;
            userRow["GroupId"] = groupid;
            userRow["loginName"] = user.UserLoginName;
            userRow["UserName"] = user.UserName;
            userRow["Tel"] = user.Telphone;
            userRow["Email"] = user.Email;
            userRow["uType"] = "1";
            userRow["password"] = "123";
            userTable.Rows.Add(userRow);
        }

        SqlConnection con = null;
        SqlTransaction trans = null;

        try
        {
            con = new SqlConnection(CommonInfo.ConQJVRMS);
            con.Open();

            trans = con.BeginTransaction();
            SqlHelperExtend.Update("Users", userTable, trans);

            trans.Commit();
            return true;
        }
        catch (Exception ex)
        {
            trans.Rollback();
            LogWriter.WriteExceptionLog(ex);

            return false;
        }
        finally
        {
            if (con != null) con.Close();
        }
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
            LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }


    /// <summary>
    /// 检查用户是否存在AD中 
    /// </summary>
    /// <param name="domainName"></param>
    /// <param name="OU"></param>
    /// <param name="adminId"></param>
    /// <param name="adminPwd"></param>
    /// <param name="listUserStr"></param>
    /// <returns></returns>
    [WebMethod]
    public string CheckUsers(string domainName, string OU, string adminId, string adminPwd, string listUserStr)
    {
        List<User> userList = new List<User>();

        SerializeObjectFactory sof = new SerializeObjectFactory();

        try
        {

            List<string> userIdList = (List<string>)sof.DesializeFromBase64(listUserStr);
            ADHelper.SearchUser(domainName, OU, adminId, adminPwd, userIdList, userList);
            return sof.SerializeToBase64(userList);


        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return string.Empty;
        }

    }

    [WebMethod]
    public string AuthUserByAD(string domain, string loginfullName, string loginId, string password)
    {
        IADsUser adUser = null;

        try
        {
            adUser = ADHelper.AuthenticateUser(domain, loginfullName, loginId, password);

            User user = new User();
            user.UserId = new Guid(adUser.GUID);
            user.GroupId = Guid.NewGuid();
            user.UserName = adUser.FullName;
            user.GroupName = string.Empty;
            user.IsDownLoad = string.Empty;
            //   returnObj = user;

            SerializeObjectFactory sof = new SerializeObjectFactory();
            return sof.SerializeToBase64(user);

        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return string.Empty;
        }
    }


    [WebMethod]
    public string AuthClientUser(string loginName, string password)
    {
        string encryptPassword = Encryption.Encrypt(password);

        string sql = "select UserId,UserName from Users"
                    + " where loginName=@loginName and password = @password and IsLocked=0";
        SqlParameter[] Parameters = new SqlParameter[2];

        Parameters[0] = new SqlParameter("@loginName", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@password", SqlDbType.VarChar, 50);

        Parameters[0].Value = loginName;
        Parameters[1].Value = encryptPassword;

        try
        {
            using (DataTable resTable = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0])
            {
                if (resTable.Rows.Count != 0)
                {

                    DataRow reader = resTable.Rows[0];
                    User user = new User();
                    user.UserId = new Guid(reader["UserId"].ToString());

                    user.UserName = reader["UserName"].ToString();

                    SerializeObjectFactory sof = new SerializeObjectFactory();

                    Quanjing.Security.FTPInfo ftp = new Quanjing.Security.FTPInfo(CommonInfo.FtpAddress, CommonInfo.FtpUser, CommonInfo.FtpPwd, "");

                    ftp.SetUserId(user.UserId);
                    return sof.SerializeToBase64(ftp);
                    // return true;
                }
                else
                {
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return null;
        }

    }

    [WebMethod]
    public string AuthUserByForm(string loginName, string password, string IPAddress)
    {

        string encryptPassword = Encryption.Encrypt(password);
        SqlParameter[] Parameters = new SqlParameter[4];
        Parameters[0] = new SqlParameter("@loginName", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@password", SqlDbType.VarChar, 50);
        Parameters[2] = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 15);
        Parameters[3] = new SqlParameter("@IsValidated", SqlDbType.Bit);

        Parameters[0].Value = loginName;
        Parameters[1].Value = encryptPassword;
        Parameters[2].Value = IPAddress;
        Parameters[3].Direction = ParameterDirection.Output;

        try
        {
            using (DataTable resTable = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "dbo.Users_ValidateUserAndGetUser", Parameters).Tables[0])
            {
                if (resTable.Rows.Count != 0)
                {
                    if (!bool.Parse(Parameters[3].Value.ToString()))
                    {
                        return null;
                    }
                    DataRow reader = resTable.Rows[0];
                    User user = new User();
                    user.UserId = new Guid(reader["UserId"].ToString());
                    user.GroupId = new Guid(reader["Groupid"].ToString());
                    user.UserName = reader["UserName"].ToString();
                    user.GroupName = reader["GroupName"].ToString();
                    user.IsDownLoad = reader["IsDownLoad"].ToString();
                    //  returnObj = user;


                    SerializeObjectFactory sof = new SerializeObjectFactory();
                    return sof.SerializeToBase64(user);
                    // return true;
                }
                else
                {
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return null;
        }

    }

    [WebMethod]
    public bool GetUserRight(string userId, string ObjectId)
    {
        try
        {
            SqlParameter[] Parameters ={ 
                    new SqlParameter("@userId", userId), 
                     new SqlParameter("@ObjectId", ObjectId),
                };
            string count = SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, "Users_GetRight", Parameters).ToString();
            return (count == "1") ? true : false;
        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }

    }

    [WebMethod]
    public bool ModifyUserInfo(Guid userId, Guid groupId, string userName, string email, string tel, bool islocked, string isdownload, bool isIPValidate)
    {

        string sql = "update Users set userName=@userName,GroupId=@groupId,tel=@tel,email=@email,IsLocked=@islocked,isdownload=@isdownload,isIPValidate=@isIPValidate,IPAddress = '' where userId=@userId";

        SqlParameter[] Parameters = new SqlParameter[8];

        Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@userName", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@tel", SqlDbType.VarChar);
        Parameters[3] = new SqlParameter("@email", SqlDbType.VarChar);
        Parameters[4] = new SqlParameter("@islocked", SqlDbType.Bit);
        Parameters[5] = new SqlParameter("@isdownload", SqlDbType.Bit);
        Parameters[6] = new SqlParameter("@isIPValidate", SqlDbType.Bit);
        Parameters[7] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);

        Parameters[0].Value = userId;
        Parameters[1].Value = userName;
        Parameters[2].Value = tel;
        Parameters[3].Value = email;
        Parameters[4].Value = islocked;
        Parameters[5].Value = isdownload;
        Parameters[6].Value = isIPValidate;

        Parameters[7].Value = groupId;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }

    }


    [WebMethod]
    public bool ModifyUserInfo1(Guid userId, Guid groupId, string userName, string email, string tel, bool islocked, bool isdownload, bool isIPValidate)
    {

        string sql = "update Users set userName=@userName,GroupId=@groupId,tel=@tel,email=@email,IsLocked=@islocked,isdownload=@isdownload,isIPValidate=@isIPValidate,IPAddress = '' where userId=@userId";

        SqlParameter[] Parameters = new SqlParameter[8];

        Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@userName", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@tel", SqlDbType.VarChar);
        Parameters[3] = new SqlParameter("@email", SqlDbType.VarChar);
        Parameters[4] = new SqlParameter("@islocked", SqlDbType.Bit);
        Parameters[5] = new SqlParameter("@isdownload", SqlDbType.Bit);
        Parameters[6] = new SqlParameter("@isIPValidate", SqlDbType.Bit);
        Parameters[7] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);

        Parameters[0].Value = userId;
        Parameters[1].Value = userName;
        Parameters[2].Value = tel;
        Parameters[3].Value = email;
        Parameters[4].Value = islocked;
        Parameters[5].Value = isdownload;
        Parameters[6].Value = isIPValidate;

        Parameters[7].Value = groupId;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }

    }



    [WebMethod]
    public bool ResetPassword(Guid userId, string newPassword)
    {
        string sql = "Update Users set password=@newpwd where userId=@userId";
        SqlParameter[] Parameters = new SqlParameter[2];

        Parameters[0] = new SqlParameter("@newpwd", SqlDbType.VarChar);
        Parameters[1] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);

        Parameters[0].Value = Encryption.Encrypt(newPassword);
        Parameters[1].Value = userId;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
        }
        catch
        {
            return false;
        }
    }

    [WebMethod]
    public bool ChangePassword(Guid userId, string oldPassword, string newPassword)
    {
        string sql = "Update Users Set Password=@newpwd where UserId=@UserId and Password=@oldpwd";

        SqlParameter[] Parameters = new SqlParameter[3];

        Parameters[0] = new SqlParameter("@newpwd", SqlDbType.VarChar);
        Parameters[1] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[2] = new SqlParameter("@oldpwd", SqlDbType.VarChar);

        Parameters[0].Value = Encryption.Encrypt(newPassword);
        Parameters[1].Value = userId;
        Parameters[2].Value = Encryption.Encrypt(oldPassword);

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
        }
        catch
        {
            return false;
        }


    }

    [WebMethod]
    public Guid CreateUser(string password, string loginName, string userName, Guid groupId, string email, string tel, bool islocked, string isdownload, bool isIPValidate)
    {

        QJVRMS.Business.User user = null;
        Guid userId;
        DateTime nowTime = DateTime.Now;
        SqlParameter[] Parameters = new SqlParameter[12];

        Parameters[0] = new SqlParameter("@loginName", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@userName", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);
        Parameters[3] = new SqlParameter("@password", SqlDbType.VarChar);
        Parameters[4] = new SqlParameter("@tel", SqlDbType.VarChar);
        Parameters[5] = new SqlParameter("@email", SqlDbType.VarChar);
        Parameters[6] = new SqlParameter("@createDate", SqlDbType.DateTime);
        Parameters[7] = new SqlParameter("@islocked", SqlDbType.Bit);
        Parameters[8] = new SqlParameter("@isdownload", SqlDbType.Bit);
        Parameters[9] = new SqlParameter("@isIPValidate", SqlDbType.Bit);
        Parameters[10] = new SqlParameter("@NewUserId", SqlDbType.UniqueIdentifier);
        Parameters[11] = new SqlParameter("@ReturnValue", SqlDbType.Int);


        Parameters[0].Value = loginName;
        Parameters[1].Value = userName;
        Parameters[2].Value = groupId;
        Parameters[3].Value = Encryption.Encrypt(password);
        Parameters[4].Value = tel;
        Parameters[5].Value = email;
        Parameters[6].Value = nowTime;
        Parameters[7].Value = islocked;
        Parameters[8].Value = isdownload;
        Parameters[9].Value = isIPValidate;
        Parameters[10].Direction = ParameterDirection.Output;
        Parameters[11].Direction = ParameterDirection.ReturnValue;


        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Users_createUser", Parameters);
            userId = new Guid(Parameters[10].Value.ToString());

            if ((Parameters[11].Value != null ? (int)Parameters[11].Value : -1) == 0)
            {
                // user = new User(loginName, userName, userId, groupId, false, email, tel, nowTime, isdownload, isIPValidate);
                return userId;
            }

            return Guid.Empty;

        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return Guid.Empty;
        }


    }


    [WebMethod]
    public string GetUserByLoginName(string loginName)
    {
        string sql = " select u.*,g.GroupName from Users u,[Group] g where u.loginName=@loginName and u.groupId=g.groupId";
        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@loginName", SqlDbType.NVarChar);
        Parameters[0].Value = loginName;

        QJVRMS.Business.User user = null;
        Guid groupId, UserId;
        bool isLocked, isIPValidate;
        string UserName, Email, Telphone, isdownload, groupName;
        DateTime createDate;

        using (IDataReader reader = SqlHelper.ExecuteReader(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters))
        {
            if (!reader.Read())
            {
                throw new Exception("用户登录ID不存在!");
            }

            groupId = new Guid(reader["groupId"].ToString());
            isLocked = bool.Parse(reader["IsLocked"].ToString());
            isIPValidate = bool.Parse(reader["IsIPValidate"].ToString());
            isdownload = reader["IsDownLoad"].ToString();
            UserId = new Guid(reader["UserId"].ToString());
            UserName = reader["Username"].ToString();
            groupName = reader["groupName"].ToString(); 

            Email = reader["email"].ToString();
            Telphone = reader["Tel"].ToString();
            createDate = DateTime.Parse(reader["CreateDate"].ToString());
        }

        user = new User(loginName, UserName, UserId, groupId, isLocked, Email, Telphone, createDate, isdownload, isIPValidate);
        user.GroupName = groupName;
        SerializeObjectFactory sof = new SerializeObjectFactory();
        return sof.SerializeToBase64(user);

    }

    [WebMethod]
    public string GetUserById(Guid userId)
    {
        //string sql = "select * from Users where loginName=@loginName and IsLocked=0";
        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = userId;

        QJVRMS.Business.User user = null;
        Guid groupId;
        bool isLocked;
        bool isIPValidate;
        string loginName, UserName, Email, Telphone, isdownload, groupName;
        DateTime createDate;

        using (IDataReader reader = SqlHelper.ExecuteReader(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "dbo.Users_GetUserByUserId", Parameters))
        {
            if (!reader.Read())
            {
                throw new Exception("用户ID不存在!");
            }

            groupId = new Guid(reader["groupId"].ToString());
            isLocked = bool.Parse(reader["IsLocked"].ToString());
            isIPValidate = bool.Parse(reader["IsIPValidate"].ToString());
            isdownload = reader["IsDownLoad"].ToString();
            loginName = reader["logInName"].ToString();
            UserName = reader["Username"].ToString();
            groupName = reader["groupName"].ToString();

            Email = reader["email"].ToString();
            Telphone = reader["Tel"].ToString();
            createDate = DateTime.Parse(reader["CreateDate"].ToString());
        }

        user = new User(loginName, UserName, userId, groupId, isLocked, Email, Telphone, createDate, isdownload, isIPValidate);
        user.GroupName = groupName;
        SerializeObjectFactory sof = new SerializeObjectFactory();
        return sof.SerializeToBase64(user);

    }

    [WebMethod]
    public bool IsUserExist(string loginName)
    {

        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@loginName", SqlDbType.NVarChar);
        Parameters[0].Value = loginName.Replace("'", "''");
        string sql = "select count(*) from Users where loginName = @loginName";
        object obj = SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters);

        if (obj != null && int.Parse(obj.ToString()) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}

