using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business.SecurityControl
{
    /// <summary>
    /// �ܿض���
    /// </summary>
    public interface ISecurityObject
    {
        Guid ObjectId { get; set;}
        SecurityObjectType ObjectType { get; set;}
    }
}
