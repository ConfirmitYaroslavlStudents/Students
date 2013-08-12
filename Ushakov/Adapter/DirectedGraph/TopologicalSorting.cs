using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonComponents;

namespace CommonComponents
{
	class TopologicalSorting
	{
		List<int> _topologicalSort;

		public TopologicalSorting() { }

		public TopologicalSorting(DirectedGraph Graph)
		{
			_topologicalSort = new List<int>();
			bool[] mark = new bool[Graph.VerticesCount];

			for (int i = 0; i < Graph.VerticesCount; ++i)
				if (!mark[i])
					DFS(i, mark, Graph);
		}

		void DFS(int vertex, bool[] mark, DirectedGraph Graph)
		{
			mark[vertex] = true;

			for (int i = 0; i < Graph.GetVertexDegree(vertex); ++i)
				if (!mark[Graph.GetEdge(vertex, i).End])
					DFS(Graph.GetEdge(vertex, i).End, mark, Graph);

			_topologicalSort.Add(vertex);
		}

		public int this[int i]
		{
			get { return _topologicalSort[i]; }
		}
	}
}