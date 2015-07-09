using System.Collections.Generic;
using System.Linq;

namespace CacheLibrary
{
    public class DataBase<T> : IDataBase<T>
    {
        private Dictionary<string, Element<T>> _dataBase = new Dictionary<string, Element<T>>();

        public DataBase()
        { }

        public DataBase(Dictionary<string, Element<T>> dataBase)
        {
            _dataBase = dataBase;
        }

        public List<string> GetListOfId()
        {
            return _dataBase.Select(element => element.Key).ToList();
        }

        public T GetItemById(string id)
        {
            return _dataBase[id].Value;
        }

        public Element<T> GetElementById(string id)
        {
            return _dataBase[id];
        }
    }
}
