using System;
using System.Collections.Generic;
using System.Text;
using QJVRMS.Business.BizData;

namespace QJVRMS.Business
{
    public class News
    {
        string title;
        string content;

        DateTime createDate;

        string loginId;
        string userName;

        char isVal, isTop, ntype;

        Guid userId, newsId;


        public Guid NewsId
        {
            get { return this.newsId; }
            set { this.newsId = value; }
        }



        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public DateTime CreateDate
        {
            get { return this.createDate; }
            set { this.createDate = value; }
        }

        public string LoginId
        {
            get { return this.loginId; }
            set { this.loginId = value; }
        }

        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }

        public Guid UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        public char IsVal
        {
            get { return this.isVal; }
            set { this.isVal = value; }
        }

        public char IsTop
        {
            get { return this.isTop; }
            set { this.isTop = value; }
        }

        public char NType
        {
            get { return this.ntype; }
            set { this.ntype = value; }
        }

        public static bool CreateNews(Guid newsId,
            string title,
            string content,
            DateTime createDate,
            string loginId,
            string userName,
            Guid userId, char isVal, char isTop,char ntype)
        {
            BizService bs = new BizService();
            return bs.CreateNews(newsId, title, content, createDate, loginId, userName, userId, isVal, isTop,ntype);
        }

        public static bool DeleteNews(Guid newsId)
        {
            BizService bs = new BizService();
            return bs.DeleteNews(newsId);
        }


        public static bool UpdateNews(Guid newsId, string title, string content, char isVal, char isTop,char ntype)
        {
            BizService bs = new BizService();
            return bs.UpdateNews(newsId, title, content, isVal, isTop,ntype);
        }

        public static System.Data.DataTable GetNewsList(string title,char ntype)
        {
            BizService bs = new BizService();
            return bs.GetNewsList(title,ntype);
        }


        public static News GetNews(Guid newsId)
        {
            BizService bs = new BizService();
            News news = new News();

            using (System.Data.DataTable dt = bs.GetNews(newsId))
            {
                System.Data.DataRow row = dt.Rows[0];
              

                news.NewsId = new Guid(row["newsId"].ToString());
                news.Title = row["title"].ToString();
                news.Content = row["ncontent"].ToString();
                news.IsTop = char.Parse(row["istop"].ToString());
                news.IsVal = char.Parse(row["isvalidate"].ToString());
                news.CreateDate = DateTime.Parse(row["createDate"].ToString());
                news.NType = char.Parse(row["ntype"].ToString());
                news.UserName = row["userName"].ToString();
            }

            return news;
        }


    }
}
