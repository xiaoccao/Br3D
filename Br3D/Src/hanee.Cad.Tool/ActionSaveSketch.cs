using devDept.Eyeshot;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionSaveSketch : ActionBase
    {
        public ActionSaveSketch(Workspace environment) : base(environment)
        {
        }

        public override void Run()
        {
            if (!GetDesign().SketchManager.Editing)
                return;

            GetDesign().SketchManager.EditExit();
        }
    }
}
