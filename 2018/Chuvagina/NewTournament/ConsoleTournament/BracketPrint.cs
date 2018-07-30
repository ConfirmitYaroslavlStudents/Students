using System;
using System.Collections.Generic;
using Tournament;

namespace ConsoleTournament
{
    public static class BracketPrint
    {
        private const string _leftUpperCorner = "\u2500\u2510";
        private const string _leftLowerCorner = "\u2500\u2518";
        private const string _rightUpperCorner = "\u250C\u2500";
        private const string _rightLowerCorner = "\u2514\u2500";
        private const string _verticalStick = "\u2502";
        private const int _horizontalAlign = 10;
        private const int _playOffAlign = -10;

        private static int[] _roundParticipants;
        private static int _maximumRound;
        private static string _upperCorner;
        private static string _lowerCorner;
        private static int _cursorLeft = 0;
        private static List<string> _line;
        private static List<ConsoleColor> _lineColor;
        private static Add _add;
        private static AddWithFilling _addWithFilling;
        private static int _lastShift;
        private static int _firstShift;

        private delegate void Add(string text, ConsoleColor color);
        private delegate void AddWithFilling(string name, string filling, ConsoleColor color);

        public static void PrintHorizontalBracket(List<Participant> bracket)
        {
            _lastShift = bracket.Count == 1 ? 1 : 0;
            _firstShift = 0;
            _add = AddLine;
            _addWithFilling = AddLine;
            _cursorLeft = 0;
            _upperCorner = _leftUpperCorner;
            _lowerCorner = _leftLowerCorner;
            Print(bracket);
        }

        public static void PrintPlayOffBracket(List<Participant> bracket)
        {
            int consoleTop = Console.CursorTop;
            if (bracket.Count == 1)
            {
                PrintHorizontalBracket(new List<Participant> { bracket[0].Left });
                bracket = new List<Participant> { bracket[0].Right };
            }
            else
            {
                PrintHorizontalBracket(bracket.GetRange(0, bracket.Count / 2));
                bracket = bracket.GetRange(bracket.Count / 2, bracket.Count / 2);
            }

            _firstShift = bracket.Count == 1 ? 1 : 0;
            _lastShift = 0;
            _add = AddReverseLine;
            _addWithFilling = AddReverseLine;
            _cursorLeft = Console.WindowWidth/2;
            Console.CursorTop = consoleTop;
            _upperCorner = _rightUpperCorner;
            _lowerCorner = _rightLowerCorner;
            Print(bracket);
        }

        private static void AddLine(string text, ConsoleColor color) 
        {
             _lineColor.Add(color);
             _line.Add($"{text,_horizontalAlign}");            
        }

        private static void AddLine(string name, string filling, ConsoleColor color)
        {
            _lineColor.Add(color);
            _line.Add($"{name,_horizontalAlign-2}");

            _lineColor.Add(ConsoleColor.Gray);
            _line.Add($"{filling,2}");
        }

        private static void AddReverseLine(string text, ConsoleColor color)
        {
            _lineColor.Insert(0,color);
            _line.Insert(0,$"{text,_playOffAlign}");
        }

        private static void AddReverseLine(string name, string filling, ConsoleColor color)
        {
            _lineColor.Insert(0,color);
            _line.Insert(0, $"{name,_playOffAlign+2}");

            _lineColor.Insert(0,ConsoleColor.Gray);
            _line.Insert(0, $"{filling,-2}");
        }

        private static int _maxDepth(Participant participant)
        {
            if (participant == null) return 0;

            if (_maxDepth(participant.Left) > _maxDepth(participant.Right))
                return _maxDepth(participant.Left) + 1;

            return _maxDepth(participant.Right) + 1;
        }

        private static void Print(List<Participant> bracket)
        {
            int maxDepth = 0;

            foreach (var participant in bracket)
            {
                if (_maxDepth(participant) > maxDepth)
                    maxDepth = _maxDepth(participant);
            }

            _roundParticipants = new int[maxDepth + 1];
            _maximumRound = maxDepth + 1;

            if (bracket.Count % 2 == 1 && bracket.Count != 1)
                bracket.RemoveAt(bracket.Count - 1);

            for (int i = 0; i < bracket.Count; i++)
            {
                if (bracket.Count == 1)
                    PrintParticipant(bracket[i], maxDepth, "");
                else if (i % 2 == 0)
                    PrintParticipant(bracket[i], maxDepth, _upperCorner);
                else
                    PrintParticipant(bracket[i], maxDepth, _lowerCorner);
            }
        }

        private static void PrintParticipant(Participant participant, int round, string filling)
        {
            if (participant.Left != null)
                PrintParticipant(participant.Left, round - 1, _upperCorner);

            PrintLine(participant, round, filling);

            if (participant.Right != null)
                PrintParticipant(participant.Right, round - 1, _lowerCorner);
        }

        private static void PrintLine(Participant participant, int round, string filling)
        {
            _line=new List<string>();
            _lineColor = new List<ConsoleColor>();
            string text = "";
            var color = ConsoleColor.Gray;

            for (int i = 1; i < round; i++)
            {
                if (_roundParticipants[i] % 2 == 1)
                    text = _verticalStick;
                else
                    text = "";

                _add(text, color);
            }

            if (participant.Winner != null && participant.Name.Equals(participant.Winner.Name))
                color = ConsoleColor.Green;
            else
                color = ConsoleColor.White;

            _addWithFilling(participant.Name, filling, color);

            _roundParticipants[round]++;

            color = ConsoleColor.Gray;
            for (int i = round + 1; i < _maximumRound; i++)
            {
                if (_roundParticipants[i] % 2 == 1)
                    text = _verticalStick;
                else
                    text="";

                _add(text, color);
            }

            Console.CursorLeft = _cursorLeft;

            for (int i = 0+_firstShift; i < _line.Count -_lastShift; i++)
            {
                Console.ForegroundColor = _lineColor[i];
                if (_firstShift==1 && _line[i].Length!=8 && i==1)
                    Console.Write("{0,-8}", "");
                Console.Write(_line[i]);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}
