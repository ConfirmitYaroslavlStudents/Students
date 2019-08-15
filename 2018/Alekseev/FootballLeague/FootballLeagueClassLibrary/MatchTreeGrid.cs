using System;
using System.Collections.Generic;

namespace Football_League
{
    [Serializable]
    public class MatchTreeGrid
    {
        public Match StartMatch;
        public  Match CurrentRoundFirstMatch;

        public MatchTreeGrid()
        {
            StartMatch = CurrentRoundFirstMatch;
        }
        public void PlayRound(List<int> userChoices)
        {
            if (CurrentRoundFirstMatch == null)
                return;

            RestoreConnections();

            Match currentMatch = CurrentRoundFirstMatch;
            var currentMatchInNextRound = new Match();
            int choicePointer = 0;

            while (currentMatch?.PlayerOne != null)
            {
                SetMatchWinnerAndLoser(currentMatch,userChoices[choicePointer]);
                choicePointer++;

                Contestant matchWinner = currentMatch.GetWinner();
                AddWinnerToNextRound(ref currentMatch, ref currentMatchInNextRound, matchWinner);
            }

            SetCurrentAndNextRoundsConnections(out currentMatch, out currentMatchInNextRound);
        }

        public List<Contestant> GetRoundLosers(Match startingMatch)
        {
            var currentMatch = startingMatch;
            List<Contestant> losers = new List<Contestant>();
            while (currentMatch != null)
            {
                if(currentMatch.GetLoser() != null)
                    losers.Add(currentMatch.Loser);
                currentMatch = currentMatch.NextMatch;
            }

            return losers;
        }

        private static void SetMatchWinnerAndLoser(Match currentMatch, int choice)
        {
            currentMatch.SetWinnerAndLoser(choice);
        }

        private void SetCurrentAndNextRoundsConnections(out Match currentMatch, out Match currentMatchInNextRound)
        {
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
        }

        private static void AddWinnerToNextRound(ref Match currentMatch, ref Match currentMatchInNextRound, Contestant matchWinner)
        {
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

        private void RestoreConnections()
        {
            var currentMatch = StartMatch;
            StartMatch = currentMatch;
            CurrentRoundFirstMatch = currentMatch;
            while (currentMatch.NextRoundMatch != null)
                currentMatch = currentMatch.NextRoundMatch;
            CurrentRoundFirstMatch = currentMatch;
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
