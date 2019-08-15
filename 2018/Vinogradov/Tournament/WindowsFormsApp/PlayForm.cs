using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentLibrary;

namespace WindowsFormsApp
{
    public partial class PlayForm : Form
    {
        private Tournament _tournament;

        public PlayForm(Tournament tournament)
        {
            InitializeComponent();
            _tournament = tournament;
        }

        private List<string>[] GetGridLines(Grid grid)
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

        private void FillLines(List<string>[] lines, int position, int gapSize)
        {
            for (int i = 0; i < gapSize; i++)
            {
                lines[position + i].Add(null);
            }
        }
    }
}
