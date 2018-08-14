using System.Collections.Generic;

namespace Championship
{
    class SingleConstructorTournament:DoubleConstructorTournament
    {
        public override List<Round> CreateTournament(List<string> players)
        {
            players = RandomSortPlayers(players);
            var tournamentGrid = CreateSingleEliminationTournamentGrid(players.Count);
            tournamentGrid = ArrangementOfPlayersInTournamentGrid(players, tournamentGrid);
            return tournamentGrid;
        }

        public static List<Round> CreateSingleEliminationTournamentGrid(int playersCount)
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
    }
}
