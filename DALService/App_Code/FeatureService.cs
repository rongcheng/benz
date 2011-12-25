using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using QJVRMS.Business;
using QJVRMS.DataAccess;
using System.Xml.Serialization;
using QJVRMS.Common;
using System.DirectoryServices;
using ActiveDs;
using QJVRMS.Business.SecurityControl;

/// <summary>
///FeatureService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FeatureService : System.Web.Services.WebService {

    public FeatureService() {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod]
    public DataTable GetFeatures(string userName, int pageSize, int pageIndex, ref int totalRecord) {
        SqlParameter[] parameters = new SqlParameter[4];
        parameters[0] = new SqlParameter("@UserName", SqlDbType.NVarChar);
        parameters[1] = new SqlParameter("@PageSize", SqlDbType.Int);
        parameters[2] = new SqlParameter("@PageIndex", SqlDbType.Int);
        parameters[3] = new SqlParameter("@TotalRecord", SqlDbType.Int);

        parameters[0].Value = userName;
        parameters[1].Value = pageSize;
        parameters[2].Value = pageIndex;
        parameters[3].Direction = ParameterDirection.Output;

        try {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Feature_GetFeatures", parameters).Tables[0];
            totalRecord = int.Parse(parameters[3].Value.ToString());

            return dt;
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public DataTable ShowFeatures(string userName, int pageSize, int pageIndex, ref int totalRecord) {
        SqlParameter[] parameters = new SqlParameter[4];
        parameters[0] = new SqlParameter("@UserName", SqlDbType.NVarChar);
        parameters[1] = new SqlParameter("@PageSize", SqlDbType.Int);
        parameters[2] = new SqlParameter("@PageIndex", SqlDbType.Int);
        parameters[3] = new SqlParameter("@TotalRecord", SqlDbType.Int);

        parameters[0].Value = userName;
        parameters[1].Value = pageSize;
        parameters[2].Value = pageIndex;
        parameters[3].Direction = ParameterDirection.Output;

        try {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Feature_ShowFeatures", parameters).Tables[0];
            totalRecord = int.Parse(parameters[3].Value.ToString());

            return dt;
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public DataTable GetFeature(string featureId) {
        string sql = "select * from Feature where FeatureId = @FeatureId";
        SqlParameter param = new SqlParameter("@FeatureId", SqlDbType.NVarChar);
        param.Value = featureId;

        try {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, param).Tables[0];
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public DataTable GetFeatureDetails(string featureId) {
        string sql = "select FDId from dbo.Feature_detail where FeatureId = @FeatureId";
        SqlParameter param = new SqlParameter("@FeatureId", SqlDbType.NVarChar);
        param.Value = featureId;

        try {
            return SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sql, param).Tables[0];
        }
        catch {
            return null;
        }
    }

    [WebMethod]
    public bool UpdateCoverImage(string featureId, string fileName, string folderName) {
        string sql = "update dbo.Feature set CoverImage = @CoverImage, FolderName = @FolderName where FeatureId = @FeatureId";
        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter("@FeatureId", SqlDbType.NVarChar);
        parameters[1] = new SqlParameter("@CoverImage", SqlDbType.NVarChar);
        parameters[2] = new SqlParameter("@FolderName", SqlDbType.NVarChar);

        parameters[0].Value = featureId;
        parameters[1].Value = fileName;
        parameters[2].Value = folderName;

        try {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, sql, parameters) > 0;
        }
        catch (Exception ex) {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }

    [WebMethod]
    public bool AddFeatureDetail(Guid featureId, Guid imageId) {
        SqlParameter[] parameters = new SqlParameter[2];

        parameters[0] = new SqlParameter("@FeatureId", SqlDbType.UniqueIdentifier);
        parameters[1] = new SqlParameter("@ImageId", SqlDbType.UniqueIdentifier);

        parameters[0].Value = featureId;
        parameters[1].Value = imageId;

        try {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Feature_AddDetail", parameters) > 0;

        }
        catch (Exception ex) {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }

    [WebMethod]
    public DataTable GetFeatureImages(Guid featureId, int type, int pageSize, int pageIndex, ref int totalRecord) {
        SqlParameter[] parameters = new SqlParameter[5];

        parameters[0] = new SqlParameter("@FeatureId", SqlDbType.UniqueIdentifier);
        parameters[1] = new SqlParameter("@Type", SqlDbType.Int);
        parameters[2] = new SqlParameter("@PageSize", SqlDbType.Int);
        parameters[3] = new SqlParameter("@PageIndex", SqlDbType.Int);
        parameters[4] = new SqlParameter("@TotalRecord", SqlDbType.Int);

        parameters[0].Value = featureId;
        parameters[1].Value = type;
        parameters[2].Value = pageSize;
        parameters[3].Value = pageIndex;
        parameters[4].Direction = ParameterDirection.Output;

        try {
            DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Feature_GetList", parameters).Tables[0];
            totalRecord = int.Parse(parameters[4].Value.ToString());

            return dt;
        }
        catch {
            return null;
        }
    }

    /// <summary>
    /// 添加和更新数据
    /// </summary>
    /// <param name="featureId"></param>
    /// <param name="featureName"></param>
    /// <param name="featureDes"></param>
    /// <param name="creator"></param>
    /// <param name="state"></param>
    /// <param name="coverImage"></param>
    /// <returns></returns>
    [WebMethod]
    public bool EditFeature(Guid featureId, string featureName,
        string featureDes, string creator, bool state, string coverImage, string type) {
        SqlParameter[] parameters = new SqlParameter[6];
        parameters[0] = new SqlParameter("@FeatureId", SqlDbType.UniqueIdentifier);
        parameters[1] = new SqlParameter("@FeatureName", SqlDbType.NVarChar);
        parameters[2] = new SqlParameter("@FeatureDes", SqlDbType.NVarChar);
        parameters[3] = new SqlParameter("@Creator", SqlDbType.VarChar);
        parameters[4] = new SqlParameter("@State", SqlDbType.Bit);
        parameters[5] = new SqlParameter("@CoverImage", SqlDbType.NVarChar);

        //if (type == "Update")
            parameters[0].Value = featureId;
        //else if (type == "Add")
            //parameters[0].Value = Guid.NewGuid();
        parameters[1].Value = featureName;
        parameters[2].Value = featureDes;
        parameters[3].Value = creator;
        parameters[4].Value = state;
        parameters[5].Value = coverImage;

        try {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS,
                CommandType.StoredProcedure, "Fearture_Edit", parameters) > 0;
        }
        catch (Exception ex) {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }

    [WebMethod]
    public bool DeleteFeatureDetail(Guid id) {
        SqlParameter[] parameters = new SqlParameter[1];

        parameters[0] = new SqlParameter("@FDId", SqlDbType.UniqueIdentifier);
        parameters[0].Value = id;

        try {
            return SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Feature_DeleteDetail", parameters) > 0;
        }
        catch {
            return false;
        }
    }

    [WebMethod]
    public DataSet SearchResource(string keyword, string beginDate, string endDate, 
        string Catalogid, string Userid, int PageSize, int PageNum, ref int rowCount, 
        string resourceType, string groupId, int validateStatus, Guid featureId, string type) {
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
                    new SqlParameter("@validateStatus",SqlDbType.Int,10),
                    new SqlParameter("@FeatureId", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@Type", SqlDbType.NVarChar)
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
        paramater[10].Value = featureId;
        paramater[11].Value = type;

        using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "QJDAM_SearchResourceFeature", paramater)) {
            //rowCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            //DataTable dt = ds.Tables[1];
            return ds;
        }
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

    [WebMethod]
    public DataSet GetResourcesImages(string id) {
        SqlParameter param = new SqlParameter("@ID", SqlDbType.NVarChar, 50);
        param.Value = id;
        try {
            using (DataSet ds = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Resources_GetImages", param)) {
                return ds;
            }
        }
        catch {
            return null;
        }
    }
}

