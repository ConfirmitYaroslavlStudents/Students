using System;
using System.Collections.Generic;
using static ConsoleTournament.ConsoleTournament;

namespace ConsoleTournament
{
    public class DataInput
    {


        public static EliminationSystem ChoseEliminationSystem()
        {
            string answer = "";
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
            var answer = "";
            do
            {
                Console.Write("Choose bracket type Horizontal, Vertical or PlayOff (h/v/p) ");
                answer = Console.ReadLine();

            } while (answer != "h" && answer != "v" && answer != "p");

            if (answer == "h")
                return BracketStyle.Horizontal;
            if (answer == "v")
                return BracketStyle.Vertical;

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


        public static List<string> InputNames(int amount)
        {
            var participants = new List<string>();
            var name = "";

            for (int i=0;i<amount;i++)
            {
                do
                {
                    Console.Write("Enter {0} participant name: ",i+1);
                    name = Console.ReadLine();
                } while (participants.Contains(name) || name.Length>8);
                participants.Add(name);
            }

            return participants;
        }

    }
}
