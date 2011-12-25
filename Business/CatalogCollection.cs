using System;
using System.Collections;
using System.Text;


namespace QJVRMS.Business
{
    /// <summary>
    /// Author: Sunan
    /// Date: 2008.05.07
    /// </summary>
    public class CatalogCollection : CollectionBase
    {
        public void Add(ICatalog item)
        {
            base.InnerList.Add(item);
        }

        public ICatalog this[int index]
        {
            get
            {
                return (ICatalog)base.InnerList[index];
            }
            set
            {
                base.InnerList[index] = value;
            }
        }
 
    }
}
