using System;
using System.Collections.Generic;

namespace Championship
{
    [Serializable]
    public class Tournament
    {
        public List<Round> TournamentRounds;
        public int IndexOfRound;

        public Tournament(List<string> players)
        {
            TournamentRounds = ConstructorTournament.CreateTournament(players);
            IndexOfRound = 0;
        }

        public void CollectorResults(List<int[]> resultsMatches)
        {
            var round = TournamentRounds[IndexOfRound];

            for (var i = 0; i < resultsMatches.Count; i++)
            {
                round.Meetings[i].Score = resultsMatches[i];
                ChooseWinner(round.Meetings[i]);
            }
            IndexOfRound++;
        }

        private void ChooseWinner(Meeting meeting)
        {
            if (meeting.SecondPlayer == null || meeting.FirstPlayer == null)
                return;

            if (meeting.Score[0] > meeting.Score[1])
            {
                PromoteWinner(meeting, meeting.FirstPlayer);
                meeting.Winner = MeetingWinningIndicator.FirstPlayer;
            }
            else
            {
                PromoteWinner(meeting, meeting.SecondPlayer);
                meeting.Winner = MeetingWinningIndicator.SecondPlayer;
            }
        }

        private void PromoteWinner(Meeting meeting, string player)
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
