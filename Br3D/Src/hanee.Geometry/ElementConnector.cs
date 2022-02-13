using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace hanee.Geometry
{
    [Serializable]

    public class ElementConnector : hanee.Geometry.Element
    {
        // long : Bentley.DgnPlatformNET.ElementId는 저장이 되지 않아서 int로 대신함
        public Dictionary<long, hanee.Geometry.ElementID> idMap = null;

        public ElementConnector()
        {
            idMap = new Dictionary<long, hanee.Geometry.ElementID>();
        }

        public ElementConnector(SerializationInfo info, StreamingContext context)
        {
            Serialize(info, false);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Serialize(info, true);
        }

        public override void Serialize(SerializationInfo info, bool serialize)
        {
            base.Serialize(info, serialize);

            idMap = SerializeHelper.Serialize<Dictionary<long, hanee.Geometry.ElementID>>(info, "idMap", idMap, serialize);
        }

        static public ElementConnector Instance
        {
            get
            {
                var elements = ElementManager.Instance.GetAllElements<ElementConnector>();
                if (elements == null || elements.Count == 0)
                {
                    var t = new ElementConnector();
                    ElementManager.Instance.AddElement(t);
                }
                return (ElementConnector)ElementManager.Instance.GetUniqueElement<ElementConnector>();
            }
        }


        static public hanee.Geometry.ElementID FindElementIdByObjectID(long objectID)
        {
            hanee.Geometry.ElementID haneeId;
            if (ElementConnector.Instance.idMap.TryGetValue((long)objectID, out haneeId))
                return haneeId;

            return null;
        }

        // 연결을 끊는다.
        static public void Disconnect(long objectID)
        {
            if (ElementConnector.Instance.idMap.ContainsKey(objectID))
                ElementConnector.Instance.idMap.Remove(objectID);
        }

        // object와 hanee id를 연결한다.
        static public void Connect(long objectID, hanee.Geometry.ElementID elementID)
        {
            ElementConnector.Instance.idMap[objectID] = elementID;
        }

        static public void Connect(long objectID, hanee.Geometry.Element element)
        {
            if (element == null)
                return;

            Connect(objectID, element.id);
        }

        // element에 연결된 정보를 끊는다.
        // 보통은 element가 삭제되기 전에 호출되는 함수임
        public static void Disconnect(Element element)
        {
            if (element == null)
                return;

            // 삭제 대상  key
            var keysToDelete = new List<long>();
            foreach (var id in ElementConnector.Instance.idMap)
            {
                if (id.Value == element.id)
                    keysToDelete.Add(id.Key);
            }

            // 삭제 대상 key를 모두 제거
            foreach (var key in keysToDelete)
            {
                ElementConnector.Instance.idMap.Remove(key);
            }
        }

        // 존재하지 않는 element를 참조하고 있는 key를 제거한다.
        public static void DisconnectAllInvalidElementId()
        {
            // 삭제 대상  key
            var keysToDelete = new List<long>();
            foreach (var id in ElementConnector.Instance.idMap)
            {
                // 없는 element를 참조하고 있다면 연결정보 제거한다.
                if (ElementManager.Instance.FindElementByID(id.Value) == null)
                    keysToDelete.Add(id.Key);
            }

            // 삭제 대상 key를 모두 제거
            foreach (var key in keysToDelete)
            {
                ElementConnector.Instance.idMap.Remove(key);
            }
        }
    }
}
