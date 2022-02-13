using devDept.Eyeshot;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public class LineTypeHelper
    {
        static public LineType hidden = new LineType("hidden", new float[] { 0.25f, -0.125f });
        static public LineType center = new LineType("center", new float[] { 1.25f, -0.25f, 0.25f, -0.25f });
        static public LineType phantom = new LineType("phantom", new float[] { 1.25f, -.25f, .25f, -.25f, .25f, -.25f });

        static public LineType[] GetAllLineTypes()
        {
            List<LineType> lineTypes = new List<LineType>();
            lineTypes.Add(LineTypeHelper.hidden);
            lineTypes.Add(LineTypeHelper.center);
            lineTypes.Add(LineTypeHelper.phantom);
            return lineTypes.ToArray();
        }
        // 이름과 가장 유사한 linetype을 리턴
        static public LineType GetSimilarLineType(string lineTypeName)
        {
            var lineTypes = LineTypeHelper.GetAllLineTypes();
            foreach (LineType lt in lineTypes)
            {

                if (string.Compare(lineTypeName, lt.Name, true) == 0)
                    return lt;
            }

            return LineTypeHelper.hidden;
        }
    }
}
