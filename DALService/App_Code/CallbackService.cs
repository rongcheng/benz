using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using QJVRMS.DataAccess;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// CallbackService 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CallbackService : System.Web.Services.WebService
{

    public CallbackService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool AddToLightBox(Guid imageId, Guid userId,string path,string serNum)
    {

        string sql = "Declare @c int Declare @e int"
                     + " Select @c=count(*) From LightBox Where itemId=@imageId and userId=@userId"
                     + " IF @c>0 return"
                     + " Select @e=count(*) From LightBox Where userId=@userId"
                     + " IF @e>=50 return"
                     + " Insert into LightBox (userId,itemId,createDate,itemPath,serialNum) values (@userId,@imageId,@createDate,@path,@serNum)";

        SqlParameter[] Parameters = new SqlParameter[5];


        Parameters[0] = new SqlParameter("@userId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@imageId", SqlDbType.UniqueIdentifier);
        Parameters[2] = new SqlParameter("@createDate", SqlDbType.DateTime);
        Parameters[3] = new SqlParameter("@path", SqlDbType.VarChar);
        Parameters[4] = new SqlParameter("@serNum", SqlDbType.VarChar);

        Parameters[0].Value = userId;
        Parameters[1].Value = imageId;
        Parameters[2].Value = DateTime.Now;
        Parameters[3].Value = path;
        Parameters[4].Value = serNum;

       
        try
        {
            int result = SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, Parameters);

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch(Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            throw new Exception("Wrong");
        }

        
    }


}

