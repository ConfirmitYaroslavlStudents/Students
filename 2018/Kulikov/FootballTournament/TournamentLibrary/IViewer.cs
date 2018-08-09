using System.Collections.Generic;

namespace TournamentLibrary
{
    public interface IViewer
    {
        void StartedNewTournament();

        int EnterCountOfPlayers();

        void EnterPlayerNames();

        string EnterPlayerName(HashSet<string> existingNames, int index);

        int EnterPlayerScore(Player player);

        void DrawIsNotPossible();

        void PrintGameResult(Game game);

        void PrintGrandFinal(Game final);

        void PrintChampion(Player champion);
    }
}
