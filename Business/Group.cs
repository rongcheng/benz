using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using QJVRMS.DataAccess;


namespace QJVRMS.Business
{
    /// <summary>
    /// Author: Sunan
    /// Date: 2008.05.07
    /// 暂时不实现层级组
    /// </summary>
    [Serializable]
    public class Group : IGroup
    {

        string description;
        string groupName;
        DateTime createDate;
        Guid groupId;

        UserCollection members;

        public Group(Guid groupId)
        {
            this.groupId = groupId;

            //string sql = "select * from [Group] where groupId=@GroupId";
            //SqlParameter[] Parameters = new SqlParameter[1];


            //Parameters[0] = new SqlParameter("@GroupId", SqlDbType.UniqueIdentifier);
            //Parameters[0].Value = groupId;

            //using (IDataReader reader = SqlHelper.ExecuteReader(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters))
            //{
            //    if (!reader.Read())
            //    {
            //        throw new Exception("没有组存在!");
            //    }

            //    GroupName = reader["Groupname"].ToString();
            //    this.createDate = DateTime.Parse(reader["CreateDate"].ToString());
            //}

            QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            using (DataTable dt = gs.GetGroup(groupId))
            {
                GroupName = dt.Rows[0]["GroupName"].ToString();
                this.createDate = DateTime.Parse(dt.Rows[0]["CreateDate"].ToString());
            }
        }

        public Group(Guid groupId, string groupName, string description, DateTime createDate)
        {
            this.groupId = groupId;
            this.GroupName = groupName;
            this.description = description;
            this.createDate = createDate;
        }


        public static DataSet GetGroupUsersStat()
        {
            QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            return gs.GetGroupUsersStat();
        }


        public static DataTable GetTopGroup()
        {
            return null;
        }

        public static IGroup GetRootGroup()
        {
            QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            using (DataTable dt = gs.GetRootGroup())
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Guid groupId = new Guid(row["groupId"].ToString());
                    string groupName = row["groupName"].ToString();
                    string des = row["Description"].ToString();
                    DateTime cDate = DateTime.Parse(row["CreateDate"].ToString());
                    Group g = new Group(groupId, groupName, des, cDate);

                    return g;
                }
            }

            return null;
        }

        public static IGroup CreateGroup(string groupName, string description)
        {
            //SqlParameter[] Parameters = new SqlParameter[4];

            //Parameters[0] = new SqlParameter("@GroupName", SqlDbType.NVarChar);
            //Parameters[1] = new SqlParameter("@CreateDate", SqlDbType.DateTime);
            //Parameters[2] = new SqlParameter("@Description", SqlDbType.NVarChar);
            //Parameters[3] = new SqlParameter("@GroupId", SqlDbType.UniqueIdentifier);

            //Parameters[3].Direction = ParameterDirection.Output;
            //DateTime now = DateTime.Now;

            //Parameters[0].Value = groupName;
            //Parameters[1].Value = now;
            //Parameters[2].Value = description;

            //try
            //{
            //    int i = SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "dbo.Group_CreateGroup", Parameters);

            //    return new Group(new Guid(Parameters[3].Value.ToString()), groupName, description, now);

            //}
            //catch(Exception ex)
            //{
            //   // QJVRMS.Common.LogWriter.WriteExceptionLog(ex, true);
            //    return null;
            //}
            QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            Guid groupId = gs.CreateGroup(groupName, description);

            return new Group(groupId, groupName, description, DateTime.Now);
        }

        public static Guid CreateChildGroup(Guid parentId, string groupName, int orderFlag)
        {
            QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            return gs.CreateChildGroup(parentId, groupName, orderFlag);
        }

        public static bool DeleteGroup(Guid groupId)
        {
            QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            return gs.DeleteGroup(groupId);
        }

        public static bool ModifyGroup(Guid groupId, string groupName, int orderFlag)
        {
            QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            return gs.ModifyGroup(groupId, groupName, orderFlag);
        }

        public static DataTable GetGroupList()
        {
            //string sql = "select  * from [Group]";

            //return SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql).Tables[0];

            QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            return gs.GetGroupList();
        }

        public static DataTable GetAllGroups(string spaceChar)
        {
            GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            return gs.GetAllGroups(spaceChar);
        }


        /// <summary>
        /// 通过机构名称名称查到机构的ID
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static string GetGroupIdByGroupName(string groupName)
        { 
            GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            return gs.GetGroupIdByGroupName(groupName);
        }


        //public static DataTable dGetCatalogList()
        //{
        //    string sql = "select  * from dbo.[Catalogs]";

        //    return SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql).Tables[0];
        //}

        public DateTime CreateDate
        {
            get { return createDate; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }



        #region IGroup 成员



        public Guid GroupId
        {
            get
            {
                return this.groupId;
            }

        }

        public string GroupName
        {
            get
            {
                return this.groupName;
            }
            set
            {
                this.groupName = value;
            }
        }

        public UserCollection Members
        {
            get
            {
                if (this.members == null)
                {
                    this.members = new UserCollection();
                    //SqlParameter[] Parameters = new SqlParameter[1];

                    //Parameters[0] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);
                    //Parameters[0].Value = this.GroupId;

                    //using (DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "dbo.Group_GetUsersByGroupId", Parameters).Tables[0])
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

                    QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
                    using (DataTable dt = gs.GetUsersByGroupId(this.GroupId))
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
            set
            {
                this.members = value;
            }
        }


        public DataTable SelectUsers(string sqlWhere)
        {
            //string sql = "select * from users where GroupId='{0}'";

            //sql = string.Format(sql, this.groupId.ToString());

            //if (!string.IsNullOrEmpty(sqlWhere))
            //{
            //    sql += " and " + sqlWhere;


            //}

            //return SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql).Tables[0];

            QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            return gs.SearchUsers(sqlWhere);
        }


        public DataTable SelectUsers(string loginName, string userName)
        {
            QJVRMS.Business.GroupWS.GroupService gs = new QJVRMS.Business.GroupWS.GroupService();
            return gs.SearchUsers(groupId, loginName, userName);
        }
        #endregion
    }



}
