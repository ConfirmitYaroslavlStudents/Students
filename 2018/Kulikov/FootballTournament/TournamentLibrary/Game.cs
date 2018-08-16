using System;

namespace TournamentLibrary
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

        public Game(Player firstPlayer, Player secondPlayer)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            IsPlayed = false;
        }

        public void Play(int firstPlayerScore, int secondPlayerScore)
        {
            FirstPlayerScore = firstPlayerScore;
            SecondPlayerScore = secondPlayerScore;
            IsPlayed = true;
            DetectWinner();
        }

        public void Play(int firstPlayerScore)
        {
            FirstPlayerScore = firstPlayerScore;
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
        }
    }
}
