using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament;

namespace ConsoleTournament
{
    public static class HorizontalBracket
    {

        private const string _leftUpperCorner = "\u2500\u2510";
        private const string _rightUpperCorner = "\u250C\u2500";
        private const string _leftLowerCorner = "\u2500\u2518";
        private const string _rightLowerCorner = "\u2514\u2500";
        private const string _verticalStick = "\u2502";
        private const string _horizontalStick = "\u005f";
        private static int[] _roundParticipants;
        private static int _maximumRound;

        public static void Print(List<Participant> bracket, int round)
        {
            _roundParticipants = new int[round + 1];
            _maximumRound = round + 1;
            if (bracket.Count % 2 == 1 && bracket.Count != 1)
                bracket.RemoveAt(bracket.Count - 1);
            for (int i = 0; i < bracket.Count; i++)
            {
                if (bracket.Count == 1)
                {
                    _maximumRound--;
                    PrintParticipant(bracket[i], round, "");
                }
                else if (i % 2 == 0)
                    PrintParticipant(bracket[i], round, _leftUpperCorner);
                else
                    PrintParticipant(bracket[i], round, _leftLowerCorner);
            }
        }

        private static void PrintParticipant(Participant participant, int round, string filling)
        {
            if (participant.Left != null)
                PrintParticipant(participant.Left, round - 1, _leftUpperCorner);

            PrintLine(participant, round, filling);

            if (participant.Right != null)
                PrintParticipant(participant.Right, round - 1, _leftLowerCorner);
        }

        private static void PrintLine(Participant participant, int round, string filling)
        {
            for (int i = 0; i < round; i++)
            {
                if (_roundParticipants[i] % 2 == 1)
                    Console.Write("{0,10}", _verticalStick);
                else
                    Console.Write("{0,10}", "");
            }
            if (participant.Winner != null && participant.Name.Equals(participant.Winner.Name))
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.White;
            Console.Write("{0,8}", participant.Name);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("{0,2}", filling);
            _roundParticipants[round]++;
            for (int i = round + 1; i < _maximumRound; i++)
            {
                if (_roundParticipants[i] % 2 == 1)
                    Console.Write("{0,10}", _verticalStick);
                else
                    Console.Write("{0,10}", "");
            }
            Console.WriteLine();
        }

    }
}
