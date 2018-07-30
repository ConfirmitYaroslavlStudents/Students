namespace Tournament
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPlayers = Messenger.ReadNumberOfPlayers();
            string[] playerNames = Messenger.ReadNames(numberOfPlayers);
            var tournament = new TournamentController(numberOfPlayers, playerNames);
            Messenger.Play(tournament);
        }
    }
}
