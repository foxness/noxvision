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
        private AnalysisInfo analysisInfo;

        private Graphics g;
        private Database database;
        public MainWindow()
        {
            InitializeComponent();
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

                    if (!database.HasAnalysis(videoFilepath))
                    {
                        aw.FilePath = videoFilepath;
                        aw.ShowDialog();
                        database.Store(videoFilepath, aw.OutputAnalysisFilepath);
                    }

                    analysisInfo = await database.Get(videoFilepath);

                    OpenFile(videoFilepath);
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            g = player.CreateGraphics();
            database = new Database();
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
                    frame.Dispose();
                }
                reader.Close();
            }
        }
    }
}
