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

        public Participant(string participantName)
        {
            Left = null;
            Right = null;
            Winner = null;
            Name = participantName;
        }

        public Participant(string participantName, Participant left, Participant right)
        {
            Left = left;
            Right = right;
            Winner = null;
            Name = participantName;
        }

        public void SetWinner(Participant participant)
        { 
            Winner = participant;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
