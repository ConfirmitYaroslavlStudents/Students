using System.Collections.Generic;
using TournamentLibrary;

namespace WPFTournament
{
    public class TournamentData
    {
        public int CountOfPlayers { get; private set; }
        public List<Player> Players { get; private set; }
        public int IndexOfCurrentPlayer { get; set; }
        public List<Game> GamesToPlay { get; private set; }
        public int PlayedOnCurrentStage { get; private set; }

        private HashSet<string> _existingNames;
        private int _currentPlayerForAddition;

        public TournamentData()
        {
            Players = new List<Player>();
            IndexOfCurrentPlayer = 1;
            _existingNames = new HashSet<string>();
            _currentPlayerForAddition = 0;
            GamesToPlay = new List<Game>();
        }

        public void GetCountOfPlayers(int count)
        {
            CountOfPlayers = count;
        }

        public bool IsPlayerExists(string name)
        {
            if (_existingNames.Contains(name))
                return true;
            else
                return false;
        }

        public void AddPlayer(string name)
        {
            _existingNames.Add(name);
            Players.Add(new Player(name));
            _currentPlayerForAddition++;
        }

        public bool IsAdditionOver()
        {
            if (CountOfPlayers == _currentPlayerForAddition)
                return true;
            else
                return false;
        }

        public void GetGamesToPlay(Tournament tournament)
        {
            GamesToPlay.Clear();
            var lastStage = tournament.WinnersGrid.Count - 1;

            foreach (var game in tournament.WinnersGrid[lastStage])
                GamesToPlay.Add(new Game(game.FirstPlayer, game.SecondPlayer));

            if (tournament is DoubleEliminationTournament)
            {
                var doubleEliminationTournament = (DoubleEliminationTournament)tournament;
                lastStage = doubleEliminationTournament.LosersGrid.Count - 1;

                foreach (var game in doubleEliminationTournament.LosersGrid[lastStage])
                    GamesToPlay.Add(new Game(game.FirstPlayer, game.SecondPlayer));
            }

            PlayedOnCurrentStage = 0;
        }

        public void SetGameScore(int firstPlayerScore, int secondPlayerScore)
        {
            GamesToPlay[PlayedOnCurrentStage].Play(firstPlayerScore, secondPlayerScore);
            PlayedOnCurrentStage++;
        }
    }
}
