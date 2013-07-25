using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DirectGraph
{
	public class DirectedEdge
	{
        public int Begin { get; set; }
        public int End { get; set; }

		public DirectedEdge(int begin, int end)
		{
			Begin = begin;
			End = end;
		}

		public DirectedEdge(DirectedEdge edge)
		{
			Begin = edge.Begin;
			End = edge.End;
		}

        public DirectedEdge Reverse()
        {
            return new DirectedEdge(End, Begin);
        }
	};

	public class DirectedGraph
	{
        public int VerticesCount { get; set; }
        public int EdgesCount { get; set; }
        List<DirectedEdge>[] Graph;

		public DirectedGraph() { }

		public DirectedGraph(int verticesCount) : this()
		{
			VerticesCount = verticesCount;
			Graph = new List<DirectedEdge>[VerticesCount];
			for (int i = 0; i < VerticesCount; ++i)
				Graph[i] = new List<DirectedEdge>();
		}

		public DirectedGraph(DirectedGraph g) : this(g.VerticesCount)
		{
			for (int i = 0; i < g.VerticesCount; ++i)
				for (int j = 0; j < g[i].Count; ++j)
				{
					Graph[i].Add(g[i, j]);
					++EdgesCount;
				}
		}

		#region Methods

		public void ReadFromConsole()
		{
			Console.Write("Enter a number of vertices, please: ");
			VerticesCount = int.Parse(Console.ReadLine());

			Console.Write("Enter a number of edges, please: ");
			EdgesCount = int.Parse(Console.ReadLine());

			Graph = new List<DirectedEdge>[VerticesCount];
			for (int i = 0; i < VerticesCount; ++i)
				Graph[i] = new List<DirectedEdge>();

			string[] sprts = { " " };
			for (int i = 0; i < EdgesCount; ++i)
			{
				Console.Write("Enter begin and end of {0} edge, please: ", i + 1);

				string s;
				s = Console.ReadLine();

				string[] spr_s = s.Split(sprts, StringSplitOptions.RemoveEmptyEntries);

				DirectedEdge t = new DirectedEdge(int.Parse(spr_s[0]) - 1, int.Parse(spr_s[1]) - 1);

				Graph[t.Begin].Add(t);
			}
		}

		public void ReadFromTxtFile(string file)
		{
			StreamReader data;
			data = new StreamReader(file);
			string s = data.ReadToEnd();

			string[] sprts = { " ", "\r\n" };
			string[] spr_s = s.Split(sprts, StringSplitOptions.RemoveEmptyEntries);

			VerticesCount = int.Parse(spr_s[0]);
			EdgesCount = int.Parse(spr_s[1]);

			Graph = new List<DirectedEdge>[VerticesCount];
			for (int i = 0; i < VerticesCount; ++i)
				Graph[i] = new List<DirectedEdge>();

			for (int i = 0; i < EdgesCount; ++i)
			{
				DirectedEdge t = new DirectedEdge(int.Parse(spr_s[i * 2 + 2]) - 1, int.Parse(spr_s[i * 2 + 3]) - 1);
				Graph[t.Begin].Add(t);
			}
		}

		public void AddEdge(DirectedEdge edg)
		{
			Graph[edg.Begin].Add(edg);
			++EdgesCount;
		}

		public int GetVertexDegree(int vrt)
		{
			return Graph[vrt].Count;
		}

		public DirectedEdge GetEdge(int vrt, int ind)
		{
			return this[vrt, ind];
		}

		public DirectedGraph Transposition()
		{
			DirectedGraph res = new DirectedGraph(VerticesCount);
			for (int i = 0; i < VerticesCount; ++i)
				for (int j = 0; j < GetVertexDegree(i); ++j)
					res.AddEdge(GetEdge(i, j).Reverse());

			return res;
		}

		#endregion

		#region Properties

		List<DirectedEdge> this[int i]
		{
			get { return Graph[i]; }
		}

		DirectedEdge this[int i, int j]
		{
			get { return Graph[i][j]; }
		}

		#endregion

	}
}
