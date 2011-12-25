using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.DataAccess;
using QJVRMS.Common;
using System.Data;
using System.Data.SqlClient;

namespace QJVRMS.Business.SecurityControl
{

    /// <summary>
    /// Author: SuNan
    /// Desc: Object Rule
    /// </summary>
    /// 

    [Serializable]
    public class ObjectRule : IRule
    {
        ISecurityObject securityObject;
        IOperator ioperator;
        OperatorMethod method;

        Guid ruleId;

        bool isValidate = false;


        public IOperator Operator
        {
            get { return ioperator; }
        }

        public OperatorMethod Method
        {
            get { return method; }
        }

        public Guid RuleId
        {
            get { return ruleId; }
            set { ruleId = value; }
        }
        public ISecurityObject SecurityObject
        {
            get { return securityObject; }
            set { this.securityObject = value; }
        }

        public ObjectRule(ISecurityObject iobject,
            IOperator ioperator,
            OperatorMethod method)
        {
            this.securityObject = iobject;
            this.ioperator = ioperator;
            this.method = method;
            this.ruleId = Guid.NewGuid();
        }


        public static bool SetRules(string rulesStr, string secObjStr, string opersStr)
        {
            QJVRMS.Business.ObjectRuleWS.ObjectRuleService ors = new QJVRMS.Business.ObjectRuleWS.ObjectRuleService();
            return ors.SetRules(rulesStr, secObjStr, opersStr);
        }


        /// <summary>
        /// …Ë÷√Rule
        /// </summary>
        /// <param name="rules"></param>
        public static bool SetRules(List<ObjectRule> rules, SecurityObject secObj, System.Collections.ArrayList opers)
        {

            QJVRMS.Common.SerializeObjectFactory sof = new QJVRMS.Common.SerializeObjectFactory();

            string rulesStr = sof.SerializeToBase64(rules);
            string secObjStr = sof.SerializeToBase64(secObj);
            string opersStr = sof.SerializeToBase64(opers);

            return SetRules(rulesStr, secObjStr, opersStr);

            //string sqlRuleFormat = "insert into AccessControlLIst (ObjectId,ObjectType,OperatorId,OperatorMethod)"
            //                        + " values ('{0}',{1},'{2}',{3});";
            //StringBuilder sqlBuilder = new StringBuilder();
            //sqlBuilder.Append("Begin Tran Begin try {0}");


            //string sqlRuleDelFormat = "Delete from AccessControlLIst Where ObjectId='{0}' and OperatorId='{1}';";
            //StringBuilder sqlDelBuilder = new StringBuilder();


            //if (rules.Count != 0)
            //{
            //    foreach (IRule rule in rules)
            //    {
            //        string sqlTemp = string.Empty;

            //        string objId = rule.SecurityObject.ObjectId.ToString();
            //        string objType = ((int)rule.SecurityObject.ObjectType).ToString();

            //        string operId = rule.Operator.OperatorId.ToString();
            //        string method = ((int)rule.Method).ToString();

            //        sqlTemp = string.Format(sqlRuleFormat, objId, objType, operId, method);
            //        sqlBuilder.Append(sqlTemp);


            //        sqlTemp = string.Format(sqlRuleDelFormat, objId, operId);
            //        sqlDelBuilder.Append(sqlTemp);

            //    }
            //}
            //else
            //{
            //    foreach (IOperator oper in opers)
            //    {
            //        sqlDelBuilder.Append(string.Format(sqlRuleDelFormat, secObj.ObjectId.ToString(), oper.OperatorId.ToString()));
            //    }

            //}


            //sqlBuilder.Append(" Commit End Try Begin Catch  IF @@TRANCOUNT > 0 Rollback DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int"
            //        + " SELECT @ErrMsg = ERROR_MESSAGE(),"
            //        + " @ErrSeverity = ERROR_SEVERITY()"
            //        + " RAISERROR(@ErrMsg, @ErrSeverity, 1)"
            //        + " End Catch");

            //string finalSql = sqlBuilder.ToString();

            //finalSql = string.Format(finalSql, sqlDelBuilder.ToString());

            //try
            //{
            //    SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.Text, finalSql);
            //    return true;
            //}
            //catch(Exception ex)
            //{
            //    LogWriter.WriteExceptionLog(ex);
            //    return false;
            //}
        }


