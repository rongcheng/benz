using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.DataAccess;

/// <summary>
///TempFiles 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class TempFiles : System.Web.Services.WebService
{

    public TempFiles()
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
    public DataTable GetTempFiles()
    {
        string sql = "Select * from TempFiles where expirationdate>getdate() Order By CreateDate Desc";
        SqlParameter[] Parameters = null;

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
    public void Insert(string title, string password_edit, string password_download, DateTime expirationDate, Guid userId)
    {
        string strSql = @"insert into TempFiles(id,title,password_edit,password_download,expirationdate,userid,createDate)
                    values(@id,@title,@password_edit,@password_download,@expirationdate,@userid,@createDate)";
        SqlParameter[] parameters = {
				new SqlParameter("@id",SqlDbType.UniqueIdentifier,16),
                new SqlParameter("@title",SqlDbType.NVarChar,100),
                new SqlParameter("@password_edit",SqlDbType.NVarChar,20),
                new SqlParameter("@password_download",SqlDbType.NVarChar,20),
                new SqlParameter("@expirationdate",SqlDbType.DateTime),
                new SqlParameter("@userid",SqlDbType.UniqueIdentifier,16),
                new SqlParameter("@createDate",SqlDbType.DateTime)
        };
        parameters[0].Value = Guid.NewGuid();
        parameters[1].Value = title;
        parameters[2].Value = password_edit;
        parameters[3].Value = password_download;
        parameters[4].Value = expirationDate;
        parameters[5].Value = userId;
        parameters[6].Value = DateTime.Now;

        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql, parameters);
        }
        catch
        { 
        }
    }


    [WebMethod]
    public bool delete(Guid id)
    {
        string sql = "Delete From TempFiles Where ID=@Id";

        SqlParameter[] Parameters = new SqlParameter[1];

        Parameters[0] = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
        Parameters[0].Value = id;

        try
        {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters) > 0;
        }
        catch
        {
            return false;
        }
    }



}