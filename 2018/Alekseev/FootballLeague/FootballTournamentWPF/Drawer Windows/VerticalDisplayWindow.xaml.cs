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
    /// Interaction logic for VerticalDisplayWindow.xaml
    /// </summary>
    public partial class VerticalDisplayWindow : Window
    {
        private readonly FullGrid _grid;
        public VerticalDisplayWindow(FullGrid grid)
        {
            _grid = grid;
            InitializeComponent();
            Draw();
        }
        private void Draw()
        {
            VerticalDrawer verticalGrid = new VerticalDrawer();
            verticalGrid.MakeVerticalGrid(_grid);
            PrintVerticalGrid(verticalGrid.VerticalGrid);
        }
        public void PrintVerticalGrid(List<string> verticalGrid)
        {
            VerticalGridRichTextBox.Document.Blocks.Add(new Paragraph(new Run("")));
            for (int i = 0; i < verticalGrid.Count - 2; i++)
            {
                PrintSingleVerticalLine(i, verticalGrid);
            }
        }
        public void AppendText(string text, string color)
        {
            RichTextBox rtb = VerticalGridRichTextBox;
            BrushConverter bc = new BrushConverter();
            TextRange tr = new TextRange(rtb.Document.ContentEnd, rtb.Document.ContentEnd) {Text = text};
            tr.ApplyPropertyValue(TextElement.ForegroundProperty, bc.ConvertFromString(color));
        }
        public void PrintSingleVerticalLine(int currentLineIndex, List<string> verticalGrid)
        {
            if (currentLineIndex % 3 != 0)
            {
                VerticalGridRichTextBox.Document.Blocks.Add(new Paragraph(new Run("")));
                AppendText(verticalGrid[currentLineIndex],"Black");
                VerticalGridRichTextBox.Document.Blocks.Add(new Paragraph(new Run("")));
            }
            else
            {
                for (int j = 0; j < verticalGrid[currentLineIndex].Length; j++)
                {
                    if (verticalGrid[currentLineIndex][j] == ' ')
                    {
                        AppendText(" ", "Black");
                    }
                    else
                    {
                        int firstNamePosition = j;
                        string firstName = HelperFunctions.MakeName(verticalGrid[currentLineIndex], j);
                        j += firstName.Length;

                        bool onlyOnePlayerToPrint = CheckIfOnlyOnePlayerToPrint(j, currentLineIndex, verticalGrid, firstName, firstNamePosition);
                        if (onlyOnePlayerToPrint)
                        {
                            j--;
                            PrintVerticalGridAutoWinner(firstName, verticalGrid, currentLineIndex);
                            continue;
                        }

                        PrintTwoPlayers(currentLineIndex, verticalGrid, ref j, firstNamePosition, firstName);
                    }
                }
                Console.WriteLine();
            }
        }

        private bool CheckIfOnlyOnePlayerToPrint(int currentPositionInLine, int currentLineIndex, List<string> verticalGrid, string firstName, int firstNamePosition)
        {
            bool isLastNameInLine = currentLineIndex + 3 >= verticalGrid.Count || currentPositionInLine >= verticalGrid[currentLineIndex + 3].Length;

            if (isLastNameInLine)
                return true;

            bool isFinalWinner = false;
            if (currentLineIndex + 3 < verticalGrid.Count &&
                firstName.Length + firstNamePosition < verticalGrid[currentLineIndex + 3].Length)
            {
                string nextRoundWinners = verticalGrid[currentLineIndex + 3];
                isFinalWinner = nextRoundWinners.Substring(firstNamePosition, firstName.Length).Contains(firstName);
            }
            if (isFinalWinner)
                return true;

            return false;
        }
        private void PrintTwoPlayers(int currentLineIndex, List<string> verticalGrid, ref int j, int firstNamePosition, string firstName)
        {
            while (j < verticalGrid[currentLineIndex].Length && verticalGrid[currentLineIndex][j] == ' ')
                j++;

            int secondNamePosition = j;
            int secondNamePositionSaver = j;

            string secondName = HelperFunctions.MakeName(verticalGrid[currentLineIndex], j);
            j += secondName.Length;

            if (currentLineIndex + 3 < verticalGrid.Count && verticalGrid[currentLineIndex + 3].Length <=
                secondNamePosition + secondName.Length - firstNamePosition)
                secondNamePosition = verticalGrid[currentLineIndex + 3].Length - secondName.Length;

            PrintFirstAndSecondNames(currentLineIndex, verticalGrid, firstNamePosition, firstName, secondName, secondNamePosition, secondNamePositionSaver);
            j--;
        }

        private void PrintFirstAndSecondNames(int currentLineIndex, List<string> verticalGrid, int firstNamePosition, string firstName, string secondName, int secondNamePosition, int secondNamePositionSaver)
        {
            string color;
            if (currentLineIndex + 3 < verticalGrid.Count && verticalGrid[currentLineIndex + 3]
                    .Substring(firstNamePosition,
                        secondNamePosition + secondName.Length - firstNamePosition)
                    .Contains(firstName))
            {
                color = "Green";
            }
            else
                color = "Red";

            if (currentLineIndex + 3 >= verticalGrid.Count)
                color = "Black";

            AppendText(firstName,color);

            while (firstNamePosition + firstName.Length != secondNamePositionSaver)
            {
                AppendText(" ", "Black");
                firstNamePosition++;
            }

            switch (color)
            {
                case "Red":
                    color = "Green";
                    break;

                case "Green":
                    color = "Red";
                    break;
            }

            AppendText(secondName,color);
        }
        private void PrintVerticalGridAutoWinner(string firstName, List<string> verticalGrid, int currentLinePosition)
        {
            string color;
            if (currentLinePosition + 3 >= verticalGrid.Count ||
                verticalGrid[currentLinePosition + 3].Contains(firstName))
                color = "Green";
            else
                color = "Red";
            if (currentLinePosition + 3 >= verticalGrid.Count)
                color = "Black";
            AppendText(firstName, color);           
        }
    }
}
