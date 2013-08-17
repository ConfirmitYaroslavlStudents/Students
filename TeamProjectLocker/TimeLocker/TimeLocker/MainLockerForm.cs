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
            locker = new Locker(Properties.Settings.Default.maxAllowedTime);

            printRemaingTimeTimer = new Timer();
            printRemaingTimeTimer.Interval = 1000;
            printRemaingTimeTimer.Tick += printRemaingTime;
            printRemaingTimeTimer.Start();
        }

        private void printRemaingTime(object o, EventArgs e)
        {
            RemaingTimeDisplay.Text = locker.GetRemainingTime().ToString();
        }
    }
}
