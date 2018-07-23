using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGrid
{
    internal class Game
    {
        private int _roundIndex;
        private int _gameIndex;
        private List<Participant> _upperBracket;
        private List<Participant> _lowerBracket;
        private int _winnerIndex;
        private int _loserIndex;

        public Game(int gameIndex, int roundIndex, List<Participant> Bracket, List<Participant> LowerBracket)
        {
            _gameIndex = gameIndex;
            _roundIndex = roundIndex;
            _upperBracket = Bracket;
            _lowerBracket = LowerBracket;
        }

        public string PlayUpperBracketGame(int firstParticipantIndex, int secondParticipantIndex)
        {
            DetectWinner(_upperBracket, firstParticipantIndex, secondParticipantIndex);   
            
            string loser = _upperBracket[_loserIndex].Name;     
            string winnerName = _upperBracket[_winnerIndex].Name;
            _upperBracket[_winnerIndex].Color = ConsoleColor.Green;

            InsertToBracket(_upperBracket, winnerName, firstParticipantIndex, secondParticipantIndex);

            return loser;
        }

        private void InsertToBracket(List<Participant> Bracket, string participant, int firstIndex, int secondIndex)
        {
            int insertIndex = secondIndex - (secondIndex - firstIndex) / 2;
            Bracket.Insert(insertIndex, new Participant(participant, _roundIndex + 1));
        }

        public void PlayLowerBracketGame(int firstParticipantIndex, int secondParticipantIndex)
        {
            DetectWinner(_lowerBracket, firstParticipantIndex, secondParticipantIndex);

            string winnerName = _lowerBracket[_winnerIndex].Name;
            _lowerBracket[_winnerIndex].Color = ConsoleColor.Green;

            InsertToBracket(_lowerBracket, winnerName, firstParticipantIndex, secondParticipantIndex);
        }

        private void DetectWinner(List<Participant> Bracket,int firstParticipantIndex, int secondParticipantIndex)
        {
            string firstParticipantName = Bracket[firstParticipantIndex].Name;
            string secondParticipantName = Bracket[secondParticipantIndex].Name;
            string winnerName = DataInput.InputWinner(firstParticipantName, secondParticipantName);
            if (winnerName == firstParticipantName)
            {
                _winnerIndex = firstParticipantIndex;
                _loserIndex = secondParticipantIndex;
            }
            else
            {
                _winnerIndex = secondParticipantIndex;
                _loserIndex = firstParticipantIndex;
            }
        }

    }
}
