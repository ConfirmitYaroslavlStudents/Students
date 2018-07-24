using System.Collections.Generic;

namespace Championship
{
    public class TournamentGrid
    {
        public List<Round> Tournament = new List<Round>();

        public void CreateTournamentGrid(int players)
        {
            var currentStage = new StageIndicator();
            var stages = 1;
            var countNextMeetings = 1;
            var countMeetings = 1;

            while (stages < players)
            {
                Tournament.Add(new Round());
                Tournament[Tournament.Count - 1].Stage = currentStage.Stage;

                for (var i = 0; i < countMeetings; i++)
                {
                    Tournament[Tournament.Count - 1].Meetings.Add(new Meeting());
                }

                if (Tournament.Count != 1)
                {
                    var meetingIndex = 0;

                    for (var i = 0; i < countNextMeetings; i++)
                    {
                        Tournament[Tournament.Count - 1].Meetings[meetingIndex].NextStage = 
                            Tournament[Tournament.Count - 2].Meetings[i];

                        Tournament[Tournament.Count - 1].Meetings[meetingIndex + 1].NextStage = 
                            Tournament[Tournament.Count - 2].Meetings[i];

                        meetingIndex += 2;
                    }

                    countNextMeetings *= 2;
                }

                countMeetings *= 2;
                currentStage.NextStage();
                stages *= 2;
            }

            Tournament.Reverse();
        }
    }
}
