using System.Windows;
using Championship;

namespace TournamentsWpfForms
{
    public partial class MainMenuWindow
    {
        private Tournament _tournament;
        private IFileManager _fileManager;

        public MainMenuWindow()
        {
            InitializeComponent();
            _fileManager = new BinaryFileManager();
        }

        private void ClickStart(object sender, RoutedEventArgs e)
        {
            if (_tournament == null)
            {
                var window = new WindowAddPlayers();
                window.Show();
                Close();
            }
            else
            {
                var window = new TournamentPlayWindow(_tournament);
                window.Show();
                Close();
            }
        }

        private void Click_AddPlayers(object sender, RoutedEventArgs e)
        {
        }

        private void Click_Load(object sender, RoutedEventArgs e)
        {
            _tournament = _fileManager.ReadFromFile();

            if (_tournament != null)
            {
                MessageBox.Show("Tournament loaded successfully");
            }
            else
            {
                MessageBox.Show("File is empty");
            }
        }

        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
