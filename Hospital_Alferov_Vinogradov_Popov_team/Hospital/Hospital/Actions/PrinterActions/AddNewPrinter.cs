using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Hospital
{
    public partial class MainWindow
    {
        private void NewOutputFormatMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvas();

            var newOutputFormatOpenFileDialog = new OpenFileDialog
            {
                Filter = "dll files (*.dll)|*.dll",
                DefaultExt = ".dll",
                Title = "Please select dll file"
            };

            if (newOutputFormatOpenFileDialog.ShowDialog() == true)
            {
                string pathToCopy = Path.Combine(AssemblyPath, @"Printers\",
                    Path.GetFileName(newOutputFormatOpenFileDialog.FileName));

                try
                {
                    File.Copy(newOutputFormatOpenFileDialog.FileName, pathToCopy, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Printer added successfully!", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}