using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.DataAccess;



/// <summary>
/// CatalogService 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CatalogService : System.Web.Services.WebService
{

    public CatalogService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

     
    [WebMethod]
    public DataTable GetCatalog(Guid catalogId)
    {
        string sql = "select * from Catalogs where catalogId=@catalogId";
        SqlParameter[] Parameters = new SqlParameter[1];


        Parameters[0] = new SqlParameter("@catalogId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = catalogId;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];

    }

    [WebMethod]
    public Guid CreateCatalog(string catalogName, Guid parentCatalogId, string descrption)
    {
        SqlParameter[] Parameters = new SqlParameter[5];

        Parameters[0] = new SqlParameter("@catalogName", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@description", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@createDate", SqlDbType.DateTime);
        Parameters[3] = new SqlParameter("@parentCataId", SqlDbType.UniqueIdentifier);

        Parameters[4] = new SqlParameter("@NewCataId", SqlDbType.UniqueIdentifier);
        Parameters[4].Direction = ParameterDirection.Output;
        DateTime now = DateTime.Now;

        Parameters[0].Value = catalogName;
        Parameters[1].Value = descrption;
        Parameters[2].Value = now;

        if (parentCatalogId == Guid.Empty)
            Parameters[3].Value = null;
        else
            Parameters[3].Value = parentCatalogId;


        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Catalog_CreateCatalog", Parameters);
            return new Guid(Parameters[4].Value.ToString());


        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            throw ex;
        }
    }

    [WebMethod]
    public bool DeleteCatalog(Guid catalogId)
    {
        string sql = "Begin Tran Begin try"
                + " delete from Catalogs where CatalogId=@catalogId"
                + " delete from AccessControlLIst where ObjectId=@catalogId"
                + " Commit End try Begin Catch  IF @@TRANCOUNT > 0 Rollback"
                + " DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int"
                + " SELECT @ErrMsg = ERROR_MESSAGE(),"
                + " @ErrSeverity = ERROR_SEVERITY()"
                + " RAISERROR(@ErrMsg, @ErrSeverity, 1)"
                + " End Catch";
        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@catalogId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = catalogId;


        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;

        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }

    [WebMethod]
    public bool ModifyCatalog(Guid catalogId, string catalogName, string catalogOrder, string descri)
    {
        string sql = "update Catalogs set CatalogName=@catalogName,CatalogOrder=@catalogOrder where CatalogId=@catalogId";

        SqlParameter[] Parameters = new SqlParameter[3];

        Parameters[0] = new SqlParameter("@catalogName", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@catalogId", SqlDbType.UniqueIdentifier);
        Parameters[2] = new SqlParameter("@catalogOrder", SqlDbType.NVarChar);

        Parameters[0].Value = catalogName;
        Parameters[1].Value = catalogId;
        Parameters[2].Value = catalogOrder;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;

        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return false;
        }

    }

    [WebMethod]
    public DataTable GetCatalogs(string catalogid)
    {
        string sql = "select distinct * from ImageStorage_Catalogs where CatalogId='" + catalogid + "'";


        DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0];

        }
        else
        {
            return null;
        }

    }

    [WebMethod]
    public DataTable GetCatalogTableByParentId(Guid parentId)
    {
        DataTable dt = null;
        string sql = string.Empty;

        if (parentId != Guid.Empty)
        {
            sql = @"select * From Catalogs where ParentID=@parentId order by CatalogOrder ASC";
            SqlParameter[] Parameters = new SqlParameter[1];

            Parameters[0] = new SqlParameter("@parentId", SqlDbType.UniqueIdentifier);
            Parameters[0].Value = parentId;

            dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];
        }
        else
        {

        }

        return dt;
    }

    [WebMethod]
    public DataTable GetTopCatalog()
    {
        string sql = "select * from Catalogs where parentid is null order by CatalogOrder ASC";

        DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0];

        return dt;
    }

    [WebMethod]
    public DataTable GetAllCatalog()
    {
        string sql = "select * from Catalogs order by CatalogOrder ASC";

        DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0];

        return dt;
    }

    [WebMethod]
    public bool CheckCatalogRight(Guid userID, Guid cataID)
    {
        SqlParameter[] Parameters = new SqlParameter[2];

        Parameters[0] = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@CataID", SqlDbType.UniqueIdentifier);

        Parameters[0].Value = userID;
        Parameters[1].Value = cataID;

        try
        {
            string strCount = SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Catalog_GetUserRight", Parameters).ToString();
            if (strCount == "0")
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }


    [WebMethod]
    public DataTable GetCatalogByMethod(Guid userId, int method)
    {
        string sql = "select ObjectId from AccessControlList where"
                    + " (OperatorId in("
                    + " select RoleId from Users_InRoles where userid=@userId"
                    + " ) or  OperatorId=@userId)"
                    + " and OperatorMethod=@method and ObjectId in (Select CatalogId from Catalogs)";

        SqlParameter[] Parameters = new SqlParameter[2];

        Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);

        Parameters[1] = new SqlParameter("@method", SqlDbType.TinyInt);


        Parameters[0].Value = userId;
        Parameters[1].Value = method;

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


    [WebMethod]
    public DataTable GetCategoryPicCount()//获取分类图片数量
    {
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetCategoryPicCount").Tables[0];

    }
}

