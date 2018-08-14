using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Championship;
using ConsoleChampionship;

namespace TournamentsWpfForms
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        private Tournament _tournament;

        public MainMenuWindow()
        {
            InitializeComponent();
        }

        private void ClickStart(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Click_AddPlayers(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Click_Load(object sender, RoutedEventArgs e)
        {
            _tournament = FileManager.DownloadTournamentFromFile();

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
            this.Close();
        }
    }
}
