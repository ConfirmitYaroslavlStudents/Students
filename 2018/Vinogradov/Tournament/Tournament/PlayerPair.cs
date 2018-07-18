using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    public class PlayerPair
    {
        public int[] Opponents;

        public PlayerPair(int first, int second)
        {
            Opponents = new int[2];
            Opponents[0] = first;
            Opponents[1] = second;
        }

        public bool PlayersReady()
        {
            if (Opponents[0] < 0 || Opponents[1] < 0)
            {
                return false;
            }
            return true;
        }
    }
}
