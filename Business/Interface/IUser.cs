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
    public interface IUser : IOperator
    {
        Group OwnerGroup { get; set;}
        
      //  string Description { get; set;}
        string UserName { get; set;}
        Guid UserId { get; set;}
        string UserLoginName { get; set;}
        RoleCollection Roles { get;}
    }
}
