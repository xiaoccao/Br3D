using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionSketchBase : ActionBase
    {
        public ActionSketchBase(Workspace environment) : base(environment)
        {
            HDesign hd = environment as HDesign;
            if (hd != null)
            {
                hd.Snapping.SetActiveObjectSnap(Snapping.objectSnapType.Point, true);
            }
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public HDesign design => GetDesign() as HDesign;
        public SketchManager sketchManager => design?.SketchManager;
        
        public async Task<UClick> GetUClick(string message)
        {
            await GetPoint3D(message);
            return GetCurrentUClick();
        }

        public UClick GetCurrentUClick()
        {
            if (design == null || sketchManager == null)
                return null;

            if (!sketchManager.IsValid())
                return null;

            var pt2D = sketchManager.SketchPlane.Project(point3D);
            Point ent = null;
            if (design.Snapping.CurrentlySnapping && design.Snapping.GetSnap() != null)
                ent = design.Snapping.GetSnap().entity as devDept.Eyeshot.Entities.Point;

            var uclick = new UClick(pt2D, ent, ent == null ? false : true);
            return uclick;
        }
    }
}
