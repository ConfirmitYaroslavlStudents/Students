using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentGrid
{
    public class Round
    {
        private int _roundIndex;
        private List<Participant> _upperBracket;
        private List<Participant> _lowerBracket;

        public Round(int index, List<Participant> UpperBracket, List<Participant> LowerBracket)
        {
            _roundIndex = index;
            _upperBracket = UpperBracket;
            _lowerBracket = LowerBracket;
        }

        public void PlayUpperBracketRound()
        {
            List<int> RoundParticipantsIndexes = new List<int>();
            RoundParticipantsIndexes = FindRoundParticipants(_upperBracket);
            if (RoundParticipantsIndexes.Count % 2 == 1 && RoundParticipantsIndexes.Count != 1)
                _upperBracket[_upperBracket.Count - 1].Round++;
            Console.WriteLine("------Upper Bracket-----");
            BracketOutput.PrintGorizontalGrid(_roundIndex, _upperBracket);

            for (int i = 1; i < RoundParticipantsIndexes.Count; i += 2)
            {
                Game game = new Game(i, _roundIndex, _upperBracket, _lowerBracket);
                game.PlayUpperBracketGame(RoundParticipantsIndexes[i - 1], RoundParticipantsIndexes[i], RoundParticipantsIndexes.Count);
            }
    
        }

        public void PlayLowerBracketRound()
        {
            List<int> RoundParticipantsIndexes = new List<int>();
            RoundParticipantsIndexes = FindRoundParticipants(_lowerBracket);
            if (RoundParticipantsIndexes.Count % 2 == 1 && RoundParticipantsIndexes.Count != 1)
                _lowerBracket[RoundParticipantsIndexes[RoundParticipantsIndexes.Count - 1]- RoundParticipantsIndexes.Count/2].Round++;
            Console.WriteLine("------Lower Bracket-----");
            BracketOutput.PrintGorizontalGrid(_roundIndex, _lowerBracket);

            bool isLastRound = RoundParticipantsIndexes.Count / 2+ RoundParticipantsIndexes.Count% 2 == 1;
            for (int i = 1; i < RoundParticipantsIndexes.Count; i += 2)
            {
                Game game = new Game(i, _roundIndex, _upperBracket, _lowerBracket);
                game.PlayLowerBracketGame(RoundParticipantsIndexes[i - 1], RoundParticipantsIndexes[i], RoundParticipantsIndexes.Count, isLastRound);
            }   

        }

        private List<int> FindRoundParticipants(List<Participant> Bracket)
        {
            List<int> RoundParticipantsIndexes = new List<int>();

            for (int i = 0; i < Bracket.Count; i++)
            {
                if (Bracket[i].Round == _roundIndex)
                {
                    int insertDepth = RoundParticipantsIndexes.Count / 2;
                    RoundParticipantsIndexes.Add(i + insertDepth);
                }

            }
            return RoundParticipantsIndexes;

        }
    }
}
