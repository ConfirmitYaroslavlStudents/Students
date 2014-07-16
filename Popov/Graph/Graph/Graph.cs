using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Graph<T> : IEnumerable<KeyValuePair<T, HashSet<T>>>
    {

        private readonly Dictionary<T, HashSet<T>> _vertexDictionary;


        /// <param name="setVertex"> Set vertex with their edges </param>
        public Graph(Dictionary<T, HashSet<T>> setVertex)
        {
            _vertexDictionary = setVertex;
        }

        /// <param name="vertex"> isolated vertex </param>
        public Graph(T vertex)
        {
            _vertexDictionary = new Dictionary<T, HashSet<T>> { { vertex, new HashSet<T>() } };
        }

        public Graph()
        {
            _vertexDictionary = new Dictionary<T, HashSet<T>>();
        }

        public void AddVertex(T vertex)
        {
            if (!_vertexDictionary.ContainsKey(vertex))
            {
                _vertexDictionary.Add(vertex, new HashSet<T>());
            }
        }

        /// <summary>
        /// Add vertex to the graph
        /// </summary>               
        public void AddVertex(T vertex, HashSet<T> edges)
        {
            if (!_vertexDictionary.ContainsKey(vertex))
            {
                _vertexDictionary.Add(vertex, edges);
                foreach (var temp in edges)
                {
                    _vertexDictionary[temp].Add(vertex);
                }
            }

        }

        /// <summary>
        /// Remove vertex of the graph
        /// </summary>           
        public void RemoveVertex(T vertex)
        {
            if (!_vertexDictionary.ContainsKey(vertex)) return;

            foreach (var temp in _vertexDictionary[vertex])
            {
                if (_vertexDictionary[temp].Contains(vertex))
                {
                    _vertexDictionary[temp].Remove(vertex);
                }
            }
            _vertexDictionary.Remove(vertex);
        }

        /// <summary>
        /// View graph in width and showing progress
        /// </summary>
        /// <param name="vertex">The top which starts</param>
        public void ViewWidth(T vertex)
        {
            if (_vertexDictionary.ContainsKey(vertex))
            {
                Console.WriteLine();
                Console.Write("View in width :  ");

                var queueVertex = new Queue<T>();
                var vertexList  = new List<T>();
                var flagInsertSpace = false;
                queueVertex.Enqueue(vertex);
                vertexList.Add(vertex);
                Console.Write(vertex+" : ");

                while (queueVertex.Count != 0)
                {
                    var top = queueVertex.Dequeue();
                    foreach (var item in _vertexDictionary[top])
                    {
                        if ((!vertexList.Contains(item)) && (!queueVertex.Contains(item)))
                        {
                            queueVertex.Enqueue(item);
                            vertexList.Add(item);
                            Console.Write(item + " ");
                            flagInsertSpace = true;
                        }
                    }

                    if (!flagInsertSpace) continue;
                    Console.Write(" : ");
                    flagInsertSpace = false;
                }
                Console.WriteLine();
            }
            else
            {
                throw new KeyNotFoundException("Such vertex don't exist");
            }
        }

        /// <summary>
        /// View graph in depth and showing progress
        /// </summary>
        /// <param name="vertex">The top which starts</param>
        public void ViewDepth(T vertex)
        {
            if (_vertexDictionary.ContainsKey(vertex))
            {
                var stackVertex = new Stack<T>();
                var vertexList = new List<T>();
                var flagInsertSpace = false;
                stackVertex.Push(vertex);
                vertexList.Add(vertex);

                Console.WriteLine();
                Console.Write("View in depth :  ");
                Console.Write(vertex + " : ");
                while (stackVertex.Count != 0)
                {
                    var top = stackVertex.Pop();

                    foreach (var item in _vertexDictionary[top])
                    {
                        if ((!vertexList.Contains(item)) && (!stackVertex.Contains(item)))
                        {
                            stackVertex.Push(item);
                            vertexList.Add(item);
                            Console.Write(item + " ");
                            flagInsertSpace = true;
                        }
                    }
                    if (!flagInsertSpace) continue;
                    Console.Write(" : ");
                    flagInsertSpace = false;

                }
                Console.WriteLine();
            }
            else
            {
                throw new KeyNotFoundException("Such vertex don't exist");
            }
        }

        /// <summary>
        /// Convert to bool matric, which called as matrix adjacency
        /// </summary>
        /// <returns></returns>
        public bool[,] ToAdjacencyMatrixy()
        {
            var matric = new bool[Count,Count];
            var listKeys = _vertexDictionary.Keys.ToList();
            for (var i = 0; i < listKeys.Count(); ++i)
            {
                foreach (var vertex in _vertexDictionary[listKeys[i]])
                {
                    var j = listKeys.IndexOf(vertex);
                    matric[i, j] = true;
                    matric[j, i] = true;
                }
            }
            return matric;
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
            return ((IEnumerable<KeyValuePair<T, HashSet<T>>>)_vertexDictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _vertexDictionary.GetEnumerator();
        }
    }
}
