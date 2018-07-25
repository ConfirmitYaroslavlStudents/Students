using System.Collections.Generic;

namespace Tournament
{
    internal class PlayOffBracketBuilding : BracketBuilding
    {
        private BracketCell[,] _leftBracket;
        private BracketCell[,] _rightBracket;
        private readonly List<Participant> _leftBracketList;
        private readonly List<Participant> _rightBracketList;
        private readonly int _roundIndex;

        public PlayOffBracketBuilding(List<Participant> leftBracket, List<Participant> rightBracket, int roundIndex)
        {
            _leftBracketList = leftBracket;
            _rightBracketList = rightBracket;
            _roundIndex = roundIndex + 1;
        }

        public BracketCell[,] GetPlayOffBracket()
        {
            _leftBracket = CreateBracket(_roundIndex, Side.Left, _leftBracketList);
            _rightBracket = CreateBracket(_roundIndex, Side.Right, _rightBracketList);
            ResultBracket = new BracketCell[_leftBracketList.Count, _roundIndex * 4];

            for (int i = 0; i < _leftBracketList.Count; i++)
            {
                for (int j = 0; j < _roundIndex * 2; j++)
                {
                    ResultBracket[i, j] = _leftBracket[i, j];

                    if (_leftBracket[i, j] != null)
                        ResultBracket[i, _roundIndex * 4 - j - 1] = _rightBracket[i, j];
                }
            }
            return ResultBracket;
        }
    }
}