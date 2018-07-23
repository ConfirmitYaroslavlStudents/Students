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
            bool isAmountSet = false;
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
            else if (a % 2 == 0) return PowerOfTwo(a / 2);
            else return false;
        }


        public static KindOfBracket ChoseBracket()
        {
            string answer = "";
            do
            {
                Console.Write("Do you want to use PlayOff type of bracket? (y/n) ");
                answer= Console.ReadLine();

            } while (answer!="y" && answer!="n" );

            if (answer == "y")
                return KindOfBracket.PlayOff;
            else
                return KindOfBracket.Horizontal;
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
            else
                return false;
        }

        public static string InputNames(int index, int maxLength, List<string> existedNames)
        {
            string result;
            bool ProblemsWithAdding;
            do
            {
                ProblemsWithAdding = true;
                Console.Write("Name of {0} participant: ", index + 1);
                result = Console.ReadLine();

                if (result.Length > maxLength)
                    Console.WriteLine("Maximum length of name is {0} ", maxLength);
                else if (existedNames.Contains(result))
                    Console.WriteLine("A participant with this name already exists. Rename current participant");
                else if (String.IsNullOrEmpty(result))
                    Console.WriteLine("You've entered an empty string. Rename current participant");
                else ProblemsWithAdding = false;

            } while (ProblemsWithAdding);

            return result;
        }

        public static string InputWinner(string firstParticipant, string secondParticipant)
        {
            string winner = "";
            bool firstIsWinner;
            bool secondIsWinner;
            do
            {
                Console.Write("The winner of the game between \"{0}\" and \"{1}\" is ", firstParticipant, secondParticipant);
                winner = Console.ReadLine();
                firstIsWinner = winner.Equals(firstParticipant);
                secondIsWinner = winner.Equals(secondParticipant);

            } while (!firstIsWinner && !secondIsWinner);

            if (firstIsWinner)
                return firstParticipant;
            else
                return secondParticipant;
        }
    }
}
