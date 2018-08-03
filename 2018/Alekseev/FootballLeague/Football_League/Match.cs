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

        public Contestant PickWinner()
        {
            if (PlayerTwo == null && PlayerOne != null)
            {
                Winner = PlayerOne;
                return Winner;
            }
            if (ConsoleWorker.ChooseMatchWinner(this) == 1)
            {
                Winner = PlayerOne;
                Loser = PlayerTwo;
            }
            else
            {
                Winner = PlayerTwo;
                Loser = PlayerOne;
            }
            return Winner;
        }

        public Contestant GetLoser()
        {
            return Loser;
        }
    }
}
