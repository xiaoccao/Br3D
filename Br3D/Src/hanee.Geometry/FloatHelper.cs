using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    static public class FloatHelper
    {
        static public string ToPercentText(this float value, float maxValue)
        {
            if (maxValue == 0)
                return "0";
            return $"{(value / maxValue) * 100:0}";
        }

        static public float FromPercentText(string percentText, float maxValue)
        {
            if (maxValue <= 0)
                return 1;

            if (float.TryParse(percentText, out float percent))
            {
                return maxValue * (percent / 100.0f);
            }

            return 1;
        }
    }
}
