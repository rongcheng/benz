using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using QJVRMS.DataAccess;
using QJVRMS.Common;
using QJVRMS.Business;
using System.Data.SqlClient;
using System.Data;



/// <summary>
/// 提供对视频文件的数据操作
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class VideoStorageService : System.Web.Services.WebService
{

    public VideoStorageService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    [WebMethod]
    public bool AddVideo
    (
        Guid ID ,
        string ItemSerialNumber ,
        string FileName ,
        string FilePath ,
        string ServerFileName,
        string FlvFileName ,
        string FlvFilePath ,
        string Caption ,
        DateTime StartDate ,
        DateTime EndDate ,
        DateTime UploadDate ,
        DateTime ShotDate ,
        string Keyword ,
        string Description ,
        DateTime UpdateTime,
        Guid UserID ,
        int Status

    )
    {
        bool _ret = false;
        string sql = @"INSERT INTO dbo.VideoStorage
        ( ID ,
          ItemSerialNumber ,
          FileName ,
          FilePath ,
          ServerFileName,  
          FlvFileName ,
          FlvFilePath ,
          Caption ,
          StartDate ,
          EndDate ,
          UploadDate ,
          ShotDate ,
          Keyword ,
          Description ,
          UpdateTime ,
          UserID ,
          Status
        )
        VALUES  (
          @ItemId,
          @ItemSerialNumber ,
          @FileName ,
          @FilePath ,
          @ServerFileName,
          @FlvFileName ,
          @FlvFilePath ,
          @Caption ,
          @StartDate ,
          @EndDate ,
          @UploadDate ,
          @ShotDate ,
          @Keyword ,
          @Description ,
          @UpdateTime ,
          @UserID ,
          @Status          
        )";

        int i = 0;
        SqlParameter[] ps = new SqlParameter[18];
        ps[i++] = new SqlParameter("@ItemId", ID);
        ps[i++] = new SqlParameter("@ItemSerialNumber", ItemSerialNumber);
        ps[i++] = new SqlParameter("@FileName", FileName);
        ps[i++] = new SqlParameter("@FilePath", FilePath);
        ps[i++] = new SqlParameter("@ServerFileName", ServerFileName);
        ps[i++] = new SqlParameter("@FlvFileName", FlvFileName);
        ps[i++] = new SqlParameter("@FlvFilePath", FlvFilePath);
        ps[i++] = new SqlParameter("@Caption", Caption);
        ps[i++] = new SqlParameter("@StartDate", StartDate);
        ps[i++] = new SqlParameter("@EndDate", EndDate);
        ps[i++] = new SqlParameter("@UploadDate", UploadDate);
        ps[i++] = new SqlParameter("@ShotDate", ShotDate);
        ps[i++] = new SqlParameter("@Keyword", Keyword);
        ps[i++] = new SqlParameter("@Description", Description);
        ps[i++] = new SqlParameter("@UpdateTime", UpdateTime);
        ps[i++] = new SqlParameter("@UserID", UserID);
        //ps[i++] = new SqlParameter("@ClipLength", v.cl);
        //ps[i++] = new SqlParameter("@ClipSize", v.Caption);
        ps[i++] = new SqlParameter("@Status", Status);

        try
        {

            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, ps);
            _ret = true;
        }
        catch
        { }


        return _ret;
    
    }



    /// <summary>
    /// 取得转换队列
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetUnConvertedVideos()
    {
        return GetVideosByStatus((int)VideoStatus.UnConverted);
    }

    /// <summary>
    /// 根据状态查询视频数据表，-1为查询所有的
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetVideosByStatus(int status)
    {
        string sql = "SELECT * FROM dbo.Resources where ResourceType='video' and 1=1";
        string sWhere = string.Empty;
        if (status != -1)
        {
            sWhere = " and status="+status.ToString();
        }
        sql += sWhere;
        sql += " order by uploaddate desc";

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql);
    
    }

    [WebMethod]
    public DataSet GetVideoInfo(string itemid)
    {
        string sql = "SELECT * FROM dbo.Resources where id=@sn";
        SqlParameter sp = new SqlParameter("@sn",itemid.ToString());
        SqlParameter[] sps = new SqlParameter[] { sp};
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql,sps);
    
    }

    [WebMethod]
    public bool UpdateVideoMetaData(string serialnumber,string duration, string bitrate,string videosize)
    {
        bool _ret = false;
        string sql = @"update VideoStorage 
                        set cliplength=@cliplength,
                            clipsize=@clipsize
                       where itemserialnumber=@sn";
        int i = 0;
        SqlParameter[] ps = new SqlParameter[3];
        ps[i++] = new SqlParameter("@cliplength", duration);
        ps[i++] = new SqlParameter("@clipsize", videosize);
        ps[i++] = new SqlParameter("@sn", serialnumber);
         

        try
        {

            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, ps);
            _ret = true;
        }
        catch
        { }


        return _ret;
        
    }


    [WebMethod]
    public bool UpdateVideoStatus(string serialnumber, int status)
    {
        bool _ret = false;
        string sql = @"update Resources set status=@status where itemserialnumber=@sn";
        int i = 0;
        SqlParameter[] ps = new SqlParameter[2];
        ps[i++] = new SqlParameter("@status", status);
        ps[i++] = new SqlParameter("@sn", serialnumber);

        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, ps);
            _ret = true;
        }
        catch
        { }


        //同时更新索引
        string[] SNs = new string[] { serialnumber };
        ResourceIndex.updateIndex(SNs);

        return _ret;

    }


    [WebMethod]
    public bool AddVideoToCatalog(Guid[] catalogId, Guid itemId)
    {
        DataTable mapTable = new DataTable();
        mapTable.Columns.Add("VideoStorageid", typeof(Guid));
        mapTable.Columns.Add("Catalogid", typeof(Guid));

        for (int i = 0; i < catalogId.Length; i++)
        {
            DataRow newRow = mapTable.NewRow();

            newRow["VideoStorageid"] = itemId;
            newRow["Catalogid"] = catalogId[i];

            mapTable.Rows.Add(newRow);
        }


        string sql = "Delete From VideoStorage_Catalogs Where videoStorageId=@itemId";
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
            SqlHelperExtend.Update("videoStorage_Catalogs", mapTable, trans);

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

}

