using System;
using System.Collections.Generic;
using System.Text;

namespace WebUI.UIBiz
{
    public interface IWebUser : QJVRMS.Business.SecurityControl.IOperator
    {
        Guid UserId { get;  }
        Guid UserGroupId { get; }
        string UserLoginName { get; }
        string UserName { get; }
        string GroupName { get;}
        string IsDownLoad { get;}
      
    }
}
