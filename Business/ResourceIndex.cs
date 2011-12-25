using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using DAMSearchEngine;
using System.Collections;

namespace QJVRMS.Business
{

    /// <summary>
    /// 更新索引，通过索引检索
    /// </summary>
    public class ResourceIndex
    {

        /// <summary>
        /// 更新索引，修改、添加时都调用这个方法
        /// </summary>
        /// <param name="SNs"></param>
        public static void updateIndex(string[] SNs)
        {
            if (!IsUsingIndex) return;
            SearchEngine se = SearchEngineClient.GetSearchEngineObject();
            se.UpdateImages(SNs);
        }

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="SNs"></param>
        public static void deleteIndex(string[] SNs)
        {
            if (!IsUsingIndex) return;
            SearchEngine se = SearchEngineClient.GetSearchEngineObject();
            se.DeleteImages(SNs);
        }

        //public static string[] GetRelatedKeywords(string keyword)
        //{
        //    ArrayList al = new ArrayList();

        //    string[] correlateKeywords=new string[]{};
        //    SearchEngine se = SearchEngineClient.GetSearchEngineObject();
        //    string[] okKeywords=se.TestKeyword(keyword, correlateKeywords);

        //    foreach (string s in correlateKeywords)
        //    {
        //        al.Add(s);                
        //    }

        //    foreach (string s1 in okKeywords)
        //    {
        //        al.Add(s1);
        //    }

        //    return al.ToArray(typeof(string));
        //}

        public static string[] GetRelatedKeywords(string keyword)
        {
            string[] correlateKeywords = new string[] { };
            SearchEngine se = SearchEngineClient.GetSearchEngineObject();
            string[] okKeywords = se.TestKeyword(keyword, ref correlateKeywords);

            if (correlateKeywords.Length > 0)
                return correlateKeywords;
            else
                return okKeywords;
        }

        public static DataSet Search(int pageSize, int pageIndex, out int pageCount, string keyword, DateTime beg, DateTime end)
        {
            SearchEngine.SearchParameter sp = new SearchEngine.SearchParameter();
            SearchEngine se = SearchEngineClient.GetSearchEngineObject();
            sp.keywords = keyword ;
            sp.pageSize = pageSize;
            sp.pageIndex = pageIndex;
            sp.highSearchParameter = "VS:2"; //validateStatus=2 也就是审核过的

            if (sp.recordCount % sp.pageSize == 0)
            {
                pageCount = sp.recordCount / pageSize;
            }
            else
            {
                pageCount = sp.recordCount / pageSize + 1;
            }


            DataTable dt = se.Search(ref sp);

            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add(new DataColumn("recordCount", typeof(System.Int32)));
            DataRow dr = dt1.NewRow();
            dr["recordCount"] = sp.recordCount;
            dt1.Rows.Add(dr);

            ds.Tables.Add(dt1);
            ds.Tables.Add(dt);

            return ds;
        }


        public static DataSet Search(string keyword, string beginDate, string endDate, string Userid, int PageSize, int PageNum, ref int rowCount, string resourceType)
        {
            /*
             存在的问题：
             * 1、不能同时选择两种资源类型
             * 2、没有跟机构相关联上      
             * 4、返回的结果有重复的资源，应该取唯一值
             */
            
                        
            SearchEngine.SearchParameter sp = new SearchEngine.SearchParameter();
            SearchEngine se = SearchEngineClient.GetSearchEngineObject();
            sp.keywords = keyword;
            if( (!string.IsNullOrEmpty(beginDate)) && (!string.IsNullOrEmpty(endDate)))
            {
                sp.startDate = Convert.ToDateTime(beginDate);
                sp.endDate = Convert.ToDateTime(endDate);
            }

            sp.ResourceType = resourceType.Replace(',',' '); 
            sp.pageSize = PageSize;
            sp.pageIndex = PageNum;
            sp.highSearchParameter = "VS:2";
           

            DataTable dt = se.Search(ref sp);

            /*过滤结果中的重复项，类似于select distinct
            int i=0;
            int colCount=dt.Columns.Count;
            string[] cols=new string[colCount];
            foreach(DataColumn column in dt.Columns)
            {
                cols[i] = column.ColumnName;
                i++;
            }
            DataTable dt2 = dt.DefaultView.ToTable(true, cols);
            */

            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable("recordCount"); ;
            dt1.Columns.Add(new DataColumn("recordCount", typeof(System.Int32)));
            DataRow dr = dt1.NewRow();
            dr["recordCount"] = sp.recordCount;
            dt1.Rows.Add(dr);

            ds.Tables.Add(dt1);
            ds.Tables.Add(dt); ;

            return ds;
        }


        /// <summary>
        /// 可以按图片的边长，文件扩展名搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="Userid"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageNum"></param>
        /// <param name="rowCount"></param>
        /// <param name="resourceType"></param>
        /// <param name="fileExt"></param>
        /// <param name="fileWH"></param>
        /// <returns></returns>
        public static DataSet Search(string keyword, string beginDate, string endDate, string Userid, int PageSize, int PageNum, ref int rowCount, string resourceType,string fileExt,string fileWH)
        {
            /*
             存在的问题：
             * 1、不能同时选择两种资源类型
             * 2、没有跟机构相关联上      
             * 4、返回的结果有重复的资源，应该取唯一值
             */


            SearchEngine.SearchParameter sp = new SearchEngine.SearchParameter();
            SearchEngine se = SearchEngineClient.GetSearchEngineObject();
            sp.keywords = keyword;
            if ((!string.IsNullOrEmpty(beginDate)) && (!string.IsNullOrEmpty(endDate)))
            {
                sp.startDate = Convert.ToDateTime(beginDate);
                sp.endDate = Convert.ToDateTime(endDate);
            }

            sp.ResourceType = resourceType.Replace(',', ' ');
            sp.pageSize = PageSize;
            sp.pageIndex = PageNum;
            sp.highSearchParameter = "VS:2";

            if (!string.IsNullOrEmpty(fileExt))
            {
                sp.FileType = fileExt.Replace(","," ");            
            }

            if (!string.IsNullOrEmpty(fileWH))
            {
                //sp.HIRes = fileWH;
                sp.Size = fileWH;
            }
            //sp.Size = "200-800";
            //sp.Size = "1000-2000";
            //sp.Size = "200-1000:683-1000";
            //sp.FileType = "";


            DataTable dt = se.Search(ref sp);

            /*过滤结果中的重复项，类似于select distinct
            int i=0;
            int colCount=dt.Columns.Count;
            string[] cols=new string[colCount];
            foreach(DataColumn column in dt.Columns)
            {
                cols[i] = column.ColumnName;
                i++;
            }
            DataTable dt2 = dt.DefaultView.ToTable(true, cols);
            */

            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable("recordCount"); ;
            dt1.Columns.Add(new DataColumn("recordCount", typeof(System.Int32)));
            DataRow dr = dt1.NewRow();
            dr["recordCount"] = sp.recordCount;
            dt1.Rows.Add(dr);

            ds.Tables.Add(dt1);
            ds.Tables.Add(dt); ;

            return ds;
        }



        public static  bool IsUsingIndex
        {
            get 
            {
                string tmp=System.Configuration.ConfigurationManager.AppSettings["isUsingIndex"];
                if (!string.IsNullOrEmpty(tmp))
                {
                    if (tmp == "1") // 1 为使用索引 0 或者其他的为不使用
                    {
                        return true;
                    }
                    
                }
                return false; 
            }
        }

        /// <summary>
        /// 重建索引，现在没有实现
        /// </summary>
        internal static void updateIndex()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
