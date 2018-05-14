using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace WaveProcess
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        static void Main(string[] args)
        {
            MultiWaveChannelMergeHelper.Merge_2FilesWith1Channel_To_OneFileWith2Channel();
            //Merge2file_To1file2channel();


        }


        public static void Merge2file_To1file2channel()
        {
            var wav1Reader = new WaveFileReader(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00001_0000364_0000837.wav");
            var wav2Reader = new WaveFileReader(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00002_0001228_0001566.wav");

            
            List<IWaveProvider> inputs = new List<IWaveProvider>() { wav1Reader, wav2Reader };
            MultiplexingWaveProvider waveProvider = new MultiplexingWaveProvider(inputs, 2);

            waveProvider.ConnectInputToOutput(0, 0);
            waveProvider.ConnectInputToOutput(1, 1);

            byte[] buffer = new byte[2 * wav1Reader.WaveFormat.SampleRate * wav1Reader.WaveFormat.Channels];

            using (
                WaveFileWriter writer = new WaveFileWriter(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\output\merge.wav", new WaveFormat(wav1Reader.WaveFormat.SampleRate, 16, 2))
                )
            {
                int bytesRead;
                while ((bytesRead = waveProvider.Read(buffer, 0, buffer.Length)) > 0)
                {
                    writer.WriteData(buffer, 0, bytesRead);
                }
            }
        }

        
        public static void SplitEachChannelToFiles()
        {

            var reader = new WaveFileReader(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00001_0000364_0000837.wav");
            var buffer = new byte[2 * reader.WaveFormat.SampleRate * reader.WaveFormat.Channels];
            var writers = new WaveFileWriter[reader.WaveFormat.Channels];
            for (int n = 0; n < writers.Length; n++)
            {
                var format = new WaveFormat(reader.WaveFormat.SampleRate, 16, 1);
                writers[n] = new WaveFileWriter(String.Format(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\output\channel{0}.wav", n + 1), format);
            }
            int bytesRead;



            while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                int offset = 0;
                while (offset < bytesRead)
                {
                    for (int n = 0; n < writers.Length; n++)
                    {
                        // write one sample
                        writers[n].Write(buffer, offset, 2);
                        offset += 2;
                    }
                }
            }



            for (int n = 0; n < writers.Length; n++)
            {
                writers[n].Dispose();
            }
            reader.Dispose();

        }

    }
}
