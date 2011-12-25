using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.DirectoryServices;
using ActiveDs;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.Common;
using QJVRMS.Business;
using QJVRMS.DataAccess;
using QJVRMS.Business.SecurityControl;

/// <summary>
///CalendarService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CalendarService : System.Web.Services.WebService {

    public CalendarService() {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public DataTable GetCalendarsMonth(string startTime, string endTime, string creator) {
        string sql = "select * from dbo.Calendar where Creator = @Creator and EndTime between @StartTime and @EndTime";
        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter("@StartTime", SqlDbType.NVarChar);
        parameters[1] = new SqlParameter("@EndTime", SqlDbType.NVarChar);
        parameters[2] = new SqlParameter("@Creator", SqlDbType.NVarChar);

        parameters[0].Value = startTime;
        parameters[1].Value = endTime;
        parameters[2].Value = creator;

        try {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS,
                CommandType.Text, sql, parameters).Tables[0];
            return dt;
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public DataTable GetCalendars(string nowTime, string creator) {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@NowTime", SqlDbType.NVarChar);
        parameters[1] = new SqlParameter("@Creator", SqlDbType.NVarChar);

        parameters[0].Value = nowTime;
        parameters[1].Value = creator;

        try {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Calendar_GetCalendars", parameters).Tables[0];
            return dt;
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public DataTable ShowCalendars(string nowTime, string creator) {
        //string sql = "select top 5 * from dbo.Calendar where Creator = @Creator and EndTime >= @NowTime order by StartTime";
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@NowDate", SqlDbType.NVarChar);
        parameters[1] = new SqlParameter("@Creator", SqlDbType.NVarChar);

        parameters[0].Value = nowTime;
        parameters[1].Value = creator;

        try {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Calendar_GetDefaultCalendars", parameters).Tables[0];
            return dt;
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public DataTable GetCalendar(Guid calendarId) {
        SqlParameter param = new SqlParameter("@CalendarId", SqlDbType.UniqueIdentifier);
        param.Value = calendarId;

        try {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, 
                CommandType.StoredProcedure, "Calendar_GetCalendar", param).Tables[0];
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public bool EditCalendar(Guid calendarId, string theme, string site, string label,
        string sDate, string sTime, string eDate, string eTime, string content, string creator) {
        SqlParameter[] parameters = new SqlParameter[10];

        parameters[0] = new SqlParameter("@CalendarId", SqlDbType.UniqueIdentifier);
        parameters[1] = new SqlParameter("@Theme", SqlDbType.NVarChar);
        parameters[2] = new SqlParameter("@Site", SqlDbType.NVarChar);
        parameters[3] = new SqlParameter("@Label", SqlDbType.NVarChar);
        parameters[4] = new SqlParameter("@SDate", SqlDbType.NVarChar);
        parameters[5] = new SqlParameter("@STime", SqlDbType.NVarChar);
        parameters[6] = new SqlParameter("@EDate", SqlDbType.NVarChar);
        parameters[7] = new SqlParameter("@ETime", SqlDbType.NVarChar);
        parameters[8] = new SqlParameter("@DContent", SqlDbType.NVarChar);
        parameters[9] = new SqlParameter("@Creator", SqlDbType.NVarChar);

        parameters[0].Value = calendarId;
        parameters[1].Value = theme;
        parameters[2].Value = site;
        parameters[3].Value = label;
        parameters[4].Value = sDate;
        parameters[5].Value = sTime;
        parameters[6].Value = eDate;
        parameters[7].Value = eTime;
        parameters[8].Value = content;
        parameters[9].Value = creator;

        try {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Calendar_Edit", parameters) > 0;
        }
        catch (Exception ex) {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }
    
    [WebMethod]
    public bool DeleteCalendar(Guid calendarId) {
        SqlParameter[] parameters = new SqlParameter[1];

        parameters[0] = new SqlParameter("@CalendarId", SqlDbType.UniqueIdentifier);
        parameters[0].Value = calendarId;

        try {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Calendar_Delete", parameters) > 0;
        }
        catch {
            return false;
        }
    }

    [WebMethod]
    public DataSet SearchCalendars(string monthTime, string sTime, string eTime, string state,
        string creator, int pageSize, int pageIndex, int type) {
        SqlParameter[] parameters = new SqlParameter[8];
        parameters[0] = new SqlParameter("@MonthTime", SqlDbType.NVarChar, 15);
        parameters[1] = new SqlParameter("@STime", SqlDbType.NVarChar, 15);
        parameters[2] = new SqlParameter("@ETime", SqlDbType.NVarChar, 15);
        parameters[3] = new SqlParameter("@State", SqlDbType.NVarChar, 2);
        parameters[4] = new SqlParameter("@Creator", SqlDbType.NVarChar, 20);
        parameters[5] = new SqlParameter("@PageSize", SqlDbType.Int);
        parameters[6] = new SqlParameter("@PageIndex", SqlDbType.Int);
        parameters[7] = new SqlParameter("@Type", SqlDbType.Int);

        parameters[0].Value = monthTime;
        parameters[1].Value = sTime;
        parameters[2].Value = eTime;
        parameters[3].Value = state;
        parameters[4].Value = creator;
        parameters[5].Value = pageSize;
        parameters[6].Value = pageIndex;
        parameters[7].Value = type;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Calendar_SearchCalendar", parameters)) {
            return ds;
        }
    }
}

