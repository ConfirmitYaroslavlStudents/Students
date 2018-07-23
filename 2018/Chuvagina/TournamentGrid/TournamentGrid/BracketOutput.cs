using System;
using System.Collections.Generic;
using static TournamentGrid.Tournament;

namespace TournamentGrid
{
    internal class BracketOutput
    {


        private const string _leftUpperCorner = "\u2500\u2510";
        private const string _rightUpperCorner = "\u250C\u2500";
        private const string _leftLowerCorner = "\u2500\u2518";
        private const string _rightLowerCorner = "\u2514\u2500";
        private const string _verticalStick = "\u2502";
        private int _roundIndex;
        private ParticipantForPrinting[,] _printedLeftBracket;
        private ParticipantForPrinting[,] _printedRightBracket;
        private List<Participant> _bracket;
        private int[] side;
        private int[] _columnNamesMaxLength;
        private KindOfBracket _kindOfBracket;

        public BracketOutput(int roundIndex, List<Participant> Bracket, KindOfBracket kindOfBracket)
        {
            _roundIndex = roundIndex;
            _bracket = Bracket;
            _kindOfBracket = kindOfBracket;
        }

        private void CreateBracket()
        {
            _columnNamesMaxLength = new int[_roundIndex + 2];
            if (_kindOfBracket == KindOfBracket.Horizontal)
                CreateLeft(_bracket);
            else if (_kindOfBracket==KindOfBracket.PlayOff)
            {
                List<Participant> leftList = new List<Participant>();
                List<Participant> rightList = new List<Participant>();


                leftList.AddRange(_bracket.GetRange(0, _bracket.Count/2));

                rightList.AddRange(_bracket.GetRange( _bracket.Count / 2+_bracket.Count%2, _bracket.Count / 2));

                CreateLeft(leftList);
                CreateRight(rightList);
            }
        }



        private void CreateRight(List<Participant> bracket)
        {
            side = new int[_roundIndex + 2];
            _printedRightBracket = new ParticipantForPrinting[bracket.Count, _roundIndex + 1];
            for (int i = 0; i < bracket.Count; i++)
            {
                side[bracket[i].Round]++;
                string kindOfBracket = "";
                if (bracket[i].Round <= _roundIndex && side[bracket[i].Round] % 2 == 1)
                    kindOfBracket = _rightUpperCorner;
                else if (bracket[i].Round <= _roundIndex)
                    kindOfBracket = _rightLowerCorner;

                for (int j = 0; j < _roundIndex + 1; j++)
                {
                    _printedRightBracket[i, j] = new ParticipantForPrinting(ConsoleColor.White);
                    if (j == bracket[i].Round && bracket[i].Round <= _roundIndex)
                    {
                        _printedRightBracket[i, j].Name = bracket[i].Name;
                        _printedRightBracket[i, j].Color = bracket[i].Color;
                        _printedRightBracket[i, j].KindOfBracket = kindOfBracket;

                        if (_printedRightBracket[i, j].Name.Length > _columnNamesMaxLength[j])
                            _columnNamesMaxLength[j] = _printedRightBracket[i, j].Name.Length;
                    }
                    else if (side[j] % 2 == 1)
                    {
                        _printedRightBracket[i, j].Name = _verticalStick;
                    }
                }
            }
        }

        private void CreateLeft(List<Participant> bracket)
        {
            side = new int[_roundIndex + 2];
            _printedLeftBracket = new ParticipantForPrinting[bracket.Count, _roundIndex + 1];
            for (int i = 0; i < bracket.Count; i++)
            {
                side[bracket[i].Round]++;
                string kindOfBracket = "";
                if (bracket[i].Round <= _roundIndex && side[bracket[i].Round] % 2 == 1)
                    kindOfBracket = _leftUpperCorner;
                else if (bracket[i].Round <= _roundIndex)
                    kindOfBracket = _leftLowerCorner;

                for (int j = 0; j < _roundIndex + 1; j++)
                {
                    _printedLeftBracket[i, j] = new ParticipantForPrinting(ConsoleColor.White);
                    if (j == bracket[i].Round && bracket[i].Round <= _roundIndex)
                    {
                        _printedLeftBracket[i, j].Name = bracket[i].Name;
                        _printedLeftBracket[i, j].Color = bracket[i].Color;
                        _printedLeftBracket[i, j].KindOfBracket = kindOfBracket;

                        if (_printedLeftBracket[i, j].Name.Length > _columnNamesMaxLength[j])
                            _columnNamesMaxLength[j] = _printedLeftBracket[i, j].Name.Length;
                    }
                    else if (side[j] % 2 == 1)
                    {
                        _printedLeftBracket[i, j].Name = _verticalStick;
                    }
                }
            }
        }


    public void PrintBracket()
        {
            CreateBracket();
            if (_kindOfBracket == KindOfBracket.PlayOff)
                PrintPlayOffBracket();
            if (_kindOfBracket == KindOfBracket.Horizontal)
                PrintHorizontalBracket();
        }

        public void PrintPlayOffBracket()
        {
            
            for (int i = 0; i < _bracket.Count/2; i++)
            {
                for (int j = 0; j < _roundIndex + 1 ; j++)
                {
                    string format = "{0," + _columnNamesMaxLength[j] + "}";
                    if (j != _roundIndex || side[_roundIndex] != 1 || _bracket[i].Round == _roundIndex)
                    {
                        PrintLeftName(_printedLeftBracket[i, j], format);
                        if (side[_bracket[i].Round] != 1)
                            PrintFilling(_printedLeftBracket[i, j], format);
                    }
                    else
                        Console.Write(String.Format(format, ""));
                }

                    Console.Write(String.Format("{0,10}", ""));
                for (int j = _roundIndex; j >=0; j--)
                {
                    string format = "{0," + (-_columnNamesMaxLength[j])+ "}";
                    if (j != _roundIndex || side[_roundIndex] != 1 || _bracket[i].Round == _roundIndex)
                    {
                         if (side[_bracket[i].Round] != 1)
                            PrintFilling(_printedRightBracket[i, j], format);

                        PrintRightName(_printedRightBracket[i, j], format);
                       
                    }
                    else
                        Console.Write(String.Format(format, ""));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void PrintHorizontalBracket()
        {
            for (int i = 0; i < _bracket.Count; i++)
            {
                for (int j = 0; j < _roundIndex + 1; j++)
                {
                    string format = "{0," + _columnNamesMaxLength[j] + "}";
                    if (j != _roundIndex || side[_roundIndex] != 1 || _bracket[i].Round == _roundIndex)
                    {
                        PrintLeftName(_printedLeftBracket[i, j], format);
                        if (side[_bracket[i].Round] != 1)
                            PrintFilling(_printedLeftBracket[i, j], format);
                    }
                    else
                        Console.Write(String.Format(format, ""));
                }

                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }


        private void PrintLeftName(ParticipantForPrinting participant, string format)
        {
            if (participant.KindOfBracket == null)
                Console.Write("  ");

            Console.ForegroundColor = participant.Color;
            Console.Write(String.Format(format, participant.Name));

        }

        private void PrintRightName(ParticipantForPrinting participant, string format)
        {         
            Console.ForegroundColor = participant.Color;
            Console.Write(String.Format(format, participant.Name));
            if (participant.KindOfBracket == null)
                Console.Write("  ");


        }

        private void PrintFilling(ParticipantForPrinting participant, string format)
        {
            if (participant.KindOfBracket != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(participant.KindOfBracket);
            }
        }


       
    }
}
