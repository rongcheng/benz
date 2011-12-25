/*
 * 基础类库 字符串处理
 * 作者：姚朔
 * 完成日期： 2006-2-28
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
    /// 枚举Pattern
    /// </summary>
    public enum Pattern
    {
        /// <summary>
        /// 图片编号
        /// </summary>
        Pic_id,
        /// <summary>
        /// 电子邮件
        /// </summary>
        Email,
        /// <summary>
        /// 目录
        /// </summary>
        Folder,

        /// <summary>
        /// 电话号码
        /// </summary>
        Telephone,
        /// <summary>
        /// 手机号码
        /// </summary>
        Mobilephone,
        /// <summary>
        /// 身份证号
        /// </summary>
        IdentifyID,
        /// <summary>
        /// 用户名
        /// </summary>
        Username,
        /// <summary>
        /// 密码
        /// </summary>
        Password,
        /// <summary>
        /// 是否有空格
        /// </summary>
        Noblank,
        /// <summary>
        /// 是否有&
        /// </summary>
        Noand,
        /// <summary>
        /// 禁止SQL注入
        /// </summary>
        Noinject,
        /// <summary>
        /// 是否日期格式
        /// </summary>
        IsDate,
        /// <summary>
        /// 是否数字格式
        /// </summary>
        IsNumeric,
        /// <summary>
        /// 是否ASCII字符
        /// </summary>
        IsASCIIText,
        /// <summary>
        /// 是否中文字符
        /// </summary>
        IsGBText
    }

    public enum MD5StrLen
    {
        int32,       //返回32位字符
        int16A,      //返回中间16位字符
        int16B,      //返回前8位后8位字符
    }

    public class QJDealWithString
    {
        /// <summary>
        /// 方法：验证字符串
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
        /// 方法：验证字符串
        /// 如果为空的话，返回 true；如果不为空，开始进行正常的验证过程
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
