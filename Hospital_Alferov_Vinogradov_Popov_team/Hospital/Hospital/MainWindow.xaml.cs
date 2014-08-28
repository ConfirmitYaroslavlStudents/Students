using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using HospitalConnectedLayer;
using Microsoft.Win32;
using PrintersLoader;
using Shared;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly CanvasPainter _canvasPainter;
        private readonly HospitalDAL _dataAccessLayer;
        private Person _currentPerson;
        private List<Template> _templates;
        private List<Analysis> _analyzes;
        private string _currentTemplateTitle;

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

        private readonly List<string> _fieldsNamesForLoadingPerson = new List<string>
        {
            "First name:",
            "Last name:",
            "Policy number:" 
        };

        private Analysis _lastAddedAnalysis;

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
            var filledPersonFields = GetInformationFromTextBoxes();

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
                    new DateTime(int.Parse(filledPersonFields["Year of birth:"]), int.Parse(filledPersonFields["Month of birth:"]),
                        int.Parse(filledPersonFields["Day of birth:"])), filledPersonFields["Address:"], filledPersonFields["Policy number:"]);

                ClearCanvas();

                if (_dataAccessLayer.AddPerson(_currentPerson))
                {
                    MessageBox.Show("Person added successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    CurrentPersonLabel.Content = string.Format("{0} {1} {2}", _currentPerson.FirstName, _currentPerson.LastName,
                        _currentPerson.PolicyNumber);
                    AnalysisMainMenuItem.IsEnabled = true;
                    _lastAddedAnalysis = null;
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
            var persons = _dataAccessLayer.GetPersons("", "", "");
            PersonsDataGrid.ItemsSource = persons;
        }
        private void LoadPersonOKButton_Click(object sender, RoutedEventArgs e)
        {
            //        var foundPersons = new List<Person>();
            //        var currentInformationAboutNesessaryPerson = GetInformationFromTextBoxes();

            //        if (!string.IsNullOrEmpty(currentInformationAboutNesessaryPerson["First name:"]))
            //        {
            //            foundPersons = _dataStorage.GetPersons("FirstName", currentInformationAboutNesessaryPerson["First name:"]);
            //        }

            //        if (!string.IsNullOrEmpty(currentInformationAboutNesessaryPerson["Last name:"]))
            //        {
            //            foundPersons = _dataStorage.GetPersons("LastName", currentInformationAboutNesessaryPerson["Last name:"]);
            //        }

            //        if (!string.IsNullOrEmpty(currentInformationAboutNesessaryPerson["Policy number:"]))
            //        {
            //            foundPersons = _dataStorage.GetPersons("PolicyNumber", currentInformationAboutNesessaryPerson["Policy number:"]);
            //        }

            //        if (foundPersons.Count != 0)
            //        {
            //            MessageBox.Show("Person loaded successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            //            _currentPerson = foundPersons[0];
            //            CurrentPersonLabel.Content = string.Format("{0} {1} {2}", _currentPerson.FirstName, _currentPerson.LastName,
            //_currentPerson.PolicyNumber);
            //            ClearCanvas();
            //            AnalysisMainMenuItem.IsEnabled = true;
            //            AnalysisExport.IsEnabled = false;
            //            _lastAddedAnalysis = null;
            //        }
        }

        #endregion

        #region AddAnalysis

        private void AddAnalyzesMainMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _templates = _dataAccessLayer.GetTemplates();

            ClearCanvas();
            _canvasPainter.PaintCanvasWithListBox(_templates.Select(template => template.Title), ChooseTemplateOKButton_Click);
        }

        private void ChooseTemplateOKButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTemplateIndex = GetSelectedTemplateIndex();

            if (selectedTemplateIndex == -1)
            {
                MessageBox.Show("There is no selected templates!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _currentTemplateTitle = _templates[selectedTemplateIndex].Title;

            ClearCanvas();
            _canvasPainter.PaintCanvasWithTextBoxes(_templates[selectedTemplateIndex].Data.Select(item => string.Format("{0}:", item)), AddAnalysisOKButton_Click);
        }

        private void AddAnalyzesOKButton_Click(object sender, RoutedEventArgs e)
        {
            var analysisInformation = GetInformationFromTextBoxes().Keys.ToList();

            if (_currentPerson == null)
            {
                MessageBox.Show("There is no selected persons!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var emptyFieldsCount = analysisInformation.Count(string.IsNullOrEmpty);

            if (emptyFieldsCount != 0)
            {
                MessageBox.Show("Not all fields are filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _lastAddedAnalysis = new Analysis(analysisInformation, _currentTemplateTitle, DateTime.Now);

            try
            {
                _dataAccessLayer.AddAnalysis(_currentPerson.PolicyNumber, _lastAddedAnalysis);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Analysis added successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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

            var currentPersonTemplatesTitles = from analysis in _analyzes select analysis.TemplateTitle;
            var currentPersonDifferentTemplatesTitles = currentPersonTemplatesTitles.Distinct();

            ClearCanvas();
            _canvasPainter.PaintCanvasWithListBox(currentPersonDifferentTemplatesTitles, LoadAnalyzesOKButton_Click);
        }

        private void LoadAnalyzesOKButton_Click(object sender, RoutedEventArgs e)
        {

            _currentTemplateTitle = GetSelectedItem();

            if (_currentTemplateTitle == null)
            {
                return;
            }

            AnalysisTabItem.IsSelected = true;

            var analyzes = from analysis in _analyzes where analysis.TemplateTitle == _currentTemplateTitle select analysis;

            AnalysisTextBox.Clear();

            var sb = new StringBuilder();

            foreach (var analysis in analyzes)
            {
                sb.Append(analysis);
            }

            AnalysisTextBox.Text = sb.ToString();
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
            if (TemplateNameTextBox.Text.Equals("Name of template") || FieldsNamesListBox.Items.Count == 0)
            {
                MessageBox.Show("Incorrect input!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var data = new List<string>();

            foreach (var item in FieldsNamesListBox.Items)
            {
                if (!data.Contains(item.ToString()))
                {
                    data.Add(item.ToString());
                }
            }

            try
            {
                if (!_dataAccessLayer.AddTemplate(new Template(data, TemplateNameTextBox.Text)))
                {
                    MessageBox.Show("Template with the same name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClearCanvas();
            MessageBox.Show("Template added successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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
                string pathToCopy = Path.Combine(Environment.CurrentDirectory, @"Printers\", Path.GetFileName(newOutputFormatOpenFileDialog.FileName));
                File.Copy(newOutputFormatOpenFileDialog.FileName, pathToCopy);
                MessageBox.Show("Printer added successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        #endregion

        private void LoadTemplatesOKButton_Click(object sender, RoutedEventArgs e)
        {
            var printersTitles = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"Printers\"), "*.dll", SearchOption.AllDirectories).Select(Path.GetFileNameWithoutExtension);

            if (!printersTitles.Any())
            {
                MessageBox.Show("No printers loaded!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var printer = PrintersLoaderLib.LoadPrinter(Path.Combine(Environment.CurrentDirectory, @"Printers\", currentOutputName) + ".dll");

            if (printer == null)
            {
                MessageBox.Show("Incorrect printer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //printer.Print(_currentPerson, _lastAddedAnalysis, _lastAddedAnalysis.TemplateTitle.Replace(" ", string.Empty) +_lastAddedAnalysis.Date.Year + _lastAddedAnalysis.Date.Month + _lastAddedAnalysis.Date.Day + _lastAddedAnalysis.Date.Second);
        }

        private void AnalysisExport_Click(object sender, RoutedEventArgs e)
        {
            var templatesTitles = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, @"Printers\"), "*.dll", SearchOption.AllDirectories).Select(Path.GetFileNameWithoutExtension);
            ClearCanvas();
            _canvasPainter.PaintCanvasWithListBox(templatesTitles, LoadTemplatesOKButton_Click);
            MessageBox.Show("Analysis exported successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadNewTemplateMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //var xmlFormatter = new XmlFormatterSlim();
            //ClearCanvas();
            //var newOutputFormatOpenFileDialog = new OpenFileDialog { Filter = "xml files (*.xml)|*.xml", DefaultExt = ".xml" };
            //newOutputFormatOpenFileDialog.Title = "Please select xml file";

            //if (newOutputFormatOpenFileDialog.ShowDialog() == true)
            //{
            //    var template2 =
            //        new Template(xmlFormatter.Deserialize(File.ReadAllText(newOutputFormatOpenFileDialog.FileName)),
            //            Path.GetFileNameWithoutExtension(newOutputFormatOpenFileDialog.FileName));
            //    _dataAccessLayer.AddTemplate(template2);
            //    MessageBox.Show("Tempate added successfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }

        private string GetSelectedItem()
        {
            foreach (var item in MainCanvas.Children)
            {
                if (item is ListBox)
                {
                    var listBox = item as ListBox;

                    if (listBox.SelectedItem == null)
                    {
                        MessageBox.Show("There is no selected templates!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    return listBox.SelectedItem.ToString();
                }
            }
            return null;
        }
        private void ClearCanvas()
        {
            GeneratedTabItem.IsSelected = true;
            MainCanvas.Children.Clear();
        }
        private Dictionary<string, string> GetInformationFromTextBoxes()
        {
            var result = new Dictionary<string, string>();

            foreach (var item in MainCanvas.Children)
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
            foreach (var item in MainCanvas.Children)
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

        private void MainWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _dataAccessLayer.CloseConnection();
        }

        private void SearchPersonButton_Click(object sender, RoutedEventArgs e)
        {
            var persons = _dataAccessLayer.GetPersons(FirstNameTextBox.Text, LastNameTextBox.Text, PolicyNumberTextBox.Text);
            PersonsDataGrid.ItemsSource = persons;
        }
    }
}