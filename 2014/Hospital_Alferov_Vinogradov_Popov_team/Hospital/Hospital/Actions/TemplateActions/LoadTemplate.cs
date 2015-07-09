using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using Shared;

namespace Hospital
{
    public partial class MainWindow
    {
        private void LoadNewTemplateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvas();

            var newOutputFormatOpenFileDialog = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt",
                DefaultExt = ".txt",
                Title = "Please select txt file"
            };

            if (newOutputFormatOpenFileDialog.ShowDialog() == true)
            {
                Template template;

                using (var input = new StreamReader(newOutputFormatOpenFileDialog.FileName, Encoding.Default))
                {
                    string line;
                    var fields = new List<string>();

                    while ((line = input.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            fields.Add(line);
                        }
                    }

                    string fileName = newOutputFormatOpenFileDialog.FileName;
                    template = new Template(fields,
                        Path.GetFileNameWithoutExtension(fileName));
                }

                try
                {
                    _dataAccessLayer.AddTemplate(template);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Template added successfully!", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}