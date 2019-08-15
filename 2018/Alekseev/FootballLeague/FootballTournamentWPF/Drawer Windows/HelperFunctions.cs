using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTournamentWPF.Drawer_Windows
{
    public static class HelperFunctions
    {
        public static string MakeName(string line, int j)
        {
            string name = "";
            while (j < line.Length && char.IsLetterOrDigit(line[j]))
            {
                name += line[j];
                j++;
            }

            return name;
        }
        public static bool CheckIfLastNameInLine(string currentHorizontalGrid, int currentPositionInLine)
        {
            bool isLastName = false;
            for (int j = currentPositionInLine; j < currentHorizontalGrid.Length; j++)
            {
                if (j >= currentHorizontalGrid.Length - 2)
                {
                    isLastName = true;
                    break;
                }
                if (char.IsLetterOrDigit(currentHorizontalGrid[j]) || ((currentHorizontalGrid[j] == '|')
                                                                       && j < currentHorizontalGrid.Length - 2))
                    break;
            }
            return isLastName;
        }
    }
}
