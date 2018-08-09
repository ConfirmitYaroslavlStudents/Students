using System;
using System.Collections.Generic;

namespace TournamentLibrary
{
    [Serializable]
    public class SingleEliminationTournament
    {
        public List<List<Game>> WinnersGrid { get; protected set; }
        public Player Champion { get; protected set; }
        public bool IsFinished { get; protected set; }
        public int CountOfPlayers { get; protected set; }

        protected int _currentWinnersStage = 0;
        protected int _gamesOnCurrentWinnersStage = 0;

        [NonSerialized]
        protected static IViewer _viewer = Viewer.GetViewer();

        public virtual void StartTournament()
        {
            WinnersGrid = new List<List<Game>>();
            WinnersGrid.Add(new List<Game>());

            CountOfPlayers = DataInput.GetCountOfPlayers();
            var players = DataInput.GetPlayersList(CountOfPlayers);

            for (int i = 0; i < players.Count; i += 2)
            {
                var firstPlayer = players[i];
                Player secondPlayer = null;

                if (i != players.Count - 1)
                    secondPlayer = players[i + 1];

                WinnersGrid[_currentWinnersStage].Add(new Game(firstPlayer, secondPlayer));
                _gamesOnCurrentWinnersStage++;
            }

            SaveLoadSystem.Save(this);
        }

        public virtual void PlayNextRound()
        {
            if (!IsFinished)
            {
                PlayGames(WinnersGrid, _currentWinnersStage);

                if (!IsGridFinished(WinnersGrid, _currentWinnersStage))
                    InitNewWinnersGridStage();
                else
                    DetectChampion();
            }
            else
                _viewer.PrintChampion(Champion);
        }

        protected void PlayGames(List<List<Game>> grid, int stage)
        {
            foreach (var game in grid[stage])
            {
                if (!game.IsPlayed)
                {
                    game.Play();

                    SaveLoadSystem.Save(this);
                }
            }
        }

        protected void InitNewWinnersGridStage()
        {
            _currentWinnersStage++;
            FillNewStage(WinnersGrid, _currentWinnersStage);
            _gamesOnCurrentWinnersStage = WinnersGrid[_currentWinnersStage].Count;

            SaveLoadSystem.Save(this);
        }

        protected void FillNewStage(List<List<Game>> grid, int currentStage)
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

        protected bool IsGridFinished(List<List<Game>> grid, int currentStage)
        {
            if (grid[currentStage].Count == 1 && grid[currentStage][0].IsPlayed)
                return true;

            return false;
        }

        protected virtual void DetectChampion()
        {
            Champion = WinnersGrid[_currentWinnersStage][0].Winner;
            IsFinished = true;

            _viewer.PrintChampion(Champion);
        }
    }
}
