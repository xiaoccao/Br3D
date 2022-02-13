using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace hanee.Geometry
{
    [Serializable]
    [XmlType(TypeName = "KeyValuePair_elements")]
    public struct KeyValuePair_elements<K, V> : ISerializable
    {
        public KeyValuePair_elements(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public KeyValuePair_elements(SerializationInfo info, StreamingContext context)
        {
            Key = (K)info.GetValue("key", typeof(K));
            Value = (V)info.GetValue("value", typeof(V));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("key", Key);
            info.AddValue("value", Value);
        }

        public K Key
        { get; set; }

        public V Value
        { get; set; }

     
    }

    // element를 관리한다.
    [Serializable]
    public class ElementManager : Singleton<ElementManager>, ISerializable
    {
        public List<KeyValuePair_elements<ElementID, Element>> elements { get; set; }

        #region event
        public delegate void BeforeRemoveElement(Element ele);
        public event BeforeRemoveElement BeforeRemoveElementHandler;
        #endregion

        public Environment environment { get; set; }
        public ElementManager()
        {
            elements = new List<KeyValuePair_elements<ElementID, Element>>();
            environment = new Environment();
        }

        public ElementManager(SerializationInfo info, StreamingContext context)
        {
            elements = (List<KeyValuePair_elements<ElementID, Element>>)info.GetValue("elements", typeof(List<KeyValuePair_elements<ElementID, Element>>));
            environment = (Environment)info.GetValue("environment", typeof(Environment));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("elements", elements);
            info.AddValue("environment", environment);
        }

        // deserialize후에 동기화를 한다.
        // deserialize할때 list의 내용이 serialize함수에서 바로 채워지 않는 경우가 있다.(특히 Terrain의 surfaces에의 Mesh가 처음엔 null로 채워짐)
        // 따라서 파일을 완전히 open하고 나서 동기화 해준다.
        public void SyncAfterDeserialize()
        {
            foreach(var ele in elements)
            {
                ele.Value.SyncAfterDeserialize();
            }
        }

        // T 타입의 유일한 Element를 리턴한다.
        // 없으면 만들어서 리턴한다.
        public Element GetUniqueElement<T>() where T : Element 
        {
            var elements = GetAllElements<T>();
            if(elements == null || elements.Count == 0)
            {
                var t = Activator.CreateInstance(typeof(T)) as T;
                AddElement(t);
                return t;
            }

            return elements[0];
        }

        // 같은 종류의 element의 인덱스
        // 없으면 -1
        public int FindIndexByType(Element ele)
        {
            if (ele == null)
                return -1;

            int idx = -1;
            var type = ele.GetType();
            foreach (var tmp in elements)
            {
                if(tmp.Value.GetType() == type)
                {
                    idx++;
                }

                if (tmp.Value == ele)
                    return idx;
            }

            return idx;
        }

        // type이 동일한 index 번째 element를 찾는다.
        public Element FindElementByTypeIndex(Type type, int index)
        {
            int curIdx = 0;
            foreach (var ele in elements)
            {
                if(ele.Value.GetType() == type)
                {
                    if (curIdx == index)
                        return ele.Value;
                    curIdx++;
                }
            }
            return null;
        }


        // type이 동일한 index 번째 element를 찾는다.
        public Element FindElementByIndex<T>(int index)
        {
            int curIdx = 0;
            foreach (var ele in elements)
            {
                if (ele.Value is T)
                {
                    if (curIdx == index)
                        return ele.Value;
                    curIdx++;
                }
            }
            return null;
        }

        // 이름으로 지정된 타입의 element를 찾는다.
        public Element FindElementByName<T>(string name)
        {
            foreach (var ele in elements)
            {
                if (ele.Value is T)
                {
                    if (ele.Value.name == name)
                        return ele.Value;
                }
            }

            return null;
        }

        
        // ID로 element를 찾는다.
        // todo : 빨리 찾을 수 있게 개선해야함
        //        
        public Element FindElementByID(ElementID id)
        {
            if (elements == null)
                return null;
            if (id == null)
                return null;

            var ele = elements.Where(x => x.Key.id == id.id);
            if (ele != null && ele.Count() > 0)
            {
                return ele.First().Value;
            }


            return null;
        }

        public void Clear()
        {
            if (elements == null)
                return;

            elements.Clear();
        }

        public List<Element> GetAllElements<T>() where T : Element
        {
            List<Element> eles = new List<Element>();
            foreach (var ele in elements)
            {
                
                if (ele.Value.GetType() == typeof(T))
                {
                    eles.Add(ele.Value);
                }
            }

            
            return eles;
        }


        public Element GetFirstElement<T>()
        {
            foreach (var ele in elements)
            {
                if (ele.Value is T)
                {
                    return ele.Value;
                }
            }

            return null;
        }

        // 타입별 element의 개수를 리턴한다.
        public int GetCountElement<T>() where T : Element
        {
            List<Element> elements = GetAllElements<T>();
            if (elements == null)
                return 0;

            return elements.Count;
        }

        // new id를 만든다.
        public int MakeNewID()
        {
            int newID = 0;
            for(int i = 0; i < elements.Count; ++i)
            {
                if(newID <= elements[i].Value.id.id)
                {
                    newID = elements[i].Value.id.id + 1;
                }
            }

            return newID;
        }

        // ElementID specifyID= : id를 강제로 지정할때 사용(가급적 사용자제)
        public void AddElement(Element ele, ElementID specifyID=null)
        {
            // 등록할때 id가 만들어 진다.
            if (specifyID == null)
                ele.id.id = MakeNewID();
            else
                ele.id.id = specifyID.id;

            // id 중복 추가 금지
            if (FindElementByID(ele.id) != null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }

            elements.Add(new KeyValuePair_elements<ElementID, Element>(ele.id, ele));
        }

        public void AddElements(Element[] elements)
        {
            foreach (var ele in elements)
            {
                AddElement(ele);
            }
        }

        public void RemoveElements(Element[] eles)
        {
            foreach(var ele in eles)
            {
                RemoveElement(ele);
            }
            
        }

        public void RemoveElement(Element ele)
        {
            if (ele == null)
                return;

            for(int i = 0; i < elements.Count; ++i)
            {
                if(elements[i].Value == ele)
                {
                    if(BeforeRemoveElementHandler != null)
                        BeforeRemoveElementHandler(ele);

                    elements.RemoveAt(i);
                    return;
                }
            }
        }

       
    }
}
