using System;

namespace Championship
{
    [Serializable]
    public class Meeting
    {
        public string FirstPlayer;
        public string SecondPlayer;
        public int[] Score;
        public Meeting NextStage;
        public MeetingWinningIndicator Winner;

        public Meeting()
        {
            Score = new int[2];
            Winner = MeetingWinningIndicator.MatchDidNotTakePlace;
        }

        public Meeting(Meeting meeting)
        {
            FirstPlayer = meeting.SecondPlayer;
            SecondPlayer = meeting.SecondPlayer;
            Score = meeting.Score;
            NextStage = meeting.NextStage;
            Winner = meeting.Winner;
        }
    }
}
