using System;
using TournamentGrid;

namespace ConsoleTournament
{
    class ConsoleTournament
    {
        static void Main(string[] args)
        {
            var footballTournament = new Tournament();
            footballTournament.Play();
            Console.ReadLine();
        }

    }
}
