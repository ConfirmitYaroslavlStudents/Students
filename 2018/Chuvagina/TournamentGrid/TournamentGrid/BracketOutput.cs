using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGrid
{
    static public class BracketOutput
    {
        static public void PrintGorizontalGrid(int currentRound, List<Participant> Bracket)
        {
            int[] side = new int[currentRound + 2];
            ParticipantForPrinting[,] ConsolePrint = new ParticipantForPrinting[Bracket.Count, currentRound + 1];
            for (int i = 0; i < Bracket.Count; i++)
            {     
                side[Bracket[i].Round]++;
                string filling = "";
                if (Bracket[i].Round <= currentRound && side[Bracket[i].Round] % 2 == 1)
                    filling = "\u2500\u2510";
                else if (Bracket[i].Round <= currentRound)
                    filling = "\u2500\u2518";
              
                    for (int j = 0; j < currentRound + 1; j++)
                    {
                        ConsolePrint[i,j] = new ParticipantForPrinting(ConsoleColor.White);
                        if (j == Bracket[i].Round && Bracket[i].Round <= currentRound)
                        {
                            ConsolePrint[i, j].Name = Bracket[i].Name;
                            ConsolePrint[i, j].Color = Bracket[i].Color;
                            ConsolePrint[i, j].Filling = filling;
                        }
                        else if (side[j] % 2 == 1)
                        {
                            ConsolePrint[i, j].Name = "\u2502";
                        }
                    }
            }

            for (int i = 0; i < Bracket.Count; i++)
            {
                for (int j = 0; j < currentRound +1; j++)
                {
                    if (j != currentRound || side[currentRound] != 1 )
                    {
                        Console.ForegroundColor = ConsolePrint[i, j].Color;
                        Console.Write(String.Format("{0,10}", ConsolePrint[i, j].Name + ConsolePrint[i, j].Filling));
                    }
                    else if (Bracket[i].Round == currentRound)
                    {
                        Console.ForegroundColor = ConsolePrint[i, j].Color;
                        Console.Write(String.Format("{0,10}", ConsolePrint[i, j].Name));
                    }
                    else
                        Console.Write(String.Format("{0,10}",""));

                }
                   
                Console.WriteLine();
               
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
