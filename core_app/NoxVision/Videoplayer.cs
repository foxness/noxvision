using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoxVision
{
    class Videoplayer
    {
        private AnalysisInfo analysisInfo;
        private VideoFileReader reader;
        public bool Running { get; private set; }

        public Videoplayer()
        {
            reader = new VideoFileReader();
        }

        public void Load(string videoFilepath, AnalysisInfo analysisInfo)
        {
            this.analysisInfo = analysisInfo;
            reader.Open(videoFilepath);
        }

        public async void Start()
        {
            Running = true;

            var previousTime = DateTime.Now;

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
        }

        public void Stop()
        {
            Running = false;
            videoplayer.Unload();
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < reader.FrameCount; i++)
            {
                var frame = reader.ReadVideoFrame();
                g.DrawImage(frame, 0, 0);
                frame.Dispose();
            }
        }
    }
}
