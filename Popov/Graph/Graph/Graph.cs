using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Graph<T> : IEnumerable<KeyValuePair<T, HashSet<T>>>, IEquatable<Graph<T>>
    {
        private readonly Dictionary<T, HashSet<T>> _vertexDictionary;


        public Graph(Dictionary<T, HashSet<T>> setVertex)
        {
            _vertexDictionary = setVertex;
        }

        public Graph(T vertex)
        {
            _vertexDictionary = new Dictionary<T, HashSet<T>> {{vertex, new HashSet<T>()}};
        }

        public Graph()
        {
            _vertexDictionary = new Dictionary<T, HashSet<T>>();
        }


        public int Count
        {
            get { return _vertexDictionary.Count(); }
        }

        public Dictionary<T, HashSet<T>> SetVertex
        {
            get { return _vertexDictionary; }
        }


        public IEnumerator<KeyValuePair<T, HashSet<T>>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<T, HashSet<T>>>) _vertexDictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _vertexDictionary.GetEnumerator();
        }


        public bool Equals(Graph<T> other)
        {
            if (Count != other.Count)
                return false;
            var firstArray = _vertexDictionary.ToArray();
            var secondArray = other._vertexDictionary.ToArray();
            for (int i = 0; i < firstArray.Count(); ++i)
            {
                if ((!firstArray[i].Key.Equals(secondArray[i].Key))
                    || (!firstArray[i].Value.SequenceEqual(secondArray[i].Value)))
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            var temp = obj as Graph<T>;
            return temp != null && temp.Equals(this);
        }


        public void AddVertex(T vertex)
        {
            if (!_vertexDictionary.ContainsKey(vertex))
            {
                _vertexDictionary.Add(vertex, new HashSet<T>());
            }
        }
        
        public void AddVertex(T vertex, HashSet<T> edges)
        {
            if (!_vertexDictionary.ContainsKey(vertex))
            {
                _vertexDictionary.Add(vertex, edges);
                foreach (T temp in edges)
                {
                    _vertexDictionary[temp].Add(vertex);
                }
            }
        }

        public void RemoveVertex(T vertex)
        {
            if (!_vertexDictionary.ContainsKey(vertex)) return;

            foreach (T temp in _vertexDictionary[vertex])
            {
                if (_vertexDictionary[temp].Contains(vertex))
                {
                    _vertexDictionary[temp].Remove(vertex);
                }
            }
            _vertexDictionary.Remove(vertex);
        }

        /// <summary>
        ///     View graph in width
        /// </summary>
        /// <param name="vertex">The top which starts</param>
        /// <param name="action">The action running with each item</param>
        public void ViewWidth(T vertex, Action<T> action)
        {
            RecursionViewWidth(vertex, action, new List<T>());
        }
        
        private void RecursionViewWidth(T vertex, Action<T> action, List<T> vertexList)
        {
            if (!_vertexDictionary.ContainsKey(vertex))
                throw new KeyNotFoundException("Such vertex don't exist");

            var queueVertex = new Queue<T>();

            queueVertex.Enqueue(vertex);
            vertexList.Add(vertex);
            InvokeActionForAllElelmentsInQueue(action, vertexList, queueVertex);

            if (vertexList.Count < _vertexDictionary.Count)
            {
                T remainingVertex = _vertexDictionary.First(x => !vertexList.Contains(x.Key)).Key;
                RecursionViewWidth(remainingVertex, action, vertexList);
            }
        }

        private void InvokeActionForAllElelmentsInQueue(Action<T> action, List<T> vertexList, Queue<T> queueVertex)
        {
            while (queueVertex.Count != 0)
            {
                T top = queueVertex.Dequeue();
                action.Invoke(top);
                foreach (T item in _vertexDictionary[top])
                {
                    if ((vertexList.Contains(item)) || (queueVertex.Contains(item))) continue;
                    queueVertex.Enqueue(item);
                    vertexList.Add(item);
                }
            }
        }

        /// <summary>
        ///     View graph in depth
        /// </summary>
        /// <param name="vertex">The top which starts</param>
        /// <param name="action">The action running with each item</param>
        public void ViewDepth(T vertex, Action<T> action)
        {
            RecursionViewDepth(vertex, action, new List<T>());
        }
        
        private void RecursionViewDepth(T vertex, Action<T> action, List<T> vertexList)
        {
            if (!_vertexDictionary.ContainsKey(vertex))
                throw new KeyNotFoundException("Such vertex don't exist");

            var stackVertex = new Stack<T>();
            stackVertex.Push(vertex);
            vertexList.Add(vertex);
            InvokeActionForAllElelmentsInStack(action, vertexList, stackVertex);

            if (vertexList.Count < _vertexDictionary.Count)
            {
                T remainingVertex = _vertexDictionary.First(x => !vertexList.Contains(x.Key)).Key;
                RecursionViewDepth(remainingVertex, action, vertexList);
            }
        }

        private void InvokeActionForAllElelmentsInStack(Action<T> action, List<T> vertexList, Stack<T> stackVertex)
        {
            while (stackVertex.Count != 0)
            {
                T top = stackVertex.Pop();
                action.Invoke(top);
                foreach (T item in _vertexDictionary[top])
                {
                    if ((vertexList.Contains(item)) || (stackVertex.Contains(item))) continue;
                    stackVertex.Push(item);
                    vertexList.Add(item);
                }
            }
        }


        public bool[,] ToAdjacencyMatrix()
        {
            var matrix = new bool[Count, Count];
            List<T> listKeys = _vertexDictionary.Keys.ToList();
            for (int i = 0; i < listKeys.Count(); ++i)
            {
                foreach (T vertex in _vertexDictionary[listKeys[i]])
                {
                    int j = listKeys.IndexOf(vertex);
                    matrix[i, j] = true;
                    matrix[j, i] = true;
                }
            }
            return matrix;
        }
    }
}