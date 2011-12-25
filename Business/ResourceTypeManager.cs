using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QJVRMS.Business
{
    public class ResourceTypeManager
    {
        /// <summary>
        /// 类型编码
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 获取所有类型列表
        /// </summary>
        /// <returns></returns>
        public static List<ResourceTypeManager> GetTypeList()
        {
            List<ResourceTypeManager> list = new List<ResourceTypeManager>();

            list.Add(new ResourceTypeManager { TypeCode = "Photo", TypeName = "图片" });
            list.Add(new ResourceTypeManager { TypeCode = "Necos", TypeName = "新闻稿" });
            list.Add(new ResourceTypeManager { TypeCode = "Video", TypeName = "视频" });
            list.Add(new ResourceTypeManager { TypeCode = "Docs", TypeName = "文档" });
            list.Add(new ResourceTypeManager { TypeCode = "Audio", TypeName = "音频" });

            return list;
        }
    }
}
