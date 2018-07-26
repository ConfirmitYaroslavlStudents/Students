using System.Collections.Generic;

namespace Tournament
{
    public class TournamentGrid
    {

        private List<Participant> _upperBracketParticipants;
        private List<Participant> _lowerBracketParticipants;

        public TournamentGrid(List<string> participants)
        {
            _upperBracketParticipants = new List<Participant>();
            _lowerBracketParticipants = new List<Participant>();

            foreach (var participant in participants)
            {
                var newParticipant = new Participant(participant);
                _upperBracketParticipants.Add(newParticipant);
            }
        }

        public void PlayRound(out List<Participant> upperBracket)
        {
            
            Round round = new Round(_upperBracketParticipants);
            round.PlayUpper(out _upperBracketParticipants);
            upperBracket = new List<Participant>(_upperBracketParticipants);
        }

        public void PlayRound(out List<Participant> upperBracket, out List<Participant> lowerBracket)
        {
            var round = new Round(_upperBracketParticipants, _lowerBracketParticipants);
            round.PlayDoubleEliminationRound(out _upperBracketParticipants, out _lowerBracketParticipants);
            upperBracket = new List<Participant>(_upperBracketParticipants);
            lowerBracket = new List<Participant>(_lowerBracketParticipants);
        }

        

    }
}
