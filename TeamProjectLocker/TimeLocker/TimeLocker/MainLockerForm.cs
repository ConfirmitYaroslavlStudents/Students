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
            locker = new Locker();

            printRemaingTimeTimer = new Timer();
            printRemaingTimeTimer.Interval = 1000;
            printRemaingTimeTimer.Tick += printRemaingTime;
            printRemaingTimeTimer.Start();
        }

        private void printRemaingTime(object o, EventArgs e)
        {
            RemaingTimeDisplay.Text = ConvertTimeInSecondsToReadebleTimeString(locker.GetRemainingTime());
        }

        private string ConvertTimeInSecondsToReadebleTimeString(int seconds)
        {
            const int SECONDS_IN_HOUR = 3600;
            const int SECONDS_IN_MINUTE = 60;

            var result = (seconds / SECONDS_IN_HOUR).ToString() + ":";
            seconds = seconds % SECONDS_IN_HOUR;
            result += (seconds / SECONDS_IN_MINUTE).ToString("00") + ":";
            seconds = seconds % SECONDS_IN_MINUTE;
            result += seconds.ToString("00");

            return result;
        }
    }
}
