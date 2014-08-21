using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using HospitalLib.Database;

namespace HospitalApplication
{
    /// <summary>
    /// Interaction logic for SelectUserWindow.xaml
    /// </summary>
    public partial class SelectUserWindow
    {
        private IDictionary<string, Person> dict;
        public SelectUserWindow()
        {
            InitializeComponent();

            var personProvider = new PersonProvider();
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            personProvider.DatabasePath = folderBrowserDialog.SelectedPath;
            var persons = personProvider.Load();

            var list = new ObservableCollection<string>();
            dict = new Dictionary<string, Person>();
            foreach (var person in persons)
            {
                var name = person.SecondName + " " + person.FirstName;
                dict.Add(name, person);
                list.Add(name);
            }

            UserComboBox.ItemsSource = list;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            PersonProvider.Person = dict[UserComboBox.Text];
            Close();
        }
    }
}
