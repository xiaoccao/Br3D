using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hanee.Geometry
{
    [Serializable]
    public class ElementID : ISerializableHelper
    {
        public ElementID()
        {
            id = EmptyID();
        }
        public ElementID(int id)
        {
            this.id = id;
        }
        public void Serialize(SerializationInfo info, bool serialize)
        {
            id = SerializeHelper.Serialize<int>(info, "id", id, serialize);
        }
        public ElementID(SerializationInfo info, StreamingContext context)
        {
            Serialize(info, false);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Serialize(info, true);
        }

        // 해당 id의 element를 리턴
        public Element GetElement()
        {
            return ElementManager.Instance.FindElementByID(this);
        }

        public bool IsEmpty()
        {
            return id == ElementID.EmptyID() ? true : false;
        }

        public int id { get; set; }
        static public int EmptyID() => -1;

      
    }
}
