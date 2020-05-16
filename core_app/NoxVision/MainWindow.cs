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
        private Timer playerTimer;
        private Videoplayer vpl;

        private AnalysisWindow aw = new AnalysisWindow();

        private string videoFilepath;
        private AnalysisInfo analysisInfo;

        private Graphics g;
        private Database database;
        public MainWindow()
        {
            InitializeComponent();

            playerTimer = new Timer();
            playerTimer.Interval = 1000 / 120;
            playerTimer.Tick += PlayerTimer_Tick;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            g = playerControl.CreateGraphics();
            database = new Database();

            vpl = new Videoplayer();
            //vpl.Load(videoFilepath, analysisInfo);
        }

        private void VideoplayerLoad()
        {
            vpl.Load(videoFilepath, analysisInfo);
        }

        private void VideoplayerStart()
        {
            vpl.Start();
            playerTimer.Start();
        }

        private void VideoplayerStop()
        {
            vpl.Stop();
            playerTimer.Stop();
        }

        private void PlayerTimer_Tick(Object sender, EventArgs e)
        {
            playerControl.Invalidate();
        }

        private void playerControl_Paint(Object sender, PaintEventArgs e)
        {
            vpl.Draw(e.Graphics);
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

                    VideoplayerLoad();
                    VideoplayerStart();
                }
            }
        }
    }
}
