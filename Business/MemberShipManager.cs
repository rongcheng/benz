using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using QJVRMS.DataAccess;
using QJVRMS.Common;

using System.DirectoryServices;
using ActiveDs;
using QJVRMS.Business.SecurityControl;

namespace QJVRMS.Business
{

    /// <summary>
    /// 用户管理
    /// </summary>
    public class MemberShipManager : IMemberShip
    {

        /*
                 DirectoryEntry de = new DirectoryEntry(
                 "LDAP://192.168.1.100/cn=kblum, ou=sales, dc=ispnet1, dc=net");
              Console.WriteLine("object: {0}", de.Path);
              PropertyCollection pc = de.Properties;
              foreach(string propName in pc.PropertyNames)
              {
                 foreach(object value in de.Properties[propName])
                    Console.WriteLine("  property = {0}   value = {1}",
                       propName, value);
              } 
         */

        public static bool DeleteUser(Guid userId)
        {
            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();

            return mss.DeleteUser(userId);
        }

        public static bool AddADUserToDB(System.Collections.ArrayList userList, Guid groupId)
        {
            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();

            QJVRMS.Common.SerializeObjectFactory sof = new QJVRMS.Common.SerializeObjectFactory();

            string userListStr = sof.SerializeToBase64(userList);
            return mss.AddADUsersToDB(userListStr, groupId);
        }


        public static List<User> CheckUsers(string domainName, string OU, string adminId, string adminPwd, List<string> userIdList)
        {
            List<User> userList = new List<User>();
            // ADHelper.SearchUser(domainName, OU, adminId, adminPwd, userIdList, userList);

            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();

            SerializeObjectFactory sof = new SerializeObjectFactory();

            //System.Collections.ArrayList al = new System.Collections.ArrayList(userIdList.Count);

            //foreach (string var in userIdList)
            //{
            //    al.Add(var);
            //}

            string idString = sof.SerializeToBase64(userIdList);

            string returnUserList = mss.CheckUsers(domainName, OU, adminId, adminPwd, idString);

            object o = sof.DesializeFromBase64(returnUserList);

            List<User> users = (List<User>)o;
            //foreach (IADsUser adUser in adList)
            //{
            //    User user = new User();

            //    user.Email = adUser.EmailAddress;
            //    user.UserLoginName = adUser.Name;
            //    user.UserId = new Guid(adUser.GUID);
            //    user.Telphone = adUser.TelephoneNumber.ToString();

            //    userList.Add(user);
            //}

            return users;
        }

        public bool AuthUserByAD(string domain, string loginfullName, string loginId, string password, ref object returnObj)
        {
            //IADsUser adUser = null;

            //try
            //{
            //    adUser = ADHelper.AuthenticateUser(domain, loginfullName, loginId, password);

            //    User user = new User();
            //    user.UserId = new Guid(adUser.GUID);
            //    user.GroupId = Guid.NewGuid();
            //    user.UserName = adUser.FullName;
            //    user.GroupName = "Test";
            //    user.IsDownLoad = string.Empty;
            //    returnObj = user;


            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    LogWriter.WriteExceptionLog(ex);
            //    return false;
            //}

            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
            SerializeObjectFactory sof = new SerializeObjectFactory();

            try
            {
                string objStr = mss.AuthUserByAD(domain, loginfullName, loginId, password);

                object o = sof.DesializeFromBase64(objStr);
                QJVRMS.Business.User user = (QJVRMS.Business.User)o;

                returnObj = user;

                return true;
            }
            catch
            {
                return false;
            }


        }


