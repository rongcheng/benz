using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace WebUI.UIBiz
{
    [Serializable]
    public class WebUser : IWebUser
    {
        Guid userId, groupId;

        string userLoginName, userName,groupName;

        string isDownLoad;

        public WebUser(Guid userId, Guid groupId, string loginName, string userName, string groupName, string isDownLoad)
        {
            this.userId = userId;
            this.groupId = groupId;
            this.userLoginName = loginName;
            this.userName = userName;
            this.groupName = groupName;
            this.isDownLoad = isDownLoad;
        }
        
        public Guid UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
           
        }

        public Guid UserGroupId
        {
            get
            {
                return this.groupId;
            }
            set
            {
                this.groupId = value;
            }
          
        }


        public string UserLoginName
        {
            get
            {
                return this.userLoginName;
            }
            set
            {
                this.userLoginName = value;
            }
        
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
           
        }

        public string GroupName
        {
            get { return this.groupName; }
            set { this.groupName = value; }
        }

        public string IsDownLoad
        {
            get { return this.isDownLoad; }
            set { this.isDownLoad = value; }
        }

        #region IOperator ≥…‘±

        public Guid OperatorId
        {
            get { return this.userId; }
        }

        public System.Collections.Generic.List<QJVRMS.Business.SecurityControl.IOperator> CorrelativeOperator
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
}
