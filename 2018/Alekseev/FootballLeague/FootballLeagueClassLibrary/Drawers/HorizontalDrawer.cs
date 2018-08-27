using System.Collections.Generic;
using System.Linq;
using FootballLeagueClassLibrary.Structure;

namespace FootballLeagueClassLibrary.Drawers
{
    public class HorizontalDrawer
    {
        public readonly List<string> HorizontalGrid = new List<string>();

        public int AllLinesCurrentPosition;
        public void MakeHorintalGrid(FullGrid grid)
        {
            foreach (MatchTreeGrid gridTree in grid.Grid)
            {
                var result = MakeGridTree(gridTree);
                if (HorizontalGrid.Count != 0)
                    HorizontalGrid.Add("");
                foreach (var str in result)
                {
                    HorizontalGrid.Add(str);
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

                currentMatch = AddPreviousRoundWinners(pointersFromLastRound, currentMatch, result, pointersInThisRound, ref currentGridLine, ref currentPlayerNumber, ref maxLineLength);

                int currentFixLength = maxLineLength;

                AddLosersFromNextTree(currentMatch, currentFixLength, result, currentGridLine, currentPlayerNumber, ref maxLineLength, pointersInThisRound);

                AlignNames(result, pointersInThisRound, maxLineLength);

                AddWinners(pointersInThisRound, result, pointersFromLastRound);

                maxLineLength = result.Select(line => line.Length).Concat(new[] { maxLineLength }).Max();
                AlignLinesToMaxLength(result, maxLineLength);

                currentGridFix = FirstRoundGridFixValue(currentGridFix, maxLineLength, isFirstRound);
                currentRoundFirstMatch = currentRoundFirstMatch.NextRoundMatch;
            }

            AddSpacesForNewTree(result);

            AllLinesCurrentPosition += currentGridFix;
            return result;
        }

        private static void AlignLinesToMaxLength(List<string> result, int maxLineLength)
        {
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Length < maxLineLength)
                    result[i] += " ";
            }
        }

        private static int FirstRoundGridFixValue(int currentGridFix, int maxLineLength, bool isFirstRound)
        {
            if (isFirstRound)
            {
                currentGridFix = maxLineLength;
            }

            return currentGridFix;
        }

        private void AddSpacesForNewTree(List<string> result)
        {
            string fixLines = "";
            while (fixLines.Length < AllLinesCurrentPosition)
                fixLines += " ";
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = fixLines + result[i];
            }
        }

        private static void AddWinners(List<int> pointersInThisRound, List<string> result, List<int> pointersFromLastRound)
        {
            while (pointersInThisRound.Count >= 2)
            {
                int firstMatchPosition = pointersInThisRound[0];
                pointersInThisRound.RemoveAt(0);
                int secondMatchPosition = pointersInThisRound[0];
                pointersInThisRound.RemoveAt(0);
                int matchWinnerPosition = (firstMatchPosition + secondMatchPosition) / 2;

                result[firstMatchPosition] += "|";
                result[secondMatchPosition] += "|";

                for (int i = firstMatchPosition + 1; i < secondMatchPosition; i++)
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
        }

        private static void AlignNames(List<string> result, List<int> pointersInThisRound, int maxLineLength)
        {
            int positionInThisRoundPointers = 0;
            for (int i = 0; i < result.Count; i++)
            {
                if (positionInThisRoundPointers < pointersInThisRound.Count &&
                    i == pointersInThisRound[positionInThisRoundPointers])
                {
                    while (result[i].Length < maxLineLength)
                        result[i] += "-";
                    positionInThisRoundPointers++;
                }
                else
                    while (result[i].Length < maxLineLength)
                        result[i] += " ";
            }
        }

        private static void AddLosersFromNextTree(Match currentMatch, int currentFixLength, List<string> result, int currentGridLine,
            int currentPlayerNumber, ref int maxLineLength, List<int> pointersInThisRound)
        {
            while (currentMatch?.PlayerOne != null)
            {
                string fixLine = "";
                for (int i = 0; i < currentFixLength - 1; i++)
                    fixLine += " ";
                result.Add(fixLine + " ");
                currentGridLine++;
                int nameLength;

                result.Add(fixLine);
                if (currentPlayerNumber == 2)
                {
                    AddPlayerToLine(result, currentMatch.PlayerOne.Name, result.Count - 1, ref currentPlayerNumber);
                    nameLength = currentMatch.PlayerOne.Name.Length;
                    if (currentMatch.PlayerTwo == null)
                        currentMatch = currentMatch.NextMatch;
                }
                else
                {
                    AddPlayerToLine(result,currentMatch.PlayerTwo.Name,result.Count-1,ref currentPlayerNumber);
                    nameLength = currentMatch.PlayerTwo.Name.Length;                
                    currentMatch = currentMatch.NextMatch;
                }
                if (maxLineLength < nameLength)
                    maxLineLength = nameLength;

                pointersInThisRound.Add(result.Count - 1);
                currentGridLine++;
            }
        }

        private static Match AddPreviousRoundWinners(List<int> pointersFromLastRound, Match currentMatch, List<string> result,
            List<int> pointersInThisRound, ref int currentGridLine, ref int currentPlayerNumber, ref int maxLineLength)
        {
            while (pointersFromLastRound.Count > 0 && currentMatch?.PlayerOne != null)
            {
                if (currentGridLine == pointersFromLastRound[0])
                {
                    if (currentPlayerNumber == 2)
                    {
                        AddPlayerToLine(result, currentMatch.PlayerOne.Name, currentGridLine, ref currentPlayerNumber);                       
                        if (currentMatch.PlayerTwo == null)
                            currentMatch = currentMatch.NextMatch;
                    }
                    else
                    {
                        AddPlayerToLine(result, currentMatch.PlayerTwo.Name, currentGridLine, ref currentPlayerNumber);
                        currentMatch = currentMatch.NextMatch;
                    }

                    if (result[currentGridLine].Length > maxLineLength)
                        maxLineLength = result[currentGridLine].Length;
                    pointersInThisRound.Add(currentGridLine);
                    pointersFromLastRound.RemoveAt(0);
                }
                currentGridLine++;
            }
            return currentMatch;
        }

        private static void AddPlayerToLine(List<string> result, string name, int currentGridLine, ref int currentPlayerNumber)
        {
            result[currentGridLine] += name;
            currentPlayerNumber = currentPlayerNumber == 1 ? 2 : 1;
        }        
    }
}