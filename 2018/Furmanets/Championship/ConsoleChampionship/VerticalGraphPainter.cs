using System;
using Championship;

namespace ConsoleChampionship
{
    internal class VerticalGraphPainter : GraphPainter
    {
        public override void PaintGraph(Tournament tournament)
        {
            var nextCursorPositionTop = 0;
            var nextCursorPositionLeft = 10;
            var nextDistanceBeetwinPalyers = 3;
            var isFirstRound = true;
            var tournamentRounds = tournament.GetTournamentToPrint();

            var horisontalSizeWindow = GetMaxLengthNameInRound(tournamentRounds[0]);
            horisontalSizeWindow *= tournamentRounds[0].Meetings.Count * nextDistanceBeetwinPalyers;
            horisontalSizeWindow += nextCursorPositionLeft * 2;

            Console.SetWindowSize(horisontalSizeWindow, 30);

            foreach (var round in tournamentRounds)
            {
                var maxLengthNameInRound = GetMaxLengthNameInRound(round);

                Console.SetCursorPosition(0, nextCursorPositionTop);
                Console.WriteLine(GetForPrintRound(round.Stage));

                var distanceBetweenPlayers = nextDistanceBeetwinPalyers;
                var positionCursorLeft = nextCursorPositionLeft;
                var positionCursorTop = nextCursorPositionTop;

                for (var j = 0; j < round.Meetings.Count; j++)
                {
                    var meeting = round.Meetings[j];

                    var isEmptyMeetingInFirstRound = meeting.FirstPlayer == null
                                         && meeting.SecondPlayer == null
                                         && round.Equals(tournamentRounds[0]);

                    if (isFirstRound)
                    {
                        positionCursorLeft += maxLengthNameInRound;
                    }

                    var lengthNameFirstPlayer = 0;
                    if (meeting.FirstPlayer != null)
                    {
                        lengthNameFirstPlayer = meeting.FirstPlayer.Length;
                    }

                    Console.SetCursorPosition(positionCursorLeft - lengthNameFirstPlayer + 1, positionCursorTop);
                    WriteNamePlayer(meeting, true);

                    positionCursorLeft += distanceBetweenPlayers;

                    if (!isEmptyMeetingInFirstRound)
                    {
                        for (var i = 0; i < distanceBetweenPlayers - 1; i++)
                        {
                            Console.Write("-");
                        }
                    }

                    var lineForNextStage = Console.CursorLeft - distanceBetweenPlayers / 2 - 1;
                    for (var i = 1; i < 5; i++)
                    {
                        Console.SetCursorPosition(lineForNextStage, positionCursorTop + i);

                        if (!isEmptyMeetingInFirstRound)
                        {
                            Console.Write("|");
                        }
                    }

                    switch (j)
                    {
                        case 1:
                            nextDistanceBeetwinPalyers = Console.CursorLeft - 1 - nextCursorPositionLeft;
                            break;
                        case 0:
                            nextCursorPositionLeft = Console.CursorLeft - 1;
                            break;
                    }

                    nextCursorPositionTop = Console.CursorTop;
                    Console.SetCursorPosition(positionCursorLeft, positionCursorTop);

                    WriteNamePlayer(meeting, false);
                    if (isFirstRound)
                    {
                        positionCursorLeft += maxLengthNameInRound;
                    }
                    positionCursorLeft += distanceBetweenPlayers;
                }
                isFirstRound = false;
            }

            var lastMeeting = tournamentRounds[tournamentRounds.Count - 1].Meetings[0];

            switch (lastMeeting.Winner)
            {
                case MeetingWinningIndicator.SecondPlayer:
                    nextCursorPositionLeft -= lastMeeting.SecondPlayer.Length / 2 - 1;
                    break;
                case MeetingWinningIndicator.FirstPlayer:
                    nextCursorPositionLeft -= lastMeeting.FirstPlayer.Length / 2 - 1;
                    break;
                case MeetingWinningIndicator.MatchDidNotTakePlace:
                    break;
            }

            Console.SetCursorPosition(nextCursorPositionLeft, nextCursorPositionTop);
            WriteNameWinnerInFinalRound(tournamentRounds);
        }

        public override void WriteNamePlayer(Meeting meeting, bool isFirst)
        {
            if (isFirst)
            {
                switch (meeting.Winner)
                {
                    case MeetingWinningIndicator.MatchDidNotTakePlace:
                        Console.Write(meeting.FirstPlayer);
                        break;
                    case MeetingWinningIndicator.FirstPlayer:
                        WriteName(meeting.FirstPlayer, meeting.Score[0]);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case MeetingWinningIndicator.SecondPlayer:
                        WriteName(meeting.FirstPlayer, meeting.Score[0]);
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
                    WriteName(meeting.SecondPlayer, meeting.Score[1]);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case MeetingWinningIndicator.FirstPlayer:
                    WriteName(meeting.SecondPlayer, meeting.Score[1]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void WriteName(string name, int score)
        {
            Console.Write(name);
            Console.CursorLeft--;
            Console.CursorTop++;
            Console.Write(score);
            Console.CursorTop--;
        }
    }
}
