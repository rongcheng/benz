using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QJVRMS.Business
{
    public interface IImageStorage
    {
        Guid  ItemId { set;get;}
        Guid userId { set;get;}
        string ItemSerialNum { set;get;}
        string FileName { set;get;}
        string FolderName { set;get;}
        string Caption { set;get;}
        string Address { set;get;}
        string Character { set;get;}
        DateTime StartDate { set;get;}
        DateTime EndDate { set;get;}
        DateTime uploadDate { set;get;}
        DateTime shotDate { set;get;}
        string Keyword { set;get;}
        string Description { set;get;}
        string ImageType { set;get;}
        string Hvsp { set;get; }
        Guid GroupId { set; get;}//add by dtf 08-06-02
      //  void Delete();//É¾³ýÍ¼Æ¬
     //   void Store();//ÐÞ¸ÄÍ¼Æ¬
    }
}
