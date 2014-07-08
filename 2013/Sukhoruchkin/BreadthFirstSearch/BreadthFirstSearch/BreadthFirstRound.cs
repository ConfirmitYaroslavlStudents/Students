using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree;
using Queue;

namespace BreadthFirstRound
{
    public class BreadthFirstRound<T>  where T : IComparable
    {
        private Queue<Tree.Node<T>> _queue;
        private Queue<Tree.Node<T>> _parentQueue;
        private Node<T> _nodeInGraph;
        private Tree<T> _tree;
        private bool _nodeIsHaveChildren;
        private Node<T> _parentNode;
        public Action<T> EventAfterGettingIntoNewNode;
        
        public BreadthFirstRound(Tree<T> tree, T inputNode)
        {
            this._queue = new Queue<Node<T>>();
            this._parentQueue = new Queue<Node<T>>();
            this._tree = tree;
            this._nodeInGraph = _tree.SearchElement(inputNode, _tree._root);
        }
        public void Start()
        {
            InitializationNewBreadthFirstRound();
            while (_queue.Count > 0)
            {
                _nodeInGraph = _queue.Dequeue();

                Event();

                NewParentNode();
                AddNewNodeInQuque(_nodeInGraph.Parent);
                AddNewNodeInQuque(_nodeInGraph.Left);
                AddNewNodeInQuque(_nodeInGraph.Right);

                if ((_parentNode != _nodeInGraph) && (_nodeIsHaveChildren))
                    _parentQueue.Enqueue(_nodeInGraph);
                _nodeIsHaveChildren = false;
            }
        }
        private void AddNewNodeInQuque(Node<T> newNode)
        {
            if ((newNode != null) && (newNode != _parentNode))
            {
                _queue.Enqueue(newNode);
                _nodeIsHaveChildren = true;
            }
        }
        private void InitializationNewBreadthFirstRound()
        {
            if (_nodeInGraph == null)
                throw new InvalidOperationException("This element is not present in the tree.");
            _queue.Enqueue(_nodeInGraph);
            _parentNode = _nodeInGraph;
        }
        private void NewParentNode()
        {
            if ((_nodeInGraph.Left != _parentNode) && (_nodeInGraph.Parent != _parentNode) && (_nodeInGraph.Right != _parentNode))
            {
                if (_parentQueue.Count != 0)
                {
                    _parentNode = _parentQueue.Dequeue();
                }
            }
        }
        private void Event()
        {
            if (EventAfterGettingIntoNewNode != null)
                    EventAfterGettingIntoNewNode(_nodeInGraph.Value);
        }
    }
}
