using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using WaveProcess.Object;

namespace WaveProcess
{
    public static class WaveConcatenateHelper
    {

        /// <summary>
        /// 這個方法會直接 把一個資料夾內的所有音檔 加上靜音並且  合併好  
        /// 輸出路徑是   "{0}\\EachFolderConcatenateResult\\{1}.wav"
        /// </summary>
        /// <param name="folderFileTimes"></param>
        public static void ProcessSlienceAndConcatenate(Folder_FileTimes folderFileTimes)
        {
            //合併用的writer
            WaveFileWriter waveFileWriter = null;
            string outputFilePath = String.Format("{0}\\EachFolderConcatenateResult\\{1}.wav",
                Path.GetDirectoryName(folderFileTimes.FolderPath), 
                Path.GetFileName(folderFileTimes.FolderPath));

            Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));

            try
            {
                int previousEnd = 0;
                foreach (var wavFileInfo in folderFileTimes.FileTimeInfos)
                {
                    using (WaveFileReader reader = new WaveFileReader(wavFileInfo.FilePath))
                    {
                        if (waveFileWriter == null)
                            // first time in create new Writer
                            waveFileWriter = new WaveFileWriter(outputFilePath, reader.WaveFormat);
                        //check wab format
                        else if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                            throw new InvalidOperationException(
                                "Can't concatenate WAV Files that don't share the same format");

                        //write previous silence
                        if (wavFileInfo.StartMilliseconds < previousEnd)
                            throw new Exception
                                ("後一個路徑音檔時間開頭毫秒數 不可大於 前一個音檔結束毫秒數，\n毫秒結尾:" + previousEnd + "\n後一個音檔" + wavFileInfo.FilePath);
                        int silenceMilliSecond = wavFileInfo.StartMilliseconds - previousEnd;
                        var silentBytes = GetSilenceBytes(silenceMilliSecond, 1);//原始音檔都是 1 channel
                        waveFileWriter.Write(silentBytes, 0, silentBytes.Length);

                        //write current wav
                        byte[] buffer = new byte[1024];
                        int readBytes;
                        while ((readBytes = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            waveFileWriter.Write(buffer, 0, readBytes);
                        }
                    }
                    previousEnd = wavFileInfo.EndMilliseconds;
                }
            }
            catch (Exception e)
            {
                ErrorRecordHelper.Record(e);
            }
            finally
            {
                if (waveFileWriter != null) { waveFileWriter.Dispose(); }
            }
        }


        /// <summary>
        /// 由  ExampleCollection.CreatBigeSampleWavFile的測試  可以得到以下數據
        /// 上面結果
        /// sample rate 8000, 2  channel 
        /// 10000000 bytes > 312500 ms
        /// sample rate 8000, 2  channel  >>  1 milisecond = 32byte   耶@@  竟然可以整除!!　
        /// sample rate 8000, 1  channel  >>  1 milisecond = 16byte   
        /// 
        /// 這邊認定sample rate 8000
        /// 只給channel 1 or 2的選項
        /// 
        /// 而所謂的Silence 由 ExampleCollection.CreateSampleWavFile
        /// 可知  Byte就是紀錄聲音的波動
        /// 所以理論上只要全部給0  沒有波動  那一段就會是靜音吧
        /// </summary>
        /// <param name="milliSecond">要產生的靜音毫秒數長度</param>
        /// <param name="channel">聲道數  1 或 2</param>
        private static byte[] GetSilenceBytes(int milliSecond, int channel)
        {
            if (channel == 1)
                return new byte[ milliSecond * 16 ];
            else
                return new byte[ milliSecond * 32 ];
        }




        /// <summary>
        /// 這個方法沒有實際用到  但是有使用裡面的寫入模式在上面的方法
        /// </summary>
        /// <param name="sourceFilePaths"></param>
        /// <param name="outputFilePath"></param>
        private static void Concatenate(List<string> sourceFilePaths, string outputFilePath)
        {
            byte[] buffer = new byte[1024];
            WaveFileWriter waveFileWriter = null;
            try
            {
                foreach (string sourceFile in sourceFilePaths)
                {
                    using (WaveFileReader reader = new WaveFileReader(sourceFile))
                    {
                        #region wav format equal check
                        if (waveFileWriter == null)
                        {
                            // first time in create new Writer
                            waveFileWriter = new WaveFileWriter(outputFilePath, reader.WaveFormat);
                        }
                        else
                        {
                            if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                            {
                                throw new InvalidOperationException("Can't concatenate WAV Files that don't share the same format");
                            }
                        }
                        #endregion 
                        int readBytes;
                        while ((readBytes = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            waveFileWriter.WriteData(buffer, 0, readBytes);
                        }
                    }
                }
            }
            finally{   if (waveFileWriter != null)  {  waveFileWriter.Dispose();  }  }
        }

        public static void Test()
        {
            Concatenate(
                new List<string>()
                {
                    @"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00001_0000364_0000837.wav",
                    @"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00001_0000364_0000837.wav",
                    @"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00001_0000364_0000837.wav",
                    @"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00002_0001228_0001566.wav",
                    @"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00003_0001718_0002017.wav",
                    @"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00004_0002571_0002882.wav",
                    @"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00005_0003056_0003367.wav",

                }
                ,
                @"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\output\TestConcatenate.wav"
                );
        }

    }
}
