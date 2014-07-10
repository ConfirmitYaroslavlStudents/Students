using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Graph<T> : IEnumerable<KeyValuePair<T, HashSet<T>>>
    {
        
        private Dictionary<T, HashSet<T>> _setVertex;  
      
        /// <param name="setVertex"> Set vertex with their edges </param>
        public Graph(Dictionary<T, HashSet<T>> setVertex) 
        { 
            this._setVertex = setVertex;           
        }  
      
        /// <param name="vertex"> isolated vertex </param>
        public Graph(T vertex)
        {
            this._setVertex = new Dictionary<T, HashSet<T>>();
            this._setVertex.Add(vertex, new HashSet<T>());
        }

        public Graph()
        {
            this._setVertex = new Dictionary<T, HashSet<T>>();
        }

        /// <summary>
        /// Add vertex to the graph
        /// </summary>               
        public void AddVertex(T vertex, HashSet<T> edges)
        {
            if (!_setVertex.ContainsKey(vertex))
            {
                _setVertex.Add(vertex, edges);
                foreach (var temp in edges)
                {
                    _setVertex[temp].Add(vertex);
                }
            }
            else
            {
                throw new Exception("Adding of the already existing vertices");
            }
        
        }

        /// <summary>
        /// Remove vertex of the graph
        /// </summary>           
        public void RemoveVertex(T vertex)
        {
            if (_setVertex.ContainsKey(vertex))
            {
                foreach (var temp in _setVertex[vertex])
                {
                    if (_setVertex[temp].Contains(vertex))
                    {
                        _setVertex[temp].Remove(vertex);
                    }
                }
                _setVertex.Remove(vertex);
            }
            else
            {
                throw new Exception("Remove of not existing vertex");
            }
        }

        public int Count
        {
            get { return _setVertex.Count(); }
        }

        public Dictionary<T, HashSet<T>> SetVertex
        {
            get { return _setVertex; }
        }

        IEnumerator<KeyValuePair<T, HashSet<T>>> IEnumerable<KeyValuePair<T, HashSet<T>>>.GetEnumerator()
        {
            foreach (var temp in _setVertex)
            {
                yield return temp;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<T>).GetEnumerator();
        }
    }
}
