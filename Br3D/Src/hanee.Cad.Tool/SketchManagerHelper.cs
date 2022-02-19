using devDept.Eyeshot;

namespace hanee.Cad.Tool
{
    static public class SketchManagerHelper
    {
        static public bool IsValid(this SketchManager sketchManager)
        {
            if (sketchManager.SketchEntity == null || !sketchManager.Editing)
                return false;

            return true;
        }
    }
}
