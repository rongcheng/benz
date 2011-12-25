using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.DataAccess;

/// <summary>
/// SearchService 的摘要说明
/// 
/// 搜索服务 
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SearchService : System.Web.Services.WebService
{

    public SearchService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    /// <summary>
    ///按关键字或视频编号，上传时间，分类搜索视频
    /// by ciqq 2010-4-9
    /// </summary>
    /// <returns></returns>    
    [WebMethod]
    public DataTable SearchVideo(string keyword, string beginDate, string endDate, string Catalogid, string Userid, int PageSize, int PageNum, ref int rowCount)
    {
        SqlParameter[] paramater = new SqlParameter[]
                {
                    new SqlParameter("@keyword",SqlDbType.NVarChar,50),
                    new SqlParameter("@BeginDate",SqlDbType.VarChar,50),
                    new SqlParameter("@EndDate",SqlDbType.VarChar,50),
                    new SqlParameter("@Catalogid",SqlDbType.NVarChar,100),
                    new SqlParameter("@userid",SqlDbType.NVarChar,50),
                    new SqlParameter("@PageSize",SqlDbType.Int),
                    new SqlParameter("@PageNum",SqlDbType.Int)
                };
        paramater[0].Value = keyword;
        paramater[1].Value = beginDate;
        paramater[2].Value = endDate;
        paramater[3].Value = Catalogid;
        paramater[4].Value = Userid;
        paramater[5].Value = PageSize;
        paramater[6].Value = PageNum;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchVideo", paramater))
        {
            rowCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            DataTable dt = ds.Tables[1];
            return dt;
        }
    }






    /// <summary>
    ///按关键字或图片编号，上传时间，分类搜索图片
    /// dailing
    /// </summary>
    /// <returns></returns>    
    [WebMethod]
    public DataTable SearchImage(string keyword, string beginDate, string endDate, string Catalogid, string Userid, int PageSize, int PageNum, ref int rowCount)
    {
        SqlParameter[] paramater = new SqlParameter[]
                {
                    new SqlParameter("@keyword",SqlDbType.NVarChar,50),
                    new SqlParameter("@BeginDate",SqlDbType.VarChar,50),
                    new SqlParameter("@EndDate",SqlDbType.VarChar,50),
                    new SqlParameter("@Catalogid",SqlDbType.NVarChar,100),
                    new SqlParameter("@userid",SqlDbType.NVarChar,50),
                    new SqlParameter("@PageSize",SqlDbType.Int),
                    new SqlParameter("@PageNum",SqlDbType.Int)
                };
        paramater[0].Value = keyword;
        paramater[1].Value = beginDate;
        paramater[2].Value = endDate;
        paramater[3].Value = Catalogid;
        paramater[4].Value = Userid;
        paramater[5].Value = PageSize;
        paramater[6].Value = PageNum;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchImageNew", paramater))
        {
            rowCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            DataTable dt = ds.Tables[1];
            return dt;
        }
    }

    [WebMethod]
    public DataSet SearchByDept(Guid catalogId, Guid userId, Guid deptID, int pageSize, int pageIndex)
    {
        //string sqlWhere = "Where i.ItemId = ic.ImageStorageid and ic.catalogId='"+catalogId+"'";
        //sqlWhere += " and ic.catalogId not in (select objectId from AccessControlList Where OperatorId='" + userId.ToString() + "' and OperatorMethod=6)";
        //sqlWhere += " and ic.catalogId not in (Select objectId from AccessControlList Where OperatorMethod=6 and OperatorId in (Select RoleId From Users_InRoles Where UserId='"+userId.ToString()+"'))";
        string tables = string.Empty;


        string sqlWhere = " Where r.Id = rc.resourceid and rc.catalogId='" + catalogId + "'";
        sqlWhere += " and rc.catalogId not in (select objectId from AccessControlList Where OperatorId='" + userId.ToString() + "' and OperatorMethod=6)";
        sqlWhere += " and rc.catalogId not in (Select objectId from AccessControlList Where OperatorMethod=6 and OperatorId in (Select RoleId From Users_InRoles Where UserId='" + userId.ToString() + "'))";
        


        if (deptID != Guid.Empty)
        {
            //sqlWhere += " and i.GroupId='" + deptID + "'";
            //tables = "ImageStorage i,ImageStorage_Catalogs ic";
            sqlWhere += " and r.userId in (select userid from users where groupid='" + deptID + "')";
        }
        else
        {
            //tables = "ImageStorage i,ImageStorage_Catalogs ic";
        }
        tables = " Resources r,Resource_catalogs rc";

     
        //增加了 filename 信息的输出 by ciqq 2010-4-2

        //string columns = "r.Id,r.ItemSerialNumber,r.ServerFolderName,r.serverfilename,r.Caption";
        string columns = "r.*";

        int pageCount = 0;

        return SqlHelperExtend.Paging(new SqlConnection(CommonInfo.ConQJVRMS), tables, columns, sqlWhere, pageSize, pageIndex, "r.Id", out pageCount);
    }


    [WebMethod]
    public DataSet Search(string sqlWhere, Guid userId, int pageIndex, int pageSize)
    {
        SqlParameter[] paramater = new SqlParameter[]
                {
                    new SqlParameter("@sqlWhereParm",SqlDbType.NVarChar),
                    new SqlParameter("@userid",SqlDbType.UniqueIdentifier),
                    new SqlParameter("@pageIndex",SqlDbType.Int),
                    new SqlParameter("@pageSize",SqlDbType.Int),
                 
               
                };

        paramater[0].Value = sqlWhere;
        paramater[1].Value = userId;
        paramater[2].Value = pageIndex;
        paramater[3].Value = pageSize;




        DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchIndex", paramater);

        return ds;
    }
    //public DataSet Search(string sqlWhere, Guid catalogId, Guid userId, int rowStart, int rowEnd)
    //{
    //    SqlParameter[] paramater = new SqlParameter[]
    //            {
    //                new SqlParameter("@sqlWhereParm",SqlDbType.NVarChar),
    //                new SqlParameter("@Catalogid",SqlDbType.UniqueIdentifier),
    //                new SqlParameter("@userid",SqlDbType.UniqueIdentifier),
    //                new SqlParameter("@rowStartParm",SqlDbType.Int),
    //                new SqlParameter("@rowEndParm",SqlDbType.Int),

    //            };
    //    paramater[0].Value = sqlWhere;
    //    paramater[1].Value = catalogId;
    //    paramater[2].Value = userId;
    //    paramater[3].Value = rowStart;
    //    paramater[4].Value = rowEnd;



    //    DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QDAM_Search", paramater);

    //    return ds;
    //}
    /// <summary>
    /// 根据关键字搜索图片 
    /// </summary>
    /// <returns></returns>
    //[WebMethod]  
    //public DataTable SearchImageByCataID(string CatalogID, string UserID, int PageSize, int PageNum, ref int rowCount)
    //{
    //    SqlParameter[] paramater = new SqlParameter[]
    //            {
    //                new SqlParameter("@CatalogID",SqlDbType.NVarChar,100),
    //                new SqlParameter("@UserID",SqlDbType.NVarChar,100),
    //                new SqlParameter("@PageSize",SqlDbType.Int),
    //                new SqlParameter("@PageNum",SqlDbType.Int)
    //            };
    //    paramater[0].Value = CatalogID;
    //    paramater[1].Value = UserID;
    //    paramater[2].Value = PageSize;
    //    paramater[3].Value = PageNum;

    //    using (DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.Con_QJVRMS, CommandType.StoredProcedure, "QJDAM_SearchImageByCataID", paramater))
    //    {
    //        rowCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
    //        DataTable dt = ds.Tables[1];
    //        return dt;
    //    }
    //}


    /// <summary>
    /// 根据关键字搜索图片 add by dtf 5-30 
    /// 获取 ItemSerialNum,Hvsp 属性值
    /// </summary>
    /// <returns></returns>
    //[WebMethod]  
    //public DataTable SearchImageByKeyword(string keyword, int PageSize, int PageNum, ref int rowCount)
    //{
    //    SqlParameter[] paramater = new SqlParameter[]
    //            {
    //                new SqlParameter("@keyword",SqlDbType.NVarChar,50),
    //                new SqlParameter("@PageSize",SqlDbType.Int),
    //                new SqlParameter("@PageNum",SqlDbType.Int)
    //            };
    //    paramater[0].Value = keyword;
    //    paramater[1].Value = PageSize;
    //    paramater[2].Value = PageNum;

    //    using (DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.Con_QJVRMS, CommandType.StoredProcedure, "QJDAM_SearchImageByKeyword", paramater))
    //    {
    //        rowCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
    //        DataTable dt = ds.Tables[1];
    //        return dt;
    //    }
    //}

}

