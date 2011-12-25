using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using QJVRMS.Common;
using QJVRMS.Business;
using System.Data;
using QJVRMS.DataAccess;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.IO;


/// <summary>
/// 资源处理类
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ResourceService : System.Web.Services.WebService
{

    public ResourceService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    #region 资源库统计

    [WebMethod]
    public DataTable GetResourceStatic()
    {   
        try
        {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure,"QJDAM_GetStat").Tables[0];
        }
        catch
        {
            return null;
        }
    }

    #endregion



    #region 资源下载次数统计

    [WebMethod]
    public DataTable GetDownloadStatic(string resourceType,DateTime startDate,DateTime endDate)
    {
        SqlParameter[] parameters = {
				new SqlParameter("@resourceType", SqlDbType.NVarChar,50),
				new SqlParameter("@startDate", SqlDbType.DateTime),
                new SqlParameter("@endDate",SqlDbType.DateTime)
                                    };
        parameters[0].Value = resourceType;
        parameters[1].Value = startDate;
        parameters[2].Value = endDate;
        
        try
        {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetDownloadStat",parameters).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    #endregion

    /// <summary>
    /// 添加一条资源记录
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="ItemSerialNumber"></param>
    /// <param name="FileName"></param>
    /// <param name="FilePath"></param>
    /// <param name="ServerFileName"></param>
    /// <param name="Caption"></param>
    /// <param name="StartDate"></param>
    /// <param name="EndDate"></param>
    /// <param name="UploadDate"></param>
    /// <param name="ShotDate"></param>
    /// <param name="Keyword"></param>
    /// <param name="Description"></param>
    /// <param name="UpdateTime"></param>
    /// <param name="UserID"></param>
    /// <param name="Status"></param>
    [WebMethod]
    public void Add
    (
        Guid ID ,
        string ItemSerialNumber ,
        string FileName ,
        string FilePath ,
        long FileSize,
        string ServerFileName,
        string Caption ,
        DateTime StartDate ,
        DateTime EndDate ,
        DateTime UploadDate ,
        DateTime ShotDate ,
        string Keywords ,
        string Description ,
        DateTime UpdateDate,
        Guid UserID ,
        int Status,
        string ResourceType,
        string Author,
        int hasCopyright
    )
	{
		StringBuilder strSql=new StringBuilder();
		strSql.Append("insert into Resources(");
        strSql.Append("ID,ItemSerialNumber,ClientFileName,UserID,ServerFileName,ServerFolderName,FileSize,Caption,Keywords,Description,ViewCount,ShotDate,StartDate,EndDate,UploadDate,UpdateDate,Status,ResourceType,validateStatus,Author,hasCopyright)");
		strSql.Append(" values (");
        strSql.Append("@ID,@ItemSerailNumber,@ClientFileName,@UserID,@ServerFileName,@ServerFolderName,@FileSize,@Caption,@Keywords,@Description,@ViewCount,@ShotDate,@StartDate,@EndDate,@UploadDate,@UpdateDate,@status,@ResourceType,@ValidateStatus,@Author,@hasCopyright)");
		SqlParameter[] parameters = {
				new SqlParameter("@ID", SqlDbType.UniqueIdentifier,16),
				new SqlParameter("@ItemSerailNumber", SqlDbType.NVarChar,15),
				new SqlParameter("@ClientFileName", SqlDbType.NVarChar,300),
				new SqlParameter("@UserID", SqlDbType.UniqueIdentifier,16),
				new SqlParameter("@ServerFileName", SqlDbType.NVarChar,300),
				new SqlParameter("@ServerFolderName", SqlDbType.NVarChar,300),
				new SqlParameter("@FileSize", SqlDbType.BigInt,8),
				new SqlParameter("@Caption", SqlDbType.NVarChar,255),
				new SqlParameter("@Keywords", SqlDbType.NVarChar,255),
				new SqlParameter("@Description", SqlDbType.NVarChar),
				new SqlParameter("@ViewCount", SqlDbType.Int,4),
				new SqlParameter("@ShotDate", SqlDbType.DateTime),
				new SqlParameter("@StartDate", SqlDbType.DateTime),
				new SqlParameter("@EndDate", SqlDbType.DateTime),
				new SqlParameter("@UploadDate", SqlDbType.DateTime),
				new SqlParameter("@UpdateDate", SqlDbType.DateTime),
                new SqlParameter("@status",SqlDbType.TinyInt),
                new SqlParameter("@ResourceType",SqlDbType.VarChar,10),
                new SqlParameter("@Author",SqlDbType.VarChar,50),
                new SqlParameter("@hasCopyright",SqlDbType.Int),
                new SqlParameter("@ValidateStatus",SqlDbType.TinyInt)
        };
		parameters[0].Value = ID;
		parameters[1].Value = ItemSerialNumber;
		parameters[2].Value = FileName;
		parameters[3].Value = UserID;
		parameters[4].Value = ServerFileName;
		parameters[5].Value = FilePath;
		parameters[6].Value = FileSize;
		parameters[7].Value = Caption;
		parameters[8].Value = Keywords;
		parameters[9].Value = Description;
		parameters[10].Value = 0;
		parameters[11].Value = ShotDate;
		parameters[12].Value = StartDate;
		parameters[13].Value = EndDate;
		parameters[14].Value = UploadDate;
        parameters[15].Value = UpdateDate;
        parameters[16].Value = Status;
        parameters[17].Value = ResourceType;
        parameters[18].Value = Author;
        parameters[19].Value = hasCopyright;
        parameters[20].Value = 2;

        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql.ToString(), parameters);
	}


    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// 
    [WebMethod]
    public void Update(string itemId,string caption,string keywords,
        string description,DateTime shotDate,DateTime startDate,
        DateTime endDate
        )
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update Resources set ");
        strSql.Append("Caption=@Caption,");
        strSql.Append("Keywords=@Keywords,");
        strSql.Append("Description=@Description,");
        strSql.Append("ShotDate=@ShotDate,");
        strSql.Append("StartDate=@StartDate,");
        strSql.Append("EndDate=@EndDate,");
        strSql.Append("UpdateDate=getDate()");
        strSql.Append(" where ID=@ID ");
        SqlParameter[] parameters = {
					new SqlParameter("@Caption", SqlDbType.NVarChar,255),
					new SqlParameter("@Keywords", SqlDbType.NVarChar,255),
					new SqlParameter("@Description", SqlDbType.NVarChar),
					new SqlParameter("@ShotDate", SqlDbType.DateTime),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@ID", SqlDbType.UniqueIdentifier)
        };
        parameters[0].Value = caption;
        parameters[1].Value = keywords;
        parameters[2].Value = description;
        parameters[3].Value = shotDate;
        parameters[4].Value = startDate;
        parameters[5].Value = endDate;
        parameters[6].Value = new Guid(itemId);
        
        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql.ToString(), parameters);
    }


    /// <summary>
    /// 提交，按用户，日期
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    [WebMethod]
    public void ValidateResourceByUserDate(Guid userId, DateTime startDate, DateTime endDate, int validateStatus)
    {
        string strSql = "update Resources set validateStatus=@validateStatus where UserId=@userID and UploadDate>@startDate and UploadDate<@endDate";
        SqlParameter[] Parameters = new SqlParameter[4];
        Parameters[0] = new SqlParameter("@validateStatus", SqlDbType.Int);
        Parameters[0].Value = validateStatus;

        Parameters[1] = new SqlParameter("@userID", SqlDbType.UniqueIdentifier);
        Parameters[1].Value = userId;

        Parameters[2] = new SqlParameter("@startDate", SqlDbType.DateTime);
        Parameters[2].Value = startDate;

        Parameters[3] = new SqlParameter("@endDate", SqlDbType.DateTime);
        Parameters[3].Value = endDate;

        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql, Parameters);
    }


    /// <summary>
    /// 提交，按用户，日期，从某个状态变为某个状态
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="validateStatusFrom"></param>
    /// <param name="validateStatusTo"></param>
    [WebMethod]
    public void ValidateResourceByUserDate1(Guid userId, DateTime startDate, DateTime endDate, int validateStatusFrom,int validateStatusTo)
    {
        string strSql = "update Resources set validateStatus=@validateStatus where validateStatus=@validateStatusFrom and UserId=@userID and UploadDate>@startDate and UploadDate<@endDate";
        SqlParameter[] Parameters = new SqlParameter[5];
        Parameters[0] = new SqlParameter("@validateStatus", SqlDbType.Int);
        Parameters[0].Value = validateStatusTo;

        Parameters[1] = new SqlParameter("@userID", SqlDbType.UniqueIdentifier);
        Parameters[1].Value = userId;

        Parameters[2] = new SqlParameter("@startDate", SqlDbType.DateTime);
        Parameters[2].Value = startDate;

        Parameters[3] = new SqlParameter("@endDate", SqlDbType.DateTime);
        Parameters[3].Value = endDate;

        Parameters[4] = new SqlParameter("@validateStatusFrom", SqlDbType.Int);
        Parameters[4].Value = validateStatusFrom;

        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql, Parameters);
    }





    [WebMethod]
    public DataSet GetResourceSNByUserDate(Guid userId, DateTime startDate, DateTime endDate)
    {
        string strSql = "Select ItemSerialNumber From  Resources where UserId=@userID and UploadDate>@startDate and UploadDate<@endDate";
        SqlParameter[] Parameters = new SqlParameter[3];       

        Parameters[0] = new SqlParameter("@userID", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = userId;

        Parameters[1] = new SqlParameter("@startDate", SqlDbType.DateTime);
        Parameters[1].Value = startDate;

        Parameters[2] = new SqlParameter("@endDate", SqlDbType.DateTime);
        Parameters[2].Value = endDate;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, strSql, Parameters);
    }



    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// 
    [WebMethod]
    public void ValidateResourceByIDs(string[] ids,int validateStatus,string reason)
    {
        string strSql;
        foreach (string itemId in ids)
        {
            strSql = "update Resources set validateStatus=@validateStatus where ID=@ID  ";

            SqlParameter[] Parameters = new SqlParameter[2];
            Parameters[0] = new SqlParameter("@validateStatus", SqlDbType.Int);
            Parameters[0].Value = validateStatus;

            Parameters[1] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
            Parameters[1].Value = new Guid(itemId);

            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql, Parameters);

            if (validateStatus == (int)ResourceEntity.ResourceStatus.NotPass)
            {
                strSql = "delete from resource_NotPassReason where resourceId=@ID; Insert Into resource_NotPassReason(Id,resourceId,reason,resourceType) Values(newId(),@ID,@reason,'resource')";
                SqlParameter[] Parameters1 = new SqlParameter[2];
                Parameters1[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
                Parameters1[0].Value =new Guid(itemId);

                Parameters1[1] = new SqlParameter("@reason", SqlDbType.NVarChar,500);
                Parameters1[1].Value = reason;

                SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql, Parameters1);

            }
        
        
        }
        
        
        
    }

    [WebMethod]
    public DataSet GetResourceSNByIDs(string[] ids)
    {
        StringBuilder sb = new StringBuilder();
        foreach (string id in ids)
        {
            sb.Append("'");
            sb.Append(id);
            sb.Append("',");        
        }
        string _sqlIds = sb.ToString().Trim().Trim(",".ToCharArray());
        
        string strSql;

        strSql = "Select ItemSerialNumber From  Resources  where ID in ({0})  ";
        strSql = string.Format(strSql, _sqlIds);

        


        DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text,strSql);
        //LogWriter.WriteLog("ccccccccccccc", new string[] { strSql ,ds.Tables[0].Rows.Count.ToString()});
        return ds;        
    }



    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// 
    [WebMethod]
    public string  GetNotPassReason(string itemId)
    {
        try
        {
            string strSql;
            strSql = "select top 1 reason from resource_notpassreason where resourceID=@ID and resourceType='resource' ";

            SqlParameter[] Parameters = new SqlParameter[1];

            Parameters[0] = new SqlParameter("@ID", SqlDbType.UniqueIdentifier);
            Parameters[0].Value = new Guid(itemId);

            object o = SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, CommandType.Text, strSql, Parameters);
            if (o == null)
            {
                return string.Empty;
            }
            return o.ToString();
        }
        catch (Exception ex)
        {
            //LogWriter.WriteExceptionLog(ex);
            LogWriter.WriteLog("UploaderEXP", new string[] { DateTime.Now.ToString(), itemId }, true);
            
        }
        return itemId;
    }


    [WebMethod]
    public bool AddResourceToCatalog(Guid[] catalogId, Guid itemId)
    {
        DataTable mapTable = new DataTable();
        mapTable.Columns.Add("Resourceid", typeof(Guid));
        mapTable.Columns.Add("Catalogid", typeof(Guid));

        for (int i = 0; i < catalogId.Length; i++)
        {
            DataRow newRow = mapTable.NewRow();

            newRow["Resourceid"] = itemId;
            newRow["Catalogid"] = catalogId[i];

            mapTable.Rows.Add(newRow);
        }


        string sql = "Delete From Resource_Catalogs Where ResourceId=@itemId";
        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@itemId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = itemId;


        SqlConnection con = null;
        SqlTransaction trans = null;

        try
        {
            con = new SqlConnection(CommonInfo.ConQJVRMS);
            con.Open();

            trans = con.BeginTransaction();

            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, Parameters);
            SqlHelperExtend.Update("Resource_Catalogs", mapTable, trans);

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


    /// <summary>
    ///按关键字或图片编号，上传时间，分类搜索图片
    /// </summary>
    /// <returns></returns>    
    [WebMethod]
    public DataSet SearchResourceOld(string keyword, string beginDate, string endDate, string Catalogid, string Userid, int PageSize, int PageNum, ref int rowCount,string resourceType,string groupId)
    {
        SqlParameter[] paramater = new SqlParameter[]
                {
                    new SqlParameter("@keyword",SqlDbType.NVarChar,50),
                    new SqlParameter("@BeginDate",SqlDbType.VarChar,50),
                    new SqlParameter("@EndDate",SqlDbType.VarChar,50),
                    new SqlParameter("@Catalogid",SqlDbType.NVarChar,100),
                    new SqlParameter("@userid",SqlDbType.NVarChar,50),
                    new SqlParameter("@PageSize",SqlDbType.Int),
                    new SqlParameter("@PageNum",SqlDbType.Int),
                    new SqlParameter("@resourceType",SqlDbType.VarChar,20),
                    new SqlParameter("@groupid",SqlDbType.VarChar,50)

                };
        paramater[0].Value = keyword;
        paramater[1].Value = beginDate;
        paramater[2].Value = endDate;
        paramater[3].Value = Catalogid;
        paramater[4].Value = Userid;
        paramater[5].Value = PageSize;
        paramater[6].Value = PageNum;
        paramater[7].Value = resourceType;
        paramater[8].Value = groupId;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchResource1", paramater))
        {
            //rowCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            //DataTable dt = ds.Tables[1];
            return ds;
        }
    }


    /// <summary>
    ///按关键字或图片编号，上传时间，分类，审核状态搜索图片
    /// </summary>
    /// <returns></returns>    
    [WebMethod]
    public DataSet SearchResource(string keyword, string beginDate, string endDate, string Catalogid, string Userid, int PageSize, int PageNum, ref int rowCount, string resourceType, string groupId,int validateStatus)
    {
        SqlParameter[] paramater = new SqlParameter[]
                {
                    new SqlParameter("@keyword",SqlDbType.NVarChar,50),
                    new SqlParameter("@BeginDate",SqlDbType.VarChar,50),
                    new SqlParameter("@EndDate",SqlDbType.VarChar,50),
                    new SqlParameter("@Catalogid",SqlDbType.NVarChar,100),
                    new SqlParameter("@userid",SqlDbType.NVarChar,50),
                    new SqlParameter("@PageSize",SqlDbType.Int),
                    new SqlParameter("@PageNum",SqlDbType.Int),
                    new SqlParameter("@resourceType",SqlDbType.VarChar,20),
                    new SqlParameter("@groupid",SqlDbType.VarChar,50),
                    new SqlParameter("@validateStatus",SqlDbType.Int,10)

                };
        paramater[0].Value = keyword;
        paramater[1].Value = beginDate;
        paramater[2].Value = endDate;
        paramater[3].Value = Catalogid;
        paramater[4].Value = Userid;
        paramater[5].Value = PageSize;
        paramater[6].Value = PageNum;
        paramater[7].Value = resourceType;
        paramater[8].Value = groupId;
        paramater[9].Value = validateStatus;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchResource1", paramater))
        {
            //rowCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            //DataTable dt = ds.Tables[1];
            return ds;
        }
    }


    /// <summary>
    ///获取某人上传的资源
    /// </summary>
    /// <returns></returns>    
    [WebMethod]
    public DataSet GetResourceByUserID(string beginDate, string endDate, string Userid, int PageSize, int PageNum, ref int rowCount, string resourceType,int validateStatus)
    {
        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@BeginDate",SqlDbType.VarChar,50),
            new SqlParameter("@EndDate",SqlDbType.VarChar,50),
            new SqlParameter("@userid",SqlDbType.NVarChar,50),
            new SqlParameter("@PageSize",SqlDbType.Int),
            new SqlParameter("@PageNum",SqlDbType.Int),
            new SqlParameter("@resourceType",SqlDbType.VarChar,20),
            new SqlParameter("@validateStatus",SqlDbType.Int)
        };
        int i = 0;
        paramater[i++].Value = beginDate;
        paramater[i++].Value = endDate;
        paramater[i++].Value = Userid;
        paramater[i++].Value = PageSize;
        paramater[i++].Value = PageNum;
        paramater[i++].Value = resourceType;
        paramater[i++].Value = validateStatus;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetResourceByUserID", paramater))
        {
            return ds;
        }
    }



    /// <summary>
    /// 取一条资源信息
    /// </summary>
    /// <param name="itemid"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetResourceInfoByItemId(string itemid)
    {
        string sql = "SELECT * FROM dbo.Resources where id=@itemid";
        SqlParameter sp = new SqlParameter("@itemid", itemid.ToString());
        SqlParameter[] sps = new SqlParameter[] { sp };
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, sps);

    }

    [WebMethod]
    public DataSet GetResourceImagesDetail(string id) {
        SqlParameter param = new SqlParameter("@ID", SqlDbType.NVarChar, 50);
        param.Value = id;
        try {
            using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Resource_ImagesDetails", param)) {
                return ds;
            }
        }
        catch {
            return null;
        }
    }

    /// <summary>
    /// 取一条资源信息，通过serialNumber
    /// </summary>
    /// <param name="itemid"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetResourceInfoBySN(string sn)
    {
        string sql = "QJDAM_GetResourceBySN";
        SqlParameter sp = new SqlParameter("@sn", sn);
        SqlParameter[] sps = new SqlParameter[] { sp };
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, sql, sps);
    }






    /***资源下载**************/
    [WebMethod]
    public bool DeleteDownMessage(int logId)
    {

        string sql = "Delete From PRODUCTION_HIGHRES_DOWNLOG Where logId=@logId";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@logId", SqlDbType.Int);
        Parameters[0].Value = logId;

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
    public DataSet GetDownLoadMessage(string username, DateTime beginDate, DateTime endDate)
    {
        SqlParameter[] paramater = new SqlParameter[]
            {
                new SqlParameter("@username",SqlDbType.VarChar,50),
                new SqlParameter("@beginDate",SqlDbType.DateTime),
                new SqlParameter("@endDate",SqlDbType.DateTime)
            };

        paramater[0].Value = username;
        paramater[1].Value = beginDate;
        paramater[2].Value = endDate;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchDownloadManagerByLoginNameAndDate", paramater))
        {
            return ds;
        }
    }
    [WebMethod]
    public DataSet GetDownloadMessageByLoginName(string username)
    {
        SqlParameter[] paramater = new SqlParameter[]
                {
                    new SqlParameter("@username",SqlDbType.VarChar,50)                   
                };
        paramater[0].Value = username;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchDownloadManagerByLoginName", paramater))
        {
            return ds;
        }
    }



    [WebMethod]
    public void Production_Hires_Down_Log(string filaName, string fileType, string username, string usage, string enduser, string folder, bool Errflag,string resourceType)
    {
        try
        {
            SqlParameter[] Parameters = new SqlParameter[7];

            Parameters[0] = new SqlParameter("@filaName", filaName);
            Parameters[1] = new SqlParameter("@fileType", fileType);
            Parameters[2] = new SqlParameter("@username", username);
            Parameters[3] = new SqlParameter("@usage", usage);
            Parameters[4] = new SqlParameter("@enduser", enduser);
            Parameters[5] = new SqlParameter("@folder", folder);
            Parameters[6] = new SqlParameter("@resourceType", resourceType);

            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, "QJDAM_Production_Highres_Success_LOG_ADD", Parameters);


        }
        catch
        {

        }
    }


    //[WebMethod]
    //public void insertResourceAttributes(string sn,Dictionary<string,string> kvp)
    //{ 
    //    string sqlPattern="insert into ([ID],[ResourceSN],[attrName],[attrValue]) VALUES('{0}','{1}','{2}','{3}')";
    //    string sql=string.Empty;

    //    foreach(string k in kvp.Keys)
    //    {
    //        sql = string.Format(sqlPattern, new Guid().ToString(), sn, k, kvp[k]);
    //        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql);        
    //    }    
    //}



    [WebMethod]
    public void insertResourceAttributes(string sn, DictionaryEntry[] de)
    {
        string sqlPattern = "insert into ResourcesAttribute([ID],[ResourceSN],[attrName],[attrValue]) VALUES('{0}','{1}','{2}','{3}')";
        string sql = string.Empty;

        sql = "delete from ResourcesAttribute where ResourceSN='"+sn.Replace("'","''")+"'";
        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql);
        foreach (DictionaryEntry d in de)
        {
            sql = string.Format(sqlPattern, Guid.NewGuid().ToString(), sn, d.Key.ToString(), d.Value.ToString());
            //writeLog(@"c:\aaaaaaaaaaaaaaaaa.txt", sql);
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql);
        }
    }


    [WebMethod]
    public DataSet GetResourceCatalogByItemId(string itemId)
    {
        SqlParameter[] paramater = new SqlParameter[]
            {
                new SqlParameter("@itemid",SqlDbType.VarChar,50),
            };

        paramater[0].Value = itemId;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Catalog_GetResourceCatalog", paramater))
        {
            return ds;
        }
    }

    [WebMethod]
    public DataSet GetResourcesByOrderId(string orderId)
    {
        SqlParameter[] paramater = new SqlParameter[]
            {
                new SqlParameter("@orderid",SqlDbType.VarChar,50),
            };

        paramater[0].Value = orderId;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchResourceByOrderId", paramater))
        {
            return ds;
        }
    }

    [WebMethod]
    public DataSet GetResourcesByViewCount(DateTime startDate,DateTime endDate,int pageSize,int pageNum,string resourceType)
    {
  
        SqlParameter[] paramater = new SqlParameter[]
            {
                new SqlParameter("@startDate",SqlDbType.DateTime),
                new SqlParameter("@endDate",SqlDbType.DateTime),
                new SqlParameter("@pageSize",SqlDbType.Int),
                new SqlParameter("@pageNum",SqlDbType.Int),
                new SqlParameter("@resourceType",SqlDbType.NVarChar,50)
            };

        paramater[0].Value = startDate;
        paramater[1].Value = endDate;
        paramater[2].Value = pageSize;
        paramater[3].Value = pageNum;
        paramater[4].Value = resourceType;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchResourceByViewCount", paramater))
        {
            return ds;
        }
    }


    /// <summary>
    /// 根据下载次数统计
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNum"></param>
    /// <param name="resourceType"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetResourcesByDownloadCount(DateTime startDate, DateTime endDate, int pageSize, int pageNum, string resourceType)
    {

        SqlParameter[] paramater = new SqlParameter[]
            {
                new SqlParameter("@startDate",SqlDbType.DateTime),
                new SqlParameter("@endDate",SqlDbType.DateTime),
                new SqlParameter("@pageSize",SqlDbType.Int),
                new SqlParameter("@pageNum",SqlDbType.Int),
                new SqlParameter("@resourceType",SqlDbType.NVarChar,50)
            };

        paramater[0].Value = startDate;
        paramater[1].Value = endDate;
        paramater[2].Value = pageSize;
        paramater[3].Value = pageNum;
        paramater[4].Value = resourceType;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchResourceByDownloadCount", paramater))
        {
            return ds;
        }
    }



    [WebMethod]
    public DataSet GetResourceUploadLog(DateTime startDate, DateTime endDate, int pageSize, int pageNum, string userId)
    {
        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@startDate",SqlDbType.DateTime),
            new SqlParameter("@endDate",SqlDbType.DateTime),
            new SqlParameter("@pageSize",SqlDbType.Int),
            new SqlParameter("@pageNum",SqlDbType.Int),
            new SqlParameter("@UserId",SqlDbType.NVarChar,50)
        };
        paramater[0].Value = startDate;
        paramater[1].Value = endDate;
        paramater[2].Value = pageSize;
        paramater[3].Value = pageNum;
        paramater[4].Value = userId;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetResourceUploadLog", paramater))
        {
            return ds;
        }

    }



    [WebMethod]
    public bool AddAttach(string itemId, string fileName,long fileLength)
    {
        string sql = "if not exists (Select fileName From Attachments Where fileName=@fileName and itemId = @itemId)"
                     + " insert Into Attachments (attachId,itemId,fileName,createDate,fileLength) Values (@attachId,@itemId,@fileName,@cDate,@fileLength)";

        SqlParameter[] Parameters = new SqlParameter[5];

        Parameters[0] = new SqlParameter("@attachId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@itemId", SqlDbType.UniqueIdentifier);
        Parameters[2] = new SqlParameter("@fileName", SqlDbType.VarChar);
        Parameters[3] = new SqlParameter("@cDate", SqlDbType.NVarChar);
        Parameters[4] = new SqlParameter("@fileLength", SqlDbType.BigInt);

        Parameters[0].Value = Guid.NewGuid();
        Parameters[1].Value = new Guid(itemId);
        Parameters[2].Value = fileName;
        Parameters[3].Value = DateTime.Now.ToString();
        Parameters[4].Value = fileLength;


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
    public DataTable GetAttachList(Guid itemId)
    {
        string sql = "Select * from Attachments Where itemId=@itemId";
        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@itemId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = itemId;

        try
        {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];
        }
        catch
        {
            return null;
        }

    }


    [WebMethod]
    public void DeleteAttach(Guid attId)
    {
        string sql = "Delete from Attachments Where attachId=@attId";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@attId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = attId;

        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters);
        }
        catch
        {

        }
    }


    [WebMethod]
    public void UpdateResourceViewCount(string id)
    {
        string sql = "UPDATE dbo.Resources SET ViewCount=ViewCount+1 WHERE ID=@id";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = new Guid(id);

        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters);
        }
        catch
        {

        }
    }


    [WebMethod]
    public bool DeleteResource(Guid itemId)
    {
        string sql = "Begin tran"
                    + " Delete From Attachments Where ItemId=@itemId"
                    + " Delete From Resource_Catalogs Where Resourceid=@itemId"
                    + " DELETE FROM Resources  WHERE Id=@itemId Commit tran";


        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@itemId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = itemId;

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
    public DataSet GetMyUploadStatus(DateTime startDate, DateTime endDate,Guid userid)
    {

        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier)   ,
            new SqlParameter("@startDate",SqlDbType.DateTime),
            new SqlParameter("@endDate",SqlDbType.DateTime)
        };
        paramater[0].Value = userid;
        paramater[1].Value = startDate;
        paramater[2].Value = endDate;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_MyUploadStat", paramater);
    }


    [WebMethod]
    public DataSet GetNewResourceStatByUser( Guid userid)
    {

        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier)   
        };
        paramater[0].Value = userid;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetNewResourceStatByUser");
    }


    /// <summary>
    /// 获取没有被通过的资源列表
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetNotPassResources(string userName, DateTime startDate, DateTime endDate, int pageSize, int pageIndex)
    {

        SqlParameter[] paramater = new SqlParameter[]
        {
            new SqlParameter("@userName",SqlDbType.NVarChar,50)   ,
            new SqlParameter("@startDate",SqlDbType.DateTime),
            new SqlParameter("@endDate",SqlDbType.DateTime),
            new SqlParameter("@pageSize",SqlDbType.Int),
            new SqlParameter("@pageNum",SqlDbType.Int)
        };
        paramater[0].Value = userName;
        paramater[1].Value = startDate;
        paramater[2].Value = endDate;
        paramater[3].Value = pageSize;
        paramater[4].Value = pageIndex;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetNotPassResources", paramater);
    }


    /// <summary>
    /// 是否有新资源
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [WebMethod]
    public bool IsAlertAdmin(Guid userId,string isSuperAdmin)
    {
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier),
            new SqlParameter("@isSuperAdmin",SqlDbType.VarChar,10)
        };
        ps[0].Value = userId;
        ps[1].Value = isSuperAdmin;

        int i = (int)SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_IsAdminResource", ps);
        return i > 0;
    }

    private  void writeLog(string filePath, string message)
    {
        FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(DateTime.Now + "\t" + message);
        sw.Close();
        fs.Close();
    }


    [WebMethod]
    /// <summary>
    /// 获得每人的收藏夹
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public DataSet GetMyLightBox(Guid userId)
    {
        SqlParameter[] ps = new SqlParameter[] {         
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier)
        };

        ps[0].Value = userId;
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "LightBox_GetLightBoxByUserId", ps);
    }

    /// <summary>
    /// 获得某个收藏夹里包含的图片
    /// </summary>
    /// <param name="id">收藏夹id</param>
    /// <returns></returns>
    [WebMethod]   
    public DataSet GetResourcesByLightBoxID(Guid id,int pageSize,int pageIndex)
    {
        SqlParameter[] ps = new SqlParameter[] {         
            new SqlParameter("@lightboxId",SqlDbType.UniqueIdentifier),
            new SqlParameter("@pageSize",SqlDbType.Int),
            new SqlParameter("@pageIndex",SqlDbType.Int)
        };

        ps[0].Value = id;
        ps[1].Value = pageSize;
        ps[2].Value = pageIndex;
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "LightBox_GetResourcesByLightBoxID", ps);    
    }




    /// <summary>
    /// 添加一个收藏夹，与用户关联
    /// </summary>
    /// <param name="title"></param>
    /// <param name="userId"></param>
    /// <param name="zipFilePath"></param>
    /// <param name="zipFileExpireDate"></param>
    /// <returns></returns>
    [WebMethod]
    public bool AddLightBox(string title,Guid userId,string zipFilePath,DateTime zipFileExpireDate)
    {
        SqlParameter[] ps = new SqlParameter[] {         
            new SqlParameter("@title",SqlDbType.NVarChar,20),
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier),
            new SqlParameter("@ZipFilePath",SqlDbType.VarChar,200),
            new SqlParameter("@ZipFileExpireDate",SqlDbType.DateTime)
        };

        ps[0].Value = title;
        ps[1].Value = userId;
        ps[2].Value = zipFilePath;
        ps[3].Value = zipFileExpireDate;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "LightBox_AddLightBox", ps) > 0;
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// 更新一个收藏夹，主要就是改名字
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <param name="zipFilePath"></param>
    /// <param name="zipFileExpireDate"></param>
    /// <returns></returns>
    [WebMethod]
    public bool EditLightBox(Guid id,string title,string zipFilePath,DateTime zipFileExpireDate)
    {
        SqlParameter[] ps = new SqlParameter[] {         
            new SqlParameter("@id",SqlDbType.UniqueIdentifier),
            new SqlParameter("@Title",SqlDbType.NVarChar,20),
            new SqlParameter("@ZipFilePath",SqlDbType.VarChar,200),
            new SqlParameter("@ZipFileExpireDate",SqlDbType.DateTime)
        };

        ps[0].Value = id;
        ps[1].Value = title;
        ps[2].Value = zipFilePath;
        ps[3].Value = zipFileExpireDate;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure,"LightBox_EditLightBox", ps) > 0;
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// 删除一个收藏夹，连同里面收藏的图片
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [WebMethod]
    public bool DelLightBox(Guid id)
    {
        SqlParameter[] ps = new SqlParameter[] {         
            new SqlParameter("@id",SqlDbType.UniqueIdentifier)           
        };

        ps[0].Value = id;
        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "LightBox_DelLightBox", ps) > 0;
        }
        catch
        {
            return false;
        }
    
    }

    
    /// <summary>
    /// 向收藏夹中添加图片
    /// </summary>
    /// <param name="resourceId">图片id</param>
    /// <param name="lightboxId">收藏夹id</param>
    /// <returns></returns>
    [WebMethod]
    public bool AddToLightBox(Guid resourceId,Guid lightboxId)
    { 
        SqlParameter[] ps = new SqlParameter[] {         
            new SqlParameter("@resourceId",SqlDbType.UniqueIdentifier),
            new SqlParameter("@lightboxId",SqlDbType.UniqueIdentifier)
        };

        ps[0].Value = resourceId;
        ps[1].Value=lightboxId;
        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "LightBox_AddToLightBox", ps) > 0;
        }
        catch
        {
            return false;
        }    
    }

    /// <summary>
    /// 从收藏夹中移除图片
    /// </summary>
    /// <param name="resourceId">图片id</param>
    /// <param name="lightboxId">收藏夹id</param>
    /// <returns></returns>
    [WebMethod]
    public bool DelFromLightBox(Guid resourceId, Guid lightboxId)
    {
        SqlParameter[] ps = new SqlParameter[] {         
            new SqlParameter("@resourceId",SqlDbType.UniqueIdentifier),
            new SqlParameter("@lightboxId",SqlDbType.UniqueIdentifier)
        };

        ps[0].Value = resourceId;
        ps[1].Value = lightboxId;
        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "LightBox_DelFromLightBox", ps) > 0;
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// 清空收藏夹
    /// </summary>
    /// <param name="lightboxId"></param>
    /// <returns></returns>
    [WebMethod]
    public bool ClearLightBox(Guid lightboxId)
    {
        SqlParameter[] ps = new SqlParameter[]{        
            new SqlParameter("@lightboxId",SqlDbType.UniqueIdentifier)
        };

        ps[0].Value = lightboxId;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "LightBox_ClearLightBox", ps) > 0;
        }
        catch
        {
            return false;
        }
    
    }


    /// <summary>
    /// 为某人创建默认收藏夹
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [WebMethod]
    public bool CreateDefaultLightbox(Guid userId)
    {
        SqlParameter[] ps = new SqlParameter[]{        
            new SqlParameter("@userId",SqlDbType.UniqueIdentifier)
        };

        ps[0].Value = userId;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "LightBox_CreateDefaultLightbox", ps) > 0;
        }
        catch
        {
            return false;
        }
    }



    /// <summary>
    /// 删除文件时，将图片缩略图存入数据库
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public bool SaveDeletedImage(Guid logId,byte[] imageData)
    {
        SqlParameter[] ps = new SqlParameter[]{        
            new SqlParameter("@logId",SqlDbType.UniqueIdentifier),
            new SqlParameter("@imageData",SqlDbType.Image)
        };

        ps[0].Value = logId;
        ps[1].Value = imageData;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "SaveDeletedImage", ps) > 0;
        }
        catch
        {
            return false;
        }
    }


    /// <summary>
    /// 从数据库中获得删除的缩略图
    /// </summary>
    /// <param name="logId"></param>
    /// <returns></returns>
    [WebMethod]
    public byte[] GetDeletedImage(Guid logId)
    {
        SqlParameter[] ps = new SqlParameter[] { 
            new SqlParameter("@logId",SqlDbType.UniqueIdentifier)
        };
        ps[0].Value = logId;

        DataSet ds= SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "GetDeletedImage", ps);
        DataTable dt = ds.Tables[0];

        byte[] buffer=new byte[]{};
        if (dt.Rows.Count > 0)
        {
            buffer = (byte[])dt.Rows[0]["resourceData"];
        }

        return buffer;
    }




}

