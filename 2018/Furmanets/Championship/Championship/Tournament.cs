using System;
using System.Collections.Generic;

namespace Championship
{
    [Serializable]
    public abstract class Tournament
    {
        public abstract void CollectorResults(List<int[]> resultsMatches);
        public abstract List<Round> GetTournamentToPrint();
        public abstract int GetIndexOfRound();
        protected virtual void PromotionWinnerToNextStage(Meeting meeting, string player)
        {
            if (meeting.NextStage == null)
            {
                return;
            }

            if (meeting.NextStage.FirstPlayer == null)
            {
                meeting.NextStage.FirstPlayer = player;
            }
            else
            {
                meeting.NextStage.SecondPlayer = player;
            }
        }
    }
}
