using System;
using System.Collections.Generic;

namespace Tournament
{
    [Serializable]
    public class DoubleEliminationTournament: SingleEliminationTournament
    {
        private List<Participant> _lowerBracketParticipants;
        private bool _isPlayingUpperBracket;
        private bool _isPlayingLastRound;

        public DoubleEliminationTournament(List<string> participants) : base(participants)
        {
            _lowerBracketParticipants = new List<Participant>();
        }

        public new Participant GetPlayingParticipants()
        {
            if (RoundBracket == null || GameIndex >= RoundBracket.Count / 2)
                OrganizeRound();

            Participant meeting = null;

            if (_isPlayingUpperBracket)
                meeting = UpperBracketParticipants[GameIndex];
            else
                meeting = _lowerBracketParticipants[GameIndex];

            return meeting;
        }

        public new void PlayGame(Side side)
        {
            string loser = null;
            if (side==Side.Left)
                loser = UpperBracketParticipants[GameIndex].Right.Name;
            else
                loser = UpperBracketParticipants[GameIndex].Left.Name;

            if (_isPlayingLastRound)
                UpperBracketParticipants[GameIndex].SetName(side);
            else if (_isPlayingUpperBracket)
            {
                UpperBracketParticipants[GameIndex].SetName(side);
                if (_lowerBracketParticipants?.Count < GameIndex * 2)
                    _lowerBracketParticipants?.Insert(GameIndex, new Participant(loser));
                else
                    _lowerBracketParticipants?.Insert(GameIndex * 2, new Participant(loser));
            }
            else          
                _lowerBracketParticipants[GameIndex].SetName(side);

            GameIndex++;
            BinarySaver.SaveDoubleToBinnary(this);
        }

        private void OrganizeRound()
        {
            _isPlayingLastRound = false;
            if (UpperBracketParticipants.Count == 1 && _lowerBracketParticipants.Count == 1)
            {
                UpperBracketParticipants.Add(_lowerBracketParticipants[0]);
                _lowerBracketParticipants.RemoveAt(0);
                _isPlayingLastRound = true;
            }

            if (UpperBracketParticipants.Count > _lowerBracketParticipants.Count)
            {
                OrganizeRound(ref UpperBracketParticipants);
                _isPlayingUpperBracket = true;
            }
            else
            {
                OrganizeRound(ref _lowerBracketParticipants);
                _isPlayingUpperBracket = false;
            }
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
