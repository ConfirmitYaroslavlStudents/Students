using System.Windows;
using System.Windows.Controls;
using HospitalLib.Factory;
using HospitalLib.Utils.Logger;
using HospitalLib.Utils.LoggingTarget;

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
            HospitalLogger.AddLoggingTarget(new EventLogLoggingTarget());

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
            //var errorMessage = string.Format("Извините, приложение завершилось с ошибкой: {0}", e.Exception.InnerException.Message);
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
            
            HospitalLogger.LogError("Unexpected errror occurred: \"{0}\" in {1}", e.Exception.Message, e.Exception.TargetSite);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Dispatcher.UnhandledException -= OnDispatcherUnhandledException;
            Close();
        }
    }

}
