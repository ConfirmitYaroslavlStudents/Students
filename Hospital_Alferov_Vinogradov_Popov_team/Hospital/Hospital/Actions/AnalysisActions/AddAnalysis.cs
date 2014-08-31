using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Shared;

namespace Hospital
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private void AddAnalysisMainMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _templates = _dataAccessLayer.GetTemplates();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClearCanvas();
            _canvasPainter.PaintCanvasWithListBox(_templates.Select(template => template.Title),
                ChooseTemplateOKButton_Click);
        }

        private void ChooseTemplateOKButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedTemplateIndex = GetSelectedTemplateIndex();

            if (selectedTemplateIndex == -1)
            {
                MessageBox.Show("There is no selected templates!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _currentTemplate =
                    _dataAccessLayer.GetTemplate(_templates[selectedTemplateIndex].Title);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            ClearCanvas();
            _canvasPainter.PaintCanvasWithTextBoxes(
                _templates[selectedTemplateIndex].Data.Select(item => string.Format("{0}: ", item)),
                AddAnalysisOKButton_Click);
        }

        private void AddAnalysisOKButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> analysisInformation = GetInformationFromTextBoxes().Values.ToList();

            if (_currentPerson == null)
            {
                MessageBox.Show("There is no selected persons!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool hasEmptyFields = analysisInformation.Any(string.IsNullOrEmpty);

            if (hasEmptyFields)
            {
                MessageBox.Show("Not all fields are filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _currentAnalysis = new Analysis(analysisInformation,
                _currentTemplate.Title, DateTime.Now);

            try
            {
                _dataAccessLayer.AddAnalysis(_currentPerson.PolicyNumber, _currentAnalysis);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Analysis added successfully!", "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);

            ClearCanvas();
        }
    }
}