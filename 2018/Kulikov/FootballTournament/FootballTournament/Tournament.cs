using System;
using System.Collections.Generic;
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
                }
            }
        }

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
                }
            }
        }

        private void GetChampion()
        {
            var champion = _stages[_currentStage][0].GetWinner();
            Console.WriteLine("\n{0} is a champion!", champion.Name);
        }
    }
}
