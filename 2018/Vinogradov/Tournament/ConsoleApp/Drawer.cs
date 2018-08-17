using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentLibrary
{
    public abstract class Drawer
    {
        public abstract void DrawTable(Tournament tournament);

        protected bool GreenLight(Tournament tournament, bool loserGrid, int line, int column)
        {
            Grid grid = tournament.Main;

            if (loserGrid)
            {
                grid = tournament.Losers;
            }

            if (column < grid.Matches.Length)
            {
                int matchNumber = line / (int)Math.Pow(2, column + 2);
                int playerInPair = (line % (int)Math.Pow(2, column + 2)) / (int)Math.Pow(2, column + 1);

                if (grid.Matches[column][matchNumber].Winner == playerInPair)
                {
                    return true;
                }
            }
            else if (!(tournament.DoubleElimination ^ grid.Winner == tournament.Champion))
            {
                return true;
            }

            return false;
        }

        protected string AlignName(string name, int size)
        {
            StringBuilder builder = new StringBuilder(size);
            int firstGap = (size - name.Length) / 2;

            for (int i = 0; i < firstGap; i++)
            {
                builder.Append(' ');
            }

            builder.Append(name);
            int secondGap = size - name.Length - firstGap;

            for (int i = 0; i < secondGap; i++)
            {
                builder.Append(' ');
            }

            return builder.ToString();
        }

        protected List<string>[] GetGridLines(Grid grid)
        {
            int tableHeight = (int)Math.Pow(2, grid.Matches.Length + 1);
            List<string>[] lines = new List<string>[tableHeight];

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new List<string>();
            }

            int gapSize = 1;
            int currentLine = 0;

            for (int k = 0; k < grid.Matches.Length; k++)
            {
                for (int i = 0; i < grid.Matches[k].Length; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        FillLines(lines, currentLine, gapSize - 1);
                        currentLine += gapSize - 1;
                        lines[currentLine].Add(grid.Matches[k][i].Opponents[j]);
                        currentLine++;
                        FillLines(lines, currentLine, gapSize);
                        currentLine += gapSize;
                    }
                }

                FillLines(lines, currentLine, tableHeight - currentLine);
                currentLine = 0;
                gapSize *= 2;
            }

            int halfHeight = tableHeight / 2;
            FillLines(lines, 0, halfHeight - 1);
            lines[halfHeight - 1].Add(grid.Winner);
            FillLines(lines, halfHeight, tableHeight - halfHeight);
            return lines;
        }

        protected void FillLines(List<string>[] lines, int position, int gapSize)
        {
            for (int i = 0; i < gapSize; i++)
            {
                lines[position + i].Add(null);
            }
        }
    }
}
