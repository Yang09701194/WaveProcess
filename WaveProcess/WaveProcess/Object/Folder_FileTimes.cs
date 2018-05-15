using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveProcess.Object
{
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
