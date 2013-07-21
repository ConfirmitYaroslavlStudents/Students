using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DirectedGraph
{
	public struct DEdge
	{
		int bgn, end;

		#region Constructors

		public DEdge(int b, int e)
		{
			bgn = b;
			end = e;
		}

		public DEdge(DEdge edg)
		{
			bgn = edg.Begin;
			end = edg.End;
		}

		#endregion

		#region Methods

		public DEdge Reverse()
		{
			DEdge res = new DEdge();
			res.Begin = End;
			res.End = Begin;
			return res;
		}

		#endregion

		#region Properties

		public int Begin
		{
			get { return bgn; }
			set { bgn = value; }
		}

		public int End
		{
			get { return end; }
			set { end = value; }
		}

		#endregion
	};

	public class DGraph
	{
		#region Fields

		int vrt_num, edg_num;
		List<DEdge>[] G;

		#endregion

		#region Constructors

		public DGraph() { }

		public DGraph(int vn) : this()
		{
			vrt_num = vn;
			G = new List<DEdge>[vrt_num];
			for (int i = 0; i < vrt_num; ++i)
				G[i] = new List<DEdge>();
		}

		public DGraph(DGraph g) : this(g.VerticesCount)
		{
			for (int i = 0; i < g.VerticesCount; ++i)
				for (int j = 0; j < g[i].Count; ++j)
				{
					G[i].Add(g[i, j]);
					++edg_num;
				}
		}

		#endregion

		#region Methods

		public void ReadFromConsole()
		{
			Console.Write("Enter a number of vertices, please: ");
			vrt_num = int.Parse(Console.ReadLine());

			Console.Write("Enter a number of edges, please: ");
			edg_num = int.Parse(Console.ReadLine());

			G = new List<DEdge>[VerticesCount];
			for (int i = 0; i < VerticesCount; ++i)
				G[i] = new List<DEdge>();

			string[] sprts = { " " };
			for (int i = 0; i < edg_num; ++i)
			{
				Console.Write("Enter begin and end of {0} edge, please: ", i + 1);

				string s;
				s = Console.ReadLine();

				string[] spr_s = s.Split(sprts, StringSplitOptions.RemoveEmptyEntries);

				DEdge t = new DEdge(int.Parse(spr_s[0]) - 1, int.Parse(spr_s[1]) - 1);

				G[t.Begin].Add(t);
			}
		}

		public void ReadFromTxtFile(string file)
		{
			StreamReader data;
			data = new StreamReader(file);
			string s = data.ReadToEnd();

			string[] sprts = { " ", "\r\n" };
			string[] spr_s = s.Split(sprts, StringSplitOptions.RemoveEmptyEntries);

			vrt_num = int.Parse(spr_s[0]);
			edg_num = int.Parse(spr_s[1]);

			G = new List<DEdge>[VerticesCount];
			for (int i = 0; i < VerticesCount; ++i)
				G[i] = new List<DEdge>();

			for (int i = 0; i < edg_num; ++i)
			{
				DEdge t = new DEdge(int.Parse(spr_s[i * 2 + 2]) - 1, int.Parse(spr_s[i * 2 + 3]) - 1);
				G[t.Begin].Add(t);
			}
		}

		public void AddEdge(DEdge edg)
		{
			G[edg.Begin].Add(edg);
			++edg_num;
		}

		public int VertexDegree(int vrt)
		{
			return G[vrt].Count;
		}

		public DEdge GetEdge(int vrt, int ind)
		{
			return this[vrt, ind];
		}

		public DGraph Transposition()
		{
			DGraph res = new DGraph(VerticesCount);
			for (int i = 0; i < VerticesCount; ++i)
				for (int j = 0; j < VertexDegree(i); ++j)
					res.AddEdge(GetEdge(i, j).Reverse());

			return res;
		}

		#endregion

		#region Properties

		public int VerticesCount
		{
			get { return vrt_num; }
			set { vrt_num = value; }
		}

		public int EdgesCount
		{
			get { return edg_num; }
			set { edg_num = value; }
		}

		List<DEdge> this[int i]
		{
			get { return G[i]; }
		}

		DEdge this[int i, int j]
		{
			get { return G[i][j]; }
		}

		#endregion

	}
}
