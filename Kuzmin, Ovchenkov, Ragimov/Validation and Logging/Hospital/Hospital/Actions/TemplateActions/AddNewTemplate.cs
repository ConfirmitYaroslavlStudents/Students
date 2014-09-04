using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using LogService;
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
            //LOGGING
            // WHAT ABOUT TRY/CATCH?
            Logger.Info("New template was successfully added.");
        }

        private void AddFieldButton_Click(object sender, RoutedEventArgs e)
        {
            FieldsNamesListBox.Items.Add(FieldNameTextBox.Text);
            FieldNameTextBox.Text = FieldNameTextBox.Tag.ToString();
            //LOGGING
            Logger.Info("Field was successfully added.");
        }

        private void SaveTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TemplateNameTextBox.Text.Equals(TemplateNameTextBox.Tag.ToString()) ||
                FieldsNamesListBox.Items.Count == 0)
            {
                //LOGGING
                Logger.Warn("Template was not saved. Uncorrect input.");
                MessageBox.Show("Incorrect input!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_isHtmlTemplateLoaded)
            {
                //LOGGING
                Logger.Warn("Template was not saved. Current template is not html.");
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
                //LOGGING
                Logger.Info("Template was successfully added to data access layer.");
            }
            catch (InvalidOperationException ex)
            {
                //LOGGING
                Logger.Error("Template was not added to data access layer.",ex);
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
                string pathToCopy = Path.Combine(Environment.CurrentDirectory, @"Templates\",
                    Path.GetFileName(newOutputFormatOpenFileDialog.FileName));

                try
                {
                    File.Copy(newOutputFormatOpenFileDialog.FileName, pathToCopy, true);
                    //LOGGING
                    Logger.Info("Html-Template was successfully loaded");
                }
                catch (Exception ex)
                {
                    //LOGGING
                    Logger.Info("Html-Template was  not loaded");
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