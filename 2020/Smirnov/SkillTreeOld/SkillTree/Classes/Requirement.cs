using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillTree.Classes
{
    [Serializable]
    public class Requirement
    {
        public Skill ConnectedVertex { get; set; }

        public Requirement() { }
        public Requirement(Skill connectedVertex)
        {
            ConnectedVertex = connectedVertex;
        }
    }
}
