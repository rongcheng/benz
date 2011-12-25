using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business.SecurityControl
{
    /// <summary>
    ///  ‹øÿ∂‘œÛ
    /// </summary>
    public interface ISecurityObject
    {
        Guid ObjectId { get; set;}
        SecurityObjectType ObjectType { get; set;}
    }
}
