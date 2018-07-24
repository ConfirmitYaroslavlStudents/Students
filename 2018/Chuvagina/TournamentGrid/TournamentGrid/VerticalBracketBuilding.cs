using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGrid
{
    class VerticalBracketBuilding: BracketBuilding
    {
        private readonly List<Participant> _bracketList;
        private readonly int _roundIndex;

        public VerticalBracketBuilding(List<Participant> bracket, int roundIndex)
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
