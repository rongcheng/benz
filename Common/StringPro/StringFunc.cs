using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QJVRMS.Common.StringPro
{
    public class StringFunc
    {        
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsEmpty(string str)
        {
            if (str != null)
            {
                return str.Trim().Length == 0;
            }
            return true;
        }

        /// <summary>
        /// 判断是否包含中文字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsIncludeChineseChar(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            return Regex.IsMatch(str, @"[\u4E00-\u9FA5\uF900-\uFA2D]+");

            //byte[] sarr;
            //for (int i = 0; i < str.Length; i++)
            //{
            //    sarr = Encoding.GetEncoding("gb2312").GetBytes(str.Substring(i, 1));
            //    if (sarr.Length == 2) return true;
            //}
            //return false;
        }

        /// <summary>
        /// 去掉字符串中重复出现的指定字符
        /// </summary>
        /// <param name="str">待处理字符串</param>
        /// <param name="separator">指定的重复字符</param>
        /// <returns></returns>
        public static string RemoveRedundantSeparator(string str, char separator)
        {
            if (str == null) return null;
            return Regex.Replace(str, separator.ToString() + "{2,}", separator.ToString());

            //string sep = separator.ToString() + separator.ToString();
            //while (str.IndexOf(sep) != 0)
            //{
            //    str.Replace(sep, separator.ToString());
            //}
            //return str;
        }

        /// <summary>
        /// 去掉字符串中重复出现的指定字符
        /// </summary>
        /// <param name="str">待处理字符串</param>
        /// <param name="separator">指定的重复字符数组</param>
        /// <returns></returns>
        public static string RemoveRedundantSeparator(string str, char[] separator)
        {
            if (str == null) return null;
            if (separator.Length == 0) return str;

            foreach (char c in separator)
            {
                str = RemoveRedundantSeparator(str, c);

                //string sep = c.ToString() + c.ToString();
                //while (str.IndexOf(sep) != 0)
                //{
                //    str.Replace(sep, c.ToString());
                //}
            }
            return str;
        }

        /// <summary>
        /// 去掉字符串中按指定分割符分割后重复出现的字符串并按照指定连接串重新连接
        /// </summary>
        /// <param name="str">待处理字符串</param>
        /// <param name="separator">指定的分割符数组</param>
        /// <param name="IgnoreCase">是否忽略大小写</param>
        /// <param name="joinString">指定的连接串</param>
        /// <returns></returns>
        public static string RemoveRedundantString(string str, char[] separator, bool IgnoreCase, string joinString)
        {
            if (str == null) return null;
            if (separator.Length == 0) return str;

            string[] arrStr = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            List<string> lsStr = new List<string> ();
            
            foreach (string s in arrStr)
            {
                if (!StringListExists(ref lsStr, s, IgnoreCase))
                    lsStr.Add(s);
            }

            StringBuilder sbStr = new StringBuilder("");

            foreach (string s in lsStr)
            {
                if (sbStr.Length > 0)
                    sbStr.Append(joinString + s);
                else
                    sbStr.Append(s);
            }
            return sbStr.ToString();
        }

        /// <summary>
        /// 将字符串按指定分割符分割分割为数组并去掉数组中重复出现的字符串
        /// </summary>
        /// <param name="str">待处理字符串</param>
        /// <param name="separator">指定的分割符数组</param>
        /// <param name="IgnoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static string[] SplitAndRemoveRedundantString(string str, char[] separator, bool IgnoreCase)
        {
            if (str == null) return null;
            if (separator.Length == 0) return new string[] { str };

            string[] arrStr = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            List<string> lsStr = new List<string>();

            foreach (string s in arrStr)
            {
                if (!StringListExists(ref lsStr, s, IgnoreCase))
                    lsStr.Add(s);
            }
            return lsStr.ToArray();
        }

        /// <summary>
        /// 判断字符串列表中是否包含要查找的字符串
        /// </summary>
        /// <param name="ls">字符串列表</param>
        /// <param name="Search">要查找的字符串</param>
        /// <param name="IgnoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static bool StringListExists(ref List<string> ls, string Search, bool IgnoreCase)
        {
            foreach (string s in ls)
            {
                if (!IgnoreCase && s == Search)
                    return true;
                else if (s.ToLower() == Search.ToLower())
                    return true;
            }
            return false;
        }
    }
}
