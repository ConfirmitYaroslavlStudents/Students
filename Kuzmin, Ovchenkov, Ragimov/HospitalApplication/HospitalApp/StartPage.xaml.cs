using System.Windows;
using HospitalApp.UserPages;

namespace HospitalApp
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void SelectUserButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new SelectUser());
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new CreateUser());
        }
    }
}
