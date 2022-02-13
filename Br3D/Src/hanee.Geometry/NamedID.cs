using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    // ID와 이름을 가지는 클래스
    public class NamedID
    {
        static public NamedID New(string name)
        {
            var namedId = new NamedID();
            namedId.id = Guid.NewGuid().ToString();
            namedId.name = name;
            namedId.description = "";
            return namedId;
        }

        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
