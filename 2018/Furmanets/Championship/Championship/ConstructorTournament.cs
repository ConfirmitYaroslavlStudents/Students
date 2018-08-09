using System;
using System.Collections.Generic;

namespace Championship
{
    public abstract class ConstructorTournament
    {
        public abstract List<Round> CreateTournament(List<string> players);

       

        protected static List<string> RandomSortPlayers(List<string> players)
        {
            var random = new Random();
            var newPlayersList = new List<string>();
            var playersCount = players.Count;

            for (var i = 0; i < playersCount; i++)
            {
                var currentPlayerNumber = random.Next(0, players.Count);
                newPlayersList.Add(players[currentPlayerNumber]);
                players.RemoveAt(currentPlayerNumber);
            }

            return newPlayersList;
        }

        protected static List<Round> ArrangementOfPlayersInTournamentGrid(List<string> players,
            List<Round> tournamentGrid)
        {
            var indexPlayer = 0;
            var countFirstStageMeetings = players.Count - tournamentGrid[0].Meetings.Count;

            for (var i = 0; i < countFirstStageMeetings; i++)
            {
                tournamentGrid[0].Meetings[i].FirstPlayer = players[indexPlayer];
                tournamentGrid[0].Meetings[i].SecondPlayer = players[indexPlayer + 1];
                indexPlayer += 2;
            }

            if (tournamentGrid.Count <= 1)
            {
                return tournamentGrid;
            }

            for (var i = tournamentGrid[1].Meetings.Count - 1; i >= countFirstStageMeetings / 2; i--)
            {
                if (indexPlayer < players.Count)
                {
                    tournamentGrid[1].Meetings[i].SecondPlayer = players[indexPlayer];

                    if (indexPlayer + 1 < players.Count)
                    {
                        tournamentGrid[1].Meetings[i].FirstPlayer = players[indexPlayer + 1];
                    }
                }
                indexPlayer += 2;

            }
            return tournamentGrid;
        }
    }
}