using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QJVRMS.Common;

namespace QJVRMS.Business
{
    public class CacheManager
    {
        public enum CacheType { TopCatalog, AllCatalog, Feature, ResouceType }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object GetItem(CacheType type)
        {

            switch (type)
            {
                case CacheType.TopCatalog:
                    if (QJVRMSCache.Get(type.ToString()) == null)
                    {
                        QJVRMSCache.Insert(type.ToString(), Catalog.GetTopCatalog());
                    }
                    break;
                case CacheType.AllCatalog:
                    if (QJVRMSCache.Get(type.ToString()) == null)
                    {
                        QJVRMSCache.Insert(type.ToString(), Catalog.GetAllCatalog());
                    }
                    break;
                case CacheType.Feature:
                    break;
                case CacheType.ResouceType:
                    if (QJVRMSCache.Get(type.ToString()) == null)
                    {
                        QJVRMSCache.Insert(type.ToString(), ResourceTypeManager.GetTypeList());
                    }
                    break;
                default:
                    break;
            }

            return QJVRMSCache.Get(type.ToString());
        }
    }
}
