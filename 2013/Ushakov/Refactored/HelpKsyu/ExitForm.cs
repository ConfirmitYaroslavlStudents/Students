using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelpKsyu
{
	public partial class ExitForm : Form
	{
		public ExitForm()
		{
			InitializeComponent();
		}

		private void yesBtn_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void noBtn_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
