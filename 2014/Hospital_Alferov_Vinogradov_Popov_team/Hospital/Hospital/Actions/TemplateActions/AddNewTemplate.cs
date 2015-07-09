using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Shared;

namespace Hospital
{
    public partial class MainWindow
    {
        private bool _isHtmlTemplateLoaded;

        private void AddNewTemplateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            WriteNewTemplateTabItem.IsSelected = true;
            TemplateNameTextBox.Text = TemplateNameTextBox.Tag.ToString();
            FieldsNamesListBox.Items.Clear();
            FieldNameTextBox.Text = FieldNameTextBox.Tag.ToString();
            _isHtmlTemplateLoaded = false;
        }

        private void AddFieldButton_Click(object sender, RoutedEventArgs e)
        {
            FieldsNamesListBox.Items.Add(FieldNameTextBox.Text);
            FieldNameTextBox.Text = FieldNameTextBox.Tag.ToString();
        }

        private void SaveTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TemplateNameTextBox.Text.Equals(TemplateNameTextBox.Tag.ToString()) ||
                FieldsNamesListBox.Items.Count == 0)
            {
                MessageBox.Show("Incorrect input!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_isHtmlTemplateLoaded)
            {
                MessageBox.Show("You should load html template!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var data = new List<string>();

            foreach (object item in FieldsNamesListBox.Items)
            {
                string fieldName = item.ToString();

                if (!data.Contains(fieldName) && !string.IsNullOrEmpty(fieldName))
                {
                    data.Add(item.ToString());
                }
            }

            try
            {
                string templateName = TemplateNameTextBox.Text;
                _dataAccessLayer.AddTemplate(new Template(data, templateName));
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClearCanvas();
            MessageBox.Show("Template added successfully!", "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);

            _isHtmlTemplateLoaded = false;
        }

        private void LoadHtmlTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            var newOutputFormatOpenFileDialog = new OpenFileDialog
            {
                Filter = "html files (*.html)|*.html",
                DefaultExt = ".html",
                Title = "Please select html file"
            };

            if (newOutputFormatOpenFileDialog.ShowDialog() == true)
            {
                string pathToCopy = Path.Combine(AssemblyPath, @"Templates\",
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

                MessageBox.Show("Html template added successfully!", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);

                _isHtmlTemplateLoaded = true;
            }
        }
    }
}