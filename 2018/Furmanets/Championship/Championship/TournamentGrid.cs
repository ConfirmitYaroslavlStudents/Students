using System.Collections.Generic;

namespace Championship
{
    public class TournamentGrid
    {
        public List<Meeting> Tournament = new List<Meeting>();

        public void CreateTournamentGrid(int players)
        {
            var currentStage = new StageIndicator();
            var stages = 1;
            var countNextMeetings = 1;
            var countMeetings = 1;

            while (stages < players)
            {
                var countPreviousStage = Tournament.Count;
                for (var i = 0; i < countMeetings; i++)
                {
                    Tournament.Add(new Meeting());
                    Tournament[i + countPreviousStage].Stage = currentStage.Stage;
                }

                if (currentStage.Stage != "final")
                {
                    var meetingIndex = countNextMeetings;

                    for (var i = 0; i < countNextMeetings; i++)
                    {
                        Tournament[meetingIndex].NextStage = Tournament[i];
                        Tournament[meetingIndex + 1].NextStage = Tournament[i];
                        meetingIndex += 2;
                    }

                    for (var i = 0; i < countNextMeetings; i++)
                    {
                        Tournament.RemoveAt(0);
                    }
                    countNextMeetings *= 2;
                }
                countMeetings *= 2;
                currentStage.NextStage();
                stages *= 2;
            }
        }
    }
}
