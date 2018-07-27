using System;
using System.Collections.Generic;

namespace Championship
{
    public class UserInterface
    {
        private Menu _mainMenu;
        private TournamentGrid _tournamentGrid;
        private GridType _gridType = GridType.StandardGrid;
        private bool isTeamInput = false;

        public UserInterface()
        {
            var mainMenu = new List<MenuItem>
            {
                new MenuItem(StartTournament, "Start championship"),
            };
            _mainMenu = new Menu(mainMenu, "Welcome");
            _mainMenu.Start();
        }

        private void StartTournament()
        {
            isTeamInput = false;

            var tournamentMenuItem = new List<MenuItem>
            {
                new MenuItem(InputTeams, "Input teams"),
                new MenuItem(DrawGrid, "Draw grid"),
                new MenuItem(PlayTour, "Play tour"),
                new MenuItem(Options, "Options")
            };

            Menu TournamentMenu = new Menu(tournamentMenuItem, "Championship");
            TournamentMenu.Start();
        }

        private void InputTeams()
        {
            _tournamentGrid = new TournamentGrid(DataInput.InputTeams());
            isTeamInput = true;
        }

        private void DrawGrid()
        {
            if (!isTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            GridDrawer gridDrawer = new GridDrawer(_tournamentGrid.Teams, 0);
            gridDrawer.DrawGrid(_tournamentGrid, _gridType);
            Console.ReadKey();
        }

        private void PlayTour()
        {
            if (!isTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            if (_tournamentGrid.Champion!=null)
            {
                ShowWarning(Messages.ChampionshipIsEnd);
                return;
            }

            for (int i = 0; i < _tournamentGrid.Tours[_tournamentGrid.CountTours].Games.Count; i++)
            {
                DataInput.InputGameScore(_tournamentGrid.Tours[_tournamentGrid.CountTours].Games[i]);
            }
            _tournamentGrid.StartNextTour();
        }

        private void Options()
        {
            var optionsMenuItems = new List<MenuItem>
            {
                new MenuItem(SetGridType, "Grid Type"),
            };

            Menu optionsMenu = new Menu(optionsMenuItems, "Options");
            optionsMenu.Start();
        }

        private void SetGridType()
        {
            var gridTypeMenuItem = new List<MenuItem>
            {
                new MenuItem(SetStandartTypeOfGrid, "Standart"),
                new MenuItem(SetDoubleTypeOfGrid, "Double"),
            };

            Menu gridMenu = new Menu(gridTypeMenuItem, "Grid Options");
            gridMenu.Start();
        }

        private void SetStandartTypeOfGrid()
        {
            _gridType = GridType.StandardGrid;
            Console.WriteLine(Messages.SelectStandartGridType);
            Console.ReadKey();
        }

        private void SetDoubleTypeOfGrid()
        {
            _gridType = GridType.DoubleGrid;
            Console.WriteLine(Messages.SelectDoubleGridType);
            Console.ReadKey();
        }

        private void ShowWarning(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }

    }
}
