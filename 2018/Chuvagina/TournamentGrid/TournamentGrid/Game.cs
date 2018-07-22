using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGrid
{
    public class Game
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

        private void InsertToBracket(List<Participant> Bracket, string participant, int firstIndex, int secondIndex)
        {
            int indexOfInsert = secondIndex - (secondIndex - firstIndex) / 2;
            Bracket.Insert(indexOfInsert, new Participant(participant, _roundIndex + 1));
        }

        public void PlayUpperBracketGame(int firstParticipantIndex, int secondParticipantIndex, int ParticipantsCount)
        {
            DetectWinners(_upperBracket, firstParticipantIndex, secondParticipantIndex);
            int koef = _lowerBracket.Count / (ParticipantsCount / 2)+1;
            int insertIndex= (_gameIndex / 2) *koef;
            _lowerBracket.Insert(insertIndex, new Participant(_upperBracket[_loserIndex].Name, _roundIndex));
            string winnerName = _upperBracket[_winnerIndex].Name;
            _upperBracket[_winnerIndex].Color = ConsoleColor.Green;
            InsertToBracket(_upperBracket, winnerName, firstParticipantIndex, secondParticipantIndex);

        }


        public void PlayLowerBracketGame(int firstParticipantIndex, int secondParticipantIndex, int ParticipantsCount, bool isLastRound)
        {
            DetectWinners(_lowerBracket, firstParticipantIndex, secondParticipantIndex);
            string winnerName = _lowerBracket[_winnerIndex].Name;
            _lowerBracket[_winnerIndex].Color = ConsoleColor.Green;
            InsertToBracket(_lowerBracket, winnerName, firstParticipantIndex, secondParticipantIndex);
            if (isLastRound)
            {
                _upperBracket.Add(new Participant(winnerName, _roundIndex));
            }
        }

        private void DetectWinners(List<Participant> Bracket,int firstParticipantIndex, int secondParticipantIndex)
        {
            int winner = DataInput.InputWinner(Bracket[firstParticipantIndex].Name, Bracket[secondParticipantIndex].Name);
            if (winner == 1)
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
