using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            string name = "0_00001_0000364_0000837.wav";

            int indexline1 = name.IndexOf('_');
            int indexline2 = name.IndexOf('_', indexline1 + 1);
            int indexline3 = name.IndexOf('_', indexline2 + 1);
            int indexDot = name.IndexOf('.');

            string startStr = name.Substring(indexline2 + 1, indexline3 - indexline2 - 1);
            int start = 10 * Convert.ToInt32(startStr);

            string endStr = name.Substring(indexline3 + 1, indexDot - indexline3 - 1);
            int end =  10 * Convert.ToInt32(endStr);

            
            Console.WriteLine(start);
            Console.WriteLine(end);

            Console.Read();

            var v = Get();
        }


        public static Tuple<int, int> GetStartEnd()
        {

            string name = "0_00001_0000364_0000837.wav";

            int indexline1 = name.IndexOf('_');
            int indexline2 = name.IndexOf('_', indexline1 + 1);
            int indexline3 = name.IndexOf('_', indexline2 + 1);
            int indexDot = name.IndexOf('.');

            string startStr = name.Substring(indexline2 + 1, indexline3 - indexline2 - 1);
            int start = 10 * Convert.ToInt32(startStr);

            string endStr = name.Substring(indexline3 + 1, indexDot - indexline3 - 1);
            int end = 10 * Convert.ToInt32(endStr);


            Console.WriteLine(start);
            Console.WriteLine(end);

            Console.Read();

            return new Tuple<int, int>(start, end);

        }

    }
}
