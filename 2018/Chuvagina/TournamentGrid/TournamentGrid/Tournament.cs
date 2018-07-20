using System;
using System.Collections.Generic;

namespace TournamentGrid
{
    class Tournament
    {
        private List<Participant> _upperBracketParticipants;
        private List<Participant> _lowerBracketParticipants;
        ///private Grid _upperBracketGrid;
      ///  private Grid _lowerBracketGrid;
        private int _nearestPowerOfTwo;
        private int _maxLengthOfString=15;
        private int _numberOfCurrentPlayers;
        private int _currentRound = 0;
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
          
                Rounds();
            

           
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
           /// _upperBracketGrid = new Grid(Amount);
           /// _lowerBracketGrid = new Grid(Amount);
           /// _upperBracketGrid.AmountOfRounds = amountOfRounds;
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
                _upperBracketParticipants.Add(new Participant(NonMixedParticipants[indexOfParticipant]));              
                NonMixedParticipants.RemoveAt(indexOfParticipant);
            }
            if (_upperBracketParticipants.Count % 2 == 1)
            {
                _upperBracketParticipants[_upperBracketParticipants.Count - 1].Round++;
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
                
                if (nameOfNewParticipant.Length > _maxLengthOfString)
                    Console.WriteLine("Maximum length of name is {0} ", _maxLengthOfString);

                if (index>=0)
                    Console.WriteLine("A participant with this name already exists. Rename current participant");

                if (String.IsNullOrEmpty(nameOfNewParticipant))
                    Console.WriteLine("You've entered an empty string. Rename current participant");

            } while (nameOfNewParticipant.Length > _maxLengthOfString || index>=0 || String.IsNullOrEmpty(nameOfNewParticipant));

            return nameOfNewParticipant;
        }    

        private List<int> FindRoundParticipants()
        {
            List<int> RoundParticipantsIndexes = new List<int>();

            for (int i = 0; i < _upperBracketParticipants.Count; i++)
            {
                if (_upperBracketParticipants[i].Round == _currentRound)
                {
                    int insertDepth = RoundParticipantsIndexes.Count / 2;
                    RoundParticipantsIndexes.Add(i + insertDepth);
                }

            }
            return RoundParticipantsIndexes;

        }

        private void Rounds()
        {
            List<int> RoundParticipants = new List<int>();
            do
            {
                DrawTournamentGrid();
                 RoundParticipants = FindRoundParticipants();
                if (RoundParticipants.Count != 1)
                    DetectWinners(RoundParticipants);
                _currentRound++;
            } while (RoundParticipants.Count != 1);
            Console.WriteLine($"The winner of the tournament is \"{_upperBracketParticipants[RoundParticipants[0]].Name}\"");
        }

        private void DrawTournamentGrid()
        {
           int[] side = new int[_currentRound+2];
            foreach(var participant in _upperBracketParticipants)
            {
                side[participant.Round]++;
                string filling = "";
                if (participant.Round <= _currentRound && side[participant.Round] % 2 == 1)
                    filling = "-----"+'\u2ba7';
                else if (participant.Round <= _currentRound)
                    filling = "-----\u2ba5";
                if (participant.Round<=_currentRound)
                {
                 for (int i=0;i<participant.Round;i++)
                                    Console.Write("\t");

                Console.ForegroundColor = participant.Color;
                Console.WriteLine(participant.Name+filling);
                }
               
                
            }
        }

        private void DetectWinners(List<int> RoundParticipantsIndexes)
        {          
           for (int i = 1; i < RoundParticipantsIndexes.Count; i +=2)
            {
                int firstParticipantIndex = RoundParticipantsIndexes[i-1];
                int secondParticipantIndex = RoundParticipantsIndexes[i];
                int winner = GetWinnersIndex(_upperBracketParticipants[firstParticipantIndex].Name, _upperBracketParticipants[secondParticipantIndex].Name);
                int winnerIndex;
                int loserIndex;
                if (winner==1)
                {
                    winnerIndex = firstParticipantIndex;
                    loserIndex = secondParticipantIndex;             
                }
                    
                else
                {
                    winnerIndex = secondParticipantIndex;
                    loserIndex = firstParticipantIndex;
                }
                string winnerName = _upperBracketParticipants[winnerIndex].Name;
                int indexOfInsert = secondParticipantIndex - (secondParticipantIndex - firstParticipantIndex) / 2;
                _upperBracketParticipants[winnerIndex].Color = ConsoleColor.Green;
                _upperBracketParticipants.Insert(indexOfInsert, new Participant(winnerName,_currentRound+1));           
            }

            if (RoundParticipantsIndexes.Count%2==1)
            {
                _upperBracketParticipants[_upperBracketParticipants.Count - 1].Round++;
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
       
    }
}
