using System.Windows;
using TournamentLibrary;

namespace WpfTournament
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tournament tournament;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_NewGame_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            CreationWindow creation = new CreationWindow();
            creation.ShowDialog();
            tournament = creation.CreatedTournament;

            if (tournament != null)
            {
                StartGame();
            }
        }

        private void Button_LoadGame_Click(object sender, RoutedEventArgs e)
        {
            tournament = SaveController.Load();

            if (tournament == null)
            {
                MessageBox.Show("Unable to load.");
                Button_NewGame_Click(sender, e);
            }
            else
            {
                Visibility = Visibility.Hidden;
                StartGame();
            }
        }

        private void StartGame()
        {
            GameWindow game = new GameWindow(tournament);
            game.ShowDialog();

            if (game.ShowMainMenu)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Close();
            }
        }
    }
}
