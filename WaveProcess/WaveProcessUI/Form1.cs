using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ookii.Dialogs.Wpf;
using WaveProcess;

namespace WaveProcessUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnChooseFolder_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog OFD = new VistaFolderBrowserDialog();
            if (OFD.ShowDialog() == true)
                TbxFolderPath.Text = OFD.SelectedPath;
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            ClearLog();
            string wavFilesParentFolderPath = TbxFolderPath.Text;
            if (!Directory.Exists(wavFilesParentFolderPath)){
                AppendUILog("選擇之資料夾不存在---" + wavFilesParentFolderPath);
                return;
            }
            try
            {
                string[] wavFolders = Directory.GetDirectories(wavFilesParentFolderPath);

                //單一資料夾內音檔串接結果輸出於
                string concatenateOutputFolder = Path.GetDirectoryName(wavFilesParentFolderPath) + "\\" + "EachWavFolderConcatenate";
                AppendUILog("單一資料夾內音檔串接結果輸出於" + concatenateOutputFolder);
                if (Directory.Exists(concatenateOutputFolder))
                    Directory.Delete(concatenateOutputFolder, true);
                //如果檔案總管開在資料夾  一進行刪除  檔案總管會花時間跳出被刪除的資料夾
                //如果沒有停頓  底下的Create好像會因為時間差沒有效果 造成資料夾不存在的錯誤  所以這裡停頓
                Thread.Sleep(2000);
                Directory.CreateDirectory(concatenateOutputFolder);

                foreach (string wavFolder in wavFolders){
                    var folderFileTimes = FileNameProcessHelper.GetFolder_FileTimes(wavFolder);
                    WaveConcatenateHelper.ProcessSlienceAndConcatenate(concatenateOutputFolder, folderFileTimes);
                }

                //Agent、客戶音檔最終合併輸出於
                string outputFolderPath = Path.GetDirectoryName(concatenateOutputFolder) + "\\" + "Result";
                AppendUILog("Agent、客戶音檔最終合併輸出於" + outputFolderPath);
                if (Directory.Exists(outputFolderPath))
                    Directory.Delete(outputFolderPath, true);
                Thread.Sleep(2000);
                Directory.CreateDirectory(outputFolderPath);

                MultiWaveChannelMergeHelper.ConcatenatedFiles_Merge2Channel
                    (outputFolderPath, concatenateOutputFolder);
            }
            catch (Exception ee)
            {
                LogHelper.LogError(ee);
            }

            AppendUILog(LogHelper.LogDetailedMsg_And_GetSimpleErrorMsg());
            AppendUILog(LogHelper.LogLogMsg_And_GetLogMsg());

        }



        private void AppendUILog(string message)
        {
            TbxExeMsg.Text += "\r\n" + DateTime.Now.ToString("T") +" " + message + "\r\n";
        }

        private void ClearLog()
        {
            TbxExeMsg.Text = "";
            LogHelper.Clear();
        }



    }
}
