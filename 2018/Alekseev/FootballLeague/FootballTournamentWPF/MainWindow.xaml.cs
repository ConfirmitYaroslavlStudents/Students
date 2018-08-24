using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Football_League;

namespace FootballTournamentWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum LeagueType
        {
            SingleElumination = 1,
            DoubleElumination = 2
        };

        private static LeagueType _leagueType;
        private static FullGrid Grid = new FullGrid();

        public MainWindow()
        {
            InitializeComponent();
            CreateTournamentButton.Visibility = Visibility.Hidden;
            ChooseWinnersButton.Visibility = Visibility.Hidden;
            DisplayResultsButton.Visibility = Visibility.Hidden;
            LoadButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;
        }

        private void MaxLosesNumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (MaxLosesNumberTextBox.Text != "")
            {
                _leagueType = int.Parse(MaxLosesNumberTextBox.Text) == 1 ? LeagueType.SingleElumination : LeagueType.DoubleElumination;

                MaxLosesNumberTextBox.Visibility = Visibility.Hidden;
                MaxLosesNumberButton.Visibility = Visibility.Hidden;
                MaxLosesNumberLabel.Visibility = Visibility.Hidden;

                CreateTournamentButton.Visibility = Visibility.Visible;
                ChooseWinnersButton.Visibility = Visibility.Visible;
                DisplayResultsButton.Visibility = Visibility.Visible;
                LoadButton.Visibility = Visibility.Visible;
                ExitButton.Visibility = Visibility.Visible;
            }
        }

        private void CreateTournamentButton_Click(object sender, RoutedEventArgs e)
        {
            var leagueCreationWindow = new LeagueCreation((int)_leagueType);
            leagueCreationWindow.ShowDialog();
            Grid = leagueCreationWindow.Grid;
            //save session
        }

        private void ChooseWinnersButton_Click(object sender, RoutedEventArgs e)
        {
            var chooseWinnersWindow = new ChooseWinners(Grid);
            chooseWinnersWindow.ShowDialog();
            List<int>[] choices = chooseWinnersWindow.Choices;
            Grid.PlayRound(choices);
            if (Grid.IsFinished)
            {
                //display one player left
            }
            //save session
        }

        private void DisplayResultsButton_Click(object sender, RoutedEventArgs e)
        {
            VerticalDisplayButton.Visibility = Visibility.Visible;
            HorizontalDisplayButton.Visibility = Visibility.Visible;

            CreateTournamentButton.Visibility = Visibility.Hidden;
            ChooseWinnersButton.Visibility = Visibility.Hidden;
            LoadButton.Visibility = Visibility.Hidden;
            ExitButton.Visibility = Visibility.Hidden;
        }

        private void VerticalDisplayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HorizontalDisplayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            //save session
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            //save session?
            this.Close();
        }
    }
}
