using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using DAMSearchEngine;
using System.Collections;

namespace QJVRMS.Business
{

    /// <summary>
    /// ����������ͨ����������
    /// </summary>
    public class ResourceIndex
    {

        /// <summary>
        /// �����������޸ġ����ʱ�������������
        /// </summary>
        /// <param name="SNs"></param>
        public static void updateIndex(string[] SNs)
        {
            if (!IsUsingIndex) return;
            SearchEngine se = SearchEngineClient.GetSearchEngineObject();
            se.UpdateImages(SNs);
        }

        /// <summary>
        /// ɾ������
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
            sp.highSearchParameter = "VS:2"; //validateStatus=2 Ҳ������˹���

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
             ���ڵ����⣺
             * 1������ͬʱѡ��������Դ����
             * 2��û�и������������      
             * 4�����صĽ�����ظ�����Դ��Ӧ��ȡΨһֵ
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

            /*���˽���е��ظ��������select distinct
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
        /// ���԰�ͼƬ�ı߳����ļ���չ������
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
             ���ڵ����⣺
             * 1������ͬʱѡ��������Դ����
             * 2��û�и������������      
             * 4�����صĽ�����ظ�����Դ��Ӧ��ȡΨһֵ
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

            /*���˽���е��ظ��������select distinct
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
                    if (tmp == "1") // 1 Ϊʹ������ 0 ����������Ϊ��ʹ��
                    {
                        return true;
                    }
                    
                }
                return false; 
            }
        }

        /// <summary>
        /// �ؽ�����������û��ʵ��
        /// </summary>
        internal static void updateIndex()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
