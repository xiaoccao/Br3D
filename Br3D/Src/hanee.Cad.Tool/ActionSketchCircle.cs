using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = devDept.Eyeshot.Entities.Point;

namespace hanee.Cad.Tool
{
    public class ActionSketchCircle : ActionSketchBase
    {
        UClick centerPoint, radiusPoint;

        public ActionSketchCircle(Workspace environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Workspace vp, MouseEventArgs e)
        {
            if (centerPoint == null || point3D == null)
            {
                ActionBase.previewEntity = null;
                return;
            }

            if (!design.SketchManager.Editing)
                return;

            var pt1 = centerPoint.Position;
            var pt2 = radiusPoint == null ? GetCurrentUClick().Position : radiusPoint.Position;
            double radius = pt1.DistanceTo(pt2);
            if (radius == 0)
                radius = 0.001;

            ActionBase.previewEntity = new Circle(sketchManager.SketchPlane, pt1, radius);
        }

        public void AddCircle(UClick center, UClick point)
        {
            Circle circle = sketchManager.AddCircle(center.Position, point.Position);

            if (center.Entity != null)
                sketchManager.CreateJoinConstraint(sketchManager.CenterPoint(circle), center.Entity);

            if (point.Entity != null && point.Entity is Point)
                sketchManager.CreateJoinConstraint((Point)point.Entity, circle);
        }

        public async Task RunAsync()
        {
            StartAction();

            while (true)
            {
                centerPoint = await GetUClick("Center point");
                if (IsCanceled())
                    break;
                
                radiusPoint = await GetUClick("Radius point");
                if (IsCanceled())
                    break;

                if (!sketchManager.IsValid())
                    break;

                AddCircle(centerPoint, radiusPoint);
                sketchManager.UpdateAndInvalidate(true);

                centerPoint = null;
                radiusPoint = null;
            }

            EndAction();
        }
    }
}
