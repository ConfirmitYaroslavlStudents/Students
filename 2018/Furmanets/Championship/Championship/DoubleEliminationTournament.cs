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

        public DoubleEliminationTournament(List<string> players)
        {
            _lowerGrid = ConstructorTournament.CreateTournamentGrid(players.Count);
            _upperGrid = ConstructorTournament.CreateTournament(players);
            _finalUpperAndLowerGrids = new Meeting();
            _upperGrid[_upperGrid.Count - 1].Meetings[0].NextStage = _finalUpperAndLowerGrids;
            _lowerGrid[_lowerGrid.Count - 1].Meetings[0].NextStage = _finalUpperAndLowerGrids;
            _indexOfRound = 0;
        }

        public override void CollectorResults(List<int[]> resultsMatches)
        {
            var roundUpper = _upperGrid[_indexOfRound];
            var roundLower = _upperGrid[_indexOfRound];

            var indexInListResults = 0;
            for (var i = 0; i < roundUpper.Meetings.Count; i++)
            {
                if (roundUpper.Meetings[i].SecondPlayer == null || roundUpper.Meetings[i].FirstPlayer == null)
                {
                    indexInListResults = i;
                    break;
                }

                roundUpper.Meetings[i].Score = resultsMatches[i];
                ChooseWinner(roundUpper.Meetings[i], true);
                indexInListResults = i;
            }

            foreach (var meeting in roundLower.Meetings)
            {
                if (indexInListResults == resultsMatches.Count)
                {
                    break;
                }
                meeting.Score = resultsMatches[indexInListResults];
                indexInListResults++;
                ChooseWinner(meeting, false);
            }
            _indexOfRound++;
        }

        public override List<Round> GetTournamentToPrint()
        {
            var tournament = new List<Round>();
            tournament.AddRange(_upperGrid);

            var indexRound = 0;
            foreach (var round in _lowerGrid)
            {
                foreach (var meeting in round.Meetings)
                {
                    tournament[indexRound].Meetings.Add(new Meeting(meeting));
                }
                indexRound++;
            }
            var finalRound = new Round();
            finalRound.Meetings.Add(_finalUpperAndLowerGrids);
            tournament.Add(finalRound);
            return tournament;
        }

        public override int GetIndexOfRound()
        {
            return _indexOfRound;
        }

        protected void ChooseWinner(Meeting meeting, bool isUpper)
        {
            if (meeting.SecondPlayer == null || meeting.FirstPlayer == null)
                return;

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