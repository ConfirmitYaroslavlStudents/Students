using System.Windows.Controls;
using HospitalLib.Factory;

namespace HospitalApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var templateLoader = Factory.BuildHtmlLoader();
            templateLoader.Load();

            Switcher.PageSwitcher = this;
            Switcher.Switch(new StartPage());
        }

        public void Navigate(Page nextPage)
        {
            Content = nextPage;
        }
    }
}
