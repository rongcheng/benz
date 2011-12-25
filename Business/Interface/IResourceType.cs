using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business.Interface
{
    /// <summary>
    /// 资源接口
    /// </summary>
    public interface IResourceType
    {
        string SourcePath
        { get; }
        string[] SourcePaths { get; }
        
        string PreviewPath
        { get;  }
        string[] PreviewPaths { get; }
        string[] PreviewPath_Reads { get; }
        string DetailPage
        { get;  }
        string[] FileExtention
        { get; }

        string ResourceType
        { get; }

        string ResourceSNPrefix
        { get; }

        int PathNumber { get; set; }

        string GetSourcePath();

        string GetSourcePath(string[] paths);

        string GetSourcePath(string userName, string fileName);

        string GetPreviewPath(string userName, string fileName, string type);

        string GetPreviewPathRead(string userName, string fileName, string type);
    }
}
