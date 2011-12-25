using System;
using System.Data;
using System.IO;
using System.Text;

/// <summary>
/// LogWriter 的摘要说明
/// </summary>.
/// 

namespace QJVRMS.Common
{
    public class LogWriter
    {
        public LogWriter()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        private static string flag = "--------------------------------------------";

        public static void WriteExceptionLog(Exception ex)
        {
            WriteExceptionLog(ex, true);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="isDetail">默认：false</param>
        public static void WriteExceptionLog(Exception ex, bool isDetail)
        {
            if (isDetail)
            {
                LogWriter.WriteLog("UploaderEXP", new string[] { DateTime.Now.ToString(), ex.Message, ex.Source, ex.StackTrace, flag }, true);
            }
            else
            {
                LogWriter.WriteLog("UploaderEXP", new string[] { DateTime.Now.ToString(), ex.Message, flag }, true);
            }
        }


        public static void WriteLog(string LogName, string[] logStr)
        {
            WriteLog(LogName, logStr, false, ".txt");
        }

        public static void WriteLog(string LogName, string[] logStr, bool noTime)
        {
            WriteLog(LogName, logStr, noTime, ".txt");
        }

        public static void WriteLog(string LogName, string[] logStr, string logExtend)
        {
            WriteLog(LogName, logStr, false, logExtend);
        }

        public static void WriteLog(string LogName, string[] logStr, bool noTime, string logExtend)
        {
            string logpath = string.Empty;

            try
            {
                if (noTime)
                {
                    logpath = AppDomain.CurrentDomain.BaseDirectory + string.Format("{0}{1}", LogName, logExtend);
                }
                else
                {
                    logpath = AppDomain.CurrentDomain.BaseDirectory + string.Format("{0:yyMMdd}{1}{2}", DateTime.Today, LogName, logExtend);
                }

                using (StreamWriter sw = new StreamWriter(logpath, true, Encoding.Default))
                {
                    for (int i = 0; i < logStr.Length; i++)
                    {
                        sw.WriteLine(logStr[i]);
                    }
                }

            }
            catch { }

        }
    }
}
