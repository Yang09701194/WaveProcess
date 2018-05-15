using System;
using System.Collections.Generic;
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
            MessageBox.Show(e.Message);
            SimpleErrors.Add(e.Message);
            DetailedErrors.Add(e.ToString());
        }

        public static void Log(string s)
        {
            Logs.Add(s);
        }
        

        //Log
        public static List<string> Logs = new List<string>();

        //Error
        public static List<string> SimpleErrors = new List<string>();
        public static List<string> DetailedErrors = new List<string>();

    }
}
