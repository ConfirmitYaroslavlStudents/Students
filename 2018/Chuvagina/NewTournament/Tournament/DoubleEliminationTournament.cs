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

        public new void GetPlayingParticipants(out Participant leftParticipant, out Participant rightParticipant)
        {
            if (RoundBracket == null || GameIndex >= RoundBracket.Count / 2)
                OrganizeRound();

            LeftParticipant = leftParticipant = RoundBracket[GameIndex * 2];
            RightParticipant = rightParticipant = RoundBracket[GameIndex * 2 + 1];
        }

        public new void PlayGame(Participant winner)
        {
            Participant loser;
            if (winner == LeftParticipant)
                loser = RightParticipant;
            else
                loser = LeftParticipant;

            if (_isPlayingLastRound)
                UpperBracketParticipants[GameIndex].SetName(winner.Name);           
            else if (_isPlayingUpperBracket)
            {
                UpperBracketParticipants[GameIndex].SetName(winner.Name);
                if (_lowerBracketParticipants?.Count < GameIndex * 2)
                    _lowerBracketParticipants?.Insert(GameIndex, new Participant(loser.Name));
                else
                    _lowerBracketParticipants?.Insert(GameIndex * 2, new Participant(loser.Name));
            }
            else          
                _lowerBracketParticipants[GameIndex].SetName(winner.Name);

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
