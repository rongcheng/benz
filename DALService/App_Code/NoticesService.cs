using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.DirectoryServices;
using ActiveDs;
using QJVRMS.Business;
using QJVRMS.DataAccess;
using QJVRMS.Common;
using QJVRMS.Business.SecurityControl;

/// <summary>
///NoticesService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class NoticesService : System.Web.Services.WebService {

    public NoticesService() {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public DataTable GetNotices(int pageSize, int pageIndex, ref int totalRecord) {
        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter("@PageSize", SqlDbType.Int);
        parameters[1] = new SqlParameter("@PageIndex", SqlDbType.Int);
        parameters[2] = new SqlParameter("@TotalRecord", SqlDbType.Int);

        parameters[0].Value = pageSize;
        parameters[1].Value = pageIndex;
        parameters[2].Direction = ParameterDirection.Output;

        try {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Notices_GetNotices", parameters).Tables[0];
            totalRecord = int.Parse(parameters[2].Value.ToString());

            return dt;
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public DataTable ShowNotices() {
        string sql = "select top 7 * from dbo.Notices order by NoticeDate desc";

        try {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS,
                 CommandType.Text, sql).Tables[0];
            return dt;
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public DataTable GetNotice(string noticeId) {
        string sql = "select * from Notices where NoticeId = @NoticeId";
        SqlParameter param = new SqlParameter("@NoticeId", SqlDbType.NVarChar);
        param.Value = noticeId;

        try {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, param).Tables[0];
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public bool EditNotice(Guid noticeId, string noticeName, string noticeContent, string creator, string type) {
        SqlParameter[] parameters = new SqlParameter[4];
        parameters[0] = new SqlParameter("@NoticeId", SqlDbType.UniqueIdentifier);
        parameters[1] = new SqlParameter("@NoticeName", SqlDbType.NVarChar);
        parameters[2] = new SqlParameter("@NoticeContent", SqlDbType.NVarChar);
        parameters[3] = new SqlParameter("@Creator", SqlDbType.NVarChar);

        //if (type == "Update")
            parameters[0].Value = noticeId;
        //else if (type == "Add")
            //parameters[0].Value = Guid.NewGuid();
        parameters[1].Value = noticeName;
        parameters[2].Value = noticeContent;
        parameters[3].Value = creator;

        try {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Notices_Edit", parameters) > 0;
        }
        catch (Exception ex) {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }

    [WebMethod]
    public bool DeleteNotice(Guid noticeId) {
        SqlParameter[] parameters = new SqlParameter[1];

        parameters[0] = new SqlParameter("@NoticeId", SqlDbType.UniqueIdentifier);
        parameters[0].Value = noticeId;

        try {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Notices_Delete", parameters) > 0;
        }
        catch {
            return false;
        }
    }
}

