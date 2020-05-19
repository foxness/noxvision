using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoxVision
{
    class ReportGenerator
    {
        private string videoFilepath;
        private AnalysisInfo analysis;

        private Brush backgroundBrush;

        public ReportGenerator(string videoFilepath, AnalysisInfo analysisInfo)
        {
            this.videoFilepath = videoFilepath;
            analysis = analysisInfo;

            backgroundBrush = new SolidBrush(Color.FromArgb(17, 17, 17));
        }

        public Bitmap GenerateReport()
        {
            int width = 1000;
            int height = 4000;

            var report = new Bitmap(width, height);
            var g = Graphics.FromImage(report);

            g.FillRectangle(backgroundBrush, 0, 0, width, height);

            return report;
        }
    }
}
