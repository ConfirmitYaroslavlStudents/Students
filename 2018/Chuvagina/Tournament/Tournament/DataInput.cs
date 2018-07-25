using System;
using System.Collections.Generic;

namespace Tournament
{
    internal class DataInput
    {
        public static int InputAmount(OrganizedTournament.KindOfBracket kindOfBracket)
        {
            int result;
            bool isAmountSet;
            do
            {
                if (kindOfBracket == OrganizedTournament.KindOfBracket.PlayOff)
                    Console.WriteLine("PlayOff bracket requires power of two amount of participants.");

                Console.Write("Amount of participants: ");

                if (kindOfBracket == OrganizedTournament.KindOfBracket.PlayOff)
                    isAmountSet = int.TryParse(Console.ReadLine(), out result) && PowerOfTwo(result);
                else isAmountSet = int.TryParse(Console.ReadLine(), out result);

            } while (!isAmountSet || result < 2);

            return result;
        }

        public static bool PowerOfTwo(int a)
        {
            if (a == 2) return true;
            if (a % 2 == 0) return PowerOfTwo(a / 2);
            return false;
        }


        public static OrganizedTournament.KindOfBracket ChoseBracket()
        {
            var answer = "";
            do
            {
                Console.WriteLine("In PlayOff bracket you'll not have an opportunity to use Double Elimination system.");
                Console.Write("Choose bracket type Horizontal, Vertical or PlayOff (h/v/p) ");
                answer = Console.ReadLine();

            } while (answer != "h" && answer != "v" && answer != "p");

            if (answer == "h")
                return OrganizedTournament.KindOfBracket.Horizontal;
            if (answer == "v")
                return OrganizedTournament.KindOfBracket.Vertical;

            return OrganizedTournament.KindOfBracket.PlayOff;
        }

        public static bool ChoseSystem()
        {
            string answer = "";
            do
            {
                Console.Write("Do you want to use Double or Single Elimination system? (d/s) ");
                answer = Console.ReadLine();

            } while (answer != "d" && answer != "s");

            if (answer == "d")
                return true;

            return false;
        }

        public static string InputNames(int index, int maxLength, List<string> existedNames)
        {
            string result;
            bool problemsWithAdding;
            do
            {
                problemsWithAdding = true;
                Console.Write("Name of {0} participant: ", index + 1);
                result = Console.ReadLine();

                if (result != null && result.Length > maxLength)
                    Console.WriteLine("Maximum length of name is {0} ", maxLength);
                else if (existedNames.Contains(result))
                    Console.WriteLine("A participant with this name already exists. Rename current participant");
                else if (string.IsNullOrEmpty(result))
                    Console.WriteLine("You've entered an empty string. Rename current participant");
                else problemsWithAdding = false;

            } while (problemsWithAdding);

            return result;
        }

        public static string InputWinner(string firstParticipant, string secondParticipant)
        {
            bool firstIsWinner;
            bool secondIsWinner;
            do
            {
                Console.Write("The winner of the game between \"{0}\" and \"{1}\" is ", firstParticipant, secondParticipant);
                var winner = Console.ReadLine();
                firstIsWinner = winner.Equals(firstParticipant);
                secondIsWinner = winner.Equals(secondParticipant);

            } while (!firstIsWinner && !secondIsWinner);

            if (firstIsWinner)
                return firstParticipant;

            return secondParticipant;
        }
    }
}