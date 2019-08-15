using System;

namespace Football_League
{
    [Serializable]
    public class Match
    {
        public Contestant Winner, Loser;
        public Match NextMatch, NextRoundMatch;
        public Contestant PlayerOne, PlayerTwo;

        public Match()
        {
            
        }
        public Match(Contestant first = null, Contestant second = null)
        {
            PlayerOne = first;
            PlayerTwo = second;
        }

        public void SetWinnerAndLoser(int userChoice)
        {
            if (PlayerTwo == null && PlayerOne != null)
            {
                Winner = PlayerOne;
                return;
            }

            Winner = userChoice == 1 ? PlayerOne : PlayerTwo;
            Loser = userChoice == 1 ? PlayerTwo : PlayerOne;
        }

        public Contestant GetWinner()
        {
            return Winner;
        }
        public Contestant GetLoser()
        {
            return Loser;
        }
    }
}
