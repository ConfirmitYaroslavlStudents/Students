using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Tournament;
using System.Windows.Documents;

namespace TournamentUI
{
    public partial class MainWindow : Window
    {
        private List<string> _participantNames=new List<string>();
        private SingleEliminationTournament _tournament;

        public MainWindow()
        {
            InitializeComponent();
        }

        public string returnWinner(string first, string second)
        {    
            WinnerDetection detection = new WinnerDetection("Winner detection", "Chose winner",first,second);
            var result = detection.ShowDialog();
            if (result==System.Windows.Forms.DialogResult.Yes)
                return first;

            return second;
        }

        private void EnterParticipantName_Click(object sender, RoutedEventArgs e)
        {
            _participantNames.Add(ParticipantName.Text);
            ParticipantName.Clear();
        }

        private void AddNode(ref int rowIndex, int colIndex, Participant node)
        {
            if (node.Left != null)
                AddNode(ref rowIndex, colIndex-1, node.Left);

            var participant = new TextBlock(new Run(node.Name));

            if (node.Winner?.Name == node.Name)
            {
                participant.Foreground = System.Windows.Media.Brushes.Green;
                participant.FontWeight = FontWeights.Bold;
            }

            Canvas.SetTop(participant, rowIndex * 30);
            Canvas.SetLeft(participant, 50* colIndex);
            UpperBracketCanvas.Children.Add(participant);
            rowIndex++;


            if (node.Right != null)
                AddNode(ref rowIndex, colIndex-1, node.Right);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _tournament = new SingleEliminationTournament(_participantNames);
            PlayGame.IsEnabled = true;
            DoubleElimination.Visibility = Visibility.Hidden;
            SingleElimination.Visibility = Visibility.Hidden;
            ParticipantName.Visibility = Visibility.Hidden;
            EnterParticipantName.Visibility = Visibility.Hidden;
            Start.Visibility = Visibility.Hidden;           
        }

        private int MaxDepth(Participant participant)
        {
            if (participant == null) return 0;

            if (MaxDepth(participant.Left) > MaxDepth(participant.Right))
                return MaxDepth(participant.Left) + 1;

            return MaxDepth(participant.Right) + 1;
        }

        private void PlayGame_Click(object sender, RoutedEventArgs e)
        {
            if (!_tournament.EndOfTheGame())
            {
                _tournament.PlayGame(returnWinner);
                var bracket = _tournament.GetBracket();
                int i = 0;
                UpperBracketCanvas.Children.Clear();
                int maxDepth = 0;
                foreach (var participant in bracket)
                    if (MaxDepth(participant) > maxDepth)
                        maxDepth = MaxDepth(participant);

                foreach (var participant in bracket)
                    AddNode(ref i, maxDepth, participant);
            }
            
        }
    }
}
