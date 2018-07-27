using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    public class SingleEliminationTournament: TournamentGrid
    {
        public SingleEliminationTournament(List<string> participants) : base(participants)
        {}

        public void PlayRound()
        {
            Round round = new Round(_upperBracketParticipants);
            _upperBracketParticipants = round.PlayUpperBracket();
        }

        public List<Participant> GetBracket()
        {
            return new List<Participant>(_upperBracketParticipants);
        }
    }
}