        /// <summary>
        /// and IPAddress validate
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <param name="returnObj"></param>
        /// <returns></returns>
        public bool AuthUserByForm(string loginName, string password, string IPAddress, ref object returnObj)
        {

            string encryptPassword = Encryption.Encrypt(password);
            //SqlParameter[] Parameters = new SqlParameter[4];
            //Parameters[0] = new SqlParameter("@loginName", SqlDbType.NVarChar);
            //Parameters[1] = new SqlParameter("@password", SqlDbType.VarChar, 50);
            //Parameters[2] = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 15);
            //Parameters[3] = new SqlParameter("@IsValidated", SqlDbType.Bit);

            //Parameters[0].Value = loginName;
            //Parameters[1].Value = encryptPassword;
            //Parameters[2].Value = IPAddress;
            //Parameters[3].Direction = ParameterDirection.Output;

            //try
            //{
            //    using (DataTable resTable = SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "dbo.Users_ValidateUserAndGetUser", Parameters).Tables[0])
            //    {
            //        if (resTable.Rows.Count != 0)
            //        {
            //            if (!bool.Parse(Parameters[3].Value.ToString()))
            //            {
            //                return false;
            //            }
            //            DataRow reader = resTable.Rows[0];
            //            User user = new User();
            //            user.UserId = new Guid(reader["UserId"].ToString());
            //            user.GroupId = new Guid(reader["Groupid"].ToString());
            //            user.UserName = reader["UserName"].ToString();
            //            user.GroupName = reader["GroupName"].ToString();
            //            user.IsDownLoad = reader["IsDownLoad"].ToString();
            //            returnObj = user;

            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogWriter.WriteExceptionLog(ex, true);
            //    return false;
            //}

            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
            SerializeObjectFactory sof = new SerializeObjectFactory();



            try
            {
                string objStr = null; 
                ///mss.GetUserByLoginName

                string isAuthByRemote = ConfigurationManager.AppSettings["AuthByRemote"];
                if (string.IsNullOrEmpty(isAuthByRemote))
                {
                    isAuthByRemote = "0";
                }


                if (isAuthByRemote.Equals("1"))
                {
                    //objStr = mss.GetUserByLoginName(loginName);
                    bool isUser = mss.IsUserExist(loginName);

                    //表里没有用户，调用集成验证
                    if (!isUser)
                    {
                        return AuthUserByRequest(loginName, password, IPAddress, ref returnObj, true);
                    }
                    else
                    {
                        objStr = mss.GetUserByLoginName(loginName);
                        object o = sof.DesializeFromBase64(objStr);
                        QJVRMS.Business.User user = (QJVRMS.Business.User)o;

                        returnObj = user;
                        //用户不是系统管理员，调用集成验证
                        string superAdminId = ConfigurationManager.AppSettings["superAdminId"];
                        if (user.UserId.ToString().ToLower() != superAdminId.ToLower())
                        {
                            return AuthUserByRequest(loginName, password, IPAddress, ref returnObj, false);
                        }
                        else
                        { 
                            //是管理员，调用数据库验证
                            objStr = mss.AuthUserByForm(loginName, password, IPAddress);
                            if(string.IsNullOrEmpty(objStr))
                            {
                                return false;
                            }
                            else
                            {
                                object o1 = sof.DesializeFromBase64(objStr);
                                QJVRMS.Business.User user1 = (QJVRMS.Business.User)o1;
                                returnObj = user1;
                                return true;    
                            }
                        }

                        //returnObj = user;

                        return true;
                    }
                }
                else
                {
                    objStr = mss.AuthUserByForm(loginName, password, IPAddress);
                    object o = sof.DesializeFromBase64(objStr);
                    QJVRMS.Business.User user = (QJVRMS.Business.User)o;
                    returnObj = user;
                    return true;                
                }




            }
            catch(Exception ex)
            {
                LogWriter.WriteExceptionLog(ex);
                return false;
            }


        }
        public static bool GetUserRight(string userId, string ObjectId)
        {
            //try
            //{
            //    SqlParameter[] Parameters ={ 
            //        new SqlParameter("@userId", userId), 
            //         new SqlParameter("@ObjectId", ObjectId),
            //    };
            //    string count = SqlHelper.ExecuteScalar(SqlHelper.Con_QJVRMS, "dbo.Users_GetRight", Parameters).ToString();
            //    return (count == "1") ? true : false;
            //}
            //catch (Exception ex)
            //{
            //    //LogWriter.WriteExceptionLog(ex, true);
            //    return false;
            //}

            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();

            return mss.GetUserRight(userId, ObjectId);

        }

