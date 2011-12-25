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
    public interface ICatalog : ISecurityObject
    {
        Guid CatalogId { get; }
        string CatalogName { get; set;}

        Catalog ParentCatalog { get; }
        Guid ParentCatalogId { get; set;}
        CatalogCollection ChildrenCatalogs { get;}


        Group OwnerGroup { get;}
        Guid OwnerGroupId { get; set;}
    }
}
