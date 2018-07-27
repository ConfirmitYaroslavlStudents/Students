using System;
using System.Collections.Generic;
using Tournament;

namespace ConsoleTournament
{
    public static class ReverseBracket
    {

        private const string _leftUpperCorner = "\u2500\u2510";
        private const string _rightUpperCorner = "\u250C\u2500";
        private const string _leftLowerCorner = "\u2500\u2518";
        private const string _rightLowerCorner = "\u2514\u2500";
        private const string _verticalStick = "\u2502";
        private const string _horizontalStick = "\u005f";
        private static int[] _roundParticipants;
        private static int _maximumRound;
        private static int _left;

        public static void Print(List<Participant> bracket, int round)
        {
            _left =  10 * (round + 1);
            if (bracket.Count==1)
            {
                HorizontalBracket.Print(new List<Participant> { bracket[0].Left }, round-1);

                Console.CursorTop= 1;
                PrintParticipant(bracket[0].Right, round-1, "");
            }
            else
            {

                HorizontalBracket.Print(bracket.GetRange(0, bracket.Count / 2), round);

                _roundParticipants = new int[round + 1];
                _maximumRound = round + 1;
                if (bracket.Count % 2 == 1 && bracket.Count != 1)
                    bracket.RemoveAt(bracket.Count - 1);
                Console.CursorTop = 1;
                for (int i = bracket.Count / 2; i < bracket.Count; i++)
                {
                    if (bracket.Count/2 == 1)
                    {
                       // maximumRound--;
                        PrintParticipant(bracket[i], round, "");
                    }
                    else if (i % 2 == 0)
                        PrintParticipant(bracket[i], round, _rightUpperCorner);
                    else
                        PrintParticipant(bracket[i], round, _rightLowerCorner);
                }
               
            }
           
        }

        public static void PrintParticipant(Participant participant, int round, string filling)
        {
            if (participant.Left != null)
                PrintParticipant(participant.Left, round - 1, _rightUpperCorner);

            PrintLine(participant, round, filling);

            if (participant.Right != null)
                PrintParticipant(participant.Right, round - 1, _rightLowerCorner);
        }

        private static void PrintLine(Participant participant, int round, string filling)
        {
            Console.CursorLeft = _left;

            for (int i = _maximumRound-1; i > round ; i--)
            {
                if (_roundParticipants[i] % 2 == 1)
                    Console.Write("{0,-10}", _verticalStick);
                else
                    Console.Write("{0,-10}", "");
            }        

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("{0,-2}", filling);

            if (participant.Winner != null && participant.Name.Equals(participant.Winner.Name))
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.Write("{0,-8}", participant.Name);
            
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = round-1; i >= 0; i--)
            {
                if (_roundParticipants[i] % 2 == 1)
                    Console.Write("{0,-10}", _verticalStick);
                else
                    Console.Write("{0,-10}", "");
            }
            _roundParticipants[round]++;
            Console.WriteLine();

        }

    }
}
