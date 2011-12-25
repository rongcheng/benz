using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.Business.SecurityControl;
using System.Data;
using System.Data.SqlClient;
using QJVRMS.DataAccess;

namespace QJVRMS.Business
{
    /// <summary>
    /// Author: Sunan
    /// Date: 2008.05.07
    /// </summary>
    /// 
    [Serializable]
    public class Role : IRole
    {

        Guid roleId;
        Guid groupId;
        IGroup owner;

        string description;
        string roleName;

        UserCollection members;


        public Role()
        {

        }

        public Role(Guid roleId)
        {
            this.roleId = roleId;

            //string sql = "select * from Roles where roleId=@roleId";
            //SqlParameter[] Parameters = new SqlParameter[1];


            //Parameters[0] = new SqlParameter("@roleId", SqlDbType.UniqueIdentifier);
            //Parameters[0].Value = roleId;

            //using (IDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters))
            //{
            //    if (!reader.Read())
            //    {
            //        throw new Exception("没有角色存在!");
            //    }

            //    this.roleName = reader["RoleName"].ToString();
            //    this.groupId = new Guid(reader["GroupId"].ToString());
            //    this.description = reader["Description"].ToString();
            //}

            QJVRMS.Business.RoleWS.RoleService rs = new QJVRMS.Business.RoleWS.RoleService();
            using (DataTable dt = rs.GetRole(roleId))
            {
                if (dt.Rows.Count == 0) throw new Exception("没有角色存在!");

                DataRow dr = dt.Rows[0];

                this.roleName = dr["RoleName"].ToString();
                this.groupId = new Guid(dr["GroupId"].ToString());
                this.description = dr["Description"].ToString();
            }
        }

        public Role(Guid roleId, Guid groupId, string roleName, string description)
        {
            this.roleId = roleId;
            this.groupId = groupId;
            this.roleName = roleName;
            this.description = description;
        }


        public string Description
        {
            get { return this.description; }
        }


        public static RoleCollection GetRoleCollection(Guid groupId)
        {
            //string sql = "select * from Roles where GroupId=@groupId order by RoleName";

            //SqlParameter[] Parameters = new SqlParameter[1];


            //Parameters[0] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);
            //Parameters[0].Value = groupId;

            RoleCollection rc = new RoleCollection();
            //using (DataTable table = SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters).Tables[0])
            //{


            //    foreach (DataRow row in table.Rows)
            //    {
            //        Guid roleId = new Guid(row["RoleId"].ToString());
            //        // new Guid(row["GroupId"].ToString());
            //        string roleName = row["roleName"].ToString();
            //        string descr = row["description"].ToString();

            //        Role role = new Role(roleId, groupId, roleName, descr);

            //        rc.Add(role);
            //    }
            //}

            QJVRMS.Business.RoleWS.RoleService rs = new QJVRMS.Business.RoleWS.RoleService();
            using (DataTable dt = rs.GetRolesByGroupId(groupId))
            {
                foreach (DataRow row in dt.Rows)
                {
                    Guid roleId = new Guid(row["RoleId"].ToString());

                    string roleName = row["roleName"].ToString();
                    string descr = row["description"].ToString();

                    Role role = new Role(roleId, groupId, roleName, descr);

                    rc.Add(role);
                }
            }

            return rc;

        }

        /// <summary>
        /// 创建用户用户组
        /// </summary>
        /// <param name="rolesId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CreateRoleUsers(Guid[] rolesId, Guid userId)
        {
            //string formatcreateSql = string.Empty;
            //formatcreateSql = "insert into users_inroles (userId,roleId) values ('{0}','{1}')";
            //string createSql = string.Empty;


            //string sql = string.Empty;

            //sql = "Begin Tran Begin try ";
            //sql += " delete from users_inroles where UserId='{0}' ";
            //sql = string.Format(sql, userId.ToString());
            //foreach (Guid roleId in rolesId)
            //{
            //    createSql = string.Format(formatcreateSql, userId.ToString(), roleId.ToString());

            //    sql += createSql;
            //}

            //sql += " Commit End try ";
            //sql += @"Begin Catch  IF @@TRANCOUNT > 0 Rollback "
            //       + " DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int "
            //       + " SELECT @ErrMsg = ERROR_MESSAGE(),"
            //       + " @ErrSeverity = ERROR_SEVERITY() "
            //       + " RAISERROR(@ErrMsg, @ErrSeverity, 1)"
            //       + " End Catch";

            //try
            //{
            //    SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql);

            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
            QJVRMS.Business.RoleWS.RoleService rs = new QJVRMS.Business.RoleWS.RoleService();
            return rs.CreateRoleUsers(rolesId, userId);


        }


