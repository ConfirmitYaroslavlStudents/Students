namespace Championship
{
    public class Game
    {
        public Team FirstTeam { get; private set; }
        public Team SecondTeam { get; private set; }
        public string Winner { get; private set; }

        public Game(Team first, Team second)
        {
            FirstTeam = first;
            SecondTeam = second;
            Winner = null;
        }

        public void SetWinner()
        {
            Winner = FirstTeam.Score > SecondTeam.Score ? FirstTeam.Name : SecondTeam.Name;
        }
    }
}
