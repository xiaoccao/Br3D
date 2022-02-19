using devDept.Eyeshot;
using devDept.Geometry;
using hanee.ThreeD;

namespace hanee.Cad.Tool
{
    public class ActionNewSketch : ActionBase
    {
        public ActionNewSketch(Workspace environment) : base(environment)
        {
        }

        public override void Run()
        {
            GetDesign().NewSketch(Plane.XY);
        }
    }
}
