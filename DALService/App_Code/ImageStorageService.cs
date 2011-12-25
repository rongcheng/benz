using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;

using QJVRMS.DataAccess;
using QJVRMS.Common;


/// <summary>
/// ImageStorage 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ImageStorageService : System.Web.Services.WebService
{

    public ImageStorageService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    //public bool DeleteImageStorage(int ItemSerialNum)
    //{
    //    string sql = "DELETE FROM ImageStorage WHERE ItemSerialNum=@serial";
    //    SqlParameter[] Parameters = new SqlParameter[1];


    //    Parameters[0] = new SqlParameter("@serial", SqlDbType.Int);
    //    Parameters[0].Value = ItemSerialNum;

    //    try
    //    {
    //        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}

    //[WebMethod]
    //public bool UpdateImageStorage(int ItemSerialNum, string FileName, string Keyword, string Description)
    //{
    //    string sql = "update ImageStorage set [FileName] = @FileName,Keyword=@Keyword,Description=@Description,UpdateTime=getdate()"
    //                 + " where ItemSerialNum = @ItemSerialNum";

    //    SqlParameter[] Parameters = new SqlParameter[4];

    //    Parameters[0] = new SqlParameter("@ItemSerialNum", SqlDbType.NVarChar);
    //    Parameters[1] = new SqlParameter("@FileName", SqlDbType.NVarChar);
    //    Parameters[2] = new SqlParameter("@Keyword", SqlDbType.NVarChar);
    //    Parameters[3] = new SqlParameter("@Description", SqlDbType.NVarChar);

    //    Parameters[0].Value = ItemSerialNum;
    //    Parameters[1].Value = FileName;
    //    Parameters[2].Value = Keyword;
    //    Parameters[3].Value = Description;
    //    try
    //    {
    //        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
    //    }
    //    catch
    //    {
    //        return false;
    //    
    //}


    [WebMethod]
    public void PutImageFromClient(string log)
    {
        SerializeObjectFactory sof = new SerializeObjectFactory();
        Quanjing.Security.UploadLogInfo loginfo = sof.DesializeFromBase64(log) as Quanjing.Security.UploadLogInfo;

        //this.AddImageStorage(loginfo.UserGuid, 
        //    loginfo.OldFileName,
        //    loginfo.UserId, 
        //    loginfo.PicRemark, 
        //    string.Empty, 
        //    string.Empty, 
        //    DateTime.Now, 
        //    DateTime.Now, 
        //    DateTime.Now, 
        //    string.Empty, 
        //    loginfo.PicRemark,
        //    System.IO.Path.GetExtension(loginfo.UpLoadFileName), 
        //    string.Empty, loginfo.ItemId,
        //    loginfo.ImageSerNum);

        SqlParameter[] Parameters = new SqlParameter[15];

        Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@FileName", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@FolderName", SqlDbType.NVarChar);
        Parameters[3] = new SqlParameter("@Caption", SqlDbType.NVarChar);
        Parameters[4] = new SqlParameter("@Address", SqlDbType.NVarChar);
        Parameters[5] = new SqlParameter("@Character", SqlDbType.NVarChar);
        Parameters[6] = new SqlParameter("@StartDate", SqlDbType.DateTime);
        Parameters[7] = new SqlParameter("@EndDate", SqlDbType.DateTime);
        Parameters[8] = new SqlParameter("@shotDate", SqlDbType.DateTime);
        Parameters[9] = new SqlParameter("@Keyword", SqlDbType.NVarChar);
        Parameters[10] = new SqlParameter("Description", SqlDbType.NVarChar);
        Parameters[11] = new SqlParameter("@ImageType", SqlDbType.NVarChar);
        Parameters[12] = new SqlParameter("@Hvsp", SqlDbType.VarChar);
        Parameters[13] = new SqlParameter("@ItemId", SqlDbType.UniqueIdentifier);
        Parameters[14] = new SqlParameter("@serNum", SqlDbType.VarChar);

        Parameters[0].Value = loginfo.UserGuid;
        Parameters[1].Value = loginfo.OldFileName;
        Parameters[2].Value = loginfo.UserId;
        Parameters[3].Value = loginfo.PicRemark;
        Parameters[4].Value = string.Empty;
        Parameters[5].Value = string.Empty;
        Parameters[6].Value = DateTime.Now;
        Parameters[7].Value = DateTime.Now;
        Parameters[8].Value = DateTime.Now;
        Parameters[9].Value = string.Empty;
        Parameters[10].Value = loginfo.PicRemark;
        Parameters[11].Value = System.IO.Path.GetExtension(loginfo.UpLoadFileName);
        Parameters[12].Value = string.Empty;
        Parameters[13].Value = loginfo.ItemId;
        Parameters[14].Value = loginfo.ImageSerNum;

        System.Collections.Generic.List<Guid> catas = null;
        catas = loginfo.CataList;


        string sql = "Insert into ImageStorage_Catalogs (ImageStorageid,Catalogid) values (@itemId,@cataId)";

        SqlConnection sc = null;
        SqlTransaction trans = null;
        try
        {
            sc = new SqlConnection(CommonInfo.ConQJVRMS);
            sc.Open();

            trans = sc.BeginTransaction();
            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "QJDAM_AddImageStorage", Parameters);


            SqlParameter[] parames = new SqlParameter[2];

            parames[0] = new SqlParameter("@itemId", SqlDbType.UniqueIdentifier);
            parames[1] = new SqlParameter("@cataId", SqlDbType.UniqueIdentifier);

            foreach (Guid cataId in catas)
            {
                parames[0].Value = loginfo.ItemId;
                parames[1].Value = cataId;

                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parames);
            }

            trans.Commit();

        }
        catch (Exception ex)
        {
            trans.Rollback();
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);


        }
    }

    [WebMethod]
    public string AddImageStorage(Guid userId,
        string fileName,
        string folderName,
        string caption,
        string address,
        string character,
        DateTime startDate,
        DateTime endDate,
        DateTime shotDate,
        string keywords,
        string desc,
        string imageType,
        string hvsp,
        Guid itemId,
        string serNum,
        Guid groupId)
    {
        SqlParameter[] Parameters = new SqlParameter[16];

        Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@FileName", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@FolderName", SqlDbType.NVarChar);
        Parameters[3] = new SqlParameter("@Caption", SqlDbType.NVarChar);
        Parameters[4] = new SqlParameter("@Address", SqlDbType.NVarChar);
        Parameters[5] = new SqlParameter("@Character", SqlDbType.NVarChar);
        Parameters[6] = new SqlParameter("@StartDate", SqlDbType.DateTime);
        Parameters[7] = new SqlParameter("@EndDate", SqlDbType.DateTime);
        Parameters[8] = new SqlParameter("@shotDate", SqlDbType.DateTime);
        Parameters[9] = new SqlParameter("@Keyword", SqlDbType.NVarChar);
        Parameters[10] = new SqlParameter("Description", SqlDbType.NVarChar);
        Parameters[11] = new SqlParameter("@ImageType", SqlDbType.NVarChar);
        Parameters[12] = new SqlParameter("@Hvsp", SqlDbType.VarChar);
        Parameters[13] = new SqlParameter("@ItemId", SqlDbType.UniqueIdentifier);
        Parameters[14] = new SqlParameter("@serNum", SqlDbType.VarChar);
        Parameters[15] = new SqlParameter("@groupId", SqlDbType.UniqueIdentifier);


        Parameters[0].Value = userId;
        Parameters[1].Value = fileName;
        Parameters[2].Value = folderName;
        Parameters[3].Value = caption;
        Parameters[4].Value = address;
        Parameters[5].Value = character;
        Parameters[6].Value = startDate;
        Parameters[7].Value = endDate;
        Parameters[8].Value = shotDate;
        Parameters[9].Value = keywords;
        Parameters[10].Value = desc;
        Parameters[11].Value = imageType;
        Parameters[12].Value = hvsp;
        Parameters[13].Value = itemId;
        Parameters[14].Value = serNum;
        Parameters[15].Value = groupId;

        try
        {

            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_AddImageStorage", Parameters).ToString();

        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return null;

        }

    }

    [WebMethod]
    public bool UpdateImageStorage(Guid itemId, string caption, string address, string character, DateTime startDate, DateTime endDate, DateTime shotDate, string keywords, string desc)
    {

        SqlParameter[] Parameters = new SqlParameter[9];

        Parameters[0] = new SqlParameter("@Caption", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@Address", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@Character", SqlDbType.NVarChar);
        Parameters[3] = new SqlParameter("@StartDate", SqlDbType.DateTime);
        Parameters[4] = new SqlParameter("@EndDate", SqlDbType.DateTime);
        Parameters[5] = new SqlParameter("@shotDate", SqlDbType.DateTime);
        Parameters[6] = new SqlParameter("@Keyword", SqlDbType.NVarChar);
        Parameters[7] = new SqlParameter("@Description", SqlDbType.NVarChar);
        Parameters[8] = new SqlParameter("@ItemId", SqlDbType.UniqueIdentifier);


        Parameters[0].Value = caption;
        Parameters[1].Value = address;
        Parameters[2].Value = character;
        Parameters[3].Value = startDate;
        Parameters[4].Value = endDate;
        Parameters[5].Value = shotDate;
        Parameters[6].Value = keywords;
        Parameters[7].Value = desc;
        Parameters[8].Value = itemId;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_UpdateImageStorage", Parameters) > 0;
        }
        catch
        {
            return false;
        }
    }

    [WebMethod]
    public bool AddAttach(Guid itemId, string fileName)
    {
        string sql = "if not exists (Select fileName From Attachments Where fileName=@fileName)"
                     +" insert Into Attachments (attachId,itemId,fileName,createDate) Values (@attachId,@itemId,@fileName,@cDate)";

        SqlParameter[] Parameters = new SqlParameter[4];

        Parameters[0] = new SqlParameter("@attachId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@itemId", SqlDbType.UniqueIdentifier);
        Parameters[2] = new SqlParameter("@fileName", SqlDbType.VarChar);
        Parameters[3] = new SqlParameter("@cDate", SqlDbType.DateTime);

        Parameters[0].Value = Guid.NewGuid();
        Parameters[1].Value = itemId;
        Parameters[2].Value = fileName;
        Parameters[3].Value = DateTime.Now;


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
    public bool DeleteImageStorage(Guid itemId)
    {
        //删除图片时，也把附件删除掉
        //by ciqq 2010-4-1
        string sql = "Begin tran"
                    + " Delete From Attachments Where ItemId=@itemId" 
                    + " Delete From ImageStorage_Catalogs Where ImageStorageid=@itemId"
                    + " DELETE  FROM ImageStorage  WHERE ItemId=@itemId Commit tran";


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
    public bool AddImageToCatalog(Guid[] catalogId, Guid itemId)
    {
        DataTable mapTable = new DataTable();
        mapTable.Columns.Add("ImageStorageid", typeof(Guid));
        mapTable.Columns.Add("Catalogid", typeof(Guid));

        for (int i = 0; i < catalogId.Length; i++)
        {
            DataRow newRow = mapTable.NewRow();

            newRow["ImageStorageid"] = itemId;
            newRow["Catalogid"] = catalogId[i];

            mapTable.Rows.Add(newRow);
        }


        string sql = "Delete From ImageStorage_Catalogs Where imageStorageId=@itemId";
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
            SqlHelperExtend.Update("ImageStorage_Catalogs", mapTable, trans);

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
    public DataTable GetImageByAuthAndNum(string imageNum, Guid userId)
    {
        string sql = "Select distinct i.* From ImageStorage i inner join ImageStorage_Catalogs ic on i.ItemId = ic.ImageStorageid"
                   + " Where i.ItemSerialNum=@imageNum and ic.catalogId not in (select objectId from AccessControlList Where OperatorId=@operId and OperatorMethod=6)"
                   + " And ic.catalogId not in (Select objectId from AccessControlList Where OperatorMethod=6 and OperatorId in (Select RoleId From Users_InRoles Where UserId=@operId))";


        SqlParameter[] Parameters = new SqlParameter[2];

        Parameters[0] = new SqlParameter("@imageNum", SqlDbType.VarChar);
        Parameters[1] = new SqlParameter("@operId", SqlDbType.UniqueIdentifier);

        Parameters[0].Value = imageNum;
        Parameters[1].Value = userId;

        try
        {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0];
        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return null;
        }


    }

    [WebMethod]
    public DataTable GetImageInfoByAuthAndId(Guid itemId, Guid userId)
    {
        string sql = "Select distinct i.* From ImageStorage i inner join ImageStorage_Catalogs ic on i.ItemId = ic.ImageStorageid"
                  + " Where i.itemId=@itemId and ic.catalogId not in (select objectId from AccessControlList Where OperatorId=@operId and OperatorMethod=6)"
                  + " And ic.catalogId not in (Select objectId from AccessControlList Where OperatorMethod=6 and OperatorId in (Select RoleId From Users_InRoles Where UserId=@operId))";


        sql = "SELECT * FROM dbo.Resources where id=@itemId";

        SqlParameter[] Parameters = new SqlParameter[2];

        Parameters[0] = new SqlParameter("@itemId", SqlDbType.UniqueIdentifier);
        //Parameters[1] = new SqlParameter("@operId", SqlDbType.UniqueIdentifier);

        Parameters[0].Value = itemId;
        //Parameters[1].Value = userId;


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
    public DataTable GetImageInfoById(Guid itemId)
    {
        SqlParameter[] paramater = new SqlParameter[]
                {
                    new SqlParameter("@itemId",itemId)                   
                };
        string sql = "select * from ImageStorage where itemId=@itemId";

        using (DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, paramater).Tables[0])
        {
            return dt;
        }
    }

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
    public DataTable GetUserByAll()
    {
        string sql = "select * from [QJDAM].[dbo].[Users]";

        using (DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0])
        {
            return dt;
        }
    }
    [WebMethod]
    public DataSet GetImageCatalog(string imageID)
    {
        SqlParameter[] paramater = new SqlParameter[]
            {
                new SqlParameter("@ImageStorageid",SqlDbType.VarChar,50),
            };

        paramater[0].Value = imageID;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Catalog_GetImageCatalog", paramater))
        {
            return ds;
        }
    }


    [WebMethod]
    public DataTable GetLightBoxList(Guid userId)
    {
        string sql = "select * from LightBox Where UserId=@userId";

        sql = @"select l.id as lightboxId,l.userId,r.* ,r.id as resourceItemId from LightBox l
right JOIN dbo.Resources r
ON l.itemId=r.ID 
Where l.UserId=@userId";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@UserID", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = userId;


        using (DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters).Tables[0])
        {
            return dt;
        }
    }

    [WebMethod]
    public bool DelItemFromLightBox(Guid userId, Guid itemId)
    {
        string sql = "Delete From LightBox Where itemId=@itemId and userId=@userId";

        SqlParameter[] Parameters = new SqlParameter[2];

        Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@itemId", SqlDbType.UniqueIdentifier);


        Parameters[0].Value = userId;
        Parameters[1].Value = itemId;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
        }
        catch
        {
            return false;
        }
    }


    #region 图片库统计

    [WebMethod]
    public DataTable GetImageStatic()
    {
        string sql = "declare @isum int,@tsum int,@VideoAllCount int,@VideoTodayCount int;"
                    + " Select @isum=Count(*) From ImageStorage;"
                    + " Select @tsum=Count(*) From ImageStorage Where datediff(d,getdate(),UploadDate)=0;"
                    + " Select @VideoAllCount=Count(*) From VideoStorage;"
                    + " Select @VideoTodayCount=Count(*) From VideoStorage Where UploadDate Between Convert(char(10),GetDate(),120) And Convert(char(10),DateAdd(day,1,GetDate()),120);"
                    + " Select @isum as isum,@tsum as tsum,@VideoAllCount as VideoAllCount,@VideoTodayCount as VideoTodayCount";

        try
        {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    #endregion

    [WebMethod]
    public void Production_Hires_Down_Log(string filaName, string fileType, string username, string usage, string enduser,string folder, bool Errflag)
    {
        try
        {
            SqlParameter[] Parameters = new SqlParameter[6];

            Parameters[0] = new SqlParameter("@filaName", filaName);
            Parameters[1] = new SqlParameter("@fileType", fileType);
            Parameters[2] = new SqlParameter("@username", username);
            Parameters[3] = new SqlParameter("@usage", usage);
            Parameters[4] = new SqlParameter("@enduser", enduser);
            Parameters[5] = new SqlParameter("@folder", folder);

            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, "QJDAM_Production_Highres_Success_LOG_ADD", Parameters);


        }
        catch
        {

        }
    }

}

