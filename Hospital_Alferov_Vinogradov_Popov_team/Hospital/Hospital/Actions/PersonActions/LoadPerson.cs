using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Shared;

namespace Hospital
{
    public partial class MainWindow
    {
        private void LoadPersonMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoadPersonTabItem.IsSelected = true;
            List<Person> persons;

            try
            {
                persons = _dataAccessLayer.GetPersons(string.Empty, string.Empty, string.Empty);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PersonsDataGrid.ItemsSource = persons;
        }

        private void SearchPersonButton_Click(object sender, RoutedEventArgs e)
        {
            List<Person> persons;

            try
            {
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string policyNumber = PolicyNumberTextBox.Text;
                persons = _dataAccessLayer.GetPersons(firstName, lastName, policyNumber);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PersonsDataGrid.ItemsSource = persons;
        }

        private void LoadPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonsDataGrid.Items.Count != 0)
            {
                int selectedIndex = 0;

                if (PersonsDataGrid.SelectedIndex != -1)
                {
                    selectedIndex = PersonsDataGrid.SelectedIndex;
                }

                _currentPerson = PersonsDataGrid.Items[selectedIndex] as Person;
                CurrentPersonLabel.Content = string.Format("{0} {1} {2}", _currentPerson.FirstName,
                    _currentPerson.LastName,
                    _currentPerson.PolicyNumber);
                AnalysisMainMenuItem.IsEnabled = true;
                ClearCanvas();
            }
        }

        private void PersonsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoadPersonButton_Click(null, null);
        }
    }
}