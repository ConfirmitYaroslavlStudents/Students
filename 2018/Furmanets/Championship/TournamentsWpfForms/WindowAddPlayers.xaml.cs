using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Championship;

namespace TournamentsWpfForms
{
    public partial class WindowAddPlayers
    {
        List<string> _players = new List<string>();
        private IFileManager _fileManager;

        public WindowAddPlayers(IFileManager fileManager)
        {
            InitializeComponent();
            PlayerNameBox.Focus();
            _fileManager = fileManager;
        }

        private void ClickBack(object sender, RoutedEventArgs e)
        {
            var window = new MainMenuWindow();
            window.Show();
            Close();
        }

        private void AddPlayerButtom(object sender, RoutedEventArgs e)
        {
            if (PlayerNameBox.Text != "")
            {
                PlayersListBox.Text += PlayerNameBox.Text + "\r\n";
                _players.Add(PlayerNameBox.Text);
            }

            PlayerNameBox.Text = "";
        }

        private void PlayerNameBox_OnMouseEnter(object sender, MouseEventArgs e)
        {
            PlayerNameBox.Text = "";
        }

        private void StartDouble_Click(object sender, RoutedEventArgs e)
        {
            var tournament = new DoubleEliminationTournament(_players);
            _fileManager.WriteToFile(tournament);
            OpenWindowTournament(tournament);
        }

        private void StarSingle_Click(object sender, RoutedEventArgs e)
        {
            var tournament = new SingleElimitationTournament(_players);
            _fileManager.WriteToFile(tournament);
            OpenWindowTournament(tournament);
        }

        private void OpenWindowTournament(Tournament tournament)
        {
            var window = new TournamentPlayWindow(tournament, _fileManager);
            window.Show();
            Close();
        }

        private void PlayerNameBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    AddPlayerButtom(sender, e);
                    break;
                case Key.Down:
                    StartSingleTournamentButton.Focus();
                    break;
                case Key.Up:
                    StartDoubleTournamentButton.Focus();
                    break;
            }
        }
    }
}
