using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business
{
    interface ISource
    {
        int SourceID { get;set;  }
        string SourceName { get; set;}
        string SourceDesc { get; set;}
        Guid GroupID { get;set;}
    }
}
