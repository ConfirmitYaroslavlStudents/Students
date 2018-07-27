using System;
using System.Collections.Generic;

namespace Tournament
{
    public abstract class TournamentGrid
    {

        protected List<Participant> _upperBracketParticipants;
        protected List<Participant> _lowerBracketParticipants;

        public TournamentGrid(List<string> participants)
        {
            _upperBracketParticipants = new List<Participant>();
            Random random = new Random();

            while (participants.Count>0)
            {
                int index = random.Next(participants.Count);
                var newParticipant = new Participant(participants[index]);
                _upperBracketParticipants.Add(newParticipant);
                participants.RemoveAt(index);
            }
        }


    }
}
