using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QJVRMS.Common.StringPro
{
    public class StringFunc
    {        
        /// <summary>
        /// �ж��ַ����Ƿ�Ϊ��
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
        /// �ж��Ƿ���������ַ�
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
        /// ȥ���ַ������ظ����ֵ�ָ���ַ�
        /// </summary>
        /// <param name="str">�������ַ���</param>
        /// <param name="separator">ָ�����ظ��ַ�</param>
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
        /// ȥ���ַ������ظ����ֵ�ָ���ַ�
        /// </summary>
        /// <param name="str">�������ַ���</param>
        /// <param name="separator">ָ�����ظ��ַ�����</param>
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
        /// ȥ���ַ����а�ָ���ָ���ָ���ظ����ֵ��ַ���������ָ�����Ӵ���������
        /// </summary>
        /// <param name="str">�������ַ���</param>
        /// <param name="separator">ָ���ķָ������</param>
        /// <param name="IgnoreCase">�Ƿ���Դ�Сд</param>
        /// <param name="joinString">ָ�������Ӵ�</param>
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
        /// ���ַ�����ָ���ָ���ָ�ָ�Ϊ���鲢ȥ���������ظ����ֵ��ַ���
        /// </summary>
        /// <param name="str">�������ַ���</param>
        /// <param name="separator">ָ���ķָ������</param>
        /// <param name="IgnoreCase">�Ƿ���Դ�Сд</param>
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
        /// �ж��ַ����б����Ƿ����Ҫ���ҵ��ַ���
        /// </summary>
        /// <param name="ls">�ַ����б�</param>
        /// <param name="Search">Ҫ���ҵ��ַ���</param>
        /// <param name="IgnoreCase">�Ƿ���Դ�Сд</param>
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
