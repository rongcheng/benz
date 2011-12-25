using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
using QJVRMS.DataAccess;
using QJVRMS.Business.SecurityControl;
using System.Xml.Serialization;
using QJVRMS.Common;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// ObjectRuleService 的摘要说明
/// </summary>
[WebService(Namespace = "http://qjDataAccess.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ObjectRuleService : System.Web.Services.WebService
{

    public ObjectRuleService()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    /// <summary>
    /// List<IRule> rules, ISecurityObject secObj, System.Collections.ArrayList opers
    /// </summary>
    /// <param name="rules"></param>
    /// <param name="secObj"></param>
    /// <param name="opers"></param>
    /// <returns></returns>
    [WebMethod]
    public bool SetRules(string rulesStr, string secObjStr, string opersStr)
    {
        SerializeObjectFactory sof = new SerializeObjectFactory();

        List<ObjectRule> rules = (List<ObjectRule>)sof.DesializeFromBase64(rulesStr);
        SecurityObject secObj = (SecurityObject)sof.DesializeFromBase64(secObjStr);
        ArrayList opers = (ArrayList)sof.DesializeFromBase64(opersStr);


        string sqlRuleFormat = "insert into AccessControlLIst (ObjectId,ObjectType,OperatorId,OperatorMethod)"
                                + " values ('{0}',{1},'{2}',{3});";
        StringBuilder sqlBuilder = new StringBuilder();
        sqlBuilder.Append("Begin Tran Begin try {0}");


        string sqlRuleDelFormat = "Delete from AccessControlLIst Where ObjectId='{0}' and OperatorId='{1}' and OperatorMethod={2};";
        StringBuilder sqlDelBuilder = new StringBuilder();


        //  if (rules.Count != 0)
        //  {
        foreach (ObjectRule rule in rules)
        {
            string sqlTemp = string.Empty;

            string objId = rule.SecurityObject.ObjectId.ToString();
            string objType = ((int)rule.SecurityObject.ObjectType).ToString();

            string operId = rule.Operator.OperatorId.ToString();
            string method = ((int)rule.Method).ToString();

            if (rule.IsValidate)
            {
                sqlTemp = string.Format(sqlRuleFormat, objId, objType, operId, method);
                sqlBuilder.Append(sqlTemp);


                sqlTemp = string.Format(sqlRuleDelFormat, objId, operId, method);
                sqlDelBuilder.Append(sqlTemp);
            }
            else
            {
                sqlTemp = string.Format(sqlRuleDelFormat, objId, operId, method);
                sqlDelBuilder.Append(sqlTemp);
            }

        }
        // }
        //else
        //{
        //    foreach (IOperator oper in opers)
        //    {
        //        sqlDelBuilder.Append(string.Format(sqlRuleDelFormat, secObj.ObjectId.ToString(), oper.OperatorId.ToString()));
        //    }

        //}


        sqlBuilder.Append(" Commit End Try Begin Catch  IF @@TRANCOUNT > 0 Rollback DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int"
                + " SELECT @ErrMsg = ERROR_MESSAGE(),"
                + " @ErrSeverity = ERROR_SEVERITY()"
                + " RAISERROR(@ErrMsg, @ErrSeverity, 1)"
                + " End Catch");

        string finalSql = sqlBuilder.ToString();

        finalSql = string.Format(finalSql, sqlDelBuilder.ToString());

        try
        {
            
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.Text, finalSql);
            return true;
        }
        catch (Exception ex)
        {
            LogWriter.WriteExceptionLog(ex);
            return false;
        }
    }


    /// <summary>
    /// List<ObjectRule> rules
    /// </summary>
    /// <param name="rules"></param>
    /// <returns></returns>
    [WebMethod]
    public string CheckRules(string rulesStr)
    {
        SerializeObjectFactory sof = new SerializeObjectFactory();
        StringBuilder sqlQuery = new StringBuilder();
        List<ObjectRule> rules = null;
        try
        {
            rules = (List<ObjectRule>)sof.DesializeFromBase64(rulesStr);
            string sql = "CREATE TABLE #RuleList(ruleId uniqueidentifier);";

            sqlQuery.Append(sql);


            foreach (IRule rule in rules)
            {
                sqlQuery.Append(rule.GetSqlQuery());
            }

            sql = "select * from #RuleList";

            sqlQuery.Append(sql);

            using (DataTable dt = SqlHelper.ExecuteDataset(CommonInfo.ConQJVRMS, CommandType.Text, sqlQuery.ToString()).Tables[0])
            {
                foreach (IRule rule in rules)
                {
                    DataRow[] rows = dt.Select("ruleId='" + rule.RuleId.ToString() + "'");
                    if (rows.Length > 0) rule.IsValidate = true;
                }
            }
        }
        catch (Exception ex)
        {
            QJVRMS.Common.LogWriter.WriteExceptionLog(ex);
            return null;
        }


        return sof.SerializeToBase64(rules);

    }

    [WebMethod]
    public bool CheckValidate(Guid objectId,Guid operatorId,int method,int otype)
    {

        SqlParameter[] Parameters = new SqlParameter[5];


        Parameters[0] = new SqlParameter("@SecurityObjId", SqlDbType.UniqueIdentifier);
        Parameters[1] = new SqlParameter("@OperatorId", SqlDbType.UniqueIdentifier);
        Parameters[2] = new SqlParameter("@OperatorMethd", SqlDbType.TinyInt);
        Parameters[3] = new SqlParameter("@SecurityObjType", SqlDbType.TinyInt);
        Parameters[4] = new SqlParameter("@ResultOutput", SqlDbType.Bit);




        Parameters[0].Value = objectId;
        Parameters[1].Value = operatorId;
        Parameters[2].Value =  method;
        Parameters[3].Value = otype;
        Parameters[4].Direction = ParameterDirection.Output;


        try
        {
            SqlHelper.ExecuteNonQuery(CommonInfo.ConQJVRMS, CommandType.StoredProcedure, "Rule_CheckRuleByCondition", Parameters);
            object result = Parameters[4].Value;

            return bool.Parse(result.ToString());
        }
        catch
        {
            return false;
        }


    }
}

