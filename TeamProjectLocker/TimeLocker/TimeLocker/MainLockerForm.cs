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
        Timer printRemaingTimeTimer;
        Locker locker;
        public MainLockerForm()
        {
            InitializeComponent();

            trayIcon.Icon = new System.Drawing.Icon("App.ico");
            locker = Locker.GetInstance();

            printRemaingTimeTimer = new Timer();
            printRemaingTimeTimer.Interval = 1000;
            printRemaingTimeTimer.Tick += printRemaingTime;
            printRemaingTimeTimer.Start();
        }

        private void printRemaingTime(object o, EventArgs e)
        {
            if (locker.GetRemainingTime() < locker.MaxAllowedTime)
                RemaingTimeDisplay.Text = locker.GetRemainingTime().ToString(@"hh\:mm\:ss");
            else
                RemaingTimeDisplay.Text = (locker.GetRemainingTime() - locker.MaxAllowedTime).ToString(@"hh\:mm\:ss");
        }

        private void trayIcon_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void MainLockerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
    }
}
