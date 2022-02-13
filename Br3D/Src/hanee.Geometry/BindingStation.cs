using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    public class BindingStation
    {
        public double value { get; set; }
        public string text
        {
            get { return StationHelper.ToInput(value); }
            set { this.value = StationHelper.ToSta(value); }
        }

        public string textWithPrefix
        {
            get { return StationHelper.ToDisplay(value); }
            set { this.value = StationHelper.ToSta(value); }
        }
    }
}
