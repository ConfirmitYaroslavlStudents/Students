using System.Collections.Generic;

namespace Championship
{
    public class DoubleGridConstructor
    {
        public List<Round> CreateTournamentGrid(int countPlayers)
        {
            var tournamentGrid = new List<Round>();
            var stage = 1;
            var stageRound = 0;
            var countMeetings = 1;
            var countNextMeeting = 1;
            var upCountMeetings = true;

            while (stage < countPlayers / 2)
            {
                var round = new Round { Stage = stageRound };
                stageRound++;

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
            stage = 1;

            foreach (var round in tournamentGrid)
            {
                round.Stage = stage;
                stage++;
            }
                
            return tournamentGrid;
        }
    }
}
