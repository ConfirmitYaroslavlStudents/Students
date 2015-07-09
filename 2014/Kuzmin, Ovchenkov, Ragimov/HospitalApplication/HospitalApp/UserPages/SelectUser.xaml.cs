using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using HospitalLib.Data;
using HospitalLib.Providers;

namespace HospitalApp.UserPages
{
    /// <summary>
    /// Interaction logic for SelectUser.xaml
    /// </summary>
    public partial class SelectUser
    {
        private IDictionary<string, Person> _personsDictionary;

        public SelectUser()
        {
            InitializeComponent();
            _personsDictionary = new Dictionary<string, Person>();
            LoadAllPerson();

            if (PersonsComboBox.Items.Count == 0)
                throw new InvalidDataException("Persons Database is empty");
            PersonsComboBox.SelectedIndex = 0;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new StartPage());
        }

        private void SelectUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonsComboBox.SelectedValue == null)
            {
                MessageBox.Show("Вам следует выбрать пациента");
                return;
            }

            CurrentState.CurrentPerson = _personsDictionary[PersonsComboBox.SelectedValue.ToString()];
            Switcher.PageSwitcher.Navigate(new UserLoaded());
        }

        private void Search(string lastName, string firstName)
        {
            var personProvider = new PersonProvider(new DatabaseProvider());
            var persons = personProvider.Search(firstName, lastName);
            AddToDictionary(persons);
        }

        private void LoadAllPerson()
        {
            var personProvider = new PersonProvider(new DatabaseProvider());
            var persons = personProvider.Load();
            AddToDictionary(persons);
        }

        private void AddToDictionary(IEnumerable<Person> persons)
        {
            var list = new ObservableCollection<string>();
            
            foreach (var person in persons)
            {
                var name = person.ToString();

                _personsDictionary[name]= person;
                list.Add(name);
            }

            PersonsComboBox.ItemsSource = list;
        }

        private void TextSomeWhereChanged(object sender, TextChangedEventArgs e)
        {
            _personsDictionary = new Dictionary<string, Person>();
            var firstName = SearchByFirstNameTextBox.Text;
            var lastName = SearchByLastNameTextBox.Text;

            if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
                Search(lastName, firstName);
            else
            {
                LoadAllPerson();
            }

            if (PersonsComboBox.Items.Count != 0)
                PersonsComboBox.SelectedIndex = 0;
        }
    }
}
