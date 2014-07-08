using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonComponents
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

		public void AddEdge(DirectedEdge edge)
		{
			Graph[edge.Begin].Add(edge);
			++EdgesCount;
		}

		public int GetVertexDegree(int vertex)
		{
			return Graph[vertex].Count;
		}

		public DirectedEdge GetEdge(int vertex, int edgeIndex)
		{
			return this[vertex, edgeIndex];
		}

		public DirectedGraph Transposition()
		{
			DirectedGraph result = new DirectedGraph(VerticesCount);
			for (int i = 0; i < VerticesCount; ++i)
				for (int j = 0; j < GetVertexDegree(i); ++j)
					result.AddEdge(GetEdge(i, j).Reverse());

			return result;
		}

		internal List<DirectedEdge> this[int i]
		{
			get { return Graph[i]; }
		}

		DirectedEdge this[int i, int j]
		{
			get { return Graph[i][j]; }
		}
	}
}
