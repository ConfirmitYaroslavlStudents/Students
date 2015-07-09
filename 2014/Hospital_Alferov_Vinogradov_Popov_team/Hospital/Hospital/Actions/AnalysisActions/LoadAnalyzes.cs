using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Shared;

namespace Hospital
{
    public partial class MainWindow
    {
        private void LoadAnalyzesMainMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _analyzes = _dataAccessLayer.GetAnalyzes(_currentPerson.PolicyNumber);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IEnumerable<string> currentPersonTemplatesTitles = from analysis in _analyzes select analysis.TemplateTitle;
            IEnumerable<string> currentPersonDifferentTemplatesTitles = currentPersonTemplatesTitles.Distinct();

            ClearCanvas();
            _canvasPainter.PaintCanvasWithListBox(currentPersonDifferentTemplatesTitles, LoadAnalyzesOKButton_Click);
        }

        private void LoadAnalyzesOKButton_Click(object sender, RoutedEventArgs e)
        {
            string currentTemplateTitle = GetSelectedItem();

            if (string.IsNullOrEmpty(currentTemplateTitle))
            {
                return;
            }

            try
            {
                _currentTemplate = _dataAccessLayer.GetTemplate(currentTemplateTitle);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AnalysisTabItem.IsSelected = true;

            IEnumerable<Analysis> analyzes = from analysis in _analyzes
                where analysis.TemplateTitle == currentTemplateTitle
                select analysis;

            AnalysisTextBox.Clear();
            AnalyzesDatesListBox.Items.Clear();

            foreach (Analysis analysis in analyzes)
            {
                AnalyzesDatesListBox.Items.Add(analysis.Date.ToString(CultureInfo.InvariantCulture));
            }
        }

        private void AnalyzesDatesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = new StringBuilder();

            if (AnalyzesDatesListBox.SelectedItem == null)
            {
                return;
            }

            //так сравниваем, потому что тики пропадают
            _currentAnalysis =
                _analyzes.FirstOrDefault(
                    item =>
                        item.Date.ToString(CultureInfo.InvariantCulture) == AnalyzesDatesListBox.SelectedItem.ToString());

            for (int i = 0; i < _currentTemplate.Data.Count; i++)
            {
                result.AppendFormat("{0}: {1}\n", _currentTemplate.Data[i], _currentAnalysis.Data[i]);
            }

            AnalysisTextBox.Text = result.ToString();
        }
    }
}