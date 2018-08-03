using System;
using System.Collections.Generic;

namespace Football_League
{
    [Serializable]
    public class MatchTreeGrid
    {
        public Match StartMatch;
        public  Match CurrentRoundFirstMatch;
        public Match CurrentMatchToDraw;

        public MatchTreeGrid()
        {
            StartMatch = CurrentRoundFirstMatch;
        }
        public List<Contestant> PlayRound()
        { // check if startMatchNextRoundMatch exists ...
            if(CurrentRoundFirstMatch == null)
                return new List<Contestant>();

            var currentMatch = StartMatch;
            StartMatch = currentMatch;
            CurrentRoundFirstMatch = currentMatch;
            while (currentMatch.NextRoundMatch != null)
                currentMatch = currentMatch.NextRoundMatch;
            CurrentRoundFirstMatch = currentMatch;

            var losers = new List<Contestant>();
            //var currentMatch = CurrentRoundFirstMatch;
            var currentMatchInNextRound = new Match();
            while (currentMatch?.PlayerOne != null)
            {
                var matchWinner = currentMatch.PickWinner();
                if(currentMatch.GetLoser() != null)
                   losers.Add(currentMatch.GetLoser());

                if (currentMatchInNextRound.PlayerOne == null)
                {
                    currentMatchInNextRound = new Match
                    {
                        PlayerOne = matchWinner
                    };
                    currentMatch.NextRoundMatch = currentMatchInNextRound;
                }

                else if (currentMatchInNextRound.PlayerTwo == null)
                {
                    currentMatchInNextRound.PlayerTwo = matchWinner;
                    currentMatch.NextRoundMatch = currentMatchInNextRound;
                    currentMatchInNextRound.NextMatch = new Match();
                    currentMatchInNextRound = currentMatchInNextRound.NextMatch;
                }

                currentMatch = currentMatch.NextMatch;
            }

            currentMatch = CurrentRoundFirstMatch.NextMatch;
            currentMatchInNextRound = CurrentRoundFirstMatch.NextRoundMatch;

            int matchNumber = 2;
            while (currentMatch != null)
            {
                if (matchNumber == 1)
                {
                    matchNumber = 2;
                    currentMatchInNextRound.NextMatch = currentMatch.NextRoundMatch;
                    currentMatchInNextRound = currentMatchInNextRound.NextMatch;
                }
                else
                    matchNumber = 1;
                currentMatch = currentMatch.NextMatch;
            }

            CurrentRoundFirstMatch = CurrentRoundFirstMatch.NextRoundMatch;
            return losers;
        }

        public void AddPlayersFromNextTree(List<Contestant> players)
        {
            var currentMatch = CurrentRoundFirstMatch;
            while (currentMatch?.NextMatch != null)
                currentMatch = currentMatch.NextMatch;
            foreach (var player in players)
            {
                if (currentMatch?.PlayerOne != null && currentMatch.PlayerTwo != null)
                    currentMatch = currentMatch.NextMatch;
                if (currentMatch == null)
                {
                    currentMatch = new Match
                    {
                        PlayerOne = player
                    };
                    continue;
                }

                if (currentMatch.PlayerTwo == null)
                {
                    currentMatch.PlayerTwo = player;
                    currentMatch.NextMatch = new Match();
                    if (StartMatch == null)
                    {
                        StartMatch = currentMatch;
                        CurrentRoundFirstMatch = currentMatch;
                    }
                    currentMatch = currentMatch.NextMatch;
                }
            }
        }

        public void SetFirstRound(List<Contestant> players)
        {
            var currentMatch = CurrentRoundFirstMatch;
            foreach (var player in players)
            {
                if (currentMatch == null)
                {
                    currentMatch = new Match();
                }
                if (currentMatch.PlayerOne == null)
                {
                    currentMatch.PlayerOne = player;
                    continue;
                }            
                currentMatch.PlayerTwo = player;

                if (StartMatch == null)
                {
                    CurrentRoundFirstMatch = currentMatch;
                    StartMatch = currentMatch;
                }
                currentMatch.NextMatch = new Match();
                currentMatch = currentMatch.NextMatch;

            }
        }
    }
}
