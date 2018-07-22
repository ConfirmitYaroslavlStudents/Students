namespace Tournament
{
    public class MatchInfo
    {
        public readonly int Tour;
        public readonly int MatchNumber;
        public int Winner;

        public MatchInfo(int tour, int match, int winner)
        {
            Tour = tour;
            MatchNumber = match;
            Winner = winner;
        }
    }
}
