using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Championship;

namespace ConsoleChampionship
{
    class Program
    {
        static void Main(string[] args)
        {
            Tournament tournament = new Tournament();
            tournament.AddingPlayers();
            tournament.CollectorResults();
        }
    }
}
