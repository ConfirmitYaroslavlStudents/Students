using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SetLib
{
    public class Set<T> : ISet<T> where T : IComparable
    {
        private BinaryNode<T> _root;

        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public Set()
        {
            _root = null;
            Count = 0;
        }

        public Set(IEnumerable<T> collection) : this()
        {
            UnionWith(collection);
        }

        public void Add(T item)
        {
            AddIfNotPresent(item);
        }

        bool ISet<T>.Add(T item)
        {
            return AddIfNotPresent(item);
        }

        private bool AddIfNotPresent(T value)
        {
            if (Contains(value))
            {
                return false;
            }

            BinaryNode<T> newNode = new BinaryNode<T>(value);

            if (_root == null)
            {
                _root = newNode;
                Count++;
                return true;
            }

            BinaryNode<T> current = _root;

            if (newNode.Parent == null)
            {
                newNode.Parent = _root;
            }

            while (true)
            {
                bool insertLeftSide = newNode.Value.CompareTo(current.Value) <= 0;

                if (insertLeftSide)
                {
                    if (current.Left == null)
                    {
                        newNode.Parent = current;
                        current.Left = newNode;
                        Count++;
                        return true;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        newNode.Parent = current;
                        current.Right = newNode;
                        Count++;
                        return true;
                    }
                    current = current.Right;
                }
            }
        }

        public bool Contains(T value)
        {
            return Find(value) != null;
        }

        private BinaryNode<T> Find(T value)
        {
            BinaryNode<T> node = _root;
            while (node != null)
            {
                if (node.Value.Equals(value))
                {
                    return node;
                }
                node = (value.CompareTo(node.Value) < 0) ? node.Left : node.Right;
            }

            return null;
        }

        public bool Remove(T value)
        {
            return Remove(Find(value));
        }

        private bool Remove(BinaryNode<T> removeNode)
        {
            if (_root == null || removeNode == null)
            {
                return false;
            }

            if (Count == 1)
            {
                _root = null;
                Count--;
                return true;
            }

            if (removeNode.IsLeaf)
            {
                RebuildTreeAfterDeleteWhenNodeIsLeaf(removeNode);
                Count--;
            }
            else if (removeNode.ChildCount == 1)
            {
                RebuildTreeAfterDeleteWhenNodeHasOneChild(removeNode);
                Count--;
            }
            else
            {
                RebuildTreeAfterDeleteWhenNodeHasTwoChildren(removeNode);
            }

            return true;
        }

        private void RebuildTreeAfterDeleteWhenNodeIsLeaf(BinaryNode<T> removeNode)
        {
            if (removeNode.IsLeft)
            {
                removeNode.Parent.Left = null;
            }
            else
            {
                removeNode.Parent.Right = null;
            }

            removeNode.Parent = null;
        }

        private void RebuildTreeAfterDeleteWhenNodeHasOneChild(BinaryNode<T> removeNode)
        {
            bool wasHead = removeNode == _root;

            if (removeNode.HasLeft)
            {
                removeNode.Left.Parent = removeNode.Parent;

                if (wasHead)
                {
                    _root = removeNode.Left;
                }
                else
                {
                    if (removeNode.IsLeft)
                    {
                        removeNode.Parent.Left = removeNode.Left;
                    }
                    else
                    {
                        removeNode.Parent.Right = removeNode.Left;
                    }
                }
            }
            else
            {
                removeNode.Right.Parent = removeNode.Parent;

                if (wasHead)
                {
                    _root = removeNode.Right;
                }
                else
                {
                    if (removeNode.IsLeft)
                    {
                        removeNode.Parent.Left = removeNode.Right;
                    }
                    else
                    {
                        removeNode.Parent.Right = removeNode.Right;
                    }
                }
            }

            removeNode.Parent = null;
            removeNode.Left = null;
            removeNode.Right = null;
        }

        private void RebuildTreeAfterDeleteWhenNodeHasTwoChildren(BinaryNode<T> removeNode)
        {
            BinaryNode<T> successorNode = removeNode.Left;
            while (successorNode.HasRight)
            {
                successorNode = successorNode.Right;
            }

            removeNode.Value = successorNode.Value;

            Remove(successorNode);
        }

        /// <summary>
        /// Изменяет текущий объект так, чтобы он содержал все элементы, имеющиеся в нем или 
        /// в указанной коллекции либо как в нем, так и в указанной коллекции.
        /// </summary>
        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            foreach (T item in other)
            {
                AddIfNotPresent(item);
            }
        }

        /// <summary>
        /// Изменяет текущий объект так, чтобы он содержал только элементы, 
        /// которые имеются в этом объекте и в указанной коллекции.
        /// </summary>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            ExceptWith(this.Where(item => !other.Contains(item)));
        }

        /// <summary>
        /// Удаляет все элементы в указанной коллекции из текущего объекта.
        /// </summary>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Count == 0)
            {
                return;
            }

            if (other.Equals(this))
            {
                Clear();
                return;
            }

            foreach (T item in other.Where(Contains))
            {
                Remove(item);
            }
        }

        /// <summary>
        /// Изменяет текущий объект так, чтобы он содержал только элементы, которые имеются либо 
        /// в этом объекте, либо в указанной коллекции, но не одновременно в них обоих.
        /// </summary>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            foreach (T item in other)
            {
                if (Contains(item))
                {
                    Remove(item);
                }
                else
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Определяет, является ли объект подмножеством указанной коллекции.
        /// </summary>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Count == 0)
            {
                return true;
            }

            return other.Count() >= Count && this.All(other.Contains);
        }

        /// <summary>
        /// Определяет, является ли объект супермножеством указанной коллекции.
        /// </summary>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (!other.Any())
            {
                return true;
            }

            return other.Count() <= Count && other.All(Contains);
        }

        /// <summary>
        /// Определяет, является ли объект строгим супермножеством указанной коллекции.
        /// </summary>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Count == 0)
            {
                return false;
            }

            return Count > other.Count() && (!other.Any() || IsSupersetOf(other));
        }

        /// <summary>
        /// Определяет, является ли объект строгим подмножеством указанной коллекции.
        /// </summary>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Count == 0)
            {
                return other.Any();
            }

            return Count < other.Count() && IsSubsetOf(other);
        }

        /// <summary>
        /// Определяет, имеются ли общие элементы в текущем объекте и в указанной коллекции.
        /// </summary>
        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return other.Any(Contains);
        }

        /// <summary>
        /// Определяет, содержат ли объект и указанная коллекция одни и те же элементы.
        /// </summary>
        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Count == other.Count() && other.All(Contains);
        }

        public void Clear()
        {
            _root = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<BinaryNode<T>> queueOfNodes = new Queue<BinaryNode<T>>();
            queueOfNodes.Enqueue(_root);

            while (queueOfNodes.Count > 0)
            {
                BinaryNode<T> currentNode = queueOfNodes.Dequeue();
                if (currentNode != null)
                {
                    if (currentNode.HasLeft)
                    {
                        queueOfNodes.Enqueue(currentNode.Left);
                    }
                    if (currentNode.HasRight)
                    {
                        queueOfNodes.Enqueue(currentNode.Right);
                    }

                    yield return currentNode.Value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            int index = arrayIndex;
            foreach (T item in this)
            {
                array[index] = item;
                index++;
            }
        }
    }
}