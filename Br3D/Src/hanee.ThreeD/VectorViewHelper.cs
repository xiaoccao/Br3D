using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    static public class VectorViewHelper
    {
        /// <summary>
        /// 치수를 vectorview에 추가한다.
        /// </summary>
        /// <param name="drawings"></param>
        /// <param name="sheet"></param>
        /// <param name="view"></param>
        /// <param name="dimension"></param>
        static public void AddDimensionByRealPosition(this VectorView view, Drawings drawings, Dimension dimension)
        {
            Block block = drawings.Blocks[view.BlockName];

            double unitsConversionFactor = Utility.GetLinearUnitsConversionFactor(linearUnitsType.Millimeters, drawings.ActiveSheet.Units);

            // Before adding the new dimension to the block, I need to consider both view transformation and scaling according to sheet's units.
            Transformation inverseTransformation = (Transformation)view.GetFullTransformation(drawings.Blocks).Clone();
            inverseTransformation.Invert();
            dimension.TransformBy(inverseTransformation * new Scaling(unitsConversionFactor));

            block.Entities.Add(dimension);
        }
    }
}
