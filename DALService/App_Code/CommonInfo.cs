using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// CommonInfo 的摘要说明
/// </summary>
public class CommonInfo
{

    protected static string conQJVRMS = ConfigurationManager.ConnectionStrings["DM"].ConnectionString;

    public static string ConQJVRMS
    {
        get
        {
            return conQJVRMS;
        }
    }


    protected static string ftpAddress = ConfigurationManager.AppSettings["FtpAddress"];

    public static string FtpAddress
    {
        get { return ftpAddress; }
    }

    protected static string ftpUser = ConfigurationManager.AppSettings["FtpUser"]; 

    public static string FtpUser
    {
        get { return ftpUser; }
    }

    protected static string ftpPwd = ConfigurationManager.AppSettings["FtpPwd"];   

    public static string FtpPwd
    {
        get { return ftpPwd; }
    }



}
