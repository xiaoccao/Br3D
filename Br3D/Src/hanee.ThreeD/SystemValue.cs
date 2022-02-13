using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    // Action 동작에 대한 시스템 설정값
    // Action 동작의 기본 설정값을 관리한다.
    public class SystemValue
    {
        public SystemValue()
        {
            DimTextHeight = 3;
            MagnetRange = 100;
            GridSnapOffset = 1;
        }

        public double DimTextHeight
        { get; set; }

        public int MagnetRange
        { get; set; }

        public double GridSnapOffset
        { get; set; }
    }
}
