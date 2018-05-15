using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace WaveProcess.DirectlyWorkExample
{
    public static class ExampleCollection
    {
        /// <summary>
        /// NAudio套件的MultiplexingWaveProvider.cs原始碼可以了解背後運作原理，很有價值
        /// https://github.com/naudio/NAudio/blob/master/NAudio/Wave/WaveProviders/MultiplexingWaveProvider.cs#L113
        /// 用法的blog說明
        /// http://mark-dot-net.blogspot.tw/2012/01/handling-multi-channel-audio-in-naudio.html
        /// </summary>
        public static void Merge_2FilesWith1Channel_To_OneFileWith2Channel()
        {
            var wav1Reader = new WaveFileReader(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00001_0000364_0000837.wav");
            var wav2Reader = new WaveFileReader(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00002_0001228_0001566.wav");

            //所有要處理channel(聲道)合併的音檔
            List<IWaveProvider> inputs = new List<IWaveProvider>() { wav1Reader, wav2Reader };
            //初始合併工具   後方建構式參數  inputs , numberOfOutputChannel (最後輸出檔案的音軌數)
            MultiplexingWaveProvider waveProvider = new MultiplexingWaveProvider(inputs, 2);

            //這裡看blog會很清楚  前面是指定特定input的檔案聲道  輸出到  輸出檔案的第幾音軌
            //我這邊  讀進來的檔案  都是 單聲道
            //所以 
            //0,0 代表wav1的單音軌  寫到輸出檔的  第0聲道 (有可能是左聲道)
            //1,1 代表wav2的單音軌  寫到輸出檔的  第1聲道 (有可能是右聲道)
            //所以結果就是把兩個單音軌的檔案  合併為一個雙聲道的檔案
            waveProvider.ConnectInputToOutput(0, 0);
            waveProvider.ConnectInputToOutput(1, 1);

            //這邊其實直接叫我寫我完全沒蓋念
            //所以我是抄底下的   SplitEachChannelToFiles  
            byte[] buffer = new byte[2 * wav1Reader.WaveFormat.SampleRate * wav1Reader.WaveFormat.Channels];

            //mp3格式查Stackoverflow 知道很單純  都是純byte  所以像是合併  就直接byte合併
            //寫檔  也可以直接File.WriteAllByte就可以播了
            //但是 wav格式比較複雜   前面會有一段byte的檔頭  紀錄SamplRate Channel等資訊
            //沒有認真研究過的話沒辦法輕易製造   就連一個wav檔  要截斷前面多少byte  才能開始取後面的實際音訊內容
            //都不太容易
            //所以要寫檔的話  比較簡單的方法就是借助NAudio套件
            //直接用WaveFileWriter來寫
            using (
                WaveFileWriter writer = new WaveFileWriter(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\output\merge.wav", new WaveFormat(wav1Reader.WaveFormat.SampleRate, 16, 2/*輸出為雙聲道*/))
                )
            {
                //這個就是標準的Read模式   傳一個緩衝區給他讀  再回傳實際讀了多少大小
                //前面幾次應該都會讀好讀滿
                //最後一區就像除法的餘數  可能只有緩衝區大小的一部份  但是可以很清楚知道讀了多少
                //所以在寫的時候  就可以很清楚地寫bytesRead大小
                //就部會有多寫空白的問題  厲害
                int bytesRead;
                while ((bytesRead = waveProvider.Read(buffer, 0, buffer.Length)) > 0)
                {
                    //用法文章
                    //http://mark-dot-net.blogspot.tw/2011/04/how-to-use-wavefilewriter.html
                    writer.Write(buffer, 0, bytesRead);
                }
            }
        }


        public static void CreateSampleWavFile()
        {
            byte[] testBytes = new byte[1000000];
            for (int i = 0; i < testBytes.Length; i++)
            {
                //被我猜到了  還真的類似  用byte array紀錄波動值 不過這還是用猜的
                //所以如果像是這樣  把所有值  都設成一樣  就沒有聲音
                //我猜   是類似   音響的膜固定在一個點  不動  所以不震動就沒有聲音
                testBytes[i] = (byte)100;

                //但是如果像這樣  讓他 每位移5  規律的在100-200跳動
                //這個化成波形圖應該會像是  -----_____-----______
                //結果就可以聽到  固定頻率的 逼 聲
                if (i%10 < 5)
                    testBytes[i] = (byte)100;
                else
                    testBytes[i] = (byte)200;
            }

            using (
           WaveFileWriter writer = new WaveFileWriter(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\output\testSample.wav", new WaveFormat(8000, 16, 2/*輸出為雙聲道*/))
           )
            {
                writer.Write(testBytes, 0, testBytes.Length);
            }
        }

        public static void CreatBigeSampleWavFile()
        {
            //*****參考用  最下面測的才準******
            //剛才測試發現  音檔SampleRate是8000
            //https://stackoverflow.com/questions/5017367/audio-samples-per-second
            //PCM samples you can divide the total length (in bytes) by the duration (in seconds) to get 
            //****the number of bytes per second****
            //所以代表  8000bytes / 1 sec


            byte[] testBytes = new byte[10000000];
            for (int i = 0; i < testBytes.Length; i++)
            {
                //被我猜到了  還真的類似  用byte array紀錄波動值 不過這還是用猜的
                //所以如果像是這樣  把所有值  都設成一樣  就沒有聲音
                //我猜   是類似   音響的膜固定在一個點  不動  所以不震動就沒有聲音
                testBytes[i] = (byte)100;

                //但是如果像這樣  讓他 每位移5  規律的在100-200跳動
                //這個化成波形圖應該會像是  -----_____-----______
                //結果就可以聽到  固定頻率的 逼 聲
                if (i % 1000 < 500)
                    testBytes[i] = (byte)100;
                else
                    testBytes[i] = (byte)200;
            }

            using (
     WaveFileWriter writer = new WaveFileWriter(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\output\testSample.wav", new WaveFormat(8000, 16, 1/*輸出聲道數*/))
            )
            {
                writer.Write(testBytes, 0, testBytes.Length);
            }

            WaveFileReader reader = new WaveFileReader(@"E:\GS2018\E\Yang\Program\Git\GitYang\WaveProcess\WaveProcess\TestWavFile\EachWavFolderConcatenate\test.wav");

            TimeSpan t = reader.TotalTime;
            double millisecond = t.TotalMilliseconds;
            millisecond = millisecond;

            //上面結果
            //sample rate 8000, 2  channel 
            //10000000 bytes > 312500 ms
            
            //sample rate 8000, 2  channel  >>  1 milisecond = 32byte   耶@@  竟然可以整除!!　
            //sample rate 8000, 1  channel  >>  1 milisecond = 16byte   

            WaveFileReader reader2 = new WaveFileReader(@"E:\Dropbox\WorkGrandsys\W\Workarea\20180511音檔 - 複製\1_8690002555DA7B59370000037\0_00001_0000364_0000837.wav");
            var format = reader2.WaveFormat;
            TimeSpan t2 = reader2.TotalTime;
            double millisecond2 = t2.TotalMilliseconds;
            millisecond2 = millisecond2;
        }



        public static void TestTrimWavFile()
        {

            TrimWavFileHelper.TrimWavFile(
                @"E:\GS2018\E\Yang\Program\Git\GitYang\WaveProcess\WaveProcess\TestWavFile\output\TestConcatenate.wav",
                @"E:\GS2018\E\Yang\Program\Git\GitYang\WaveProcess\WaveProcess\TestWavFile\output\TestTrim.wav",
                new TimeSpan(), TimeSpan.FromSeconds(10) );


        }








        /// <summary>
        /// 來源:  https://stackoverflow.com/questions/12075062/saving-each-wav-channel-as-a-mono-channel-wav-file-using-naudio/12149659#12149659
        /// </summary>
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
