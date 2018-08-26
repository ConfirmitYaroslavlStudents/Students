using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Championship;
using TournamentsWpfForms.Drawers;
using TournamentsWpfForms.Drawers.HorisontalDrawers;

namespace TournamentsWpfForms
{
    public partial class TournamentPlayWindow
    {
        private readonly Tournament _tournament;
        public Canvas PlaceForDrawind { get; }
        private readonly IFileManager _fileManager;
        private readonly IDrawer _drawer;

        public TournamentPlayWindow(Tournament tournament, IFileManager fileManager)
        {
            InitializeComponent();
            _tournament = tournament;
            _fileManager = fileManager;
            _drawer = new HorisontalDrawer();
            PlaceForDrawind = new Canvas();
            SetCurrentMeeting(_tournament.GetNextMeeting());
            
            if (tournament is DoubleEliminationTournament)
            {
                ButtonLowerGrid.Visibility = Visibility.Visible;
                ButtonUpperGrid.Visibility = Visibility.Visible;
            }

            PrintUpperGrid(new object(), new RoutedEventArgs());
        }

        private void SetCurrentMeeting(Meeting meeting)
        {
            if (meeting == null)
            {
                NewTornamentButton.Visibility = Visibility.Visible;
                return;
            }

            FirstPlayerLabel.Content = meeting.FirstPlayer;
            SecondPlayerLabel.Content = meeting.SecondPlayer;
        }

        private void TakeResults(object sender, RoutedEventArgs e)
        {
            var currentMeeting = _tournament.GetNextMeeting();

            if (currentMeeting == null)
            {
                MessageBox.Show("All matches was played.");
                return;
            }

            if (FirstPlayerScore.Text.Any(digit => !char.IsDigit(digit)))
            {
                MessageBox.Show("Error! Invalid introduced score of the first player.");
                return;
            }

            if (SecondPlayerScore.Text.Any(digit => !char.IsDigit(digit)))
            {
                MessageBox.Show("Error! Invalid introduced score of the second player.");
                return;
            }

            var results = new[] { int.Parse(FirstPlayerScore.Text), int.Parse(SecondPlayerScore.Text) };

            if (results[0] == results[1])
            {
                MessageBox.Show("Error! The account in the playoffs can not be equal.");
                return;
            }

            FirstPlayerScore.Text = "";
            SecondPlayerScore.Text = "";

            _tournament.CollectResults(results);
            SetCurrentMeeting(_tournament.GetNextMeeting());
            _fileManager.WriteToFile(_tournament);

            PrintUpperGrid(sender, e);
        }

        private void NewTornamentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var menuAddPlayers = new WindowAddPlayers(_fileManager);
            menuAddPlayers.Show();
            Close();
        }

        private void PrintUpperGrid(object sender, RoutedEventArgs e)
        {
            var rounds = _tournament.GetTournamentToPrint();
            _drawer.GetListOfTournamentItems(rounds[0]);

            var listOfItems = _drawer.GetListOfTournamentItems(rounds[0]);

            CanvasForPrintTournamentGrid.Children.Clear();

            foreach (var item in listOfItems)
            {
                CanvasForPrintTournamentGrid.Children.Add(item);
            }
        }

        private void PrintLowerGrid(object sender, RoutedEventArgs e)
        {
            var rounds = _tournament.GetTournamentToPrint();
            var _drawerLower = new HorisontalLowerGridPainter();
            var listOfItems = _drawerLower.GetListOfLowreGridItems(rounds[1], 0);

            CanvasForPrintTournamentGrid.Children.Clear();

            foreach (var item in listOfItems)
            {
                CanvasForPrintTournamentGrid.Children.Add(item);
            }
        }
    }
}
