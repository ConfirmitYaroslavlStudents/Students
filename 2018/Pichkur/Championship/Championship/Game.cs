using System;

namespace Championship
{
    public class Game
    {
        public string Winner { get; private set; }

        public Team FirstTeam;
        public Team SecondTeam;

        public Game()
        {
            Winner = null;
        }

        public void SetWinner()
        {
            FirstTeam.SetScore();
            SecondTeam.SetScore();

            while (FirstTeam.Score == SecondTeam.Score)
            {
                Console.WriteLine("\nPlay-Off game cannot end in a draw.Try again.\n");

                FirstTeam.SetScore();
                SecondTeam.SetScore();
            }

            if (FirstTeam.Score > SecondTeam.Score)
            {
                Winner = FirstTeam.Name;
            }
            else
            {
                Winner = SecondTeam.Name;
            }
        }

        public void PrintResultOfGame()
        {
            Console.WriteLine("{0} {1}:{2} {3}", FirstTeam.Name, FirstTeam.Score, SecondTeam.Score, SecondTeam.Name);
            Console.WriteLine("{0} won!", Winner);
            Console.WriteLine();
        }

        public void PrintOpponents()
        {
            Console.WriteLine("{0} vs {1}", FirstTeam.Name, SecondTeam.Name);
        }
    }
}
