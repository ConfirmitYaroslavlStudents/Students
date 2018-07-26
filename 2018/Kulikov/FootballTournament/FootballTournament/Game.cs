using System;

namespace FootballTournament
{
    public class Game
    {
        private Player _firstPlayer;
        private Player _secondPlayer;
        private int _firstPlayerScore;
        private int _secondPlayerScore;
        private bool _isPlayed;

        public Game(Player firstPlayer, Player secondPlayer)
        {
            _firstPlayer = firstPlayer;
            _secondPlayer = secondPlayer;
            _isPlayed = false;
        }

        public Game(Player firstPlayer)
        {
            _firstPlayer = firstPlayer;
            _isPlayed = false;
        }

        public int FirstPlayerScore
        {
            get { return _firstPlayerScore; }
            set
            {
                if (value >= 0)
                    _firstPlayerScore = value;
                else
                    throw new ArgumentException("The value can't be less than 0!");
            }
        }

        public int SecondPlayerScore
        {
            get { return _secondPlayerScore; }
            set
            {
                if (value >= 0)
                    _secondPlayerScore = value;
                else
                    throw new ArgumentException("The value can't be less than 0!");
            }
        }

        public void Play()
        {
            if (_secondPlayer != null)
            {
                Console.Write("{0} scores: ", _firstPlayer.Name);
                FirstPlayerScore = int.Parse(Console.ReadLine());
                Console.Write("{0} scores: ", _secondPlayer.Name);
                SecondPlayerScore = int.Parse(Console.ReadLine());

                if (_firstPlayerScore == _secondPlayerScore)
                {
                    Console.WriteLine("There is can't be a draw. Try again:");
                    Play();
                }
                else
                {
                    _isPlayed = true;
                    Console.WriteLine(ToString());
                }
            }
        }

        public Player DetectWinner()
        {
            if (_secondPlayer != null)
            {
                if (_firstPlayerScore > _secondPlayerScore)
                    return _firstPlayer;
                else
                    return _secondPlayer;
            }
            else
                return _firstPlayer;
        }

        public Player DetectLoser()
        {
            if (_secondPlayer != null)
            {
                if (_firstPlayerScore > _secondPlayerScore)
                    return _secondPlayer;
                else
                    return _firstPlayer;
            }
            else
                return null;
        }

        public override string ToString()
        {
            if (_secondPlayer != null)
            {
                if (_isPlayed == true)
                    return $"{_firstPlayer.Name} {_firstPlayerScore}:{_secondPlayerScore} {_secondPlayer.Name}";
                else
                    return $"{_firstPlayer.Name} -:- { _secondPlayer.Name}";
            }
            else
                return $"{_firstPlayer.Name}";
        }
    }
}
