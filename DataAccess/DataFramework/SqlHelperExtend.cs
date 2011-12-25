using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace QJVRMS.DataAccess
{
    public class SqlHelperExtend
    {

        private SqlConnection scon;
        private SqlTransaction trans;



        public SqlTransaction BeginTrans(string conStr)
        {
            scon = new SqlConnection(conStr);
            scon.Open();
            trans = scon.BeginTransaction();
            return trans;
        }

        public void CommitTrans()
        {
            this.trans.Commit();
        }

        public void RollbackTrans()
        {
            if (trans != null)
                this.trans.Rollback();
        }

        public void CloseTrans()
        {
            this.scon.Close();
        }


        public static int Update(string table, DataTable dataTable, SqlTransaction trans)
        {
            int i = 0;
            try
            {
                string selectSql = string.Format("select * from {0}", table);
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommandBuilder scb = new SqlCommandBuilder(adapter);

                adapter.SelectCommand = new SqlCommand(selectSql);
                adapter.SelectCommand.Connection = trans.Connection;
                adapter.SelectCommand.Transaction = trans;

                i = adapter.Update(dataTable);
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }

            return i;
        }

        public static int Update(string table, DataTable dataTable, string ConStr)
        {
            string selectSql = string.Format("select * from {0}", table);
            int i;

            SqlDataAdapter adapter = new SqlDataAdapter(selectSql, ConStr);
            SqlCommandBuilder scb = new SqlCommandBuilder(adapter);


            i = adapter.Update(dataTable);

            return i;
        }

        public static int Update(DataSet ds, string table, string dataTable, string ConStr)
        {

            string selectSql = string.Format("select * from {0}", table);
            int i;

            SqlDataAdapter adapter = new SqlDataAdapter(selectSql, ConStr);
            SqlCommandBuilder scb = new SqlCommandBuilder(adapter);


            i = adapter.Update(ds, dataTable);

            return i;
        }

        public static void Update(DataSet ds, string[] tables, string[] dataTable, string ConStr)
        {
            using (SqlConnection con = new SqlConnection(ConStr))
            {
                con.Open();
                SqlDataAdapter ada = null;
                SqlTransaction trans = null;
                try
                {
                    int num1 = dataTable.Length;
                    if (dataTable.Length != tables.Length)
                    {
                        throw new Exception("数据结构错误！");
                    }

                    trans = con.BeginTransaction();
                    for (int num2 = 0; num2 < num1; num2++)
                    {
                        ada = new SqlDataAdapter();
                        SqlCommandBuilder scb = new SqlCommandBuilder(ada);
                        ada.SelectCommand = new SqlCommand(string.Format("select * from {0}", tables[num2]));
                        ada.SelectCommand.Connection = con;
                        ada.SelectCommand.Transaction = trans;
                        ada.Update(ds.Tables[dataTable[num2]]);
                    }

                    trans.Commit();

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        public static void Update(DataSet ds, string[] tables, string[] dataTable, SqlTransaction trans)
        {

            SqlDataAdapter ada = null;

            try
            {
                int num1 = dataTable.Length;
                if (dataTable.Length != tables.Length)
                {
                    throw new Exception("数据结构错误！");
                }


                for (int num2 = 0; num2 < num1; num2++)
                {
                    ada = new SqlDataAdapter();
                    SqlCommandBuilder scb = new SqlCommandBuilder(ada);
                    ada.SelectCommand = new SqlCommand(string.Format("select * from {0}", tables[num2]));
                    ada.SelectCommand.Connection = trans.Connection;
                    ada.SelectCommand.Transaction = trans;
                    ada.Update(ds.Tables[dataTable[num2]]);
                }



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// input: string [] array
        /// return: 逗号分割：'abc','edf','ghg'
        /// </summary>
        /// <param name="strList">字符串数组</param>
        /// <returns></returns>
        public static string GetStringSplit(string[] strList)
        {
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < strList.Length; i++)
            {
                if (i != (strList.Length - 1))
                {
                    strBuilder.Append("'" + strList[i] + "',");
                }
                else
                {
                    strBuilder.Append("'" + strList[i] + "'");
                }
            }

            return strBuilder.ToString();
        }


        /// <summary>
        /// General Paging
        /// </summary>
        /// <param name="con">数据库连接对象</param>
        /// <param name="table">查询的表名称</param>
        /// <param name="columns">选择的列</param>
        /// <param name="sqlWhere">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="orderColumn">排序字段</param>
        /// <param name="pageCount">页面数量 output</param>
        /// <returns></returns>
        public static DataSet Paging(SqlConnection con, string table, string columns, string sqlWhere, int pageSize, int pageIndex, string orderColumn, out int pageCount)
        {
            //string sql = "With TempPageTable As "
            //       + " (SELECT  {5},ROW_NUMBER() OVER (Order By {0})as RowNumber FROM {1} {2})"
            //         +" Select * From TempPageTable Where RowNumber between {3} and {4}"
            //       + " Select Count(*) From {1} {2}";


            int rowStart = 0, rowEnd = 0;

            rowStart = pageSize * pageIndex + 1;
            rowEnd = (pageIndex + 1) * pageSize;
            pageCount = 0;


            SqlParameter[] Parameters = new SqlParameter[6];


            Parameters[0] = new SqlParameter("@tableParm", SqlDbType.VarChar);
            Parameters[1] = new SqlParameter("@sqlWhereParm", SqlDbType.NVarChar);
            Parameters[2] = new SqlParameter("@columnsParm", SqlDbType.VarChar);
            Parameters[3] = new SqlParameter("@keyColumnParm", SqlDbType.VarChar);
            Parameters[4] = new SqlParameter("@rowStartParm", SqlDbType.Int);
            Parameters[5] = new SqlParameter("@rowEndParm", SqlDbType.Int);

            Parameters[0].Value = table;
            Parameters[1].Value = sqlWhere;
            Parameters[2].Value = columns;
            Parameters[3].Value = orderColumn;
            Parameters[4].Value = rowStart;
            Parameters[5].Value = rowEnd;




            //sql = string.Format(sql, orderColumn, table, sqlWhere, rowStart.ToString(), rowEnd.ToString(), columns);

            return SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "QJDAM_GeneralPaging", Parameters);

            // return SqlHelper.ExecuteDataset(con, CommandType.Text, sql);
        }

       

    }
}
