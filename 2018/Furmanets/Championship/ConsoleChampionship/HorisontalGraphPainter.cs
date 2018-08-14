using System;
using System.Collections.Generic;
using Championship;

namespace ConsoleChampionship
{
    internal class HorisontalGraphPainter : GraphPainter
    {
        private int _upperGridWinnerLeft;
        private int _upperGridWinnerTop;
        private int _lowerGridWinnerLeft;
        private int _lowerGridWinnerTop;
        public override void PaintGraph(Tournament tournament)
        {
            var nextCursorPositionTop = 3;
            var tournamentRounds = tournament.GetTournamentToPrint();
            var positionDownCoursor = 0;

            positionDownCoursor = PrintTournirGrid(tournamentRounds, nextCursorPositionTop);

            nextCursorPositionTop = positionDownCoursor;

            if (tournament is DoubleEliminationTournament doubleTournament)
            {
                var lowerRounds = doubleTournament.GetLowerGrid();
                PrintTournirGrid(lowerRounds, nextCursorPositionTop);
                PaintGrandFinal(doubleTournament.FinalUpperAndLowerGrids);
            }

        }

        private void PaintGrandFinal(Meeting finalUpperAndLowerGrids)
        {
            var indexForWriteLine = (_lowerGridWinnerTop - _upperGridWinnerTop) / 2;

            Console.SetCursorPosition(_upperGridWinnerLeft, _upperGridWinnerTop);
            while (_upperGridWinnerLeft != _lowerGridWinnerLeft)
            {
                Console.Write("-");
                _upperGridWinnerLeft++;
            }
            for (int i = _upperGridWinnerTop; i < _lowerGridWinnerTop-1; i++)
            {
                Console.SetCursorPosition(_upperGridWinnerLeft, i + 1);
                Console.Write("|");

                if (_upperGridWinnerTop + indexForWriteLine == i)
                {
                    Console.Write("-----");
                    if (finalUpperAndLowerGrids.Winner == MeetingWinningIndicator.FirstPlayer)
                    {
                        WriteNamePlayer(finalUpperAndLowerGrids, true);
                    }

                    if (finalUpperAndLowerGrids.Winner == MeetingWinningIndicator.SecondPlayer)
                    {
                        WriteNamePlayer(finalUpperAndLowerGrids, false);
                    }
                }
            }
        }

        private int PrintTournirGrid(List<Round> tournamentRounds, int cursorPositionTop)
        {
            var nextCursorPositionLeft = 0;
            var nextDistanceBeewinPalyers = 2;
            var nextCursorPositionTop = cursorPositionTop;

            foreach (var round in tournamentRounds)
            {
                var maxLengthName = GetMaxLengthNameInRound(round);

                Console.SetCursorPosition(nextCursorPositionLeft, 0);
                Console.WriteLine(GetForPrintRound(round.Stage));

                var distanceBetweenPlayers = nextDistanceBeewinPalyers;
                var positionCursorLeft = nextCursorPositionLeft;
                var positionCursorTop = nextCursorPositionTop;

                for (var i = 0; i < round.Meetings.Count; i++)
                {
                    var meeting = round.Meetings[i];
                    var isEmptyMeetingInFirstRound = meeting.FirstPlayer == null
                                                     && meeting.SecondPlayer == null
                                                     && round.Equals(tournamentRounds[0]) && cursorPositionTop == 0;

                    Console.SetCursorPosition(positionCursorLeft, positionCursorTop);

                    WriteNamePlayer(meeting, true);

                    var indexForDrowLine = distanceBetweenPlayers / 2;

                    for (var j = 1; j < distanceBetweenPlayers; j++)
                    {
                        Console.SetCursorPosition(positionCursorLeft + maxLengthName, positionCursorTop + j);

                        if (!isEmptyMeetingInFirstRound)
                        {
                            Console.Write("|");
                        }

                        if (j == indexForDrowLine)
                        {
                            if (!isEmptyMeetingInFirstRound)
                            {
                                Console.Write("-----");
                            }

                            if (i == 0)
                            {
                                nextCursorPositionLeft = Console.CursorLeft;
                                nextCursorPositionTop = Console.CursorTop;
                                nextDistanceBeewinPalyers *= 2;
                            }
                        }
                    }

                    positionCursorTop += distanceBetweenPlayers;
                    Console.SetCursorPosition(positionCursorLeft, positionCursorTop);
                    WriteNamePlayer(meeting, false);

                    positionCursorTop += distanceBetweenPlayers;
                    if (i == 0)
                    {
                        cursorPositionTop = Console.CursorTop + 10;
                    }
                }
            }

            Console.SetCursorPosition(nextCursorPositionLeft, nextCursorPositionTop);
            WriteNameWinnerInFinalRound(tournamentRounds);

            if (_upperGridWinnerTop == 0)
            {
                _upperGridWinnerTop = Console.CursorTop;
                _upperGridWinnerLeft = Console.CursorLeft;
            }
            else
            {
                _lowerGridWinnerLeft = Console.CursorLeft;
                _lowerGridWinnerTop = Console.CursorTop;
            }

            return cursorPositionTop;
        }
    }
}