using System;
using Championship;

namespace ConsoleChampionship
{
    internal class HorisontalGraphPainter : GraphPainter
    {
        public override void PaintGraph(Tournament tournament)
        {
            var nextCursorPositionTop = 3;
            var nextCursorPositionLeft = 0;
            var nextDistanceBeewinPalyers = 2;

            foreach (var round in tournament.TournamentRounds)
            {
                var maxLengthName = GetMaxLengthNameInRound(round);

                Console.SetCursorPosition(nextCursorPositionLeft, 0);
                Console.WriteLine(GetForPrintRound(round.Stage));

                var distanceBetweenPlayers = nextDistanceBeewinPalyers;
                var positionCursorLeft = nextCursorPositionLeft;
                var positionCursorTop = nextCursorPositionTop;
                var isFirstLine = true;
                var isSecondLine = false;

                foreach (var meeting in round.Meetings)
                {
                    if (meeting.FirstPlayer == null && meeting.SecondPlayer == null && round.Equals(tournament.TournamentRounds[0]))
                    {
                        continue;
                    }

                    Console.SetCursorPosition(positionCursorLeft, positionCursorTop);

                    WriteNamePlayer(meeting, true);

                    var wereDrowLine = distanceBetweenPlayers / 2;

                    for (var i = 1; i < distanceBetweenPlayers; i++)
                    {
                        Console.SetCursorPosition(positionCursorLeft + maxLengthName, positionCursorTop + i);
                        Console.Write("|");

                        if (i == wereDrowLine)
                        {
                            Console.Write("-----");
                            if (isSecondLine)
                            {
                                isSecondLine = false;
                                nextDistanceBeewinPalyers = Console.CursorTop - nextDistanceBeewinPalyers;
                            }

                            if (isFirstLine)
                            {
                                nextCursorPositionLeft = Console.CursorLeft;
                                nextCursorPositionTop = Console.CursorTop;
                                isFirstLine = false;
                                isSecondLine = true;
                                nextDistanceBeewinPalyers = nextCursorPositionTop;
                            }
                        }

                    }
                    positionCursorTop += distanceBetweenPlayers;
                    Console.SetCursorPosition(positionCursorLeft, positionCursorTop);
                    WriteNamePlayer(meeting, false);

                    positionCursorTop += distanceBetweenPlayers;
                }

            }
            Console.SetCursorPosition(nextCursorPositionLeft, nextCursorPositionTop);
            WriteNameWinnerInFinalRound(tournament);
        }
    }
}