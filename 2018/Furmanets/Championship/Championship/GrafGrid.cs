using System.Collections.Generic;
using ConsoleChampionship;

namespace Championship
{
    public class GrafGrid
    {
        public List<string> Graf = new List<string>();
        private readonly int _maxLengthName;

        public GrafGrid(int maxLengthName)
        {
            _maxLengthName = maxLengthName;
        }

        public void ConstructionGraf(Tournament tournament)
        {
            var grid = tournament.Grid;
            var distanceBetweenStage = 7;

            for (var i = 0; i < grid.Tournament[0].Meetings.Count * 6; i++)
            {
                Graf.Add(new string(' ', 2));
            }

            var previousLines = new List<int>();
            var isFirsRound = true;

            foreach (var round in grid.Tournament)
            {
                Graf[0] += new string(' ', _maxLengthName - 4) + GetForPrintRound(round.Stage) + new string(' ', distanceBetweenStage);

                if (isFirsRound)
                {
                    CreateFirstRound(round.Meetings, previousLines);
                    isFirsRound = false;
                }
                else
                {
                    previousLines = CreateOtherRounds(round, previousLines);
                }
            }
            Graf[previousLines[0]] += tournament.PlayerWinner;

            Draftsman.CongratulationWinner(tournament.PlayerWinner);
        }

        public static string GetForPrintRound(int round)
        {
            if (round == 1)
            {
                return "final";
            }

            return "1/" + round;
        }

        private void CreateFirstRound(List<Meeting> meetings, List<int> previousLines)
        {
            var meetingNumber = 3;

            foreach (var meeting in meetings)
            {
                if (meeting.FirstPlayer == null && meeting.SecondPlayer == null)
                {
                    previousLines.Add(meetingNumber + 1);
                    meetingNumber += 5;
                    continue;
                }

                IdentificationMeeting(meeting, out var nameLengthFirstPlayer, out var nameLengthSecondPlayer,
                    out var goalsFisrtPlayer, out var goalsSecondPlayer);

                Graf[meetingNumber] +=
                    new string(' ', _maxLengthName - nameLengthFirstPlayer) + meeting.FirstPlayer + goalsFisrtPlayer;

                Graf[meetingNumber + 1] += new string(' ', Graf[meetingNumber].Length - Graf[meetingNumber + 1].Length) + "|----";
                previousLines.Add(meetingNumber + 1);
                Graf[meetingNumber + 2] +=
                    new string(' ', _maxLengthName - nameLengthSecondPlayer) + meeting.SecondPlayer + goalsSecondPlayer;

                meetingNumber += 5;
            }
        }

        private void IdentificationMeeting(Meeting meeting, out int nameLengthFirstPlayer, out int nameLengthSecondPlayer,
            out string goalsFisrtPlayer, out string goalsSecondPlayer)
        {
            nameLengthFirstPlayer = 0;
            nameLengthSecondPlayer = 0;
            goalsFisrtPlayer = "";
            goalsSecondPlayer = "";

            if (meeting.Score[0] != 0 || meeting.Score[1] != 0)
            {
                goalsFisrtPlayer += " " + meeting.Score[0];
                goalsSecondPlayer += " " + meeting.Score[1];
            }

            if (meeting.FirstPlayer != null)
            {
                nameLengthFirstPlayer = meeting.FirstPlayer.Length;
            }

            if (meeting.SecondPlayer != null)
            {
                nameLengthSecondPlayer = meeting.SecondPlayer.Length;
            }
        }

        private List<int> CreateOtherRounds(Round rounds, List<int> previousLines)
        {
            var meetings = rounds.Meetings;
            var indexPreviousLine = 0;
            var newPreviousLines = new List<int>();
            var nameFinish = Graf[previousLines[0]].Length + _maxLengthName;

            foreach (var meeting in meetings)
            {
                IdentificationMeeting(meeting, out var nameLengthFirstPlayer, out var nameLengthSecondPlayer,
                    out var goalsFisrtPlayer, out var goalsSecondPlayer);

                Graf[previousLines[indexPreviousLine]] +=
                    new string(' ', nameFinish - nameLengthFirstPlayer - Graf[previousLines[indexPreviousLine]].Length)
                    + meeting.FirstPlayer + goalsFisrtPlayer;

                Graf[previousLines[indexPreviousLine + 1]] +=
                    new string(' ', nameFinish - nameLengthSecondPlayer - Graf[previousLines[indexPreviousLine + 1]].Length)
                    + meeting.SecondPlayer + goalsSecondPlayer;

                for (int i = previousLines[indexPreviousLine] + 1; i < previousLines[indexPreviousLine + 1]; i++)
                {
                    Graf[i] += new string(' ', Graf[previousLines[indexPreviousLine]].Length - Graf[i].Length) + "|";
                }

                var middleBetweenPlayers = previousLines[indexPreviousLine] +
                                           (previousLines[indexPreviousLine + 1] - previousLines[indexPreviousLine]) /
                                           2;

                Graf[middleBetweenPlayers] += "-----";
                newPreviousLines.Add(middleBetweenPlayers);

                indexPreviousLine += 2;
            }

            while (Graf[Graf.Count - 1] == "  ")
            {
                Graf.RemoveAt(Graf.Count - 1);
            }
            return newPreviousLines;
        }

    }
}


