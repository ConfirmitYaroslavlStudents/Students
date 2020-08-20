namespace SkillTree.Graph
{
    public class Edge
    {
        public Edge() { } 
        public Edge(Vertex connectedVertex)
        {
            ConnectedVertex = connectedVertex;
        }

        public Vertex ConnectedVertex { get; set; }
    }
}
