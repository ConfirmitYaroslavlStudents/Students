using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectGraph;

namespace HelpKsyu
{
	public class KosarajuAlgorithm
	{
		#region Fields

        public int StrongConnectedComponentCount { get; private set; }
		private List<List<int>> _strongConnectedComponentVertices;
		private List<List<DirectedEdge>> _strongConnectedComponentEdges;

		#endregion

		public KosarajuAlgorithm(DirectedGraph Graph)
		{
            _strongConnectedComponentEdges = new List<List<DirectedEdge>>();
            _strongConnectedComponentVertices = new List<List<int>>();

            BuildStrongConnectedComponents(Graph);
		}

        private void BuildStrongConnectedComponents(DirectedGraph Graph)
        {
            TopologicalSorting topologicalSortedVertices = new TopologicalSorting(Graph);
            DirectedGraph transposedGraph = Graph.Transposition();

            int[] mark = new int[Graph.VerticesCount];
            for (int i = 0; i < Graph.VerticesCount; ++i)
                mark[i] = -1;

            for (int i = transposedGraph.VerticesCount - 1; i >= 0; --i)
                if (mark[topologicalSortedVertices[i]] == -1)
                {
                    List<DirectedEdge> edges = new List<DirectedEdge>();
                    _strongConnectedComponentEdges.Add(edges);

                    List<int> vertices = new List<int>();
                    _strongConnectedComponentVertices.Add(vertices);

                    AddStrongConnectedComponent(topologicalSortedVertices[i], mark, transposedGraph);

                    _strongConnectedComponentVertices[StrongConnectedComponentCount].Sort();
                    ++StrongConnectedComponentCount;
                }
        }

        void AddStrongConnectedComponent(int vertex, int[] mark, DirectedGraph Graph)
        {
            mark[vertex] = StrongConnectedComponentCount;
            _strongConnectedComponentVertices[StrongConnectedComponentCount].Add(vertex);

            for (int i = 0; i < Graph.GetVertexDegree(vertex); ++i)
                if (mark[Graph.GetEdge(vertex, i).End] == StrongConnectedComponentCount || mark[Graph.GetEdge(vertex, i).End] == -1)
                {
                    _strongConnectedComponentEdges[StrongConnectedComponentCount].Add(Graph.GetEdge(vertex, i).Reverse());
                    if (mark[Graph.GetEdge(vertex, i).End] == -1)
                        AddStrongConnectedComponent(Graph.GetEdge(vertex, i).End, mark, Graph);
                }
        }

		#region Methods

		public int GetVertex(int strongConnectedComponent, int vertexIndex)
		{
			return _strongConnectedComponentVertices[strongConnectedComponent][vertexIndex];
		}

		public DirectedEdge GetEdge(int strongConnectedComponent, int edgeIndex)
		{
			return _strongConnectedComponentEdges[strongConnectedComponent][edgeIndex];
		}

		public int GetStrongConnegtionComponentVerticesCount(int strongConnectedComponent)
		{
			return _strongConnectedComponentVertices[strongConnectedComponent].Count;
		}

		public int GetStrongConnectedComponentEdgesCount(int strongConnectedComponent)
		{
			return _strongConnectedComponentEdges[strongConnectedComponent].Count;
		}

		public bool IsStrongConnected(int vertex1, int vertex2)
		{
			for (int i = 0; i < StrongConnectedComponentCount; ++i)
				if (_strongConnectedComponentVertices[i].Contains(vertex1) && _strongConnectedComponentVertices[i].Contains(vertex2))
					return true;

			return false;
		}

		#endregion
	}
}
