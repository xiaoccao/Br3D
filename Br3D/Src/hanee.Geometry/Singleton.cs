using System;
using System.Reflection;

namespace hanee.Geometry
{
    public abstract class Singleton<T> where T : class
    {
        private static class DummyNested
        {
            static DummyNested()
            {
                try
                {
                    INSTANCE =
                        Activator.CreateInstance(typeof(T), BindingFlags.Instance | BindingFlags.NonPublic, null,
                                                 new object[] { }, null) as T;
                }
                catch (MissingMethodException)
                {
                    INSTANCE = Activator.CreateInstance(typeof(T)) as T;
                }
            }

            public static T INSTANCE;
        }

        public static void Reinitialize(T inst)
        {
            DummyNested.INSTANCE = inst;
        }

        public static T Instance
        {
            get
            {
                return DummyNested.INSTANCE;
            }
        }
    }
}
