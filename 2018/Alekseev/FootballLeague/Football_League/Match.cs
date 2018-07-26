namespace Football_League
{
    public class Match
    {
        private readonly Contestant _playerOne;
        private readonly Contestant _playerTwo;
        public Contestant Winner;
        public Contestant Loser;

        public Match(Contestant first = null, Contestant second = null)
        {
            _playerOne = first;
            _playerTwo = second;
        }

        public string PlayerOne => _playerOne.Name;

        public string PlayerTwo => _playerTwo.Name;

        public bool AutoWin()
        {
            if (_playerTwo == null)
            {
                Winner = _playerOne;
                Loser = null;
                return true;
            }
            if (_playerOne == null)
            {
                Loser = null;
                Winner = _playerTwo;
                return true;
            }
            return false;
        }
        public void SetWinner(int number = 0)
        {
            if (AutoWin())
                return;
            Winner = (number == 1) ? _playerOne : _playerTwo;
            Loser = (number == 1) ? _playerTwo : _playerOne;
            if(_playerTwo != null)
                Winner.Position = (_playerOne.Position + _playerTwo.Position) / 2;
        }
    }
}
