using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business
{
    interface IUsage
    {
        int UsageID { get;set;  }
        string UsageName { get; set;}
        string UsageDesc { get; set;}
        Guid GroupID { get;set;}
    }
}
