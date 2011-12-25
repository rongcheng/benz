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
    public interface IRole  : IOperator
    {
        IGroup Owner { get; }

        Guid RoleId { get;set;}
        string RoleName { get;}
        Guid GroupId { get;}
        UserCollection Members { get;}
    }
}
