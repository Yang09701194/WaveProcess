using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            string folderPath =
                @"E:\GS2018\E\Yang\Program\Git\GitYang\WaveProcess\WaveProcess\TestWavFile\1_8690002555DA7B59370000037";

            var folderFileTimes = FileNameProcessHelper.GetFolder_FileTimes(folderPath);
            folderFileTimes = folderFileTimes;

            WaveConcatenateHelper.ProcessSlienceAndConcatenate(folderFileTimes);


            
        }
    }
}
