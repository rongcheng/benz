using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace QJVRMS.Common.StringPro
{
    public class StrValidate
    {
        /// <summary>
        /// 枚举
        /// </summary>
        public enum Pattern
        {
            Pic_id,     ///图片编号
            Email,      ///电子邮件
            Folder,     ///目录
            Telephone,  ///电话号码
            Mobilephone,///手机号码
            IdentifyID, ///身份证号
            Username,   ///用户名
            Password,   ///密码
            Noblank,    ///是否有空格
            Noand,      ///是否有&
            Noinject,   ///禁止SQL注入
            IsDate,       ///是否日期格式
            IsNumeric,    ///是否数字格式
            IsASCIIText,  ///是否ASCII字符
            IsGBText      ///是否中文字符
        }

        public static bool InputValidate(string InputStr, Pattern kind)
        {
            if ((InputStr.Trim() == "") || (InputStr == null))
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
    }
}
