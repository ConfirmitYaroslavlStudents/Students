using System;
using System.Windows;
using System.Windows.Forms;
using HospitalLib.Database;
using HospitalLib.Parser;
using HospitalLib.Utils;

namespace HospitalApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            var chooseFile = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Filter = Properties.Resources.filter
            };
            if (chooseFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var fileName = chooseFile.FileName;
                GenerateTemplate(fileName);
            }
        }

        private void GenerateTemplate(string fileName)
        {
            var parser = new Parser();
            var generator = new FormGenerator();
            FormGenerator.Template = parser.Load(fileName);;
            generator.GenerateForm(BlankGrid, HospitalMainWindow);
        }

        private void LoadFromBaseButton_Click(object sender, RoutedEventArgs e)
        {
            var dataBaseWindow = new SelectFromBaseWindow();
            dataBaseWindow.Show();
            dataBaseWindow.Closed += dataBaseWindowClosedHandler;
        }
        public void dataBaseWindowClosedHandler(object sender, EventArgs eventArgs)
        {
            var generator = new FormGenerator();
            generator.GenerateForm(BlankGrid, HospitalMainWindow);
        }

        public void UserWindowClosedHandler(object sender, EventArgs eventArgs)
        {
            FirstdNameTextBox.Text = PersonProvider.Person.FirstName;
            SecondNameTextBox.Text = PersonProvider.Person.SecondName;
        }

        private void ChooseUserButton_Click(object sender, RoutedEventArgs e)
        {
            var userWindow = new SelectUserWindow();
            userWindow.Show();
            userWindow.Closed += UserWindowClosedHandler;
        }

        private void PrinButton_Click(object sender, RoutedEventArgs e)
        {
             var name = FirstdNameTextBox.Text;
            var lastName = SecondNameTextBox.Text;
             var person = new Person { FirstName = name, SecondName = lastName };
            var printer = new Printer();
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            printer.PrinterPath = folderBrowserDialog.SelectedPath;
            printer.Print(FormGenerator.Template, person);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var name = FirstdNameTextBox.Text;
            var lastName = SecondNameTextBox.Text;
            var storage = new Storage();
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            storage.DatabasePath = folderBrowserDialog.SelectedPath;
            var person = new Person { FirstName = name, SecondName = lastName };
            storage.Save(FormGenerator.Template, person);
        }

        private void SaveUserButton_Click(object sender, RoutedEventArgs e)
        {
            var person = new Person { FirstName = FirstdNameTextBox.Text, SecondName = SecondNameTextBox.Text };
            var personProvider = new PersonProvider();
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            personProvider.DatabasePath = folderBrowserDialog.SelectedPath;
            personProvider.Save(person);
        }
    }
}
