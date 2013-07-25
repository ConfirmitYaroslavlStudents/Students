using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectGraph;

namespace HelpKsyu
{
	class TopologicalSorting
	{
		List<int> _topologicalSort;

		#region Constructors

		public TopologicalSorting() { }

		public TopologicalSorting(DirectedGraph Graph)
		{
			_topologicalSort = new List<int>();
			bool[] was = new bool[Graph.VerticesCount];

			for (int i = 0; i < Graph.VerticesCount; ++i)
				if (!was[i])
					DFS(i, ref was, Graph);
		}

		#endregion

		#region AuxiliaryAlgorithms

		void DFS(int vertex, ref bool[] was, DirectedGraph Graph)
		{
			was[vertex] = true;

			for (int i = 0; i < Graph.GetVertexDegree(vertex); ++i)
				if (!was[Graph.GetEdge(vertex, i).End])
					DFS(Graph.GetEdge(vertex, i).End, ref was, Graph);

			_topologicalSort.Add(vertex);
		}

		#endregion

		#region Properties

		public int this[int i]
		{
			get { return _topologicalSort[i]; }
		}

		#endregion
	}
}
