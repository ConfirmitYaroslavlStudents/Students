using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using HospitalApp.UserPages;
using HospitalLib.Data;
using HospitalLib.Providers;

namespace HospitalApp.AnalysisPages
{
    /// <summary>
    ///     Interaction logic for NewAnalysis.xaml
    /// </summary>
    public partial class NewAnalysis
    {
        private readonly IDictionary<string, Template> _templatesDictionary;

        public NewAnalysis()
        {
            InitializeComponent();
            _templatesDictionary = new Dictionary<string, Template>();
            LoadAllTemplates();
        }

        private void LoadAllTemplates()
        {
            var templateProvider = new TemplateProvider(new DatabaseProvider());
            IList<Template> templates = templateProvider.Load();
            AddToDictionary(templates);
        }

        private void AddToDictionary(IEnumerable<Template> templates)
        {
            var list = new ObservableCollection<string>();

            foreach (Template template in templates)
            {
                string name = template.ToString();

                _templatesDictionary[name] = template;
                list.Add(name);
            }

            TemplatesComboBox.ItemsSource = list;
            if (list.Count == 0)
                throw new InvalidDataException("Templates database is empty");

            TemplatesComboBox.SelectedIndex = 0;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new UserLoaded());
        }

        private void CreateAnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TemplatesComboBox.SelectedValue.ToString()))
            {
                MessageBox.Show("Вам следует выбрать анализ", "Сообщение");
                return;
            }

            CurrentState.CurrentTemplate = _templatesDictionary[TemplatesComboBox.SelectedValue.ToString()];
            CurrentState.CurrentAnalysis = new Analysis(CurrentState.CurrentTemplate, CurrentState.CurrentPerson);

            Switcher.PageSwitcher.Navigate(new AnalysisLoaded());
        }
    }
}