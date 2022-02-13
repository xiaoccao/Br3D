using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    public class SingletonElement<T> : Element where T : Element
    {
        static public T GetInstance()
        {
            return (T)ElementManager.Instance.GetUniqueElement<T>();
        }
    }
}
