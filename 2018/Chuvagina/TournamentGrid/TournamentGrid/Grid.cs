using System;
using System.Collections.Generic;

namespace TournamentGrid
{
    class Grid
    {
        const int maxLengthOfString = 15;

        private List<ParticipantInGrid[]> ResultList = new List<ParticipantInGrid[]>();
        private int _step = 2;
        private int _currentRound=0;
        private int[] _amountOfRoundPlayers;
        public int AmountOfRounds;      

        public Grid(int amountOfParticipants)
        {
            _amountOfRoundPlayers = new int[amountOfParticipants];
            int initialAmount = amountOfParticipants;
            for (int i=0; i<amountOfParticipants;i++)
            {
                _amountOfRoundPlayers[i] = initialAmount;
                initialAmount = initialAmount / 2 + initialAmount % 2;
            }
        }

        public void IncreaseStep()
        {
            _step *= 2;
        }

        public int MaxLengthOfString()
        {
            return maxLengthOfString;
        }

        public void IncreaseCurrentRound()
        {
            _currentRound += 1;
        }

        public void DrawTournamentGrid()
        {
            Console.WriteLine();
            foreach (ParticipantInGrid[] Line in ResultList)
            {
                foreach (ParticipantInGrid Participant in Line)
                    if (Participant != null && Participant.IsSeen)
                    {
                        Console.ForegroundColor = Participant.Color;
                        Console.Write(String.Format("{0,-16:0.0}", Participant.Name));
                    }
                    else Console.Write(String.Format("{0,-16:0.0}", ""));
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ReadLine();
        }

        public void AddInitialParticipants(string newParticipant)
        {
            ParticipantInGrid[] ParticipantForGrid = new ParticipantInGrid[AmountOfRounds+1];
            bool isSeen = true;
            if (_amountOfRoundPlayers[_currentRound] % 2 == 1 && ResultList.Count == _amountOfRoundPlayers[0]-1)
                isSeen = false;
            ParticipantForGrid[_currentRound] = new ParticipantInGrid(newParticipant,isSeen);
            ResultList.Add(ParticipantForGrid);
        }

        public void AddWinner(string participantName, int indexOfGameInCurrentRound, int indexOfWinner)
        {
            int indexOfNewWinner = -1 + _step * (indexOfGameInCurrentRound + 1);
            
            if (indexOfWinner == 1)
                ResultList[indexOfNewWinner - _step / 2][_currentRound - 1].Color = ConsoleColor.Green;

            if (indexOfNewWinner >= ResultList.Count)
                indexOfNewWinner=ResultList.Count-1;

            if (indexOfWinner == 2)
                ResultList[indexOfNewWinner][_currentRound - 1].Color = ConsoleColor.Green;

            bool isSeen = true;
            if (_amountOfRoundPlayers[_currentRound] % 2 == 1 && indexOfNewWinner == ResultList.Count - 1 && indexOfGameInCurrentRound!=0)
                isSeen = false;

            ResultList[indexOfNewWinner][_currentRound] = new ParticipantInGrid(participantName,isSeen);
        }

        
    }
}
