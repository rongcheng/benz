using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.Business.SecurityControl;
namespace QJVRMS.Business
{
    /// <summary>
    /// Author: Sunan
    /// Date: 2008.05.07
    /// </summary>
    public interface IGroup
    {
        Guid GroupId { get;  }
        string GroupName { get; set;}
        UserCollection Members { get;set;}
    }
}
