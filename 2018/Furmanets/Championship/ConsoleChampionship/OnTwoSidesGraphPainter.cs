using System;
using Championship;

namespace ConsoleChampionship
{
    internal class OnTwoSidesGraphPainter:GraphPainter
    {
        public override void PaintGraph(Tournament tournament)
        {
            var nextCursorPositionTop = 3;
            var nextCursorPositionLeft = 0;
            var nextDistanceBeewinPalyers = 2;
            var tournamentRounds = tournament.GetTournamentToPrint();

            foreach (var round in tournamentRounds)
            {
                var maxLengthName = GetMaxLengthNameInRound(round);

                Console.SetCursorPosition(nextCursorPositionLeft, 0);
                Console.WriteLine(GetForPrintRound(round.Stage));

                var distanceBetweenPlayers = nextDistanceBeewinPalyers;
                var positionCursorLeft = nextCursorPositionLeft;
                var positionCursorTop = nextCursorPositionTop;
                var isFirstLine = true;
                var isSecondLine = false;

                for (var j = 0; j < round.Meetings.Count/2; j++)
                {
                    var meeting = round.Meetings[j];
                    if (meeting.FirstPlayer == null && meeting.SecondPlayer == null &&
                        round.Equals(tournamentRounds[0]))
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
            WriteNameWinnerInFinalRound(tournamentRounds);
        }
    }
}
