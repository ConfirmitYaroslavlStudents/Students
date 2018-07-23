using System;
using System.Collections.Generic;

namespace TournamentGrid
{
    static internal class BracketOutput
    {
        private const string _leftUpperCorner = "\u2500\u2510";
        private const string _rightUpperCorner = "\u250C\u2500";
        private const string _leftLowerCorner = "\u2500\u2518";
        private const string _rightLowerCorner = "\u2514\u2500";
        private const string _verticalStick = "\u2502";


        static public void PrintHorizontalBracket(int currentRound, List<Participant> Bracket)
        {
            int[] side = new int[currentRound + 2];
            int[] columnNamesMaxLength = new int[currentRound + 2];
            ParticipantForPrinting[,] ConsolePrint = new ParticipantForPrinting[Bracket.Count, currentRound + 1];
            for (int i = 0; i < Bracket.Count; i++)
            {
                side[Bracket[i].Round]++;
                string kindOfBracket = "";
                if (Bracket[i].Round <= currentRound && side[Bracket[i].Round] % 2 == 1)
                    kindOfBracket = _leftUpperCorner;
                else if (Bracket[i].Round <= currentRound)
                    kindOfBracket = _leftLowerCorner;

                for (int j = 0; j < currentRound + 1; j++)
                {
                    ConsolePrint[i, j] = new ParticipantForPrinting(ConsoleColor.White);
                    if (j == Bracket[i].Round && Bracket[i].Round <= currentRound)
                    {
                        ConsolePrint[i, j].Name = Bracket[i].Name;
                        ConsolePrint[i, j].Color = Bracket[i].Color;
                        ConsolePrint[i, j].KindOfBracket = kindOfBracket;

                        if (ConsolePrint[i, j].Name.Length > columnNamesMaxLength[j])
                            columnNamesMaxLength[j] = ConsolePrint[i, j].Name.Length;
                    }
                    else if (side[j] % 2 == 1)
                    {
                        ConsolePrint[i, j].Name = _verticalStick;
                    }
                }
            }
            for (int i = 0; i < Bracket.Count; i++)
            {
                for (int j = 0; j < currentRound + 1; j++)
                {
                    string format = "{0," + columnNamesMaxLength[j] + "}";
                    if (j != currentRound || side[currentRound] != 1 || Bracket[i].Round == currentRound)
                    {
                        PrintName(ConsolePrint[i, j], format);
                        if (side[Bracket[i].Round] != 1)
                            PrintFilling(ConsolePrint[i, j], format);
                    }
                    else
                        Console.Write(String.Format(format, ""));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static public void PrintBothHorizontalBrackets(int currentRound, List<Participant> upperBracket, List<Participant> lowerBracket)
        {
            Console.Clear();
            Console.WriteLine("------Upper Bracket------");
            PrintHorizontalBracket(currentRound, upperBracket);
            Console.WriteLine("------Lower Bracket------");
            PrintHorizontalBracket(currentRound, lowerBracket);
        }

        private static void PrintName(ParticipantForPrinting participant, string format)
        {
            if (participant.KindOfBracket == null)
                Console.Write("  ");

            Console.ForegroundColor = participant.Color;
            Console.Write(String.Format(format, participant.Name));

        }

        private static void PrintInterestingName(ParticipantForPrinting participant, string format)
        {         
            Console.ForegroundColor = participant.Color;
            Console.Write(String.Format(format, participant.Name));
            if (participant.KindOfBracket == null)
                Console.Write("  ");


        }

        private static void PrintFilling(ParticipantForPrinting participant, string format)
        {
            if (participant.KindOfBracket != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(participant.KindOfBracket);
            }
        }


       
    }
}
