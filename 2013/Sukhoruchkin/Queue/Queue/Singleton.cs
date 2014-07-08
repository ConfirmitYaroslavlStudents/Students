using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Queue
{
    public class Singleton<T> where T : class
    {
        private static T _instance;

        private Singleton()
        {
        }

        private static T CreateInstance()
        {
            ConstructorInfo constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static, null, new Type[0], new ParameterModifier[0]);
            return (T)constructor.Invoke(null);
        }
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = CreateInstance();
                return _instance;
            }
        }
    }
}
