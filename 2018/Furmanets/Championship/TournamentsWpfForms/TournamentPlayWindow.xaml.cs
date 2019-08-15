using Championship;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TournamentsWpfForms.Drawers;
using TournamentsWpfForms.Drawers.HorisontalDrawers;

namespace TournamentsWpfForms
{
    public partial class TournamentPlayWindow
    {
        private readonly Tournament _tournament;
        public Canvas PlaceForDrawind { get; }
        //private readonly IDrawer _drawer;

        public TournamentPlayWindow(Tournament tournament)
        {
            InitializeComponent();
            _tournament = tournament;
            PlaceForDrawind = new Canvas();
            SetCurrentMeeting(_tournament.GetNextMeeting());

            if (tournament.GetTournamentToPrint().Length == 3)
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

            PrintUpperGrid(sender, e);
        }

        private void NewTornamentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var menuAddPlayers = new WindowAddPlayers();
            menuAddPlayers.Show();
            Close();
        }

        private void PrintUpperGrid(object sender, RoutedEventArgs e)
        {
            var drawer = new HorisontalDrawer();
            var rounds = _tournament.GetTournamentToPrint();
            drawer.GetListOfTournamentItems(rounds[0]);

            var listOfItems = drawer.GetListOfTournamentItems(rounds[0]);

            CanvasForPrintTournamentGrid.Children.Clear();

            foreach (var item in listOfItems)
            {
                CanvasForPrintTournamentGrid.Children.Add(item);
            }

            if (rounds.Length == 3)
            {
              var  drawer2 = new HorisontalLowerGridPainter();
              listOfItems =  drawer2.GetListOfLowreGridItems(rounds[2], 400);

                foreach (var item in listOfItems)
                {
                    CanvasForPrintTournamentGrid.Children.Add(item);
                }
            }
        }

        private void PrintLowerGrid(object sender, RoutedEventArgs e)
        {
            var rounds = _tournament.GetTournamentToPrint();
            var drawerLower = new HorisontalLowerGridPainter();
            var listOfItems = drawerLower.GetListOfLowreGridItems(rounds[1], 0);

            CanvasForPrintTournamentGrid.Children.Clear();

            foreach (var item in listOfItems)
            {
                CanvasForPrintTournamentGrid.Children.Add(item);
            }
        }
    }
}
