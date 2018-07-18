using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Championship
{
    public class Tournament
    {
        public TournamentGrid Standings;
        private Paint _paint = new Paint();
        private List<string> _players;
        private int _playersCount;
        private int _stage;
        string separator = "*****************************************************";

        public void AddingPlayers()
        {
            Console.WriteLine("Enter the players, to complete the input, use Z");
            List<string> players = new List<string>();

            for (int i = 0; ; i++)
            {
                var currentPalyer = Console.ReadLine();

                if (currentPalyer == "Z" || currentPalyer == "z")
                {
                    break;
                }
                players.Add(currentPalyer);
            }
            PlayerPlacement(players);
        }

        public void PlayerPlacement(List<string> players)
        {
            var random = new Random();
            _playersCount = players.Count;
            _players = new List<string>();
            for (int i = 0; i < _playersCount; i++)
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
                meeting.PlayerFirst = _players[indexPlayer];
                indexPlayer++;
            }

            foreach (var meeting in Standings.Tournament)
            {
                meeting.PlayerSecond = _players[indexPlayer];
                indexPlayer++;
                if (_players.Count == indexPlayer)
                {
                    break;
                }
            }

            _paint.PaintTournamentStage(Standings);
        }

        public void CollectorResults()
        {
            var NextRoundGrid = new TournamentGrid();
            foreach (var meeting in Standings.Tournament)
            {
                if (meeting.PlayerSecond != null)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.Write("Write score: ");
                    Console.WriteLine(meeting.PlayerFirst + " vs " + meeting.PlayerSecond);
                    var result = Console.ReadLine().Split(new[] {' ', ':'}, StringSplitOptions.RemoveEmptyEntries);
                    meeting.Score = result[0] + ":" + result[1];
                    if (Convert.ToInt32(result[0]) > Convert.ToInt32(result[1]))
                    {
                        if (meeting.Stage == "final")
                        {
                            Console.Clear();
                            Console.WriteLine("Winner! "+ meeting.PlayerFirst+"!"+ "  Congratulations!");
                            Console.ReadKey();
                        }
                        else
                        {
                            PromoteWinner(meeting, meeting.PlayerFirst, ref NextRoundGrid);
                        }
                    }
                    else
                    {
                        if (meeting.Stage == "final")
                        {
                            Console.Clear();
                            Console.WriteLine("Winner! " + meeting.PlayerSecond + "Congratulations!");
                            Console.ReadKey();
                        }
                        else
                        {
                            PromoteWinner(meeting, meeting.PlayerSecond, ref NextRoundGrid);
                        }
                    }
                }
                else
                {
                    if (meeting.Stage == "final")
                    {
                        Console.Clear();
                        Console.WriteLine("Winner! " + meeting.PlayerFirst + "Congratulations!");
                        Console.ReadKey();
                    }
                    else
                    {
                        PromoteWinner(meeting, meeting.PlayerFirst, ref NextRoundGrid);
                    }
                }
            }

            Console.Clear();
            _paint.PaintTournamentRound(Standings);
            Standings = NextRoundGrid;

            if (Standings.Tournament.Count != 0)
            {
                _paint.PaintTournamentStage(Standings);
                CollectorResults();
            }
        }

        private static void PromoteWinner(Meeting meeting, string player, ref TournamentGrid nextRoundGrid)
        {
            if (meeting.NextStage.PlayerFirst == null)
            {
                meeting.NextStage.PlayerFirst = player;
            }
            else
            {
                meeting.NextStage.PlayerSecond = player;
                nextRoundGrid.Tournament.Add(meeting.NextStage);
            }
        }
    }
}
