using System;
using System.Collections.Generic;

namespace Tournament
{
    public class OrganizedTournament
    {
        public enum KindOfBracket
        {
            Vertical,
            Horizontal,
            PlayOff
        }

        private readonly List<Participant> _upperBracketParticipants = new List<Participant>();
        private readonly List<Participant> _lowerBracketParticipants = new List<Participant>();
        internal static int MaxLengthOfString = 10;
        private int _currentRound = 0;
        private int _amount = 0;
        private bool _doubleEliminated;
        internal static KindOfBracket BracketKind;

        public void Play()
        {
            BracketKind = DataInput.ChoseBracket();
            _doubleEliminated = BracketKind != KindOfBracket.PlayOff && DataInput.ChoseSystem();
            SetAmount();
            SetNames();
            Rounds();
        }


        private void SetAmount()
        {
            var amountOfParticipants = DataInput.InputAmount(BracketKind);
            _amount = amountOfParticipants;
        }

        private void SetNames()
        {
            var nonMixedParticipants = new List<string>();

            for (int i = 0; i < _amount; i++)
            {
                var nameOfNewParticipant = DataInput.InputNames(i, MaxLengthOfString, nonMixedParticipants);
                nonMixedParticipants.Add(nameOfNewParticipant);
            }

            RandomParticipants(nonMixedParticipants);
        }

        private void RandomParticipants(List<string> nonMixedParticipants)
        {
            Random random = new Random();

            while (nonMixedParticipants.Count > 0)
            {
                int indexOfParticipant = random.Next(nonMixedParticipants.Count);
                _upperBracketParticipants.Add(new Participant(nonMixedParticipants[indexOfParticipant]));
                nonMixedParticipants.RemoveAt(indexOfParticipant);
            }
        }

        private void Rounds()
        {
            bool isLastRound = false;

            do
            {
                Round round = new Round(_currentRound, _upperBracketParticipants, _lowerBracketParticipants);
                isLastRound = _doubleEliminated ? round.PlayDoubleEliminatedRound() : round.PlayRound();
                _currentRound++;

            } while (!isLastRound);


        }


    }
}

