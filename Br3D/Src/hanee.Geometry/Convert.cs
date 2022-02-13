using devDept.Geometry;
using System;
using System.Collections.Generic;


namespace hanee.Geometry
{
    public class Convert
    {
        public static Point3D[] Points2DTo3D(Point2D[] points)
        {
            var points3D = new List<Point3D>();
            foreach (var point in points)
            {
                points3D.Add(point.To3D());
            }

            return points3D.ToArray();
        }

        // degree를 Vector2D로 변환
        public static Vector2D DegreeToVector2D(double degree)
        {
            double rad = Utility.DegToRad(degree);
            return new Vector2D(Math.Cos(rad), Math.Sin(rad));
        }
    }
}
