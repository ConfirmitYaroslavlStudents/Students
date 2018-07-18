using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship
{
    public class TournamentGrid
    {
        public List<Meeting> Tournament;

        public TournamentGrid()
        {
            Tournament = new List<Meeting>();
        }

        public void CreateTournamentGrid(int players)
        {
            var currentStage = new StageIndicator();
            var stages = 1;
            var countNextMeetings = 1;
            var countMeetings = 1;

            while (stages < players)
            {
                var countPrevStage = Tournament.Count;
                for (var i = 0; i < countMeetings; i++)
                {
                    Tournament.Add(new Meeting());
                    Tournament[i + countPrevStage].Stage = currentStage.Stage;
                }

                if (currentStage.Stage != "final")
                {
                    var indexmeeting = countNextMeetings;

                    for (var i = 0; i < countNextMeetings; i++)
                    {
                        Tournament[indexmeeting].NextStage = Tournament[i];
                        Tournament[indexmeeting + 1].NextStage = Tournament[i];
                        indexmeeting += 2;
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
