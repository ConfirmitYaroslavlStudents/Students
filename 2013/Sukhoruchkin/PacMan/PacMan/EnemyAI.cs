using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacMan
{
    public class EnemyAI
    {
        public class Node
        {
            public Point Value;
            public Node Left;
            public Node Right;
            public Node Top;
            public Node Down;
            public Node Last;
            public int Color;

            public Node(Point value)
            {
                this.Value = value;
                this.Color = 0;
                this.Last = null;
                this.Left = null;
                this.Right = null;
                this.Top = null;
                this.Down = null;

            }
        }

        private List<Node> _graph;
        private List<Point> _vertexCoordinates;

        public EnemyAI(List<Point> vertexCoordinates)
        {
            this._vertexCoordinates = vertexCoordinates;
            this._graph = CreateGraph();
        }

        private List<Node> CreateGraph()
        {
            var result = new List<Node>();
            foreach (Point point in _vertexCoordinates)
            {
                Point _newVertex = point;
                var _newNode = new Node(_newVertex);
                _newNode.Down = GetNeighbor(_newVertex, new Point(0, 50));
                _newNode.Top = GetNeighbor(_newVertex, new Point(0, -50));
                _newNode.Left = GetNeighbor(_newVertex, new Point(-50, 0));
                _newNode.Right = GetNeighbor(_newVertex, new Point(50, 0));
                result.Add(_newNode);
            }
            return result;
        }
        private Node GetNeighbor(Point _vertexCoordinate, Point offset)
        {
            foreach (Point possibleNeighbor in _vertexCoordinates)
            {
                if (possibleNeighbor == new Point(_vertexCoordinate.X + offset.X, _vertexCoordinate.Y + offset.Y))
                {
                    return new Node(possibleNeighbor);
                }
            }
            return null;
        }
        private Node SearchNode(Point node)
        {
            Node result=new Node(node);
            foreach (Node possibleNode in _graph)
            {
                if (possibleNode.Value == node)
                    result= possibleNode;
            }
            return result;
        }
        public Point GetNextStep(Point firstNodeCoordinate,Point secondNodeCoordinate)
        {
            Node firstNode = SearchNode(firstNodeCoordinate);
            Node secondNodee = SearchNode(secondNodeCoordinate);
            return FindNextStep(firstNode,secondNodee);
        }
        private Point FindNextStep(Node firstNode,Node secondNode)
        {
            Point result = new Point();
            var queue = new Queue<Node>();
            queue.Enqueue(firstNode);
            Node nodeInGraph = new Node(new Point());
            while (queue.Count > 0)
            {
                nodeInGraph = queue.Dequeue();
                nodeInGraph.Color = 2;
                if ((nodeInGraph.Top != null) && (nodeInGraph.Top.Color == 0) )
                {
                    nodeInGraph.Top.Color = 1;
                    nodeInGraph.Top.Last = nodeInGraph;
                    queue.Enqueue(nodeInGraph.Top);
                    if (nodeInGraph.Top.Value == secondNode.Value)
                    {
                        result = CalculateNextStep(nodeInGraph.Top);
                        break;
                    }
                }
                if ((nodeInGraph.Down != null) && (nodeInGraph.Down.Color == 0) )
                {
                    nodeInGraph.Down.Color = 1;
                    nodeInGraph.Down.Last = nodeInGraph;
                    queue.Enqueue(nodeInGraph.Down);
                    if (nodeInGraph.Down.Value == secondNode.Value)
                    {
                        result = CalculateNextStep(nodeInGraph.Down);
                        break;
                    }
                }
                if ((nodeInGraph.Left != null) && (nodeInGraph.Left.Color == 0))
                {
                    nodeInGraph.Left.Color = 1;
                    nodeInGraph.Left.Last = nodeInGraph;
                    queue.Enqueue(nodeInGraph.Left);
                    if (nodeInGraph.Left.Value == secondNode.Value)
                    {
                        result = CalculateNextStep(nodeInGraph.Left);
                        break;
                    }
                }
                if ((nodeInGraph.Right != null) && (nodeInGraph.Right.Color == 0))
                {
                    nodeInGraph.Right.Color = 1;
                    nodeInGraph.Right.Last = nodeInGraph;
                    queue.Enqueue(nodeInGraph.Right);
                    if (nodeInGraph.Right.Value == secondNode.Value)
                    {
                        result = CalculateNextStep(nodeInGraph.Right);
                        break;
                    }
                }
            }
            return result;
        }
        private Point CalculateNextStep(Node guide)
        {
            Point result = new Point(0,0);
            Node temp = new Node(new Point());
            while (temp.Last.Last != null)
            {
                temp = temp.Last;
            }
            result = temp.Value;
            return result;
        }
    }
}
