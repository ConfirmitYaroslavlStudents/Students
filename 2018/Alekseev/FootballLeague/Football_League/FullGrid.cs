using System;
using System.Collections.Generic;

namespace Football_League
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
        public void PlayRound()
        {
            if (!IsFinished)
            {
                int singlePlayerLeftFirst = -1;
                bool onePlayerLeftInTournament = true;

                for (int i = Grid.Count - 1; i >= 0; i--)
                {
                    var losers = Grid[i].PlayRound();
                    if (losers.Count != 0 && i < Grid.Count - 1)
                    {
                        onePlayerLeftInTournament = false;
                        Grid[i + 1].AddPlayersFromNextTree(losers);
                        continue;
                    }

                    if (Grid[i].CurrentRoundFirstMatch != null && losers.Count == 0)
                    {
                        if (singlePlayerLeftFirst != -1)
                        {
                            onePlayerLeftInTournament = false;
                            int singlePlayerLeftSecond = i;
                            Match winnersMatch = new Match(Grid[singlePlayerLeftFirst].CurrentRoundFirstMatch.PlayerOne,
                                Grid[singlePlayerLeftSecond].CurrentRoundFirstMatch.PlayerOne);

                            if (winnersMatch.PickWinner() == winnersMatch.PlayerOne)
                                Grid[singlePlayerLeftSecond].CurrentRoundFirstMatch = null;
                            else
                                Grid[singlePlayerLeftFirst].CurrentRoundFirstMatch = null;
                            singlePlayerLeftFirst = -1;
                        }
                        else
                        {
                            singlePlayerLeftFirst = i;
                        }
                    }
                    else if (losers.Count > 0)
                        onePlayerLeftInTournament = false;
                }

                if (onePlayerLeftInTournament)
                    EndTournament();
            }
        }

        public void EndTournament()
        {
            IsFinished = true;
            ConsoleWorker.OnePlayerLeft();
        }
        public void InitialiseGrid(List<Contestant> players)
        {
            Grid?[0].SetFirstRound(players);
        }
    }
}
