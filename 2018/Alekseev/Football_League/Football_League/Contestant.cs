using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_League
{
    public class Contestant
    {
        readonly string _name;

        public int position = 0;
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public Contestant(string name, int pos)
        {
            if (name.Length < 20 && name.Length > 0)
                _name = name;
            position = pos;
        }
    }
}
