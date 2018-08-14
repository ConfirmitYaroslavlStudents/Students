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

        public Meeting CloneMeeting()
        {
            var newCloneMeeting = new Meeting();
            if (this.FirstPlayer != null)
            {
                newCloneMeeting.FirstPlayer = string.Copy(FirstPlayer);
            }

            if (SecondPlayer != null)
            {
                newCloneMeeting.SecondPlayer = string.Copy(SecondPlayer);
            }

            Score.CopyTo(newCloneMeeting.Score, 0);

            if (NextStage != null)
            {
                newCloneMeeting.NextStage = NextStage.CloneMeeting();
            }

            newCloneMeeting.Winner = Winner;
            return newCloneMeeting;
        }
    }
}
