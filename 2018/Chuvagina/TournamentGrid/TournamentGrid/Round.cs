using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TournamentGrid.Tournament;

namespace TournamentGrid
{
    internal class Round
    {
        private int _roundIndex;
        private List<Participant> _upperBracket;
        private List<Participant> _lowerBracket;
        private List<int> _upperBracketParticipantsIndexes = new List<int>();
        private List<int> _lowerBracketParticipantsIndexes = new List<int>();
        private bool _isLastRound = false;
        private KindOfBracket _kindOfBracket;

        public Round(int index, List<Participant> UpperBracket, List<Participant> LowerBracket,KindOfBracket kindOfBracket)
        {
            _roundIndex = index;
            _upperBracket = UpperBracket;
            _lowerBracket = LowerBracket;
            _upperBracketParticipantsIndexes= FindRoundParticipants(_upperBracket);
            _lowerBracketParticipantsIndexes = FindRoundParticipants(_lowerBracket);
            _kindOfBracket = kindOfBracket;

        }

        public bool PlayDoubleEliminatedRound()
        {
            int AmountOfUpperBracketParticipants = AmountOfParticipants(_roundIndex, _upperBracket);
            int AmountOfLowerBracketParticipants = AmountOfParticipants(_roundIndex, _lowerBracket);
         
            if (AmountOfUpperBracketParticipants == 0)
                foreach (var participant in _upperBracket)
                    participant.Round++;
            
            if (AmountOfLowerBracketParticipants==1 && AmountOfUpperBracketParticipants < 2)
            {
                PlayLastRound();
                return true;
            }

            PlayUpperBracketRound(_upperBracketParticipantsIndexes);
            _lowerBracketParticipantsIndexes = FindRoundParticipants(_lowerBracket);
            PlayLowerBracketRound(_lowerBracketParticipantsIndexes);
            
            return false;
        }

        public bool PlayRound()
        {
            int AmountOfUpperBracketParticipants = AmountOfParticipants(_roundIndex, _upperBracket);

            if (AmountOfUpperBracketParticipants == 1)
            {
                Console.Clear();
                BracketOutput printUpper = new BracketOutput(_roundIndex, _upperBracket, _kindOfBracket);
                return true;      
            }
                     

            PlayUpperBracketRound(_upperBracketParticipantsIndexes);      

            return false;

        }


            private int AmountOfParticipants(int roundIndex, List<Participant> bracket)
        {
            int amountOfParticipants = 0;

            foreach (var participant in bracket)
                if (participant.Round == roundIndex)
                    amountOfParticipants++;

            return amountOfParticipants;
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

        private void PlayUpperBracketRound(List<int> RoundParticipantsIndexes)
        {
            HideNonPlayingParticipants(RoundParticipantsIndexes, _upperBracket);

            Console.Clear();
            BracketOutput printUpper = new BracketOutput(_roundIndex, _upperBracket, _kindOfBracket );
            printUpper.PrintBracket();
            List<string> Losers = new List<string>();

            for (int i = 1; i < RoundParticipantsIndexes.Count; i += 2)
            {
                Game game = new Game(i, _roundIndex, _upperBracket, _lowerBracket);
                Losers.Add(game.PlayUpperBracketGame(RoundParticipantsIndexes[i - 1], RoundParticipantsIndexes[i]));
            }

            if (!_isLastRound)
                AddToLowerBracket(Losers);

            Console.WriteLine(Losers.Count);
        }     

        private void AddToLowerBracket(List<string> losers)
        {
            int LowerBracketAmountOfPlayers = _lowerBracket.Count;
            int insertIndex;

            for (int i = losers.Count-1; i>=0;i--)
            {
                insertIndex = i * (LowerBracketAmountOfPlayers / losers.Count);
                _lowerBracket.Insert(insertIndex, new Participant(losers[i],_roundIndex));
            }

        }

        private void PlayLowerBracketRound(List <int> RoundParticipantsIndexes)
        {
            HideNonPlayingParticipants(RoundParticipantsIndexes, _lowerBracket);
            BracketOutput printUpper = new BracketOutput(_roundIndex, _upperBracket,_kindOfBracket);
            printUpper.PrintBracket();
            BracketOutput printLower = new BracketOutput(_roundIndex, _lowerBracket,_kindOfBracket);
            printLower.PrintBracket();

            for (int i = 1; i < RoundParticipantsIndexes.Count; i += 2)
            {
                Game game = new Game(i, _roundIndex, _upperBracket, _lowerBracket);
                game.PlayLowerBracketGame(RoundParticipantsIndexes[i - 1], RoundParticipantsIndexes[i]);
            }   

        }

        private void HideNonPlayingParticipants(List<int> participants, List <Participant> bracket)
        {
            int indexOfNonPlayingParticipant = participants[participants.Count - 1] - participants.Count / 2;

            if (participants.Count % 2 == 1 && participants.Count != 1)
                bracket[indexOfNonPlayingParticipant].Round++;
        }

        private void PlayLastRound()
        {
            _upperBracket.Add(new Participant(_lowerBracket[_lowerBracketParticipantsIndexes[0]].Name, _roundIndex));
            _upperBracketParticipantsIndexes = FindRoundParticipants(_upperBracket);
            _isLastRound = true;

            PlayUpperBracketRound(_upperBracketParticipantsIndexes);
            foreach (var participant in _lowerBracket)
                participant.Round++;

            Console.Clear();
            BracketOutput printUpper = new BracketOutput(_roundIndex+1, _upperBracket,_kindOfBracket);
            printUpper.PrintBracket();
            BracketOutput printLower = new BracketOutput(_roundIndex+1, _lowerBracket,_kindOfBracket);
            printLower.PrintBracket();
        }

    }
}
