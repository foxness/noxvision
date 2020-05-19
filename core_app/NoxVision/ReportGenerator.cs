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
            DrawStatCircle(g);

            return report;
        }

        private Dictionary<string, double> GetStatCircle()
        {
            var stats = new Dictionary<string, int>();

            var processed = new HashSet<int>();
            foreach (var frame in analysis.frames)
            {
                foreach (var obj in frame.objs)
                {
                    if (processed.Contains(obj.id))
                    {
                        continue;
                    }

                    processed.Add(obj.id);

                    var key = obj.label.ToString();
                    if (!stats.ContainsKey(key))
                    {
                        stats.Add(key, 0);
                    }

                    stats[key] += 1;
                }
            }

            var sum = stats.Values.Sum();
            var output = new Dictionary<string, double>();
            foreach (var key in stats.Keys)
            {
                output.Add(key, (double)stats[key] / sum);
            }

            return output;
        }

        private void DrawStatCircle(Graphics g)
        {
            var stats = GetStatCircle();
            var a = 1;
        }

        private void DrawHeader(Graphics g)
        {
            var logo = Properties.Resources.logoBig;

            g.DrawImage(logo, 20, 20, 350, 350);
            g.DrawString("NoxVision отчет", headerFont, textBrush, 350, 130);
        }

        private void FillBackground(Graphics g)
        {
            g.FillRectangle(backgroundBrush, 0, 0, width, height);
        }
    }
}
