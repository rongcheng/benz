using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business.SecurityControl
{
    [Serializable]
    public class SecurityObject : ISecurityObject
    {

        private Guid objectId;
        private SecurityObjectType objectType;


        public SecurityObject(Guid objectId, SecurityObjectType objType)
        {
            this.objectId = objectId;
            this.objectType = objType;
        }

        #region ISecurityObject ≥…‘±

        public Guid ObjectId
        {
            get
            {
                return this.objectId;
            }
            set
            {
                this.objectId = value;
            }
        }

        public SecurityObjectType ObjectType
        {
            get
            {
                return objectType;
            }
            set
            {
                objectType = value;
            }
        }

        #endregion
    }
}
