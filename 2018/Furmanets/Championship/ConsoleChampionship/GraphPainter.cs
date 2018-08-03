using System;
using Championship;

namespace ConsoleChampionship
{
    abstract class GraphPainter
    {
        public abstract void PaintGraph(Tournament tournament);
        
        public virtual void WriteNamePlayer(Meeting meeting, bool isFirst)
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

        public void WriteNameWinnerInFinalRound(Tournament tournament)
        {
            var tournamentGrid = tournament.GetTournamentToPrint();
            var finalRound = tournamentGrid[tournamentGrid.Count - 1];

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

        public int GetMaxLengthNameInRound(Round round)
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

        public string GetForPrintRound(int round)
        {
            if (round == 1)
            {
                return "final";
            }

            return "1/" + round;
        }
    }
}
