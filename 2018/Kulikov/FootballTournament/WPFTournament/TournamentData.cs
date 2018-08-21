using System.Collections.Generic;
using TournamentLibrary;

namespace WPFTournament
{
    public class TournamentData
    {
        public int CountOfPlayers { get; private set; }
        public List<Player> Players { get; private set; }

        private HashSet<string> _existingNames;
        private int _currentPlayerForAddition;

        public TournamentData()
        {
            Players = new List<Player>();
            _existingNames = new HashSet<string>();
            _currentPlayerForAddition = 0;
        }

        public void GetCountOfPlayers(int count)
        {
            CountOfPlayers = count;
        }

        public bool IsPlayerExists(string name)
        {
            if (_existingNames.Contains(name))
                return true;
            else
                return false;
        }

        public void AddPlayer(string name)
        {
            _existingNames.Add(name);
            Players.Add(new Player(name));
            _currentPlayerForAddition++;
        }

        public bool IsAdditionOver()
        {
            if (CountOfPlayers == _currentPlayerForAddition)
                return true;
            else
                return false;
        }
    }
}
