using System;
using System.Collections.Generic;

namespace Championship
{
    public class ConstructorTournament
    {
        public static List<Round> CreateTournament(List<string> players)
        {
            players = RandomSortPlayers(players);
            var tournamentGrid = CreateTournamentGrid(players.Count);
            tournamentGrid = ArrangementOfPlayersInTournamentGrid(players, tournamentGrid);
            return tournamentGrid;
        }

        public static List<Round> CreateTournamentGrid(int playersCount)
        {
            var tournament = new List<Round>();
            var stage = 1;
            var countNextMeetings = 1;
            var countMeetings = 1;

            while (stage < playersCount)
            {
                tournament.Add(new Round());
                tournament[tournament.Count - 1].Stage = stage;

                for (var i = 0; i < countMeetings; i++)
                {
                    tournament[tournament.Count - 1].Meetings.Add(new Meeting());
                }

                if (tournament.Count != 1)
                {
                    var meetingIndex = 0;

                    for (var i = 0; i < countNextMeetings; i++)
                    {
                        tournament[tournament.Count - 1].Meetings[meetingIndex].NextStage =
                            tournament[tournament.Count - 2].Meetings[i];

                        tournament[tournament.Count - 1].Meetings[meetingIndex + 1].NextStage =
                            tournament[tournament.Count - 2].Meetings[i];

                        meetingIndex += 2;
                    }

                    countNextMeetings *= 2;
                }

                countMeetings *= 2;
                stage *= 2;
            }

            tournament.Reverse();
            return tournament;
        }

        private static List<string> RandomSortPlayers(List<string> players)
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

        private static List<Round> ArrangementOfPlayersInTournamentGrid(List<string> players,
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