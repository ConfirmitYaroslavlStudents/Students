using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTournament
{
    public class UserInterface
    {
        private Tournament _tournament;
        private TournamentMode _tournamentMode;

        public void Init()
        {
            var mainMenuList = new List<MenuItem>
            {
                new MenuItem(StartTournament, " Start new tournament"),
                new MenuItem(LoadTournament, " Load tournament"),
            };

            var mainMenu = new Menu(mainMenuList, "TOURNAMENT MENU");
            mainMenu.Start();
        }

        private void StartTournament()
        {
            var selectModeMenuList = new List<MenuItem>
            {
                new MenuItem(SelectSingleElimination, " Single Elimination"),
                new MenuItem(SelectDoubleElimination, " Double Elimination"),
            };

            var selectModeMenu = new Menu(selectModeMenuList, "Select tournament mode");
            selectModeMenu.Start();
        }

        private void SelectSingleElimination()
        {
            _tournamentMode = TournamentMode.SingleElimination;
            InputData();
        }

        private void SelectDoubleElimination()
        {
            _tournamentMode = TournamentMode.DoubleElimination;
            InputData();
        }

        private void InputData()
        {
            _tournament = new Tournament();
            _tournament.Init(_tournamentMode);

            var tournamentMenuList = new List<MenuItem>
            {
                new MenuItem(PlayRound, " Play next round"),
                new MenuItem(ShowGrid, " Show tournament grid"),
            };

            var tournamentMenu = new Menu(tournamentMenuList, "Playing tournament ...");
            tournamentMenu.Start();
        }

        private void PlayRound()
        {
            _tournament.PlayNextRound();

            Console.ReadKey();
        }

        private void ShowGrid()
        {
            if (_tournamentMode == TournamentMode.SingleElimination)
                TournamentGrid.ShowSingleEliminationGrid(_tournament);
            else
                TournamentGrid.ShowDoubleEliminationGrid(_tournament);

            Console.ReadKey();
        }

        private void LoadTournament()
        {

        }
    }
}
