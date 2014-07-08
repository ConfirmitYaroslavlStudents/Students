using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeLocker
{
    public partial class MainLockerForm : Form
    {
        Timer _printRemaingTimeTimer;
        Locktimer _lockTimer;
		WindowsLocker _windowsLocker;

        public MainLockerForm()
        {
            InitializeComponent();

			trayIcon.Icon = new System.Drawing.Icon("App.ico");

			_windowsLocker = new WindowsLocker();

			_lockTimer = new Locktimer(_windowsLocker);

            _printRemaingTimeTimer = new Timer();
            _printRemaingTimeTimer.Interval = 1000;
            _printRemaingTimeTimer.Tick += PrintRemaingTime;
            _printRemaingTimeTimer.Start();
        }

        private void PrintRemaingTime(object o, EventArgs e)
        {
			RemaingTimeDisplay.Text = _lockTimer.GetRemainingTime().ToString(@"hh\:mm\:ss");
        }

        private void TrayIconClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void MainLockerFormClosing(object sender, FormClosingEventArgs e)
        {
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				this.WindowState = FormWindowState.Minimized;
			}
        }
    }
}
