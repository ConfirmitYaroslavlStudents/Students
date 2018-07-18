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

        private string EnterNames(int indexOfParticipant)
        {
            string nameOfNewParticipant;
            bool inList = false ;
            do
            {
                Console.Write("Name of {0} participant: ", indexOfParticipant + 1);
                nameOfNewParticipant = Console.ReadLine();

                if (nameOfNewParticipant.Length > _tournamentGrid.MaxLengthOfString())
                    Console.WriteLine("Maximum length of name is {0} ", _tournamentGrid.MaxLengthOfString());

                inList = IsInList(nameOfNewParticipant);
                if (inList)
                    Console.WriteLine("A participant with this name already exists. Rename current participant");

                if (String.IsNullOrEmpty(nameOfNewParticipant))
                    Console.WriteLine("You've entered an empty string. Rename current participant");

            } while (nameOfNewParticipant.Length > _tournamentGrid.MaxLengthOfString() || inList || String.IsNullOrEmpty(nameOfNewParticipant));

            return nameOfNewParticipant;
        }

        private bool IsInList(string newNaME)
        {
            bool isInList = false;
            foreach (Participant participant in _allParticipants)
                if (participant.Name.Equals(newNaME))
                    isInList = true;
            return isInList;
        }

        private void SetNames()
        {
            string[] mixedParticipants = new string[Amount];
            string nameOfNewParticipant;
            for (int i = 0; i < Amount; i++)
            {
                nameOfNewParticipant = EnterNames(i);
                int indexOfNewParticipant = Mix();
                mixedParticipants[indexOfNewParticipant] = nameOfNewParticipant;                          
            }
            for (int i=0;i<Amount;i++)
            {
                _allParticipants.Insert(i, new Participant(mixedParticipants[i]));
                _tournamentGrid.AddInitialParticipants(mixedParticipants[i], i);
            }
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
            _tournamentGrid.ChangeCurrentRound();
            List<Participant> currentParicipants = CreateCurrentPlayersList();

            _numberOfCurrentPlayers = currentParicipants.Count;

            if (currentParicipants.Count != 1)
                DetectWinners(currentParicipants);
            else
                _endOfTheGame = true;
            _tournamentGrid.ChangeStep();
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
            int roundWinners = _numberOfCurrentPlayers / 2 + _numberOfCurrentPlayers % 2;

            for (int i = 1; i < roundParticipants.Count; i += 2)
            {
                winner = ReadWinner(roundParticipants[i - 1].Name, roundParticipants[i].Name);
                AddWinner(winner, i / 2, roundWinners);
            }

            if (roundParticipants.Count % 2 == 1)
            {
                winner = roundParticipants[roundParticipants.Count - 1].Name;
                AddWinner(winner, roundWinners - 1, roundWinners);
            }
                
            CompleteTreeWithNullElements(roundWinners);

        }
        
        private string ReadWinner(string firstParicipantName, string secondParticipantName)
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

        private void CompleteTreeWithNullElements(int numberOfRoundWinners)
        {
            NearestPowerOfTwo(numberOfRoundWinners, out int nearestPowerofTwo, out int amountOfRounds);

            for (int i = Amount; i < nearestPowerofTwo; i++)
            {
                _allParticipants.Add(new Participant(null));
            }
        }

        private void AddWinner(string participantName, int line, int roundWinners)
        {
            _allParticipants.Add(new Participant(participantName));
            _tournamentGrid.NumberOfCurrentPlayers = _numberOfCurrentPlayers;
            _tournamentGrid.AddWinner( participantName, line, roundWinners);       
        }
           
    }
}
