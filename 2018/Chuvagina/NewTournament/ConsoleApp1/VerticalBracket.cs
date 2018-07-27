using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament;

namespace ConsoleTournament
{
    public static class VerticalBracket
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
            Console.Clear();
            for (int i = 0; i < bracket.Count; i++)
            {
                PrintVertical(bracket[i], round);
            }
            Console.WriteLine();

        }

        private static void PrintVertical(Participant participant, int round)
        {
            if (participant.Left != null)
                PrintVertical(participant.Left, round - 1);

            if (participant.Right != null)
                PrintVertical(participant.Right, round - 1);
            string format = "{0," + 3 + "}";
            Console.CursorTop = round * 2;
            Console.Write(format, participant.Name);
        }


    }
}
