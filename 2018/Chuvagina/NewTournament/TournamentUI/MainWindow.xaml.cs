using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tournament;
using static Tournament.SingleEliminationTournament;

namespace TournamentUI
{
    public partial class MainWindow : Window
    {
        private SingleEliminationTournament _tournament;
       
        public MainWindow()
        {
            InitializeComponent();
        }

        public Side ReturnWinner(Participant meeting)
        {
           
            var detection = new WinnerDetection("Winner detection", "Chose winner", meeting.Left.Name, meeting.Right.Name);
            var result = detection.ShowDialog();
            if (result==true)
                return Side.Left;

            return Side.Right;
        }

        private void EnterParticipantName_Click(object sender, RoutedEventArgs e)
        {
            var newParticipant = ParticipantName.Text;

            if (newParticipant!="" && PartisipantsList.Items.IndexOf(newParticipant) < 0)
                PartisipantsList.Items.Add(ParticipantName.Text); ;
     
            ParticipantName.Clear();
        }


        private void Start_Click(object sender, RoutedEventArgs e)
        {
            var participantsList = new List<string>();
            foreach (var item in PartisipantsList.Items)
                participantsList.Add(item.ToString());

            if (SingleElimination.IsChecked == true)

            {
                _tournament = new SingleEliminationTournament(participantsList);
                Tournament.KeyUp += SingleEliminationTournament_KeyUp;
            }
            else
            {
                _tournament = new DoubleEliminationTournament(participantsList);
                Tournament.KeyUp += DoubleEliminationTournament_KeyUp;
            }

            HideElements();

            List<Participant> bracket = _tournament.GetBracket();
            BracketDrawing.DrawSingleElimination(_tournament, UpperBracketCanvas);
        }

        private void HideElements()
        {
            DoubleElimination.Visibility = Visibility.Hidden;
            SingleElimination.Visibility = Visibility.Hidden;
            ParticipantName.Visibility = Visibility.Hidden;
            EnterParticipantName.Visibility = Visibility.Hidden;
            Start.Visibility = Visibility.Hidden;
            PartisipantsList.Visibility = Visibility.Hidden;
            Loading.Visibility = Visibility.Hidden;
            Brackets.Visibility = Visibility.Visible;
            Instructions.Visibility = Visibility.Visible;

        }


        private void DoubleEliminationTournament_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space) return;

            var doubleEliminationTournament = _tournament as DoubleEliminationTournament;
            if (_tournament.EndOfTheGame()) return;

            var meeting = doubleEliminationTournament.GetPlayingParticipants();
            var side = ReturnWinner(meeting);
            doubleEliminationTournament.PlayGame(side);

            BracketDrawing.DrawDoubleElimination(doubleEliminationTournament, UpperBracketCanvas, LowerBracketCanvas);
        }

        private void SingleEliminationTournament_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space) return;

            if (_tournament.EndOfTheGame()) return;

            var meeting = _tournament.GetPlayingParticipants();
            var side = ReturnWinner(meeting);
            _tournament.PlayGame(side);

            BracketDrawing.DrawSingleElimination(_tournament, UpperBracketCanvas);
            
        }

        

        private void Loading_Click(object sender, RoutedEventArgs e)
        {
             _tournament = BinarySaver.LoadSingleFromBinnary();

            if (_tournament != null)
            {
                BracketDrawing.DrawSingleElimination(_tournament, UpperBracketCanvas);
                Tournament.KeyUp += SingleEliminationTournament_KeyUp;

                HideElements();
                return;
            }
          

            _tournament = BinarySaver.LoadDoubleFromBinnary();

            if (_tournament != null)
            {
                var doubleElimination = _tournament as DoubleEliminationTournament;
                BracketDrawing.DrawDoubleElimination(doubleElimination, UpperBracketCanvas, LowerBracketCanvas);
                Tournament.KeyUp += DoubleEliminationTournament_KeyUp;

                HideElements();
                return;
            }

            MessageBox.Show("There is no saved tournament");
            return;
        }
    }
}
