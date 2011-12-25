using System;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace QJVRMS.Common
{
    public class Encryption
    {
      

        #region  MD5 
        /// <summary>
        /// 返回MD5字符串
        /// </summary>
        /// <param name="_string"></param>
        /// <returns></returns>
        public static string GetMD5string(string _string)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(_string, "md5");

        }

        public string MD5(string SourceStr, MD5StrLen BitRight)
        {
            switch (BitRight)
            {
                case MD5StrLen.int16A:
                    return FormsAuthentication.HashPasswordForStoringInConfigFile(SourceStr, "MD5").ToLower().Substring(8, 16);

                case MD5StrLen.int32:
                    return FormsAuthentication.HashPasswordForStoringInConfigFile(SourceStr, "MD5").ToLower();

                case MD5StrLen.int16B:
                    return FormsAuthentication.HashPasswordForStoringInConfigFile(SourceStr, "MD5").ToLower().Remove(8, 16);
                default:
                    return null;
            }
        }

        #endregion


        #region DES
        /// <summary>
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
            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.UTF8.GetBytes(str));
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
            return System.Text.Encoding.UTF8.GetString(outbyte);

        }

        #endregion



        #region 
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
            string Enstr = Encrypt(Str);
            string mdStr = GetMD5string("YaoShuo&" + Str);
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
            if (Str.Trim().Length < 56)
                throw new Exception("String not be Verified");
            string Srcstr = Str.Remove(Str.Length - 29);
            string Desstr = Decrypt(Srcstr);
            string mdStr = GetMD5string("YaoShuo&" + Desstr);
            if (mdStr == Str.Remove(0, Str.Length - 32))
                return Desstr;
            else
                throw new Exception("String not be Verified");
        }

        #endregion

    }
}
