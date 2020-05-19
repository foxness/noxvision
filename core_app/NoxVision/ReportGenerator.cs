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
        private List<Brush> niceBrushes;

        public ReportGenerator(string videoFilepath, AnalysisInfo analysisInfo)
        {
            this.videoFilepath = videoFilepath;
            analysis = analysisInfo;

            headerFont = new Font("Open Sans", 48, FontStyle.Bold);

            backgroundBrush = new SolidBrush(Color.FromArgb(17, 17, 17));
            textBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

            niceBrushes = new List<Brush>();
            niceBrushes.Add(new SolidBrush(Color.FromArgb(14, 105, 16)));
            niceBrushes.Add(new SolidBrush(Color.FromArgb(255, 67, 0)));
            niceBrushes.Add(new SolidBrush(Color.FromArgb(101, 21, 127)));
            niceBrushes.Add(new SolidBrush(Color.FromArgb(255, 189, 0)));
            niceBrushes.Add(new SolidBrush(Color.FromArgb(255, 0, 81)));
            niceBrushes.Add(new SolidBrush(Color.FromArgb(143, 155, 249)));
            niceBrushes.Add(new SolidBrush(Color.FromArgb(191, 118, 210)));
            niceBrushes.Add(new SolidBrush(Color.FromArgb(194, 75, 79)));
            niceBrushes.Add(new SolidBrush(Color.FromArgb(102, 104, 191)));
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

        private void DrawSector(Graphics g, Brush b, int x, int y, int radius, float percentage)
        {
            g.FillPie(b, x - radius, y - radius, radius * 2, radius * 2, -90, percentage * 360);
        }

        private void DrawStatCircle(Graphics g)
        {
            int x = 500;
            int y = 650;
            int r = 230;

            var stats = GetStatCircle();
            DrawSector(g, niceBrushes[0], x, y, r, 1f);

            var categories = stats.Keys.OrderBy(k => stats[k]);
            int i = 1;
            float buildup = 0;
            foreach (var category in categories)
            {
                var percentage = (float)stats[category];
                DrawSector(g, niceBrushes[i], x, y, r, 1 - (percentage + buildup));
                buildup += percentage;
                i++;
            }
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
