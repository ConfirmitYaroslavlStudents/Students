using System;
using System.Collections.Generic;

namespace FootballLeagueClassLibrary.Structure
{
    [Serializable]
    public class FullGrid
    {
        public List<MatchTreeGrid> Grid = new List<MatchTreeGrid>();
        public bool IsFinished;

        public FullGrid()
        {           
        }
        public void SetGridTreesNumber(int num)
        {
            for (var i = 0; i < num; i++)
            {
                Grid.Add(new MatchTreeGrid());
            }
        }
        public void PlayRound(List<int>[] choices)
        {
            if (!IsFinished)
            {
                int singlePlayerLeftFirst = -1;
                bool onePlayerLeftInTournament = true;

                for (int i = Grid.Count - 1; i >= 0; i--)
                {
                    var currentRoundMatch = Grid[i].CurrentRoundFirstMatch;
                    Grid[i].PlayRound(choices[i]);

                    GridChangesAccordingToNumberOfLosers(i, currentRoundMatch, ref onePlayerLeftInTournament,ref singlePlayerLeftFirst);
                }

                if (onePlayerLeftInTournament)
                    EndTournament();
            }
        }

        private void GridChangesAccordingToNumberOfLosers(int currentTreePointer,Match currentRoundMatch, ref bool onePlayerLeftInTournament,ref int singlePlayerLeftFirst)
        {
            var losers = Grid[currentTreePointer].GetRoundLosers(currentRoundMatch);
            if (losers.Count != 0 && currentTreePointer < Grid.Count - 1)
            {
                onePlayerLeftInTournament = false;
                Grid[currentTreePointer + 1].AddPlayersFromNextTree(losers);
                return;
            }

            if (Grid[currentTreePointer].CurrentRoundFirstMatch != null && losers.Count == 0)
            {
                if (singlePlayerLeftFirst != -1)
                {
                    onePlayerLeftInTournament = false;
                    int singlePlayerLeftSecond = currentTreePointer;
                    Match winnersMatch = new Match(Grid[singlePlayerLeftFirst].CurrentRoundFirstMatch.PlayerOne,
                        Grid[singlePlayerLeftSecond].CurrentRoundFirstMatch.PlayerOne);

                    if (winnersMatch.GetWinner() == winnersMatch.PlayerOne)
                        Grid[singlePlayerLeftSecond].CurrentRoundFirstMatch = null;
                    else
                        Grid[singlePlayerLeftFirst].CurrentRoundFirstMatch = null;
                    singlePlayerLeftFirst = -1;
                }
                else
                {
                    singlePlayerLeftFirst = currentTreePointer;
                }
            }
            else if (losers.Count > 0)
                onePlayerLeftInTournament = false;
        }
        public void EndTournament()
        {
            IsFinished = true;
        }
        public void InitialiseGrid(List<Contestant> players)
        {
            Grid?[0].SetFirstRound(players);
        }
    }
}
