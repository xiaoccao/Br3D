using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    public static class ObjectHelper
    {
        public static double ToDouble(this Object obj)
        {
            if (obj == null)
                return 0;

            return obj.ToString().ToDouble();
        }
    }
}
