using ConsoleChampionship;
using System;
using System.Collections.Generic;

namespace Championship
{
    public class Tournament
    {
        public TournamentGrid Grid;
        public List<string> Players = new List<string>();
        private int _playersCount;
        public string PlayerWinner;
        public int IndexRound = 0;

        public void PlayerPlacement(List<string> players)
        {
            var random = new Random();
            _playersCount = players.Count;
            Players = new List<string>();

            for (var i = 0; i < _playersCount; i++)
            {
                var currentPlayer = players[random.Next(0, players.Count)];
                Players.Add(currentPlayer);
                players.Remove(currentPlayer);
            }

            Grid = new TournamentGrid();
            Grid.CreateTournamentGrid(_playersCount);

            var indexPlayer = 0;
            var countFirstStageMeetings = Players.Count - Grid.Tournament[0].Meetings.Count;

            for (int i = 0; i < countFirstStageMeetings; i++)
            {
                Grid.Tournament[0].Meetings[i].FirstPlayer = Players[indexPlayer];
                Grid.Tournament[0].Meetings[i].SecondPlayer = Players[indexPlayer + 1];
                indexPlayer += 2;
            }

            if (Grid.Tournament.Count <= 1)
            {
                return;
            }

            for (var i = Grid.Tournament[1].Meetings.Count - 1; i >= countFirstStageMeetings / 2; i--)
            {
                try
                {
                    Grid.Tournament[1].Meetings[i].SecondPlayer = Players[indexPlayer];
                    Grid.Tournament[1].Meetings[i].FirstPlayer = Players[indexPlayer + 1];
                    indexPlayer += 2;
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }

        }

        public void CollectorResults()
        {
            var round = Grid.Tournament[IndexRound];

            foreach (var meeting in round.Meetings)
            {
                meeting.Score = Draftsman.GetResultOfMatch(meeting);
                ChoiceWinner(meeting);
            }
            IndexRound++;
        }

        private void ChoiceWinner(Meeting meeting)
        {
            if (meeting.SecondPlayer != null && meeting.FirstPlayer != null)
            {
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
                PlayerWinner = player;
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
