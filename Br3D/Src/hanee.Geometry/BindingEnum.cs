using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    public class BindingEnum<T> where T : Enum
    {
        public override string ToString()
        {
            return this.text;
        }
        public BindingEnum(T value, string[] items)
        {
            this.items = new List<string>();
            this.items.AddRange(items);
            this.value = value;
        }

        public T value { get; set; }
        public string text
        {
            get
            {
                if (items.Count > (int)(object)value)
                    return items[(int)(object)value];
                return items[0];
            }
            set
            {
                int idx = items.IndexOf(value);
                if (idx > -1)
                    this.value = (T)(object)idx;
            }
        }
        public List<string> items { get; set; }
    }    
}
