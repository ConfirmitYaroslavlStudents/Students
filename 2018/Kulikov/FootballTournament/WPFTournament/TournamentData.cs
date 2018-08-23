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
        private Tournament _tournament;
        private int _gamesNeedToPlay;

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
            _tournament = tournament;
            GamesToPlay.Clear();
            var lastStage = tournament.WinnersGrid.Count - 1;

            foreach (var game in tournament.WinnersGrid[lastStage])
            {
                if(game.SecondPlayer!=null)
                    GamesToPlay.Add(new Game(game.FirstPlayer, game.SecondPlayer));
            }

            if (tournament is DoubleEliminationTournament)
            {
                var doubleEliminationTournament = (DoubleEliminationTournament)tournament;
                lastStage = doubleEliminationTournament.LosersGrid.Count - 1;

                foreach (var game in doubleEliminationTournament.LosersGrid[lastStage])
                {
                    if (GamesToPlay[GamesToPlay.Count - 1].SecondPlayer == null)
                        GamesToPlay[GamesToPlay.Count - 1] = new Game(GamesToPlay[GamesToPlay.Count - 1].FirstPlayer, game.Winner);
                    else
                        GamesToPlay.Add(new Game(game.Winner, null));
                }

                if (GamesToPlay[GamesToPlay.Count - 1].SecondPlayer == null)
                    GamesToPlay.Remove(GamesToPlay[GamesToPlay.Count - 1]);
            }

            PlayedOnCurrentStage = 0;
            _gamesNeedToPlay = GamesToPlay.Count;
        }

        public void SetGameScore(int firstPlayerScore, int secondPlayerScore)
        {
            GamesToPlay[PlayedOnCurrentStage].Play(firstPlayerScore, secondPlayerScore);

            if (_tournament is DoubleEliminationTournament && GamesToPlay.Count > 1 && PlayedOnCurrentStage < _gamesNeedToPlay)
            {
                var lastGame = GamesToPlay[GamesToPlay.Count - 1];

                if (lastGame.SecondPlayer == null)
                    GamesToPlay[GamesToPlay.Count - 1] = new Game(lastGame.FirstPlayer, GamesToPlay[PlayedOnCurrentStage].Loser);
                else
                {
                    if (PlayedOnCurrentStage == _gamesNeedToPlay - 1 && PlayedOnCurrentStage % 2 == 0)
                    {
                        PlayedOnCurrentStage++;
                        return;
                    }
                    else
                        GamesToPlay.Add(new Game(GamesToPlay[PlayedOnCurrentStage].Loser, null));
                }
            }

            PlayedOnCurrentStage++;
        }

        public bool IsStagePlayed()
        {
            if (PlayedOnCurrentStage >= GamesToPlay.Count)
                return true;
            else
                return false;
        }
    }
}