        //   public static void CheckRules(

        public static void CheckRules(List<ObjectRule> rules)
        {
            QJVRMS.Common.SerializeObjectFactory sof = new QJVRMS.Common.SerializeObjectFactory();

            string rulesStr = sof.SerializeToBase64(rules);
            QJVRMS.Business.ObjectRuleWS.ObjectRuleService ors = new QJVRMS.Business.ObjectRuleWS.ObjectRuleService();
            string ruleResult = ors.CheckRules(rulesStr);

            object o = sof.DesializeFromBase64(ruleResult);

            List<ObjectRule> result = (List<ObjectRule>)o;
        
            for (int i = 0; i < result.Count; i++)
            {
                rules[i].IsValidate = result[i].IsValidate;
            }

         
            
            //StringBuilder sqlQuery = new StringBuilder();

            //string sql = "CREATE TABLE #RuleList(ruleId uniqueidentifier);";

            //sqlQuery.Append(sql);


            //foreach (IRule rule in rules)
            //{
            //    sqlQuery.Append(rule.GetSqlQuery());
            //}

            //sql = "select * from #RuleList";

            //sqlQuery.Append(sql);

            //using (DataTable dt = SqlHelper.ExecuteDataset(SqlHelper.SqlCon_QJVRMS, CommandType.Text, sqlQuery.ToString()).Tables[0])
            //{
            //    foreach (IRule rule in rules)
            //    {
            //        DataRow[] rows = dt.Select("ruleId='" + rule.RuleId.ToString() + "'");
            //        if (rows.Length > 0) rule.IsValidate = true;
            //    }
            //}

        }

        public string GetSqlQuery()
        {
            string sql = @" insert into #RuleList select '{0}'"
                           + " from AccessControlList where"
                           + " ObjectId='{1}' and OperatorId='{2}' and OperatorMethod={3} and ObjectType={4};"
                           + " insert into #RuleList"
                           + " select '{0}' from AccessControlList "
                           + " where OperatorId in ( select RoleId from Users_InRoles where Userid = '{2}')"
                           + " and ObjectId='{1}'  and OperatorMethod={3} and ObjectType={4};";
            sql = string.Format(sql, this.RuleId.ToString(),
                this.SecurityObject.ObjectId.ToString(),
                this.Operator.OperatorId.ToString(),
                ((int)this.Method).ToString(),
                ((int)this.SecurityObject.ObjectType).ToString());

            return sql;

        }

        public bool IsValidate
        {
            get
            {
                return this.isValidate;
            }
            set
            {
                this.isValidate = value;
            }


        }

        public void CheckValidate()
        {
            QJVRMS.Business.ObjectRuleWS.ObjectRuleService ors = new QJVRMS.Business.ObjectRuleWS.ObjectRuleService();
            this.isValidate = ors.CheckValidate(securityObject.ObjectId, ioperator.OperatorId, (int)method, (int)securityObject.ObjectType);
        //    SqlParameter[] Parameters = new SqlParameter[5];


        //    Parameters[0] = new SqlParameter("@SecurityObjId", SqlDbType.UniqueIdentifier);
        //    Parameters[1] = new SqlParameter("@OperatorId", SqlDbType.UniqueIdentifier);
        //    Parameters[2] = new SqlParameter("@OperatorMethd", SqlDbType.TinyInt);
        //    Parameters[3] = new SqlParameter("@SecurityObjType", SqlDbType.TinyInt);
        //    Parameters[4] = new SqlParameter("@ResultOutput", SqlDbType.Bit);




        //    Parameters[0].Value = securityObject.ObjectId;
        //    Parameters[1].Value = ioperator.OperatorId;
        //    Parameters[2].Value = (int)method;
        //    Parameters[3].Value = (int)securityObject.ObjectType;
        //    Parameters[4].Direction = ParameterDirection.Output;


        //    try
        //    {
        //        SqlHelper.ExecuteNonQuery(SqlHelper.SqlCon_QJVRMS, CommandType.StoredProcedure, "Rule_CheckRuleByCondition", Parameters);
        //        object result = Parameters[4].Value;

        //        this.isValidate = bool.Parse(result.ToString());
        //    }
        //    catch
        //    {
        //        this.isValidate = false;
        //    }

             
         }



    }
}
