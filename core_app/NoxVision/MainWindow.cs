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
            UpdatePlayButton();
            UpdateControlLocations();
        }

        private void Player_OnPlaybackEnded()
        {
            UpdatePlayer();
            RedrawPlayer();
        }

        private void UpdatePlayer()
        {
            UpdatePlayButton();
            playerTimer.Enabled = player.Playing;
        }

        private void UpdatePlayButton()
        {
            playButton.Invalidate();
        }

        private void VideoplayerLoad()
        {
            player.Load(videoFilepath, analysisInfo);
        }

        private void VideoplayerStart()
        {
            player.Play();
            playerTimer.Start();
            UpdatePlayButton();
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

        private void UpdateControlLocations()
        {
            playerControl.Size = new Size(ClientSize.Width, ClientSize.Height - 100);
            playButton.Location = new Point(15, ClientSize.Height - 61);
            player.SetSize(playerControl.ClientSize.Width, playerControl.ClientSize.Height);
        }

        private void MainWindow_Resize(Object sender, EventArgs e)
        {
            UpdateControlLocations();
            RedrawPlayer();
        }

        private void MainWindow_FormClosing(Object sender, FormClosingEventArgs e)
        {
            player.Unload();
        }

        private void playButton_Click(Object sender, EventArgs e)
        {
            player.OnPlayButtonClick();

            UpdatePlayer();
        }

        private void playButton_Paint(Object sender, PaintEventArgs e)
        {
            var img = player.Playing ? Properties.Resources.pause : Properties.Resources.play;
            e.Graphics.DrawImage(img, 0, 0, playButton.ClientSize.Width, playButton.ClientSize.Height);
        }
    }
}
