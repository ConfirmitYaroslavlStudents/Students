using System.Collections.Generic;
using System.Windows;
using HospitalApp.UserPages;
using HospitalLib.Data;
using HospitalLib.Providers;

namespace HospitalApp
{
    /// <summary>
    ///     Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void SelectUserButton_Click(object sender, RoutedEventArgs e)
        {
            var personProvider = new PersonProvider(new DatabaseProvider());
            IList<Person> persons = personProvider.Load();
            if (persons.Count == 0)
            {
                MessageBox.Show("База данных пациентов пуста!", "Error!");
                return;
            }

            Switcher.PageSwitcher.Navigate(new SelectUser());
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new CreateUser());
        }
    }
}