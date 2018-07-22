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
        private bool _isLastRound = false;

        public Round(int index, List<Participant> UpperBracket, List<Participant> LowerBracket)
        {
            _roundIndex = index;
            _upperBracket = UpperBracket;
            _lowerBracket = LowerBracket;
        }

        public bool PlayRound()
        {
            List<int> UpperBracketParticipantsIndexes = new List<int>();
            UpperBracketParticipantsIndexes = FindRoundParticipants(_upperBracket);
            PlayUpperBracketRound(UpperBracketParticipantsIndexes);

            List<int> LowerBracketParticipantsIndexes = new List<int>();
            LowerBracketParticipantsIndexes = FindRoundParticipants(_lowerBracket);

            if (LowerBracketParticipantsIndexes.Count == 1 )
            {
                _upperBracket.Add(new Participant(_lowerBracket[LowerBracketParticipantsIndexes[0]].Name,_roundIndex));
                UpperBracketParticipantsIndexes = FindRoundParticipants(_upperBracket);
                PlayUpperBracketRound(UpperBracketParticipantsIndexes);
                Console.Clear();
                Console.WriteLine("------Upper Bracket-----");
                BracketOutput.PrintGorizontalGrid(_roundIndex+1, _upperBracket);
                Console.WriteLine("------Lower Bracket-----");
                BracketOutput.PrintGorizontalGrid(_roundIndex, _lowerBracket);
                return true;
            }

            PlayLowerBracketRound(LowerBracketParticipantsIndexes);
            return false;
        }


        public void PlayUpperBracketRound(List<int> RoundParticipantsIndexes)
        {          
            if (RoundParticipantsIndexes.Count % 2 == 1 && RoundParticipantsIndexes.Count != 1)
                _upperBracket[_upperBracket.Count - 1].Round++;
            Console.Clear();
            Console.WriteLine("------Upper Bracket-----");
            BracketOutput.PrintGorizontalGrid(_roundIndex, _upperBracket);

            for (int i = 1; i < RoundParticipantsIndexes.Count; i += 2)
            {
                Game game = new Game(i, _roundIndex, _upperBracket, _lowerBracket);
                game.PlayUpperBracketGame(RoundParticipantsIndexes[i - 1], RoundParticipantsIndexes[i], RoundParticipantsIndexes.Count);
            }
    
        }

        public void PlayLowerBracketRound(List <int> RoundParticipantsIndexes)
        {
            if (RoundParticipantsIndexes.Count % 2 == 1 && RoundParticipantsIndexes.Count != 1)
                _lowerBracket[RoundParticipantsIndexes[RoundParticipantsIndexes.Count - 1]- RoundParticipantsIndexes.Count/2].Round++;
            Console.Clear();
            Console.WriteLine("------Upper Bracket-----");
            BracketOutput.PrintGorizontalGrid(_roundIndex, _upperBracket);
            Console.WriteLine("------Lower Bracket-----");
            BracketOutput.PrintGorizontalGrid(_roundIndex, _lowerBracket);

            for (int i = 1; i < RoundParticipantsIndexes.Count; i += 2)
            {
                Game game = new Game(i, _roundIndex, _upperBracket, _lowerBracket);
                game.PlayLowerBracketGame(RoundParticipantsIndexes[i - 1], RoundParticipantsIndexes[i], RoundParticipantsIndexes.Count, _isLastRound);
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
