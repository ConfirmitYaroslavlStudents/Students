using System;
using System.Collections.Generic;

namespace TournamentGrid
{
    class Tournament
    {
        public int Amount { get; private set; }
        private List<Participant> _allParticipants;
        private Grid _tournamentGrid;
        private int _nearestPowerOfTwo;
        private int _numberOfCurrentPlayers;
        private bool _endOfTheGame = false;
        private bool[] _mix;

        public Tournament()
        {
            _allParticipants = new List<Participant>();
            _tournamentGrid = new Grid();
            Amount = 0;
        }

        public void SetParticipants()
        {
            SetAmount();
            SetNames();
        }

        public void Play()
        {
            while (!_endOfTheGame)
            {
                _tournamentGrid.DrawTournamentGrid();
                Round();
            }

            Console.WriteLine("The winner of the tournament is \"{0}\"", _allParticipants[_allParticipants.Count - 1].Name);
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
            _tournamentGrid.AmountOfRounds = amountOfRounds;
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
            string[] mixedParticipants = new string[Amount];
            string nameOfNewParticipant;
            for (int i = 0; i < Amount; i++)
            {
                nameOfNewParticipant = EnterNames(i,mixedParticipants);
                int indexOfNewParticipant = Mix();
                mixedParticipants[indexOfNewParticipant] = nameOfNewParticipant;
            }
            for (int i = 0; i < Amount; i++)
            {
                _allParticipants.Insert(i, new Participant(mixedParticipants[i]));
                _tournamentGrid.AddInitialParticipants(mixedParticipants[i], i);
            }
        }

        private string EnterNames(int indexOfParticipant, string[] mixedParticipants)
        {
            string nameOfNewParticipant;
            int index;
            do
            {
                Console.Write("Name of {0} participant: ", indexOfParticipant + 1);
                nameOfNewParticipant = Console.ReadLine();
                index= Array.IndexOf(mixedParticipants, nameOfNewParticipant);
                
                if (nameOfNewParticipant.Length > _tournamentGrid.MaxLengthOfString())
                    Console.WriteLine("Maximum length of name is {0} ", _tournamentGrid.MaxLengthOfString());

                if (index>=0)
                    Console.WriteLine("A participant with this name already exists. Rename current participant");

                if (String.IsNullOrEmpty(nameOfNewParticipant))
                    Console.WriteLine("You've entered an empty string. Rename current participant");

            } while (nameOfNewParticipant.Length > _tournamentGrid.MaxLengthOfString() || index>=0 || String.IsNullOrEmpty(nameOfNewParticipant));

            return nameOfNewParticipant;
        }    

        private int Mix()
        {
            int nextRandom;
            Random randomNumber = new Random();
            do
            {
                nextRandom=randomNumber.Next(Amount);

            } while (_mix[nextRandom]) ;

            _mix[nextRandom] = true;

            return nextRandom;
        }
 
        private void Round()
        {
            _tournamentGrid.IncreaseCurrentRound();
            List<Participant> currentParicipants = CreateCurrentPlayersList();

            _numberOfCurrentPlayers = currentParicipants.Count;

            if (currentParicipants.Count != 1)
                DetectWinners(currentParicipants);
            else
                _endOfTheGame = true;
            _tournamentGrid.IncreaseStep();
        }

        private List<Participant> CreateCurrentPlayersList()
        {
            List<Participant> winnersOfPreviousRound = new List<Participant>();

            foreach (Participant participant in _allParticipants)
            {
                if (participant != null && participant.Status)
                {
                    winnersOfPreviousRound.Add(participant);
                    participant.ChangeStatus();
                }
            }

            return winnersOfPreviousRound;
        }

        private void DetectWinners(List<Participant> roundParticipants)
        {
            string winner;
            int indexOfGameInCurrentRound=0;
            for (int i = 1; i < roundParticipants.Count; i += 2)
            {
                winner = GetWinnersName(roundParticipants[i - 1].Name, roundParticipants[i].Name);
                AddWinnerToNextRound(winner, indexOfGameInCurrentRound++);
            }

            if (roundParticipants.Count % 2 == 1)
            {
                winner = roundParticipants[roundParticipants.Count - 1].Name;
                AddWinnerToNextRound(winner, indexOfGameInCurrentRound);
            }
        }
        
        private string GetWinnersName(string firstParicipantName, string secondParticipantName)
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

            return winner;
        }

        private void AddWinnerToNextRound(string participantName, int indexOfGameInCurrentRound)
        {
            _allParticipants.Add(new Participant(participantName));
            _tournamentGrid.AddWinner( participantName, indexOfGameInCurrentRound,_numberOfCurrentPlayers);       
        }
           
    }
}
