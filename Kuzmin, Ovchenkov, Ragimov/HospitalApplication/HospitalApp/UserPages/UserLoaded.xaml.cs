using System.Windows;
using HospitalApp.AnalysisPages;
using HospitalLib.Providers;

namespace HospitalApp.UserPages
{
    /// <summary>
    /// Interaction logic for UserLoaded.xaml
    /// </summary>
    public partial class UserLoaded
    {
        public UserLoaded()
        {
            InitializeComponent();
            UserLabel.Content = CurrentState.CurrentPerson.ToString();
        }

        private void ChangeUserButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new StartPage());
        }

        private void AnalysisHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var analysisProvider = new AnalysisProvider(new DatabaseProvider());
            var analyzes = analysisProvider.Load(CurrentState.CurrentPerson);
            if (analyzes.Count == 0)
            {
                MessageBox.Show("История анализов данного пациента пуста!", "Error!");
                return;
            }

            Switcher.PageSwitcher.Navigate(new AnalysisHisory());
        }

        private void NewAnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new NewAnalysis());
        }
    }
}
