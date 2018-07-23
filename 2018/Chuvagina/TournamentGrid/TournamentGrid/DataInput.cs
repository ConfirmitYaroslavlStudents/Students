using System;
using System.Collections.Generic;

namespace TournamentGrid
{
    internal static class DataInput
    {
        public static int InputAmount()
        {
            int result;
            do
            {
                Console.Write("Amount of participants: ");
            } while (!int.TryParse(Console.ReadLine(), out result));
           
            return result;
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
