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
        public MainLockerForm()
        {
            InitializeComponent();
            Locker locker = new Locker();
        }
    }
}
