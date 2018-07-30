using System;
using Championship;

namespace ConsoleChampionship
{
    internal class GraphPainter
    {
        public static void PaintGraphHorisontal(Tournament tournament)
        {
            var previousCursorPositionTop = 3;
            var previousCursorPositionLeft = 0;
            var nextDistanceBeewinPalyers = 2;

            foreach (var round in tournament.TournamentRounds)
            {
                var maxLengthName = GetMaxLengthNameInRound(round);

                Console.SetCursorPosition(previousCursorPositionLeft, 0);
                Console.WriteLine(GetForPrintRound(round.Stage));

                var distanceBetweenPlayers = nextDistanceBeewinPalyers;
                var positionCursorLeft = previousCursorPositionLeft;
                var positionCursorTop = previousCursorPositionTop;
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
                                previousCursorPositionLeft = Console.CursorLeft;
                                previousCursorPositionTop = Console.CursorTop;
                                isFirstLine = false;
                                isSecondLine = true;
                                nextDistanceBeewinPalyers = previousCursorPositionTop;
                            }
                        }

                    }
                    positionCursorTop += distanceBetweenPlayers;
                    Console.SetCursorPosition(positionCursorLeft, positionCursorTop);
                    WriteNamePlayer(meeting, false);

                    positionCursorTop += distanceBetweenPlayers;
                }

            }
            Console.SetCursorPosition(previousCursorPositionLeft, previousCursorPositionTop);
            WriteNameWinnerInFinalRound(tournament);
        }

        private static void WriteNamePlayer(Meeting meeting, bool isFirst)
        {
            if (isFirst)
            {
                switch (meeting.Winner)
                {
                    case MeetingWinningIndicator.MatchDidNotTakePlace:
                        Console.Write(meeting.FirstPlayer);
                        break;
                    case MeetingWinningIndicator.FirstPlayer:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(meeting.FirstPlayer + " " + meeting.Score[0]);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case MeetingWinningIndicator.SecondPlayer:
                        Console.Write(meeting.FirstPlayer + " " + meeting.Score[0]);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return;
            }

            switch (meeting.Winner)
            {
                case MeetingWinningIndicator.MatchDidNotTakePlace:
                    Console.Write(meeting.SecondPlayer);
                    break;
                case MeetingWinningIndicator.SecondPlayer:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(meeting.SecondPlayer + " " + meeting.Score[1]);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case MeetingWinningIndicator.FirstPlayer:
                    Console.Write(meeting.SecondPlayer + " " + meeting.Score[1]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void WriteNameWinnerInFinalRound(Tournament tournament)
        {
            var finalRound = tournament.TournamentRounds[tournament.TournamentRounds.Count - 1];

            switch (finalRound.Meetings[0].Winner)
            {
                case MeetingWinningIndicator.FirstPlayer:
                    Console.WriteLine(finalRound.Meetings[0].FirstPlayer);
                    break;
                case MeetingWinningIndicator.SecondPlayer:
                    Console.WriteLine(finalRound.Meetings[0].SecondPlayer);
                    break;
                case MeetingWinningIndicator.MatchDidNotTakePlace:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static int GetMaxLengthNameInRound(Round round)
        {
            var maxLengthName = 0;

            foreach (var meeting in round.Meetings)
            {
                if (meeting.FirstPlayer != null && meeting.FirstPlayer.Length > maxLengthName)
                {
                    maxLengthName = meeting.FirstPlayer.Length;
                }

                if (meeting.SecondPlayer != null && meeting.SecondPlayer.Length > maxLengthName)
                {
                    maxLengthName = meeting.SecondPlayer.Length;
                }
            }
            return maxLengthName;
        }

        private static string GetForPrintRound(int round)
        {
            if (round == 1)
            {
                return "final";
            }

            return "1/" + round;
        }
    }
}
