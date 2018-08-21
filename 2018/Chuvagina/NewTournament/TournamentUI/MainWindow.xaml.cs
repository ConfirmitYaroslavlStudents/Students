using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tournament;

namespace TournamentUI
{
    public partial class MainWindow : Window
    {
        private SingleEliminationTournament _tournament;
       
        public MainWindow()
        {
            InitializeComponent();
        }

        public string ReturnWinner(string first, string second)
        {
           
            var detection = new WinnerDetection("Winner detection", "Chose winner",first,second);
            var result = detection.ShowDialog();
            if (result==true)
                return first;

            return second;
        }

        private void EnterParticipantName_Click(object sender, RoutedEventArgs e)
        {
            var newParticipant = ParticipantName.Text;

            if (newParticipant!="" && PartisipantsList.Items.IndexOf(newParticipant) < 0)
                PartisipantsList.Items.Add(ParticipantName.Text); ;
     
            ParticipantName.Clear();
        }

        private void AddNode(ref int rowIndex, int colIndex, UiParticipant node, Canvas canvas)
        {
            if (node.Left != null)
                AddNode(ref rowIndex, colIndex - 1, node.Left, canvas);

            BracketDrawing.AddParticipantToCanvas(rowIndex, colIndex, node, canvas);
            rowIndex++;

            if (node.Right != null)
                AddNode(ref rowIndex, colIndex - 1, node.Right, canvas);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            var participantsList= new List<string>();
            foreach (var item in PartisipantsList.Items)
                participantsList.Add(item.ToString());

            if (SingleElimination.IsChecked == true)
                _tournament = new SingleEliminationTournament(participantsList);
            else
                _tournament = new DoubleEliminationTournament(participantsList);

            DoubleElimination.Visibility = Visibility.Hidden;
            SingleElimination.Visibility = Visibility.Hidden;
            ParticipantName.Visibility = Visibility.Hidden;
            EnterParticipantName.Visibility = Visibility.Hidden;
            Start.Visibility = Visibility.Hidden;
            PartisipantsList.Visibility = Visibility.Hidden;
            Brackets.Visibility = Visibility.Visible;
            Instructions.Visibility = Visibility.Visible;
            Tournament.KeyUp += Tournament_KeyUp;
        }

        private static int MaxDepth(Participant participant)
        {
            if (participant == null) return 0;

            if (MaxDepth(participant.Left) > MaxDepth(participant.Right))
                return MaxDepth(participant.Left) + 1;

            return MaxDepth(participant.Right) + 1;
        }

        private void Tournament_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != Key.Space) return;

            if (_tournament.EndOfTheGame()) return;

            if (_tournament is DoubleEliminationTournament tmp1)
                tmp1.PlayGame(ReturnWinner);
            else
                _tournament.PlayGame(ReturnWinner);

            List<Participant> bracket = _tournament.GetBracket();
            int i = 0;
            UpperBracketCanvas.Children.Clear();
            int maxDepth = 0;

            foreach (var participant in bracket)
                if (MaxDepth(participant) > maxDepth)
                    maxDepth = MaxDepth(participant);

            foreach (var participant in bracket)
            {
                UiParticipant newParticipant = null;
                newParticipant = CloneParticipant.Clone(participant);
                AddNode(ref i, maxDepth, newParticipant, UpperBracketCanvas);
            }

            i = 0;
            LowerBracketCanvas.Children.Clear();
            if (!(_tournament is DoubleEliminationTournament tmp)) return;
            {
                bracket = tmp.GetLowerBracket();
                foreach (var participant in bracket)
                {
                    UiParticipant newParticipant = null;
                    newParticipant = CloneParticipant.Clone(participant);
                    AddNode(ref i, maxDepth, newParticipant, LowerBracketCanvas);
                }

            }

        }
    }
}
