using System;
using System.Collections;

namespace TournamentLibrary
{
    [Serializable]
    public class Match
    {
        public Player[] Opponents = new Player[2];
        public int WinnerIndex;
        
        public Match(Player first, Player second)
        {
            Opponents[0] = first;
            Opponents[1] = second;
            WinnerIndex = -1;
        }

        public bool PlayersReady
        {
            get
            {
                if (Opponents[0] == null || Opponents[1] == null)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
