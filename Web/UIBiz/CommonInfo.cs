using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Web.UI;
using System.Collections.Generic;
using QJVRMS.Business;
using System.IO;
using QJVRMS.Business.ResourceType;

namespace WebUI.UIBiz
{
    public class CommonInfo
    {

        #region MethodInfo ²Ù×÷·½·¨ÐÅÏ¢

        private static XmlDocument xmlMehtodMap = GetMethodMap();
        private static XmlDocument GetMethodMap()
        {
            XmlDocument xd = new XmlDocument();
            xd.Load(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OperatorMethods.xml"));
            return xd;
        }



        public static string GetMehtodName(QJVRMS.Business.SecurityControl.OperatorMethod method)
        {
            Int16 methodIndex = (Int16)method;

            XmlNode node = xmlMehtodMap.DocumentElement.SelectSingleNode("Method[@id='" + methodIndex.ToString() + "']");

            return node.InnerText;
        }


        public static Dictionary<int, string> GetMethodDict()
        {
            XmlNodeList list = xmlMehtodMap.DocumentElement.SelectNodes("Method");
            Dictionary<int, string> dict = new Dictionary<int, string>(list.Count);

            foreach (XmlElement node in list)
            {
                dict.Add(int.Parse(node.Attributes["id"].Value), node.InnerText);
            }

            return dict;
        }

        #endregion


        #region AppConfig

        //private static string userStateLoc = ConfigurationManager.AppSettings["SessionLocation"];

        ///// <summary>
        ///// ÓÃ»§µÄ×´Ì¬Î»ÖÃ
        ///// 0:Session 1:Cookie
        ///// </summary>
        //public static string UserStateLoc
        //{
        //    get { return userStateLoc; }
        //}

        public static string AttachFolder
        {
            get { return "Attachments"; }
        }

        static string fileDownPath = ConfigurationManager.AppSettings["FilePath"];

        public static string FileDownPath
        {
            get { return fileDownPath; }
        }


        static string[] viewByDept = ConfigurationManager.AppSettings["ViewByDept"].Split(',');

        public static string[] ViewByDept
        {
            get { return viewByDept; }
        }

        private static string[] repeatColor = ConfigurationManager.AppSettings["repeatColor"].Split(',');

        public static string[] RepeatColor
        {
            get { return repeatColor; }
        }
 
        private static double cookieTimeout = double.Parse(ConfigurationManager.AppSettings["cookieTimeout"]);


        public static double CookieTimeout
        {
            get { return cookieTimeout; }
        }


        private static Guid superAdminId = new Guid(ConfigurationManager.AppSettings["superAdminId"]);

        /// <summary>
        ///  ³¬¼¶¹ÜÀíÔ±ID
        /// </summary>
        public static Guid SuperAdminId
        {
            get { return superAdminId; }
        }

        /// <summary>
        /// ÊÓÆµ´æ´¢Â·¾¶
        /// </summary>
        private static string videoRootPath = ConfigurationManager.AppSettings["videoRootPath"];
        public static string VideoRootPath { get { return videoRootPath; } }

        /// <summary>
        /// Ô¤ÀÀÍ¼Æ¬ºÍÔ¤ÀÀÊÓÆµµÄ´æ´¢Â·¾¶
        /// </summary>
        private static string videoPreviewPath = ConfigurationManager.AppSettings["videoPreviewPath"];
        public static string VideoPreviewPath { get { return videoPreviewPath; } }




        /// <summary>
        /// ×ÊÔ´´æ´¢Â·¾¶
        /// </summary>
        private static string resourceRootPath = ConfigurationManager.AppSettings["resourceRootPath"];
        public static string ResourceRootPath { get { return resourceRootPath; } }




        private static string imageRootPath = ConfigurationManager.AppSettings["imageRootPath"];

        /// <summary>
        /// Ô­Í¼Æ¬¸ùÂ·¾¶
        /// </summary>
        public static string ImageRootPath
        {
            get { return imageRootPath; }
        }


        private static string slimageRootPath170 = ConfigurationManager.AppSettings["slimageRootPath170"];

        /// <summary>
        /// Ñ¹ËõÍ¼Æ¬´æ´¢¸ùÂ·¾¶
        /// </summary>
        public static string SlImageRootPath170
        {
            get { return slimageRootPath170; }
        }
        private static string slimageRootPath170Read = ConfigurationManager.AppSettings["slimageRootPath170Read"];

        /// <summary>
        /// Ñ¹ËõÍ¼Æ¬´æ´¢¸ùÂ·¾¶
        /// </summary>
        public static string SlImageRootPath170Read
        {
            get { return slimageRootPath170Read; }
        }

        private static string slimageRootPath400 = ConfigurationManager.AppSettings["slimageRootPath400"];

        /// <summary>
        /// Ñ¹ËõÍ¼Æ¬´æ´¢¸ùÂ·¾¶
        /// </summary>
        public static string SlImageRootPath400
        {
            get { return slimageRootPath400; }
        }
        private static string slimageRootPath400Read = ConfigurationManager.AppSettings["slimageRootPath400Read"];

        /// <summary>
        /// Ñ¹ËõÍ¼Æ¬´æ´¢¸ùÂ·¾¶
        /// </summary>
        public static string SlImageRootPath400Read
        {
            get { return slimageRootPath400Read; }
        }
        private static string slimageRootPath1m = ConfigurationManager.AppSettings["slimageRootPath1m"];

        /// <summary>
        /// Ñ¹ËõÍ¼Æ¬´æ´¢¸ùÂ·¾¶
        /// </summary>
        public static string SlImageRootPath1m
        {
            get { return slimageRootPath1m; }
        }

        private static string slimageRootPath2m = ConfigurationManager.AppSettings["slimageRootPath2m"];

        /// <summary>
        /// Ñ¹ËõÍ¼Æ¬´æ´¢¸ùÂ·¾¶
        /// </summary>
        public static string SlImageRootPath2m
        {
            get { return slimageRootPath2m; }
        }

        private static string slimageRootPath10m = ConfigurationManager.AppSettings["slimageRootPath10m"];

        /// <summary>
        /// Ñ¹ËõÍ¼Æ¬´æ´¢¸ùÂ·¾¶
        /// </summary>
        public static string SlImageRootPath10m
        {
            get { return slimageRootPath10m; }
        }

        private static string slimageRootPath20m = ConfigurationManager.AppSettings["slimageRootPath20m"];

       

        /// <summary>
        /// Ñ¹ËõÍ¼Æ¬´æ´¢¸ùÂ·¾¶
        /// </summary>
        public static string SlImageRootPath20m
        {
            get { return slimageRootPath20m; }
        }
        private static string slimageRootPath50m = ConfigurationManager.AppSettings["slimageRootPath50m"];

        /// <summary>
        /// Ñ¹ËõÍ¼Æ¬´æ´¢¸ùÂ·¾¶
        /// </summary>
        public static string SlImageRootPath50m
        {
            get { return slimageRootPath50m; }
        }


        private static string webPort = ConfigurationManager.AppSettings["webPort"];

        /// <summary>
        /// web ¶Ë¿ÚºÅ 
        /// </summary>
        public static string WebPort
        {
            get { return webPort; }
        }


        private static string domainName = ConfigurationManager.AppSettings["DomainName"];
        public static string DomainName
        {
            get { return domainName; }
        }


        private static string domainNamePrefix = ConfigurationManager.AppSettings["DomainNamePrefix"];
        public static string DomainNamePrefix
        {
            get
            {
                return domainNamePrefix;
            }
        }


        private static string authByAD = ConfigurationManager.AppSettings["AuthByAD"];
        /// <summary>
        /// True:ADÑéÖ¤ Fals
        /// </summary>
        public static bool AuthByAD
        {
            get
            {
                return authByAD == "1";
            }
        }

        //  private static int pageCount = int.Parse(ConfigurationManager.AppSettings["QJpageCount"]);
        public static int PageCount
        {
            get
            {
                return 20;

            }
        }
        #endregion

        public static string GetImageUrl(int imageSize, string folder, string serialNum, string fileType)
        {
            string url = string.Empty;

            switch (fileType.ToUpper())
            {
                case ".JPG":
                case ".JPEG":
                case ".GIF":
                case ".PNG":
                case ".BMP":
                case ".TIFF":
                case ".PCX":
                case ".TGA":
                case ".EXIF":
                case ".CR2":
                case ".NEF":
                case ".PSD":
                case ".FPX":
                    if (fileType.ToUpper() == ".CR2" || fileType.ToUpper() == ".NEF" || fileType.ToUpper() == ".PSD")
                        fileType = ".jpg";
                    if (imageSize == 170)
                        //url += WebUI.UIBiz.CommonInfo.SlImageRootPath170Read + folder + @"/" + serialNum + fileType;
                        //yangguang
                        //url+= new ImageType().PreviewPath_170_Read+ folder + @"/" + serialNum + fileType;
                        url += new ImageType().GetPreviewPathRead(folder, serialNum + fileType, "170");
                    else if (imageSize == 400)
                        //url += WebUI.UIBiz.CommonInfo.SlImageRootPath400Read + folder + @"/" + serialNum + fileType;
                        //yangguang
                        //url += new ImageType().PreviewPath_400_Read + folder + @"/" + serialNum + fileType;
                        url += new ImageType().GetPreviewPathRead(folder, serialNum + fileType, "400");
                    else
                        url += folder + @"/" + serialNum + fileType;

                    break;
                case ".TXT":
                    url += @"/images/txt.jpg"; break;
                case ".DOC":
                case ".DOCX":
                    url += @"/images/doc.jpg"; break;
                case "WMF":
                    url += @"/images/wmf.jpg"; break;
                case ".XLS":
                case ".XLSX":
                    url += @"/images/xls.jpg"; break;
                //case ".PSD":
                //    url += @"/images/psd.jpg"; break;
                case ".PPT":
                    url += @"/images/ppt.jpg"; break;
                case ".PPS":
                    url += @"/images/pps.jpg"; break;
                case ".PDF":
                    url += @"/images/pdf.jpg"; break;
                case ".MDB":
                case ".ACCDB":
                    url += @"/images/mdb.jpg"; break;
                case ".RAR":
                    url += @"/images/rar.jpg"; break;
                case ".ZIP":
                    url += @"/images/zip.jpg"; break;
                case ".HTM":
                case ".HTML":
                    url += @"/images/htm.jpg"; break;
                default:
                    url += @"/images/other.jpg"; break;
            }

            return url;
        }


        public static string GetResourceImageUrl(int imageSize, string serverFileName,string serverFolderName)
        {
            string url = string.Empty;
            string fileType = string.Empty;
            fileType = Path.GetExtension(serverFileName);
            switch (fileType.ToUpper())
            {
                case ".JPG":
                case ".JPEG":
                case ".GIF":
                case ".PNG":
                case ".BMP":
                case ".TIFF":
                case ".PCX":
                case ".TGA":
                case ".EXIF":
                case ".FPX":

                    if (imageSize == 170)
                        url += WebUI.UIBiz.CommonInfo.SlImageRootPath170Read + serverFolderName + @"/" + serverFileName;
                    else if (imageSize == 400)
                        url += WebUI.UIBiz.CommonInfo.SlImageRootPath400Read + serverFolderName + @"/" + serverFileName;
                    else
                        url += serverFolderName + @"/" + serverFileName;

                    break;
                case ".TXT":
                    url += @"/images/txt.jpg"; break;
                case ".DOC":
                case ".DOCX":
                    url += @"/images/doc.jpg"; break;
                case "WMF":
                    url += @"/images/wmf.jpg"; break;
                case ".XLS":
                case ".XLSX":
                    url += @"/images/xls.jpg"; break;
                case ".PSD":
                    url += @"/images/psd.jpg"; break;
                case ".PPT":
                    url += @"/images/ppt.jpg"; break;
                case ".PPS":
                    url += @"/images/pps.jpg"; break;
                case ".PDF":
                    url += @"/images/pdf.jpg"; break;
                case ".MDB":
                case ".ACCDB":
                    url += @"/images/mdb.jpg"; break;
                case ".RAR":
                    url += @"/images/rar.jpg"; break;
                case ".ZIP":
                    url += @"/images/zip.jpg"; break;
                case ".HTM":
                case ".HTML":
                    url += @"/images/htm.jpg"; break;
                default:
                    url += @"/images/other.jpg"; break;
            }

            return url;
        }





        public static string GetImageUrl(int imageSize, IImageStorage oIImageStorage)
        {

            return GetImageUrl(imageSize, oIImageStorage.FolderName, oIImageStorage.ItemSerialNum, oIImageStorage.ImageType);

        }



        private static string _website_title =  ConfigurationManager.AppSettings["website_title"];
        public static string WebSite_Title
        {
            get 
            {
                if (string.IsNullOrEmpty(_website_title))
                {
                    return "È«¾°×ÊÔ´¹ÜÀíÆ½Ì¨";
                }
                else
                {
                    return _website_title;
                }
            }            
        }



        private static string _orderNewFunctionID = ConfigurationManager.AppSettings["OrderNewFunctionId"];
        public static string OrderNewFunctionID
        {
            get
            {
                if (string.IsNullOrEmpty(_orderNewFunctionID))
                {
                    return "257be0db-3c51-458d-913c-b568bef7b154";
                }
                else
                {
                    return _orderNewFunctionID;
                }
            }
        }

        
    }
}