        public static bool AuthUserByRequest(string loginName, string password, string ipAddress, ref object returnObj, bool isCreated) {
            string addressParam = ConfigurationManager.AppSettings["RequestUrl"];
            string lnParam = ConfigurationManager.AppSettings["LoginNameParamName"];
            string pParam = ConfigurationManager.AppSettings["PasswordParamName"];
            string ipParam = ConfigurationManager.AppSettings["IPParamName"];

            string url;
            if (addressParam.Contains("?"))
            {
                url = addressParam + "&" + lnParam + "=" + loginName + "&" + pParam + "=" + password + "&" + ipParam + "=" + ipAddress;
            }
            else
            {
                url = addressParam + "?" + lnParam + "=" + loginName + "&" + pParam + "=" + password + "&" + ipParam + "=" + ipAddress;                
            }
            
                
            string result = DoGetRequest(url);

            if (result == "0") {

                //这里远程验证如果错误的话，就进行一次数据库验证
                QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
                SerializeObjectFactory sof = new SerializeObjectFactory();
                string objStr = mss.AuthUserByForm(loginName, password, ipAddress);                

                if (!string.IsNullOrEmpty(objStr))
                {
                    object o = sof.DesializeFromBase64(objStr);
                    QJVRMS.Business.User user = (QJVRMS.Business.User)o;
                    returnObj = user;
                    return true;
                }

                return false;
            }
            else {



                //这里先要获得该用户的机构(groupId)和角色(roleId)
                    string[] arrIds = new Boss().GetVrmsId(loginName, password);
                    string roleId = arrIds[0];
                    string groupId = arrIds[1];
                    string email=arrIds[2];

                    if (string.IsNullOrEmpty(roleId))
                    {
                        roleId = ConfigurationManager.AppSettings["RoleID"];
                    }
                    if (string.IsNullOrEmpty(groupId))
                    {
                        groupId = "356b8e9c-005d-47ae-8aad-e7d1d60a1496";
                    }

                    if(string.IsNullOrEmpty(email))
                    {
                        email= loginName + "@quanjing.com";
                    }





                if (isCreated)
                {
                    
                    MemberShipManager msm = new MemberShipManager();
                    //string email = loginName + "@sany.com.cn";
                    IUser u = msm.CreateUser(password, loginName, loginName,
                        new Guid(groupId), email, string.Empty, false, "false", false);
                    QJVRMS.Business.User user = (QJVRMS.Business.User)u;
                    //string roleID = ConfigurationManager.AppSettings["RoleID"];
                    string roleID = roleId;
                    //分配角色
                    Role.CreateRoleUsers(new Guid[] { new Guid(roleID) }, user.UserId);
                    returnObj = user;
                }
                else
                {
                    QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
                    SerializeObjectFactory sof = new SerializeObjectFactory();

                    //string objStr = mss.AuthUserByForm(loginName, password, ipAddress);
                    string objStr = mss.GetUserByLoginName(loginName);

                    object o = sof.DesializeFromBase64(objStr);
                    QJVRMS.Business.User user = (QJVRMS.Business.User)o;
                    returnObj = user;

                    //如果数据库里有这个用户的话，就更新一次密码（这里应该判断一下用户是否相等），更新一下角色、机构和email
                    mss.ResetPassword(user.UserId,password);

                    bool isDownloaded = false;
                    if (user.IsDownLoad.ToLower().Equals("true"))
                    {
                        isDownloaded = true;
                    }
                    mss.ModifyUserInfo1(user.UserId, new Guid(groupId), user.UserName, email, user.Telphone, user.IsLocked, isDownloaded, user.IsIPValidate);

                    Role.CreateRoleUsers(new Guid[] { new Guid(roleId) }, user.UserId);
                    
                    
                    
                }
                
            }

            return true;
        }
        //GET方式发送得结果
        private static string DoGetRequest(string url) {
            HttpWebRequest hwRequest;
            HttpWebResponse hwResponse;

            string result = string.Empty;
            try {
                hwRequest = (HttpWebRequest)WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "GET";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
            }
            catch (System.Exception err) {
                return "0";
            }

            try {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                result = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err) {
                return err.ToString();
            }

            return result;
        }


