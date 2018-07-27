namespace Tournament
{
    public class Participant
    {
        public string Name { get; internal set; }
        public Participant Winner { get; internal set; }
        public Participant Left { get; internal set; }
        public Participant Right { get; internal set; }

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
