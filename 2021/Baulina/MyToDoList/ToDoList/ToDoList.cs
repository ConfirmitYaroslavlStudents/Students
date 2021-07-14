using System;
using System.Collections;
using System.Collections.Generic;

namespace MyToDoList
{
    [Serializable]
    public class ToDoItem : IEquatable<ToDoItem>
    {
        public bool IsComplete { get; set; }
        public string Description { get; set; }

        public ToDoItem() { }
        public ToDoItem(string description)
        {
            Description = description;
        }

        public ToDoItem(string description, bool isComplete) : this(description)
        {
            IsComplete = isComplete;
        }

        public bool Equals(ToDoItem other)
        {
            return other != null && IsComplete == other.IsComplete && Description.Equals(other.Description);
        }
    }

    public class ToDoList : IEnumerable<ToDoItem>
    {
        private List<ToDoItem> _items;

        public bool IsEmpty => _items.Count == 0;
        public int Count => _items.Count;

        public ToDoList()
        {
            _items = new List<ToDoItem>();
        }

        public ToDoList(IEnumerable<ToDoItem> other)
        {
            _items = new List<ToDoItem>();
            foreach (var item in other)
            {
                _items.Add(new ToDoItem(item.Description, item.IsComplete));
            }
        }
        public ToDoItem this[int i]
        {
            get
            {
                if (i >= Count || i < 0)
                    throw new IndexOutOfRangeException();
                return _items[i];
            }
            set
            {
                if (i >= Count || i < 0)
                    throw new IndexOutOfRangeException();
                _items[i] = value;
            }
        }

        public void Add(string description)
        {
            _items.Add(new ToDoItem(description));
        }

        public void Add(ToDoItem item)
        {
            _items.Add(item);
        }

        private void EditDescription(ToDoItem item, string newDescription)
        {
            item.Description = newDescription;
        }

        public void EditDescription(int index, string newDescription)
        {
            EditDescription(_items[index], newDescription);
        }

        private void MarkAsComplete(ToDoItem item)
        {
            item.IsComplete = true;
        }

        public void MarkAsComplete(int index)
        {
            MarkAsComplete(_items[index]);
        }

        private void Remove(ToDoItem item)
        {
            _items.Remove(item);
        }

        public void Remove(int index)
        {
            Remove(_items[index]);
        }

        public ToDoItem[] ToArray()
        {
            var result = new ToDoItem[Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = _items[i];
            }

            return result;
        }

        public IEnumerator<ToDoItem> GetEnumerator()
        {
            foreach (var item in _items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
