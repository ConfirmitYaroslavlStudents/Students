using System;
using System.Collections.Generic;
using System.Data;

namespace TournamentLibrary
{
    [Serializable]
    public abstract class Tournament
    {
        public List<List<Game>> WinnersGrid { get; protected set; }
        public Player Champion { get; protected set; }
        public bool IsFinished { get; protected set; }
        public int CountOfPlayers { get; protected set; }

        protected int _currentWinnersStage = 0;
        protected int _gamesOnCurrentWinnersStage = 0;

        [NonSerialized]
        protected IDataManager _printer;

        protected Tournament() { }

        public Tournament(IDataManager printer)
        {
            _printer = printer;
        }

        public void SetPrinter(IDataManager printer)
        {
            _printer = printer;
        }

        public abstract void PlayNextRound();

        public virtual void StartTournament()
        {
            WinnersGrid = new List<List<Game>>();
            WinnersGrid.Add(new List<Game>());

            DataInput dataInput = new DataInput(_printer);
            CountOfPlayers = dataInput.GetCountOfPlayers();
            var players = dataInput.GetPlayersList(CountOfPlayers);

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

        protected abstract void DetectChampion();

        protected void PlayGames(List<List<Game>> grid, int stage)
        {
            foreach (var game in grid[stage])
                PlayGame(game);
        }

        protected void PlayGame(Game game)
        {
            if (!game.IsPlayed)
            {
                if (game.SecondPlayer != null)
                {
                    _printer.EnterPlayerScore(game.FirstPlayer);
                    var firstPlayerScore = _printer.GetPlayerScore();

                    _printer.EnterPlayerScore(game.SecondPlayer);
                    var secondPlayerScore = _printer.GetPlayerScore();

                    while (firstPlayerScore == secondPlayerScore)
                    {
                        _printer.DrawIsNotPossible();

                        _printer.EnterPlayerScore(game.FirstPlayer);
                        firstPlayerScore = _printer.GetPlayerScore();

                        _printer.EnterPlayerScore(game.SecondPlayer);
                        secondPlayerScore = _printer.GetPlayerScore();
                    }

                    game.Play(firstPlayerScore, secondPlayerScore);

                    _printer.PrintGameResult(game);
                }
                else
                    game.Play();

                SaveLoadSystem.Save(this);
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

        public void ResponseData(Foo foo)
        {
            switch (foo.RequestedData)
            {
                case RequestedData.PlayersCount:
                    CountOfPlayers = int.Parse(foo.Message);
                    _printer.RequestData(new Foo(){RequestedData = RequestedData.PlayerName});
                    break;
            }
        }
    }
}
