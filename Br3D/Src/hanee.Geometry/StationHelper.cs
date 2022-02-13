using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    public class StationHelper
    {
        public const double fbSectionGap = 0.00001;
        /// <summary>
        /// station을 출력용으로 변환
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        static public string ToDisplay(double station, bool includePrefix=true)
        {
            int km = (int)(station / 1000);
            double m = station - (km * 1000);
            string prefix = includePrefix ? "STA." : "";
            return $"{prefix}{km.ToString("0")}+{m.ToString("000.0000")}";
        }

        static public string ToInput(double station)
        {
            return ToDisplay(station, false);
        }

        static public string ToDraw(double station)
        {
            return ToDisplay(station, false);
        }


        // station을 키로 변경해서 리턴
        static public string ToKey(double station)
        {
            return station.ToString("0.00000");
        }

        // 키를 station으로 변경
        public static bool ToSta(string key, out double sta)
        {
            // 문자에 +가 있으면 제거
            string tmp = key.Replace("+", "");
            // 문자에 STA.이 있으면 제거
            tmp = tmp.Replace("STA.", "");


            if (double.TryParse(tmp, out sta))
                return true;

            return false;
        }

        public static double ToSta(string key)
        {
            double sta;
            if (ToSta(key, out sta))
                return sta;

            return sta;
        }
    }
}
