using System;
using System.Windows;
using System.Windows.Controls;

namespace TournamentUI
{
    public partial class WinnerDetection : Window
    {
        Label Message = new Label();
        Button FirstPlayer = new Button();
        Button SecondPlayer = new Button();

        public WinnerDetection(string title, string body, string firstPlayer, string secondPlayer)
        {
            InitializeComponent();

            Title = title;

            FirstPlayer.Width = 70;
            FirstPlayer.Content = firstPlayer;
            FirstPlayer.VerticalAlignment = VerticalAlignment.Bottom;
            FirstPlayer.HorizontalAlignment = HorizontalAlignment.Left;

            SecondPlayer.Width = 70;
            SecondPlayer.Content = secondPlayer;
            SecondPlayer.VerticalAlignment = VerticalAlignment.Bottom;
            SecondPlayer.HorizontalAlignment = HorizontalAlignment.Right;
            

            Message.Content = body;
            Message.VerticalAlignment = VerticalAlignment.Top;
            Message.HorizontalAlignment = HorizontalAlignment.Center;

            WinnerDetectionGrid.Children.Add(FirstPlayer);
            WinnerDetectionGrid.Children.Add(SecondPlayer);
            WinnerDetectionGrid.Children.Add(Message);

            FirstPlayer.Click += FirstPlayer_Click;
            SecondPlayer.Click += SecondPlayer_Click;
        }

        private void FirstPlayer_Click(object sender, EventArgs e)
        {
            DialogResult = true;
            Close();            
        }

        private void SecondPlayer_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
