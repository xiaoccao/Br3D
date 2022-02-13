using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Collections.Generic;
using System.Drawing;
using Region = devDept.Eyeshot.Entities.Region;

namespace hanee.Geometry
{
    public static class Drawer
    {
        // 화살표를 그린다.
        static public EntityList DrawArrow2D(Point3D fromPoint, Point3D toPoint, double arrowLength, double arrowThick)
        {
            double len = fromPoint.DistanceTo(toPoint);

            EntityList entities = new EntityList();

            // 수평으로 만들고 돌린다.
            Line line = new Line(new Point3D(0, 0, 0), new Point3D(len, 0, 0));
            line.Color = Color.Red;
            line.ColorMethod = colorMethodType.byEntity;
            entities.Add(line);

            List<Point3D> points = new List<Point3D>();
            points.Add(new Point3D(len, 0, 0));
            points.Add(new Point3D(len - arrowLength, arrowThick, 0));
            points.Add(new Point3D(len - arrowLength, -arrowThick, 0));
            points.Add(new Point3D(len, 0, 0));

            Region r = new Region(new LinearPath(points));
            r.Color = Color.Red;
            r.ColorMethod = colorMethodType.byEntity;
            entities.Add(r);

            entities.Rotate2D(0, 0, (toPoint - fromPoint).AsVector.ToRadian());
            entities.Translate(fromPoint.AsVector);
            return entities;
        }
    }
}
