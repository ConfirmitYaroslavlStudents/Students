using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LogService;
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
                //LOGGING
                Logger.Error("Templates were not added",ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClearCanvas();
            _canvasPainter.PaintCanvasWithListBox(_templates.Select(template => template.Title),
                ChooseTemplateOKButton_Click);
            //LOGGING
            Logger.Info("Templates were added");
        }

        private void ChooseTemplateOKButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedTemplateIndex = GetSelectedTemplateIndex();

            if (selectedTemplateIndex == -1)
            {
                //LOGGING
                Logger.Info("Templates were not choosen");
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
                //LOGGING
                Logger.Error("Current template was not obtained",ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            ClearCanvas();
            _canvasPainter.PaintCanvasWithTextBoxes(
                _templates[selectedTemplateIndex].Data.Select(item => string.Format("{0}: ", item)),
                AddAnalysisOKButton_Click);
            //LOGGING
            Logger.Info("Template was choosen");
        }

        private void AddAnalysisOKButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> analysisInformation = GetInformationFromTextBoxes().Values.ToList();

            if (_currentPerson == null)
            {
                //LOGGING
                Logger.Info("Anylysis was not choosen");
                MessageBox.Show("There is no selected persons!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool hasEmptyFields = analysisInformation.Any(string.IsNullOrEmpty);

            if (hasEmptyFields)
            {

                //LOGGING
                Logger.Info("Not all fields in template are filled");
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
                //LOGGING
                Logger.Error("Anylysis was added",ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //LOGGING
            Logger.Info("Anylysis was added");
            MessageBox.Show("Analysis added successfully!", "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);

            ClearCanvas();
        }
    }
}