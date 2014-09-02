using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using HospitalConnectedLayer;
using Shared;

namespace Hospital
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static readonly string AssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Substring(8));

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
            string dataProvider = ConfigurationManager.AppSettings["provider"];

            try
            {
                _dataAccessLayer = new HospitalDAL(dataProvider, connectionString);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            }

            _canvasPainter = new CanvasPainter(MainCanvas);
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

                    //we added in all of textboxes "First Name: " string in Tag(in CanvasPrinter) prop, so we can extract field name with
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

        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {
            const string message = "Stydents Yaroslavl Demidov State University, Mathematics faculty, " +
                                   "Computer Security 31\nVinogradov Kirill - GUI\nAlferov Roman - DAL, Printers\nPopov Sergey - Printers";
            MessageBox.Show(message, "About Us", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}