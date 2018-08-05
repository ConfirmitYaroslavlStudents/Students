using System;
using System.Linq;
using Championship;

namespace ConsoleChampionship
{
    public class UserInteractor
    {
        public static int[] GetResultOfMatch(Meeting meeting)
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Write score: ");
                Console.WriteLine(meeting.FirstPlayer + " vs " + meeting.SecondPlayer);
                var resultsMatch = Console.ReadLine()?.Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (resultsMatch == null || resultsMatch.Length != 2 || resultsMatch[0] == resultsMatch[1])
                {
                    Console.WriteLine("Incorrect match result data, please re-enter.");
                    continue;
                }

                if (resultsMatch.SelectMany(line => line).Any(digit => !char.IsDigit(digit)))
                {
                    Console.WriteLine("Incorrect match result data, please re-enter.");
                    continue;
                }

                meeting.Score[0] = int.Parse(resultsMatch[0]);
                meeting.Score[1] = int.Parse(resultsMatch[1]);

                return meeting.Score;
            }
        }
    }
}
