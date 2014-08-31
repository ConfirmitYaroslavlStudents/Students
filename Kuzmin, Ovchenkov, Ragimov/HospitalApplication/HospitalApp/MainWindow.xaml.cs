using System.Windows;
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

            Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            var templateLoader = Factory.BuildHtmlLoader();
            templateLoader.Load();

            Switcher.PageSwitcher = this;
            Switcher.Switch(new StartPage());
        }

        public void Navigate(Page nextPage)
        {
            Content = nextPage;
        }

        private void OnDispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var errorMessage = string.Format("Извините, приложение завершилось с ошибкой: {0}", e.Exception.InnerException.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Dispatcher.UnhandledException -= OnDispatcherUnhandledException;
            Close();
        }
    }

}
