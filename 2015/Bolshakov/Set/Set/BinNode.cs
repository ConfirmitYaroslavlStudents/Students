using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetLib
{
    class BinNode<T>
    {
        #region Propertie

        public T Data { get; private set; }

        public int Key { get; private set; }

        public int Height { get; private set; }

        public int BalanceFactor 
        {
            get 
            {
                var leftHeight = LeftChild != null ? LeftChild.Height : 0;
                var rightHeight = RightChild != null ? RightChild.Height : 0;
                return rightHeight - leftHeight;
            } 
            private set {} 
        }

        public BinNode<T> LeftChild { get; set; }

        public BinNode<T> RightChild { get; set; }

        #endregion

        #region Public Methods

        public BinNode(ref T data)
        {
            Data = data;
            Key = Data.GetHashCode();
            Height = 1;
            LeftChild = RightChild = null;
        }

        public void FixHeight()
        {
            var leftHeight = LeftChild != null ? LeftChild.Height : 0;
            var rightHeight = RightChild != null ? RightChild.Height : 0;
            Height = Math.Max(leftHeight, rightHeight)+1;
        }
        #endregion
    }
}
