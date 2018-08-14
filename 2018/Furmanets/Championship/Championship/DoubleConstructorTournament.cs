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
            var countNextMeeting = 1;
            var upCountMeetings = true;

            while (stage < players.Count / 2)
            {
                var round = new Round { Stage = stage };

                for (var i = 0; i < countMeetings; i++)
                {
                    round.Meetings.Add(new Meeting());
                }
                tournamentGrid.Add(round);

                upCountMeetings = !upCountMeetings;

                if (tournamentGrid.Count != 1)
                {
                    var meetingIndex = 0;

                    for (var i = 0; i < countNextMeeting; i++)
                    {
                        if (tournamentGrid.Count == 2)
                        {
                            tournamentGrid[tournamentGrid.Count - 1].Meetings[meetingIndex].NextStage =
                                tournamentGrid[tournamentGrid.Count - 2].Meetings[i];
                            break;
                        }
                        if (countNextMeeting == countMeetings)
                        {
                            tournamentGrid[tournamentGrid.Count - 1].Meetings[meetingIndex].NextStage =
                                tournamentGrid[tournamentGrid.Count - 2].Meetings[i];

                            tournamentGrid[tournamentGrid.Count - 1].Meetings[meetingIndex + 1].NextStage =
                                tournamentGrid[tournamentGrid.Count - 2].Meetings[i+1];
                            i++;
                        }
                        else
                        {
                            tournamentGrid[tournamentGrid.Count - 1].Meetings[meetingIndex].NextStage =
                                tournamentGrid[tournamentGrid.Count - 2].Meetings[i];

                            tournamentGrid[tournamentGrid.Count - 1].Meetings[meetingIndex + 1].NextStage =
                                tournamentGrid[tournamentGrid.Count - 2].Meetings[i];
                        }

                        meetingIndex += 2;
                    }
                }

                countNextMeeting = countMeetings;

                if (upCountMeetings)
                {
                    countMeetings *= 2;
                    stage *= 2;
                }
            }
            tournamentGrid.Reverse();

            return tournamentGrid;
        }
    }
}
