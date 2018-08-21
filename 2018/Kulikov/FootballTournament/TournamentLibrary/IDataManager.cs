namespace TournamentLibrary
{
    public interface IDataManager
    {
        void StartedNewTournament();

        void EnterCountOfPlayers();

        int GetCountOfPlayers();

        void EnterPlayerNames();

        void EnterPlayerName(int index);

        string GetPlayerName();

        void NameAlreadyExists();

        void EnterPlayerScore(Player player);

        int GetPlayerScore();

        void DrawIsNotPossible();

        void PrintGameResult(Game game);

        void PrintGrandFinal(Game final);

        void PrintChampion(Player champion);
    }
}
