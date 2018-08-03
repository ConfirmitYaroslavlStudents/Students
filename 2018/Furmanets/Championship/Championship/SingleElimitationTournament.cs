using System;
using System.Collections.Generic;

namespace Championship
{
    [Serializable]
    public class SingleElimitationTournament:Tournament
    {
        private readonly List<Round> _tournamentRounds;
        private int _indexOfRound;

        public SingleElimitationTournament(List<string> players)
        {
            _tournamentRounds = ConstructorTournament.CreateTournament(players);
            _indexOfRound = 0;
        }

        public override void CollectorResults(List<int[]> resultsMatches)
        {
            var round = _tournamentRounds[_indexOfRound];

            for (var i = 0; i < resultsMatches.Count; i++)
            {
                round.Meetings[i].Score = resultsMatches[i];
                ChooseWinner(round.Meetings[i]);
            }
            _indexOfRound++;
        }

        public override List<Round> GetTournamentToPrint()
        {
            return _tournamentRounds;
        }

        public override int GetIndexOfRound()
        {
            return _indexOfRound;
        }

        private void ChooseWinner(Meeting meeting)
        {
            if (meeting.SecondPlayer == null || meeting.FirstPlayer == null)
                return;

            if (meeting.Score[0] > meeting.Score[1])
            {
                PromotionWinnerToNextStage(meeting, meeting.FirstPlayer);
                meeting.Winner = MeetingWinningIndicator.FirstPlayer;
            }
            else
            {
                PromotionWinnerToNextStage(meeting, meeting.SecondPlayer);
                meeting.Winner = MeetingWinningIndicator.SecondPlayer;
            }
        }
    }
}
