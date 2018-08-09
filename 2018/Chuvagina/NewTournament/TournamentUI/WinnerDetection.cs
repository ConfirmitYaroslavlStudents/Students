using System;
using System.Drawing;
using System.Windows.Forms;

namespace TournamentUI
{
    public partial class WinnerDetection : Form
    {
        Label message = new Label();
        Button b1 = new Button();
        Button b2 = new Button();

        public WinnerDetection()
        {

        }

        public WinnerDetection(string title, string body, string button1, string button2)
        {
            ClientSize = new Size(490, 150);
            Text = title;

            b1.Location = new Point(311, 112);
            b1.Size = new Size(75, 23);
            b1.Text = button1;
            b1.BackColor = DefaultBackColor;

            b2.Location = new Point(411, 112);
            b2.Size = new Size(75, 23);
            b2.Text = button2;
            b2.BackColor = DefaultBackColor;

            message.Location = new Point(10, 10);
            message.Text = body;
            message.Font = DefaultFont;
            message.AutoSize = true;

            BackColor = Color.White;
            ShowIcon = false;

            Controls.Add(b1);
            Controls.Add(b2);
            Controls.Add(message);

            b1.Click += Button_Click;
            b1.DialogResult = DialogResult.Yes;
            b2.Click += Button_Click;
            b2.DialogResult = DialogResult.No;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