        /// <summary>
        /// boss验证
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool loginBoss(string userName,string password)
        {
            com.quanjing.boss.WSAuthentication obj = new QJVRMS.Business.com.quanjing.boss.WSAuthentication();
            return obj.GetAuthentication(userName, password);
            
        }



        /// <summary>
        /// boss验证，获取用户的分公司和职位
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string[] GetBossGroup(string userName, string password)
        {
            com.quanjing.boss.WSAuthentication obj = new QJVRMS.Business.com.quanjing.boss.WSAuthentication();
            //return obj.GetAuthentication(userName, password);
            return obj.GetUserInfo(userName, password);
        }



        

        #region IMemberShip 成员

        /// <summary>
        /// 修改用户基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <param name="userName"></param>
        /// <param name="groupId"></param>
        /// <param name="email"></param>
        /// <param name="tel"></param>
        /// <param name="islocked"></param>
        /// <returns></returns>
        public bool ModifyUserInfo(Guid userId, Guid groupId,string userName, string email, string tel, bool islocked, string isdownload, bool isIPValidate)
        {
            //SqlParameter[] Parameters = new SqlParameter[8];

            //Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
            //Parameters[1] = new SqlParameter("@userName", SqlDbType.NVarChar);
            //Parameters[2] = new SqlParameter("@tel", SqlDbType.VarChar);
            //Parameters[3] = new SqlParameter("@email", SqlDbType.VarChar);
            //Parameters[4] = new SqlParameter("@islocked", SqlDbType.Bit);
            //Parameters[5] = new SqlParameter("@isdownload", SqlDbType.Bit);
            //Parameters[6] = new SqlParameter("@isIPValidate", SqlDbType.Bit);
            //Parameters[7] = new SqlParameter("@returnValue", SqlDbType.Int);

            //Parameters[0].Value = userId;
            //Parameters[1].Value = userName;
            //Parameters[2].Value = tel;
            //Parameters[3].Value = email;
            //Parameters[4].Value = islocked;
            //Parameters[5].Value = isdownload;
            //Parameters[6].Value = isIPValidate;

            //Parameters[7].Direction = ParameterDirection.ReturnValue;

            //try
            //{
            //    SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "dbo.Users_ModifyUserInfo", Parameters);
            //    int returnValue = int.Parse(Parameters[7].Value.ToString());

            //    if (returnValue == -1) return false;
            //    else return true;

            //}
            //catch
            //{
            //    return false;
            //}

            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
            return mss.ModifyUserInfo(userId, groupId,userName, email, tel, islocked, isdownload, isIPValidate);

        }

        public bool ResetPassword(Guid userId, string newPassword)
        {
            //string sql = "Update Users set password=@newpwd where userId=@userId";
            //SqlParameter[] Parameters = new SqlParameter[2];

            //Parameters[0] = new SqlParameter("@newpwd", SqlDbType.VarChar);
            //Parameters[1] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);

            //Parameters[0].Value = Encryption.Encrypt(newPassword);
            //Parameters[1].Value = userId;

            //try
            //{
            //    return SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters) > 0;
            //}
            //catch
            //{
            //    return false;
            //}
            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
            return mss.ResetPassword(userId, newPassword);
        }

        public bool ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            //string sql = "Update Users Set Password=@newpwd where UserId=@UserId and Password=@oldpwd";

            //SqlParameter[] Parameters = new SqlParameter[3];

