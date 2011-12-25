using System;
using System.Collections;
using System.Text;

namespace QJVRMS.Business
{
    /// <summary>
    /// Author: Sunan
    /// Date: 2008.05.07
    /// </summary>
    /// 
    [Serializable]
    public class UserCollection : CollectionBase
    {
        public void Add(User item)
        {
            base.InnerList.Add(item);
        }


        public User this[int index]
        {
            get
            {
                return (User)base.InnerList[index];
            }
        }

        public User this[Guid userId]
        {
            get
            {
                foreach (object obj in base.InnerList)
                {
                    if (((User)obj).UserId == userId)
                    {
                        return (obj as User);
                    }
                }
                return null;
            }
        }

        public UserCollection this[bool islocked]
        {
            get
            {
                UserCollection temp = new UserCollection();

                foreach (object obj in base.InnerList)
                {
                    User user = obj as User;
                    if ( user.IsLocked == islocked)
                    {
                        temp.Add(user);
                    }
                }

                return temp;
            }
        }
 
    }
}
