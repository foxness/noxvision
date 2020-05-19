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

        private static readonly int width = 1000;
        private static readonly int height = 4000;

        private Font headerFont;

        private Brush backgroundBrush;
        private Brush textBrush;

        public ReportGenerator(string videoFilepath, AnalysisInfo analysisInfo)
        {
            this.videoFilepath = videoFilepath;
            analysis = analysisInfo;

            headerFont = new Font("Open Sans", 48, FontStyle.Bold);

            backgroundBrush = new SolidBrush(Color.FromArgb(17, 17, 17));
            textBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        }

        public Bitmap GenerateReport()
        {
            var report = new Bitmap(width, height);
            var g = Graphics.FromImage(report);

            FillBackground(g);
            DrawHeader(g);

            return report;
        }

        private void FillBackground(Graphics g)
        {
            g.FillRectangle(backgroundBrush, 0, 0, width, height);
        }

        private void DrawHeader(Graphics g)
        {
            var logo = Properties.Resources.logoBig;

            g.DrawImage(logo, 20, 20, 350, 350);
            g.DrawString("NoxVision отчет", headerFont, textBrush, 350, 130);
        }
    }
}
