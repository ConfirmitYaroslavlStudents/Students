using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacMan
{
    public class EnemyArtificialIntelligence
    {
        private List<Node> _graph;
        private List<Point> _vertexCoordinates;
        private Point _pacManCoordinate;
        private Node _nodeInGraph;
        private Queue<Node> _queueForBFS;

        public EnemyArtificialIntelligence(List<Point> vertexCoordinates)
        {
            this._vertexCoordinates = vertexCoordinates;
            this._graph = CreateEmptyGraph();
            FillGraph();
        }
        private List<Node> CreateEmptyGraph()
        {
            var result = new List<Node>();
            foreach (Point newNodeValue in _vertexCoordinates)
            {
                result.Add(new Node(newNodeValue));
            }
            return result;
        }
        private void FillGraph()
        {
            foreach (Node node in _graph)
            {
                node.Down = GetNeighbor(new Point(0, GameSettings.CellSize), node);
                node.Top = GetNeighbor(new Point(0, -GameSettings.CellSize), node);
                node.Left = GetNeighbor(new Point(-GameSettings.CellSize, 0), node);
                node.Right = GetNeighbor(new Point(GameSettings.CellSize, 0), node);
            }
        }
        private Node GetNeighbor(Point offset,Node currentNode)
        {
            foreach (Node node in _graph)
            {
                if (currentNode.Value == new Point(node.Value.X - offset.X, node.Value.Y - offset.Y))
                    return node;
            }
            return null;
        }
        private Node SearchNode(Point valueSearchingNode)
        {
            Node result = new Node(valueSearchingNode);
            foreach (Node possibleNode in _graph)
            {
                if (possibleNode.Value == valueSearchingNode)
                    result = possibleNode;
            }
            return result;
        }
        public Point GetNextStep(Point enemyCordinate,Point pacManCoordinate)
        {
            ReturnBasicStatusGraph();
            this._pacManCoordinate = pacManCoordinate;
            Node enemyNode = SearchNode(enemyCordinate);
            return FindNextStep(enemyNode);
        }
        private Point FindNextStep(Node enemyNode)
        {
            Point result = enemyNode.Value;
            _queueForBFS = new Queue<Node>();
            _queueForBFS.Enqueue(enemyNode);
            enemyNode.isFound = true;
            while (_queueForBFS.Count > 0 && result == enemyNode.Value)
            {
                _nodeInGraph = _queueForBFS.Dequeue();
                result = AddNewNodeInQueue(_nodeInGraph.Top, result);
                result = AddNewNodeInQueue(_nodeInGraph.Down, result);
                result = AddNewNodeInQueue(_nodeInGraph.Left, result);
                result = AddNewNodeInQueue(_nodeInGraph.Right, result);
            }
            return result;
        }
        private Point AddNewNodeInQueue(Node node, Point result)
        {
            if ((node != null) && (node.isFound == false))
            {
                node.isFound = true;
                node.Last = _nodeInGraph;
                _queueForBFS.Enqueue(node);
                if (node.Value == _pacManCoordinate)
                {
                    result = CalculateNextStep(node);
                }
            }
            return result;
        }
        private Point CalculateNextStep(Node guide)
        {
            while (guide.Last.Last != null)
            {
                guide = guide.Last;
            }
            return guide.Value;
        }
        private void ReturnBasicStatusGraph()
        {
            foreach (Node node in _graph)
            {
                node.Last = null;
                node.isFound = false;
            }
        }
    }
    class Node
    {
        public Point Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Top { get; set; }
        public Node Down { get; set; }
        public Node Last { get; set; }
        public bool isFound { get; set; }

        public Node(Point value)
        {
            this.Value = value;
            this.Last = null;
            this.Left = null;
            this.Right = null;
            this.Top = null;
            this.Down = null;

        }
    }
}
