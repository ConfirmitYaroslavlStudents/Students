using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_League
{
    public class Match
    {
        Contestant player_one;
        Contestant player_two;
        public Contestant winner;

        public Match(Contestant first, Contestant second)
        {
            player_one = first;
            player_two = second;
        }

        public void PrintPlayers()
        {
            Console.WriteLine("1. {0}\n2. {1}\n", player_one.Name, player_two.Name);
        }     

        public bool AutoWin()
        {
            if (player_two.Name == "-")
            {
                winner = player_one;
                return true;
            }
            if (player_one.Name == "-")
            {
                winner = player_two;
                return true;
            }
            return false;
        }
        public void SetWinner(int number = 0)
        {
            if (AutoWin())
                return;
            winner = (number == 1) ? player_one : player_two;
            if(player_two.Name != "-")
                winner.position = (player_one.position + player_two.position) / 2;
        }
    }
}
