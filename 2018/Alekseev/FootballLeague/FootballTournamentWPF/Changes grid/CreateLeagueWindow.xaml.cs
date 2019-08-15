using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FootballLeagueClassLibrary.Structure;
using FootballTournamentWPF.Notification_Screens;
using Football_League;

namespace FootballTournamentWPF.Changes_grid
{
    /// <summary>
    /// Interaction logic for CreateLeagueWindow.xaml
    /// </summary>
    public partial class CreateLeagueWindow : Window
    {
        private int _numberOfPlayers;
        private List<Contestant> _players = new List<Contestant>();

        public FullGrid Grid;
        public CreateLeagueWindow(FullGrid grid)
        {
            Grid = grid;
            InitializeComponent();
        }


        private void NumberOfPlayersButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NumberOfPlayersTextBox.Text, out _numberOfPlayers))
            {
            }
        }
        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            _players.Add(new Contestant(AddPlayerTextBox.Text));
            AddPlayerTextBox.Clear();
            var popupAdded = new PlayerAddedPopupWindow();
            popupAdded.ShowDialog();
            CloseWindowIfNeeded();
        }

        private void CloseWindowIfNeeded()
        {
            if (_numberOfPlayers == _players.Count)
            {
                Randomize(ref _players);
                Grid.InitialiseGrid(_players);
                this.Close();
            }
        }
        public static void Randomize(ref List<Contestant> players)
        {
            Random randomGenerator = new Random();
            int n = players.Count();
            while (n > 1)
            {
                n--;
                int k = randomGenerator.Next(n + 1);
                Contestant player = players[k];
                players[k] = players[n];
                players[n] = player;
            }
        }
    }
}
