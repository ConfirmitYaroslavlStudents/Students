using System.Windows;

namespace FootballTournamentWPF.Notification_Screens
{
    /// <summary>
    /// Interaction logic for PlayerAddedPopupWindow.xaml
    /// </summary>
    public partial class PlayerAddedPopupWindow : Window
    {
        public PlayerAddedPopupWindow()
        {
            InitializeComponent();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
