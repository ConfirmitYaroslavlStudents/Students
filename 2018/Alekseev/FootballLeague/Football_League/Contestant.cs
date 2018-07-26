using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_League
{
    public class Contestant
    {
        public int Position = 0;
        public string Name { get; }

        public Contestant(string name, int pos)
        {
            Name = name;
            Position = pos;
        }
    }
}
