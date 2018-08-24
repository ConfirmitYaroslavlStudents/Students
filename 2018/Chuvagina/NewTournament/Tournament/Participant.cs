using System;

namespace Tournament
{
    [Serializable]
    public class Participant
    {
        public string Name { get; private set; }
        public Participant Winner { get; private set; }
        public readonly Participant Left;
        public readonly Participant Right;

        internal Participant(string participantName)
        {
            Left = null;
            Right = null;
            Winner = null;
            Name = participantName;
        }

        internal Participant(string participantName, Participant left, Participant right)
        {
            Left = left;
            Right = right;
            Winner = null;
            Name = participantName;
        }

        internal void SetWinner(Participant participant)
        { 
            Winner = participant;
        }

        internal void SetName(string name)
        {
            Name = name;
        }
    }
}
