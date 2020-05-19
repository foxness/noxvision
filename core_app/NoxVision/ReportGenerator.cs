using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NoxVision
{
    class ReportGenerator
    {
        private string videoFilepath;
        private AnalysisInfo analysis;

        private static readonly int width = 1000;
        private static readonly int height = 2000;

        private Font headerFont;
        private Font pieFont;
        private Font titleFont;

        private Pen piePen;
        private Pen titlePen;

        private Brush backgroundBrush;
        private Brush textBrush;
        private List<Brush> niceBrushes;

        public ReportGenerator(string videoFilepath, AnalysisInfo analysisInfo)
        {
            this.videoFilepath = videoFilepath;
            analysis = analysisInfo;

            headerFont = new Font("Open Sans", 48, FontStyle.Bold);
            pieFont = new Font("Open Sans", 18, FontStyle.Regular);
            titleFont = new Font("Open Sans", 24, FontStyle.Regular);

            piePen = new Pen(Color.White, 3);
            titlePen = new Pen(Color.FromArgb(147, 126, 243), 4);

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

                    var key = LabelToString(obj.label);
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
            int x = width / 2;
            int y = 750;
            int r = 230;
            int smallR = 150;
            int bigR = 300;
            int titleY = y - (800 - 370);

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
                    category = "остальное";
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

                var str = $"{category} ({percentage:P2})";
                var strSize = g.MeasureString(str, pieFont);

                var direction = Math.Abs(angle) < Math.PI / 2 ? 1 : -1;
                float tx = bx + strSize.Width * direction;
                float ty = by;

                g.DrawLine(piePen, sx, sy, bx, by);
                g.DrawLine(piePen, bx, by, tx, ty);
                
                g.DrawString(str, pieFont, textBrush, bx + (direction == 1 ? 0 : -strSize.Width + 5), by - strSize.Height);
            }

            DrawTitle(g, "Распознанные объекты в видео", titleY);
        }

        private void DrawTitle(Graphics g, string title, int titleY)
        {
            var titleSize = g.MeasureString(title, titleFont);

            g.DrawLine(titlePen, 0, titleY + titleSize.Height / 2, (width - titleSize.Width) / 2 - 20, titleY + titleSize.Height / 2);
            g.DrawString(title, titleFont, textBrush, (width - titleSize.Width) / 2, titleY);
            g.DrawLine(titlePen, (width + titleSize.Width) / 2 - 5, titleY + titleSize.Height / 2, width, titleY + titleSize.Height / 2);
        }

        private void DrawHeader(Graphics g)
        {
            int logosize = 320;

            var logo = Properties.Resources.logoBig;

            g.DrawImage(logo, 0, 0, logosize, logosize);
            g.DrawString("NoxVision отчет", headerFont, textBrush, 350, 120);
        }

        private void FillBackground(Graphics g)
        {
            g.FillRectangle(backgroundBrush, 0, 0, width, height);
        }

        private string LabelToString(Label l)
        {
            //background, aeroplane, bicycle, bird, boat,
            //bottle, bus, car, cat, chair, cow, diningtable,
            //dog, horse, motorbike, person, pottedplant, sheep,
            //sofa, train, tvmonitor

            switch (l)
            {
                case Label.aeroplane: return "самолет";
                case Label.background: return "фон";
                case Label.bicycle: return "велосипед";
                case Label.bird: return "птица";
                case Label.boat: return "лодка";
                case Label.bottle: return "бутылка";
                case Label.bus: return "автобус";
                case Label.car: return "машина";
                case Label.cat: return "кошка";
                case Label.chair: return "стул";
                case Label.cow: return "корова";
                case Label.diningtable: return "стол";
                case Label.dog: return "собака";
                case Label.horse: return "лошадь";
                case Label.motorbike: return "мотоцикл";
                case Label.person: return "человек";
                case Label.pottedplant: return "растение в горшке";
                case Label.sheep: return "овца";
                case Label.sofa: return "диван";
                case Label.train: return "поезд";
                case Label.tvmonitor: return "телевизор";
            }

            return "неизвестно";
        }
    }
}
