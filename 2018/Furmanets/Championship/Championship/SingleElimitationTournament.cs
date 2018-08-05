using System;
using System.Collections.Generic;

namespace Championship
{
    [Serializable]
    public class SingleElimitationTournament : Tournament
    {
        private readonly List<Round> _tournamentRounds;
        private int _indexOfRound;
        private int _indexOfMatch;

        public SingleElimitationTournament(List<string> players)
        {
            _tournamentRounds = ConstructorTournament.CreateSingleEliminationTournament(players);
            _indexOfRound = 0;
        }

        public override void CollectorResults(int[] resultMatch)
        {
            var round = _tournamentRounds[_indexOfRound];
            var meeting = round.Meetings[_indexOfMatch];

            meeting.Score = resultMatch;
            ChooseWinner(meeting);

            _indexOfMatch++;

            if (round.Meetings.Count == _indexOfMatch || 
                round.Meetings[_indexOfMatch].FirstPlayer == null || 
                round.Meetings[_indexOfMatch].SecondPlayer == null)
            {
                _indexOfMatch = 0;
                _indexOfRound++;
            }
        }

        public override List<Round> GetTournamentToPrint()
        {
            return CloneTournament(_tournamentRounds);
        }

        private void ChooseWinner(Meeting meeting)
        {
            if (meeting.SecondPlayer == null || meeting.FirstPlayer == null)
                return;

            if (meeting.Score[0] == meeting.Score[1])
            {
                throw new Exception("The score in the match can't be equal.");
            }

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
