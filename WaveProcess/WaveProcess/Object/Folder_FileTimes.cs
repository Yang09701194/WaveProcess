using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveProcess.Object
{
    /// <summary>
    /// 資料夾路徑   和  裡面的音檔檔案資訊
    /// </summary>
    public class Folder_FileTimes
    {
        public Folder_FileTimes(string folderPath, List<FileTimeInfo> fileTimeInfos)
        {
            FolderPath = folderPath;
            FileTimeInfos = fileTimeInfos;
        }

        public string FolderPath { get; set; }
        public List<FileTimeInfo> FileTimeInfos { get; set; }
        
    }
}
