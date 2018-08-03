using System.Collections.Generic;
using System.Text;

namespace Football_League
{
    public class VerticalDrawer
    {
        private readonly List<string> _verticalGrid = new List<string>();

        public void MakeVerticalGrid(FullGrid grid)
        {
            for(int i = 0; i < grid.Grid.Count; i++)
            {
                var result = MakeGridTree(grid.Grid[i]);
                for (int j = 0; j < result.Count; j++)
                {
                    if (_verticalGrid.Count <= j + i * 3)
                        _verticalGrid.Add(result[j]);
                    else
                        _verticalGrid[j + i * 3] += result[j];
                }
            }
        }

        public List<string> MakeGridTree(MatchTreeGrid gridTree)
        {
            List<string> result = new List<string>();
            Match firstInCurrentRound = gridTree.StartMatch;

            while (firstInCurrentRound != null)
            {
                string names = "";
                Match matchToDraw = firstInCurrentRound;

                if (result.Count == 0)
                {
                    while (matchToDraw.PlayerOne != null && matchToDraw.PlayerTwo != null)
                    {
                        names += matchToDraw.PlayerOne.Name + " " + matchToDraw.PlayerTwo.Name + " ";
                        matchToDraw = matchToDraw.NextMatch;
                    }
                    if (matchToDraw.PlayerOne != null)
                        names += matchToDraw.PlayerOne.Name + " ";
                }
                else
                {
                    int playerDrawnLast = 2;
                    for (int i = 0; i < result[result.Count - 1].Length; i++)
                    {
                        if (result[result.Count - 1][i] == '|')
                        {
                            switch (playerDrawnLast)
                            {
                                case 2:
                                    names += matchToDraw.PlayerOne.Name + " ";
                                    playerDrawnLast = 1;
                                    i = names.Length - 1;

                                    if (matchToDraw.PlayerTwo == null)
                                        matchToDraw = matchToDraw.NextMatch;

                                    continue;

                                case 1:
                                    names += matchToDraw.PlayerTwo.Name + " ";
                                    playerDrawnLast = 2;
                                    matchToDraw = matchToDraw.NextMatch;
                                    i = names.Length - 1;
                                    break;
                            }
                        }
                        else
                            names += " ";
                    }

                    while (matchToDraw?.PlayerOne != null )
                    {
                        switch (playerDrawnLast)
                        {
                            case 2:
                                names += matchToDraw.PlayerOne.Name + " ";
                                playerDrawnLast = 1;
                                continue;

                            case 1 when matchToDraw.PlayerTwo != null:
                                names += matchToDraw.PlayerTwo.Name + " ";
                                playerDrawnLast = 2;
                                matchToDraw = matchToDraw.NextMatch;
                                break;
                        }
                    }
                    for(int i = 0; i < result.Count; i++)
                    {
                        while (result[i].Length < names.Length)
                            result[i] += " ";
                    }
                }

                List<StringBuilder> connectionLines =
                    new List<StringBuilder> {new StringBuilder(), new StringBuilder()};
                int firstWordIndex = -1;

                for (int i = 0; i < names.Length; i++)
                {
                    if (names[i] == ' ')
                    {
                        connectionLines[0].Append(" ");
                        connectionLines[1].Append(" ");
                        continue;
                    }

                    int newNameLength = 0;
                    while (i < names.Length && names[i] != ' ')
                    {
                        newNameLength++;
                        i++;
                    }
                    if (firstWordIndex == -1)
                    {
                        firstWordIndex = i - newNameLength + newNameLength/2;
                        for (int j = i - newNameLength; j < i; j++)
                        {
                            connectionLines[0].Append(" ");
                            connectionLines[1].Append(" ");
                        }
                        connectionLines[0][firstWordIndex] = '|';
                        connectionLines[1][firstWordIndex] = '|';
                        i--;
                        continue;
                    }
                    if (firstWordIndex != -1)
                    {
                        int secondWordIndex = i - newNameLength + newNameLength / 2;
                        for (int j = i - newNameLength; j < i; j++)
                        {
                            connectionLines[0].Append(" ");
                            connectionLines[1].Append(" ");
                        }
                        connectionLines[1][firstWordIndex] = ' ';
                        for (int j = firstWordIndex + 1; j < secondWordIndex; j++)
                            connectionLines[0][j] = '_';
                        connectionLines[0][secondWordIndex] = '|';
                        connectionLines[1][(secondWordIndex + firstWordIndex) / 2] = '|';

                        i--;
                        firstWordIndex = -1;
                    }

                }
                result.Add(names);
                result.Add(connectionLines[0].ToString());
                result.Add(connectionLines[1].ToString());
                firstInCurrentRound = firstInCurrentRound.NextRoundMatch;
            }
            return result;
        }
        public void PrintGrid()
        {
            ConsoleWorker.PrintVerticalGrid(_verticalGrid);
        }
    }
}
