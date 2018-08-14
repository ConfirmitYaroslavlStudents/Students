using System;
using System.Collections.Generic;

namespace TournamentLibrary
{
    [Serializable]
    public class SingleEliminationTournament : Tournament
    {
        public override void PlayNextRound()
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

        protected override void DetectChampion()
        {
            Champion = WinnersGrid[_currentWinnersStage][0].Winner;
            IsFinished = true;

            _viewer.PrintChampion(Champion);
        }
    }
}
