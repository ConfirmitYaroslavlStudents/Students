using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using FootballLeagueClassLibrary.Drawers;
using FootballLeagueClassLibrary.Structure;
using Football_League;

namespace FootballTournamentWPF.Drawer_Windows
{
    /// <summary>
    /// Interaction logic for HorizontalDisplayWindow.xaml
    /// </summary>
    public partial class HorizontalDisplayWindow : Window
    {
        private readonly FullGrid _grid;

        public HorizontalDisplayWindow(FullGrid grid)
        {
            _grid = grid;
            InitializeComponent();
            Draw();
        }

        private void Draw()
        {
            HorizontalDrawer horizontalGrid = new HorizontalDrawer();
            horizontalGrid.MakeHorintalGrid(_grid);
            PrintHorizontalGrid(horizontalGrid.HorizontalGrid);
        }
        public void AppendText(string text, string color)
        {
            RichTextBox rtb = HorizontalDrawerRichTextBox;
            BrushConverter bc = new BrushConverter();
            TextRange tr = new TextRange(rtb.Document.ContentEnd, rtb.Document.ContentEnd) { Text = text };
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, bc.ConvertFromString(color));
        }

        public void PrintHorizontalGrid(List<string> horizontalGrid)
        {
            HorizontalDrawerRichTextBox.Document.Blocks.Clear();
            int currentTreeStartIndex = 1;
            for (int currentLineIndex = 1; currentLineIndex < horizontalGrid.Count; currentLineIndex++)
            {
                if (horizontalGrid[currentLineIndex].Length == 0)
                {
                    currentTreeStartIndex = currentLineIndex + 1;
                    continue;
                }
                PrintSingleHorizontalLine(currentTreeStartIndex, horizontalGrid[currentLineIndex], horizontalGrid);
            }
            HorizontalDrawerRichTextBox.Document.Blocks.Add(new Paragraph(new Run("")));
        }

        public void PrintSingleHorizontalLine(int currentTreeStartIndex, string currentHorizontalGrid,
            List<string> horizontalGrid)
        {
            HorizontalDrawerRichTextBox.Document.Blocks.Add(new Paragraph(new Run("")));
            int currentPositionInLine = 0;

            while (currentPositionInLine < currentHorizontalGrid.Length)
            {
                if (!Char.IsLetterOrDigit(currentHorizontalGrid[currentPositionInLine]))
                {
                    AppendText(currentHorizontalGrid[currentPositionInLine].ToString(),"Black");
                    currentPositionInLine++;
                }

                else
                {
                    string name = HelperFunctions.MakeName(currentHorizontalGrid, currentPositionInLine);
                    currentPositionInLine += name.Length;

                    bool isLastName = HelperFunctions.CheckIfLastNameInLine(currentHorizontalGrid, currentPositionInLine);
                    PrintColoredName(currentTreeStartIndex, currentPositionInLine, name, horizontalGrid, isLastName,
                        currentHorizontalGrid);
                }
            }
        }
        private void PrintColoredName(int currentGridPointer, int currentPositionInLine, string name,
            List<string> horizontalGrid, bool isLastName, string currentHorizontalGrid)
        {
            string color = "";
            if (isLastName)
            {
                int longestLineLength = 0;
                foreach (var line in horizontalGrid)
                    if (line.Length > longestLineLength && line != currentHorizontalGrid)
                        longestLineLength = line.Length;

                if (longestLineLength > currentHorizontalGrid.Length)
                    color = "OrangeRed";
                if (longestLineLength < currentHorizontalGrid.Length)
                    color = "LawnGreen";
                AppendText(name,color);
                return;
            }

            bool isNameFoundInNextRound = false;
            for (int j = currentGridPointer; j < horizontalGrid.Count; j++)
            {
                if (horizontalGrid[j] == "")
                    break;

                isNameFoundInNextRound = horizontalGrid[j].Substring(currentPositionInLine + 1,
                    horizontalGrid[j].Length - (currentPositionInLine + 1)).Contains(name);
                if (isNameFoundInNextRound)
                    break;
            }
            color = isNameFoundInNextRound ? "LawnGreen" : "OrangeRed";
            AppendText(name,color);
            
        }

    }
}
