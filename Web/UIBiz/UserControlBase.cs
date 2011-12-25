/* class:   UserControlBase
 * detail:  �û��ؼ�����
 * 
 * create by:   ���� 2006-04-21
 * last modify: ����
 * date:    2006-04-27
 */

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

namespace OnlineNew.WebUI
{
    /// <summary>
    /// User Control Base Class
    /// </summary>
    public class UserControlBase: System.Web.UI.UserControl
    {
        public UserControlBase()
        {
        }

        #region ��ʾ��Ϣ��ʾ�Ի���
        /// <summary>
        /// show a "alert" window
        /// </summary>
        /// <param name="Message">Message</param>
        public void ShowAlert(string Message)
        {
            string message =
                "<script language='javascript' type='text/javascript'>alert('" + Message + "')</script>";
            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "ShowMessage", message);
        }

        /// <summary>
        /// show a "Confirm" window
        /// </summary>
        /// <param name="page">request page (So as: this)</param>
        /// <param name="ConfirmMessage">Confirm message</param>
        public void ShowConfirmMessage(string ConfirmMessage)
        {
            string message =
                "<script language='javascript' type='text/javascript'>confirm('" + ConfirmMessage + "')</script>";
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowConfirmMessage", message);
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲�����ҳ����ת
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="url">��ת��Ŀ��URL</param>
        public void ShowAndRedirect(string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            Type jsType = this.Page.GetType();
            this.Page.ClientScript.RegisterStartupScript(jsType, "ShowAndRedirect", Builder.ToString());
        }
        #endregion

        #region Window operations
        /// <summary>
        /// close window without conform
        /// </summary>
        public void CloseWindow()
        {
            string script = "<script language='javascript' type='text/javascript'>window.opener=null;window.close()</script>";
            Type jsType = this.Page.GetType();
            this.Page.ClientScript.RegisterClientScriptBlock(jsType, "CloseWindow", script);
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
            Type jsType = this.Page.GetType();
            this.Page.ClientScript.RegisterClientScriptBlock(jsType, "OpnerNewWindow", script);
        }

        #endregion

        #region costumer Register java script
        /// <summary>
        /// ����Զ���ű���Ϣ
        /// </summary>
        /// <param name="script">����ű�</param>
        public void ResponseScript(string script)
        {
            script = "<script language='javascript' type='text/javascript' defer>" + script + "</script>";
            Type jsType = this.Page.GetType();
            this.Page.ClientScript.RegisterStartupScript(jsType, "ResponseScript", script);
        }
        #endregion

        #region Clear Control
        /// <summary>
        /// ȡ��ҳ�����������ؼ�����������ǵ�ֵ
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
            IsGBText,
            /// <summary>
            /// �Ƿ��й���������
            ///</summary>
            IsPostCode
        }

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
                        RegexPattern = new Regex(@"([^'&%^!#*|?*+\t\n\r\\.]{3,50})$");
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
                        RegexPattern = new Regex(@"[^\f\n\r\t\v]{6,20}$");
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
                    case Pattern.IsPostCode :
                        RegexPattern = new Regex(@"[\d]{6}$");
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
                        RegexPattern = new Regex(@"([^'&%^!#*|?*+\t\n\r\\.]{3,50})$");
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
                        RegexPattern = new Regex(@"[^\f\n\r\t\v]{6,20}$");
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
                    case Pattern.IsPostCode:
                        RegexPattern = new Regex(@"[\d]{6}$");
                        break;
                    default:
                        return false;
                }
                return RegexPattern.IsMatch(InputStr);
            }

        }
        #endregion

        #region �������ռ�����Ŀո� (Trim)
        public void TrimInputValue()
        {
            foreach (System.Web.UI.Control control in this.Controls)
            {
                switch (control.GetType().Name.ToUpper())
                {
                    case "TEXTBOX":
                        System.Web.UI.WebControls.TextBox textBox = control as System.Web.UI.WebControls.TextBox;
                        if (textBox.TextMode != TextBoxMode.Password)
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