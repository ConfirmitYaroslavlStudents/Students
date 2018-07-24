using System;

namespace TournamentGrid
{
    internal class Participant 
    {
        public string Name { get;}
        public ConsoleColor Color = ConsoleColor.White;
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
