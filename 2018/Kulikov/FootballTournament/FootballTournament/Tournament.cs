using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTournament
{
    public class Tournament
    {
        private List<List<Game>> _stages = new List<List<Game>>();
        private int _currentStage;
        private int _gamesOnCurrentStage;
        private int _playedOnCurrentStage;

        public Tournament() { }

        public void Load()
        {
            Console.WriteLine("Tournament commands: ");
            Console.WriteLine("start - start new tournament");
            Console.WriteLine("show - show all games and results");
            Console.WriteLine("play - play next round");
            Console.WriteLine("exit - close programm\n");
            Switch(Console.ReadLine());
        }

        public void Switch(string input)
        {
            while (input != "exit")
            {
                switch (input)
                {
                    case "start":
                        Start();
                        break;
                    case "play":
                        PlayStage();
                        break;
                    case "show":
                        Show();
                        break;
                    case "exit":
                        return;
                }
                input = Console.ReadLine();
            }
        }

        public void Start()
        {
            _stages.Clear();
            _stages.Add(new List<Game>());
            _currentStage = 0;
            _gamesOnCurrentStage = 0;
            _playedOnCurrentStage = 0;

            Console.WriteLine("\nNew tournament started!");
            var countOfPlayers = CheckCountOfPlayers();
            Console.WriteLine("\nEnter names of players: ");
            var players = new List<Player>();

            for (int i = 0; i < countOfPlayers; i++)
            {
                Console.Write("{0}. ", i + 1);
                players.Add(new Player(Console.ReadLine()));
                if (i % 2 != 0)
                {
                    _stages[_currentStage].Add(new Game(players[i - 1], players[i]));
                    _gamesOnCurrentStage++;
=======

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
        public int CountOfPlayers { get; private set; }

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
                PlayGames(WinnersGrid, _currentWinnersStage);

                if (!IsWinnersGridFinished())
                    InitNewWinnersGridStage();
                else
                    DetectChampion();
            }
            else
                ConsoleWorker.PrintChampion(Champion);
        }

        private void PlayGames(List<List<Game>> grid, int stage)
        {
            foreach (var game in grid[stage])
            {
                if (!game.IsPlayed)
                {
                    game.Play();

                    SaveLoadSystem.Save(this);
>>>>>>> Kulikov_Tournament
                }
            }
        }

<<<<<<< HEAD
        private int CheckCountOfPlayers()
        {
            Console.Write("Enter count of players: ");
            var countOfPlayers = int.Parse(Console.ReadLine());
            while (!IsDegreeOfTwo(countOfPlayers))
            {
                Console.WriteLine("{0} is not degree of two! Try again:", countOfPlayers);
                Console.Write("Enter count of players: ");
                countOfPlayers = int.Parse(Console.ReadLine());
            }
            return countOfPlayers;
        }

        private bool IsDegreeOfTwo(int number)
        {
            if (number == 1)
            {
                return false;
            }

            while (number % 2 == 0)
                number /= 2;
            return number == 1;
        }

        public void PlayStage()
        {
            Console.WriteLine("\nPlaying round №{0}:", _currentStage + 1);
            foreach (var game in _stages[_currentStage])
            {
                game.Play();
                _playedOnCurrentStage++;
            }

            if (_playedOnCurrentStage == _gamesOnCurrentStage)
            {
                if (_gamesOnCurrentStage > 1) NewStage();
                else
                {
                    Show();
                    GetChampion();
                }
            }
        }

        private void NewStage()
        {
            var lastStage = _currentStage;
            _currentStage++;
            _playedOnCurrentStage = 0;
            _gamesOnCurrentStage = 0;
            _stages.Add(new List<Game>());

            for (int i = 0; i < _stages[lastStage].Count; i += 2)
            {
                var firstPlayer = _stages[lastStage][i].GetWinner();
                var secondPlayer = _stages[lastStage][i + 1].GetWinner();
                _stages[_currentStage].Add(new Game(firstPlayer, secondPlayer));
                _gamesOnCurrentStage++;
            }
        }

        public void Show()
        {
            Console.WriteLine("\nResults: ");
            for (int i = 0; i < _stages.Count; i++)
            {
                Console.WriteLine("\nRound №{0}:", i + 1);
                for (int j = 0; j < _stages[i].Count; j++)
                {
                    Console.WriteLine("{0}. {1}", j + 1, _stages[i][j].ToString());
=======
        private void InitNewWinnersGridStage()
        {
            _currentWinnersStage++;
            FillNewStage(WinnersGrid, _currentWinnersStage);
            _gamesOnCurrentWinnersStage = WinnersGrid[_currentWinnersStage].Count;

            SaveLoadSystem.Save(this);
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
                    PlayGames(WinnersGrid, _currentWinnersStage);
                }

                if (_currentLosersStage <= _currentWinnersStage || !IsLosersGridFinished())
                {
                    InitNewLosersGridStage();

                    PlayGames(LosersGrid, _currentLosersStage);
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
>>>>>>> Kulikov_Tournament
                }
            }
        }

<<<<<<< HEAD
        private void GetChampion()
        {
            var champion = _stages[_currentStage][0].GetWinner();
            Console.WriteLine("\n{0} is a champion!", champion.Name);
=======
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
>>>>>>> Kulikov_Tournament
        }
    }
}
