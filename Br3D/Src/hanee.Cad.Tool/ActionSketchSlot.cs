using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = devDept.Eyeshot.Entities.Point;

namespace hanee.Cad.Tool
{
    public class ActionSketchSlot : ActionSketchBase
    {
        UClick startPoint, endPoint, radiusPoint;
        
        public ActionSketchSlot(Workspace environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }



        double SlotRad(Point2D start, Point2D end, Point2D radial)
        {
            Segment2D seg = new Segment2D(start, end);
            Point2D closest = seg.PointAt(seg.ClosestPointTo(radial));
            double slotRad = radial.DistanceTo(closest);
            return slotRad;
        }

        private CompositeCurve ThreePointsSlot(Point3D start, Point3D end, Point3D radial)
        {
            var design = GetDesign();
            if (!design.SketchManager.Editing)
                return null;
            var plane = design.SketchManager.SketchPlane;
            var startPoint2D = plane.Project(start);
            var endPoint2D = plane.Project(end);
            var radiusPoint2D = plane.Project(radial);
            return ThreePointsSlot(startPoint2D, endPoint2D, radiusPoint2D);
        }


        private CompositeCurve ThreePointsSlot(Point2D start, Point2D end, Point2D radial)
        {
            double slotRad = SlotRad(start, end, radial);
            if (slotRad == 0)
                slotRad = 0.001;
            return CompositeCurve.CreateSlot(
                GetDesign().SketchManager.DrawingPlane,
                start.X,
                start.Y,
                start.DistanceTo(end),
                slotRad,
                (end - start).AsVector.Angle
            );
        }

        protected override void OnMouseMove(devDept.Eyeshot.Workspace vp, MouseEventArgs e)
        {
            if (startPoint == null || point3D == null)
            {
                ActionBase.previewEntity = null;
                return;
            }

            var pt1 = startPoint.Position;
            var pt2 = endPoint == null ? GetCurrentUClick()?.Position : endPoint.Position;
            var pt3 = radiusPoint == null ? GetCurrentUClick()?.Position : radiusPoint.Position;
            ActionBase.previewEntity = ThreePointsSlot(pt1, pt2, pt3);
        }



        public async Task RunAsync()
        {
            StartAction();
            var design = GetDesign() as HDesign;
            var sketchManager = design.SketchManager;

            while (true)
            {
                startPoint = await GetUClick("Start point");
                if (IsCanceled())
                    break;
                endPoint = await GetUClick("End point");
                if (IsCanceled())
                    break;
                radiusPoint = await GetUClick("Radius point");
                if (IsCanceled())
                    break;

                if (!sketchManager.IsValid())
                    break;

                // slot 추가
                double radius = SlotRad(startPoint.Position, endPoint.Position, radiusPoint.Position);
                var slot = sketchManager.AddSlot(startPoint.Position.X, startPoint.Position.Y, startPoint.Position.DistanceTo(endPoint.Position), radius, (endPoint.Position - startPoint.Position).AsVector.Angle);
                

                Circle c1 = slot[3] as Circle;
                Circle c2 = slot[1] as Circle;

                if (startPoint.Entity is Point)
                    sketchManager.CreateJoinConstraint(startPoint.Entity as Point, sketchManager.CenterPoint(c1));

                if (endPoint.Entity is Point)
                    sketchManager.CreateJoinConstraint(endPoint.Entity as Point, sketchManager.CenterPoint(c2));

                startPoint = null;
                endPoint = null;
                radiusPoint = null;

                sketchManager.UpdateAndInvalidate(true);
                design.Invalidate();
            }

            EndAction();
        }
    }
}
