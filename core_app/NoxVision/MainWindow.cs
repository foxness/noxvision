using Accord.Video.FFMPEG;
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

namespace NoxVision
{
    public partial class MainWindow : Form
    {
        private AnalysisWindow aw = new AnalysisWindow();

        private string videoFilepath;
        private string analysisFilepath;
        private AnalysisInfo analysisInfo;

        private Graphics g;
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
                    OpenFile(videoFilepath);

                    //mediaPlayer.URL = @"R:\my\projects\noxvision\analysis_engine\output.avi";
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            g = player.CreateGraphics();
        }

        private void OpenFile(string filePath)
        {
            using (var reader = new VideoFileReader())
            {
                reader.Open(filePath);
                for (int i = 0; i < reader.FrameCount; i++)
                {
                    var frame = reader.ReadVideoFrame();
                    g.DrawImage(frame, 0, 0);
                }
                reader.Close();
            }
        }
    }
}
