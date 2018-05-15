using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveProcess.Object;

namespace WaveProcess
{
    public static class FileNameProcessHelper
    {

        
        public static Folder_FileTimes GetFolder_FileTimes(string folderPath)
        {
            string[] wavFilePaths = Directory.GetFiles(folderPath);
            List<FileTimeInfo> fileTimeInfos = new List<FileTimeInfo>();
            foreach (string wavFilePath in wavFilePaths)
            {
                try
                {
                    string wavFileName = Path.GetFileName(wavFilePath);
                    var stEnd = GetfilenameStartEnd(wavFileName);
                    fileTimeInfos.Add(new FileTimeInfo(wavFilePath, stEnd.Item1, stEnd.Item2));
                }
                catch (Exception e)
                {
                    ErrorRecordHelper.Record(e);
                }
            }
            return new Folder_FileTimes(folderPath, fileTimeInfos);
        }

        

        private static Tuple<int, int> GetfilenameStartEnd(string name)
        {
            //string name = "0_00001_0000364_0000837.wav";

            int indexline1 = name.IndexOf('_');
            int indexline2 = name.IndexOf('_', indexline1 + 1);
            int indexline3 = name.IndexOf('_', indexline2 + 1);
            int indexDot = name.IndexOf('.');

            string startStr = name.Substring(indexline2 + 1, indexline3 - indexline2 - 1);
            //名稱是1/100秒，要再x10才是毫秒
            int start = 10 * Convert.ToInt32(startStr);

            string endStr = name.Substring(indexline3 + 1, indexDot - indexline3 - 1);
            int end = 10 * Convert.ToInt32(endStr);
            
            return new Tuple<int, int>(start, end);
        }

    }

}
