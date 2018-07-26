using System;
using System.Collections.Generic;

namespace FootballTournament
{
    public class Tournament
    {
        private List<List<Game>> _winnersGrid = new List<List<Game>>();
        private List<List<Game>> _losersGrid = new List<List<Game>>();
        private int _currentWinnersStage = 0;
        private int _currentLosersStage = -1;
        private int _gamesOnCurrentWinnersStage = 0;
        private int _gamesOnCurrentLosersStage = 0;
        public bool _winnersGridIsFinished = false;
        private string _tournamentMode;

        public void Init()
        {
            _tournamentMode = DataInput.ChooseTournamentMode();
            StartTournament();
        }

        private void StartTournament()
        {
            _winnersGrid.Add(new List<Game>());
            var countOfPlayers = DataInput.GetCountOfPlayers();
            var players = DataInput.GetPlayersList(countOfPlayers);

            for (int i = 0; i < players.Count; i += 2)
            {
                if (i == players.Count - 1)
                    _winnersGrid[_currentWinnersStage].Add(new Game(players[i]));
                else
                    _winnersGrid[_currentWinnersStage].Add(new Game(players[i], players[i + 1]));
                _gamesOnCurrentWinnersStage++;
            }

            TournamentGrid.ShowSingleEliminationGrid(_winnersGrid, _tournamentMode);
            PlayWinnersGridStage();
        }

        private void PlayWinnersGridStage()
        {
            foreach (var game in _winnersGrid[_currentWinnersStage])
                game.Play();

            if (_winnersGrid[_currentWinnersStage].Count == 1)
            {
                Player checkLoser = _winnersGrid[_currentWinnersStage][0].DetectLoser();

                if (checkLoser == null)
                    _winnersGridIsFinished = true;
            }

            if (_tournamentMode == "SE")
            {
                if (!_winnersGridIsFinished)
                {
                    InitNewWinnersGridStage();
                    TournamentGrid.ShowSingleEliminationGrid(_winnersGrid, _tournamentMode);
                    PlayWinnersGridStage();
                }
                else
                {
                    TournamentGrid.ShowSingleEliminationGrid(_winnersGrid, _tournamentMode);
                    DetectChampion();
                }
            }
            else
            {
                InitNewLosersGridStage();
                TournamentGrid.ShowDoubleEliminationGrid(_winnersGrid, _losersGrid);
                PlayLosersGridStage();
            }
        }

        private void InitNewWinnersGridStage()
        {
            _currentWinnersStage++;
            FillNewStage(_winnersGrid, _currentWinnersStage);
            _gamesOnCurrentWinnersStage = _winnersGrid[_currentWinnersStage].Count;
        }

        private void FillNewStage(List<List<Game>> grid, int currentStage)
        {
            var lastStage = currentStage - 1;
            grid.Add(new List<Game>());

            for (int i = 0; i < grid[lastStage].Count; i += 2)
            {
                var firstPlayer = grid[lastStage][i].DetectWinner();

                if (i == grid[lastStage].Count - 1)
                    grid[currentStage].Add(new Game(firstPlayer));
                else
                {
                    var secondPlayer = grid[lastStage][i + 1].DetectWinner();
                    grid[currentStage].Add(new Game(firstPlayer, secondPlayer));
                }
            }
        }

        private void InitNewLosersGridStage()
        {
            _currentLosersStage++;

            if (_currentLosersStage > 0)
            {
                FillNewStage(_losersGrid, _currentLosersStage);
                _gamesOnCurrentLosersStage = _losersGrid[_currentLosersStage].Count;
            }
            else
                _losersGrid.Add(new List<Game>());

            if (!_winnersGridIsFinished)
            {
                for (int i = 0; i < _winnersGrid[_currentWinnersStage].Count; i += 2)
                {
                    var firstPlayer = _winnersGrid[_currentWinnersStage][i].DetectLoser();

                    if (firstPlayer != null)
                    {
                        if (i == _winnersGrid[_currentWinnersStage].Count - 1)
                            _losersGrid[_currentLosersStage].Add(new Game(firstPlayer));
                        else
                        {
                            var secondPlayer = _winnersGrid[_currentWinnersStage][i + 1].DetectLoser();
                            _losersGrid[_currentLosersStage].Add(new Game(firstPlayer, secondPlayer));
                        }
                    }

                    _gamesOnCurrentLosersStage++;
                }
            }
        }

        private void PlayLosersGridStage()
        {
            foreach (var game in _losersGrid[_currentWinnersStage])
                game.Play();

            if (!_winnersGridIsFinished)
            {
                InitNewWinnersGridStage();
                TournamentGrid.ShowDoubleEliminationGrid(_winnersGrid, _losersGrid);
                PlayWinnersGridStage();
            }
            else
            {
                InitNewLosersGridStage();

                if (_gamesOnCurrentLosersStage > 1)
                {

                    TournamentGrid.ShowDoubleEliminationGrid(_winnersGrid, _losersGrid);
                    PlayLosersGridStage();
                }
                else
                {
                    TournamentGrid.ShowDoubleEliminationGrid(_winnersGrid, _losersGrid);
                    DetectChampion();
                }
            }
        }

        private void DetectChampion()
        {
            Player champion;

            if (_tournamentMode == "SE")
                champion = _winnersGrid[_currentWinnersStage][0].DetectWinner();
            else
            {
                var firstPlayer = _winnersGrid[_currentWinnersStage][0].DetectWinner();
                var secondPlayer = _losersGrid[_currentWinnersStage][0].DetectWinner();
                var final = new Game(firstPlayer, secondPlayer);

                Console.WriteLine("GRAND FINAL");
                Console.WriteLine($"{final.ToString()}\n");

                final.Play();
                champion = final.DetectWinner();             
            }

            Console.WriteLine($"\n{champion.Name} is a champion!");
        }
    }
}
