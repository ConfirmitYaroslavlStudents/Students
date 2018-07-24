using System;
using System.Collections.Generic;

namespace Championship
{
    public class Draftsman
    {
        public static void PaintTournamentStage(Round round)
        {
            Console.Clear();
            Console.WriteLine(GetForPrintRound(round.Stage));
            Console.WriteLine();

            foreach (var meeting in round.Meetings)
            {
                if (meeting.FirstPlayer == null && meeting.SecondPlayer == null)
                {
                    continue;
                }

                Console.WriteLine(meeting.FirstPlayer + " vs " + meeting.SecondPlayer);
            }
            Console.WriteLine("Tap Enter ...");
            Console.ReadKey();
        }

        public static void PaintTournamentRound(Round round)
        {
            Console.WriteLine(GetForPrintRound(round.Stage));
            Console.WriteLine();

            foreach (var meeting in round.Meetings)
            {
                if (meeting.FirstPlayer == null && meeting.SecondPlayer == null)
                {
                    continue;
                }

                var scorePaint = meeting.Score[0] + ":" + meeting.Score[1];

                Console.WriteLine(meeting.FirstPlayer + " " + scorePaint + " " + meeting.SecondPlayer);
            }

            Console.WriteLine("Tap Enter ...");
            Console.ReadKey();
        }

        public static List<string> AddPlayers()
        {
            //Console.WriteLine("Enter the players, to complete the input, use Z");
            List<string> players = new List<string>();
            players.Add("Петя");
            players.Add("Вова");
            players.Add("Коля");
            players.Add("Евгений");
            players.Add("Катя");
            players.Add("Митяй");
            players.Add("Глеб");
            players.Add("Максим");
            // players.Add("Олег");
            //for (; ; )
            //{
            //    var currentPalyer = Console.ReadLine();

            //    if (currentPalyer == "Z" || currentPalyer == "z")
            //    {
            //        break;
            //    }
            //    players.Add(currentPalyer);
            //}
            return players;
        }

        public static int[] GetResultOfMatch(Meeting meeting)
        {
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
            GrafGrid grafToPrint = new GrafGrid();
            grafToPrint.ConstructionGraf(grid);

            foreach (var line in grafToPrint.Graf)
            {
                foreach (var letter in line)
                {
                    if (letter == '@')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        continue;
                    }
                    if (letter == '$')
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    Console.Write(letter);
                }
                Console.WriteLine();
            }
        }

        public static string GetForPrintRound(int round)
        {
            if (round == 1)
            {
                return "final";
            }

            return "1/" + round;
        }
    }
}
