using System;
using System.Collections.Generic;

namespace Championship
{
    internal static class DataValidation
    {
        public static int CheckInteger()
        {
            int number = -1;

            while (!int.TryParse(Console.ReadLine(), out number) || number < 0)
            {
                Console.Write("Enter a positive integer.\nTry again:");
            }

            return number;
        }

        public static int CheckCountOfTeam()
        {
            int count = -1;

            while (!int.TryParse(Console.ReadLine(), out count) || !IsPowerOfTwo(count))
            {
                Console.Write("Enter the count of teams multiple of a power of two.\nTry again:");
            }

            return count;
        }

        private static bool IsPowerOfTwo(int number)
        {
            if (number == 1)
            {
                return false;
            }

            while (number % 2 == 0)
                number /= 2;

            return number == 1;
        }

        public static int PowerOfTwo(int number)
        {
            int pow = 0;

            while (number % 2 == 0)
            {
                number /= 2;
                pow++;
            }

            return pow;
        }

        public static string CheckTeamName(Queue<string> teams)
        {
            string TeamName = Console.ReadLine();

            while (teams.Contains(TeamName))
            {
                Console.Write("This name is already used.Try again:");
                TeamName = Console.ReadLine();
            }
             
            return TeamName;
        }
    }
}
