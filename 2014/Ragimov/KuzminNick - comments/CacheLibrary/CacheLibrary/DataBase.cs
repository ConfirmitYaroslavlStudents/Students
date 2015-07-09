using System.Collections.Generic;
using System.Linq;

namespace CacheLibrary
{
    public class DataBase<T> : IDataBase<T>
    {
        private Dictionary<string, Element<T>> _dataBase = new Dictionary<string, Element<T>>();

        //I think we should use constructor here instead of this
        public void InitializeDataBase(Dictionary<string, Element<T>> dataBase)
        {
            _dataBase = dataBase;
        }

        public List<string> GetListOfId()
        {
            return _dataBase.Select(element => element.Key).ToList();
        }

        public void AddItem(Element<T> item)
        {
            _dataBase.Add(item.Id, item);
        }

        public T GetItemById(string id)
        {
            return _dataBase[id].Value;
        }

        public void Clear()
        {
            _dataBase.Clear();
        }
    }
}
