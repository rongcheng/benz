using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace QJVRMS.Business {
    interface IFeature {
        bool EditFeature(string featureId, string featureName,
            string featureDes, string creator, bool state, string coverImage, string type);
        string GetFeaturesContent(string userName, int pageSize, int pageIndex);
        string ShowFeaturesContent(string userName, int pageSize, int pageIndex);
        string GetFeatureContent(string featureId, string logName);
        string GetFeatureImagesContent(string featureId, int type, int pageSize, int pageIndex);
        string ShowFeatureImagesContent(string featureId, int type, int pageSize, int pageIndex);
        string SearchImagesContent(string keyWord, string catalogId, string featureId, int pageSize, int pageNum, string param, string type);
        bool AddFeatureDetail(string featureId, string imageId);
        string GetTopCatalogContent();
        string GetChildCatalogContent(string parentId);
        string CatalogImagesContent(string catalogId, string userId, string featureId, int pageSize, int pageNum, string param, string type);
        bool DeleteFeatureDetail(string id);
        bool UpdateCoverImage(string featureId, string fileName, string floderName);
        DataTable ShowFeatureImages(string featureId, int type, int pageSize, int pageIndex, ref int totalRecord);
        DataTable GetFeatures(string userName, int pageSize, int pageIndex);
    }
}
