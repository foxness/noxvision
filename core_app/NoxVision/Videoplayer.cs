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
    class Videoplayer
    {
        private AnalysisInfo analysisInfo;
        private VideoFileReader reader;
        private DateTime startTime;
        private int currentFrame;
        private bool videoLoaded;

        private int width;
        private int height;

        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public double Framerate { get; private set; }

        public bool Running { get; private set; }

        public Videoplayer()
        {
            reader = new VideoFileReader();
            videoLoaded = false;
        }

        public void Load(string videoFilepath, AnalysisInfo analysisInfo)
        {
            this.analysisInfo = analysisInfo;
            reader.Open(videoFilepath);
            FrameWidth = reader.Width;
            FrameHeight = reader.Height;
            Framerate = reader.FrameRate.Value;
            videoLoaded = true;
        }

        public void SetSize(int w, int h)
        {
            width = w;
            height = h;
        }

        public async void Start()
        {
            currentFrame = 0;
            var startTime = DateTime.Now;
            var previousTime = startTime;

            Running = true;
            while (Running)
            {
                var dt = DateTime.Now - previousTime;
                previousTime += dt;
                Update(dt);
                await Task.Delay(8);
            }
        }

        public void Update(TimeSpan dt)
        {

        }

        public void Unload()
        {
            reader.Close();
            reader.Dispose();
            videoLoaded = false;
        }

        public void Stop()
        {
            Running = false;
            Unload();
        }

        private (int, int) CalculateFrameLocation(int frameWidth, int frameHeight)
        {
            int x = (width - frameWidth) / 2;
            int y = (height - frameHeight) / 2;

            return (x, y);
        }

        public void Draw(Graphics g)
        {
            if (!videoLoaded)
            {
                return;
            }

            var frame = reader.ReadVideoFrame();
            if (frame != null)
            {
                (int x, int y) = CalculateFrameLocation(frame.Width, frame.Height);
                g.DrawImage(frame, x, y);
                frame.Dispose();

                currentFrame++;
            }
        }
    }
}
