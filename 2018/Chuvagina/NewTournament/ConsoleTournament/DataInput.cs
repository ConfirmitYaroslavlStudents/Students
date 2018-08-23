using System;
using System.Collections.Generic;
using static ConsoleTournament.ConsoleTournament;

namespace ConsoleTournament
{
    public class DataInput
    {
        public static EliminationSystem ChoseEliminationSystem()
        {
            string answer;
            do
            {
                Console.Write("Do you want to use Double or Single Elimination system? (d/s) ");
                answer = Console.ReadLine();

            } while (answer != "d" && answer != "s");

            if (answer == "d")
                return EliminationSystem.Double;

            return EliminationSystem.Single;
        }

        public static BracketStyle ChoseBracket()
        {
            string answer;

            do
            {
                Console.Write("Choose bracket type Horizontal or PlayOff (h/p) ");
                answer = Console.ReadLine();

            } while (answer != "h" && answer != "p");

            if (answer == "h")
                return BracketStyle.Horizontal;

            return BracketStyle.PlayOff;
        }

        public static int InputAmount()
        {
            int result;

            do
            {
                Console.Write("Amount of participants: ");

            } while (!int.TryParse(Console.ReadLine(), out result));

            return result;
        }

        public static string InputWinner(string first, string second)
        {         
            Console.Write($"The winner between \"{first}\" and \"{second}\" is: ");  
            return Console.ReadLine();
        }

        public static bool IsFromSavedFile()
        {
            string loadSavedBracket;

            do
            {
                Console.Write("Do you want to load saved tournament? (y/n) ");
                loadSavedBracket = Console.ReadLine();

            } while (loadSavedBracket!="y" && loadSavedBracket!="n");

            return loadSavedBracket == "y";
        }

        public static List<string> InputNames(int amount, int maxLength)
        {
            var participants = new List<string>();

            for (int i=0;i<amount;i++)
            {
                string name;

                do
                {
                    Console.Write("Enter {0} participant name: ",i+1);
                    name = Console.ReadLine();

                } while (name=="" || participants.Contains(name) || name.Length> maxLength);

                participants.Add(name);
            }

            return participants;
        }

    }
}
