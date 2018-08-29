using System;
using System.Windows;
using TournamentLibrary;

namespace WPFTournament
{
    public partial class MainWindow : Window
    {
        private Tournament _tournament;
        private WPFManager _printer;
        private TournamentData _tournamentData;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MI_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MI_Load_Click(object sender, RoutedEventArgs e)
        {
            _tournamentData = new TournamentData();
            _printer = new WPFManager(this, _tournamentData);
            _tournament = SaveLoadSystem.Load(_printer);

            Grid_GameMenu.Visibility = Visibility.Visible;
            SP_StartTournament.Visibility = Visibility.Collapsed;
            LB_Players.Items.Clear();
            LB_Results.Items.Clear();

            var firstStage = _tournament.WinnersGrid[0];

            for (int i = 0; i < firstStage.Count; i++)
            {
                LB_Players.Items.Add(firstStage[i].FirstPlayer.Name);

                if (firstStage[i].SecondPlayer != null)
                    LB_Players.Items.Add(firstStage[i].SecondPlayer.Name);
            }
        }

        private void MI_Start_Click(object sender, RoutedEventArgs e)
        {
            SP_StartTournament.Visibility = Visibility.Visible;
            SP_SelectMode.Visibility = Visibility.Visible;
            Grid_GameMenu.Visibility = Visibility.Collapsed;

            Rtb_Info.Clear();
            LB_Players.Items.Clear();
            TB_CountOfPlayers.Clear();
            TB_PlayerName.Clear();
            LB_Results.Items.Clear();

            _tournamentData = new TournamentData();
            _tournament = null;
            _printer = new WPFManager(this, _tournamentData);

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
                _tournament.ResponseData(new Foo()
                {
                    Message = TB_CountOfPlayers.Text,
                    RequestedData = RequestedData.PlayersCount
                });
                _printer.EnterPlayerNames();
                _printer.EnterPlayerName(_tournamentData.IndexOfCurrentPlayer);
            }
        }

        private void Btn_AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            var name = TB_PlayerName.Text;

            if (name.Length > 12)
            {
                Rtb_Info.AppendText("\nThis name is too long. Try again!");
                return;
            }

            if (!_tournamentData.IsPlayerExists(name))
            {
                _tournamentData.AddPlayer(name);
                _tournamentData.IndexOfCurrentPlayer++;
                LB_Players.Items.Add(name);
            }
            else
                _printer.NameAlreadyExists();

            TB_PlayerName.Clear();

            if (_tournamentData.IsAdditionOver())
            {
                SP_AddPlayers.Visibility = Visibility.Collapsed;
                SP_StartTournament.Visibility = Visibility.Collapsed;
                Grid_GameMenu.Visibility = Visibility.Visible;

                _tournament.StartTournament();
            }
            else
                _printer.EnterPlayerName(_tournamentData.IndexOfCurrentPlayer);
        }

        private void Rtb_Info_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            Rtb_Info.SelectionStart = Rtb_Info.Text.Length;
            Rtb_Info.ScrollToEnd();
        }

        private void Btn_PlayNextRound_Click(object sender, RoutedEventArgs e)
        {
            if (!_tournament.IsFinished)
            {
                Grid_PlayGame.Visibility = Visibility.Visible;
                Btn_PlayNextRound.Visibility = Visibility.Collapsed;

                _tournamentData.GetGamesToPlay(_tournament);
                var game = _tournamentData.GamesToPlay[0];
                var firstPlayer = game.FirstPlayer;
                var secondPlayer = game.SecondPlayer;

                _printer.EnterPlayerScore(firstPlayer);
                _printer.EnterPlayerScore(secondPlayer);
            }
            else
                _printer.PrintChampion(_tournament.Champion);
        }

        private void Btn_PlayGame_Click(object sender, RoutedEventArgs e)
        {
            var firstPlayerScore = int.Parse(TB_FirstPlayerScore.Text);
            var secondPlayerScore = int.Parse(TB_SecondPlayerScore.Text);

            if (firstPlayerScore != secondPlayerScore)
            {
                _tournamentData.SetGameScore(firstPlayerScore, secondPlayerScore);

                if (!_tournamentData.IsStagePlayed())
                {
                    var game = _tournamentData.GamesToPlay[_tournamentData.PlayedOnCurrentStage];
                    var firstPlayer = game.FirstPlayer;
                    var secondPlayer = game.SecondPlayer;

                    _printer.EnterPlayerScore(firstPlayer);
                    _printer.EnterPlayerScore(secondPlayer);

                    TB_FirstPlayerScore.Clear();
                    TB_SecondPlayerScore.Clear();
                }
                else
                {
                    Grid_PlayGame.Visibility = Visibility.Collapsed;
                    Btn_PlayNextRound.Visibility = Visibility.Visible;
                    TB_FirstPlayerScore.Clear();
                    TB_SecondPlayerScore.Clear();

                    _tournament.PlayNextRound();
                    LB_Results.Items.Add("");
                }

                Label_Draw.Visibility = Visibility.Collapsed;
            }
            else
            {
                _printer.DrawIsNotPossible();
                TB_FirstPlayerScore.Clear();
                TB_SecondPlayerScore.Clear();
            }
        }
    }
}
