using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveProcess
{
    public static class LogHelper
    {

        public static void Clear()
        { 
            SimpleErrors.Clear();
            DetailedErrors.Clear();
            Logs.Clear();
        }

        public static void LogError(Exception e)
        {
            //MessageBox.Show(e.Message);
            SimpleErrors.Add(DateTime.Now.ToString("s")+e.Message);
            DetailedErrors.Add(DateTime.Now.ToString("s")+e.ToString());
        }

        public static void Log(string s)
        {
            Logs.Add(DateTime.Now.ToString("s")+s);
        }
        

        //Log
        public static List<string> Logs = new List<string>();

        //Error
        public static List<string> SimpleErrors = new List<string>();
        public static List<string> DetailedErrors = new List<string>();


        #region 輸出



        public static string LogLogMsg_And_GetLogMsg()
        {
            if (Logs.Any())
            {
                string msg = "一般訊息:\r\n" + Logs.Aggregate((pre, next) => pre + "\n" + next);
                File.AppendAllText(".\\Log.txt", msg);
                return msg;
            }
            else
                return "";
        }


        public static string LogDetailedMsg_And_GetSimpleErrorMsg()
        {
            LogDetailedErrorMsgToLogFile();

            if (SimpleErrors.Any())
            {
                return "製行過程發生錯誤:\r\n" + SimpleErrors.Aggregate((pre, next) => pre + "\r\n" + next);
            }
            else
                return "執行成功";
        }
        
        private static void LogDetailedErrorMsgToLogFile()
        {
            if (DetailedErrors.Any())
            {
                File.AppendAllText(".\\Log.txt",
                    DetailedErrors.Aggregate((pre, next) => pre + "\r\n" + next)
                    );
            }
        }

        #endregion 


    }
}
