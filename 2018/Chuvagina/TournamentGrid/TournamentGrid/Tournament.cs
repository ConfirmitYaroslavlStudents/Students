using System;
using System.Collections.Generic;
using System.Text;

namespace TournamentGrid
{
    public class Tournament
    {
        private List<Participant> _upperBracketParticipants = new List<Participant>();
        private List<Participant> _lowerBracketParticipants=new List<Participant>();
        private int _maxLengthOfString=15;
        private int _currentRound = 0;
        private int Amount = 0;


        public void Play()
        {
                SetAmount();
                SetNames();
                Rounds();
              
        }

        private void SetAmount()
        {
            int amountOfParticipants = 0;
            amountOfParticipants = DataInput.InputInt("Amount of participants: ");
            Amount = amountOfParticipants;
        }
      
        private void SetNames()
        {
            List<string> NonMixedParticipants = new List<string>();
            string nameOfNewParticipant;

            for (int i = 0; i < Amount; i++)
            {
                string message = String.Format("Name of {0} participant: ", i + 1);
                nameOfNewParticipant = DataInput.InputNames(message, _maxLengthOfString, NonMixedParticipants);
                NonMixedParticipants.Add(nameOfNewParticipant);
            }

            RandomParticipants(NonMixedParticipants);
        }
   
        private void RandomParticipants(List<string> NonMixedParticipants)
        {
            Random random = new Random();
            while (NonMixedParticipants.Count > 0)
            {
                int indexOfParticipant = random.Next(NonMixedParticipants.Count);
                _upperBracketParticipants.Add(new Participant(NonMixedParticipants[indexOfParticipant]));
                NonMixedParticipants.RemoveAt(indexOfParticipant);
            }

        }

       

        private void Rounds()
        {
            bool isLastRound = false;
            do
            {
                Round round = new Round(_currentRound, _upperBracketParticipants, _lowerBracketParticipants);
                isLastRound = round.PlayRound();
                _currentRound++;
            } while (!isLastRound);
               
             
        }
    
       
    }
}
