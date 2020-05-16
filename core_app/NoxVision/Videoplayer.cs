using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
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
            videoLoaded = true;
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

        public void Update(TimeSpan time)
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

        public void Draw(Graphics g)
        {
            if (!videoLoaded)
            {
                return;
            }

            var frame = reader.ReadVideoFrame();
            if (frame != null)
            {
                g.DrawImage(frame, 0, 0);
                frame.Dispose();

                currentFrame++;
            }
        }
    }
}
