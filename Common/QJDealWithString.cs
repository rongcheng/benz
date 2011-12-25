/*
 * ������� �ַ�������
 * ���ߣ�Ҧ˷
 * ������ڣ� 2006-2-28
 *
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security ;

namespace QJVRMS.Common
{
    
    /// <summary>
    /// ö��Pattern
    /// </summary>
    public enum Pattern
    {
        /// <summary>
        /// ͼƬ���
        /// </summary>
        Pic_id,
        /// <summary>
        /// �����ʼ�
        /// </summary>
        Email,
        /// <summary>
        /// Ŀ¼
        /// </summary>
        Folder,

        /// <summary>
        /// �绰����
        /// </summary>
        Telephone,
        /// <summary>
        /// �ֻ�����
        /// </summary>
        Mobilephone,
        /// <summary>
        /// ���֤��
        /// </summary>
        IdentifyID,
        /// <summary>
        /// �û���
        /// </summary>
        Username,
        /// <summary>
        /// ����
        /// </summary>
        Password,
        /// <summary>
        /// �Ƿ��пո�
        /// </summary>
        Noblank,
        /// <summary>
        /// �Ƿ���&
        /// </summary>
        Noand,
        /// <summary>
        /// ��ֹSQLע��
        /// </summary>
        Noinject,
        /// <summary>
        /// �Ƿ����ڸ�ʽ
        /// </summary>
        IsDate,
        /// <summary>
        /// �Ƿ����ָ�ʽ
        /// </summary>
        IsNumeric,
        /// <summary>
        /// �Ƿ�ASCII�ַ�
        /// </summary>
        IsASCIIText,
        /// <summary>
        /// �Ƿ������ַ�
        /// </summary>
        IsGBText
    }

    public enum MD5StrLen
    {
        int32,       //����32λ�ַ�
        int16A,      //�����м�16λ�ַ�
        int16B,      //����ǰ8λ��8λ�ַ�
    }

    public class QJDealWithString
    {
        /// <summary>
        /// ��������֤�ַ���
        /// </summary>
        /// <param name="InputStr"></param>
        /// <param name="kind"></param>
        /// <returns></returns>

        public bool InputValidate(string InputStr,Pattern kind)
        {
            if ((InputStr.Trim()=="") || (InputStr==null))
                return false;
            else
            {
                Regex RegexPattern;
                switch (kind)
                {
                    case Pattern.Pic_id:
                        RegexPattern = new Regex(@"([^'&%^!#*|?*+\t\n\r\\.]{3,20})$");
                        break;
                    case Pattern.Email:
                        RegexPattern = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
                        break;
                    case Pattern.Folder:
                        RegexPattern = new Regex(@"[\w]{2,15}$");
                        break;
                    case Pattern.Telephone :
                        RegexPattern = new Regex(@"[\d-]{6,30}$");
                        break;
                    case Pattern.Mobilephone :
                        RegexPattern = new Regex(@"[\d]{8,11}$");
                        break;
                    case Pattern.IdentifyID:
                        RegexPattern = new Regex(@"[0-9]{17}([0-9]|[xXyY]){1}$|[\d]{15}$");
                        //RegexPattern = new Regex(@"[\d]{15,18}$");
                        break;
                    case Pattern.Username:
                        RegexPattern = new Regex(@"[^'&%\^\?\t\n\r\\\*,\+]{2,20}$");
                        break;
                    case Pattern.Password:
                        RegexPattern = new Regex(@"[\w]{6,20}$");
                        break;
                    case Pattern.Noblank:
                        RegexPattern = new Regex(@"([^ ]+$");
                        break;
                    case Pattern.Noand :
                        RegexPattern = new Regex(@"[^&\+]+$");
                        break;
                    case Pattern.Noinject:
                        RegexPattern = new Regex(@"[^'&%\^\?\t\n\r\\\*\+]+$");
                        break;
                    case Pattern.IsDate:
                        RegexPattern = new Regex(@"^([1-2]\d{3})[-](0?[1-9]|10|11|12)[\-]([1-2]?[0-9]|0[1-9]|30|31)$");
                        //RegexPattern = new Regex(@"^([1-2]\d{3})[.](0?[1-9]|10|11|12)[\.]([1-2]?[0-9]|0[1-9]|30|31)$");
                        break;
                    case Pattern.IsNumeric:
                        RegexPattern = new Regex(@"^[0-9.]+$");
                        break;
                    case Pattern.IsASCIIText:
                        RegexPattern = new Regex(@"^[\w]+$");
                        break;
                    case Pattern.IsGBText :
                        RegexPattern = new Regex(@"[^x00-xff']+$");
                        break;
                    default:
                        return false;
                }
                return RegexPattern.IsMatch(InputStr);
            }

        }

        /// <summary>
        /// ��������֤�ַ���
        /// ���Ϊ�յĻ������� true�������Ϊ�գ���ʼ������������֤����
        /// </summary>
        /// <param name="InputStr"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public bool InputValidate_NotRequired(string InputStr, Pattern kind)
        {
            if ((InputStr.Trim() == "") || (InputStr == null))
                return true;
            else
            {
                Regex RegexPattern;
                switch (kind)
                {
                    case Pattern.Pic_id:
                        RegexPattern = new Regex(@"([^'&%^!#*|?*+\t\n\r\\.]{3,20})$");
                        break;
                    case Pattern.Email:
                        RegexPattern = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
                        break;
                    case Pattern.Folder:
                        RegexPattern = new Regex(@"[\w]{2,15}$");
                        break;
                    case Pattern.Telephone:
                        RegexPattern = new Regex(@"[\d-]{6,30}$");
                        break;
                    case Pattern.Mobilephone:
                        RegexPattern = new Regex(@"[\d]{8,11}$");
                        break;
                    case Pattern.IdentifyID:
                        RegexPattern = new Regex(@"[\d]{15,18}$");
                        break;
                    case Pattern.Username:
                        RegexPattern = new Regex(@"[^'&%\^\?\t\n\r\\\*,\+]{2,20}$");
                        break;
                    case Pattern.Password:
                        RegexPattern = new Regex(@"[\w]{6,20}$");
                        break;
                    case Pattern.Noblank:
                        RegexPattern = new Regex(@"([^ ]+$");
                        break;
                    case Pattern.Noand:
                        RegexPattern = new Regex(@"[^&\+]+$");
                        break;
                    case Pattern.Noinject:
                        RegexPattern = new Regex(@"[^'&%\^\?\t\n\r\\\*\+]+$");
                        break;
                    case Pattern.IsDate:
                        RegexPattern = new Regex(@"^([1-2]\d{3})[-](0?[1-9]|10|11|12)[\-]([1-2]?[0-9]|0[1-9]|30|31)$");
                        break;
                    case Pattern.IsNumeric:
                        RegexPattern = new Regex(@"^[0-9.]+$");
                        break;
                    case Pattern.IsASCIIText:
                        RegexPattern = new Regex(@"^[\w]+$");
                        break;
                    case Pattern.IsGBText:
                        RegexPattern = new Regex(@"[^x00-xff']+$");
                        break;
                    default:
                        return false;
                }
                return RegexPattern.IsMatch(InputStr);
            }
        }
        //
    }
    //
}
