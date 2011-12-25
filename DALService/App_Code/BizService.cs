using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using QJVRMS.Business;
using QJVRMS.DataAccess;
using System.Xml.Serialization;
using QJVRMS.Common;

/// <summary>
///  其他业务服务
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.None)]

public class BizService : System.Web.Services.WebService
{

    public BizService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    #region 新闻


    [WebMethod]
    public DataTable GetNews(Guid newsId)
    {
        string sql = "Select * From News Where newsId=@newsId";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@newsId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = newsId;



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
    public DataTable GetNewsList(string title,char ntype)
    {
        string sql = "Select Top 50 * From News Where title like @title";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@title", SqlDbType.NVarChar);
        Parameters[0].Value = title;


        if (ntype != null)
        {
            sql += " And ntype=@ntype";
            Array.Resize<SqlParameter>(ref Parameters, Parameters.Length + 1);
            Parameters[Parameters.Length - 1] = new SqlParameter("@ntype", SqlDbType.Char);
            Parameters[Parameters.Length - 1].Value = ntype;
        }

        sql += " Order by IsTop desc,CreateDate desc";

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
    public bool UpdateNews(Guid newsId, string title, string content, char isVal, char isTop,char ntype)
    {
        string sql = "Update News Set title=@title,nContent=@content,isValidate=@isVal,isTop=@isTop,ntype=@ntype Where newsId=@newsId";

        SqlParameter[] Parameters = new SqlParameter[6];
        Parameters[0] = new SqlParameter("@title", SqlDbType.NVarChar);
        Parameters[1] = new SqlParameter("@content", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@newsId", SqlDbType.UniqueIdentifier);
        Parameters[3] = new SqlParameter("@isVal", SqlDbType.Char);
        Parameters[4] = new SqlParameter("@isTop", SqlDbType.Char);
        Parameters[5] = new SqlParameter("@ntype", SqlDbType.Char);

        Parameters[0].Value = title;
        Parameters[1].Value = content;
        Parameters[2].Value = newsId;
        Parameters[3].Value = isVal;
        Parameters[4].Value = isTop;
        Parameters[5].Value = ntype;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }


    [WebMethod]
    public bool DeleteNews(Guid newsId)
    {
        string sql = "Delete From News Where newsId=@newsId";

        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@newsId", SqlDbType.UniqueIdentifier);

        Parameters[0].Value = newsId;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }

    [WebMethod]
    public bool CreateNews(Guid newsId, string title,
            string content,
            DateTime createDate,
            string loginId,
            string userName,
            Guid userId,
        char isVal,
        char isTop,
        char ntype)
    {
        string sql = @"Insert into News (newsId,title,nContent,createDate,loginId,userId,userName,isValidate,isTop,ntype)"
                    + " Values (@newsId,@title,@nContent,@createDate,@loginId,@userId,@userName,@isVal,@isTop,@ntype)";


        SqlParameter[] Parameters = new SqlParameter[10];
        Parameters[0] = new SqlParameter("@newsId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@title", SqlDbType.NVarChar);
        Parameters[2] = new SqlParameter("@nContent", SqlDbType.NVarChar);
        Parameters[3] = new SqlParameter("@createDate", SqlDbType.DateTime);
        Parameters[4] = new SqlParameter("@loginId", SqlDbType.VarChar);
        Parameters[5] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[6] = new SqlParameter("@userName", SqlDbType.NVarChar);
        Parameters[7] = new SqlParameter("@isVal", SqlDbType.NVarChar);
        Parameters[8] = new SqlParameter("@isTop", SqlDbType.NVarChar);
        Parameters[9] = new SqlParameter("@ntype", SqlDbType.Char);


        Parameters[0].Value = newsId;
        Parameters[1].Value = title;
        Parameters[2].Value = content;
        Parameters[3].Value = createDate;
        Parameters[4].Value = loginId;
        Parameters[5].Value = userId;
        Parameters[6].Value = userName;
        Parameters[7].Value = isVal;
        Parameters[8].Value = isTop;
        Parameters[9].Value = ntype;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }

    }

    #endregion

    #region 其他

    [WebMethod]
    public DataTable GetTopLatestImage()
    {
        string sql = "Select Top 5 itemId,userId,ItemSerialNum,FolderName,Caption,ImageType From ImageStorage Order By uploadDate Desc";

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql).Tables[0];

      
    }


    [WebMethod]
    public DataTable GetTopImagesOfCatalog(Guid parentCataId)
    {
        string sql = "Select Top 5 itemId,userId,ItemSerialNum,FolderName,Caption,ImageType From"
                    + " ImageStorage i,ImageStorage_Catalogs ic Where i.itemId=ic.ImageStorageid and ic.catalogId in"
                    + " (Select c.catalogId From Catalogs c Where c.catalogId=@cataId or c.parentId=@cataId)"
                    + " Order by i.uploaddate desc";


        SqlParameter[] Parameters = new SqlParameter[1];
        Parameters[0] = new SqlParameter("@cataId", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = parentCataId;


        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql,Parameters).Tables[0];

    }

    #endregion 


    #region 获取流水号

    [WebMethod]
    public string GetSN(string prefix)
    {
        return GetSysSeq(prefix, 5, DateTime.Now);
    }


    [WebMethod]
    public string GetImageSeq(DateTime dt)
    {
        return GetSysSeq("PA", 5, dt);
    }

    [WebMethod]
    public string GetImageSeqByServerDate()
    {
        return GetSysSeq("PA", 5, DateTime.Now);
    }

    [WebMethod]
    public string GetVideoSeq(DateTime dt)
    {
        return GetSysSeq("V", 5, dt);
    }

    [WebMethod]
    public string GetSysSeq(string preFix, int flowNumLength, DateTime date)
    {
        if (string.IsNullOrEmpty(preFix))
        {
            throw new System.ArgumentNullException("areaPreFix");
        }

        string seqNum = string.Empty;
        string formatSeqNum = string.Empty;
        string finalFLowNum = string.Empty;
        string dateStr = string.Empty;

        preFix = preFix.ToUpper();
        dateStr = date.ToString("yyMMdd");

        SqlParameter[] parameters = {
					new SqlParameter("@ParamID", SqlDbType.VarChar,5),	
                    new SqlParameter("@Date", SqlDbType.VarChar,10)                    
            };
        parameters[0].Value = preFix;
        parameters[1].Value = dateStr;


        seqNum = SqlHelper.ExecuteScalar(CommonInfo.ConQJVRMS, "QJDAM_GetSysSeq", parameters).ToString();
        formatSeqNum = seqNum;

        while (formatSeqNum.Length < flowNumLength)
        {
            formatSeqNum = "0" + formatSeqNum;
        }

        finalFLowNum = preFix + dateStr + formatSeqNum;

        return finalFLowNum;
    }

    #endregion

    /// <summary>
    /// 添加AD用户到DB
    /// </summary>
    /// <param name="userList"></param>
    /// <param name="groupid"></param>
    /// <returns></returns>
    [WebMethod]
    public bool AddADUsersToDB(string userListStr, Guid groupid)
    {
        DataTable userTable = new DataTable();
        userTable.Columns.Add("UserId", typeof(Guid));
        userTable.Columns.Add("GroupId", typeof(Guid));
        userTable.Columns.Add("loginName", typeof(string));
        userTable.Columns.Add("UserName", typeof(string));
        userTable.Columns.Add("Tel", typeof(string));
        userTable.Columns.Add("Email", typeof(string));
        userTable.Columns.Add("uType", typeof(string));

        userTable.Columns.Add("password", typeof(string));
        userTable.Columns.Add("isLocked", typeof(string));
        userTable.Columns.Add("isDownload", typeof(string));
        userTable.Columns.Add("isIpValidate", typeof(string));
        userTable.Columns.Add("IpAddress", typeof(string));
        userTable.Columns.Add("CreateDate", typeof(DateTime));



        SerializeObjectFactory sof = new SerializeObjectFactory();
        ArrayList userList = (ArrayList)sof.DesializeFromBase64(userListStr);

        foreach (object ouser in userList)
        {
            QJVRMS.Business.User user = ouser as QJVRMS.Business.User;

            DataRow userRow = userTable.NewRow();

            userRow["UserId"] = user.UserId;
            userRow["GroupId"] = groupid;
            userRow["loginName"] = user.UserLoginName;
            userRow["UserName"] = user.UserName;
            userRow["Tel"] = user.Telphone;
            userRow["Email"] = user.Email;
            userRow["uType"] = "1";
            userRow["password"] = "123";
            userTable.Rows.Add(userRow);
        }

        SqlConnection con = null;
        SqlTransaction trans = null;

        try
        {
            con = new SqlConnection(CommonInfo.ConQJVRMS);
            con.Open();

            trans = con.BeginTransaction();
            SqlHelperExtend.Update("Users", userTable, trans);

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

