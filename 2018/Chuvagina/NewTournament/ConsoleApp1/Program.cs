using System;
using System.Collections.Generic;
using Tournament;

namespace ConsoleApp1
{
    class Program
    {
        public static void Print(Participant participant, int level)
        {
            if (participant.Left != null)
                Print(participant.Left, level+1);

            if (participant.Winner != null && participant.Winner.Name == participant.Name)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.White;

            for (int i = 5; i > level; i--)
                Console.Write("\t");
            Console.WriteLine(participant.Name);

            if (participant.Right != null)
                Print(participant.Right,level+1);

        }

        static void Main(string[] args)
        {
            var List = new List<string> { "1", "2", "3", "4", "5"};
            var Tournament = new TournamentGrid(List);
            List<Participant> upperBracket;
            List<Participant> lowerBracket;
            do
            {
                Tournament.PlayRound(out upperBracket,out lowerBracket);
                if (upperBracket.Count % 2 == 1 && upperBracket.Count!=1)
                    upperBracket.RemoveAt(upperBracket.Count-1);
                Console.WriteLine("----Upper Bracket----");
                foreach (Participant participant in upperBracket)
                    Print(participant, 0);

                if (lowerBracket.Count % 2 == 1 && lowerBracket.Count!=1)
                    lowerBracket.RemoveAt(lowerBracket.Count - 1);
                Console.WriteLine("----Lower Bracket----");
                foreach (Participant participant in lowerBracket)
                    Print(participant, 0);

                Console.ReadLine();
            } while (upperBracket.Count + lowerBracket.Count> 1);
            

        }
    }
}
