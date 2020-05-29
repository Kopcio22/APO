using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APO.Utility
{
    //komentarz testowy v2
    class Histogram
    {
        public static Dictionary<Color, int> HistogramMap(Bitmap image)
        {

            var histDict = new Dictionary<Color, int>();

            for (int x = 0; x < image.Width; ++x)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    Color color = image.GetPixel(x, y);
                    if (histDict.ContainsKey(color))
                        histDict[color] = histDict[color] + 1;
                    else
                        histDict.Add(color, 1);
                }
            }
            return histDict;

        }
        public static Size FitSize(Size imageSize)
        {
            int newHeight, newWidth;

            if ((imageSize.Height >= Main.ActiveForm.Size.Height) || (imageSize.Width >= Main.ActiveForm.Size.Width))
            {
                newHeight = (int)(imageSize.Height * 0.5);
                newWidth = (int)(imageSize.Width * 0.5);
            }
            else
            {
                newHeight = imageSize.Height;
                newWidth = imageSize.Width;
            }

            Size newSize = new Size(newWidth, newHeight);

            return newSize;
        }
        public static int[] HistogramLUT(Dictionary<Color, int> histogramMap, string color = "red")
        {
            var pairs = new Dictionary<byte, int>();
            int[] LUT = new int[256];

            switch (color)
            {
                case "red":
                    foreach (var x in histogramMap.Keys)
                    {
                        if (pairs.ContainsKey(x.R))
                            pairs[x.R] += histogramMap[x];
                        else
                            pairs.Add(x.R, histogramMap[x]);
                    }
                    foreach (var x in pairs.Keys)
                    {
                        LUT[x] = pairs[x];
                    }
                    break;

                case "green":
                    foreach (var x in histogramMap.Keys)
                    {
                        if (pairs.ContainsKey(x.G))
                            pairs[x.G] += histogramMap[x];
                        else
                            pairs.Add(x.G, histogramMap[x]);
                    }
                    foreach (var x in pairs.Keys)
                    {
                        LUT[x] = pairs[x];
                    }
                    break;
                case "blue":
                    foreach (var x in histogramMap.Keys)
                    {
                        if (pairs.ContainsKey(x.B))
                            pairs[x.B] += histogramMap[x];
                        else
                            pairs.Add(x.B, histogramMap[x]);
                    }
                    foreach (var x in pairs.Keys)
                    {
                        LUT[x] = pairs[x];
                    }
                    break;
            }
            return LUT;
        }
    }
}
