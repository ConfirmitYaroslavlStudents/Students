using System;
using System.Collections.Generic;

namespace TournamentGrid
{
    internal class Game
    {
        private readonly int _roundIndex;
        private readonly List<Participant> _upperBracket;
        private readonly List<Participant> _lowerBracket;
        private int _winnerIndex;
        private int _loserIndex;

        public Game(int roundIndex, List<Participant> bracket, List<Participant> lowerBracket)
        {
            _roundIndex = roundIndex;
            _upperBracket = bracket;
            _lowerBracket = lowerBracket;
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

        private void InsertToBracket(List<Participant> bracket, string participant, int firstIndex, int secondIndex)
        {
            int insertIndex = secondIndex - (secondIndex - firstIndex) / 2;
            bracket.Insert(insertIndex, new Participant(participant, _roundIndex + 1));
        }

        public void PlayLowerBracketGame(int firstParticipantIndex, int secondParticipantIndex)
        {
            DetectWinner(_lowerBracket, firstParticipantIndex, secondParticipantIndex);

            string winnerName = _lowerBracket[_winnerIndex].Name;
            _lowerBracket[_winnerIndex].Color = ConsoleColor.Green;

            InsertToBracket(_lowerBracket, winnerName, firstParticipantIndex, secondParticipantIndex);
        }

        private void DetectWinner(List<Participant> bracket,int firstParticipantIndex, int secondParticipantIndex)
        {
            string firstParticipantName = bracket[firstParticipantIndex].Name;
            string secondParticipantName = bracket[secondParticipantIndex].Name;
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
