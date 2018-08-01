using System;
using System.Collections.Generic;

namespace Tournament
{
    public class DoubleEliminationTournament: SingleEliminationTournament
    {
        private List<Participant> _lowerBracketParticipants;

        public DoubleEliminationTournament(List<string> participants, Func<string, string> inputWinner) : base(participants,inputWinner)
        {
            _lowerBracketParticipants = new List<Participant>();
        }

        public DoubleEliminationTournament(Func<string, string> inputWinner) : base(inputWinner)
        {
            _lowerBracketParticipants = BinarySaver.LoadListFromBinnary<Participant>("lowerBracket");
        }

        public new void PlayRound()
        {
            var round = new Round(UpperBracketParticipants, _lowerBracketParticipants);

            if (UpperBracketParticipants.Count > _lowerBracketParticipants.Count)
            {
                round.PlayUpperBracket(InputWinner);
                UpperBracketParticipants = round.UpperBracketParticipants;
                _lowerBracketParticipants = round.LowerBracketParticipants;

                if (UpperBracketParticipants.Count == 1 && _lowerBracketParticipants.Count == 1)
                    _lowerBracketParticipants.RemoveAt(0);
            }
            else
            {
                _lowerBracketParticipants = round.PlayLowerBracket(InputWinner);

                if (UpperBracketParticipants.Count == 1 && _lowerBracketParticipants.Count == 1)
                {
                    UpperBracketParticipants.Add(_lowerBracketParticipants[0]);
                    _lowerBracketParticipants.RemoveAt(0);
                }
            }

            BinarySaver.SaveListToBinnary("upperBracket", UpperBracketParticipants);
            BinarySaver.SaveListToBinnary("lowerBracket", _lowerBracketParticipants);
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
