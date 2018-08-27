using System.Collections.Generic;
using System.Windows;
using FootballLeagueClassLibrary.Structure;
using Football_League;

namespace FootballTournamentWPF.Changes_grid
{
    /// <summary>
    /// Interaction logic for ChooseWinnersWindow.xaml
    /// </summary>
    public partial class ChooseWinnersWindow : Window
    {
        private readonly FullGrid _grid;
        private int _currentTreeNumber = -1;
        private Match _currentMatch = new Match();

        public List<int>[] Choices;

        public ChooseWinnersWindow(FullGrid grid)
        {
            _grid = grid;
            Choices = new List<int>[_grid.Grid.Count];
            InitializeComponent();

            PlayerOneButton.Visibility = Visibility.Hidden;
            PlayerTwoButton.Visibility = Visibility.Hidden;
            ChooseMatchWinnersLabel.Visibility = Visibility.Hidden;
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerOneButton.Visibility = Visibility.Hidden;
            PlayerTwoButton.Visibility = Visibility.Hidden;
            ChooseMatchWinnersLabel.Visibility = Visibility.Hidden;
            DisplayNextMatchIfPossible();
        }
        private void DisplayNextMatchIfPossible()
        {
            if (_currentMatch?.PlayerOne != null)
            {
                if (_currentMatch.PlayerTwo == null)
                {
                    Choices[_currentTreeNumber].Add(1);
                    SetNextTreeChoices();
                }
                else
                {
                    PlayerOneButton.Content = _currentMatch.PlayerOne.Name;
                    PlayerTwoButton.Content = _currentMatch.PlayerTwo.Name;
                }
            }
            else
            {
               SetNextTreeChoices();
            }
        }

        private void SetNextTreeChoices()
        {
            _currentTreeNumber++;
            if (_currentTreeNumber >= _grid.Grid.Count)
            {
                this.Close();
                return;
            }
            Choices[_currentTreeNumber] = new List<int>();
            _currentMatch = _grid.Grid[_currentTreeNumber].CurrentRoundFirstMatch;
            DisplayNextMatchIfPossible();
        }
        private void PlayerOneButton_Click(object sender, RoutedEventArgs e)
        {
            Choices[_currentTreeNumber].Add(1);
            _currentMatch = _currentMatch.NextMatch;
            DisplayNextMatchIfPossible();
        }

        private void PlayerTwoButton_Click(object sender, RoutedEventArgs e)
        {
            Choices[_currentTreeNumber].Add(2);
            _currentMatch = _currentMatch.NextMatch;
            DisplayNextMatchIfPossible();
        }
    }
}
