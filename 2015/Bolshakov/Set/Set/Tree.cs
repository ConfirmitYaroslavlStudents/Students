using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetLib
{
    public class Tree<T>
    {
        #region Properties

        public int Count { get; private set; }
        #endregion

        #region Public Methods
        
        public Tree()
        {
            _root = null;
            Count = 0;
            _treeModified = true;
        }

        public bool Add(T data)
        {
            bool result = false;
            if(_root != null)
            {
                bool modified;
                _root = Insert<T>(_root, ref data,out modified);
                if (modified)
                {
                    _treeModified = result = true;
                    Count++;
                }
            }
            else
            {
                _root = new BinNode<T>(ref data);
                Count++;
                _treeModified = true;
            }
            return result;
        }

        public bool Remove(T data)
        {
            bool modified;
            _root = Delete<T>(_root,ref data, out modified);
            if (modified)
            {
                Count--;
                _treeModified = true;
            }
            return modified;
        }

        public BinNode<T> Search(T data)
        {
            return Find<T>(_root,ref data);
        }
        #endregion

        #region Private Static methods

        private static BinNode<Q> Delete<Q>(BinNode<Q> node,ref Q data,out bool treeModified)
        {
            treeModified = false;
            if (node == null)
                return null;
            var dataKey = data.GetHashCode();
            if(dataKey<node.Key)
                node.LeftChild = Delete<Q>(node.LeftChild,ref data,out treeModified);
            if(dataKey>node.Key)
                node.RightChild = Delete<Q>(node.RightChild, ref data,out treeModified);
            else
            {
                if (data.Equals(node.Data))
                {
                    treeModified = true;
                    var l = node.LeftChild;
                    var r = node.RightChild;
                    if (r == null)
                        return l;
                    var min = FindMin<Q>(r);
                    min.RightChild = RemoveMin<Q>(r);
                    min.LeftChild = l;
                    return Balance<Q>(min);
                }
                return node;
            }
            return Balance<Q>(node);
        }

        private static BinNode<Q> FindMin<Q>(BinNode<Q> p)
        {
            return p.LeftChild == null ? p : FindMin<Q>(p.LeftChild);
        }

        private static BinNode<Q> RemoveMin<Q>(BinNode<Q> p)
        {
            if (p.LeftChild == null)
                return p.RightChild;
            p.LeftChild = RemoveMin<Q>(p.LeftChild);
            return Balance<Q>(p);
        }

        private static BinNode<Q> Insert<Q>(BinNode<Q> node,ref Q data,out bool treeModified)
        {
            if (node == null)
            {
                treeModified = true;
                return new BinNode<Q>(ref data);
            }
            var newNodeKey = data.GetHashCode();
            if (node.Key > newNodeKey)
                node.LeftChild = Insert<Q>(node.LeftChild, ref data, out treeModified);
            else if (node.Key < newNodeKey)
                node.RightChild = Insert<Q>(node.RightChild, ref data, out treeModified);
            else
                treeModified = false;
            return Balance<Q>(node);
        }

        private static BinNode<Q> RightTurn<Q>(BinNode<Q> p)
        {
            var q = p.LeftChild;

            p.LeftChild = q.RightChild;
            q.RightChild = p;
            p.FixHeight();
            q.FixHeight();

            return q;
        }

        private static BinNode<Q> LeftTurn<Q>(BinNode<Q> p)
        {
            var q = p.RightChild;

            p.RightChild = q.LeftChild;
            q.LeftChild = p;
            p.FixHeight();
            q.FixHeight();

            return q;
        }

        private static BinNode<Q> Balance<Q>(BinNode<Q> p)
        {
            p.FixHeight();
            if(p.BalanceFactor==2)
            {
                if (p.RightChild.BalanceFactor < 0)
                    p.RightChild = RightTurn<Q>(p.RightChild);
                p = LeftTurn<Q>(p);
            }
            if(p.BalanceFactor==-2)
            {
                if (p.LeftChild.BalanceFactor > 0)
                    p.LeftChild = LeftTurn<Q>(p.LeftChild);
                p = RightTurn<Q>(p);
            }
            return p;
        }

        private static BinNode<Q> Find<Q>(BinNode<Q> p, ref Q data)
        {
            if (p == null)
                return null;
            var dataKey = data.GetHashCode();
            if (dataKey < p.Key)
                return Find<Q>(p.LeftChild, ref data);
            else if (dataKey > p.Key)
                return Find<Q>(p.RightChild, ref data);
            return p;
        }
        #endregion

        #region Private members

        private BinNode<T> _root;

        private bool _treeModified;
	    #endregion
    }
}
