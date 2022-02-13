using devDept.Eyeshot;
using System.Drawing;

namespace hanee.Geometry
{
    static public class LegendHelper
    {
        public static Color GetColorByValue(this Legend legend, double value)
        {
            int ColorTableLen = legend.ColorTable.Length;
            for (int c = 0; c < ColorTableLen; c++)
            {
                if (value <= legend.Values[c + 1])
                {
                    return Color.FromArgb(legend.ColorTable[c].R, legend.ColorTable[c].G, legend.ColorTable[c].B);
                }
            }

            return Color.White;
        }
    }
}
