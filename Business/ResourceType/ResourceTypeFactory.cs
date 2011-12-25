using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.Business.Interface;

namespace QJVRMS.Business.ResourceType
{
    /// <summary>
    /// 资源类型工厂类，用来生成各种不同的资源类型
    /// </summary>
    public class ResourceTypeFactory
    {
        /// <summary>
        /// 根据资源类型生成对应的类，image video document 
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public static IResourceType getResourceTypeByString(string resourceType)
        {
            IResourceType obj;

            resourceType = resourceType.ToLower();

            if (resourceType.Equals("image"))
            {
                obj = new ImageType();
            }
            else if (resourceType.Equals("video"))
            {
                obj = new VideoType();
            }
            else if (resourceType.Equals("document"))
            {
                obj = new DocumentType();
            }
            else
            {
                obj = new OtherType();
            }
            return obj;

        }

        /// <summary>
        /// 根据文件扩展名来生成对应的资源类型
        /// </summary>
        /// <param name="fileExtention"></param>
        /// <returns></returns>
        public static IResourceType getResourceType(string fileExtention)
        {
            IResourceType obj;

            fileExtention = fileExtention.ToLower();

            if (ArrayContains(new ImageType().FileExtention, fileExtention))
            {
                obj = new ImageType();                
            }
            else if (ArrayContains(new VideoType().FileExtention, fileExtention))
            {
                obj = new VideoType();
            }
            else if (ArrayContains(new DocumentType().FileExtention, fileExtention))
            {
                obj = new DocumentType();
            }
            else
            {
                obj = new OtherType();
            }
            return obj;
        }

        private static bool ArrayContains(string[] arr, string s)
        {
            s=s.ToLower();
            foreach (string arrElement in arr)
            {
                if (arrElement.ToLower().Equals(s))
                    return true;
            }
            return false;
        }

    }
}
