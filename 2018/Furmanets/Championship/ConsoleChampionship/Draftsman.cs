using System;
using System.Linq;
using System.Threading;
using Championship;

namespace ConsoleChampionship
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
            Console.WriteLine("Press any key to go back...");
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
            Console.WriteLine();
        }

        public static int[] GetResultOfMatch(Meeting meeting)
        {
            Console.WriteLine();
            Console.Write("Write score: ");
            Console.WriteLine(meeting.FirstPlayer + " vs " + meeting.SecondPlayer);
            var resultsMatch = Console.ReadLine()?.Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

            if (resultsMatch == null || resultsMatch.Length != 2 || resultsMatch[0] == resultsMatch[1])
            {
                Console.WriteLine("Incorrect match result data, please re-enter.");
                return GetResultOfMatch(meeting);
            }

            if (resultsMatch.SelectMany(line => line).Any(digit => !char.IsDigit(digit)))
            {
                Console.WriteLine("Incorrect match result data, please re-enter.");
                return GetResultOfMatch(meeting);
            }

            meeting.Score[0] = int.Parse(resultsMatch[0]);
            meeting.Score[1] = int.Parse(resultsMatch[1]);

            return meeting.Score;
        }

        public static void CongratulationWinner(string namePlayer)
        {
            Console.Clear();
            Console.WriteLine("Winner! " + namePlayer + "! " + "  Congratulations!");
            Thread.Sleep(2000);
        }

        public static void PaintGraf(Tournament tournament)
        {
            Console.Clear();

            var maxLengthNamePlayer = tournament.Players.Select(player => player.Length).Concat(new[] { 0 }).Max();

            var grafToPrint = new GrafGrid(maxLengthNamePlayer);
            grafToPrint.ConstructionGraf(tournament);

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