        /// <summary>
        /// 删除用户组
        /// 
        /// I:删除用户组用户
        /// II:删除受控对象
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static bool DeleteRole(Guid roleId)
        {
            //string sql = "Begin Tran Begin try "
            //             + " Delete from Users_inRoles where RoleId=@roleId"
            //             + " Delete from AccessControlLIst where OperatorId=@roleId"
            //             + " Delete from Roles where RoleId=@roleId"
            //             + " Commit End Try"
            //            + " Begin Catch  IF @@TRANCOUNT > 0 Rollback "
            //            + " DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int "
            //            + " SELECT @ErrMsg = ERROR_MESSAGE(),"
            //            + " @ErrSeverity = ERROR_SEVERITY() "
            //            + " RAISERROR(@ErrMsg, @ErrSeverity, 1)"
            //            + " End Catch";

            //SqlParameter[] Parameters = new SqlParameter[1];

            //Parameters[0] = new SqlParameter("@roleId", SqlDbType.UniqueIdentifier);
            //Parameters[0].Value = roleId;

            //try
            //{
            //    SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //   // QJVRMS.Common.LogWriter.WriteExceptionLog(ex, true);
            //    return false;
            //}

            QJVRMS.Business.RoleWS.RoleService rs = new QJVRMS.Business.RoleWS.RoleService();
            return rs.DeleteRole(roleId);
        }

        /// <summary>
        /// 删除用户组
        /// 
        /// I:删除用户组用户
        /// II:删除受控对象
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        //public static bool Deleteuserda(Guid userId)
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
        //        SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // QJVRMS.Common.LogWriter.WriteExceptionLog(ex, true);
        //        return false;
        //    }
        //}


        public static IRole NewRole(Guid groupId, string roleName, string description, SecurityObject[] secObj, OperatorMethod method)
        {
            //SqlParameter[] Parameters = new SqlParameter[4];

            //Parameters[0] = new SqlParameter("@RoleName", SqlDbType.NVarChar);
            //Parameters[1] = new SqlParameter("@description", SqlDbType.NVarChar);
            //Parameters[2] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);
            //Parameters[3] = new SqlParameter("@roleId", SqlDbType.UniqueIdentifier);

            //Parameters[3].Direction = ParameterDirection.Output;


            //Parameters[0].Value = roleName;
            //Parameters[1].Value = description;
            //Parameters[2].Value = groupId;


            //SqlTransaction trans = null;
            Role role = null;

            //            using (SqlConnection con = new SqlConnection(SqlHelper.Con_QJVRMS))
            //            {
            //                con.Open();
            //                trans = con.BeginTransaction();

            //                try
            //                {
            //                    SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "dbo.Role_CreateRole", Parameters);
            //                    Guid roleId = new Guid(Parameters[3].Value.ToString());


            //                    string formatcreateSql = @"insert into accessControlList (ObjectId,ObjectType,OperatorId,OperatorMethod)
            //                                values ('{0}',{1},'{2}',{3})";

            //                    string sql = string.Empty;

            //                    foreach (ISecurityObject secobj in secObj)
            //                    {
            //                        string secObjId = secobj.ObjectId.ToString();
            //                        int oType = (int)secobj.ObjectType;
            //                        int methodIndex = (int)method;
            //                        sql += string.Format(formatcreateSql, secObjId, oType.ToString(), roleId.ToString(), methodIndex.ToString());


            //                    }
            //                    if( sql != string.Empty )
            //                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql);

            //                    role = new Role(roleId, groupId, roleName, description);


            //                    trans.Commit();
            //                }
            //                catch (Exception e)
            //                {
            //                    trans.Rollback();
            //                   // QJVRMS.Common.LogWriter.WriteExceptionLog(e, true);
            //                    throw e;
            //                }


            //            }
            QJVRMS.Common.SerializeObjectFactory sof = new QJVRMS.Common.SerializeObjectFactory();
            string objStr = sof.SerializeToBase64(secObj);

            QJVRMS.Business.RoleWS.RoleService rs = new QJVRMS.Business.RoleWS.RoleService();
            Guid roleId = rs.NewRole(groupId, roleName, description, objStr, (int)method);
            role = new Role(roleId, groupId, roleName, description);

            return role;

        }