            //Parameters[0] = new SqlParameter("@newpwd", SqlDbType.VarChar);
            //Parameters[1] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
            //Parameters[2] = new SqlParameter("@oldpwd", SqlDbType.VarChar);

            //Parameters[0].Value = Encryption.Encrypt(newPassword);
            //Parameters[1].Value = userId;
            //Parameters[2].Value = Encryption.Encrypt(oldPassword);

            //try
            //{
            //    return SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters) > 0;
            //}
            //catch
            //{
            //    return false;
            //}
            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
            return mss.ChangePassword(userId, oldPassword, newPassword);

        }

        public IUser CreateUser(string password, string loginName, string userName, Guid groupId, string email, string tel, bool islocked, string isdownload, bool isIPValidate)
        {

            IUser user = null;
            Guid userId;
            DateTime nowTime = DateTime.Now;
            //SqlParameter[] Parameters = new SqlParameter[12];

            //Parameters[0] = new SqlParameter("@loginName", SqlDbType.NVarChar);
            //Parameters[1] = new SqlParameter("@userName", SqlDbType.NVarChar);
            //Parameters[2] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);
            //Parameters[3] = new SqlParameter("@password", SqlDbType.VarChar);
            //Parameters[4] = new SqlParameter("@tel", SqlDbType.VarChar);
            //Parameters[5] = new SqlParameter("@email", SqlDbType.VarChar);
            //Parameters[6] = new SqlParameter("@createDate", SqlDbType.DateTime);
            //Parameters[7] = new SqlParameter("@islocked", SqlDbType.Bit);
            //Parameters[8] = new SqlParameter("@isdownload", SqlDbType.Bit);
            //Parameters[9] = new SqlParameter("@isIPValidate", SqlDbType.Bit);
            //Parameters[10] = new SqlParameter("@NewUserId", SqlDbType.UniqueIdentifier);
            //Parameters[11] = new SqlParameter("@ReturnValue", SqlDbType.Int);


            //Parameters[0].Value = loginName;
            //Parameters[1].Value = userName;
            //Parameters[2].Value = groupId;
            //Parameters[3].Value = Encryption.Encrypt(password);
            //Parameters[4].Value = tel;
            //Parameters[5].Value = email;
            //Parameters[6].Value = nowTime;
            //Parameters[7].Value = islocked;
            //Parameters[8].Value = isdownload;
            //Parameters[9].Value = isIPValidate;
            //Parameters[10].Direction = ParameterDirection.Output;
            //Parameters[11].Direction = ParameterDirection.ReturnValue;


            //try
            //{
            //    SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Users_createUser", Parameters);
            //    userId = new Guid(Parameters[10].Value.ToString());

            //    if ((Parameters[11].Value != null ? (int)Parameters[11].Value : -1) == 0)
            //    {
            //        user = new User(loginName, userName, userId, groupId, false, email, tel, nowTime, isdownload, isIPValidate);
            //    }

            //}
            //catch
            //{
            //    return null;
            //}

            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
            userId = mss.CreateUser(password, loginName, userName, groupId, email, tel, islocked, isdownload, isIPValidate);
            user = new User(loginName, userName, userId, groupId, false, email, tel, nowTime, isdownload, isIPValidate);
            return user;
        }

