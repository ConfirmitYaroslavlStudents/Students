using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using HospitalConnectedLayer;
using Microsoft.Win32;
using PrintersLoaderLibrary;
using Shared;
using Shared.Interfaces;

namespace Hospital
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly CanvasPainter _canvasPainter;
        private readonly HospitalDAL _dataAccessLayer;

        private readonly List<string> _fieldsNamesForAddingPerson = new List<string>
        {
            "First name:",
            "Last name:",
            "Year of birth:",
            "Month of birth:",
            "Day of birth:",
            "Address:",
            "Policy number:"
        };

        private List<Analysis> _analyzes;
        private Analysis _currentAnalysis;
        private Person _currentPerson;
        private Template _currentTemplate;
        private List<Template> _templates;

        public MainWindow()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string dp = ConfigurationManager.AppSettings["provider"];

            try
            {
                _dataAccessLayer = new HospitalDAL();
                _dataAccessLayer.OpenConnection(dp, connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            }

            _canvasPainter = new CanvasPainter(MainCanvas);
        }

        #region AddPerson

        private void AddPersonMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvas();
            _canvasPainter.PaintCanvasWithTextBoxes(_fieldsNamesForAddingPerson, AddPersonOKButton_Click);
        }

        private void AddPersonOKButton_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> filledPersonFields = GetInformationFromTextBoxes();

            foreach (var filledPersonField in filledPersonFields)
            {
                if (string.IsNullOrEmpty(filledPersonField.Value))
                {
                    MessageBox.Show("Not all fields are filled!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            try
            {
                _currentPerson = new Person(filledPersonFields["First name:"], filledPersonFields["Last name:"],
                    new DateTime(int.Parse(filledPersonFields["Year of birth:"]),
                        int.Parse(filledPersonFields["Month of birth:"]),
                        int.Parse(filledPersonFields["Day of birth:"])), filledPersonFields["Address:"],
                    filledPersonFields["Policy number:"]);

                ClearCanvas();

                if (_dataAccessLayer.AddPerson(_currentPerson))
                {
                    MessageBox.Show("Person added successfully!", "Information", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    CurrentPersonLabel.Content = string.Format("{0} {1} {2}", _currentPerson.FirstName,
                        _currentPerson.LastName,
                        _currentPerson.PolicyNumber);
                    AnalysisMainMenuItem.IsEnabled = true;
                    _currentAnalysis = null;
                }
                else
                {
                    _currentPerson = null;
                    MessageBox.Show("This person already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region LoadPerson

        private void LoadPersonMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoadPersonTabItem.IsSelected = true;
            List<Person> persons;

            try
            {
                persons = _dataAccessLayer.GetPersons("", "", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PersonsDataGrid.ItemsSource = persons;
        }

        private void SearchPersonButton_Click(object sender, RoutedEventArgs e)
        {
            List<Person> persons = _dataAccessLayer.GetPersons(FirstNameTextBox.Text, LastNameTextBox.Text,
                PolicyNumberTextBox.Text);
            PersonsDataGrid.ItemsSource = persons;
        }

        private void LoadPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonsDataGrid.Items.Count != 0)
            {
                int selectedIndex = 0;

                if (PersonsDataGrid.SelectedIndex != -1)
                {
                    selectedIndex = PersonsDataGrid.SelectedIndex;
                }
                _currentPerson = PersonsDataGrid.Items[selectedIndex] as Person;
                CurrentPersonLabel.Content = string.Format("{0} {1} {2}", _currentPerson.FirstName,
                    _currentPerson.LastName,
                    _currentPerson.PolicyNumber);
                AnalysisMainMenuItem.IsEnabled = true;
                ClearCanvas();
            }
        }

        private void PersonsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoadPersonButton_Click(null, null);
        }

        #endregion

        #region AddAnalysis

        private void AddAnalysisMainMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _templates = _dataAccessLayer.GetTemplates();

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
                _currentTemplate = _dataAccessLayer.GetTemplate(_templates[selectedTemplateIndex].Title);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            ClearCanvas();
            _canvasPainter.PaintCanvasWithTextBoxes(
                _templates[selectedTemplateIndex].Data.Select(item => string.Format("{0}:", item)),
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

            int emptyFieldsCount = analysisInformation.Count(string.IsNullOrEmpty);

            if (emptyFieldsCount != 0)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Analysis added successfully!", "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);

            ClearCanvas();
        }

        #endregion

        #region LoadAnalyzes

        private void LoadAnalyzesMainMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _analyzes = _dataAccessLayer.GetAnalyzes(_currentPerson.PolicyNumber);
            }
            catch (Exception ex)
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
            catch (Exception ex)
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

        private void AnalyzesDatesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = new StringBuilder();

            if (AnalyzesDatesListBox.SelectedItem == null)
            {
                return;
            }

            //так ставниваем, потому что тики пропадают
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

        #endregion

        #region WriteNewTemplate

        private void WriteNewTemplateMenuItem_Click(object sender, RoutedEventArgs e)
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

        private void SaveTemplateButton_Click(object sender, RoutedEventArgs e)
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
                if (!_dataAccessLayer.AddTemplate(new Template(data, TemplateNameTextBox.Text)))
                {
                    MessageBox.Show("Template with the same name already exists!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClearCanvas();
            MessageBox.Show("Template added successfully!", "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        #endregion

        #region AddNewPrinter

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
                string pathToCopy = Path.Combine(Environment.CurrentDirectory, @"Printers\",
                    Path.GetFileName(newOutputFormatOpenFileDialog.FileName));

                try
                {
                    File.Copy(newOutputFormatOpenFileDialog.FileName, pathToCopy);
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

        #endregion

        #region ExportAnalysis

        private void AnalysisExportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> printersTitles =
                Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"Printers\"), "*.dll",
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

            IPrinter printer =
                PrintersLoader.LoadPrinter(
                    Path.Combine(Environment.CurrentDirectory, @"Printers\", currentPrinterName) + ".dll");

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
                printer.Print(_currentPerson, _currentAnalysis, _currentTemplate);
                MessageBox.Show("Analysis exported successfully!", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        #endregion

        private void LoadNewTemplateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var xmlFmt = new XmlSerializer(typeof (Template));

            using (Stream fStream = new FileStream("template.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFmt.Serialize(fStream, new Template(new List<string> {"hemoglobin", "erythrocytes"}, "Blood Test"));
            }


            ClearCanvas();

            var newOutputFormatOpenFileDialog = new OpenFileDialog
            {
                Filter = "xml files (*.xml)|*.xml",
                DefaultExt = ".xml",
                Title = "Please select xml file"
            };

            if (newOutputFormatOpenFileDialog.ShowDialog() == true)
            {
                Template template;

                using (Stream fStream = File.OpenRead("circle.xml"))
                {
                    template = xmlFmt.Deserialize(fStream) as Template;
                    if (template == null)
                    {
                        MessageBox.Show("Incorrect template format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                try
                {
                    if (!_dataAccessLayer.AddTemplate(template))
                    {
                        MessageBox.Show("Template with this name already exists!", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Template added successfully!", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void MainWin_Closing(object sender, CancelEventArgs e)
        {
            _dataAccessLayer.CloseConnection();
        }

        #region SharedMethods

        private string GetSelectedItem()
        {
            foreach (object item in MainCanvas.Children)
            {
                if (item is ListBox)
                {
                    var listBox = item as ListBox;

                    if (listBox.SelectedItem == null)
                    {
                        MessageBox.Show("There is no selected templates!", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return null;
                    }
                    return listBox.SelectedItem.ToString();
                }
            }
            return null;
        }

        private void ClearCanvas()
        {
            MainTabItem.IsSelected = true;
            MainCanvas.Children.Clear();
        }

        private Dictionary<string, string> GetInformationFromTextBoxes()
        {
            var result = new Dictionary<string, string>();

            foreach (object item in MainCanvas.Children)
            {
                if (item is TextBox)
                {
                    var textBox = item as TextBox;

                    //we added in all of textboxes "First Name: " string in Tag(in GacnvasFactory) prop, so we can extract field name with
                    //_fieldsNamesForAddingPerson dictionary
                    result[textBox.Tag.ToString()] = textBox.Text;
                }
            }

            return result;
        }

        private int GetSelectedTemplateIndex()
        {
            int selectedIndex = -1;
            foreach (object item in MainCanvas.Children)
            {
                if (item is ListBox)
                {
                    var listBox = item as ListBox;
                    selectedIndex = listBox.SelectedIndex;
                    break;
                }
            }

            return selectedIndex;
        }

        #endregion
    }
}