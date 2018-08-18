using System;

namespace Championship
{
    [Serializable]
    public class Game
    {
        public Team FirstTeam { get; private set; }
        public Team SecondTeam { get; private set; }
        public Team Winner { get; private set; }
        public int FirstTeamScore { get; set; }
        public int SecondTeamScore { get; set; }
        public bool IsPlayed = false;

        public Game(Team first, Team second)
        {
            FirstTeam = first;
            SecondTeam = second;
            Winner = null;
        }

        public void SetWinner()
        {
            if (Winner != null)
                return;

            if (SecondTeam == null)
            {
                Winner = FirstTeam;
                return;
            }

            if (FirstTeamScore > SecondTeamScore)
            {
                Winner = FirstTeam;
            }
            else
            {
                Winner = SecondTeam;
            }
            
        }

        public Team GetLoser()
        {
            if (Winner.Equals(FirstTeam))
                return SecondTeam;
            else
                return FirstTeam;
        }
    }
}
