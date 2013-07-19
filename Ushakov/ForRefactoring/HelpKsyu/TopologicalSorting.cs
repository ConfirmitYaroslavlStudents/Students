using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectedGraph;

namespace HelpKsyu
{
	class TopologicalSorting
	{
		List<int> TS;

		#region Constructors

		public TopologicalSorting() { }

		public TopologicalSorting(DGraph G)
		{
			TS = new List<int>();
			bool[] was = new bool[G.VerticesCount];

			for (int i = 0; i < G.VerticesCount; ++i)
				if (!was[i])
					DFS(i, ref was, G);
		}

		#endregion

		#region Auxiliaryalgorithms

		void DFS(int v, ref bool[] was, DGraph G)
		{
			was[v] = true;

			for (int i = 0; i < G.VertexDegree(v); ++i)
				if (!was[G.GetEdge(v, i).End])
					DFS(G.GetEdge(v, i).End, ref was, G);

			TS.Add(v);
		}

		#endregion

		#region Properties

		public int this[int i]
		{
			get { return TS[i]; }
		}

		#endregion
	}
}
