using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    public class BindingInt
    {
        public override string ToString()
        {
            return this.text;
        }

        public BindingInt(int value)
        {
            this.value = value;
        }
        public int value { get; set; }
        public string text
        {
            get { return value.ToString(); }
            set { this.value = value.ToInt(); }
        }
    }

   
}
