using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkillTree
{
    [Serializable]
    public class GraphInformation
    {
        [XmlAttribute]
        public int MAXId { get; set; }

        [XmlArray]
        public Vertex[] Vertexs { get; set; }

        [XmlArray]
        public List<int[]> Dependences { get; set; }

        public GraphInformation()
        {
        }

        internal void AddVertexs(Vertex[] vertexs)
        {
            Vertexs = vertexs;
            Dependences = new List<int[]>();
            if(Dependences == null)
            {
                Dependences = new List<int[]>();
            }
            for (int i = 0; i < Vertexs.Length; i++) 
            {
                Dependences.Add(vertexs[i].GetDependences());
            }           
        }

    }
}
