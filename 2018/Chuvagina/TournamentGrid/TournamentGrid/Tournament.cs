using System;
using System.Collections.Generic;

namespace TournamentGrid
{
    public class Tournament
    {
        public enum KindOfBracket
        {
            Vertical,
            Horizontal,
            PlayOff
        }

        private readonly List<Participant> _upperBracketParticipants = new List<Participant>();
        private readonly List<Participant> _lowerBracketParticipants=new List<Participant>();
        private int _maxLengthOfString=15;
        private int _currentRound = 0;
        private int _amount = 0;
        private bool _doubleEliminated ;
        private KindOfBracket _kindOfBracket;

        public void Play()
        {
            _kindOfBracket = DataInput.ChoseBracket();
            _doubleEliminated = _kindOfBracket != KindOfBracket.PlayOff && DataInput.ChoseSystem();
            SetAmount();
            SetNames();
            Rounds();
        }


        private void SetAmount()
        {
            int amountOfParticipants = 0;
            amountOfParticipants = DataInput.InputAmount(_kindOfBracket);
            _amount = amountOfParticipants;
        }
      
        private void SetNames()
        {
            List<string> nonMixedParticipants = new List<string>();

            for (int i = 0; i < _amount; i++)
            {
                var nameOfNewParticipant = DataInput.InputNames(i,_maxLengthOfString, nonMixedParticipants);
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
                Round round = new Round(_currentRound, _upperBracketParticipants, _lowerBracketParticipants,_kindOfBracket);
                isLastRound = _doubleEliminated ? round.PlayDoubleEliminatedRound() : round.PlayRound();
                _currentRound++;

            } while (!isLastRound);
               
             
        }
    
       
    }
}
