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
using TournamentLibrary;

namespace WPFTournament
{
    public partial class MainWindow : Window
    {
        private Tournament _tournament;
        private WPFPrinter _printer = new WPFPrinter();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MI_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MI_Start_Click(object sender, RoutedEventArgs e)
        {
            SP_StartTournament.Visibility = Visibility.Visible;
            GB_SelectMode.Visibility = Visibility.Visible;
        }

        private void Btn_Next_Click(object sender, RoutedEventArgs e)
        {
            if (RB_SingleElimination.IsChecked == true)
            {
                _tournament = new SingleEliminationTournament(_printer);
                GB_SelectMode.Visibility = Visibility.Collapsed;
            }
            else
                if (RB_DoubleElimination.IsChecked == true)
            {
                _tournament = new DoubleEliminationTournament(_printer);
                GB_SelectMode.Visibility = Visibility.Collapsed;
            }
        }
    }
}
