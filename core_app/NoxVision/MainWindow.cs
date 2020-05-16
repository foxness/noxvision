using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;

namespace NoxVision
{
    public partial class MainWindow : Form
    {
        private AnalysisWindow aw = new AnalysisWindow();

        private string videoFilepath;
        private string analysisFilepath;
        private AnalysisInfo analysisInfo;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task<AnalysisInfo> GetAnalysisInfo()
        {
            var rawJson = File.ReadAllText(analysisFilepath);
            var info = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<AnalysisInfo>(rawJson));

            return info;
        }

        private async void openMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;

            using (var ofd = new OpenFileDialog())
            {
                // ofd.InitialDirectory = "c:\\";
                ofd.InitialDirectory = @"R:\my\drive\sync\things\projects\noxvisioncloud\people - counting - opencv\videos";
                ofd.Filter = "Video files (*.mp4;*.avi)|*.mp4;*.avi";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    videoFilepath = ofd.FileName;
                    aw.FilePath = videoFilepath;
                    aw.ShowDialog();
                    analysisFilepath = aw.OutputAnalysisFilepath;

                    analysisInfo = await GetAnalysisInfo();

                    //mediaPlayer.URL = @"R:\my\projects\noxvision\analysis_engine\output.avi";
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private async void OpenFile(string filePath)
        {
            IMediaInfo inputFile = await FFmpeg.GetMediaInfo(filePath);
        }
    }
}
