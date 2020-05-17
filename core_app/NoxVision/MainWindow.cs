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
        private static readonly int menuStripHeight = 27;
        private static readonly int playerControlsHeight = 50;

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
            player.OnPlaybackEnded += Player_OnPlaybackEnded;
            UpdatePlayerSize();
            UpdatePlayButtonText();
        }

        private void Player_OnPlaybackEnded()
        {
            UpdatePlayer();
            RedrawPlayer();
        }

        private void UpdatePlayerSize()
        {
            player.SetSize(playerControl.ClientSize.Width, playerControl.ClientSize.Height);
            ClientSize = new Size(playerControl.Size.Width, playerControl.Size.Height + menuStripHeight + playerControlsHeight);
            playButton.Location = new Point(20, ClientSize.Height - 40);
        }

        private void UpdatePlayer()
        {
            UpdatePlayButtonText();
            playerTimer.Enabled = player.Playing;
        }

        private void UpdatePlayButtonText()
        {
            playButton.Text = player.Playing ? "Pause" : "Play";
        }

        private void playButton_Click(Object sender, EventArgs e)
        {
            player.OnPlayButtonClick();

            UpdatePlayer();
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
            player.Play();
            playerTimer.Start();
            UpdatePlayButtonText();
        }

        private void VideoplayerStop()
        {
            player.Stop();
            playerTimer.Stop();
        }

        private void RedrawPlayer()
        {
            playerControl.Invalidate();
        }

        private void PlayerTimer_Tick(Object sender, EventArgs e)
        {
            RedrawPlayer();
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
