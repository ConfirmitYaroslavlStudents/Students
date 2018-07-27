using System;
using System.Collections.Generic;

namespace Tournament
{
    internal class Round
    {


        private List<Participant> _upperBracketParticipants;
        private List<Participant> _lowerBracketParticipants;

        public Round(List<Participant> upperBracketParticipants, List<Participant> lowerBracketParticipants)
        {
            _upperBracketParticipants = upperBracketParticipants;
            _lowerBracketParticipants = lowerBracketParticipants;
        }


        public Round(List<Participant> participants)
        {
            _upperBracketParticipants = participants;
        }

        public void PlayUpperBracket(out List<Participant> upperBracket, out List<Participant> lowerBracket)
        {
            upperBracket = new List<Participant>();

            for (int i = 0; i < _upperBracketParticipants.Count / 2; i++)
            {
                var leftParticipant = _upperBracketParticipants[i * 2];
                var rightParticipant = _upperBracketParticipants[i * 2 + 1];
                var game = new Game(leftParticipant, rightParticipant);
                game.PlayGame(out Participant winner, out Participant loser);
                upperBracket.Add(winner);
                if (_lowerBracketParticipants.Count < i * 2)
                    _lowerBracketParticipants.Insert(i, loser);
                else
                    _lowerBracketParticipants.Insert(i * 2, loser);

            }

            if (_upperBracketParticipants.Count % 2 == 1)
                upperBracket.Add(_upperBracketParticipants[_upperBracketParticipants.Count - 1]);

            lowerBracket = _lowerBracketParticipants;

        }

        public List<Participant> PlayUpperBracket()
        {
            var upperBracket = new List<Participant>();

            for (int i = 0; i < _upperBracketParticipants.Count / 2; i++)
            {
                var leftParticipant = _upperBracketParticipants[i * 2];
                var rightParticipant = _upperBracketParticipants[i * 2 + 1];
                var game = new Game(leftParticipant, rightParticipant);
                game.PlayGame(out Participant winner, out Participant loser);
                upperBracket.Add(winner);

            }

            if (_upperBracketParticipants.Count % 2 == 1)
                upperBracket.Add(_upperBracketParticipants[_upperBracketParticipants.Count - 1]);

            return upperBracket;

        }


        public List<Participant> PlayLowerBracket()
        {
            var lowerBracket = new List<Participant>();
            for (int i = 0; i < _lowerBracketParticipants.Count / 2; i++)
            {
                var leftParticipant = _lowerBracketParticipants[i * 2];
                var rightParticipant = _lowerBracketParticipants[i * 2 + 1];
                var game = new Game(leftParticipant, rightParticipant);
                game.PlayGame(out Participant winner, out Participant loser);
                lowerBracket.Add(winner);
            }

            if (_lowerBracketParticipants.Count % 2 == 1)
                lowerBracket.Add(_lowerBracketParticipants[_lowerBracketParticipants.Count - 1]);

            return lowerBracket;

        }



    }
}
