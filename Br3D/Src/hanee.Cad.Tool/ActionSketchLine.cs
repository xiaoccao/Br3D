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
    public class ActionSketchLine : ActionSketchBase
    {
        UClick startPoint, endPoint;

        public ActionSketchLine(Workspace environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Workspace vp, MouseEventArgs e)
        {
            if (startPoint == null || point3D == null)
            {
                ActionBase.previewEntity = null;
                return;
            }

            if (!sketchManager.Editing)
                return;

            var pt1 = startPoint.Position;
            var pt2 = endPoint == null ? GetCurrentUClick()?.Position : endPoint.Position;
            if (pt1.Equals(pt2))
                return;

            ActionBase.previewEntity = new Line(design.SketchManager.SketchPlane, pt1, pt2);
        }

        public Line AddLine(UClick begin, UClick end)
        {
            Line line = sketchManager.AddLine(begin.Position, end.Position);

            if (begin.Entity != null)
                sketchManager.CreateJoinConstraint(sketchManager.StartPoint(line), begin.Entity);

            if (end.Entity != null)
                sketchManager.CreateJoinConstraint(sketchManager.EndPoint(line), end.Entity);

            return line;
        }

   

        public async Task RunAsync()
        {
            StartAction();
            var design = GetDesign() as HDesign;
            var sketchManager = design.SketchManager;

            while (true)
            {
                if (startPoint == null)
                {

                    startPoint = await GetUClick("Start point");
                    if (IsCanceled())
                        break;
                }
                

                endPoint = await GetUClick("End point");
                if (IsCanceled())
                    break;

                if (!sketchManager.IsValid())
                    break;

                // slot 추가
                AddLine(startPoint, endPoint);
                sketchManager.UpdateAndInvalidate(true);

                startPoint = null;
                endPoint = null;

            }

            EndAction();
        }
    }
}
