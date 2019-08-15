using System;
using System.Windows;
using System.Windows.Controls;

namespace TournamentUI
{
    public partial class WinnerDetection : Window
    {
        private bool _resultIsSet = false;

        public WinnerDetection(string title, string body, string firstPlayer, string secondPlayer)
        {
            InitializeComponent();
            Title = title;

            var firstBlock = new TextBlock()
            {
                Text = firstPlayer,
                TextWrapping = TextWrapping.Wrap
            };

            var FirstPlayer = new Button()
            {
                Width = 70,
                Height = 40,
                Content = firstBlock,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Left,
                ToolTip = firstPlayer
            };

            var secondBlock = new TextBlock()
            {
                Text = secondPlayer,
                TextWrapping = TextWrapping.Wrap
            }; 

            var SecondPlayer = new Button()
            {
                Width = 70,
                Height = 40,
                Content = secondBlock,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Right,
                ToolTip = secondPlayer
            };

            var Message = new TextBlock()
            {
                Text = body,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            WinnerDetectionGrid.Children.Add(FirstPlayer);
            WinnerDetectionGrid.Children.Add(SecondPlayer);
            WinnerDetectionGrid.Children.Add(Message);

            FirstPlayer.Click += FirstPlayer_Click;
            SecondPlayer.Click += SecondPlayer_Click;
        }

        private void FirstPlayer_Click(object sender, EventArgs e)
        {
            _resultIsSet = true;
            DialogResult = true;
            Close();            
        }

        private void SecondPlayer_Click(object sender, EventArgs e)
        {
            _resultIsSet = true;
            DialogResult = false;
            Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!_resultIsSet)
            {
                e.Cancel = true;
            }
        }

    }
}
