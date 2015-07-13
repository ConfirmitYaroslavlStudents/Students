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
        #endregion

        #region ISetMethods

        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
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

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _tree.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Members

        private Tree<T> _tree;
        #endregion
    }
}
