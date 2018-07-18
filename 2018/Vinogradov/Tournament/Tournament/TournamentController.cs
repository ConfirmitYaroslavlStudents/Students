using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    public class TournamentController
    {
        public string[] Players;
        public PlayerPair[][] Matches;
        public int Winner;

        public TournamentController()
        {
            Players = new string[4];
            Matches = new PlayerPair[2][];
            Matches[0] = new PlayerPair[2];
            Matches[1] = new PlayerPair[1];
            Winner = -1;


            for (int i = 0; i < 4; i += 2)
            {
                Matches[0][i / 2] = new PlayerPair(i, i + 1);
            }
            for (int k = 1; k < Matches.Length; k++)
            {
                for (int i = 0; i < Matches[k].Length; i++)
                    Matches[k][i] = new PlayerPair(-1, -1);
            }

            Messenger.NameInput(Players);
            Shuffle();
        }

        private void Shuffle()
        {
            Random rng = new Random();
            for (int i = Players.Length - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                if (i != j)
                {
                    string tmp = Players[i];
                    Players[i] = Players[j];
                    Players[j] = tmp;
                }
            }
        }

        public void PlayGame(int tour, int match, int winner)
        {
            tour--;
            winner--;
            match--;

            bool correctTour = (tour >= 0 && tour < Matches.Length);
            bool correctWinner = (winner == 0 || winner == 1);

            if (correctTour && correctWinner)
            {
                bool correctMatch = (match >= 0 && match < Matches[tour].Length);
                if (correctMatch && Matches[tour][match].PlayersReady())
                {

                    if (tour == Matches.Length - 1)
                    {
                        if (Winner == -1)
                        {
                            Winner = Matches[tour][match].Opponents[winner];
                        }
                        else throw new InvalidOperationException("This match was already played.");
                    }
                    else if (Matches[tour + 1][match / 2].Opponents[match % 2] == -1)
                    {
                        Matches[tour + 1][match / 2].Opponents[match % 2] = Matches[tour][match].Opponents[winner];
                    }
                    else throw new InvalidOperationException("This match was already played.");
                }
                else
                    throw new ArgumentException("Invalid match parameters.");
            }
            else
                throw new ArgumentException("Invalid match parameters.");
        }

        public void Restart()
        {
            Matches[1][0] = new PlayerPair(-1, -1);
            Winner = -1;
        }
        
    }
}
