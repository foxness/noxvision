using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NoxVision
{
    class ReportGenerator
    {
        private static readonly int width = 1000;
        private static readonly int height = 3000;

        private string videoFilepath;
        private AnalysisInfo analysis;

        private Font headerFont;
        private Font pieFont;
        private Font titleFont;

        private Pen piePen;
        private Pen titlePen;
        private Pen borderPen;

        private Brush backgroundBrush;
        private Brush textBrush;
        private List<Brush> niceBrushes;

        private Random random;

        public ReportGenerator(string videoFilepath, AnalysisInfo analysisInfo)
        {
            this.videoFilepath = videoFilepath;
            analysis = analysisInfo;

            random = new Random();

            headerFont = new Font("Open Sans", 48, FontStyle.Bold);
            pieFont = new Font("Open Sans", 18, FontStyle.Regular);
            titleFont = new Font("Open Sans", 24, FontStyle.Regular);

            piePen = new Pen(Color.White, 3);
            titlePen = new Pen(Color.FromArgb(147, 126, 243), 4);
            borderPen = new Pen(Color.FromArgb(150, 224, 218), 5);

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

            (var randomObjImgs, var uniquePersonImgs) = AnalyzeVideo();

            FillBackground(g);
            DrawHeader(g);
            DrawStatCircle(g);
            DrawObjectStripe(g, randomObjImgs);
            DrawPeople(g, uniquePersonImgs);

            return report;
        }

        private bool OneContainsTwo(int sx1, int sy1, int ex1, int ey1, int sx2, int sy2, int ex2, int ey2)
        {
            return sx1 <= sx2 && sy1 <= sy2 && ex1 >= ex2 && ey1 >= ey2;
        }

        private List<Tuple<int, List<int>>> GetUniquePersonRects()
        {
            var hashsets = analysis.frames.Select(f => f.faces.Select(face => face.cluster).ToHashSet()).ToList();
            var clusters = new HashSet<int>();
            foreach (var hashset in hashsets)
            {
                clusters.UnionWith(hashset);
            }

            if (clusters.Contains(-1))
            {
                clusters.Remove(-1);
            }

            var clusterRects = new Dictionary<int, List<Tuple<int, List<int>>>>();
            foreach (var cluster in clusters)
            {
                var rects = new List<Tuple<int, List<int>>>();
                for (int i = 0; i < analysis.frames.Count; i++)
                {
                    foreach (var face in analysis.frames[i].faces)
                    {
                        if (face.cluster != cluster)
                        {
                            continue;
                        }

                        var rectFound = false;

                        int fSx = face.rect[0];
                        int fSy = face.rect[1];
                        int fEx = face.rect[2];
                        int fEy = face.rect[3];

                        foreach (var obj in analysis.frames[i].objs)
                        {
                            int oSx = obj.rect[0];
                            int oSy = obj.rect[1];
                            int oEx = obj.rect[2];
                            int oEy = obj.rect[3];

                            if (OneContainsTwo(oSx, oSy, oEx, oEy, fSx, fSy, fEx, fEy))
                            {
                                var rect = new List<int>();
                                rect.Add(oSx);
                                rect.Add(oSy);
                                rect.Add(oEx);
                                rect.Add(oSx);

                                var t = Tuple.Create(i, rect);
                                rects.Add(t);
                                rectFound = true;
                                break;
                            }
                        }

                        if (rectFound)
                        {
                            break;
                        }
                    }
                }

                clusterRects.Add(cluster, rects);
            }

            var uniquePersonRects = new List<Tuple<int, List<int>>>();
            foreach (var cluster in clusterRects.Keys)
            {
                var samePersonRects = clusterRects[cluster];
                var randRect = samePersonRects[random.Next(samePersonRects.Count)];
                uniquePersonRects.Add(randRect);
            }

            return uniquePersonRects;
        }

        private void DrawPeople(Graphics g, List<Bitmap> uniquePersonImgs)
        {
            const int titleY = 1700;
            const int startY = titleY + 200;
            const int peoplePerRow = 5;
            const int marginH = 100;
            const int rowDist = 300;
            const int maxWidth = 150;

            int distBetweenPeople = (width - marginH * 2 - maxWidth * peoplePerRow) / (peoplePerRow - 1);

            DrawTitle(g, $"На видео было найдено {uniquePersonImgs.Count} человек(а)", titleY);

            int i = 0;
            for (int y = 0; true; y++)
            {
                if (i == uniquePersonImgs.Count)
                {
                    break;
                }

                for (int x = 0; x < peoplePerRow; x++)
                {
                    if (i == uniquePersonImgs.Count)
                    {
                        break;
                    }

                    var img = uniquePersonImgs[i];
                    int w = maxWidth;
                    int h = img.Height * maxWidth / img.Width;

                    int xx = marginH + x * (maxWidth + distBetweenPeople);
                    int yy = startY - h / 2 + y * rowDist;

                    g.DrawImage(img, xx, yy, w, h);
                    i++;
                }
            }
        }

        // aka Get Random Object Images and Unique People Images
        private (List<Bitmap>, List<Bitmap>) AnalyzeVideo()
        {
            const int randomImgCount = 30;

            var objCount = analysis.frames.Select(f => f.objs.Count).Sum();
            var randImgIds = new HashSet<int>();

            while (randImgIds.Count < randomImgCount)
            {
                var id = random.Next(objCount);
                randImgIds.Add(id);
            }

            var uniquePersonRects = GetUniquePersonRects();
            var uniquePersonImgs = new List<Bitmap>();

            var reader = new VideoFileReader();
            reader.Open(videoFilepath);

            var randomObjectImgs = new List<Bitmap>();

            int currentId = 0;
            var framecount = reader.FrameCount;
            for (int i = 0; i < framecount; i++)
            {
                if (i % 10 == 0)
                {
                    Console.WriteLine($"frame {i + 1}/{framecount} ({(i + 1) / (double)framecount:P2})");
                }

                var frame = reader.ReadVideoFrame(i);

                foreach (var uniquePersonRect in uniquePersonRects)
                {
                    if (uniquePersonRects.Count == uniquePersonImgs.Count)
                    {
                        break;
                    }

                    if (uniquePersonRect.Item1 == i)
                    {
                        var rect = uniquePersonRect.Item2;
                        var upi = Util.Subregion(frame, rect[0], rect[1], rect[2], rect[3]);

                        uniquePersonImgs.Add(upi);
                    }
                }

                foreach (var obj in analysis.frames[i].objs)
                {
                    if (randomObjectImgs.Count == randImgIds.Count)
                    {
                        break;
                    }

                    if (randImgIds.Contains(currentId))
                    {
                        var img = Util.Subregion(frame, obj.rect[0], obj.rect[1], obj.rect[2], obj.rect[3]);
                        randomObjectImgs.Add(img);
                    }

                    currentId++;
                }

                if (randomObjectImgs.Count == randImgIds.Count && uniquePersonRects.Count == uniquePersonImgs.Count)
                {
                    break;
                }

                frame.Dispose();
            }

            reader.Dispose();

            uniquePersonImgs.Shuffle(random);
            randomObjectImgs.Shuffle(random);

            return (randomObjectImgs, uniquePersonImgs);
        }

        private void DrawObjectStripe(Graphics g, List<Bitmap> randomObjImgs)
        {
            const int stripeH = 500;

            var imgs = randomObjImgs;

            var temp = new Bitmap(width, stripeH);
            var og = Graphics.FromImage(temp);
            
            for (int i = 0; i < 100; i++)
            {
                int cx = random.Next(temp.Width);
                int cy = random.Next(temp.Height);
                int j = random.Next(imgs.Count);

                var img = imgs[j];
                var x1 = cx - img.Width / 2;
                var y1 = cy - img.Height / 2;

                var x2 = x1 + img.Width;
                var y2 = y1;

                var x3 = x2;
                var y3 = y2 + img.Height;

                var x4 = x1;
                var y4 = y3;

                og.DrawImage(img, x1, y1);

                og.DrawLine(borderPen, x1, y1, x2, y2);
                og.DrawLine(borderPen, x2, y2, x3, y3);
                og.DrawLine(borderPen, x3, y3, x4, y4);
                og.DrawLine(borderPen, x1, y1, x4, y4);
            }

            const int stripeY = 1100;
            const int slopeH = 80;

            og.Dispose();
            g.DrawImage(temp, 0, stripeY);
            temp.Dispose();
            
            var points = new Point[3];
            points[0] = new Point(0, stripeY);
            points[1] = new Point(width, stripeY);
            points[2] = new Point(0, stripeY + slopeH);

            g.FillPolygon(backgroundBrush, points);

            points[0] = new Point(0, stripeY + stripeH);
            points[1] = new Point(width, stripeY + stripeH);
            points[2] = new Point(width, stripeY + stripeH - slopeH);

            g.FillPolygon(backgroundBrush, points);
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
