using System.Collections.Generic;
using System.Linq;
using CacheLibrary;

namespace CacheLibraryWithoutTimers
{
    public class DataBase<T> : IDataBase<T>
    {
        private Dictionary<string, Element<T>> _dataBase = new Dictionary<string, Element<T>>();

        public void InitializeDataBase(Dictionary<string, Element<T>> dataBase)
        {
            _dataBase = dataBase;
        }

        public List<string> GetListOfIdentifiers()
        {
            return _dataBase.Select(element => element.Key).ToList();
        }

        public void AddItem(Element<T> item)
        {
            _dataBase.Add(item.Identifier, item);
        }

        public Element<T> GetItemByIdentifier(string identifier)
        {
            return _dataBase[identifier];
        }

        public void Clear()
        {
            _dataBase.Clear();
        }
    }
}
