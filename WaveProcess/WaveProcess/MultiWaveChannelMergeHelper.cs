﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace WaveProcess
{
    /// <summary>
    /// 把兩個單音軌的檔案  合併為一個雙聲道的檔案
    /// </summary>
    public class MultiWaveChannelMergeHelper
    {

        /// <summary>
        /// 把
        /// "已將個別資料夾內多個音檔串接好的結果音檔集合"
        /// 的 Agent 和 客戶 的音檔  各自寫到一個聲道 合併成雙聲道的單一音檔  集合
        /// </summary>
        /// <param name="outputFolderPath">輸出資料夾路徑</param>
        /// <param name="concatenatedFolderPath">包含 已將個別資料夾內多個音檔串接好的結果音檔集合 的資料夾</param>
        public static void ConcatenatedFiles_Merge2Channel(string outputFolderPath, string concatenatedFolderPath)
        {
            //取得所有  串接完成的 音檔集合
            string[] concatenatedfiles = Directory.GetFiles(concatenatedFolderPath);
            
            Dictionary<string, List<string>> dic_Key_2ChannelFiles = new Dictionary<string, List<string>>();

            //ex:  
            //1_8690002555DA7B59370000037.wav
            //2_8690002555DA7B59370000037.wav
            //以底線後方字串  將檔案分組   
            //分組方法使用字典 底線後方字串為key  完整檔名為value
            //正確分完的結果   會是每個key  對應到開頭為1(Agent音檔) 和 2(客戶音檔)
            foreach (string concatenatedfile in concatenatedfiles)
            {
                if (!concatenatedfile.Contains("_"))
                {
                    LogHelper.LogError(new Exception("Concatenate後的檔名不包含底線，格式錯誤===" + concatenatedfile));
                    continue;
                }
                
                string key = Path.GetFileName(concatenatedfile).Split('_')[1];
                if (dic_Key_2ChannelFiles.ContainsKey(key))
                    dic_Key_2ChannelFiles[key].Add(concatenatedfile);
                else
                    dic_Key_2ChannelFiles.Add(key, new List<string>() {concatenatedfile});
            }
            //分組後不是兩個檔案  報錯
            var keyNotWith2Files = dic_Key_2ChannelFiles.Where(kv => kv.Value.Count != 2);
            keyNotWith2Files.Select(kv=>kv.Key).ToList().
                ForEach( key => LogHelper.LogError( new Exception("Concatenate後的底線後方名稱相同之檔案數不為兩個 --- " + key)) );
            //分組後是兩個檔案的  進行聲道合併
            var keyWith2Files = dic_Key_2ChannelFiles.Where(kv => kv.Value.Count == 2);
            foreach (KeyValuePair<string, List<string>> key2Files in keyWith2Files)
            {
                try
                {
                    var files = key2Files.Value;
                    Merge_2FilesWith1Channel_To_OneFileWith2Channel(files[0], files[1],
                        outputFolderPath + "\\" + key2Files.Key);
                }
                catch (Exception e){ LogHelper.LogError(e); }
            }
        }


        /// <summary>
        /// 把兩個單音軌的檔案  合併為一個雙聲道的檔案
        /// 詳細說明  見ExampleCollection.cs
        /// </summary>
        /// <param name="inputWavFilePath1">wav檔1</param>
        /// <param name="inputWavFilePath2">wav檔2</param>
        /// <param name="outputFilePath">合併輸出到</param>
        public static void Merge_2FilesWith1Channel_To_OneFileWith2Channel
            (string inputWavFilePath1, string inputWavFilePath2, string outputFilePath)
        {
            var wav1Reader = new WaveFileReader(inputWavFilePath1);
            var wav2Reader = new WaveFileReader(inputWavFilePath2);

            List<IWaveProvider> inputs = new List<IWaveProvider>() { wav1Reader, wav2Reader };
            MultiplexingWaveProvider waveProvider = new MultiplexingWaveProvider(inputs, 2);

            waveProvider.ConnectInputToOutput(0, 0);
            waveProvider.ConnectInputToOutput(1, 1);

            byte[] buffer = new byte[2 * wav1Reader.WaveFormat.SampleRate * wav1Reader.WaveFormat.Channels];

            using (WaveFileWriter writer = new WaveFileWriter(outputFilePath,
                    new WaveFormat(wav1Reader.WaveFormat.SampleRate, 16, 2))
                )
            {
                int bytesRead;
                while ((bytesRead = waveProvider.Read(buffer, 0, buffer.Length)) > 0)
                {
                    writer.Write(buffer, 0, bytesRead);
                }
            }
            //reader也要dispose才不會咬住檔案
            wav1Reader.Dispose();
            wav2Reader.Dispose();
        }

    }
}
