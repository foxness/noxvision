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
        private Font pieFont;

        private Pen piePen;

        private Brush backgroundBrush;
        private Brush textBrush;
        private List<Brush> niceBrushes;

        public ReportGenerator(string videoFilepath, AnalysisInfo analysisInfo)
        {
            this.videoFilepath = videoFilepath;
            analysis = analysisInfo;

            headerFont = new Font("Open Sans", 48, FontStyle.Bold);
            pieFont = new Font("Open Sans", 18, FontStyle.Regular);

            piePen = new Pen(Color.White, 3);

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
            int smallR = 150;
            int bigR = 300;

            var stats = GetStatCircle();
            DrawSector(g, niceBrushes[0], x, y, r, 1f);

            var centerAngles = new List<double>();
            var categories = stats.Keys.OrderBy(k => stats[k]).ToList();
            float buildup = 0;

            for (int i = 0; i < categories.Count; i++)
            {
                var category = categories[i];
                var brush = niceBrushes[i + 1];

                var percentage = (float)stats[category];
                DrawSector(g, brush, x, y, r, 1 - (percentage + buildup));
                centerAngles.Add((1 - (buildup + percentage / 2)) * Math.PI * 2 - Math.PI / 2);
                buildup += percentage;
            }

            var othersSum = stats.Keys.Where(k => stats[k] < 0.03).Select(k => stats[k]).Sum();

            for (int i = 0; i < categories.Count + 1; i++)
            {
                double angle;
                double percentage;
                string category;
                if (i == 0)
                {
                    angle = (1 - othersSum) * Math.PI * 2 - Math.PI / 2;
                    percentage = othersSum;
                    category = "others";
                }
                else
                {
                    angle = centerAngles[i - 1];

                    category = categories[i - 1];
                    percentage = stats[category];

                    if (percentage < 0.03)
                    {
                        continue;
                    }
                }
                
                float sx = (float)(x + smallR * Math.Cos(angle));
                float sy = (float)(y + smallR * Math.Sin(angle));
                float bx = (float)(x + bigR * Math.Cos(angle));
                float by = (float)(y + bigR * Math.Sin(angle));

                var direction = Math.Abs(angle) < Math.PI / 2 ? 1 : -1;
                float tx = bx + 100 * direction;
                float ty = by;

                g.DrawLine(piePen, sx, sy, bx, by);
                g.DrawLine(piePen, bx, by, tx, ty);

                var str = $"{category} ({percentage:P2})";
                g.DrawString(str, pieFont, textBrush, bx + (direction == 1 ? 0 : -(str.Length * 12)), by - 40);
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
