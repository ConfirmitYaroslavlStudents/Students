using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SkillTree
{
    public class GraphStatus<T>
    {
        public Dictionary<int, bool> VertexStatuses;

        [JsonIgnore]
        public Graph<T> Graph;

        public GraphStatus(Graph<T> graph, Dictionary<int, bool> vertexStatuses)
        {
            Graph = graph;
            VertexStatuses = vertexStatuses;
        }

        public GraphStatus(Graph<T> graph)
        {
            BuildNewGraphStatus(graph);
            Graph = graph;
        }

        private void BuildNewGraphStatus(Graph<T> graph)
        {
            VertexStatuses = new Dictionary<int, bool>();
            foreach (var vertex in graph) VertexStatuses.Add(vertex.Id, false);
        }

        public bool IsVertexAvailable(Vertex<T> vertex) => vertex.Dependencies.TrueForAll(i => VertexStatuses[i]);

        public bool IsVertexFinished(Vertex<T> vertex) => VertexStatuses[vertex.Id];

        public void FinishVertex(Vertex<T> vertex)
        {
            if (!IsVertexAvailable(vertex))
                throw new InvalidOperationException("Vertex not Available.");
            VertexStatuses[vertex.Id] = true;
        }
    }
}
