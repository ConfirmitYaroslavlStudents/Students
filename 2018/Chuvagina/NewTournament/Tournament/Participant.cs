using System;

namespace Tournament
{
    [Serializable]
    public class Participant
    {
        public readonly string Name;
        public Participant Winner { get; internal set; }
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

    }
}
