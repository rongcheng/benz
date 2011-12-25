using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;
using QJVRMS.DataAccess;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///KeywordService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class KeywordService : System.Web.Services.WebService
{

    public KeywordService()
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
    public int Add(int parentId,string keyword,int? sort)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into KeyWords(");
        strSql.Append("parentId,keyword,sort)");
        strSql.Append(" values (");
        strSql.Append("@parentId,@keyword,@sort)");
        strSql.Append(";select @@IDENTITY");
        SqlParameter[] parameters = {
					new SqlParameter("@parentId", SqlDbType.Int,4),
					new SqlParameter("@keyword", SqlDbType.NVarChar,20),
					new SqlParameter("@sort", SqlDbType.Int,4)};
        parameters[0].Value = parentId;
        parameters[1].Value = keyword;
        parameters[2].Value = sort;

        
        return  SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql.ToString(), parameters);
           
    }

    /// <summary>
    /// 获得数据列表
    /// </summary>
    [WebMethod]
    public DataSet GetKeywordsByParentId(int parentId)
    {
       
        string sql = "select * FROM KeyWords where parentId=@pid order by keyword";
        SqlParameter[] ps = { new SqlParameter("@pid",SqlDbType.Int,4)};
        ps[0].Value = parentId;

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, ps);
    }



    /// <summary>
    /// 更新一条数据
    /// </summary>
    [WebMethod]
    public void UpdateById(int id,int parentId,string keyword,int sort)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update KeyWords set ");
        strSql.Append("parentId=@parentId,");
        strSql.Append("keyword=@keyword,");
        strSql.Append("sort=@sort");
        strSql.Append(" where id=@id ");
        SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@parentId", SqlDbType.Int,4),
					new SqlParameter("@keyword", SqlDbType.NVarChar,20),
					new SqlParameter("@sort", SqlDbType.Int,4)};
        parameters[0].Value = id;
        parameters[1].Value = parentId;
        parameters[2].Value = keyword;
        parameters[3].Value = sort;


        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql.ToString(), parameters);
    }


    /// <summary>
    /// 删除一条数据
    /// </summary>
    [WebMethod]
    public void Delete(int id)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from KeyWords ");
        strSql.Append(" where id=@id or parentid=@id");
        SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
        parameters[0].Value = id;

        SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, strSql.ToString(), parameters);
    }



}

