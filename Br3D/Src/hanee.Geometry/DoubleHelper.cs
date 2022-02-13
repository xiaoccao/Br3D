using devDept.Geometry;
using System;

namespace hanee.Geometry
{
    public static class DoubleHelper
    {
        // d1이 d2보다 큰지?
        public static bool Bigger(this double d1, double d2, double tol)
        {
            if (d1 - d2 > tol)
                return true;

            return false;
        }

        public static bool Equals(this double d1, double d2, double tol)
        {
            if (Math.Abs(d1 - d2) <= tol)
                return true;

            return false;
        }

        public static double ToDegree(this double radians)
        {
            return devDept.Geometry.UtilityEx.RadToDeg(radians);
        }

        public static double ToRadians(this double degree)
        {
            return devDept.Geometry.UtilityEx.DegToRad(degree);
        }

        public static Vector2D ToVector(this double radians)
        {
            return new Vector2D(Math.Cos(radians), Math.Sin(radians));
        }
    }
}
