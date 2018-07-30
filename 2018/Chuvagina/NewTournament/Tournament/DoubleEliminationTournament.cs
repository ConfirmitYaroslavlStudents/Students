using System.Collections.Generic;

namespace Tournament
{
    public class DoubleEliminationTournament: SingleEliminationTournament
    {
        private List<Participant> _lowerBracketParticipants;

        public DoubleEliminationTournament(List<string> participants) : base(participants)
        {
            _lowerBracketParticipants = new List<Participant>();
        }

        public DoubleEliminationTournament() : base()
        {
            _lowerBracketParticipants = LoadListFromBinnary<Participant>("lowerBracket"); ;
        }

        public override void PlayRound()
        {
            var round = new Round(UpperBracketParticipants, _lowerBracketParticipants);

            if (UpperBracketParticipants.Count > _lowerBracketParticipants.Count)
            {
                round.PlayUpperBracket();
                UpperBracketParticipants = round.UpperBracketParticipants;
                _lowerBracketParticipants = round.LowerBracketParticipants;

                if (UpperBracketParticipants.Count == 1 && _lowerBracketParticipants.Count == 1)
                    _lowerBracketParticipants.RemoveAt(0);
            }
            else
            {
                _lowerBracketParticipants = round.PlayLowerBracket();

                if (UpperBracketParticipants.Count == 1 && _lowerBracketParticipants.Count == 1)
                {
                    UpperBracketParticipants.Add(_lowerBracketParticipants[0]);
                    _lowerBracketParticipants.RemoveAt(0);
                }
            }

            SaveListToBinnary("upperBracket", UpperBracketParticipants);
            SaveListToBinnary("lowerBracket", _lowerBracketParticipants);
        }

        public List<Participant> GetLowerBracket()
        {
            return new List<Participant>(_lowerBracketParticipants);
        }

        public override bool EndOfTheGame()
        {
            return base.EndOfTheGame() && _lowerBracketParticipants.Count==0;
        }

    }
}
