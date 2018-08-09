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

        [NonSerialized]
        private static IViewer _viewer = Viewer.GetViewer();

        public Game(Player firstPlayer, Player secondPlayer)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            IsPlayed = false;
        }

        public void Play()
        {
            if (SecondPlayer != null)
            {
                FirstPlayerScore = _viewer.EnterPlayerScore(FirstPlayer);
                SecondPlayerScore = _viewer.EnterPlayerScore(SecondPlayer);

                if (FirstPlayerScore == SecondPlayerScore)
                {
                    _viewer.DrawIsNotPossible();
                    Play();
                }
                else
                {
                    IsPlayed = true;
                    DetectWinner();
                    _viewer.PrintGameResult(this);
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
        }
    }
}
