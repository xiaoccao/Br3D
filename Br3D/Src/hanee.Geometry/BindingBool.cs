using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    public class BindingBool
    {
        public override string ToString()
        {
            return this.text;
        }
        public BindingBool(bool value)
        {
            this.value = value;
        }
        public bool value { get; set; }
        public string text
        {
            get { return value.ToString(); }
            set { this.value = value.ToBool(); }
        }
    }
}
