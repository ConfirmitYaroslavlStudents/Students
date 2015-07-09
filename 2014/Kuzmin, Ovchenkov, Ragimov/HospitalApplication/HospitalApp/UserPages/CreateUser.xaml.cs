using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HospitalLib.Data;
using HospitalLib.Factory;
using HospitalLib.Providers;

namespace HospitalApp.UserPages
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser
    {
        public CreateUser()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new StartPage());
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Check()) return;

            var person = CreatePerson();
            SavePerson(person);
            CurrentState.CurrentPerson = person;

            Switcher.PageSwitcher.Navigate(new UserLoaded());
        }

        private bool Check()
        {
            var result = CheckTextBox(FirstNameTextBox);
            result &= CheckTextBox(LastNameTextBox);
            result &= CheckTextBox(MiddleNameTextBox);
            result &= CheckDatePicker(BirthDayDatePicker);

            return result;
        }

        private bool CheckDatePicker(DatePicker datePicker)
        {
            DateTime result;
            if (string.IsNullOrEmpty(datePicker.Text) || !DateTime.TryParse(datePicker.Text, out result))
            {
                datePicker.Background = Brushes.LightPink;
                return false;
            }

            return true;
        }

        private bool CheckTextBox(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.Text))
                return true;
           
            textBox.Background = Brushes.LightPink;
            return false;
        }

        private void SavePerson(Person person)
        {
            var personProvider = new PersonProvider(new DatabaseProvider());
            personProvider.Save(ref person);
        }

        private Person CreatePerson()
        {
            var fistName = FirstNameTextBox.Text;
            var lastName = LastNameTextBox.Text;
            var middleName = MiddleNameTextBox.Text;
            var birthDate = DateTime.Parse(BirthDayDatePicker.Text);

            return Factory.BuidPerson(fistName, lastName, middleName, birthDate);
        }

        private void LastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LastNameTextBox.Background = Brushes.White;
        }

        private void FirstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FirstNameTextBox.Background = Brushes.White;
        }

        private void MiddleNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MiddleNameTextBox.Background = Brushes.White;
        }

        private void BirthDayDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            BirthDayDatePicker.Background = Brushes.White;
        }
    }
}
