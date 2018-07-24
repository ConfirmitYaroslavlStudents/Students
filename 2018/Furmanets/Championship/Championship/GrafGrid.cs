using System;
using System.Collections.Generic;
using System.Text;

namespace Championship
{
    class GrafGrid
    {
        public List<string> Graf = new List<string>();
        private int maxLengthName = 10;

        public void ConstructionGraf(TournamentGrid grid)
        {
            var distanceBetweenStage = 7;
            for (var i = 0; i < grid.Tournament[0].Meetings.Count * 6; i++)
            {
                Graf.Add(new string(' ', 2));
            }
            var previousLines = new List<int>();
            var flag = true;
            foreach (var round in grid.Tournament)
            {
                Graf[0] += new string(' ', distanceBetweenStage) + Draftsman.GetForPrintRound(round.Stage) + new string(' ', 8);
                if (flag)
                {
                    CreateFirstRound(round.Meetings, previousLines);
                    flag = false;
                }
                else
                {
                    previousLines = CreateOtherRounds(round, previousLines);
                }
            }
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
                var indentationFirstPlayer = 0;
                var indentationSecondPlayer = 0;
                var goalsFisrtPlayer = "";
                var goalsSecondPlayer = "";

                if (meeting.Score[0] != 0 || meeting.Score[1] != 0)
                {
                    goalsFisrtPlayer += " " + meeting.Score[0];
                    goalsSecondPlayer += " " + meeting.Score[1];
                }

                if (meeting.FirstPlayer != null)
                {
                    indentationFirstPlayer = meeting.FirstPlayer.Length;
                }

                if (meeting.SecondPlayer != null)
                {
                    indentationSecondPlayer = meeting.SecondPlayer.Length;
                }

                Graf[meetingNumber] +=
                    new string(' ', maxLengthName - indentationFirstPlayer) + meeting.FirstPlayer + goalsFisrtPlayer;

                Graf[meetingNumber + 1] += new string(' ', Graf[meetingNumber].Length - Graf[meetingNumber + 1].Length) + "|----";
                previousLines.Add(meetingNumber + 1);
                Graf[meetingNumber + 2] +=
                    new string(' ', maxLengthName - indentationSecondPlayer) + meeting.SecondPlayer + goalsSecondPlayer;

                meetingNumber += 5;
            }
        }

        private List<int> CreateOtherRounds(Round rounds, List<int> previousLines)
        {
            var meetings = rounds.Meetings;
            var indexPreviousLine = 0;
            var newPreviousLines = new List<int>();
            var nameFinish = Graf[previousLines[0]].Length + maxLengthName;

            foreach (var meeting in meetings)
            {
                var characterCountFirstPlayer = 0;
                var characterCountSecondPlayer = 0;

                if (meeting.FirstPlayer != null)
                {
                    characterCountFirstPlayer = meeting.FirstPlayer.Length;
                }
                if (meeting.SecondPlayer != null)
                {
                    characterCountSecondPlayer = meeting.SecondPlayer.Length;
                }

                Graf[previousLines[indexPreviousLine]] +=
                    new string(' ', nameFinish - characterCountFirstPlayer - Graf[previousLines[indexPreviousLine]].Length) + meeting.FirstPlayer;

                Graf[previousLines[indexPreviousLine + 1]] +=
                    new string(' ', nameFinish - characterCountSecondPlayer - Graf[previousLines[indexPreviousLine + 1]].Length) + meeting.SecondPlayer;

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


