using System;
using Tournament;

namespace ConsoleTournament
{
    class ConsoleTournament
    {
        static void Main(string[] args)
        {
            var footballTournament = new OrganizedTournament();
            footballTournament.Play();
            Console.ReadLine();     
        }
    }
}
