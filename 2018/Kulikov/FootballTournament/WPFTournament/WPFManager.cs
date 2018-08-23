using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TournamentLibrary;

namespace WPFTournament
{
    public class WPFManager : IDataManager
    {
        private MainWindow _mainWindow;
        private TournamentData _tournamentData;
        private TextBox _info;
        private int _indexOfPlayer;
        private int _indexOfGame;
        private int _numberOfPlayer;

        public WPFManager(MainWindow mainWindow, TournamentData tournamentData)
        {
            _mainWindow = mainWindow;
            _info = _mainWindow.Rtb_Info;
            _tournamentData = tournamentData;
            _indexOfPlayer = 0;
            _indexOfGame = 0;
            _numberOfPlayer = 0;
        }

        public void DrawIsNotPossible()
        {
            _mainWindow.Label_Draw.Visibility = Visibility.Visible;
        }

        public void EnterCountOfPlayers()
        {
            _info.AppendText("\nEnter count of players");
        }

        public void EnterPlayerName(int index)
        {
            _info.AppendText($"\nEnter name of {index} player");
        }

        public void EnterPlayerNames()
        {
            _info.AppendText("\nEnter names of players");
        }

        public void EnterPlayerScore(Player player)
        {
            if (_indexOfPlayer == 0)
            {
                _mainWindow.Label_FirstPlayerScore.Content = $"{player.Name} scores ";
                _indexOfPlayer++;
            }
            else
            {
                _mainWindow.Label_SecondPlayerScore.Content = $"{player.Name} scores ";
                _indexOfPlayer = 0;
            }
        }

        public int GetCountOfPlayers()
        {
            return _tournamentData.CountOfPlayers;
        }

        public string GetPlayerName()
        {
            var name = _tournamentData.Players[_indexOfPlayer].Name;

            if (_indexOfPlayer == _tournamentData.CountOfPlayers - 1)
                _indexOfPlayer = 0;
            else
                _indexOfPlayer++;

            return name;
        }

        public int GetPlayerScore()
        {
            var game = _tournamentData.GamesToPlay[_indexOfGame];
            int value;

            if (_numberOfPlayer == 0)
            {
                value = game.FirstPlayerScore;
                _numberOfPlayer = 1;
            }
            else
            {
                value = game.SecondPlayerScore;
                _numberOfPlayer = 0;
                _indexOfGame++;
            }

            if(_indexOfGame == _tournamentData.GamesToPlay.Count)
            {
                _numberOfPlayer = 0;
                _indexOfGame = 0;
            }

            return value;
        }

        public void NameAlreadyExists()
        {
            _info.AppendText("\nPlayer with this name already exists! Try again!");
        }

        public void PrintChampion(Player champion)
        {
            _mainWindow.LB_Results.Items.Add($"Tournament is finished. {champion.Name} is a champion!");
        }

        public void PrintGameResult(Game game)
        {
            _mainWindow.LB_Results.Items.Add(game.Result());
        }

        public void PrintGrandFinal(Game final)
        {
            _mainWindow.LB_Results.Items.Add("GRAND FINAL");
            PrintGameResult(final);
        }

        public void StartedNewTournament()
        {
            _info.AppendText("Select tournament mode");
        }
    }
}
