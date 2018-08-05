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

                foreach (var meeting in round.Meetings)
                {
                    var isEmptyMeetingInFirstRound = meeting.FirstPlayer == null
                                                     && meeting.SecondPlayer == null
                                         && round.Equals(tournamentRounds[0]);

                    Console.SetCursorPosition(positionCursorLeft, positionCursorTop);

                    WriteNamePlayer(meeting, true);

                    var indexForDrowLine = distanceBetweenPlayers / 2;

                    for (var i = 1; i < distanceBetweenPlayers; i++)
                    {
                        Console.SetCursorPosition(positionCursorLeft + maxLengthName, positionCursorTop + i);

                        if (!isEmptyMeetingInFirstRound)
                        {
                            Console.Write("|");
                        }

                        if (i == indexForDrowLine)
                        {
                            if (!isEmptyMeetingInFirstRound)
                            {
                                Console.Write("-----");
                            }

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