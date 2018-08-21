using System;
using System.Collections.Generic;

namespace TournamentLibrary
{
    [Serializable]
    public class DoubleEliminationTournament : Tournament
    {
        public List<List<Game>> LosersGrid { get; private set; }
        public Game GrandFinal { get; private set; }

        private int _currentLosersStage = -1;
        private int _gamesOnCurrentLosersStage = 0;

        public DoubleEliminationTournament(IPrinter printer)
        {
            _printer = printer;
        }

        public override void StartTournament()
        {
            base.StartTournament();
            
            LosersGrid = new List<List<Game>>();
            LosersGrid.Add(new List<Game>());

            SaveLoadSystem.Save(this);
        }

        public override void PlayNextRound()
        {
            if (!IsFinished)
            {
                if (_currentLosersStage > _currentWinnersStage && IsGridFinished(LosersGrid,_currentLosersStage))
                    DetectChampion();

                if (!IsGridFinished(WinnersGrid, _currentWinnersStage))
                {
                    PlayGames(WinnersGrid, _currentWinnersStage);
                }

                if (_currentLosersStage <= _currentWinnersStage || !IsGridFinished(LosersGrid, _currentLosersStage))
                {
                    InitNewLosersGridStage();

                    PlayGames(LosersGrid, _currentLosersStage);
                }

                if (!IsGridFinished(WinnersGrid, _currentWinnersStage))
                    InitNewWinnersGridStage();
            }
            else
                _printer.PrintChampion(Champion);
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

            SaveLoadSystem.Save(this);
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

        protected override void DetectChampion()
        {
            var firstPlayer = WinnersGrid[_currentWinnersStage][0].Winner;
            var secondPlayer = LosersGrid[_currentLosersStage][0].Winner;
            GrandFinal = new Game(firstPlayer, secondPlayer);

            _printer.PrintGrandFinal(GrandFinal);

            PlayGame(GrandFinal);
            Champion = GrandFinal.Winner;

            IsFinished = true;

            _printer.PrintChampion(Champion);
        }
    }
}
