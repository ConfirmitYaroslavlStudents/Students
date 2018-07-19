using System;
using System.Collections.Generic;

namespace TournamentGrid
{
    class Tournament
    {
        private List<Participant> _upperBracketParticipants;
        private List<Participant> _lowerBracketParticipants;
        private Grid _upperBracketGrid;
        private Grid _lowerBracketGrid;
        private int _nearestPowerOfTwo;
        private int _numberOfCurrentPlayers;
        private bool _endOfTheGame = false;
        private bool[] _mix;

        public Tournament()
        {
            _upperBracketParticipants = new List<Participant>();
            _lowerBracketParticipants = new List<Participant>();
            Amount = 0;
        }

        public int Amount { get; private set; }

        public void SetParticipants()
        {
            SetAmount();
            SetNames();
        }

        public void Play()
        {
            while (!_endOfTheGame)
            {
                _upperBracketGrid.DrawTournamentGrid();
                _lowerBracketGrid.DrawTournamentGrid();
                Round();
            }

            Console.WriteLine("The winner of the tournament is \"{0}\"", _upperBracketParticipants[_upperBracketParticipants.Count - 1].Name);
        }

        private void SetAmount()
        {
            int amountOfParticipants = 0;
            bool succesParse;

            do
            {
                Console.Write("Amount of participants: ");
                try
                {
                    amountOfParticipants = Int32.Parse(Console.ReadLine());
                    succesParse = true;
                }
                catch
                {
                    succesParse = false;
                }

            } while (!succesParse);

            Amount = amountOfParticipants;
            _mix = new bool[Amount];
            NearestPowerOfTwo(Amount, out _nearestPowerOfTwo, out int amountOfRounds);
            _numberOfCurrentPlayers = Amount;
            _upperBracketGrid = new Grid(Amount);
            _lowerBracketGrid = new Grid(Amount);
            _upperBracketGrid.AmountOfRounds = amountOfRounds;
        }

        private void NearestPowerOfTwo(int inputNumber, out int nearestPowerOfTwo, out int amountOfRounds)
        {
            nearestPowerOfTwo = 1;
            amountOfRounds = 0;
            while (nearestPowerOfTwo < inputNumber)
            {
                nearestPowerOfTwo *= 2;
                amountOfRounds++;
            }
        }

        private void SetNames()
        {
            List<string> NonMixedParticipants = new List<string>();
            string nameOfNewParticipant;
            for (int i = 0; i < Amount; i++)
            {
                nameOfNewParticipant = EnterNames(i,NonMixedParticipants);
                NonMixedParticipants.Add(nameOfNewParticipant);
            }
            Random random = new Random();
            while (NonMixedParticipants.Count>0)
            {
                int indexOfParticipant = random.Next(NonMixedParticipants.Count);
                _upperBracketParticipants.Add(new Participant( NonMixedParticipants[indexOfParticipant]));
                _upperBracketGrid.AddInitialParticipants(NonMixedParticipants[indexOfParticipant]);
                NonMixedParticipants.RemoveAt(indexOfParticipant);
            }
        
        }

        private string EnterNames(int indexOfParticipant, List<string>nonMixedParticipants)
        {
            string nameOfNewParticipant;
            int index;
            do
            {
                Console.Write("Name of {0} participant: ", indexOfParticipant + 1);
                nameOfNewParticipant = Console.ReadLine();
                index= nonMixedParticipants.IndexOf(nameOfNewParticipant);
                
                if (nameOfNewParticipant.Length > _upperBracketGrid.MaxLengthOfString())
                    Console.WriteLine("Maximum length of name is {0} ", _upperBracketGrid.MaxLengthOfString());

                if (index>=0)
                    Console.WriteLine("A participant with this name already exists. Rename current participant");

                if (String.IsNullOrEmpty(nameOfNewParticipant))
                    Console.WriteLine("You've entered an empty string. Rename current participant");

            } while (nameOfNewParticipant.Length > _upperBracketGrid.MaxLengthOfString() || index>=0 || String.IsNullOrEmpty(nameOfNewParticipant));

            return nameOfNewParticipant;
        }    

        private void Round()
        {
            _upperBracketGrid.IncreaseCurrentRound();

            _numberOfCurrentPlayers = _upperBracketParticipants.Count;

            if (_upperBracketParticipants.Count != 1)
                DetectWinners();
            else
                _endOfTheGame = true;
            _upperBracketGrid.IncreaseStep();
        }


        private void DetectWinners()
        {
            int winnerIndex;
            int indexOfGameInCurrentRound=0;
            int roundTours = _upperBracketParticipants.Count / 2;

            for (int i = 0; i < roundTours; i ++)
            {
                winnerIndex = GetWinnersIndex(_upperBracketParticipants[i].Name, _upperBracketParticipants[i+1].Name);
                Participant winner;
                Participant loser;
                if (winnerIndex==1)
                {
                    winner =_upperBracketParticipants[i];
                    loser= _upperBracketParticipants[i+1];             
                }
                    
                else
                {
                    winner = _upperBracketParticipants[i+1];
                    loser = _upperBracketParticipants[i];
                }
                AddWinnerToNextRound(winner.Name, indexOfGameInCurrentRound++, winnerIndex);
                _upperBracketParticipants.Remove(loser);
                _lowerBracketParticipants.Add(loser);
                _lowerBracketGrid.AddInitialParticipants(loser.Name);

            }

            if (_upperBracketParticipants.Count != roundTours)
            {              
                AddWinnerToNextRound(_upperBracketParticipants[roundTours].Name, indexOfGameInCurrentRound,2);
            }
            
        }
        
        private int GetWinnersIndex(string firstParicipantName, string secondParticipantName)
        {
            bool firstIsWinner = false;
            bool secondIsWinner = false;
            string winner = "";
            do
            {
                Console.Write("The winner of the game between \"{0}\" and \"{1}\" is ", firstParicipantName, secondParticipantName);
                winner = Console.ReadLine();
                firstIsWinner = winner.Equals(firstParicipantName);
                secondIsWinner = winner.Equals(secondParticipantName);

            } while (!firstIsWinner && !secondIsWinner);

            if (firstIsWinner)
                return 1;
            else
                return 2;
        }

        private void AddWinnerToNextRound(string participantName, int indexOfGameInCurrentRound, int index)
        {
            _upperBracketGrid.AddWinner( participantName, indexOfGameInCurrentRound,index);       
        }
           
    }
}
