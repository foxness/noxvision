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
        private bool mouseDown;

        private AnalysisWindow aw;
        private SettingsWindow sw;

        private string videoFilepath;
        private AnalysisInfo analysisInfo;

        private Graphics g;
        private Database database;
        private Brush blackBrush;
        private Brush blueBrush;
        public MainWindow()
        {
            InitializeComponent();

            aw = new AnalysisWindow();
            sw = new SettingsWindow();

            mouseDown = false;
            
            blackBrush = new SolidBrush(Color.Black);
            blueBrush = new SolidBrush(Color.Blue);

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
            reportItem.Enabled = true;
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
            track.Invalidate();
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
            track.Location = new Point(80, ClientSize.Height - 60);
            track.Size = new Size(ClientSize.Width - 100, 48);
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

        private void PlayButtonClick()
        {
            player.OnPlayButtonClick();

            UpdatePlayer();
        }

        private void playButton_Click(Object sender, EventArgs e)
        {
            PlayButtonClick();
        }

        private void playButton_Paint(Object sender, PaintEventArgs e)
        {
            var img = player.Playing ? Properties.Resources.pause : Properties.Resources.play;
            e.Graphics.DrawImage(img, 0, 0, playButton.ClientSize.Width, playButton.ClientSize.Height);
        }

        private void track_Paint(Object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            int barW = 7;
            int barH = 15;
            int w = track.ClientSize.Width;
            int h = track.ClientSize.Height;

            //g.FillRectangle(greenBrush, 0, 0, w, h);

            g.FillRectangle(blackBrush, 0, (h - barH) / 2, barW, barH);
            g.FillRectangle(blackBrush, w - barW, (h - barH) / 2, barW, barH);

            g.FillRectangle(blackBrush, 0, (h - barW) / 2, w, barW);

            var p = player.Progress;
            if (p != -1)
            {
                int tickW = 5;
                int tickH = 18;
                g.FillRectangle(blueBrush, (int)(p * w) - tickW / 2, h / 2 - tickH / 2, tickW, tickH);
            }
        }

        private void track_MouseDown(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                SetPlayerProgress(e);
            }
        }

        private void track_MouseUp(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
            }
        }

        private void SetPlayerProgress(MouseEventArgs e)
        {
            var progress = (double)e.X / track.Width;
            if (progress < 0)
            {
                progress = 0;
            }
            else if (progress > 1)
            {
                progress = 1;
            }

            player.SetProgress(progress);
            RedrawPlayer();
        }

        private void track_MouseMove(Object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                SetPlayerProgress(e);
            }
        }

        private void exitItem_Click(Object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsItem_Click(Object sender, EventArgs e)
        {
            sw.ShowDialog();
            player.LoadSettings(new SettingsDb());
        }

        private void aboutItem_Click(Object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        private async void temp()
        {
            var vfp = @"R:\my\drive\sync\things\projects\noxvisioncloud\asd.mp4";
            var ai = await database.Get(vfp);
            var rw = new ReportWindow(@"C:\Users\Rivershy\Desktop\asd.png", vfp, analysisInfo);
            rw.ShowDialog();
        }

        private void reportItem_Click(Object sender, EventArgs e)
        {
            //if (player.Playing)
            //{
            //    PlayButtonClick(); // pause the video for report generation
            //}

            //var sfd = new SaveFileDialog();
            //sfd.Filter = "Png Image|*.png";
            //sfd.Title = "Сохранение отчета";

            //// REMOVE ON PRODUCTION
            //sfd.FileName = @"C:\Users\Rivershy\Desktop\asd.png";

            //sfd.ShowDialog();

            //if (sfd.FileName != "")
            //{
            //    var rw = new ReportWindow(sfd.FileName, videoFilepath, analysisInfo);
            //    rw.ShowDialog();
            //}

            // -------------------------------------------------

            temp();
        }
    }
}
