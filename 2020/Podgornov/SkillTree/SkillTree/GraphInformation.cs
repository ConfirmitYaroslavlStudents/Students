using System.Collections.Generic;
using System.Linq;

namespace SkillTree
{
    public class GraphInformation
    {
        public Dictionary<int, Vertex> Vertexes { get; set; }

        public int[][] VertexesDependencies { get; set; }

        public GraphInformation() { }

        public GraphInformation(Dictionary<int, Vertex> vertexes)
        {
            Vertexes = vertexes;
            VertexesDependencies = new int[Vertexes.Count][];
            for (int i = 0; i < Vertexes.Count; i++)
            {
                VertexesDependencies[i] = Vertexes[i].GetVertexesDependenciesId();
            }
        }

        public Graph BuildGraph()
        {
            var vertexesArray = Vertexes.Values.ToArray();
            var graph = new Graph();
            for (int i = 0; i < Vertexes.Count; i++)
            {
                for (int j = 0; j < VertexesDependencies[i].Length; j++)
                {
                    Vertexes[i].AddDependence(Vertexes[VertexesDependencies[i][j]]);
                }
            }
            for (int i = 0; i < vertexesArray.Length; i++)
            {
                vertexesArray[i].Id = i;
            }
            graph.AddVertexes(vertexesArray);
            return graph;
        }
    }
}
