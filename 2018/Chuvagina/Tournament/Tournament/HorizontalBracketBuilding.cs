using System.Collections.Generic;

namespace Tournament
{
    internal class HorizontalBracketBuilding : BracketBuilding
    {

        private readonly List<Participant> _bracketList;
        private readonly int _roundIndex;

        public HorizontalBracketBuilding(List<Participant> bracket, int roundIndex)
        {
            _bracketList = bracket;
            _roundIndex = roundIndex + 1;
        }

        public BracketCell[,] GetHorizontalBracket()
        {
            ResultBracket = CreateBracket(_roundIndex, Side.Left, _bracketList);
            return ResultBracket;
        }

    }
}