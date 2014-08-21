using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using HospitalLib.Database;
using HospitalLib.Parser;
using HospitalLib.Template;

namespace HospitalApplication
{
    /// <summary>
    /// Interaction logic for SelectFromBaseWindow.xaml
    /// </summary>
    public partial class SelectFromBaseWindow
    {
        private IDictionary<string, Template> _dict; 

        public SelectFromBaseWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var name = SearchFirstNameBox.Text;
            var lastName = SearchLastNameBox.Text;
            var storage = new Storage();
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            storage.DatabasePath = folderBrowserDialog.SelectedPath;
            var person = new Person {FirstName = name, SecondName = lastName};

            var history = storage.Search(person, new Parser());
            var list = new ObservableCollection<string>();
            _dict = new Dictionary<string, Template>();

            if (history != null)
            {
                foreach (var template in history)
                {
                    _dict[template.TemplateType] = template;
                    list.Add(template.TemplateType);
                }
            }

            HistoryBox.ItemsSource = list;
        
    }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            FormGenerator.Template = _dict[HistoryBox.Text];
            Close();
        }
    }
}
