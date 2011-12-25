using System;
using System.Collections;
using System.Text;

namespace QJVRMS.Business
{
    /// <summary>
    /// Author: Sunan
    /// Date: 2008.05.07
    /// </summary>
    [Serializable]
    public class RoleCollection : CollectionBase
    {
        public void Add(IRole item)
        {
            base.InnerList.Add(item);
        }

        public IRole this[int index]
        {
            get
            {
                return (IRole)base.InnerList[index];
            }
            set
            {
                base.InnerList[index] = value;
            }
        }

        public IRole this[Guid roleId]
        {
            get
            {
                foreach (IRole role in base.InnerList)
                {
                    if (role.RoleId == roleId)
                    {
                        return role;
                    }
                }

                return null;
            }
        }
    }
}
