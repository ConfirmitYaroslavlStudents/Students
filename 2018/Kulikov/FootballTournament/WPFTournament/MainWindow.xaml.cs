using System;
using System.Windows;
using TournamentLibrary;

namespace WPFTournament
{
    public partial class MainWindow : Window
    {
        private Tournament _tournament;
        private WPFManager _printer;
        private TournamentData _tournamentData = new TournamentData();

        public MainWindow()
        {
            InitializeComponent();

            _printer = new WPFManager(this, _tournamentData);
        }

        private void MI_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MI_Load_Click(object sender, RoutedEventArgs e)
        {
            _tournament = SaveLoadSystem.Load();
        }

        private void MI_Start_Click(object sender, RoutedEventArgs e)
        {
            SP_StartTournament.Visibility = Visibility.Visible;

            _printer.StartedNewTournament();
        }

        private void Btn_StartSingleElimination_Click(object sender, RoutedEventArgs e)
        {
            StartTournament();

            _tournament = new SingleEliminationTournament(_printer);
        }

        private void Btn_StartDoubleElimination_Click(object sender, RoutedEventArgs e)
        {
            StartTournament();

            _tournament = new DoubleEliminationTournament(_printer);
        }

        private void StartTournament()
        {
            SP_SelectMode.Visibility = Visibility.Collapsed;
            SP_EnterCountOfPlayers.Visibility = Visibility.Visible;

            _printer.EnterCountOfPlayers();
        }

        private void Btn_NextEnterCountOfPlayers_Click(object sender, RoutedEventArgs e)
        {
            var count = -1;

            if (int.TryParse(TB_CountOfPlayers.Text, out count) && count > 0)
            {
                SP_EnterCountOfPlayers.Visibility = Visibility.Collapsed;
                SP_AddPlayers.Visibility = Visibility.Visible;

                _tournamentData.GetCountOfPlayers(int.Parse(TB_CountOfPlayers.Text));
                _printer.EnterPlayerNames();
            }
        }

        private void Btn_AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            var name = TB_PlayerName.Text;

            if (!_tournamentData.IsPlayerExists(name))
                _tournamentData.AddPlayer(name);
            else
                _printer.NameAlreadyExists();

            TB_PlayerName.Clear();

            if (_tournamentData.IsAdditionOver())
            {
                SP_AddPlayers.Visibility = Visibility.Collapsed;
                Grid_PlayGame.Visibility = Visibility.Visible;

                _tournament.StartTournament();

                Label_Status.Content = "Playing tournament...";
            }
        }
    }
}
