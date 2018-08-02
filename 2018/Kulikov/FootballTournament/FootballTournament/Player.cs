using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTournament
{
    public class Player
    {
        public string Name { get; set; }
=======

namespace FootballTournament
{
    [Serializable]
    public class Player
    {
        public string Name { get; private set; }
>>>>>>> Kulikov_Tournament

        public Player(string name)
        {
            Name = name;
        }
    }
}
