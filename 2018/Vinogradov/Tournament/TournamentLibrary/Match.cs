using System;

namespace TournamentLibrary
{
    [Serializable]
    public class Match
    {
        public string[] Opponents = new string[2];
        public int Winner;

        public Match(string first, string second)
        {
            Opponents[0] = first;
            Opponents[1] = second;
            Winner = -1;
        }

        public bool PlayersReady
        {
            get
            {
                if (Opponents[0] == string.Empty || Opponents[1] == string.Empty)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
