using System.Collections.Generic;
using System.Globalization;

namespace CacheLib
{
    public interface IStorage<in TKey, out TValue>
    {
        TValue Get (TKey key);
    }
    //PiStorage:IStorage<int,string> -> PiStorage : IStorage<int,string>
    public class PiStorage:IStorage<int,string>
    {
        public string Get(int key)
        {
            return "Pi"; //Teoretically you can find anything you want in "Pi" number
        }
    }

    public class AsteriskStorage : IStorage<int, string>
    {
        //private Dictionary<int, string> _dictionary -> private readonly Dictionary<int, string> _dictionary;
        private Dictionary<int, string> _dictionary;

        public AsteriskStorage()
        {
            _dictionary = new Dictionary<int, string>();
        }
        public string Get(int key)
        {
            if (_dictionary.ContainsKey(key))
            {
                _dictionary[key] += "*";
            }
            else
            {
                _dictionary[key] = key.ToString(CultureInfo.InvariantCulture);
            }
            return _dictionary[key];
        }
    }
}
