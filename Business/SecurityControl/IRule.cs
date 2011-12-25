using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business.SecurityControl
{
    /// <summary>
    /// πÊ‘Ú
    /// </summary>
    public interface IRule
    {
        bool IsValidate { get;  set;}
        void CheckValidate();
        Guid RuleId { get; set;}
        ISecurityObject SecurityObject { get;}
        IOperator Operator { get;}
        OperatorMethod Method { get;}
        string GetSqlQuery();
         
    }
}
