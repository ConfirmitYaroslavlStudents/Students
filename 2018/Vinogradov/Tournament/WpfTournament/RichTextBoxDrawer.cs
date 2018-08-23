using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using TournamentLibrary;

namespace WpfTournament
{
    class RichTextBoxDrawer : Drawer
    {
        private RichTextBox _box;
        private const string _newLine = "\r\n";
        private const string _mainColor = "Black";
        private const string _highlightColor = "Green";

        public RichTextBoxDrawer(RichTextBox box)
        {
            _box = box;
        }

        private void AppendTextWithColor(string text, string color)
        {
            BrushConverter bc = new BrushConverter();
            TextRange tr = new TextRange(_box.Document.ContentEnd, _box.Document.ContentEnd);
            tr.Text = text;
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, bc.ConvertFromString(color));
        }

        public override void DrawTable(Tournament tournament)
        {
            List<string>[] mainGames = GetGridLines(tournament.Main);
            List<string>[] losersGames = null;

            if (tournament.DoubleElimination)
            {
                losersGames = GetGridLines(tournament.Losers);

                if (losersGames[0].Count > mainGames[0].Count)
                {
                    for (int i = 0; i < mainGames.Length; i++)
                    {
                        mainGames[i].Add(mainGames[i][mainGames[i].Count - 1]);
                    }
                }

                FillLines(mainGames, 0, mainGames.Length - 1);
                string name = string.Empty;

                if (tournament.Champion != null)
                {
                    name = tournament.Champion.Name;
                }

                mainGames[mainGames.Length - 1].Add(name);
                FillLines(losersGames, 0, losersGames.Length);
            }

            PrintGrid(mainGames, tournament, false);

            if (tournament.DoubleElimination)
            {
                PrintGrid(losersGames, tournament, true);
            }
        }

        private void PrintGrid(List<string>[] lines, Tournament tournament, bool isLoserGrid)
        {
            for (int k = 0; k < lines.Length; k++)
            {
                for (int i = 0; i < lines[k].Count; i++)
                {
                    PrintCell(tournament, isLoserGrid, lines, k, i);
                }

                AppendTextWithColor(_newLine, _mainColor);
            }
        }

        private void PrintCell(Tournament tournament, bool isLoserGrid, List<string>[] lines, int line, int column)
        {
            string cell = lines[line][column];

            if (cell != null)
            {
                if (column > 0)
                {
                    AppendTextWithColor("-", _mainColor);
                }
                else
                {
                    AppendTextWithColor(" ", _mainColor);
                }

                string color = _mainColor;

                if (line == lines.Length - 1 || GreenLight(tournament, isLoserGrid, line, column))
                {
                    color = _highlightColor;
                }

                AppendTextWithColor(AlignName(cell, NameValidator.MaxChars), color);
                int gridLength = tournament.Main.Matches.Length;

                if (isLoserGrid)
                {
                    gridLength = tournament.Losers.Matches.Length;
                }

                if (column < gridLength)
                {
                    AppendTextWithColor("-", _mainColor);
                }
                else
                {
                    AppendTextWithColor(" ", _mainColor);
                }
            }
            else
            {
                AppendTextWithColor(AlignName(string.Empty, (NameValidator.MaxChars + 2)), _mainColor);
            }
        }
    }
}
