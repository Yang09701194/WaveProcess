using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using WaveProcess.DirectlyWorkExample;

namespace WaveProcess
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        static void Main(string[] args)
        {
            //ExampleCollection.Merge_2FilesWith1Channel_To_OneFileWith2Channel();
            //ExampleCollection.CreateSampleWavFile();
            //WaveConcatenateHelper.Test();
            //ExampleCollection.CreatBigeSampleWavFile();
            ExampleCollection.TestTrimWavFile();

            //return;

            string wavParentFolder = @"E:\GS2018\E\Yang\Program\Git\GitYang\WaveProcess\WaveProcess\TestWavFile\WaveFiles";
            string[] wavFolders = Directory.GetDirectories(wavParentFolder);

            string concatenateOutputFolder = Path.GetDirectoryName(wavParentFolder) + "\\" + "EachWavFolderConcatenate";
            if (Directory.Exists(concatenateOutputFolder))
                Directory.Delete(concatenateOutputFolder, true);
            //如果檔案總管開在資料夾  一進行刪除  檔案總管會花時間跳出被刪除的資料夾
            //如果沒有停頓  底下的Create好像會因為時間差沒有效果 造成資料夾不存在的錯誤
            //所以這裡停頓
            Thread.Sleep(2000);
            Directory.CreateDirectory(concatenateOutputFolder);

            foreach (string wavFolder in wavFolders)
            {
                var folderFileTimes = FileNameProcessHelper.GetFolder_FileTimes(wavFolder);
                WaveConcatenateHelper.ProcessSlienceAndConcatenate(concatenateOutputFolder, folderFileTimes);
            }

            MultiWaveChannelMergeHelper.ConcatenatedFiles_Merge2Channel(concatenateOutputFolder);
        }
    }
}
