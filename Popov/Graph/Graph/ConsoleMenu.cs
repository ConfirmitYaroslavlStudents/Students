using System;
using System.Collections.Generic;
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


        /// <summary>
        /// Add new Command into Menu
        /// </summary>
        /// <param name="name">Name commande</param>
        /// <param name="procedure">It is delegate, which invoke by this index</param>
        public void AddCommand(string name, Action procedure)
        {
            if ((!String.IsNullOrEmpty(name)) && (procedure != null))
            {
                _menuList.Add(new KeyValuePair<string, Action>(name,procedure));
            }
            else
            {
                throw new NullReferenceException("Attempt to add a null refernce in list");
            }
        }

        /// <summary>
        /// Remove Commande into Menu
        /// </summary>
        /// <param name="index">Index into list</param>
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

        /// <summary>
        /// Clear the list menu
        /// </summary>
        public void ClearList()
        {
            _menuList.Clear();
        }

        public void Show()
        {
            for (var i = 0; i < _menuList.Count; ++i)
            {
                Console.WriteLine("{0}. {1}",(i+1), _menuList[i].Key);
            }
        }

        /// <summary>
        /// Invoke delegate index
        /// </summary>
        /// <param name="index">Number into menu</param>
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
