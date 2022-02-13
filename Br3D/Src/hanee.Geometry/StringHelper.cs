using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    public static class StringHelper
    {
        public static double ToDouble(this string str)
        {
            if (double.TryParse(str, out double val))
                return val;
            return 0;
        }
        public static bool ToBool(this string str)
        {
            if (bool.TryParse(str, out bool val))
                return val;
            return false;
        }

        public static int ToInt(this string str)
        {
            if (int.TryParse(str, out int val))
                return val;
            return 0;
        }

        // 문자열을 count 횟수만큼 반복해서 리턴
        public static string Mutiple(this string str, int count)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < count; ++i)
                sb.Append(str);
            return sb.ToString();
        }
    }
}
