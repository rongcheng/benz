using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;

using QJVRMS.DataAccess;


/// <summary>
/// GiftService 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class GiftService : System.Web.Services.WebService
{

    public GiftService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 获取礼品类型列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public DataTable GetGiftTypeList()
    {
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, System.Data.CommandType.StoredProcedure, "UP_GiftType_GetList").Tables[0];
    }

    /// <summary>
    /// 添加礼品信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <param name="typeId"></param>
    /// <param name="quantity"></param>
    /// <param name="imageId"></param>
    /// <param name="status"></param>
    /// <param name="remark"></param>
    /// <returns></returns>
    [WebMethod]
    public int AddGift(string id, string title, string typeId, int quantity, string imageId, int status, string remark)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@Id",id),
            new SqlParameter("@Title",title),
            new SqlParameter("@TypeId",typeId),
            new SqlParameter("@Quantity",quantity),
            new SqlParameter("@ImageId",new Guid(imageId)),
            new SqlParameter("@status",status),
            new SqlParameter("@remark",remark)
        };

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Gift_ADD", parameters);
    }

    /// <summary>
    /// 更新礼品信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <param name="typeId"></param>
    /// <param name="quantity"></param>
    /// <param name="imagesId"></param>
    /// <param name="status"></param>
    /// <param name="remark"></param>
    /// <returns></returns>
    [WebMethod]
    public int UpdateGift(string id, string title, string typeId, int quantity, string imageId, int status, string remark)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@Id",id),
            new SqlParameter("@Title",title),
            new SqlParameter("@TypeId",typeId),
            new SqlParameter("@Quantity",quantity),
            new SqlParameter("@ImageId",new Guid(imageId)),
            new SqlParameter("@status",status),
            new SqlParameter("@remark",remark)
        };

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Gift_Update", parameters);
    }

    /// <summary>
    /// 删除礼品信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public int DeleteGift(string id)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@Id",id)
        };

        return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Gift_Delete", parameters);
    }

    /// <summary>
    /// 获取礼品信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable GetGiftModel(string id)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@Id",id)
        };

        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Gift_GetModel", parameters).Tables[0];
    }

    /// <summary>
    /// 获取新的礼品ID
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string GetNewId()
    {
        string flag = "GF";
        string dateFlag = DateTime.Now.ToString("yyyyMMdd");

        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@ParamID",flag),
            new SqlParameter("@Date",dateFlag)
        };
        DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_GetSysSeq", parameters).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return flag + dateFlag + dt.Rows[0][0].ToString().PadLeft(4, '0');
        }
        return null;
    }

    /// <summary>
    /// 查询礼品列表
    /// </summary>
    /// <param name="title"></param>
    /// <param name="typeId"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable GetGiftList(string title, string typeId, string imageId)
    {
        SqlParameter[] parameters = new SqlParameter[] { 
            new SqlParameter("@title",title),
            new SqlParameter("@typeId",typeId),
            new SqlParameter("@imageId",imageId)
        };
        return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "UP_Gift_GetList", parameters).Tables[0];
    }
}

