using Football_League;
using System.Collections.Generic;
using System.Windows;
using FootballLeagueClassLibrary.FileSystem_Savers_and_Loaders;
using FootballLeagueClassLibrary.Structure;
using FootballTournamentWPF.Changes_grid;
using FootballTournamentWPF.Drawer_Windows;
using FootballTournamentWPF.Notification_Screens;

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
        private static FullGrid _grid = new FullGrid();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void MaxLosesNumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (MaxLosesNumberTextBox.Text != "")
            {
                _leagueType = int.Parse(MaxLosesNumberTextBox.Text) == 1 ? LeagueType.SingleElumination : LeagueType.DoubleElumination;
                _grid.SetGridTreesNumber((int)_leagueType);
            }
        }
        private void CreateTournamentButton_Click(object sender, RoutedEventArgs e)
        {
            var createLeagueWindow = new CreateLeagueWindow(_grid);
            createLeagueWindow.ShowDialog();

            _grid = createLeagueWindow.Grid;
            SaverLoader.SaveCurrentSession((int)_leagueType, _grid);
        }
        private void ChooseWinnersButton_Click(object sender, RoutedEventArgs e)
        {          
            var chooseWinnersWindow = new ChooseWinnersWindow(_grid);
            if (_grid.IsFinished)
            {
                new OnePlayerLeftWindow().ShowDialog();
                SaverLoader.SaveCurrentSession((int)_leagueType, _grid);
                return;
            }
            chooseWinnersWindow.ShowDialog();
            List<int>[] choices = chooseWinnersWindow.Choices;

            _grid.PlayRound(choices);
            if (_grid.IsFinished)
                new OnePlayerLeftWindow().ShowDialog();
            SaverLoader.SaveCurrentSession((int)_leagueType, _grid);
        }
        private void VerticalDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            var verticalDisplayWindow = new VerticalDisplayWindow(_grid);
            verticalDisplayWindow.ShowDialog();
        }
        private void HorizontalDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            var horizontalDisplayWindow = new HorizontalDisplayWindow(_grid);
            horizontalDisplayWindow.ShowDialog();
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            _grid = SaverLoader.LoadLastSave((int)_leagueType);
            if (_grid == null)
                MessageBox.Show("Save file isn't created");
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
