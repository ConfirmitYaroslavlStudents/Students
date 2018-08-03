using System;
using System.Collections.Generic;

namespace Tournament
{
    public class DoubleEliminationTournament: SingleEliminationTournament
    {
        private List<Participant> _lowerBracketParticipants;
        private bool _isPlayingUpperBracket;
        private bool _isPlayingLastRound;

        public DoubleEliminationTournament(List<string> participants) : base(participants)
        {
            _lowerBracketParticipants = new List<Participant>();
        }

        public DoubleEliminationTournament() : base()
        {
            _lowerBracketParticipants = BinarySaver.LoadListFromBinnary<Participant>(LowerFileName);
        }

        public new void PlayGame(Func<string, string> inputWinner)
        {
            if (RoundBracket == null || GameIndex >= RoundBracket.Count / 2)
                OrganizeLowerBracketRound();

            var leftParticipant = RoundBracket[GameIndex * 2];
            var rightParticipant = RoundBracket[GameIndex * 2 + 1];
            var game = new Game(leftParticipant, rightParticipant);
            game.PlayGame(inputWinner, out string winner, out string loser);

            if (_isPlayingLastRound)
                UpperBracketParticipants[GameIndex].SetName(winner);           
            else if (_isPlayingUpperBracket)
            {
                UpperBracketParticipants[GameIndex].SetName(winner);
                if (_lowerBracketParticipants?.Count < GameIndex * 2)
                    _lowerBracketParticipants?.Insert(GameIndex, new Participant(loser));
                else
                    _lowerBracketParticipants?.Insert(GameIndex * 2, new Participant(loser));
            }
            else          
                _lowerBracketParticipants[GameIndex].SetName(winner);

            GameIndex++;
            SaveData();
        }

        private void OrganizeLowerBracketRound()
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

        public new void SaveData()
        {
            base.SaveData();
            BinarySaver.SaveListToBinnary(LowerFileName, _lowerBracketParticipants);
        }

    }
}
