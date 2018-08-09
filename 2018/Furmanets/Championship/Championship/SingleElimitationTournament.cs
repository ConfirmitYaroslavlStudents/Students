using System;
using System.Collections.Generic;

namespace Championship
{
    [Serializable]
    public class SingleElimitationTournament : Tournament
    {
        private readonly List<Round> _tournamentRounds;

        public SingleElimitationTournament(List<string> players)
        {
            var singleconstructor = new SingleConstructorTournament();
            _tournamentRounds = singleconstructor.CreateTournament(players);
            IndexOfRound = 0;
        }

        public override void CollectorResults(int[] resultMatch)
        {
            var round = _tournamentRounds[IndexOfRound];
            var meeting = round.Meetings[IndexOfMatch];

            meeting.Score = resultMatch;
            ChooseWinner(meeting);

            IndexOfMatch++;

            if (round.Meetings.Count == IndexOfMatch || 
                round.Meetings[IndexOfMatch].FirstPlayer == null || 
                round.Meetings[IndexOfMatch].SecondPlayer == null)
            {
                IndexOfMatch = 0;
                IndexOfRound++;
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
