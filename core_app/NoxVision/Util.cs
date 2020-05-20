using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoxVision
{
    public static class Util
    {
        public static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                var textData = Encoding.UTF8.GetBytes(text);
                var hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static Bitmap Subregion(Bitmap orig, int sx, int sy, int ex, int ey)
        {
            if (sx < 0)
            {
                sx = 0;
            }

            if (sy < 0)
            {
                sy = 0;
            }

            if (ex >= orig.Width)
            {
                ex = orig.Width - 1;
            }

            if (ey >= orig.Height)
            {
                ey = orig.Height - 1;
            }

            var w = ex - sx;
            var h = ey - sy;

            var output = new Bitmap(w, h);
            for (var x = 0; x < w; x++)
            {
                for (var y = 0; y < h; y++)
                {
                    var pixel = orig.GetPixel(sx + x, sy + y);
                    output.SetPixel(x, y, pixel);
                }
            }

            return output;
        }

        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string LabelToString(Label l)
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
