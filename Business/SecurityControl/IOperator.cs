using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business.SecurityControl
{
    public interface IOperator
    {
        Guid OperatorId { get;}
       List<IOperator> CorrelativeOperator { get;}

    }
}
