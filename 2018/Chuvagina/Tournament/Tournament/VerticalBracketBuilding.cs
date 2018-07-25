using System;
using System.Collections.Generic;
using System.Text;

namespace Tournament
{
    internal class VerticalBracketBuilding: BracketBuilding
    {
        private readonly List<Participant> _bracketList;
        private readonly int _roundIndex;

        public VerticalBracketBuilding(List<Participant> bracket, int roundIndex)
        {
            _bracketList = bracket;
            _roundIndex = roundIndex + 1;
        }

        public BracketCell[,] GetVerticalBracket()
        {
            ResultBracket = CreateBracket(_roundIndex, Side.Vertical, _bracketList);
            return ResultBracket;
        }
    }
}
