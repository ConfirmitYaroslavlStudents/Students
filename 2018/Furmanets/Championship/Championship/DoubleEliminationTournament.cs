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
        private int _indexOfRound;
        private int _indexOfMatch;

        public DoubleEliminationTournament(List<string> players)
        {
            _lowerGrid = ConstructorTournament.CreateTournamentGrid(players.Count);
            _upperGrid = ConstructorTournament.CreateTournament(players);
            for (var i = 0; i < _upperGrid.Count; i++)
            {
                _upperGrid[i].Stage *= 2;
                _lowerGrid[i].Stage *= 2;
            }
            _lowerGrid.Add(new Round());
            _lowerGrid[_lowerGrid.Count - 1].Meetings.Add(new Meeting());
            _lowerGrid[_lowerGrid.Count - 1].Stage = 2;

            _finalUpperAndLowerGrids = new Meeting();
            _upperGrid[_upperGrid.Count - 1].Meetings[0].NextStage = _finalUpperAndLowerGrids;
            _lowerGrid[_lowerGrid.Count - 1].Meetings[0].NextStage = _finalUpperAndLowerGrids;
            _indexOfRound = 0;
            _indexOfMatch = 0;
        }

        public override void CollectorResults(int[] resultMatch)
        {
            if (_indexOfRound > _upperGrid.Count + 1)
            {
                throw new Exception("All matches are over");
            }

            if (_indexOfRound == _upperGrid.Count)
            {
                _indexOfRound++;
                _finalUpperAndLowerGrids.Score = resultMatch;
                return;
            }

            var roundUpper = _upperGrid[_indexOfRound];
            var roundLower = _lowerGrid[_indexOfRound];

            if (_indexOfMatch < roundUpper.Meetings.Count)
            {
                roundUpper.Meetings[_indexOfMatch].Score = resultMatch;
                ChooseWinner(roundUpper.Meetings[_indexOfMatch], true);
                _indexOfMatch++;

                while (roundUpper.Meetings.Count > _indexOfMatch)
                {

                    if (roundUpper.Meetings[_indexOfMatch].FirstPlayer != null &&
                        roundUpper.Meetings[_indexOfMatch].SecondPlayer != null)
                    {
                        return;
                    }

                    if (roundUpper.Meetings[_indexOfMatch].FirstPlayer != null)
                    {
                        PromotionOnePlayerInGameToNextRound(roundUpper.Meetings[_indexOfMatch]);
                    }

                    _indexOfMatch++;
                }
            }
            else
            {
                var indexOfMatchCurrent = _indexOfMatch - roundLower.Meetings.Count;
                roundLower.Meetings[indexOfMatchCurrent].Score = resultMatch;
                ChooseWinner(roundLower.Meetings[indexOfMatchCurrent], false);
                _indexOfMatch++;
            }

            while (roundLower.Meetings.Count * 2 > _indexOfMatch)
            {
                if (roundLower.Meetings[_indexOfMatch - roundLower.Meetings.Count].FirstPlayer != null &&
                    roundLower.Meetings[_indexOfMatch - roundLower.Meetings.Count].SecondPlayer != null)
                {
                    return;
                }

                if (roundLower.Meetings[_indexOfMatch - roundLower.Meetings.Count].FirstPlayer != null)
                {
                    PromotionOnePlayerInGameToNextRound(roundLower.Meetings[_indexOfMatch - roundLower.Meetings.Count]);
                }

                _indexOfMatch++;
            }

            if (_indexOfMatch >= roundLower.Meetings.Count * 2)
            {
                _indexOfMatch = 0;
                _indexOfRound++;
            }
        }

        public override List<Round> GetTournamentToPrint()
        {
            var tournament = CloneTournament(_upperGrid);

            var indexRound = 0;
            foreach (var round in _lowerGrid)
            {
                foreach (var meeting in round.Meetings)
                {
                    tournament[indexRound].Meetings.Add(meeting.CloneMeeting());
                }
                indexRound++;
            }

            var finalRound = new Round
            {
                Stage = 1
            };
            finalRound.Meetings.Add(_finalUpperAndLowerGrids);
            tournament.Add(finalRound);
            return tournament;
        }

        public override int GetIndexOfRound()
        {
            return _indexOfRound;
        }

        public override int GetIndexOfMatch()
        {
            return _indexOfMatch;
        }

        protected void ChooseWinner(Meeting meeting, bool isUpper)
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
            if (meeting.NextStage.FirstPlayer == null)
            {
                meeting.NextStage.FirstPlayer = meeting.FirstPlayer;
            }
            else
            {
                meeting.NextStage.SecondPlayer = meeting.FirstPlayer;
            }
        }

        protected void PromotionLoserToNextStage(string player)
        {
            var round = _lowerGrid[_indexOfRound];

            foreach (var meeting in round.Meetings)
            {
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