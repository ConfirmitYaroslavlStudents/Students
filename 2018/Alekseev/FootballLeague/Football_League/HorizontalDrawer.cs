using System.Collections.Generic;
using System.Linq;

namespace Football_League
{
    public class HorizontalDrawer
    {
        private readonly List<string> _horizontalGrid = new List<string>();

        public int AllLinesCurrentPosition;
        public void MakeHorintalGrid(FullGrid grid)
        {
            foreach (MatchTreeGrid gridTree in grid.Grid)
            {
                var result = MakeGridTree(gridTree);
                if (_horizontalGrid.Count != 0)
                    _horizontalGrid.Add("");
                foreach (var str in result)
                {
                    _horizontalGrid.Add(str);
                }
            }
        }

        public List<string> MakeGridTree(MatchTreeGrid grid)
        {
            List<string> result = new List<string>();
            List<int> pointersFromLastRound = new List<int>();
            int currentGridFix = 0;
            Match currentRoundFirstMatch = grid.StartMatch;
            int maxLineLength = 0;

            while (currentRoundFirstMatch?.PlayerOne != null)
            {
                List<int> pointersInThisRound = new List<int>();
                int currentPlayerNumber = 2;
                int currentGridLine = 0;
                bool isFirstRound = pointersFromLastRound.Count == 0;
                Match currentMatch = currentRoundFirstMatch;

                while (pointersFromLastRound.Count > 0 && currentMatch?.PlayerOne != null)
                {
                    if (currentGridLine == pointersFromLastRound[0])
                    {
                        if (currentPlayerNumber == 2)
                        {
                            result[currentGridLine] += currentMatch.PlayerOne.Name;
                            currentPlayerNumber = 1;
                            if (currentMatch.PlayerTwo == null)
                                currentMatch = currentMatch.NextMatch;
                        }
                        else
                        {
                            result[currentGridLine] += currentMatch.PlayerTwo.Name;
                            currentPlayerNumber = 2;
                            currentMatch = currentMatch.NextMatch;
                        }

                        if (result[currentGridLine].Length > maxLineLength)
                            maxLineLength = result[currentGridLine].Length;
                        pointersInThisRound.Add(currentGridLine);
                        pointersFromLastRound.RemoveAt(0);
                    }
                    currentGridLine++;
                }

                while (currentMatch?.PlayerOne != null)
                {
                    string fixLine = "";
                    for (int i = 0; i < maxLineLength - 1; i++)
                        fixLine += " ";
                    result.Add(fixLine + " ");
                    currentGridLine++;

                    if (currentPlayerNumber == 2)
                    {
                        result.Add(fixLine + currentMatch.PlayerOne.Name);
                        if (maxLineLength < currentMatch.PlayerOne.Name.Length)
                            maxLineLength = currentMatch.PlayerOne.Name.Length;
                        currentPlayerNumber = 1;
                        if (currentMatch.PlayerTwo == null)
                            currentMatch = currentMatch.NextMatch;
                    }
                    else
                    {
                        result.Add(fixLine + currentMatch.PlayerTwo.Name);
                        if (maxLineLength < currentMatch.PlayerTwo.Name.Length)
                            maxLineLength = currentMatch.PlayerTwo.Name.Length;
                        currentPlayerNumber = 2;
                        currentMatch = currentMatch.NextMatch;
                    }

                    pointersInThisRound.Add(result.Count - 1);
                    currentGridLine++;
                }

                int positionInThisRoundPointers = 0;
                for (int i = 0; i < result.Count; i++)
                {
                    if(i == pointersInThisRound[positionInThisRoundPointers])
                        while (result[i].Length < maxLineLength)
                            result[i] += "-";
                    else
                        while (result[i].Length < maxLineLength)
                            result[i] += " ";
                }

                while (pointersInThisRound.Count >= 2)
                {
                    int firstMatchPosition = pointersInThisRound[0];
                    pointersInThisRound.RemoveAt(0);
                    int secondMatchPosition = pointersInThisRound[0];
                    pointersInThisRound.RemoveAt(0);
                    int matchWinnerPosition = (firstMatchPosition + secondMatchPosition) / 2;

                    result[firstMatchPosition] += "|";
                    result[secondMatchPosition] += "|";

                    for(int i = firstMatchPosition + 1; i < secondMatchPosition; i++)
                        if (i != matchWinnerPosition)
                            result[i] += "|";
                        else
                            result[i] += "--";

                    pointersFromLastRound.Add(matchWinnerPosition);
                }

                if (pointersInThisRound.Count == 1)
                {
                    result[pointersInThisRound[0]] += "--";
                    pointersFromLastRound.Add(pointersInThisRound[0]);
                    pointersInThisRound.RemoveAt(0);
                }

                maxLineLength = result.Select(line => line.Length).Concat(new[] {maxLineLength}).Max();

                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].Length < maxLineLength)
                        result[i] += " ";
                }

                if (isFirstRound)
                {
                    currentGridFix = maxLineLength;
                }
                currentRoundFirstMatch = currentRoundFirstMatch.NextRoundMatch;
            }

            string fixLines = "";
            while (fixLines.Length < AllLinesCurrentPosition)
                fixLines += " ";
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = fixLines + result[i];
            }

            AllLinesCurrentPosition += currentGridFix;
            return result;
        }

        public void PrintGrid()
        {
            ConsoleWorker.PrintHorizontalGrid(_horizontalGrid);
        }
    }
}
