using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTournament
{
    public class Game
    {
        private int _firstPlayerScore;
        private int _secondPlayerScore;

        public Game() { }
=======

namespace FootballTournament
{
    [Serializable]
    public class Game
    {
        public Player FirstPlayer { get; private set; }
        public Player SecondPlayer { get; private set; }
        public int FirstPlayerScore { get; private set; }
        public int SecondPlayerScore { get; private set; }
        public Player Winner { get; private set; }
        public Player Loser { get; private set; }
        public bool IsPlayed { get; private set; }
>>>>>>> Kulikov_Tournament

        public Game(Player firstPlayer, Player secondPlayer)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
<<<<<<< HEAD
        }

        public Player FirstPlayer { get; set; }

        public Player SecondPlayer { get; set; }

        public bool IsPlayed = false;

        public int FirstPlayerScore
        {
            get { return _firstPlayerScore; }
            set
            {
                if (value >= 0) _firstPlayerScore = value;
                else throw new ArgumentException("The value can't be less than 0!");
            }
        }

        public int SecondPlayerScore
        {
            get { return _secondPlayerScore; }
            set
            {
                if (value >= 0) _secondPlayerScore = value;
                else throw new ArgumentException("The value can't be less than 0!");
            }
        }

        public void Play()
        {
            Console.Write("{0} scores: ", FirstPlayer.Name);
            FirstPlayerScore = int.Parse(Console.ReadLine());
            Console.Write("{0} scores: ", SecondPlayer.Name);
            SecondPlayerScore = int.Parse(Console.ReadLine());

            if (FirstPlayerScore == SecondPlayerScore)
            {
                Console.WriteLine("There is can't be a draw. Try again:");
                Play();
            }
            else
            {
                IsPlayed = true;
                Console.WriteLine(ToString());
            }
        }

        public Player GetWinner()
        {
            if (FirstPlayerScore > SecondPlayerScore) return FirstPlayer;
            else return SecondPlayer;
        }

        public override string ToString()
        {
            if (IsPlayed == true) return FirstPlayer.Name + ' ' + FirstPlayerScore + ':' + SecondPlayerScore + ' ' + SecondPlayer.Name;
            else return FirstPlayer.Name + ' ' + '-' + ':' + '-' + ' ' + SecondPlayer.Name;
=======
            IsPlayed = false;
        }

        public void Play()
        {
            if (SecondPlayer != null)
            {
                FirstPlayerScore = ConsoleWorker.EnterPlayerScore(FirstPlayer);
                SecondPlayerScore = ConsoleWorker.EnterPlayerScore(SecondPlayer);

                if (FirstPlayerScore == SecondPlayerScore)
                {
                    ConsoleWorker.DrawIsNotPossible();
                    Play();
                }
                else
                {
                    IsPlayed = true;
                    DetectWinner();
                    ConsoleWorker.PrintGameResult(this);
                }
            }
            else
                Winner = FirstPlayer;
        }

        public void DetectWinner()
        {
            if (FirstPlayerScore > SecondPlayerScore)
            {
                Winner = FirstPlayer;
                Loser = SecondPlayer;
            }
            else
            {
                Winner = SecondPlayer;
                Loser = FirstPlayer;
            }
        }

        public string Result()
        {
            if (SecondPlayer != null)
            {
                if (IsPlayed == true)
                    return $"{FirstPlayer.Name} {FirstPlayerScore}:{SecondPlayerScore} {SecondPlayer.Name}";
                else
                    return $"{FirstPlayer.Name} -:- { SecondPlayer.Name}";
            }
            else
                return $"{FirstPlayer.Name}";
>>>>>>> Kulikov_Tournament
        }
    }
}
