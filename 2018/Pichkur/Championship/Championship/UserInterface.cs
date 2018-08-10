using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Championship
{
    [Serializable]
    public class UserInterface
    {
        private Menu _mainMenu;
        private ChampionshipData _data=new ChampionshipData();

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
            _data.PlayTour = PlaySingleTour;
            _data.InputTeams = InputTeamsForSingleTournament;
            _data.DrawGrid = DrawSingleGrid;
            _data.isTeamInput = false;
            StartTournament();
        }

        private void InitDoubleTournament()
        {
            _data.PlayTour = PlayDoubleTour;
            _data.InputTeams = InputTeamsForDoubleTournament;
            _data.DrawGrid = DrawDoubleGrid;
            _data.isTeamInput = false;
            StartTournament();
        }

        private void InitChampionship()
        {
            if (Download())
                StartTournament();
            else
                ShowWarning(Messages.DownloadError);
        }

        private void StartTournament()
        {
            var tournamentMenuItem = new List<MenuItem>
            {
                new MenuItem(_data.InputTeams, "Input teams"),
                new MenuItem(_data.DrawGrid, "Draw grid"),
                new MenuItem(_data.PlayTour, "Play tour"),
                new MenuItem(Options, "Options")
            };

            Menu TournamentMenu = new Menu(tournamentMenuItem, "Championship");
            TournamentMenu.Start();
        }

        private void InputTeamsForSingleTournament()
        {
            if (_data.isTeamInput)
                ShowWarning(Messages.TeamsInputAlready);
            else
            {
                _data._singleTournamentGrid = new SingleEliminationGrid(DataInput.InputTeams());
                _data.isTeamInput = true;
                Save();
            }
        }

        private void InputTeamsForDoubleTournament()
        {
            if (_data.isTeamInput)
                ShowWarning(Messages.TeamsInputAlready);
            else
            {
                _data._doubleTournamentGrid = new DoubleEliminationGrid(DataInput.InputTeams());
                _data.isTeamInput = true;
                Save();
            }
        }

        public void InputTourGamesScore(Tour tour)
        {
            foreach (var game in tour.Games)
            {
                DataInput.InputGameScore(game);
                Save();
            }
        }

        private void PlaySingleTour()
        {
            if (!_data.isTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            if (_data._singleTournamentGrid.Champion != null)
            {
                ShowWarning(Messages.ChampionshipIsEnd);
                ShowWarning(Messages.ShowChampion(_data._singleTournamentGrid.Champion));
                return;
            }

            InputTourGamesScore(_data._singleTournamentGrid.Tours[_data._singleTournamentGrid.CountTours]);
            _data._singleTournamentGrid.StartNextTour();
            Save();
        }

        private void PlayDoubleTour()
        {
            if (!_data.isTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            if (_data._doubleTournamentGrid.WinnersTour.Champion != null
                && _data._doubleTournamentGrid.LosersTour.Champion != null)
            {
                if (_data._doubleTournamentGrid.FinalGame == null)
                {
                    _data._doubleTournamentGrid.SetFinalGame();
                    DataInput.InputGameScore(_data._doubleTournamentGrid.FinalGame);
                }
                else
                {
                    _data._doubleTournamentGrid.FinalGame.SetWinner();
                    ShowWarning(Messages.ChampionshipIsEnd);
                    ShowWarning(Messages.ShowChampion(_data._doubleTournamentGrid.FinalGame.Winner));
                    return;
                }
            }


            InputTourGamesScore(_data._doubleTournamentGrid.WinnersTour.Tours[_data._doubleTournamentGrid.WinnersTour.CountTours]);
            Save();

            if (_data._doubleTournamentGrid.LosersTour != null)
                InputTourGamesScore(_data._doubleTournamentGrid.LosersTour.Tours[_data._doubleTournamentGrid.LosersTour.CountTours]);

            _data._doubleTournamentGrid.StartNextTour();
            Save();
        }

        private void DrawSingleGrid()
        {
            if (!_data.isTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            GridDrawer gridDrawer = new GridDrawer(_data._singleTournamentGrid.Teams, 0);
            gridDrawer.DrawGrid(_data._singleTournamentGrid, _data._gridType);
            Console.ReadKey();
        }
        
        private void DrawDoubleGrid()
        {
            if (!_data.isTeamInput)
            {
                ShowWarning(Messages.NotAInputTeams);
                return;
            }

            GridDrawer gridDrawer = new GridDrawer(_data._doubleTournamentGrid.WinnersTour.Teams, 0);
            gridDrawer.DrawGrid(_data._doubleTournamentGrid.WinnersTour, _data._gridType);

            if (_data._doubleTournamentGrid.LosersTour!=null)
            {
                GridDrawer gridDrawer1 = new GridDrawer(_data._doubleTournamentGrid.LosersTour.Teams, gridDrawer._maxCursorTop+1);
                gridDrawer1.DrawGrid(_data._doubleTournamentGrid.LosersTour, _data._gridType);
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
            _data._gridType = GridType.StandardGrid;
            Console.WriteLine(Messages.SelectStandartGridType);
            Console.ReadKey();
        }

        private void SetDoubleTypeOfGrid()
        {
            _data._gridType = GridType.DoubleGrid;
            Console.WriteLine(Messages.SelectDoubleGridType);
            Console.ReadKey();
        }

        private void ShowWarning(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }

        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("Championship.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, _data);
            }
        }

        public bool Download()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            bool isSuccessful = true;
            using (FileStream fs = new FileStream("Championship.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    _data = (ChampionshipData)formatter.Deserialize(fs);
                }
                catch
                {
                    isSuccessful = false;
                }
            }
            return isSuccessful;

        }
    }
}
