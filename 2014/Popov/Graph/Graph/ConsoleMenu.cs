using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Graph
{
    public class ConsoleMenu
    {
        private readonly List<KeyValuePair<string, Action>> _menuList;


        public ConsoleMenu()
        {
            _menuList = new List<KeyValuePair<string, Action>>();
        }

        public ConsoleMenu(List<KeyValuePair<string, Action>> menuList)
        {
            _menuList = menuList;
        }
        
        
        public void ClearList()
        {
            _menuList.Clear();
        }

        public void Show()
        {
            for (var i = 0; i < _menuList.Count; ++i)
            {
                
                Trace.WriteLine(string.Format("{0}. {1}",(i+1), _menuList[i].Key));
            }
        }
       
        public void LaunchCommand(int index)
        {
            --index;
            if ((index >= 0) && (index < _menuList.Count))
            {
                _menuList[index].Value.Invoke();
            }
            else
            {
                throw new IndexOutOfRangeException("Attempt to invoke action at index, which is no exist");
            }
        }

        public void RemoveCommand(int index)
        {
            if ((index > 0) && (index <= _menuList.Count))
            {
                _menuList.RemoveAt(index);
            }
            else
            {
                throw new IndexOutOfRangeException("Attempt to remove element at index, which is no exist");
            }
        }

        public bool ContainsNumberCommande(int index)
        {
            return ((index <= _menuList.Count) && (index >= 1));
        }

        public void AddCommand(string name, Action procedure)
        {
            if ((!String.IsNullOrEmpty(name)) && (procedure != null))
            {
                _menuList.Add(new KeyValuePair<string, Action>(name, procedure));
            }
            else
            {
                throw new NullReferenceException("Attempt to add a null refernce in list");
            }
        }


        public List<Action> MethodList 
        {
            get { return (from pair in _menuList select pair.Value).ToList(); }
        }

        public List<string> NameList
        {
            get { return (from pair in _menuList select pair.Key).ToList(); }
        }

        public int Count
        {
            get { return NameList.Count; }
        }
           
    }
}