        public static bool ModifyRole(string roleName, string description, Guid roleId, SecurityObject[] secObj, OperatorMethod method)
        {
            //            string formatcreateSql = string.Empty;
            //            formatcreateSql = @"insert into accessControlList (ObjectId,ObjectType,OperatorId,OperatorMethod)
            //                                values ('{0}',{1},'{2}',{3})";
            //            string createSql = string.Empty;


            //            string sql = string.Empty;

            //            sql = "Begin Tran Begin try ";

            //            sql += "update Roles set RoleName='{0}',Description='{1}' where roleId='{2}'";
            //            sql = string.Format(sql, roleName, description, roleId.ToString());

            //            sql += " delete from accessControlList where OperatorId='{0}' ";
            //            sql = string.Format(sql, roleId.ToString());

            //            foreach (ISecurityObject secobj in secObj)
            //            {
            //                string secObjId = secobj.ObjectId.ToString();
            //                int oType = (int)secobj.ObjectType;
            //                int methodIndex = (int)method;
            //                createSql = string.Format(formatcreateSql, secObjId, oType.ToString(), roleId.ToString(), methodIndex.ToString());

            //                sql += createSql;
            //            }

            //            sql += " Commit End try ";
            //            sql += "Begin Catch  IF @@TRANCOUNT > 0 Rollback"
            //                    + " DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int"
            //                    + " SELECT @ErrMsg = ERROR_MESSAGE(),"
            //                    + " @ErrSeverity = ERROR_SEVERITY()"
            //                    + "RAISERROR(@ErrMsg, @ErrSeverity, 1)"
            //                    + " End Catch";

            //            try
            //            {
            //                SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql);

            //                return true;
            //            }
            //            catch (Exception e)
            //            {
            //              //  QJVRMS.Common.LogWriter.WriteExceptionLog(e, true);
            //                return false;
            //            }
            QJVRMS.Common.SerializeObjectFactory sof = new QJVRMS.Common.SerializeObjectFactory();
            string objStr = sof.SerializeToBase64(secObj);

            QJVRMS.Business.RoleWS.RoleService rs = new QJVRMS.Business.RoleWS.RoleService();
            return rs.ModifyRole(roleName, description, roleId, objStr, (int)method);


        }

        public UserCollection Members
        {
            get
            {
                if (this.members == null)
                {

                    this.members = new UserCollection();
                    //SqlParameter[] Parameters = new SqlParameter[1];

                    //Parameters[0] = new SqlParameter("@roleId", SqlDbType.UniqueIdentifier);
                    //Parameters[0].Value = this.roleId;

                    //using (DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Role_GetUsersByRole", Parameters).Tables[0])
                    //{
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        Guid userId = new Guid(dr["userId"].ToString());
                    //        string loginName = dr["loginName"].ToString();
                    //        string userName = dr["UserName"].ToString();
                    //        string tel = dr["tel"].ToString();
                    //        string email = dr["email"].ToString();
                    //        bool islocked = bool.Parse(dr["IsLocked"].ToString());
                    //        bool isIPValidate = bool.Parse(dr["IsIPValidate"].ToString());
                    //        string isdownload = dr["IsDownLoad"].ToString();
                    //        DateTime createDate = DateTime.Parse(dr["CreateDate"].ToString());

                    //        User user = new User(loginName, userName, userId, this.GroupId, islocked, email, tel, createDate, isdownload, isIPValidate);
                    //        this.members.Add(user);
                    //    }
                    //}
                    QJVRMS.Business.RoleWS.RoleService rs = new QJVRMS.Business.RoleWS.RoleService();
                    using (DataTable dt = rs.GetUsersOfRole(this.roleId))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Guid userId = new Guid(dr["userId"].ToString());
                            string loginName = dr["loginName"].ToString();
                            string userName = dr["UserName"].ToString();
                            string tel = dr["tel"].ToString();
                            string email = dr["email"].ToString();
                            bool islocked = bool.Parse(dr["IsLocked"].ToString());
                            bool isIPValidate = bool.Parse(dr["IsIPValidate"].ToString());
                            string isdownload = dr["IsDownLoad"].ToString();
                            DateTime createDate = DateTime.Parse(dr["CreateDate"].ToString());

                            User user = new User(loginName, userName, userId, this.GroupId, islocked, email, tel, createDate, isdownload, isIPValidate);
                            this.members.Add(user);
                        }
                    }
                }

                return this.members;
            }

        }

        public static string GetRoleIdByName(string roleName)
        {
            QJVRMS.Business.RoleWS.RoleService rs = new QJVRMS.Business.RoleWS.RoleService();
            return rs.GetRoleIdByName(roleName);
        }

        #region IRole 成员

        public List<IOperator> CorrelativeOperator
        {
            get { return null; }
        }

        public Guid OperatorId
        {
            get { return this.roleId; }
        }


        public IGroup Owner
        {
            get
            {
                if (this.owner == null)
                {
                    this.owner = new Group(this.groupId);
                }

                return this.owner;
            }

        }

        public Guid RoleId
        {
            get
            {
                return this.roleId;
            }
            set
            {
                this.roleId = value;
            }

        }

        public Guid GroupId
        {
            get
            {
                return this.groupId;
            }

        }


        public string RoleName
        {
            get
            {
                return this.roleName;
            }

        }

        #endregion
    }
}
