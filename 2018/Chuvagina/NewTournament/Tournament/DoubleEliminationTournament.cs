using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    public class DoubleEliminationTournament:TournamentGrid
    {
        public DoubleEliminationTournament(List<string> participants) : base(participants)
        {
            _lowerBracketParticipants = new List<Participant>();
        }

        public void PlayUpperBracket()
        {
            var round = new Round(_upperBracketParticipants, _lowerBracketParticipants);
            round.PlayUpperBracket(out _upperBracketParticipants, out _lowerBracketParticipants);
        }

        public void PlayLastRound()
        {
            PlayUpperBracket();
            _lowerBracketParticipants.RemoveAt(0);
        }

        public bool PlayLowerBracket()
        {
            var round = new Round(_upperBracketParticipants, _lowerBracketParticipants);
            _lowerBracketParticipants = round.PlayLowerBracket();
            if (_upperBracketParticipants.Count == 1 && _lowerBracketParticipants.Count == 1)
            {
                _upperBracketParticipants.Add(_lowerBracketParticipants[0]);
                _lowerBracketParticipants.RemoveAt(0);
                return true;
            }
               
            else
                return false;
        }

        public List<Participant> GetUpperBracket()
        {
            return new List<Participant>(_upperBracketParticipants);
        }

        public List<Participant> GetLowerBracket()
        {
            return new List<Participant>(_lowerBracketParticipants);
        }
    }
}
