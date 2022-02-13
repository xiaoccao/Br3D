using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    static public class ByteHelper
    {
        // inverse : true 0이 100%, 255가 0%
        static public string ToPercentText(this byte value, bool inverse=false)
        {
            var percent = (float)(value / 255.0f) * 100;
            if (inverse)
                percent = 100 - percent;
            return $"{percent:0}";
        }

                // inverse : true 0이 100%, 255가 0%

        static public byte FromPercentText(string percentText, bool inverse=false)
        {
            if (float.TryParse(percentText, out float percent))
            {
                var val = (byte)(255.0 * (percent / 100.0f));
                if (inverse)
                    val = (byte)(255 - val); 
                return val;
            }

            return 255;
        }
    }
}
