using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTournament
{
    class Program
    {
        static void Main(string[] args)
        {
            Tournament tournament = new Tournament();
            tournament.Load();
        }
    }
}
