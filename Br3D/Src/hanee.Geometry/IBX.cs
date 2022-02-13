using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    public interface IBX
    {
        void FromElement(hanee.Geometry.Element ele);
        void ToElement(hanee.Geometry.Element ele);
    }
}
