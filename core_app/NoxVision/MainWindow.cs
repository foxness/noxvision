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
        private Videoplayer player;

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

            player = new Videoplayer();
            UpdatePlayerSize();
        }

        private void UpdatePlayerSize()
        {
            player.SetSize(playerControl.ClientSize.Width, playerControl.ClientSize.Height);
            ClientSize = new Size(playerControl.Size.Width, playerControl.Size.Height + 27);
        }

        private void VideoplayerLoad()
        {
            player.Load(videoFilepath, analysisInfo);
            var width = player.FrameWidth;
            var height = player.FrameHeight;

            playerControl.ClientSize = new Size(width, height);
            UpdatePlayerSize();
        }

        private void VideoplayerStart()
        {
            player.Start();
            playerTimer.Start();
        }

        private void VideoplayerStop()
        {
            player.Stop();
            playerTimer.Stop();
        }

        private void PlayerTimer_Tick(Object sender, EventArgs e)
        {
            playerControl.Invalidate();
        }

        private void playerControl_Paint(Object sender, PaintEventArgs e)
        {
            player.Draw(e.Graphics);
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
