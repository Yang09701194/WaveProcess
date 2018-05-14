using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace WaveProcess
{
    /// <summary>
    /// 詳細說明  見ExampleCollection.cs
    /// </summary>
    public class MultiWaveChannelMergeHelper
    {
        /// <summary>
        /// 把兩個單音軌的檔案  合併為一個雙聲道的檔案
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

            using (
                WaveFileWriter writer = new WaveFileWriter(outputFilePath,
                    new WaveFormat(wav1Reader.WaveFormat.SampleRate, 16, 2))
                )
            {
                int bytesRead;
                while ((bytesRead = waveProvider.Read(buffer, 0, buffer.Length)) > 0)
                {
                    writer.WriteData(buffer, 0, bytesRead);
                }
            }
        }

    }
}
