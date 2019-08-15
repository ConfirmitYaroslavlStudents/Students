using System;
using System.Collections.Generic;
using ChampionshipLibrary;
using GridRendererLibrary;
using MenuLibrary;

namespace UserInterfaceLibrary
{
    public class UserInterface
    {
        private Menu _mainMenu;
        private IChampionship _championship;
        private GridRendererVisitor _gridRenderer = new GridRendererVisitor();
        private DataInput dataInput = new DataInput(new ConsoleWorker());

        public UserInterface()
        {
            var mainMenu = new List<MenuItem>
            {
                new MenuItem(InitSingleTournament, "Start single elimination championship"),
                new MenuItem(InitDoubleTournament, "Start double elimination championship"),
                new MenuItem(InitChampionship, "Download last championship"),
            };

            _mainMenu = new Menu(mainMenu, "Welcome");
        }

        public void Start()
        {
            _mainMenu.Start();
        }

        private void InitSingleTournament()
        {
            _championship = new SingleChampionshipManager(dataInput);
            StartTournament();
        }

        private void InitDoubleTournament()
        {
            _championship = new DoubleChampionshipManager(dataInput);
            StartTournament();
        }

        private void InitChampionship()
        {
            _championship = SaveLoadSystem.Load();
            StartTournament();
        }

        private void StartTournament()
        {
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
            if (_championship.IsTeamInput)
                ShowWarning(Messages.TeamsInputAlready);
            else
            {
                _championship.InputTeams();
            }
        }

        private void PlayTour()
        {
            if (!_championship.IsTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            if (_championship.Champion != null)
            {
                ShowWarning(Messages.ChampionshipIsEnd);
                return;
            }

            _championship.PlayTour();
        }

        private void DrawGrid()
        {
            if (!_championship.IsTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            _championship.Accept(_gridRenderer);
            Console.ReadKey();
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
            _gridRenderer.GridType = GridRendererType.StandardGrid;
            Console.WriteLine(Messages.SelectStandartGridType);
            Console.ReadKey();
        }

        private void SetDoubleTypeOfGrid()
        {
            _gridRenderer.GridType = GridRendererType.DoubleGrid;
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
