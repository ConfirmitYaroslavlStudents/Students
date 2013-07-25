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

        public int StrongConnectedComponentCount { get; set; }
		private List<List<int>> _strongConnectedComponentVertices;
		private List<List<DirectedEdge>> _strongConnectedComponentEdges;

		#endregion

		#region Constructors

		public KosarajuAlgorithm() { }

		public KosarajuAlgorithm(DirectedGraph Graph)
		{
			TopologicalSorting topologicalSortedGraph = new TopologicalSorting(Graph);

			DirectedGraph transposedGraph = Graph.Transposition();

			int[] mark = new int[Graph.VerticesCount];
			for (int i = 0; i < Graph.VerticesCount; ++i)
				mark[i] = -1;

			_strongConnectedComponentEdges = new List<List<DirectedEdge>>();
			_strongConnectedComponentVertices = new List<List<int>>();

			for (int i = transposedGraph.VerticesCount - 1; i >= 0; --i)
				if (mark[topologicalSortedGraph[i]] == -1)
				{
					List<DirectedEdge> edges = new List<DirectedEdge>();
					_strongConnectedComponentEdges.Add(edges);

					List<int> vertices = new List<int>();
					_strongConnectedComponentVertices.Add(vertices);

					AddStrongConnectedComponent(topologicalSortedGraph[i], ref mark, transposedGraph);

					_strongConnectedComponentVertices[StrongConnectedComponentCount].Sort();
					++StrongConnectedComponentCount;
				}
		}

		#endregion

		#region Methods

		public int GetVertex(int scc, int ind)
		{
			return _strongConnectedComponentVertices[scc][ind];
		}

		public DirectedEdge GetEdge(int strongConnectedComponent, int ind)
		{
			return _strongConnectedComponentEdges[strongConnectedComponent][ind];
		}

		public int StrongConnegtionComponentVerticesCount(int strongConnectedComponent)
		{
			return _strongConnectedComponentVertices[strongConnectedComponent].Count;
		}

		public int StrongConnectedComponentEdgesCount(int strongConnectedComponent)
		{
			return _strongConnectedComponentEdges[strongConnectedComponent].Count;
		}

		public bool StrongConnect(int vertex1, int vertex2)
		{
			for (int i = 0; i < StrongConnectedComponentCount; ++i)
				if (_strongConnectedComponentVertices[i].Contains(vertex1) && _strongConnectedComponentVertices[i].Contains(vertex2))
					return true;

			return false;
		}

		#endregion

		#region AuxiliaryAlgorithms

		void AddStrongConnectedComponent(int vertex, ref int[] mark, DirectedGraph Graph)
		{
			mark[vertex] = StrongConnectedComponentCount;
			_strongConnectedComponentVertices[StrongConnectedComponentCount].Add(vertex);

			for (int i = 0; i < Graph.GetVertexDegree(vertex); ++i)
				if (mark[Graph.GetEdge(vertex, i).End] == StrongConnectedComponentCount || mark[Graph.GetEdge(vertex, i).End] == -1)
				{
					_strongConnectedComponentEdges[StrongConnectedComponentCount].Add(Graph.GetEdge(vertex, i).Reverse());
					if (mark[Graph.GetEdge(vertex, i).End] == -1)
						AddStrongConnectedComponent(Graph.GetEdge(vertex, i).End, ref mark, Graph);
				}
		}

		#endregion
	}
}
