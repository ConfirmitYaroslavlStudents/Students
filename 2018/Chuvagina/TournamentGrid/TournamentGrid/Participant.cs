using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGrid
{
    internal class Participant 
    {
        public string Name { get; private set; }
        public ConsoleColor Color = ConsoleColor.White;
        public bool IsSeen = true;
        public int Round;

        public Participant(string nameOfParticipant)
        {
            Name = nameOfParticipant;

        }

        public Participant(string nameOfParticipant, int round)
        {
            Name = nameOfParticipant;
            Round = round;
        }
    }
}
