using System.Windows;

namespace FootballTournamentWPF.Notification_Screens
{
    /// <summary>
    /// Interaction logic for OnePlayerLeftWindow.xaml
    /// </summary>
    public partial class OnePlayerLeftWindow : Window
    {
        public OnePlayerLeftWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
