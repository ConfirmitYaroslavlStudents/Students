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

        public MatchInfo(string tour, string match, string winner)
        {
            Tour = int.Parse(tour) - 1;
            MatchNumber = int.Parse(match) - 1;
            Winner = int.Parse(winner) - 1;
        }
    }
}
