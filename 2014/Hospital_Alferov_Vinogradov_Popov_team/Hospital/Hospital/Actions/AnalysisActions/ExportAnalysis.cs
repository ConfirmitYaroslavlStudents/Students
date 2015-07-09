using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using PrintersLoaderLibrary;
using Shared;
using Shared.Interfaces;
using TemplateFillerLibrary;

namespace Hospital
{
    public partial class MainWindow
    {
        private void AnalysisExportButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> printersTitles =
                Directory.GetFiles(Path.Combine(AssemblyPath, @"Printers\"), "*.dll",
                    SearchOption.AllDirectories).Select(Path.GetFileNameWithoutExtension);

            if (!printersTitles.Any())
            {
                MessageBox.Show("No printers loaded!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClearCanvas();
            _canvasPainter.PaintCanvasWithListBox(printersTitles, AnalysisExportOKButton_Click);
        }

        private void AnalysisExportOKButton_Click(object sender, RoutedEventArgs e)
        {
            string currentPrinterName = GetSelectedItem();

            if (currentPrinterName == null)
            {
                return;
            }

            IPrinter printer;

            try
            {
                printer =
                     PrintersLoader.LoadPrinter(
                         Path.Combine(AssemblyPath, @"Printers\", currentPrinterName) + ".dll");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (printer == null)
            {
                MessageBox.Show("Incorrect printer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentAnalysis == null)
            {
                MessageBox.Show("You must select an analysis!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var exportSaveFileDialog = new SaveFileDialog
            {
                Filter = "All files (*.*)|*.*",
                FilterIndex = 2,
                RestoreDirectory = true,
                Title = "Please enter a name of exported file"
            };

            if (exportSaveFileDialog.ShowDialog() == true)
            {
                printer.PathToFile = exportSaveFileDialog.FileName;

                try
                {
                    string pathToCurrentHtmlTemplate = Path.Combine(AssemblyPath, @"Templates\",
                        _currentTemplate.Title + ".html");
                    var templateFiller = new TemplateFiller(pathToCurrentHtmlTemplate);

                    printer.Print(templateFiller.FillTemplate(_currentPerson, _currentAnalysis, _currentTemplate));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Analysis exported successfully!", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}