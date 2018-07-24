using System;
using System.Collections.Generic;
using static TournamentGrid.Tournament;

namespace TournamentGrid
{
    internal static class DataInput
    {
        public static int InputAmount(KindOfBracket kindOfBracket)
        {
            int result;
            bool isAmountSet;
            do
            {
                if (kindOfBracket==KindOfBracket.PlayOff)
                    Console.WriteLine("PlayOff bracket requires power of two amount of participants.");

                Console.Write("Amount of participants: ");

                if (kindOfBracket==KindOfBracket.PlayOff)
                    isAmountSet = int.TryParse(Console.ReadLine(), out result) && PowerOfTwo(result);
                else isAmountSet = int.TryParse(Console.ReadLine(), out result);

            } while (!isAmountSet);
           
            return result;
        }

        public static bool PowerOfTwo(int a)
        {
            if (a == 2) return true;
            if (a % 2 == 0) return PowerOfTwo(a / 2);
            return false;
        }


        public static KindOfBracket ChoseBracket()
        {
            var answer = "";
            do
            {
                Console.WriteLine("In PlayOff bracket you'll not have an opportunity to use Double Elimination system.");
                Console.Write("Do you want to use PlayOff type of bracket? (y/n) ");
                answer = Console.ReadLine();

            } while (answer!="y" && answer!="n" );

            return answer == "y" ? KindOfBracket.PlayOff : KindOfBracket.Horizontal;
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
