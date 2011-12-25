using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QJVRMS.Business
{
    public class Boss
    {
        
        /// <summary>
        /// 得到某个用户的roleId,groupid
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string[] GetVrmsId(string userName, string password)
        {
            string roleName = "";
            string groupName = "";
            string email = "";

            string[] arr = MemberShipManager.GetBossGroup(userName, password);

            roleName =  GetVrmsRoleByBossRole(arr[1]);
            groupName = GetVrmsGroupByBossGroup(arr[0]);
            email = arr[2];
            

            string[] arrRet = new string[] { Role.GetRoleIdByName(roleName),Group.GetGroupIdByGroupName(groupName),email };

            return arrRet;
        }


        /// <summary>
        /// 根据boss的角色名称得到vrms中的对应的角色名称
        /// </summary>
        /// <param name="bossRoleName"></param>
        /// <returns></returns>
        public string GetVrmsRoleByBossRole(string bossRoleName)
        {

            string _ret = string.Empty;
            string xmlFile = GetXmlFile();
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);

            DataTable dt = ds.Tables["roles"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "bossRoleName='" + bossRoleName.Trim() + "'";
            DataTable dt1 = dv.ToTable();
            if (dt1.Rows.Count > 0)
            {
                _ret = dt1.Rows[0]["vrmsRoleName"].ToString();
            }
            return _ret;        
        }


        /// <summary>
        /// 根据boss的机构名称得到对应的vrms中的机构名称
        /// </summary>
        /// <param name="bossGroup"></param>
        /// <returns></returns>
        public string GetVrmsGroupByBossGroup(string bossGroupName)
        {
            string _ret = string.Empty;
            string xmlFile = GetXmlFile();

            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);

            DataTable dt = ds.Tables["groups"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "bossGroupName='" + bossGroupName.Trim() + "'";
            DataTable dt1 = dv.ToTable();
            if (dt1.Rows.Count > 0)
            {
                _ret = dt1.Rows[0]["vrmsGroupName"].ToString();
            }

            return _ret;        
        }



        /// <summary>
        /// xml文件的保存位置
        /// </summary>
        /// <returns></returns>
        private string GetXmlFile()
        {
           return  System.Web.HttpContext.Current.Server.MapPath("/xml/boss.xml");
        }
    }
}
