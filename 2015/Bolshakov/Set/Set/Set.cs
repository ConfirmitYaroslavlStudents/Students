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

        public Set(IEnumerable<T> other)
        {
            foreach (var item in other)
            {
                _tree.Add(item);
            }
        }

        public override bool Equals(object obj)
        {
            //TODO: Creat fast equals

            if (!(obj is Set<T>))
                return false;
            var otherSet = obj as Set<T>;
            foreach (var item in this)
            {
                if (!otherSet.Contains<T>(item))
                    return false;
            }
            return true;
        }
        #endregion

        #region Iset Methods
        
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
                _tree.Remove(item);
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
                if (!this.Contains<T>(item))
                    return false;
            return true;
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (this.IsSubsetOf(other) && !this.IsSupersetOf(other))
                return true;
            return false;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (this.IsSupersetOf(other) && !this.IsSubsetOf(other))
                return true;
            return false;
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            foreach (var item in this)
                if (other.Contains<T>(item))
                    return true;
            return false;
        }

        public void UnionWith(IEnumerable<T> other)
        {
            foreach (var item in other)
                _tree.Add(item);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            var exceptableSet = new Set<T>(this).Except<T>(other);
            this.Except<T>(exceptableSet);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            var intersection = new Set<T>(this).Intersect<T>(other);
            this.Union<T>(other).Except<T>(intersection);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            if (this.Count != other.Count<T>())
                return false;
            if (this.IsSubsetOf(other) && this.IsSupersetOf(other))
                return true;
            return false;
        }

        void ICollection<T>.Add(T item)
        {
            _tree.Add(item);
        }

        public bool IsReadOnly
        {
            //TODO: Implement real return value
            get { return false; }
        }
        #endregion

        #region Private Members

        private Set(Tree<T> root)
        {
            _tree = root;
        }

        private Tree<T> _tree;
        #endregion
    }
}
