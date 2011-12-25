using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business
{
    interface IFunctionList
    {
        string FunctionID { get; set;}
        string FunctionName { get; set;}
        string UrlPath { get; set;}
        string Description { get; set;}
        int OrderFlag { get; set;}
        int Type { get;set;  }
        string FunctionImageName { get; set;}       
    }
}
