using System;
using System.Collections.Generic;

namespace Championship
{
    class Draftsman
    {
        public static void PaintTournamentStage(TournamentGrid grid)
        {
            Console.Clear();
            Console.WriteLine(grid.Tournament[0].Stage);
            Console.WriteLine();


            foreach (var meeting in grid.Tournament)
            {
                if (meeting.SecondPlayer == null)
                {
                    Console.WriteLine(meeting.FirstPlayer + " vs " + "----------");
                }
                else
                {
                    Console.WriteLine(meeting.FirstPlayer + " vs " + meeting.SecondPlayer);
                }
            }
            Console.WriteLine("Tap Enter ...");
            Console.ReadKey();
        }

        public static void PaintTournamentRound(TournamentGrid grid)
        {
            Console.Clear();
            Console.WriteLine(grid.Tournament[0].Stage);
            Console.WriteLine();

            foreach (var meeting in grid.Tournament)
            {
                string scorePaint;
                if (meeting.Score[0] == 0 && meeting.Score[1] == 0)
                {
                    scorePaint = "--:--";
                }
                else
                {
                    scorePaint = meeting.Score[0] + ":" + meeting.Score[1];
                }
                if (meeting.SecondPlayer == null)
                {
                    Console.WriteLine(meeting.FirstPlayer + " " + scorePaint + " " + "----------");
                }
                else
                {
                    Console.WriteLine(meeting.FirstPlayer + " " + scorePaint + " " + meeting.SecondPlayer);
                }
            }

            Console.WriteLine("Tap Enter ...");
            Console.ReadKey();
        }

        public static List<string> AddPlayers()
        {
            Console.WriteLine("Enter the players, to complete the input, use Z");
            List<string> players = new List<string>();

            for (; ; )
            {
                var currentPalyer = Console.ReadLine();

                if (currentPalyer == "Z" || currentPalyer == "z")
                {
                    break;
                }
                players.Add(currentPalyer);
            }
            return players;
        }

        public static int[] GetResultOfMatch(Meeting meeting)
        {
            Console.Clear();
            Console.WriteLine();
            Console.Write("Write score: ");
            Console.WriteLine(meeting.FirstPlayer + " vs " + meeting.SecondPlayer);
            var resultsMatch = Console.ReadLine().Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

            meeting.Score[0] = int.Parse(resultsMatch[0]);
            meeting.Score[1] = int.Parse(resultsMatch[1]);

            return meeting.Score;
        }

        public static void CongratulationWinner(string namePlayer)
        {
            Console.Clear();
            Console.WriteLine("Winner! " + namePlayer + "! " + "  Congratulations!");
            Console.ReadKey();
        }

        public static void PaintGraf(TournamentGrid grid)
        {
            Console.Clear();

            foreach (var meeting in grid.Tournament)
            {
                Console.WriteLine(meeting.FirstPlayer);
                Console.WriteLine(meeting.SecondPlayer);
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
