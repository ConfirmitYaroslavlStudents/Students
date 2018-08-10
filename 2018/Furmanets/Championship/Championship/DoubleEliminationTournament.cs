using System;
using System.Collections.Generic;

namespace Championship
{
    [Serializable]
    public class DoubleEliminationTournament : Tournament
    {
        private readonly List<Round> _upperGrid;
        private readonly List<Round> _lowerGrid;
        private readonly Meeting _finalUpperAndLowerGrids;

        public DoubleEliminationTournament(List<string> players)
        {
            var doubleConstructorTournament = new DoubleConstructorTournament();
            var singleConstructorTournament = new SingleConstructorTournament();

            _lowerGrid = doubleConstructorTournament.CreateTournament(players);
            _upperGrid = singleConstructorTournament.CreateTournament(players);

            _finalUpperAndLowerGrids = new Meeting();
            _upperGrid[_upperGrid.Count - 1].Meetings[0].NextStage = _finalUpperAndLowerGrids;
            _lowerGrid[_lowerGrid.Count - 1].Meetings[0].NextStage = _finalUpperAndLowerGrids;

            IndexOfRound = 0;
            IndexOfMatch = 0;
        }

        public override void CollectorResults(int[] resultMatch)
        {
            if (IndexOfRound > _upperGrid.Count + 1)
            {
                throw new Exception("All matches are over");
            }

            if (IndexOfRound == _upperGrid.Count)
            {
                IndexOfRound++;
                _finalUpperAndLowerGrids.Score = resultMatch;
                return;
            }

            var roundUpper = _upperGrid[IndexOfRound];
            var roundLower = _lowerGrid[IndexOfRound];

            if (IndexOfMatch < roundUpper.Meetings.Count)
            {
                roundUpper.Meetings[IndexOfMatch].Score = resultMatch;
                ChooseWinner(roundUpper.Meetings[IndexOfMatch], true);
                IndexOfMatch++;

                while (roundUpper.Meetings.Count > IndexOfMatch)
                {

                    if (roundUpper.Meetings[IndexOfMatch].FirstPlayer != null &&
                        roundUpper.Meetings[IndexOfMatch].SecondPlayer != null)
                    {
                        return;
                    }

                    if (roundUpper.Meetings[IndexOfMatch].FirstPlayer != null)
                    {
                        PromotionOnePlayerInGameToNextRound(roundUpper.Meetings[IndexOfMatch]);
                    }

                    IndexOfMatch++;
                }
            }
            else
            {
                var indexOfMatchCurrent = IndexOfMatch - roundUpper.Meetings.Count;
                roundLower.Meetings[indexOfMatchCurrent].Score = resultMatch;
                ChooseWinner(roundLower.Meetings[indexOfMatchCurrent], false);
                IndexOfMatch++;
            }

            while (roundLower.Meetings.Count * 2 > IndexOfMatch)
            {
                if (roundLower.Meetings[IndexOfMatch - roundLower.Meetings.Count].FirstPlayer != null &&
                    roundLower.Meetings[IndexOfMatch - roundLower.Meetings.Count].SecondPlayer != null)
                {
                    return;
                }

                if (roundLower.Meetings[IndexOfMatch - roundLower.Meetings.Count].FirstPlayer != null)
                {
                    PromotionOnePlayerInGameToNextRound(roundLower.Meetings[IndexOfMatch - roundLower.Meetings.Count]);
                }

                IndexOfMatch++;
            }

            if (IndexOfMatch >= roundLower.Meetings.Count + roundUpper.Meetings.Count)
            {
                IndexOfMatch = 0;
                IndexOfRound++;
            }
        }

        public override List<Round> GetTournamentToPrint()
        {
            var tournament = new List<Round>();

            tournament.AddRange(CloneTournament(_upperGrid));

            var indexRound = 0;
            for (var i = 0; i < _lowerGrid.Count - 1; i++)
            {
                var round = _lowerGrid[i];

                for (var j = 0; j < round.Meetings.Count; j++)
                {
                    var meeting = round.Meetings[j];
                    tournament[indexRound].Meetings.Add(meeting.CloneMeeting());
                }

                indexRound++;
            }

            tournament.Add(new Round());

            tournament[tournament.Count - 1].Meetings.Add(_lowerGrid[_lowerGrid.Count - 1].Meetings[0]);


            var finalRound = new Round
            {
                Stage = 1
            };
            finalRound.Meetings.Add(_finalUpperAndLowerGrids);
            tournament.Add(finalRound);
            return tournament;
        }

        public override Meeting NextMeeting()
        {
            if (_upperGrid[IndexOfRound].Meetings.Count > IndexOfMatch)
            {
                return _upperGrid[IndexOfRound].Meetings[IndexOfMatch];
            }
            return _lowerGrid[IndexOfRound].Meetings[IndexOfMatch - _upperGrid[IndexOfRound].Meetings.Count];
        }

        protected void ChooseWinner(Meeting meeting, bool isUpper)
        {
            if (meeting.SecondPlayer == null || meeting.FirstPlayer == null)
            {
                throw new Exception("Method ChooseWinner in DoubleElimination; Empty players;");
            }

            if (meeting.Score[0] == meeting.Score[1])
            {
                throw new Exception("The score in the match can't be equal.");
            }

            if (meeting.Score[0] > meeting.Score[1])
            {
                PromotionWinnerToNextStage(meeting, meeting.FirstPlayer);
                if (isUpper)
                {
                    PromotionLoserToNextStage(meeting.SecondPlayer);
                }
                meeting.Winner = MeetingWinningIndicator.FirstPlayer;
            }
            else
            {
                PromotionWinnerToNextStage(meeting, meeting.SecondPlayer);
                if (isUpper)
                {
                    PromotionLoserToNextStage(meeting.FirstPlayer);
                }
                meeting.Winner = MeetingWinningIndicator.SecondPlayer;
            }
        }

        protected void PromotionOnePlayerInGameToNextRound(Meeting meeting)
        {
            meeting.NextStage.SecondPlayer = meeting.FirstPlayer;
        }

        protected void PromotionLoserToNextStage(string player)
        {
            var round = _lowerGrid[IndexOfRound];

            for (var i = 0; i < round.Meetings.Count; i++)
            {
                var meeting = round.Meetings[i];

                if (meeting.FirstPlayer == null)
                {
                    meeting.FirstPlayer = player;
                    return;
                }
                if (meeting.SecondPlayer == null)
                {
                    meeting.SecondPlayer = player;
                    return;
                }

            }
        }
    }
}