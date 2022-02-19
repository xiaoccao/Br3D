using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionSketchSlot : ActionBase
    {
        Point3D startPoint, endPoint, radiusPoint;

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

            ActionBase.previewEntity = ThreePointsSlot(startPoint, endPoint == null ? point3D : endPoint, radiusPoint == null ? point3D : radiusPoint);
        }



        public async Task RunAsync()
        {
            StartAction();
            var design = GetDesign();
            var sketchManager = design.SketchManager;
            
            while (true)
            {
                startPoint = await GetPoint3D("Start point");
                if (IsCanceled())
                    break;
                endPoint = await GetPoint3D("End point");
                if (IsCanceled())
                    break;
                radiusPoint = await GetPoint3D("Radius point");
                if (IsCanceled())
                    break;

                if (!sketchManager.IsValid())
                    break;

                // slot 추가
                var plane = sketchManager.SketchPlane;
                var startPoint2D = plane.Project(startPoint);
                var endPoint2D = plane.Project(endPoint);
                var radiusPoint2D = plane.Project(radiusPoint);

                var slot = sketchManager.AddSlot(startPoint2D.X, startPoint2D.Y, startPoint2D.DistanceTo(endPoint2D), SlotRad(startPoint2D, endPoint2D, radiusPoint2D), (endPoint2D - startPoint2D).AsVector.Angle);
                sketchManager.UpdateAndInvalidate(true);

                startPoint = null;
                endPoint = null;
                radiusPoint = null;

                //Circle c1 = slot[3] as Circle;
                //Circle c2 = slot[1] as Circle;

                //if (Clicks[0].Entity is Point)
                //    Design.SketchManager.CreateJoinConstraint((Point)Clicks[0].Entity, Design.SketchManager.CenterPoint(c1));

                //if (Clicks[1].Entity is Point)
                //    Design.SketchManager.CreateJoinConstraint((Point)Clicks[1].Entity, Design.SketchManager.CenterPoint(c2));
            }

            EndAction();
        }
    }
}
