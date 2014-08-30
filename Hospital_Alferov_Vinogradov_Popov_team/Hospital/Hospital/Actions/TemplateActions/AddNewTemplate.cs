using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Shared;

namespace Hospital
{
    public partial class MainWindow
    {
        private void AddNewTemplateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            WriteNewTemplateTabItem.IsSelected = true;
            TemplateNameTextBox.Text = TemplateNameTextBox.Tag.ToString();
            FieldsNamesListBox.Items.Clear();
            FieldNameTextBox.Text = FieldNameTextBox.Tag.ToString();
        }

        private void AddFieldButton_Click(object sender, RoutedEventArgs e)
        {
            FieldsNamesListBox.Items.Add(FieldNameTextBox.Text);
            FieldNameTextBox.Text = FieldNameTextBox.Tag.ToString();
        }

        private async void SaveTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TemplateNameTextBox.Text.Equals(TemplateNameTextBox.Tag.ToString()) ||
                FieldsNamesListBox.Items.Count == 0)
            {
                MessageBox.Show("Incorrect input!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                await Task.Run(() => _dataAccessLayer.AddTemplate(new Template(data, templateName)));
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClearCanvas();
            MessageBox.Show("Template added successfully!", "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}