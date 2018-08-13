using System;
using System.Drawing;
using System.Windows.Forms;

namespace TournamentUI
{
    public partial class WinnerDetection : Form
    {
        Label message = new Label();
        Button FirstPlayer = new Button();
        Button SecondPlayer = new Button();

        public WinnerDetection(string title, string body, string firstPlayer, string secondPlayer)
        {
            Text = title;

            FirstPlayer.Location = new Point(70, 75);
            FirstPlayer.Size = new Size(75, 23);
            FirstPlayer.Text = firstPlayer;
            FirstPlayer.BackColor = DefaultBackColor;

            SecondPlayer.Location = new Point(155, 75);
            SecondPlayer.Size = new Size(75, 23);
            SecondPlayer.Text = secondPlayer;
            SecondPlayer.BackColor = DefaultBackColor;

            message.Location = new Point(10, 10);
            message.Text = body;
            message.Font = DefaultFont;
            message.AutoSize = true;

            BackColor = Color.White;

            Controls.Add(FirstPlayer);
            Controls.Add(SecondPlayer);
            Controls.Add(message);

            FirstPlayer.Click += Button_Click;
            FirstPlayer.DialogResult = DialogResult.Yes;
            SecondPlayer.Click += Button_Click;
            SecondPlayer.DialogResult = DialogResult.No;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
