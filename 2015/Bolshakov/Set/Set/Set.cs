using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetLib
{
    public class Set<T>: ISet<T>
    {
        #region Public Methods

        public Set()
        {
            _tree = new Tree<T>();
        }
        #endregion

        #region Implemented Iset Methods
        
        public bool Add(T item)
        {
           return _tree.Add(item);
        }

        public bool Remove(T item)
        {
            return _tree.Remove(item);
        }

        public bool Contains(T item)
        {
            return _tree.Search(item) != null ? true : false;
        }

        public int Count
        {
            get { return _tree.Count; }
            private set { }
        }

        public void Clear()
        {
            _tree.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _tree.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var index = arrayIndex;
            foreach (var item in this)
            {
                //TODO: Make real copying elements
                array[index] = item;
                index++;
            }
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                _tree.Remove(item);
            }
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            foreach (var item in this)
            {
                if (!other.Contains<T>(item))
                    return false;
            }
            return true;
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                if (!this.Contains<T>(item))
                    return false;
            }
            return true;
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ISetMethods

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        #region Private Members

        private Tree<T> _tree;
        #endregion
    }
}
