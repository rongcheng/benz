using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.DataAccess;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.Business.SecurityControl;
using System.Xml.Serialization;

namespace QJVRMS.Business
{
    /// <summary>
    /// Author: Sunan
    /// Date: 2008.05.07
    /// </summary>
    [Serializable]
    public class User : IUser
    {

        string userLoginName;

        Guid userId;
        string userName, groupName;
        //string userDescription;
        RoleCollection roles;
        string email;
        string tel;

        Group group;
        Guid groupId;

        bool isLocked = false;
        bool isIPValidate = false;
        string isDownLoad = "0";
        DateTime createDate;
         
        public User(string loginName, string userName, Guid userId, Guid groupId, bool isLocked, string email, string tel, DateTime createDate, string isDownLoad, bool isIPValidate)
        {
            this.UserLoginName = loginName;
            this.UserName = userName;
            this.UserId = userId;
            this.groupId = groupId;
            this.isLocked = isLocked;
            this.Email = email;
            this.Telphone = tel;
            this.createDate = createDate;
            this.isDownLoad = isDownLoad;
            this.isIPValidate = isIPValidate;
        }

        public User(Guid userId)
        {
            this.userId = userId;
        }

        public User()
        {
        }

        [XmlIgnore]
        public RoleCollection Roles
        {
            get
            {
                if (this.roles == null)
                {
//                    string sql = @"select u.roleID,r.groupId, r.roleName from users_inroles u,roles r
//                                where u.userId=@userId and u.roleId=r.roleId";

//                    SqlParameter[] Parameters = new SqlParameter[1];
//                    Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
//                    Parameters[0].Value = this.userId;

//                    DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters).Tables[0];

//                    RoleCollection roles = new RoleCollection();
//                    foreach (DataRow row in dt.Rows)
//                    {
//                        Role role = new Role(new Guid(row["roleID"].ToString()), new Guid(row["groupId"].ToString()), row["roleName"].ToString(), string.Empty);
//                        roles.Add(role);
//                    }

//                    this.roles = roles;

                    QJVRMS.Business.UserWS.UserService us = new QJVRMS.Business.UserWS.UserService();
                    RoleCollection roles = new RoleCollection();
                    using (DataTable dt = us.GetRolesOfUser(this.userId))
                    {
                 
                        foreach (DataRow row in dt.Rows)
                        {
                            Role role = new Role(new Guid(row["roleID"].ToString()), new Guid(row["groupId"].ToString()), row["roleName"].ToString(), string.Empty);
                            roles.Add(role);
                        }
                    }

                    this.roles = roles;

                }

                return this.roles;
            }
        }

        //public User(string loginName)
        //{
        //    this.userLoginName = loginName;

        //    //只取出没有锁定的用户 IsLocked=0
        //    string sql = "select * from Users where loginName=@loginId and IsLocked=0";
        //    SqlParameter[] Parameters = new SqlParameter[1];
        //    Parameters[0] = new SqlParameter("@loginId", SqlDbType.VarChar);
        //    Parameters[0].Value = loginName;

        //    using (IDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters))
        //    {
        //        if (!reader.Read())
        //        {
        //            throw new Exception("用户登录ID不存在!");
        //        }

        //        this.groupId = new Guid(reader["groupId"].ToString());
        //        this.isLocked = bool.Parse(reader["IsLocked"].ToString());
        //        this.UserId = new Guid(reader["UserId"].ToString());
        //        this.UserName = reader["Username"].ToString();

        //        this.Email = reader["email"].ToString();
        //        this.Telphone = reader["Tel"].ToString();
        //        this.createDate = DateTime.Parse(reader["CreateDate"].ToString());
        //    }

        //}

        public string GroupName
        {
            get { return this.groupName; }
            set { this.groupName = value; }
        }

        public Guid GroupId
        {
            get { return this.groupId; }
            set { this.groupId = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string Telphone
        {
            get { return tel; }
            set { tel = value; }

        }

        public bool IsLocked
        {
            get { return isLocked; }
        }

        public bool IsIPValidate
        {
            get { return this.isIPValidate; }
            set { this.isIPValidate = value; }
        }

        public string IsDownLoad
        {
            set { this.isDownLoad = value; }
            get { return isDownLoad; }
        }


       
        #region IUser 成员

        [XmlIgnore]
        public List<IOperator> CorrelativeOperator
        {
            get { return null; }
        }

        public Guid OperatorId
        {
            get { return this.userId; }
        }

        [XmlIgnore]
        public Group OwnerGroup
        {
            get
            {
                if (this.group == null)
                {
                    this.group = new Group(this.groupId);
                }

                return this.group;
            }
            set
            {
                this.group = value;
            }
        }


        //public string Description
        //{
        //    get
        //    {
        //        return this.userDescription;
        //    }
        //    set
        //    {
        //        this.userDescription = value;
        //    }
        //}

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        public Guid UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }

        public string UserLoginName
        {
            get { return userLoginName; }
            set { this.userLoginName = value; }
        }


        #endregion
    }
}
