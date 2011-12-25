using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Business
{
    public interface IMemberShip
    {
        bool ResetPassword(Guid userId, string newPassword);
        bool ChangePassword(Guid userId, string oldPassword, string newPassword);
        IUser CreateUser(string password, string loginName, string userName, Guid groupId, string email, string tel, bool islocked, string isdownload, bool isIPValidate);
        bool ModifyUserInfo(Guid userId,Guid groupId, string userName, string email, string tel, bool islocked, string isdownload,bool isIPValidate);
        User GetUser(string userId);
        UserCollection GetAllUser(int pageIndex, int pageSize, string sqlWhere, out int records);
        bool LockUser(string userId,bool isLocked);
       
    }
}
