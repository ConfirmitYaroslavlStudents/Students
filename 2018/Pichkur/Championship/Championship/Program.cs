using System;
using System.Collections.Generic;

namespace Championship
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Press Enter to start Championship!");
            Console.ReadKey();
            Console.Clear();
            Console.Write("Enter count of Teams:");

            Queue<string> Teams = new Queue<string>();
            int CountOfTeam = DataValidation.CheckCountOfTeam();
            Console.WriteLine("Enter names of teams");

            for (int i = 0; i < CountOfTeam; i++)
            {
                Console.Write("{0}. ", i+1);
                Teams.Enqueue(DataValidation.CheckTeamName(Teams));
            }

            Console.Clear();

            TournamentGrid Grid = new TournamentGrid(Teams, DataValidation.PowerOfTwo(CountOfTeam)-1);
            Grid.StartTournament();
            Grid.ResultOfTournament();
        }
    }
}
