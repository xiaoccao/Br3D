using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    static public class Point2DListHelper
    {
        static public List<Point3D> ToPoint3DList(this List<Point2D> points)
        {
            var point3Ds = new List<Point3D>();
            foreach(var pt in points)
            {
                var pt3D = pt.To3D();
                point3Ds.Add(pt3D);
            }
            return point3Ds;
               
        }
    }
}
