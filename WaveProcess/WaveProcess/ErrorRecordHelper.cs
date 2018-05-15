using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveProcess
{
    public static class ErrorRecordHelper
    {

        public static void Clear()
        { 
             SimpleErrors.Clear();
             DetailedErrors.Clear();
        }

        public static void Record(Exception e)
        {
            MessageBox.Show(e.Message);
            SimpleErrors.Add(e.Message);
            DetailedErrors.Add(e.ToString());
        }
        
        public static List<string> SimpleErrors = new List<string>();
        public static List<string> DetailedErrors = new List<string>();


    }
}
