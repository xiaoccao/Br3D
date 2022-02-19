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

namespace hanee.Cad.Tool
{
    public class ActionSketchCircle : ActionBase
    {
        Point3D centerPoint, radiusPoint;

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

            var design = GetDesign();
            if (!design.SketchManager.Editing)
                return;

            var plane = design.SketchManager.SketchPlane;
            var centerPoint2D = plane.Project(centerPoint);
            var radiusPoint2D = plane.Project(radiusPoint == null ? point3D : radiusPoint);
            double radius = centerPoint2D.DistanceTo(radiusPoint2D);
            if (radius == 0)
                radius = 0.001;

            ActionBase.previewEntity = new Circle(plane, centerPoint2D, radius);
        }

        public async Task RunAsync()
        {
            StartAction();
            var design = GetDesign();
            var sketchManager = design.SketchManager;

            while (true)
            {
                centerPoint = await GetPoint3D("Center point");
                if (IsCanceled())
                    break;
                
                radiusPoint = await GetPoint3D("Radius point");
                if (IsCanceled())
                    break;

                if (!sketchManager.IsValid())
                    break;

                // slot 추가
                var plane = sketchManager.SketchPlane;
                var centerPoint2D = plane.Project(centerPoint);
                var radiusPoint2D = plane.Project(radiusPoint);
                double radius = centerPoint2D.DistanceTo(radiusPoint2D);

                var slot = sketchManager.AddCircle(centerPoint2D, radius);
                sketchManager.UpdateAndInvalidate(true);

                centerPoint = null;
                radiusPoint = null;
            }

            EndAction();
        }
    }
}
