using System;
using System.Collections.Generic;

namespace Championship
{
    public class Tournament
    {
        public TournamentGrid Grid;
        private List<string> _players;
        private int _playersCount;
        private string _playerWinner;

        public void Start()
        {
            var players = Draftsman.AddPlayers();
            PlayerPlacement(players);
            Draftsman.PaintGraf(Grid);
            CollectorResults();
        }

        private void PlayerPlacement(List<string> players)
        {
            var random = new Random();
            _playersCount = players.Count;
            _players = new List<string>();

            for (var i = 0; i < _playersCount; i++)
            {
                var currentPlayer = players[random.Next(0, players.Count)];
                _players.Add(currentPlayer);
                players.Remove(currentPlayer);
            }

            Grid = new TournamentGrid();
            Grid.CreateTournamentGrid(_playersCount);

            var indexPlayer = 0;
            var countFirstStageMeetings = _players.Count - Grid.Tournament[0].Meetings.Count;

            for (int i = 0; i < countFirstStageMeetings; i++)
            {
                Grid.Tournament[0].Meetings[i].FirstPlayer = _players[indexPlayer];
                Grid.Tournament[0].Meetings[i].SecondPlayer = _players[indexPlayer + 1];
                indexPlayer += 2;
            }

            for (var i = Grid.Tournament[1].Meetings.Count-1; i >= countFirstStageMeetings / 2; i--)
            {
                try
                {
                    Grid.Tournament[1].Meetings[i].SecondPlayer = _players[indexPlayer];
                    Grid.Tournament[1].Meetings[i].FirstPlayer = _players[indexPlayer + 1];
                    indexPlayer += 2;
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }

            }

            Draftsman.PaintTournamentStage(Grid.Tournament[0]);
        }

        private void CollectorResults()
        {
            foreach (var round in Grid.Tournament)
            {
                Draftsman.PaintGraf(Grid);
                Console.ReadKey();
                Draftsman.PaintTournamentStage(round);

                foreach (var meeting in round.Meetings)
                {
                    ChoiceWinner(meeting);
                }

                if (Grid.Tournament.Count != 0)
                {
                    Draftsman.PaintTournamentRound(round);
                }
            }

            Draftsman.CongratulationWinner(_playerWinner);
        }

        private void ChoiceWinner(Meeting meeting)
        {
            if (meeting.SecondPlayer != null&&meeting.FirstPlayer!=null)
            {
                Draftsman.GetResultOfMatch(meeting);

                if (meeting.Score[0] > meeting.Score[1])
                {
                   // meeting.FirstPlayer = "@" + meeting.FirstPlayer + "$";
                    PromoteWinner(meeting, meeting.FirstPlayer);
                }
                else
                {
                   // meeting.SecondPlayer = "@" + meeting.SecondPlayer + "$";
                    PromoteWinner(meeting, meeting.SecondPlayer);
                }
            }
        }

        private void PromoteWinner(Meeting meeting, string player)
        {
            if (meeting.NextStage == null)
            {
                _playerWinner = player;
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
