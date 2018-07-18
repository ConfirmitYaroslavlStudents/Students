using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    class Program
    {
        static void Main(string[] args)
        {
            var tournament = new TournamentController();
            Messenger.Play(tournament);
        }
    }
}
