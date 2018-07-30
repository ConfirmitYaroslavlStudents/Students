using System.Collections.Generic;

namespace Tournament
{
    internal class Round
    {
        public List<Participant> UpperBracketParticipants { get; private set; }
        public List<Participant> LowerBracketParticipants { get; private set; }

        internal Round(List<Participant> upperBracketParticipants, List<Participant> lowerBracketParticipants)
        {
            UpperBracketParticipants = upperBracketParticipants;
            LowerBracketParticipants = lowerBracketParticipants;
        }

        internal Round(List<Participant> participants)
        {
            UpperBracketParticipants = participants;
        }

        internal void PlayUpperBracket()
        {
            List<Participant> upperBracket = new List<Participant>();

            for (int i = 0; i < UpperBracketParticipants.Count / 2; i++)
            {
                var leftParticipant = UpperBracketParticipants[i * 2];
                var rightParticipant = UpperBracketParticipants[i * 2 + 1];
                var game = new Game(leftParticipant, rightParticipant);
                game.PlayGame(out Participant winner, out Participant loser);
                upperBracket.Add(winner);

                if (LowerBracketParticipants?.Count < i * 2)
                    LowerBracketParticipants?.Insert(i, loser);
                else
                    LowerBracketParticipants?.Insert(i * 2, loser);
            }

            if (UpperBracketParticipants.Count % 2 == 1)
                upperBracket.Add(UpperBracketParticipants[UpperBracketParticipants.Count - 1]);

            UpperBracketParticipants = upperBracket;
        }

        internal List<Participant> PlayLowerBracket()
        {
            var lowerBracket = new List<Participant>();

            for (int i = 0; i < LowerBracketParticipants.Count / 2; i++)
            {
                var leftParticipant = LowerBracketParticipants[i * 2];
                var rightParticipant = LowerBracketParticipants[i * 2 + 1];
                var game = new Game(leftParticipant, rightParticipant);
                game.PlayGame(out Participant winner, out Participant loser);
                lowerBracket.Add(winner);
            }

            if (LowerBracketParticipants.Count % 2 == 1)
                lowerBracket.Add(LowerBracketParticipants[LowerBracketParticipants.Count - 1]);

            return lowerBracket;
        }

    }
}
