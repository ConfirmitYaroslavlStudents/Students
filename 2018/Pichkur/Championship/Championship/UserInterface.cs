using System;
using System.Collections.Generic;

namespace Championship
{
    public class UserInterface
    {
        private Menu _mainMenu;
        private TournamentGrid _tournamentGrid;
        private DoubleGrid _doubleTournamentGrid;
        private GridType _gridType = GridType.StandardGrid;
        private bool isTeamInput = false;
        private Action PlayTour;
        private Action InputTeams;
        private Action DrawGrid;

        public UserInterface()
        {
            var mainMenu = new List<MenuItem>
            {
                new MenuItem(InitSimpleTournament, "Start sinple championship"),
                new MenuItem(InitDoubleTournament, "Start double championship"),
            };
            _mainMenu = new Menu(mainMenu, "Welcome");
           
        }

        public void Start()
        {
            _mainMenu.Start();
        }

        private void InitSimpleTournament()
        {
            PlayTour = PlayTours;
            InputTeams = InputTeamss;
            DrawGrid = DrawSimpleGrid;
            StartTournament();
        }

        private void InitDoubleTournament()
        {
            PlayTour = PlayTours1;
            InputTeams = InputTeamss1;
            DrawGrid = DrawDoubleGrid;
            StartTournament();
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

        private void InputTeamss()
        {
             _tournamentGrid = new TournamentGrid(DataInput.InputTeams());
            isTeamInput = true;
        }

        private void InputTeamss1()
        {
            _doubleTournamentGrid = new DoubleGrid(DataInput.InputTeams());
            isTeamInput = true;
        }


        private void PlayTours()
        {
            if (!isTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            if (_tournamentGrid.Champion != null)
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

        private void PlayTours1()
        {
            if (!isTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            if (_doubleTournamentGrid.WinnersTour.Champion != null&&_doubleTournamentGrid.LoserTour.Champion!=null)
            {
                if (_doubleTournamentGrid.FinalGame == null)
                {
                    _doubleTournamentGrid.SetFinalGame();
                    DataInput.InputGameScore(_doubleTournamentGrid.FinalGame);
                }
                else
                {
                    ShowWarning(Messages.ChampionshipIsEnd);
                    return;
                }
            }

            for (int i = 0; i < _doubleTournamentGrid.WinnersTour.Tours[_doubleTournamentGrid.WinnersTour.CountTours].Games.Count; i++)
            {
                DataInput.InputGameScore(_doubleTournamentGrid.WinnersTour.Tours[_doubleTournamentGrid.WinnersTour.CountTours].Games[i]);
            }

            if (_doubleTournamentGrid.LoserTour != null)
            {
                for (int i = 0; i < _doubleTournamentGrid.LoserTour.Tours[_doubleTournamentGrid.LoserTour.CountTours].Games.Count; i++)
                {
                    DataInput.InputGameScore(_doubleTournamentGrid.LoserTour.Tours[_doubleTournamentGrid.LoserTour.CountTours].Games[i]);
                }
            }
            _doubleTournamentGrid.StartNextTour();
        }

        private void DrawSimpleGrid()
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

        private void DrawDoubleGrid()
        {
            if (!isTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            GridDrawer gridDrawer = new GridDrawer(_doubleTournamentGrid.WinnersTour.Teams, 0);
            gridDrawer.DrawGrid(_doubleTournamentGrid.WinnersTour, _gridType);

            if (_doubleTournamentGrid.LoserTour!=null)
            {
                GridDrawer gridDrawer1 = new GridDrawer(_doubleTournamentGrid.LoserTour.Teams, gridDrawer._maxCursorTop+1);
                gridDrawer1.DrawGrid(_doubleTournamentGrid.LoserTour, _gridType);
            }
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
