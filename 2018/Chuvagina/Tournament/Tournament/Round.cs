using System.Collections.Generic;

namespace Tournament
{
    internal class Round
    {
        private readonly int _roundIndex;
        private readonly List<Participant> _upperBracket;
        private readonly List<Participant> _lowerBracket;
        private List<int> _upperBracketParticipantsIndexes;
        private List<int> _lowerBracketParticipantsIndexes;
        private bool _isLastRound = false;

        public Round(int index, List<Participant> upperBracket, List<Participant> lowerBracket)
        {
            _roundIndex = index;
            _upperBracket = upperBracket;
            _lowerBracket = lowerBracket;
            _upperBracketParticipantsIndexes = FindRoundParticipants(_upperBracket);
            _lowerBracketParticipantsIndexes = FindRoundParticipants(_lowerBracket);       
        }

        public bool PlayDoubleEliminatedRound()
        {
            int amountOfUpperBracketParticipants = AmountOfParticipants(_roundIndex, _upperBracket);
            int amountOfLowerBracketParticipants = AmountOfParticipants(_roundIndex, _lowerBracket);

            if (amountOfUpperBracketParticipants == 0)
                foreach (var participant in _upperBracket)
                    participant.Round++;

            if (amountOfLowerBracketParticipants == 1 && amountOfUpperBracketParticipants < 2)
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
            int amountOfUpperBracketParticipants = AmountOfParticipants(_roundIndex, _upperBracket);

            if (amountOfUpperBracketParticipants == 1)
            {
                var printUpper = new BracketPrint(_roundIndex, _upperBracket);
                printUpper.Print(BracketPrint.Bracket.Upper);
                return true;
            }

            PlayUpperBracketRound(_upperBracketParticipantsIndexes);
            return false;

        }


        private static int AmountOfParticipants(int roundIndex, List<Participant> bracket)
        {
            int amountOfParticipants = 0;

            foreach (var participant in bracket)
                if (participant.Round == roundIndex)
                    amountOfParticipants++;

            return amountOfParticipants;
        }

        private List<int> FindRoundParticipants(List<Participant> bracket)
        {
            var roundParticipantsIndexes = new List<int>();

            for (int i = 0; i < bracket.Count; i++)
            {
                if (bracket[i].Round == _roundIndex)
                {
                    int insertDepth = roundParticipantsIndexes.Count / 2;
                    roundParticipantsIndexes.Add(i + insertDepth);
                }
            }

            return roundParticipantsIndexes;

        }

        private void PlayUpperBracketRound(List<int> roundParticipantsIndexes)
        {
            HideNonPlayingParticipants(roundParticipantsIndexes, _upperBracket);

            var printUpper = new BracketPrint(_roundIndex, _upperBracket);
            printUpper.Print(BracketPrint.Bracket.Upper);
            var losers = new List<string>();

            for (int i = 1; i < roundParticipantsIndexes.Count; i += 2)
            {
                var game = new Game(_roundIndex, _upperBracket, _lowerBracket);
                losers.Add(game.PlayUpperBracketGame(roundParticipantsIndexes[i - 1], roundParticipantsIndexes[i]));
            }

            if (!_isLastRound)
                AddToLowerBracket(losers);
        }

        private void AddToLowerBracket(List<string> losers)
        {
            int lowerBracketAmountOfPlayers = _lowerBracket.Count;

            for (int i = losers.Count - 1; i >= 0; i--)
            {
                int insertIndex = i * (lowerBracketAmountOfPlayers / losers.Count);
                _lowerBracket.Insert(insertIndex, new Participant(losers[i], _roundIndex));
            }

        }

        private void PlayLowerBracketRound(List<int> roundParticipantsIndexes)
        {
            HideNonPlayingParticipants(roundParticipantsIndexes, _lowerBracket);

            var printLower = new BracketPrint(_roundIndex, _lowerBracket);
            printLower.Print(BracketPrint.Bracket.Lower);

            for (int i = 1; i < roundParticipantsIndexes.Count; i += 2)
            {
                var game = new Game(_roundIndex, _upperBracket, _lowerBracket);
                game.PlayLowerBracketGame(roundParticipantsIndexes[i - 1], roundParticipantsIndexes[i]);
            }
        }

        private static void HideNonPlayingParticipants(List<int> participants, List<Participant> bracket)
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

            var printUpper = new BracketPrint(_roundIndex + 1, _upperBracket);
            printUpper.Print(BracketPrint.Bracket.Upper);

            var printLower = new BracketPrint(_roundIndex + 1, _lowerBracket);
            printLower.Print(BracketPrint.Bracket.Lower);
        }
    }
}