using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.DataAccess;
using System.Data.SqlClient;
using System.Data;

namespace QJVRMS.Business
{
    public class Catalog : ICatalog
    {

        string catalogName, description;

        Guid catalogId, parentCatalogId, ownerGroupId;
        Catalog parentCatalog;
        Group ownerGroup;
        DateTime createDate;

        CatalogCollection childCatalog;


        public Catalog(Guid catalogId)
        {
           
            this.catalogId = catalogId;

            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            using (DataTable dt = cs.GetCatalog(catalogId))
            {
                if (dt.Rows.Count == 0) throw new Exception("没有类存在!");
                DataRow cataRow = dt.Rows[0];

                this.catalogName = cataRow["catalogName"].ToString();
                this.createDate = DateTime.Parse(cataRow["CreateDate"].ToString());
                this.description = cataRow["description"].ToString();


                if (cataRow["parentID"] != null && cataRow["parentID"].ToString() != "")
                {
                    string s = cataRow["parentID"].ToString();
                    this.parentCatalogId = new Guid(s);
                }
            }
        }

        public Catalog(string catalogName, Guid catalogId, Guid parentCatalogId, string descrption, DateTime createDate)
        {
            this.catalogName = catalogName;
            this.catalogId = catalogId;
            this.parentCatalogId = parentCatalogId;

            this.createDate = createDate;
        }

        public static Catalog CreateCatalog(string catalogName, Guid parentCatalogId, string descrption)
        {
           
            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            Guid newCataId = cs.CreateCatalog(catalogName, parentCatalogId, descrption);

            return new Catalog(catalogName,
                   newCataId,
                   parentCatalogId,
                   descrption,
                   DateTime.Now);
        }

        /// <summary>
        /// 删除分类
        /// 
        /// I:如果分类存在项目则不能删除
        /// II:删除分类的同时，将删除用户组所对应的功能
        /// </summary>
        /// <param name="catalogId"></param>
        /// <returns></returns>
        public static bool DeleteCatalog(Guid catalogId)
        {
           
            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            return cs.DeleteCatalog(catalogId);
        }


        public static bool ModifyCatalog(Guid catalogId, string catalogName, string catalogOrder, string descri)
        {
            
            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            return cs.ModifyCatalog(catalogId, catalogName, catalogOrder, descri);

        }
        /// <summary>
        /// 根据catalogid获取该类图片所属类
        /// </summary>
        /// <param name="catalogid">图片类Id</param>
        /// <returns>DataTable</returns>
        public static DataTable GetCatalogs(Guid catalogid)
        {
           
            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            return cs.GetCatalog(catalogid);
        }


        public static DataTable GetCatalogTableByParentId(Guid parentId)
        {
           

            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            return cs.GetCatalogTableByParentId(parentId);
        }


        /// <summary>
        /// 获取顶级分类
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTopCatalog()
        {
         
            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            return cs.GetTopCatalog();
        }

        /// <summary>
        /// 只取出1层
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        //public static DataTable GetCatalogTableByGroupId(Guid groupId)
        //{
        //    string sql = "select * from Catalogs where parentid is null and groupid=@catalogId order by CatalogOrder ASC";
        //    SqlParameter[] Parameters = new SqlParameter[1];


        //    Parameters[0] = new SqlParameter("@catalogId", SqlDbType.UniqueIdentifier);
        //    Parameters[0].Value = groupId;

        //    DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sql, Parameters).Tables[0];

        //    return dt;
        //}

        public static DataTable GetAllCatalog()
        {
           
            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            return cs.GetAllCatalog();
        }

        public static DataTable GetCategoryPicCount()
        {
            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            return cs.GetCategoryPicCount();
        }
        

      
        public static bool GetCataRight(Guid userID, Guid cataID)
        {
          
            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            return cs.CheckCatalogRight(userID, cataID);

        }

        public static DataTable GetCatalogByMethod(Guid userId, QJVRMS.Business.SecurityControl.OperatorMethod method)
        {
            QJVRMS.Business.CatalogWS.CatalogService cs = new QJVRMS.Business.CatalogWS.CatalogService();
            return cs.GetCatalogByMethod(userId, (int)method);
        }

        /// <summary>
        /// 获取对应节点下所有层的分类
        /// </summary>
        /// <param name="catalogId"></param>
        /// <returns></returns>
        public static DataTable GetAllSubCatalog(string catalogId)
        {
            String parentIds = catalogId;

            DataTable dtAllCatalogs = CacheManager.GetItem(CacheManager.CacheType.AllCatalog) as DataTable;

            DataTable result = dtAllCatalogs.Clone();
            result.Clear();

            if (dtAllCatalogs != null)
            {
                for (int i = 0; i < dtAllCatalogs.Rows.Count; i++)
                {
                    DataRow row = dtAllCatalogs.Rows[i];
                    string pId = row["ParentId"].ToString();
                    if (!string.IsNullOrEmpty(pId) 
                        && parentIds.Contains(pId))
                    {
                        DataRow newRow = result.NewRow();

                        newRow.ItemArray = row.ItemArray;

                        parentIds += "," + pId;

                        result.Rows.Add(newRow);
                    }
                }
            }
            return result;
        }

        #region ICatalog 成员

        public Guid CatalogId
        {
            get
            {
                return this.catalogId;
            }

        }

        public string CatalogName
        {
            get
            {
                return this.catalogName;
            }
            set
            {
                this.catalogName = value;
            }
        }

        public Catalog ParentCatalog
        {
            get
            {
                if (this.parentCatalog == null)
                {
                    this.parentCatalog = new Catalog(this.CatalogId);
                }

                return this.parentCatalog;
            }

        }

        public Guid ParentCatalogId
        {
            get
            {
                return this.parentCatalogId;
            }
            set
            {
                ParentCatalogId = value;
            }
        }

        public CatalogCollection ChildrenCatalogs
        {
            get
            {
                return childCatalog;
            }

        }

        public Group OwnerGroup
        {
            get
            {
                if (this.ownerGroup == null)
                {
                    this.ownerGroup = new Group(this.OwnerGroupId);
                }
                return this.ownerGroup;
            }

        }

        public Guid OwnerGroupId
        {
            get
            {
                return this.ownerGroupId;
            }
            set
            {
                this.ownerGroupId = value;
            }
        }

        #endregion

        #region ISecurityObject 成员

        public Guid ObjectId
        {
            get
            {
                return this.catalogId;
            }
            set
            {
                this.catalogId = value;
            }

        }

        public QJVRMS.Business.SecurityControl.SecurityObjectType ObjectType
        {
            get
            {
                return QJVRMS.Business.SecurityControl.SecurityObjectType.Item;
            }
            set
            {

            }

        }

        #endregion
    }
}
