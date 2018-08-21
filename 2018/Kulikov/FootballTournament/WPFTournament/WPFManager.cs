using System.Windows.Controls;
using TournamentLibrary;

namespace WPFTournament
{
    public class WPFManager : IDataManager
    {
        private MainWindow _mainWindow;
        private TournamentData _tournamentData;
        private Label _statusBar;
        private int _indexOfPlayer;

        public WPFManager(MainWindow mainWindow, TournamentData tournamentData)
        {
            _mainWindow = mainWindow;
            _statusBar = _mainWindow.Label_Status;
            _tournamentData = tournamentData;
            _indexOfPlayer = 0;
        }

        public void DrawIsNotPossible()
        {
            _statusBar.Content = "Draw is not possible! Try again:";
        }

        public void EnterCountOfPlayers()
        {
            _statusBar.Content = "Enter count of players:";
        }

        public void EnterPlayerName(int index)
        {
            _statusBar.Content = $"Enter name of {index} player:";
        }

        public void EnterPlayerNames()
        {
            _statusBar.Content = "Enter names of players:";
        }

        public void EnterPlayerScore(Player player)
        {
            _statusBar.Content = $"{player.Name} scores:";
        }

        public int GetCountOfPlayers()
        {
            return _tournamentData.CountOfPlayers;
        }

        public string GetPlayerName()
        {
            var name = _tournamentData.Players[_indexOfPlayer].Name;
            _indexOfPlayer++;

            return name;
        }

        public int GetPlayerScore()
        {
            throw new System.NotImplementedException();
        }

        public void NameAlreadyExists()
        {
            _statusBar.Content = "Player with this name already exists! Try again:";
        }

        public void PrintChampion(Player champion)
        {
            throw new System.NotImplementedException();
        }

        public void PrintGameResult(Game game)
        {
            throw new System.NotImplementedException();
        }

        public void PrintGrandFinal(Game final)
        {
            throw new System.NotImplementedException();
        }

        public void StartedNewTournament()
        {
            _statusBar.Content = "Select tournament mode:";
        }
    }
}
