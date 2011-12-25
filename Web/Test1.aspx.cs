using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using QJVRMS.Common;
using System.IO;
using System.Drawing.Imaging;


using DAMSearchEngine;
using System.Collections.Generic;
using System.Text;
using QJVRMS.Business;
namespace WebUI
{
    public partial class Test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //throw new Exception("hahaha");
            //string img1 = @"c:\rc111.jpg";

            //string waterMark = @"c:\water.gif";

            //ImageHelper obj = new ImageHelper(img1);
            //obj.SetGrayscale();
            //obj.SetColorFilter(ColorFilterTypes.Green);
            //obj.SetGamma(200, 100, 100);
            //obj.SetBrightness(-80);
            //obj.SetContrast(50);
            //obj.SetInvert();
            //obj.Resize(900);
            //obj.Resize(600, 1000);
            //obj.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
            //obj.Crop(230, 160, 200, 200);
            //obj.SetGrayscale();
            //obj.InsertText("中国人", 10, 10, "", 32, "", "red", "");
            //obj.InsertImage(waterMark, 20, 20);
            //obj.InsertImage(waterMark, WathermarkPosition.BottomLeft);


            
            //obj.Save(@"c:\z2222.jpg");

            //MemoryStream ms=new MemoryStream();
            //obj.GetImage().Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
            //Response.ContentType = "image/Jpeg";
            //Response.Write(obj.GetImage());
            //Response.BinaryWrite(ms.ToArray());

            //Response.ContentType = "image/Jpeg";
            //Response.Clear();
            //Response.BufferOutput = true;
            //obj.GetImage().Save(Response.OutputStream, ImageFormat.Jpeg);
            //Response.End();

            //obj.Dispose();

            //s = "男";
            //Response.Write(s);
            //string s = Request.QueryString["s"];
            //Response.Write(s);
            //string[] sa = GetRelatedKeywords(s);

            //foreach (string x in sa)
            //{
            //    Response.Write(x);
            //    Response.Write("<br/>");
            //}



            //IList<string> color = new List<string>();
            //for (int r = 0; r <=256; r=r+64)
            //{
            //    for (int g = 0; g <=256; g=g+64)
            //    {

            //        for (int b = 0; b <= 256; b = b + 64)
            //        {

            //            color.Add("#" + to2(Convert.ToString(r,16))+ to2(Convert.ToString(g, 16)) + to2(Convert.ToString(b, 16)));
                        
                    
            //        }
            //    }
            //}

            //Response.Write("<div>");
            //foreach (string s in color)
            //{
            //    Response.Write("<font color="+s+">10</font> ");
            //}
            //Response.Write("</div>");



           // Response.Write(MemberShipManager.loginBoss("test2011", "12341234x").ToString());



            Boss objBoss=new Boss();

            string roleName = "总监";
            string groupName = "香港";

            roleName = objBoss.GetVrmsRoleByBossRole(roleName);
            groupName = objBoss.GetVrmsGroupByBossGroup(groupName);

            Response.Write("groupId:"+Group.GetGroupIdByGroupName(groupName));
            Response.Write("<br>");
            Response.Write("roleId:"+Role.GetRoleIdByName(roleName));




            


            string userName, password;
            userName = "administrator";
            password = "adminboss";


            bool isOk = MemberShipManager.loginBoss(userName, password);

            if (isOk)
            {
                string[] arr = MemberShipManager.GetBossGroup(userName, password);
                foreach (string s in arr)
                {
                    Response.Write(s + "<br/>");
                }
            }
            else
            {
                Response.Write("用户名密码不正确");
            }

        }

        private static string to2(string s)
        {
            if (s.Length == 1)
                return "0" + s;
            else
                return s;
        }
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
        protected void Button1_Click(object sender, EventArgs e)
        {

            //Response.AddHeader("Content-Length",

            HttpPostedFile f = this.FileUpload1.PostedFile;
            
            
            
            HttpFileCollection MyFileCollection;
               
            HttpPostedFile MyFile;
               int FileLen;
               System.IO.Stream MyStream;

               MyFileCollection = Request.Files;
               MyFile = MyFileCollection[0];

               FileLen = MyFile.ContentLength;

               FileLen = 200;

            byte[] input = new byte[FileLen];

               // Initialize the stream.
               MyStream = MyFile.InputStream;

               MyStream.Position = MyStream.Length - 100;

               // Read the file into the byte array.
               MyStream.Read(input, 0, FileLen);
           
            
            
            
            
            string MyString=string.Empty;
           // Copy the byte array into a string.
            for (int Loop1 = 0; Loop1 < FileLen; Loop1++)
            {
                MyString = MyString + input[Loop1].ToString();
            }

            using(StreamWriter sw=new StreamWriter(@"c:\aaaaa.txt"))
            {
                sw.Write(MyString);
            }


            MyFile.SaveAs(@"c:\bbbbbbb.bbb");
            //Response.Write(MyString);








        }


  }

}
