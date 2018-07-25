using System.Collections.Generic;

namespace Tournament
{
    internal abstract class BracketBuilding
    {
        public enum Side
        {
            Left,
            Right,
            Vertical
        }

        private const string LeftUpperCorner = "\u2500\u2510";
        private const string RightUpperCorner = "\u250C\u2500";
        private const string LeftLowerCorner = "\u2500\u2518";
        private const string RightLowerCorner = "\u2514\u2500";
        private const string VerticalStick = "\u2502";
        private const string HorizontalStick = "\u005f";
        protected BracketCell[,] ResultBracket;

        protected void RemoveLastColumn(BracketCell[,] bracket, int lastColumn, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (bracket[i, lastColumn] != null)
                    bracket[i, lastColumn] = new BracketCell();
            }
        }

        private string CreateHorizontalStick()
        {
            string horizontalSticks = "";
            for (int i = 0; i < OrganizedTournament.MaxLengthOfString-1; i++)
            {
                horizontalSticks += HorizontalStick;
            }

            return horizontalSticks;
        }

        private void AddBrackets(int row, int amountOfColumns, BracketCell[,] resultGrid, int[] amountOfRoundParticipants, Side side)
        {
            var upperCorner = RightUpperCorner;
            var lowerCorner = RightLowerCorner;
            var stick = VerticalStick;

            if (side == Side.Left)
            {
                upperCorner = LeftUpperCorner;
                lowerCorner = LeftLowerCorner;
            }
            else if (side == Side.Vertical)
            {
                upperCorner = VerticalStick+CreateHorizontalStick();
                lowerCorner = VerticalStick;
                stick = HorizontalStick+ CreateHorizontalStick();
            }

            for (int j = 1; j < amountOfColumns; j += 2)
            {
                if (resultGrid[row, j - 1] != null)
                {
                    if (amountOfRoundParticipants[j] % 2 == 1)
                        resultGrid[row, j] = new BracketCell(upperCorner);
                    else
                        resultGrid[row, j] = new BracketCell(lowerCorner);
                }
                else if (amountOfRoundParticipants[j] % 2 == 1)
                    resultGrid[row, j] = new BracketCell(stick);
            }
        }

        protected BracketCell[,] CreateBracket(int roundIndex, Side side, List<Participant> bracket)
        {
            int bracketAmountOfColumns = roundIndex * 2;
            int maxColumnIndex = 0;
            int[] amountOfRoundParticipants = new int[bracketAmountOfColumns];
            var result = new BracketCell[bracket.Count, bracketAmountOfColumns];

            for (int i = 0; i < bracket.Count; i++)
            {
                if (bracket[i].Round < roundIndex)
                {
                    int columnIndex = bracket[i].Round * 2;
                    if (columnIndex > maxColumnIndex)
                        maxColumnIndex = columnIndex;
                    result[i, columnIndex] = new BracketCell(bracket[i].Name, bracket[i].Color);
                    amountOfRoundParticipants[columnIndex + 1]++;
                }

                AddBrackets(i, bracketAmountOfColumns, result, amountOfRoundParticipants, side);
            }

            if (amountOfRoundParticipants[roundIndex * 2 - 1] == 1)
                RemoveLastColumn(result, maxColumnIndex + 1, bracket.Count);

            return result;
        }
    }
}