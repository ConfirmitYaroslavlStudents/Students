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
            int numberOfPlayers = Messenger.ReadNumberOfPlayers();
            var tournament = new TournamentController(numberOfPlayers, Messenger.ReadNames(numberOfPlayers));
            Messenger.Play(tournament);
        }
    }
}