        public User GetUser(string loginName)
        {
            //string sql = "select * from Users where loginName=@loginName and IsLocked=0";
            //SqlParameter[] Parameters = new SqlParameter[1];
            //Parameters[0] = new SqlParameter("@loginName", SqlDbType.NVarChar);
            //Parameters[0].Value = loginName;

            User user = null;
            //Guid groupId, UserId;
            //bool isLocked, isIPValidate;
            //string UserName, Email, Telphone, isdownload;
            //DateTime createDate;

            //using (IDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "dbo.Users_GetUserByLoginName", Parameters))
            //{
            //    if (!reader.Read())
            //    {
            //        throw new Exception("用户登录ID不存在!");
            //    }

            //    groupId = new Guid(reader["groupId"].ToString());
            //    isLocked = bool.Parse(reader["IsLocked"].ToString());
            //    isIPValidate = bool.Parse(reader["IsIPValidate"].ToString());
            //    isdownload = reader["IsDownLoad"].ToString();
            //    UserId = new Guid(reader["UserId"].ToString());
            //    UserName = reader["Username"].ToString();

            //    Email = reader["email"].ToString();
            //    Telphone = reader["Tel"].ToString();
            //    createDate = DateTime.Parse(reader["CreateDate"].ToString());
            //}

            //user = new User(loginName, UserName, UserId, groupId, isLocked, Email, Telphone, createDate, isdownload, isIPValidate);

            //return user;


            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
            string objStr = mss.GetUserByLoginName(loginName);
            SerializeObjectFactory sof = new SerializeObjectFactory();
            object o = sof.DesializeFromBase64(objStr);

            user = (User)o;

            return user;

        }
        //
        public bool IsUserExist(string loginName)
        {
            //string sql = "select * from Users where loginName=@loginName and IsLocked=0";
            //SqlParameter[] Parameters = new SqlParameter[1];
            //Parameters[0] = new SqlParameter("@loginName", SqlDbType.NVarChar);
            //Parameters[0].Value = loginName.Replace("'", "''");
            //string sql = "select count(*) from Users where loginName = @loginName";
            //object obj = SqlHelper.ExecuteScalar(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters);

            //if (obj != null && int.Parse(obj.ToString()) > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
            return mss.IsUserExist(loginName);

        }


        public User GetUser(Guid userId)
        {
            //string sql = "select * from Users where loginName=@loginName and IsLocked=0";
            //SqlParameter[] Parameters = new SqlParameter[1];
            //Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
            //Parameters[0].Value = userId;

             User user = null;
            //Guid groupId;
            //bool isLocked;
            //bool isIPValidate;
            //string loginName, UserName, Email, Telphone, isdownload;
            //DateTime createDate;

            //using (IDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "dbo.Users_GetUserByUserId", Parameters))
            //{
            //    if (!reader.Read())
            //    {
            //        throw new Exception("用户ID不存在!");
            //    }

            //    groupId = new Guid(reader["groupId"].ToString());
            //    isLocked = bool.Parse(reader["IsLocked"].ToString());
            //    isIPValidate = bool.Parse(reader["IsIPValidate"].ToString());
            //    isdownload = reader["IsDownLoad"].ToString();
            //    loginName = reader["logInName"].ToString();
            //    UserName = reader["Username"].ToString();

            //    Email = reader["email"].ToString();
            //    Telphone = reader["Tel"].ToString();
            //    createDate = DateTime.Parse(reader["CreateDate"].ToString());
            //}

            //user = new User(loginName, UserName, userId, groupId, isLocked, Email, Telphone, createDate, isdownload, isIPValidate);

            //return user;

            QJVRMS.Business.MemWS.MemberShipService mss = new QJVRMS.Business.MemWS.MemberShipService();
            string objStr = mss.GetUserById(userId);
            SerializeObjectFactory sof = new SerializeObjectFactory();
            object o = sof.DesializeFromBase64(objStr);

            user = (User)o;

            return user;

        }

        public UserCollection GetAllUser(int pageIndex, int pageSize, string sqlWhere, out int records)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool LockUser(string userId, bool isLocked)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public DataTable GetUsersByRoleId(Guid roleId)
        {
            QJVRMS.Business.UserWS.UserService us = new QJVRMS.Business.UserWS.UserService();
            return  us.GetUsersByRoleId(roleId);        
        }


        /// <summary>
        /// 获得某人的email
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserEmailByUserLoginName(string userName)
        {
            User obj = this.GetUser(userName);
            return obj.Email;
        }

        /// <summary>
        /// 获得某人的email
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserEmailByUserID(string userID)
        {
            User obj = this.GetUser(new Guid(userID));
            return obj.Email;
        }

        
        #endregion
    }
}
