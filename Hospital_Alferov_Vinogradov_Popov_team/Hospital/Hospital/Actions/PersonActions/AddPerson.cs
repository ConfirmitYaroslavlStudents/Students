using System;
using System.Collections.Generic;
using System.Windows;
using Shared;

namespace Hospital
{
    public partial class MainWindow
    {
        private void AddPersonMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvas();
            _canvasPainter.PaintCanvasWithTextBoxes(_fieldsNamesForAddingPerson, AddPersonOKButton_Click);
        }

        private void AddPersonOKButton_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> filledPersonFields = GetInformationFromTextBoxes();

            foreach (var filledPersonField in filledPersonFields)
            {
                if (string.IsNullOrEmpty(filledPersonField.Value))
                {
                    MessageBox.Show("Not all fields are filled!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            try
            {
                _currentPerson = new Person(filledPersonFields["First name:"], filledPersonFields["Last name:"],
                    new DateTime(int.Parse(filledPersonFields["Year of birth:"]),
                        int.Parse(filledPersonFields["Month of birth:"]),
                        int.Parse(filledPersonFields["Day of birth:"])), filledPersonFields["Address:"],
                    filledPersonFields["Policy number:"]);

                ClearCanvas();

                try
                {
                    _dataAccessLayer.AddPerson(_currentPerson);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _currentPerson = null;
                    return;
                }

                MessageBox.Show("Person added successfully!", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                CurrentPersonLabel.Content = string.Format("{0} {1} {2}", _currentPerson.FirstName,
                    _currentPerson.LastName,
                    _currentPerson.PolicyNumber);
                AnalysisMainMenuItem.IsEnabled = true;
                _currentAnalysis = null;
            }
            catch (FormatException)
            {
                MessageBox.Show("Incorrect input format!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Your input is so long!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Your input is so empty value!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}