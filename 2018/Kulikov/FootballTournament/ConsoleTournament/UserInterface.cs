using System;
using System.Collections.Generic;
using TournamentLibrary;

namespace ConsoleTournament
{
    public class UserInterface
    {
        private Tournament _tournament;
        private TournamentGridType _tournamentGridType;
        private TournamentGridDrawer _tournamentGridDrawer = new TournamentGridDrawer();
        private DoubleSidedTournamentGridDrawer _doubleSidedGridDrawer = new DoubleSidedTournamentGridDrawer();

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
            _tournament = new SingleEliminationTournament();
            InputData();
        }

        private void SelectDoubleElimination()
        {
            _tournament = new DoubleEliminationTournament();
            InputData();
        }

        private void InputData()
        {
            _tournament.StartTournament();

            var tournamentMenuList = new List<MenuItem>
            {
                new MenuItem(PlayRound, " Play next round"),
                new MenuItem(ShowGrid, " Show tournament grid"),
            };

            if (_tournament is SingleEliminationTournament)
            {
                tournamentMenuList.Add(new MenuItem(SelectOneSidedGrid, " Select one-sided type of grid"));
                tournamentMenuList.Add(new MenuItem(SelectDoubleSidedGrid, " Select double-sided type of grid"));
                _tournamentGridType = TournamentGridType.OneSided;
            }

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
            if (_tournament is SingleEliminationTournament)
            {
                if (_tournamentGridType == TournamentGridType.OneSided)
                    _tournamentGridDrawer.ShowSingleEliminationGrid(_tournament);
                else
                    _doubleSidedGridDrawer.Show(_tournament as SingleEliminationTournament);
            }
            else
                _tournamentGridDrawer.ShowDoubleEliminationGrid(_tournament as DoubleEliminationTournament);

            Console.ReadKey();
        }

        private void SelectOneSidedGrid()
        {
            _tournamentGridType = TournamentGridType.OneSided;
        }

        private void SelectDoubleSidedGrid()
        {
            _tournamentGridType = TournamentGridType.DoubleSided;
        }

        private void LoadTournament()
        {
            _tournament = SaveLoadSystem.Load();

            var tournamentMenuList = new List<MenuItem>
            {
                new MenuItem(PlayRound, " Play next round"),
                new MenuItem(ShowGrid, " Show tournament grid"),
            };

            if (_tournament is SingleEliminationTournament)
            {
                tournamentMenuList.Add(new MenuItem(SelectOneSidedGrid, " Select one-sided type of grid"));
                tournamentMenuList.Add(new MenuItem(SelectDoubleSidedGrid, " Select double-sided type of grid"));
                _tournamentGridType = TournamentGridType.OneSided;
            }

            var tournamentMenu = new Menu(tournamentMenuList, "Playing tournament ...");
            tournamentMenu.Start();
        }
    }
}