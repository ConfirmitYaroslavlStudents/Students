using System;
using System.Collections.Generic;

namespace Championship
{
    public class DoubleConstructorTournament : ConstructorTournament
    {
        public override List<Round> CreateTournament(List<string> players)
        {
            var tournamentGrid = new List<Round>();
            var stage = 1;
            var countMeetings = 1;
            var upCountMeetings = true;

            while (stage < players.Count / 2)
            {
                var round = new Round { Stage = stage };

                for (var i = 0; i < countMeetings; i++)
                {
                    round.Meetings.Add(new Meeting());
                }

                upCountMeetings = !upCountMeetings;

                if (upCountMeetings)
                {
                    countMeetings *= 2;
                    stage *= 2;
                }
                tournamentGrid.Add(round);
            }
            tournamentGrid.Reverse();
            return tournamentGrid;
        }

        public List<Round>[] CreateDoubleEliminationTournament(List<string> players)
        {
            var grids = new List<Round>[2];
            var singleConstructor = new SingleConstructorTournament();
            var doubleConstructor = new DoubleConstructorTournament();

            var lowerGrid = doubleConstructor.CreateTournament(players);
            var upperGrid = singleConstructor.CreateTournament(players);


            grids[0] = upperGrid;
            grids[1] = lowerGrid;
            return grids;
        }
    }
}
