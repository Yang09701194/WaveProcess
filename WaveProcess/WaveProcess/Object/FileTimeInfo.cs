using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaveProcess.Object
{
    /// <summary>
    /// 預計處理邏輯
    /// 
    /// 1.把每個資料夾的多個單聲道檔案  加上靜音後串聯為一個檔案  個別輸出
    /// 所以 輸入為  資料夾清單 >  輸出為  串聯完的音檔清單
    /// 
    /// 所以讀取時  前面的0 1不重要  因為同個資料夾裡的都一樣
    /// 所以只要記錄  檔案路徑 毫秒數起 毫秒數訖  就可以了
    /// 
    /// 2.把所有串聯完成之音檔名稱後方相同的  取開頭1,2 合併為雙聲道單一檔案 
    /// 即完成
    /// </summary>
    public class FileTimeInfo
    {
        public FileTimeInfo(string filePath, int startMilliseconds, int endMilliseconds)
        {
            FilePath = filePath;
            StartMilliseconds = startMilliseconds;
            EndMilliseconds = endMilliseconds;
        }

        public string FilePath { get; set; }
        public int StartMilliseconds { get; set; }
        public int EndMilliseconds { get; set; }


        public override string ToString()
        {
            return StartMilliseconds.ToString() + " " + EndMilliseconds;
        }
    }

}
