using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using HospitalApp.UserPages;
using HospitalLib.Data;
using HospitalLib.Providers;
using HospitalLib.Utils;
using mshtml;

namespace HospitalApp.AnalysisPages
{
    /// <summary>
    /// Interaction logic for AnalysisLoaded.xaml
    /// </summary>
    public partial class AnalysisLoaded
    {
        public AnalysisLoaded()
        {
            InitializeComponent();
            UserLabel.Content = CurrentState.CurrentPerson.ToString();
            WebBrowser.NavigateToString(CurrentState.CurrentTemplate.HtmlTemplate);
           
        }

        private void LoadData()
        {
            var dom = (HTMLDocument)WebBrowser.Document;

            if (dom != null)
            {
                var elems = dom.getElementsByTagName("INPUT");
                foreach (HTMLInputElement elem in elems)
                {
                    string name = elem.getAttribute("name");
                    if (!string.IsNullOrEmpty(name))
                    {
                        if (CurrentState.CurrentAnalysis.GetData(name) != null)
                            elem.value = CurrentState.CurrentAnalysis.GetData(name);
                    }
                }
            }
        }

        private void ChangeUserButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new StartPage());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new UserLoaded());
        }

        private void PrintAnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            var printer = new Printer();
            printer.Print(GetFileName(), GetText());
            ChangeOpacity(PrintAnalysisButton, 350);
        }

        private string GetText()
        {
            var parser = new HtmlToTextParser();
            dynamic doc = WebBrowser.Document;
            var htmlText = doc.documentElement.InnerHtml;

            return parser.Parse(htmlText);
        }

        private string GetFileName()
        {
            var analysis = CurrentState.CurrentAnalysis;
            var person = CurrentState.CurrentPerson;

            var result = person.Id + "_" + person.FirstName + "_" + person.LastName +
                "_" + analysis.GetTemplateName() + "_" + analysis.CreationTime.ToShortDateString();
           
            return result;
        }

        private void SaveAnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            var analysis = GetAnalysis();
            var analysisProvider = new AnalysisProvider(new DatabaseProvider());
            if (analysis.New)
            {
                CurrentState.CurrentAnalysis.New = false;
                analysisProvider.Save(ref analysis);
            }
            else
                analysisProvider.Update(analysis);

            ChangeOpacity(SaveAnalysisButton, 350);
        }



        private void ChangeOpacity(Button control, int speed, bool fade=true)
        {
            var storyboard = new Storyboard();
            var duration = new TimeSpan(0, 0, 0, 0, speed); 
            DoubleAnimation animation;
            if (fade)
            {
                control.Style = (Style)FindResource("RoundCornerGreen");
                animation = new DoubleAnimation { From = 1.0, To = 0.0, Duration = new Duration(duration) };
                storyboard.Completed += (s, ea) => ChangeOpacity(control, speed, false);
            }

            else
            {
                animation = new DoubleAnimation { From = 0.0, To = 1.0, Duration = new Duration(duration) };
                storyboard.Completed += (s, ea) =>
                {
                    control.Style = (Style)FindResource("RoundCorner");
                };
            }

            Storyboard.SetTargetName(animation, control.Name);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity", 0));
            storyboard.Children.Add(animation);

            storyboard.Begin(control);
        }

        private Analysis GetAnalysis()
        {
            var dom = (HTMLDocument)WebBrowser.Document;

            if (dom != null)
            {
                var elems = dom.getElementsByTagName("INPUT");
                foreach (HTMLInputElement elem in elems)
                {
                    string value = elem.getAttribute("value");
                    string name = elem.getAttribute("name");
                    if (!string.IsNullOrEmpty(name))
                    {
                        CurrentState.CurrentAnalysis.AddData(name, value);
                    }
                }
            }
            CurrentState.CurrentAnalysis.Template = CurrentState.CurrentTemplate;
            CurrentState.CurrentAnalysis.Person = CurrentState.CurrentPerson;
          
            return CurrentState.CurrentAnalysis;
        }

        private void WebBrowser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            LoadData();
        }
    }
}
