using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

namespace WebUI.common
{
    /// <summary>
    /// PageBase 的摘要说明
    /// </summary>
    public class PageBase : System.Web.UI.Page
    {
        public PageBase()
        {
            try
            {
                urlSuffix = Context.Request.Url.Host + Context.Request.ApplicationPath;
                pageUrlBase = @"http://" + urlSuffix;
            }
            catch
            {
                // for design time
            }
        }
        protected const string UNKNOWERROR = "操作没有成功，请重试。如果重试后仍有问题，请<a href=\"/comments/feedback.aspx\">告诉我们</a>。";
        protected const string UNKNOWERROREN = "Action failed. Please try again. If the problem still exists, please <a href=\"/comments/feedback.asp\">contact us</a>.";

        private static String pageUrlBase;
        private static String urlSuffix;

        protected override void OnLoad(EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                //TODO:如果用户没有登录信息，就退出
            }
            base.OnLoad(e);
            //if (Request.Browser.JavaScript && Request.Browser.MajorVersion >= 5 && Request.Browser.Browser == "IE" && Request.Browser.Cookies)
            //    base.OnLoad(e);
            //else
            //    Response.Redirect("/unsupported.htm");
            #region 设置日期格式
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("zh-CN");
            System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern = "yyyy-MM-dd";
            #endregion
        }

        /// <summary>
        /// 属性UrlBase用于获得URLs的签证
        /// </summary>
        public static String UrlBase
        {
            get
            {
                return pageUrlBase;
            }
        }
        /// <summary>
        /// 取得错误输出
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <returns>错误信息的格式化版本</returns>
        public string GetErrorHtml(string errorMessage)
        {
            return GetErrorHtml(errorMessage, "95%");
        }

        /// <summary>
        /// 重载，返回错误输出，可以指定表格宽度。
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="Width">表格宽度</param>
        /// <returns>错误信息的格式化版本</returns>
        public string GetErrorHtml(string errorMessage, string Width)
        {
            string message = @"<table width='" + Width + "' border='0' align='center' cellpadding='3' cellspacing='1' bgcolor='#cccccc'>"
              + "<tr>"
              + "<td bgcolor='#ffffff'><table width='100%' border='0' align='center' cellpadding='3' cellspacing='0'>"
              + "<tr>"
              + "<td width='30'><img src='/images/alart.gif' width='26' height='22'></td>"
              + "<td><span class='alert'>" + errorMessage + "</span></td>"
              + "</tr>"
              + "</table>"
              + "</td>"
              + "</tr>"
              + "</table>";

            return message;
        }


        /// <summary>
        /// 取得正确信息的输出
        /// </summary>
        /// <param name="okMessage">要显示的正确信息</param>
        /// <returns>正确信息的格式化版本</returns>
        public string GetOKHtml(string okMessage)
        {
            return GetOKHtml(okMessage, "95%");
        }

        /// <summary>
        /// 重载，返回正确信息的输出，可以指定表格宽度。
        /// </summary>
        /// <param name="okMessage">要显示的正确信息</param>
        /// <param name="Width">表格宽度</param>
        /// <returns>正确信息的格式化版本</returns>
        public string GetOKHtml(string okMessage, string Width)
        {
            string message = @"<table width='" + Width + "' border='0' align='center' cellpadding='3' cellspacing='1' bgcolor='#cccccc'>"
              + "<tr>"
              + "<td bgcolor='#ffffff'><table width='100%' border='0' align='center' cellpadding='3' cellspacing='0'>"
              + "<tr>"
              + "<td width='30'><img src='/images/alart_succ.gif' width='26' height='22'></td>"
              + "<td><span class='alert'>" + okMessage + "</span></td>"
              + "</tr>"
              + "</table>"
              + "</td>"
              + "</tr>"
              + "</table>";

            return message;
        }


