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
    }
}
