using Accord;
using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NoxVision
{
    public delegate void MyEventHandler();

    class Videoplayer
    {
        public event MyEventHandler OnPlaybackEnded;

        private AnalysisInfo analysisInfo;
        private VideoFileReader reader;
        private DateTime startTime;
        private int currentFrame;
        private bool videoLoaded;

        private int frameX;
        private int frameY;
        private int frameWidth;
        private int frameHeight;

        private int clientWidth;
        private int clientHeight;
        private long frameCount;

        private Pen pen;
        private Font font;
        private Brush brush;

        public int VideoWidth { get; private set; }
        public int VideoHeight { get; private set; }
        public double Framerate { get; private set; }

        public bool Playing { get; private set; }

        public Videoplayer()
        {
            reader = new VideoFileReader();
            videoLoaded = false;
            pen = new Pen(Color.GreenYellow, 2);
            font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular);
            brush = new SolidBrush(Color.White);
            Playing = false;
        }

        public void OnPlayButtonClick()
        {
            if (!videoLoaded)
            {
                return;
            }

            Playing = !Playing;
        }

        public void Load(string videoFilepath, AnalysisInfo analysisInfo)
        {
            this.analysisInfo = analysisInfo;
            reader.Open(videoFilepath);
            VideoWidth = reader.Width;
            VideoHeight = reader.Height;
            Framerate = reader.FrameRate.Value;
            videoLoaded = true;
            currentFrame = 0;
            frameCount = reader.FrameCount;
            CalculateFrameRectangle();
        }

        public void SetSize(int w, int h)
        {
            clientWidth = w;
            clientHeight = h;

            if (videoLoaded)
            {
                CalculateFrameRectangle();
            }
        }

        private (int, int) VideoCoordsToFrame(int x, int y)
        {
            int nx = (int)((double)x / VideoWidth * frameWidth + frameX);
            int ny = (int)((double)y / VideoHeight * frameHeight + frameY);

            return (nx, ny);
        }

        public void Play()
        {
            Playing = true;
        }

        public void Pause()
        {
            Playing = false;
        }

        public void Unload()
        {
            reader.Close();
            reader.Dispose();
            videoLoaded = false;
        }

        public void Stop()
        {
            Playing = false;
            Unload();
        }

        private void CalculateFrameRectangle()
        {
            int x, y, w, h;

            if ((double)VideoWidth / VideoHeight > (double)clientWidth / clientHeight)
            {
                w = clientWidth;
                h = clientWidth * VideoHeight / VideoWidth;

                x = 0;
                y = (clientHeight - h) / 2;
            }
            else
            {
                w = clientHeight * VideoWidth / VideoHeight;
                h = clientHeight;

                x = (clientWidth - w) / 2;
                y = 0;
            }

            frameX = x;
            frameY = y;
            frameWidth = w;
            frameHeight = h;
        }

        private void DrawBox(Graphics g, int startX, int startY, int endX, int endY)
        {
            (int sX, int sY) = VideoCoordsToFrame(startX, startY);
            (int eX, int eY) = VideoCoordsToFrame(endX, endY);

            g.DrawRectangle(pen, sX, sY, eX - sX, eY - sY);
        }

        private void DrawLabeledBox(Graphics g, string label, int startX, int startY, int endX, int endY)
        {
            DrawBox(g, startX, startY, endX, endY);

            (int sX, int sY) = VideoCoordsToFrame(startX, startY);
            g.DrawString(label, font, brush, sX, sY - 20);
        }

        private void DrawFrame(Graphics g, Bitmap frame)
        {
            g.DrawImage(frame, frameX, frameY, frameWidth, frameHeight);

            var cf = analysisInfo.frames[currentFrame];

            foreach (var obj in cf.objs)
            {
                DrawLabeledBox(g, obj.label.ToString(), obj.rect[0], obj.rect[1], obj.rect[2], obj.rect[3]);
            }

            foreach (var face in cf.faces)
            {
                DrawLabeledBox(g, "face", face.rect[0], face.rect[1], face.rect[2], face.rect[3]);
            }
        }

        private void Update()
        {
            if (!Playing)
            {
                return;
            }

            if (currentFrame + 1 >= frameCount)
            {
                Playing = false;
                currentFrame = 0;
                OnPlaybackEnded();
            }
            else
            {
                currentFrame++;
            }
        }

        public void Draw(Graphics g)
        {
            if (!videoLoaded)
            {
                return;
            }

            var frame = reader.ReadVideoFrame(currentFrame);
            if (frame != null)
            {
                DrawFrame(g, frame);
                frame.Dispose();
                Update();
            }
        }
    }
}