        /// <summary>
        /// 对影响Web页面显示的字符进行转义编码
        /// </summary>
        /// <param name="OutString">需要进行转义编码的字符串</param>
        /// <returns>进行转义编码后的字符串</returns>
        public string GetHtmlEncode(string OutString)
        {
            return Server.HtmlEncode(OutString);
        }
        /// <summary>
        /// 对影响Web页面显示的字符进行转义解码
        /// </summary>
        /// <param name="OutString">需要进行转义解码的字符串</param>
        /// <returns>进行转义编码后的字符串</returns>
        public string GetHtmlDecode(string OutString)
        {
            return Server.HtmlDecode(OutString);
        }

        #region 显示消息提示对话框
        /// <summary>
        /// show a "alert" window
        /// </summary>
        /// <param name="Message">Message</param>
        public void ShowAlert(string Message)
        {
            string message =
                "<script language='javascript' type='text/javascript'>alert('" + Message + "')</script>";
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowMessage", message);
        }

        /// <summary>
        /// show a "Confirm" window
        /// </summary>
        /// <param name="ConfirmMessage">Confirm message</param>
        public  void ShowConfirmMessage(string ConfirmMessage)
        {
            string message =
                "<script language='javascript' type='text/javascript'>confirm('" + ConfirmMessage + "')</script>";
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowConfirmMessage", message);
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public void ShowAndRedirect(string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            Type jsType = this.GetType();
            this.ClientScript.RegisterStartupScript(jsType, "ShowAndRedirect", Builder.ToString());
        }
        #endregion

        #region Window operations
        /// <summary>
        /// close window without conform
        /// </summary>
        public void CloseWindow()
        {
            string script = "<script language='javascript' type='text/javascript'>window.opener=null;window.close()</script>";
            Type jsType = this.GetType();
            this.ClientScript.RegisterClientScriptBlock(jsType, "CloseWindow", script);
        }

        /// <summary>
        /// Open a new window
        /// </summary>
        /// <param name="strURL">new window's URL</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        public void OpnerNewWindow(string strURL, int width, int height)
        {
            string script =
                "<script language='javascript' type='text/javascript'>" +
                "window.open('" + strURL + "'," +
                " '_blank', " +
                "'width=" + width + ", height=" + height + "," +
                "toolbar=no,titlebar=no,status=nos,crollbars=no,menubar=no,location=no ')" +
                "</script>";
            Type jsType = this.GetType();
            this.ClientScript.RegisterClientScriptBlock(jsType, "OpnerNewWindow", script);
        }

        #endregion

        #region costumer Register java script
        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="script">输出脚本</param>
        public void ResponseScript(string script)
        {
            script = "<script language='javascript' type='text/javascript' defer>" + script + "</script>";
            Type jsType = this.GetType();
            this.ClientScript.RegisterStartupScript(jsType, "ResponseScript", script);
        }
        #endregion

        #region Clear Control
        /// <summary>
        /// 取得页面的所有输入控件，并清空他们的值
        /// </summary>
        public void ClearControl()
        {
            foreach (System.Web.UI.Control control in this.Controls)
            {
                switch (control.GetType().Name.ToUpper())
                {
                    #region For Web Controls
                    case "TEXTBOX":
                        System.Web.UI.WebControls.TextBox textBox = control as System.Web.UI.WebControls.TextBox;
                        textBox.Text = "";
                        break;
                    case "DROPDOWNLIST":
                        System.Web.UI.WebControls.DropDownList dropDownList = control as System.Web.UI.WebControls.DropDownList;
                        dropDownList.SelectedIndex = -1;
                        break;
                    case "CHECKBOX":
                        System.Web.UI.WebControls.CheckBox checkBox = control as System.Web.UI.WebControls.CheckBox;
                        checkBox.Checked = false;
                        break;
                    case "CHECKBOXLIST":
                        System.Web.UI.WebControls.CheckBoxList checkBoxList = control as System.Web.UI.WebControls.CheckBoxList;
                        checkBoxList.SelectedIndex = -1;
                        break;
                    case "LISTBOX":
                        System.Web.UI.WebControls.ListBox listBox = control as System.Web.UI.WebControls.ListBox;
                        listBox.SelectedIndex = -1;
                        break;
                    case "RADIOBUTTON":
                        System.Web.UI.WebControls.RadioButton radioButton = control as System.Web.UI.WebControls.RadioButton;
                        radioButton.Checked = false;
                        break;
                    case "RADIOBUTTONLIST":
                        System.Web.UI.WebControls.RadioButtonList radioButtonList = control as System.Web.UI.WebControls.RadioButtonList;
                        radioButtonList.SelectedIndex = -1;
                        break;
                    #endregion

                    #region For Html Controls
                    case "HTMLINPUTTEXT":
                        System.Web.UI.HtmlControls.HtmlInputText htmlInputText = control as System.Web.UI.HtmlControls.HtmlInputText;
                        htmlInputText.Value = "";
                        break;
                    case "HTMLINPUTPASSWORD":
                        System.Web.UI.HtmlControls.HtmlInputPassword htmlInputPassword = control as System.Web.UI.HtmlControls.HtmlInputPassword;
                        htmlInputPassword.Value = "";
                        break;
                    case "HTMLINPUTFILE":
                        System.Web.UI.HtmlControls.HtmlInputFile htmlInputFile = control as System.Web.UI.HtmlControls.HtmlInputFile;
                        htmlInputFile.Value = "";
                        break;
                    case "HTMLSELECT":
                        System.Web.UI.HtmlControls.HtmlSelect htmlSelect = control as System.Web.UI.HtmlControls.HtmlSelect;
                        htmlSelect.SelectedIndex = -1;
                        break;
                    case "HTMLTEXTAREA":
                        System.Web.UI.HtmlControls.HtmlTextArea htmlTextArea = control as System.Web.UI.HtmlControls.HtmlTextArea;
                        htmlTextArea.Value = "";
                        break;
                    case "HTMLINPUTRADIOBUTTON":
                        System.Web.UI.HtmlControls.HtmlInputRadioButton htmlInputRadioButton = control as System.Web.UI.HtmlControls.HtmlInputRadioButton;
                        htmlInputRadioButton.Checked = false;
                        break;
                    #endregion
                }
            }
        }
        #endregion

        #region InputValidate

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

        /// <summary>
        /// 方法：验证字符串
        /// </summary>
        /// <param name="InputStr"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public bool InputValidate(string InputStr, Pattern kind)
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
                    case Pattern.Noand:
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
                    case Pattern.IsGBText:
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
        #endregion

        #region 清楚输入空间两侧的空格 (Trim)
        public void TrimInputValue()
        {
            foreach (System.Web.UI.Control control in this.Controls)
            {
                switch (control.GetType().Name.ToUpper())
                {
                    case "TEXTBOX":
                        System.Web.UI.WebControls.TextBox textBox = control as System.Web.UI.WebControls.TextBox;
                        textBox.Text = textBox.Text.Trim();
                        break;
                    case "HTMLINPUTTEXT":
                        System.Web.UI.HtmlControls.HtmlInputText htmlInputText = control as System.Web.UI.HtmlControls.HtmlInputText;
                        htmlInputText.Value = htmlInputText.Value.Trim();
                        break;
                    case "HTMLINPUTPASSWORD":
                        System.Web.UI.HtmlControls.HtmlInputPassword htmlInputPassword = control as System.Web.UI.HtmlControls.HtmlInputPassword;
                        htmlInputPassword.Value = htmlInputPassword.Value.Trim();
                        break;
                    case "HTMLINPUTFILE":
                        System.Web.UI.HtmlControls.HtmlInputFile htmlInputFile = control as System.Web.UI.HtmlControls.HtmlInputFile;
                        htmlInputFile.Value = htmlInputFile.Value.Trim();
                        break;
                    case "HTMLTEXTAREA":
                        System.Web.UI.HtmlControls.HtmlTextArea htmlTextArea = control as System.Web.UI.HtmlControls.HtmlTextArea;
                        htmlTextArea.Value = htmlTextArea.Value.Trim();
                        break;
                }
            }
        }
        #endregion
    }
}
