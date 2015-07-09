using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using LogService;
using Microsoft.Win32;
using PrintersLoaderLibrary;
using Shared;
using TemplateFillerLibrary;

namespace Hospital
{
    public partial class MainWindow
    {
        private void AnalysisExportButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> printersTitles =
                Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"Printers\"), "*.dll",
                    SearchOption.AllDirectories).Select(Path.GetFileNameWithoutExtension);

            if (!printersTitles.Any())
            {
                //LOGGING
                Logger.Warn("No printers loaded!");
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
                //LOGGING
                Logger.Info("Analysis was not exported. Printer's name was not initialized");
                return;
            }

            Printer printer =
                PrintersLoader.LoadPrinter(
                    Path.Combine(Environment.CurrentDirectory, @"Printers\", currentPrinterName) + ".dll");

            if (printer == null)
            {
                //LOGGING
                Logger.Warn("Analysis was not exported. Uncorrect printer");
                MessageBox.Show("Incorrect printer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_currentAnalysis == null)
            {
                //LOGGING
                Logger.Warn("Analysis was not exported. Current Analysis was not selected.");
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
                    string pathToCurrentHtmlTemplate = Path.Combine(Environment.CurrentDirectory, @"Templates\",
                        _currentTemplate.Title + ".html");
                    var templateFiller = new TemplateFiller(pathToCurrentHtmlTemplate);

                    printer.Print(templateFiller.FillTemplate(_currentPerson, _currentAnalysis, _currentTemplate));
                    //LOGGING
                    Logger.Info("Analysis was exporeted.");
                }
                catch (Exception ex)
                {
                    //LOGGING
                    Logger.Info("Analysis was not exporeted. Unknown exception.");
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Analysis exported successfully!", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}