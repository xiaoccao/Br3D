using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

namespace hanee.Geometry
{
    // 1. ISerializableHelper 를 상속받은 클래스는 3가지 함수를 재정의 해야 한다.(Serialize, 생성자, GetObjectData)
    //   (1) 클래스의 데이타를 serialize 하는 코드를 작성한다.
    //       : void Serialize(SerializationInfo info, bool serialize);
    //   (2) 생성자 ( load할 때 클래스를 new 하는 용도로 호출됨 )
    //       : void Foo(SerializationInfo info, StreamingContext context) { Serialize(info, false); }
    //   (3) GetObjectData (save할 때 호출됨)
    //       : void GetObjectData(SerializationInfo info, StreamingContext context) { Serialize(info, true); }
    //   참고 : https://blog.naver.com/hileejaeho/222116359741
    // 
    // 2. 주의할점
    //   (1) ISerializableHelper를 상속받는 클래스는 ICloneable을 동시에 상속 받지 말아야 한다.
    //       : 클래스가 List 안으로 들어갈때 불러오기가 안됨.(null로 들어옴)
    public interface ISerializableHelper : ISerializable
    {
        void Serialize(SerializationInfo info, bool serialize);
    }

    static public class SerializeHelper
    {
        static public bool IsExistValue(SerializationInfo info, String key)
        {
            var iter = info.GetEnumerator();
            while(iter.MoveNext())
            {
                if (iter.Name.Equals(key))
                    return true;
            }
            return false;
        }

        static public T Serialize<T>(SerializationInfo info, String key, T val, bool serialize)
        {
            try
            {
                if (serialize)
                {
                    info.AddValue(key, val);
                    return val;
                }
                else
                {
                    if (!IsExistValue(info, key))
                        return val;

                    return (T)info.GetValue(key, typeof(T));
                }
            }
            catch
            {
                return val;
            }
            
        }
    }
}
