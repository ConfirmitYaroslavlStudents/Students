using System;
using System.Collections.Generic;

namespace Championship
{
    [Serializable]
    public class DoubleEliminationTournament : Tournament
    {
        private readonly List<Round> _upperGrid;
        private readonly List<Round> _lowerGrid;
        private int _indexLowerRounds;
        private int _indexLowerMeetings;
        private bool _nextMatchIsUpper = true;
        public Meeting FinalUpperAndLowerGrids { get; private set; }


        public DoubleEliminationTournament(List<string> players)
        {
            var doubleConstructorTournament = new DoubleConstructorGrid();
            var singleConstructorTournament = new SingleConstructorTournament();

            _lowerGrid = doubleConstructorTournament.CreateTournamentGrid(players.Count);
            _upperGrid = singleConstructorTournament.CreateTournament(players);

            FinalUpperAndLowerGrids = new Meeting();
            _upperGrid[_upperGrid.Count - 1].Meetings[0].NextStage = FinalUpperAndLowerGrids;
            _lowerGrid[_lowerGrid.Count - 1].Meetings[0].NextStage = FinalUpperAndLowerGrids;

            IndexOfRound = 0;
            IndexOfMatch = 0;
            _indexLowerRounds = 0;
            _indexLowerMeetings = 0;
        }

        public override void CollectorResults(int[] resultMatch)
        {
            var currentMeeting = NextMeeting();

            currentMeeting.Score = resultMatch;
            ChooseWinner(currentMeeting, _nextMatchIsUpper);
            if (_nextMatchIsUpper)
            {
                IndexOfMatch++;
                if (_upperGrid[IndexOfRound].Meetings.Count <= IndexOfMatch)
                {
                    _nextMatchIsUpper = !_nextMatchIsUpper;
                    IndexOfMatch = 0;
                    IndexOfRound++;
                }
            }
            else
            {
                _indexLowerMeetings++;

                if (currentMeeting.Equals(FinalUpperAndLowerGrids))
                {
                    IndexOfRound++;
                    return;
                }

                if (_lowerGrid[_indexLowerRounds].Meetings.Count <= _indexLowerMeetings)
                {
                    if (_indexLowerRounds % 2 == 0)
                    {
                        _nextMatchIsUpper = !_nextMatchIsUpper;
                    }

                    _indexLowerMeetings = 0;
                    _indexLowerRounds++;
                }
            }
        }

        public override List<Round> GetTournamentToPrint()
        {
            return CloneTournament(_upperGrid);
        }

        public List<Round> GetLowerGrid()
        {
            return CloneTournament(_lowerGrid);
        }

        public override Meeting NextMeeting()
        {
            if (IndexOfRound > _upperGrid.Count + 1)
            {
                throw new Exception("All matches are over");
            }

            if (IndexOfRound == _upperGrid.Count)
            {
                return FinalUpperAndLowerGrids;
            }

            if (_nextMatchIsUpper)
            {
                return _upperGrid[IndexOfRound].Meetings[IndexOfMatch];
            }

            return _lowerGrid[_indexLowerRounds].Meetings[_indexLowerMeetings];
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

        protected void PromotionLoserToNextStage(string player)
        {
            var round = _lowerGrid[IndexOfRound];

            foreach (var meeting in round.Meetings)
            {
                if (meeting.SecondPlayer == null)
                {
                    meeting.SecondPlayer = player;
                    return;
                }

                if (meeting.FirstPlayer == null)
                {
                    meeting.FirstPlayer = player;
                    return;
                }
            }

            IndexOfRound++;
            PromotionLoserToNextStage(player);
            IndexOfRound--;
        }
    }
}