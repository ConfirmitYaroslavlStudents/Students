using System;
using System.Collections.Generic;
using System.Windows;
using LogService;
using Shared;
using Validation;

namespace Hospital
{
    public partial class MainWindow
    {
        public string ValidateFields(Dictionary<string, string> filledPersonFields)
        {
            var firstName = filledPersonFields["First name:"];
            var lastName = filledPersonFields["Last name:"];
            var year = filledPersonFields["Year of birth:"];
            var month = filledPersonFields["Month of birth:"];
            var day = filledPersonFields["Day of birth:"];
            var address = filledPersonFields["Address:"];
            var policyNumber = filledPersonFields["Policy number:"];

            var result = "";
            if (!Validator.CheckName(firstName))
                result += "Name, ";
            if (!Validator.CheckName(lastName))
                result += "Lastname, ";
            if (!Validator.CheckDate(day, month, year))
                result += "Date of Birth, ";
            if (!Validator.CheckAddress(address))
                result += "Address, ";
            if (!Validator.CheckPolicyNumber(policyNumber))
                result += "Policy number, ";

            if (result.Length > 2)
            {
                result = result.Remove(result.Length - 2, 2);
            }

            return result;
        }

        private void AddPersonMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvas();
            _canvasPainter.PaintCanvasWithTextBoxes(_fieldsNamesForAddingPerson, AddPersonOKButton_Click);
            //LOGGING
            Logger.Info("Person was added to canvas");
        }

        private void AddPersonOKButton_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> filledPersonFields = GetInformationFromTextBoxes();

            var errors = ValidateFields(filledPersonFields);
            if (errors.Length != 0)
            {
                //LOGGING
                Logger.Info("User validation failed!!!");
                MessageBox.Show("Check field " + errors, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var filledPersonField in filledPersonFields)
            {
                if (string.IsNullOrEmpty(filledPersonField.Value))
                {
                    //LOGGING
                    Logger.Info("Not all fields are filled!!!");
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
                    //LOGGING
                    Logger.Error("Person was not added to canvas",ex);
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
            catch (FormatException ex)
            {
                //LOGGING
                Logger.Error("Person was not added to canvas. Uncorrect input format.",ex);
                MessageBox.Show("Incorrect input format!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OverflowException ex)
            {
                //LOGGING
                Logger.Error("Person was not added to canvas. Input data is too long.",ex);
                MessageBox.Show("Your input is so long!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentNullException ex)
            {
                //LOGGING
                Logger.Error("Person was not added to canvas. Input data is empty.",ex);
                MessageBox.Show("Your input is so empty value!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                //LOGGING
                Logger.Error("Person was not added to canvas. Unknown exception.",ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}