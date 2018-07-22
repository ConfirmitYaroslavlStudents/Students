using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    public class Match
    {
        public int[] Opponents = new int[2];
        public int Winner;

        public Match(int first, int second)
        {
            Opponents[0] = first;
            Opponents[1] = second;
            Winner = -1;
        }

        public bool PlayersReady
        {
            get
            {
                if (Opponents[0] == -1 || Opponents[1] == -1)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
