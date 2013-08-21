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

            locker = LockerLoader.GetLocker();

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
    }
}
