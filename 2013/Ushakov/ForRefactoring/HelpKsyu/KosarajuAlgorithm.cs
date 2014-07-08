using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectedGraph;

namespace HelpKsyu
{
	public class KosarajuAlgorithm
	{
		#region Fields

		int scc_num;
		List<List<int>> SCCVrt;
		List<List<DEdge>> SCCEdg;

		#endregion

		#region Constructors

		public KosarajuAlgorithm() { }

		public KosarajuAlgorithm(DGraph G)
		{
			TopologicalSorting Gts = new TopologicalSorting(G);

			DGraph Gtr = G.Transposition();

			int[] mark = new int[G.VerticesCount];
			for (int i = 0; i < G.VerticesCount; ++i)
				mark[i] = -1;
			SCCEdg = new List<List<DEdge>>();
			SCCVrt = new List<List<int>>();

			for (int i = Gtr.VerticesCount - 1; i >= 0; --i)
				if (mark[Gts[i]] == -1)
				{
					List<DEdge> tscce = new List<DEdge>();
					SCCEdg.Add(tscce);

					List<int> tsccv = new List<int>();
					SCCVrt.Add(tsccv);

					AddSCC(Gts[i], ref mark, Gtr);

					SCCVrt[SCCCount].Sort();
					++SCCCount;
				}
		}

		#endregion

		#region Methods

		public int GetVertex(int scc, int ind)
		{
			return SCCVrt[scc][ind];
		}

		public DEdge GetEdge(int scc, int ind)
		{
			return SCCEdg[scc][ind];
		}

		public int SCCVrtxCount(int scc)
		{
			return SCCVrt[scc].Count;
		}

		public int SCCEdgCount(int scc)
		{
			return SCCEdg[scc].Count;
		}

		public bool StrongConnect(int v1, int v2)
		{
			for (int i = 0; i < SCCCount; ++i)
				if (SCCVrt[i].Contains(v1) && SCCVrt[i].Contains(v2))
					return true;

			return false;
		}

		#endregion

		#region Auxiliaryalgorithms

		void AddSCC(int v, ref int[] mark, DGraph G)
		{
			mark[v] = SCCCount;
			SCCVrt[SCCCount].Add(v);

			for (int i = 0; i < G.VertexDegree(v); ++i)
				if (mark[G.GetEdge(v, i).End] == SCCCount || mark[G.GetEdge(v, i).End] == -1)
				{
					SCCEdg[SCCCount].Add(G.GetEdge(v, i).Reverse());
					if (mark[G.GetEdge(v, i).End] == -1)
						AddSCC(G.GetEdge(v, i).End, ref mark, G);
				}
		}

		#endregion

		#region Properties

		public int SCCCount
		{
			get { return scc_num; }
			set { scc_num = value; }
		}

		#endregion
	}
}
