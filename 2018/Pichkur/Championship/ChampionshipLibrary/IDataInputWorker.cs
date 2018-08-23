namespace ChampionshipLibrary
{
    public interface IDataInputWorker
    {
        void WriteLineMessage(string message);
        void WriteMessage(string message);
        string InputString();
        string InputTeamName();
        string InputTeamScore();
    }
}
