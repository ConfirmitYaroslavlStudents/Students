namespace Tournament
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isDoubleElimination = Messenger.ReadDoubleEliminationFlag();
            int numberOfPlayers = Messenger.ReadNumberOfPlayers();
            string[] playerNames = Messenger.ReadNames(numberOfPlayers);
            var tournament = new TournamentController(numberOfPlayers, playerNames, isDoubleElimination);
            Messenger.Play(tournament);
        }
    }
}
