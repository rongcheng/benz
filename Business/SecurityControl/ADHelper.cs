using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using ActiveDs;


namespace QJVRMS.Business.SecurityControl
{

    /// <summary>
    /// Author: Sunan
    /// Desc: Adhelper
    /// </summary>
    public class ADHelper
    {

        private static string prefix = "LDAP://";



        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainName">quanjing.com</param>
        /// <param name="userDomainName">quanjing\sunan</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static IADsUser AuthenticateUser(string domainName, string userDomainName, string loginId, string password)
        {

            DirectoryEntry userEntry = null;
            IADsUser objUser = null;
            domainName = "DC=" + domainName.Replace(".", ",DC=");

            string ldapPath = prefix + domainName;

            //sAMAccountName
            string userFilter = String.Format("(&(sAMAccountName={0})(objectCategory=person)(objectClass=user))", loginId);

            // 找到用户的LDAP路径
            DirectoryEntry RootDE = new DirectoryEntry(ldapPath, userDomainName, password, AuthenticationTypes.Secure);


            string userPath = string.Empty;
            using (DirectorySearcher objSearch = new DirectorySearcher(RootDE, userFilter))
            {
                SearchResult objRes = objSearch.FindOne();
                userPath = objRes.Path;
                objRes = null;
            }

            try
            {
                userEntry = new DirectoryEntry(userPath, userDomainName, password, AuthenticationTypes.Secure);

                objUser = userEntry.NativeObject as IADsUser;



                return objUser;

            }
            catch (Exception ex)
            {
                Common.LogWriter.WriteExceptionLog(ex);
                return null;
            }
        }

        public static void SearchUser(string domainName,
            string OU,
            string adminId,
            string adminPwd,
            List<string> userIdList,
            List<User> resultUserList)
        {
            domainName = "DC=" + domainName.Replace(".", ",DC=");
            OU = OU.Trim();
            string ldapPath = prefix + OU + "," + domainName;
            string myFilter = "(&(objectCategory=person)(objectClass=user)";

            string filter = string.Empty;
            filter += "(|";
            foreach (string str in userIdList)
            {

                filter += "(sAMAccountName=" + str + ")";
            }

            filter += "))";

            myFilter += filter;

            DirectoryEntry root = new DirectoryEntry(ldapPath, adminId, adminPwd, AuthenticationTypes.Secure);
            DirectorySearcher objSearcher = new DirectorySearcher(root, myFilter);


            List<IADsUser> list = new List<IADsUser>(20);

            using (SearchResultCollection objResCol = objSearcher.FindAll())
            {

                foreach (SearchResult sr in objResCol)
                {
                    DirectoryEntry de = sr.GetDirectoryEntry();

                    IADsUser adUser = de.NativeObject as IADsUser;
                    string userId = de.Properties["sAMAccountName"].Value.ToString().ToLower();



                    if (!adUser.IsAccountLocked
                                          && !adUser.AccountDisabled
                                          && userIdList.IndexOf(userId) > -1)
                    {
                        User user = new User();

                        user.Email = adUser.EmailAddress;
                        user.UserLoginName = userId;
                        user.UserName = adUser.FullName;
                        user.UserId = new Guid(adUser.GUID);

                        try
                        {
                            if (adUser.TelephoneNumber != null)
                                user.Telphone = adUser.TelephoneNumber.ToString();
                        }
                        catch { }

                        resultUserList.Add(user);

                    }


                }
            }
        }
    }
}
