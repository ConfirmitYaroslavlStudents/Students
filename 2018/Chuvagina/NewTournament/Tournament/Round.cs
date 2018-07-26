using System;
using System.Collections.Generic;

namespace Tournament
{
    internal class Round
    {
        private enum _side
        {
            Left,
            Right
        }

        private List<Participant> _upperBracketParticipants;
        private List<Participant> _lowerBracketParticipants;

        public Round(List<Participant> upperBracketParticipants, List<Participant> lowerBracketParticipants)
        {
            _upperBracketParticipants = upperBracketParticipants;
            _lowerBracketParticipants = lowerBracketParticipants;
        }


        public Round(List<Participant> participants)
        {
            _upperBracketParticipants = participants;
        }

        public void PlayUpper(out List<Participant> upperBracket)
        {
            upperBracket = new List<Participant>();

            for (int i = 0; i < _upperBracketParticipants.Count / 2; i++)
            {
                var leftParticipant = _upperBracketParticipants[i * 2];
                var rightParticipant = _upperBracketParticipants[i * 2 + 1];
                var side = DetectWinner(leftParticipant, rightParticipant);
                string Winner = "";
                switch (side)
                {
                    case _side.Left:
                        Winner = leftParticipant.Name;
                        break;
                    case _side.Right:
                        Winner = rightParticipant.Name;
                        break;
                }

                var newParticipant = new Participant(Winner, leftParticipant, rightParticipant);
                leftParticipant.Winner = newParticipant;
                rightParticipant.Winner = newParticipant;
                upperBracket.Add(newParticipant);
            }

            if (_upperBracketParticipants.Count % 2 == 1)
                upperBracket.Add(_upperBracketParticipants[_upperBracketParticipants.Count - 1]);

        }

        public void PlayDoubleEliminationRound(out List<Participant> upperBracket, out List<Participant> lowerBracket)
        {
            if (_upperBracketParticipants.Count == 1 && _lowerBracketParticipants.Count == 1)
            {
                _upperBracketParticipants.Add(_lowerBracketParticipants[0]);
                _lowerBracketParticipants.RemoveAt(0);
                PlayUpper(out upperBracket);
                lowerBracket = _lowerBracketParticipants;
            }
            else
                PlayDoubleElimination(out upperBracket, out lowerBracket);
        }

        public void PlayDoubleElimination(out List<Participant> upperBracket, out List<Participant> lowerBracket)
        {

            upperBracket = new List<Participant>();
            lowerBracket = new List<Participant>();

            for (int i = 0; i < _upperBracketParticipants.Count / 2; i++)
            {
                var leftParticipant = _upperBracketParticipants[i * 2];
                var rightParticipant = _upperBracketParticipants[i * 2 + 1];
                var side = DetectWinner(leftParticipant, rightParticipant);
                string Winner="";
                string Loser="";
                switch (side)
                {
                    case _side.Left:
                        Winner = leftParticipant.Name;
                        Loser = rightParticipant.Name;
                        break;
                    case _side.Right:
                        Winner = rightParticipant.Name;
                        Loser = leftParticipant.Name;
                        break;
                }

                var newParticipant = new Participant(Winner,leftParticipant,rightParticipant);    
                leftParticipant.Winner = newParticipant;
                rightParticipant.Winner = newParticipant;
                upperBracket.Add(newParticipant);

                var newLowerBracketParticipant = new Participant(Loser);
                _lowerBracketParticipants.Insert(i, newLowerBracketParticipant);
            }

            if (_upperBracketParticipants.Count % 2 == 1)
                upperBracket.Add(_upperBracketParticipants[_upperBracketParticipants.Count - 1]);

            for (int i = 0; i < _lowerBracketParticipants.Count / 2; i++)
            {
                var leftParticipant = _lowerBracketParticipants[i * 2];
                var rightParticipant = _lowerBracketParticipants[i * 2 + 1];
                var side = DetectWinner(leftParticipant, rightParticipant);
                string Winner = "";
                string Loser = "";
                switch (side)
                {
                    case _side.Left:
                        Winner = leftParticipant.Name;
                        Loser = rightParticipant.Name;
                        break;
                    case _side.Right:
                        Winner = rightParticipant.Name;
                        Loser = leftParticipant.Name;
                        break;
                }

                var newParticipant = new Participant(Winner, leftParticipant, rightParticipant);
                leftParticipant.Winner = newParticipant;
                rightParticipant.Winner = newParticipant;
                lowerBracket.Add(newParticipant);
            }

            if (_lowerBracketParticipants.Count % 2 == 1)
                upperBracket.Add(_lowerBracketParticipants[_lowerBracketParticipants.Count - 1]);

        }


        private _side DetectWinner(Participant leftParticipant, Participant rightParticipant)
        {
            Console.WriteLine(leftParticipant.Name + " " + rightParticipant.Name);
            string Name = Console.ReadLine();
            if (Name.Equals(leftParticipant.Name))
                return _side.Left;
            else
                return _side.Right;
        }
    }
}
