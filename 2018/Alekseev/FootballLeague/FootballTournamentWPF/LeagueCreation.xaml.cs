using System.Collections.Generic;
using System.Windows;
using Football_League;

namespace FootballTournamentWPF
{
    /// <summary>
    /// Interaction logic for LeagueCreation.xaml
    /// </summary>
    public partial class LeagueCreation : Window
    {
        private int _numberOfPlayers;
        private int _currentPlayerNumber;
        readonly List<Contestant> players = new List<Contestant>();

        public FullGrid Grid;            
        public LeagueCreation(int leagueType)
        {
            Grid = new FullGrid();
            Grid.SetGridTreesNumber(leagueType);
            InitializeComponent();
        }

        private void PlayerNumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerNumberTextBox.Text != "")
            {
                _numberOfPlayers = int.Parse(PlayerNumberTextBox.Text);
            }
        }

        private void NewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            players.Add(new Contestant(NewPlayerTextBox.Text));
            NewPlayerTextBox.Clear();

            _currentPlayerNumber++;
            if (_currentPlayerNumber == _numberOfPlayers)
            {
                Grid.InitialiseGrid(players);
                Close();
            }
        }
    }
}
