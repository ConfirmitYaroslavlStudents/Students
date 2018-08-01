using System;
using System.Collections.Generic;

namespace FootballTournament
{
    [Serializable]
    public class Tournament
    {
        public List<List<Game>> WinnersGrid { get; private set; }
        public List<List<Game>> LosersGrid { get; private set; }
        public Player Champion { get; private set; }
        public TournamentMode TournamentMode { get; private set; }
        public bool IsFinished { get; private set; }
        public Game GrandFinal { get; private set; }

        private int _currentWinnersStage = 0;
        private int _currentLosersStage = -1;
        private int _gamesOnCurrentWinnersStage = 0;
        private int _gamesOnCurrentLosersStage = 0;

        public void Init(TournamentMode tournamentMode)
        {
            TournamentMode = tournamentMode;
            IsFinished = false;
            StartTournament();
        }

        private void StartTournament()
        {
            WinnersGrid = new List<List<Game>>();
            WinnersGrid.Add(new List<Game>());

            if (TournamentMode == TournamentMode.DoubleElimination)
            {
                LosersGrid = new List<List<Game>>();
                LosersGrid.Add(new List<Game>());
            }

            var countOfPlayers = DataInput.GetCountOfPlayers();
            var players = DataInput.GetPlayersList(countOfPlayers);

            for (int i = 0; i < players.Count; i += 2)
            {
                var firstPlayer = players[i];
                Player secondPlayer = null;

                if (i != players.Count - 1)
                    secondPlayer = players[i + 1];

                WinnersGrid[_currentWinnersStage].Add(new Game(firstPlayer, secondPlayer));
                _gamesOnCurrentWinnersStage++;
            }
        }

        public void PlayNextRound()
        {
            if (TournamentMode == TournamentMode.SingleElimination)
                PlaySingleEliminationRound();
            else
                PlayDoubleEliminationRound();
        }

        private void PlaySingleEliminationRound()
        {
            if (!IsFinished)
            {
                foreach (var game in WinnersGrid[_currentWinnersStage])
                    game.Play();

                if (!IsWinnersGridFinished())
                    InitNewWinnersGridStage();
                else
                    DetectChampion();
            }
            else
                ConsoleWorker.PrintChampion(Champion);
        }

        private void InitNewWinnersGridStage()
        {
            _currentWinnersStage++;
            FillNewStage(WinnersGrid, _currentWinnersStage);
            _gamesOnCurrentWinnersStage = WinnersGrid[_currentWinnersStage].Count;
        }

        private void FillNewStage(List<List<Game>> grid, int currentStage)
        {
            var lastStage = currentStage - 1;
            grid.Add(new List<Game>());

            for (int i = 0; i < grid[lastStage].Count; i += 2)
            {
                var firstPlayer = grid[lastStage][i].Winner;
                Player secondPlayer = null;

                if (i != grid[lastStage].Count - 1)
                    secondPlayer = grid[lastStage][i + 1].Winner;

                grid[currentStage].Add(new Game(firstPlayer, secondPlayer));
            }
        }

        private bool IsWinnersGridFinished()
        {
            if (WinnersGrid[_currentWinnersStage].Count == 1 && WinnersGrid[_currentWinnersStage][0].IsPlayed)
                return true;

            return false;
        }

        private void PlayDoubleEliminationRound()
        {
            if (!IsFinished)
            {
                if (_currentLosersStage > _currentWinnersStage && IsLosersGridFinished())
                    DetectChampion();

                if (!IsWinnersGridFinished())
                {
                    foreach (var game in WinnersGrid[_currentWinnersStage])
                        game.Play();
                }

                if (_currentLosersStage <= _currentWinnersStage || !IsLosersGridFinished())
                {
                    InitNewLosersGridStage();

                    foreach (var game in LosersGrid[_currentLosersStage])
                        game.Play();
                }

                if (!IsWinnersGridFinished())
                    InitNewWinnersGridStage();
            }
            else
                ConsoleWorker.PrintChampion(Champion);
        }

        private void InitNewLosersGridStage()
        {
            _currentLosersStage++;

            if (_currentLosersStage > 0)
            {
                FillNewStage(LosersGrid, _currentLosersStage);
                _gamesOnCurrentLosersStage = LosersGrid[_currentLosersStage].Count;
            }

            if (_currentLosersStage <= _currentWinnersStage)
                FillLosersStageByWinnersGrid();
        }

        private void FillLosersStageByWinnersGrid()
        {
            for (int i = 0; i < WinnersGrid[_currentWinnersStage].Count; i += 2)
            {
                var firstPlayer = WinnersGrid[_currentWinnersStage][i].Loser;

                if (firstPlayer != null)
                {
                    Player secondPlayer = null;

                    if (i != WinnersGrid[_currentWinnersStage].Count - 1)
                        secondPlayer = WinnersGrid[_currentWinnersStage][i + 1].Loser;

                    LosersGrid[_currentLosersStage].Add(new Game(firstPlayer, secondPlayer));
                    _gamesOnCurrentLosersStage++;
                }
            }
        }

        private bool IsLosersGridFinished()
        {
            if (LosersGrid[_currentLosersStage].Count == 1 && LosersGrid[_currentLosersStage][0].IsPlayed)
                return true;

            return false;
        }

        private void DetectChampion()
        {
            if (TournamentMode == TournamentMode.SingleElimination)
                Champion = WinnersGrid[_currentWinnersStage][0].Winner;
            else
            {
                var firstPlayer = WinnersGrid[_currentWinnersStage][0].Winner;
                var secondPlayer = LosersGrid[_currentLosersStage][0].Winner;
                GrandFinal = new Game(firstPlayer, secondPlayer);

                ConsoleWorker.PrintGrandFinal(GrandFinal);

                GrandFinal.Play();
                Champion = GrandFinal.Winner;             
            }

            IsFinished = true;

            ConsoleWorker.PrintChampion(Champion);
        }
    }
}
