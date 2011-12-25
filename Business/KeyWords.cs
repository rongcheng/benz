using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace QJVRMS.Business
{
    /// <summary>
    /// 对关键字进行增删改查的类
    /// </summary>
    public class KeyWords
    {
        public KeyWords()
        { }

        #region Model
        private int _id;
        private int? _parentid;
        private string _keyword;
        private int? _sort;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? parentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string keyword
        {
            set { _keyword = value; }
            get { return _keyword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        #endregion Model

        public int Add(int parentId, string keyword, int? sort)
        {
            KeywordWS.KeywordService ks = new QJVRMS.Business.KeywordWS.KeywordService();
            return ks.Add(parentId,keyword,sort);
        }

        public DataSet GetKeywordsByParentid(int parentId)
        {
            KeywordWS.KeywordService ks = new QJVRMS.Business.KeywordWS.KeywordService();
            return ks.GetKeywordsByParentId(parentId);
        }

        public void UpdateById(int id, int parentId, string keyword, int sort)
        {
            KeywordWS.KeywordService ks = new QJVRMS.Business.KeywordWS.KeywordService();
            ks.UpdateById(id, parentId, keyword, sort);
        }

        public void Delete(int id)
        {
            KeywordWS.KeywordService ks = new QJVRMS.Business.KeywordWS.KeywordService();
            ks.Delete(id);
        }
    }
}
