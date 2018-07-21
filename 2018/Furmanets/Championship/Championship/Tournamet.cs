using System;
using System.Collections.Generic;

namespace Championship
{
    public class Tournament
    {
        public TournamentGrid Standings;
        private List<string> _players;
        private int _playersCount;

        public void Start()
        {
            var players = Draftsman.AddPlayers();
            PlayerPlacement(players);
            Draftsman.PaintGraf(Standings);
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

            Standings = new TournamentGrid();
            Standings.CreateTournamentGrid(_playersCount);

            var indexPlayer = 0;

            foreach (var meeting in Standings.Tournament)
            {
                meeting.FirstPlayer = _players[indexPlayer];
                indexPlayer++;
            }

            foreach (var meeting in Standings.Tournament)
            {
                meeting.SecondPlayer = _players[indexPlayer];
                indexPlayer++;
                if (_players.Count == indexPlayer)
                {
                    break;
                }
            }

            Draftsman.PaintTournamentStage(Standings);
        }

        private void CollectorResults()
        {
            var nextRoundGrid = new TournamentGrid();
            foreach (var meeting in Standings.Tournament)
            {
                ChoiseWinner(meeting, nextRoundGrid);
            }

            Console.Clear();
            Draftsman.PaintTournamentRound(Standings);
            Standings = nextRoundGrid;

            if (Standings.Tournament.Count != 0)
            {
                Draftsman.PaintTournamentStage(Standings);
                CollectorResults();
            }
        }

        private void ChoiseWinner(Meeting meeting, TournamentGrid nextRoundGrid)
        {
            if (meeting.SecondPlayer != null)
            {
                Draftsman.GetResultOfMatch(meeting);

                if (meeting.Score[0] > meeting.Score[1])
                {
                    if (meeting.Stage == "final")
                    {
                        Draftsman.CongratulationWinner(meeting.FirstPlayer);
                    }
                    else
                    {
                        PromoteWinner(meeting, meeting.FirstPlayer, nextRoundGrid);
                    }
                }
                else
                {
                    if (meeting.Stage == "final")
                    {
                        Draftsman.CongratulationWinner(meeting.SecondPlayer);
                    }
                    else
                    {
                        PromoteWinner(meeting, meeting.SecondPlayer, nextRoundGrid);
                    }
                }
            }
            else
            {
                if (meeting.Stage == "final")
                {
                    Draftsman.CongratulationWinner(meeting.SecondPlayer);
                }
                else
                {
                    PromoteWinner(meeting, meeting.FirstPlayer, nextRoundGrid);
                }
            }
        }

        private void PromoteWinner(Meeting meeting, string player, TournamentGrid nextRoundGrid)
        {

            if (meeting.NextStage.FirstPlayer == null)
            {
                meeting.NextStage.FirstPlayer = player;
            }
            else
            {
                meeting.NextStage.SecondPlayer = player;
                nextRoundGrid.Tournament.Add(meeting.NextStage);
            }
        }
    }
}
