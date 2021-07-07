using System;

namespace BinTree
{
    public class BinTree<T> where T : IComparable
    {
        #region Свойства
        public T value
        {
            get;
            private set;
        }
        public BinTree<T> more
        {
            get;
            private set;
        }
        public BinTree<T> less
        {
            get;
            private set;
        }
        #endregion
        #region Переменные
        bool Exist = false;
        #endregion
        #region Методы
        public void Add(T item)
        {
            if (!Exist) 
            { 
                value = item;
                Exist = true;
            }
            else
            {
                int compare = value.CompareTo(item);
                if (compare == 0)
                    return;
                if (value.CompareTo(item) < 0)
                {
                    if (more == null)
                    { 
                        more = new BinTree<T>();
                    }
                    more.Add(item);
                }
                else
                {
                    if (less == null)
                    {
                        less = new BinTree<T>();
                    }
                    less.Add(item);
                }
            }
        }
        public BinTree<T> Find(T item)
        {
            if (!Exist)
                return null;
            int compare = value.CompareTo(item);
            if (compare == 0)
                return this;
            else
            if (compare == -1)
            {
                if (more != null)
                    return more.Find(item);
            }
            else
            {
                if (less != null)
                    return less.Find(item);
            }
            return null;
        }
        public bool Contains(T item)
        {
            if (!Exist)
                return false;
            int compare = value.CompareTo(item);
            if (compare == 0)
                return true;
            else
            if (compare == -1)
            {
                if (more != null)
                    return more.Contains(item);
            }
            else
            {
                if (less != null)
                    return less.Contains(item);
            }
            return false;
        }
        public void Remove(T item)
        {
            if (!Exist)
                return;
            else
            if (value.CompareTo(item) == 0)
            {
                if (less == null)
                {
                    if (more == null)
                    {
                        Exist = false;
                        return;
                    }
                    less = more.less;
                    value = more.value;
                    Exist = more.Exist;
                    more = more.more;
                }
                else
                {
                    if (more == null)
                    {
                        more = less.more;
                        value = less.value;
                        Exist = less.Exist;
                        less = less.less;
                        return;
                    }
                    BinTree<T> Closest = more;
                    while (Closest.less != null)
                        Closest = Closest.less;
                    value = Closest.value;
                    Closest.Exist = false;
                    if(Closest.more != null)
                    {
                        Closest.less = Closest.more.less;
                        Closest.value = Closest.more.value;
                        Closest.Exist = Closest.more.Exist;
                        Closest.more = Closest.more.more;
                    }
                }
            }
            else
            {
                BinTree<T> found = Find(item);
                if(found != null)
                    found.Remove(item);
            }
        }
        #endregion
    }
}
