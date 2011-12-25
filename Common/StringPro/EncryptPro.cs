using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections; 

namespace QJVRMS.Common.StringPro
{
    /// <summary>
    /// string proess
    /// </summary>
    public class EncryptPro 
    {
        //private static string[] key = { 
        //                                "3","4","5","6","7","8","9","A","B","C",
        //                                "D","E","F","G","H","I","J","K","M","N",
        //                                "P","Q","R","S","T","U","V","W","X","Y" 
        //    };
        //变换关系
        //1<->7   ,2<->12 ,3<->8  ,4<->23 ,5<->9
        //6<->10  ,11<->21,13<->24,14<->19,15<->30
        //16<->26 ,17<->28,18<->27,20<->22,25<->29
        private static readonly string[] key = { 
                                        "9","e","a","r","b","c","3","5","7","8",
                                        "p","4","s","m","y","u","w","v","g","q",
                                        "d","n","6","f","x","i","k","j","t","h" 
            };
        /// <summary>
        /// 10进制转30进制
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string DecimalToThirty(Int64 num)
        {
            string ret_str = "";
            while (num > 0)
            {
                ret_str = key[num % 30] + ret_str;
                num /= 30;
            }

            return ret_str;
        }
        /// <summary>
        /// 30进制转10进制
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Int64 ThirtyToDecimal(string num_str)
        {
            ArrayList ar = new ArrayList(key);
            Int64 k;
            Int64 num = 0;

            for (int i = 0; i < num_str.Length; i++)
            {
                k = ar.IndexOf(num_str[i].ToString());
                if (k < 0) throw new Exception("有非法数据!");
                k *= (Int64)Math.Pow(30, num_str.Length - i - 1);
                num += k;
            }
            return num;
        }
        /// <summary>
        /// MD5 function, return 32 byte string  
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string MD5Hash(string Str)
        {
            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //md5.ComputeHash((new UnicodeEncoding()).GetBytes(Str));
            //byte[] hash = md5.Hash;

            //StringBuilder sb = new StringBuilder();
            //foreach (byte byt in hash)
            //{
            //    sb.Append(String.Format("{0:X1}", byt));
            //}
            //return sb.ToString();
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Str, "MD5").ToLower(); 
        }

        /// <summary>
        /// 加密字符串
        /// 
        /// 采用对称加密算法
        /// 采用3DES类。
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string Encrypt(string Str)
        {
            string str = Str;
            char[] Base64Code = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '_', '=' };
            byte empty = (byte)0;
            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(str));
            System.Text.StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            int page = messageLen / 3;
            int use = 0;
            if ((use = messageLen % 3) > 0)
            {
                for (int i = 0; i < 3 - use; i++)
                    byteMessage.Add(empty);
                page++;
            }
            outmessage = new System.Text.StringBuilder(page * 4);
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[3];
                instr[0] = (byte)byteMessage[i * 3];
                instr[1] = (byte)byteMessage[i * 3 + 1];
                instr[2] = (byte)byteMessage[i * 3 + 2];
                int[] outstr = new int[4];
                outstr[0] = instr[0] >> 2;

                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                if (!instr[1].Equals(empty))
                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                else
                    outstr[2] = 64;
                if (!instr[2].Equals(empty))
                    outstr[3] = (instr[2] & 0x3f);
                else
                    outstr[3] = 64;
                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
            }
            return outmessage.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string Decrypt(string Str)
        {
            string str = Str;
            //if ((str.Length % 4) != 0)
            //{
            //    throw new ArgumentException("Coding is error");
            //}
            if (!System.Text.RegularExpressions.Regex.IsMatch(str, "^[A-Z0-9+_=]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("Coding is error");
            }
            string Base64Code = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+_=";
            int page = str.Length / 4;
            System.Collections.ArrayList outMessage = new System.Collections.ArrayList(page * 3);
            char[] message = str.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
            return System.Text.Encoding.Default.GetString(outbyte);

        }

        /// <summary>
        /// 封装字符串
        /// 
        /// 加密并数字签名
        /// </summary>
        /// <param name="Str"></param>
        /// <returns>56-184位字符串</returns>
        public static string Package(string Str)
        {
            if (Str.Trim().Length == 0)
                throw new ArgumentNullException();
            string Enstr=Encrypt(Str);
            string mdStr = MD5Hash("YaoShuo&" + Str);
            return Enstr + mdStr;
        }

        /// <summary>
        /// 解开字符串封装
        /// 
        /// 解密并验证数字签名
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string UnPackage(string Str)
        {
            if (Str.Trim().Length<56)
                throw new Exception("String not be Verified");
            string Srcstr = Str.Remove(Str.Length-29);
            string Desstr = Decrypt(Srcstr);
            string mdStr = MD5Hash("YaoShuo&" + Desstr);
            if (mdStr == Str.Remove(0, Str.Length - 32))
                return Desstr;
            else
                throw new Exception("String not be Verified");
        }
    }
}
