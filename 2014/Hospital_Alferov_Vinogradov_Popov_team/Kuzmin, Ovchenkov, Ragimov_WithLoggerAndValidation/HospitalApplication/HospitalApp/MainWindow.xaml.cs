using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using HospitalLib.Factory;
using HospitalLib.Loader;
using HospitalLib.Utils;
using HospitalLib.Utils.Logger;
using HospitalLib.Utils.LoggingTarget;

namespace HospitalApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            HospitalLogger.AddLoggingTarget(new FileLoggingTarget());

            try
            {
                HtmlLoader templateLoader = Factory.BuildHtmlLoader();
                templateLoader.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                HospitalLogger.LogError("Unexpected errror occurred: \"{0}\" in {1}", ex.Message,
                    ex.TargetSite);
                Environment.Exit(-1);
            }

            Switcher.PageSwitcher = this;
            Switcher.Switch(new StartPage());
        }

        public void Navigate(Page nextPage)
        {
            Content = nextPage;
        }

        private void OnDispatcherUnhandledException(object sender,
            DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;

            HospitalLogger.LogError("Unexpected errror occurred: \"{0}\" in {1}", e.Exception.Message,
                e.Exception.TargetSite);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Dispatcher.UnhandledException -= OnDispatcherUnhandledException;
            Close();
        }
    }
}