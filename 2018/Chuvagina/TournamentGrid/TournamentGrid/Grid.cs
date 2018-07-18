using System;
using System.Collections.Generic;

namespace TournamentGrid
{
    class Grid
    {
        private List<string> ResultLines;
        public int NumberOfCurrentPlayers;
        public int AmountOfRounds;
        private int _step = 2;
        private int _currentRound=0;
        const int maxLengthOfString = 15;

        public Grid()
        {
            ResultLines = new List<string>();
        }

        public void ChangeStep()
        {
            _step *= 2;
        }

        public int MaxLengthOfString()
        {
            return maxLengthOfString;
        }

        public void ChangeCurrentRound()
        {
            _currentRound += 1;
        }

        public void Add(string newParticipant)
        {
            ResultLines.Add(newParticipant);
        }

        public void DrawTournamentGrid()
        {
            Console.WriteLine();
            foreach (string line in ResultLines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        public void AddInitialParticipants(string nameOfNewParticipant, int indexOfNewParticipant)
        {
            string participantGridName = SetString(nameOfNewParticipant, maxLengthOfString, '_');
            if (indexOfNewParticipant % 2 == 1)
                Add(participantGridName + "|");
            else
                Add(participantGridName + " ");
        }

        public void AddWinner(string participantName, int line, int roundWinners)
        {
            int amountOfNextRoundPlayers = NumberOfCurrentPlayers / 2 + NumberOfCurrentPlayers % 2;
            int indexOfCurrentLine = (line + 1) * _step - 1;
            bool playingNextRound = (line != roundWinners - 1);

            if (_currentRound == AmountOfRounds)
            {
                ResultLines[ResultLines.Count-1] += participantName;
            }

            else if (playingNextRound)
                AddRowInGrid(participantName, indexOfCurrentLine, line, line == roundWinners - 1);            

            else if (amountOfNextRoundPlayers % 2 == 0)
                AddTheOnlyParticipant(participantName, "|");

            else if (amountOfNextRoundPlayers % 2 == 1)
                AddTheOnlyParticipant(participantName, " ");

        }

        private void AddRowInGrid(string participantName, int indexOfCurrentLine, int line, bool isLast)
        {
            string participantGridName;
            bool isLastRound = _currentRound ==AmountOfRounds;

            participantGridName = SetString(participantName, maxLengthOfString, '_');
            ResultLines[indexOfCurrentLine] = ResultLines[indexOfCurrentLine] + participantGridName;
            if (!isLast)
            {
                if (line % 2 == 0)
                    AddStrategies(indexOfCurrentLine);
                else
                    ResultLines[indexOfCurrentLine] = ResultLines[indexOfCurrentLine] + "|";
            }
            else
            {
                int lastLine = ResultLines.Count - 1;
                participantGridName = SetString(participantName, maxLengthOfString, '_');
                ResultLines[lastLine] = ResultLines[lastLine] + "|" + participantGridName;
            }
            if (_currentRound == AmountOfRounds - 1 && isLast)
            {
                ResultLines[ResultLines.Count - 1] = ResultLines[ResultLines.Count - 1] + "|";
            }
        }


        private void AddStrategies(int indexOfCurrentLine)
        {
             for (int i = 1; i < _step; i++)
             {
                  if (indexOfCurrentLine + i + 1 < ResultLines.Count)
                  {
                      int lengthOfNextLine = ResultLines[indexOfCurrentLine + i].Length; ;
                      string lost = "";
                      int amountOfLostChars = (_currentRound + 1) * (maxLengthOfString + 1) - lengthOfNextLine - 1;
                      lost = SetString(lost, amountOfLostChars, ' ');
                      ResultLines[indexOfCurrentLine + i] = ResultLines[indexOfCurrentLine + i] + lost + "|";
                  }                        
             }
        }

        private void AddTheOnlyParticipant(string participantName, string adding)
        {
            ResultLines[ResultLines.Count - 1] = ResultLines[ResultLines.Count - 1] + SetString(participantName, maxLengthOfString, '_') + adding;
        }


        private string SetString(string inputString, int lostChars, char filling)
        {
            for (int i = inputString.Length; i < lostChars; i++)
            {
                inputString += filling;
            }

            return inputString;
        }
    }
}
