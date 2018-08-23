using Tournament;

namespace TournamentUI
{
    static class CloneParticipant
    {
        internal static UiParticipant Clone(Participant participant)
        {
            var newRoot = new UiParticipant(participant.Name, null);
            AddChildren(newRoot, participant.Left, participant.Right);
            return newRoot;
        }

        private static void AddChildren(UiParticipant newParticipant, Participant left, Participant right)
        {
            UiParticipant leftRoot = null;
            UiParticipant rightRoot = null;
            if (left != null)
                leftRoot = new UiParticipant(left.Name, newParticipant);

            if (right != null)
                rightRoot = new UiParticipant(right.Name, newParticipant);

            newParticipant.SetChildren(leftRoot, rightRoot);

            if (left != null && right != null)
            {
                AddChildren(leftRoot, left.Left, left.Right);
                AddChildren(rightRoot, right.Left, right.Right);
            }
        }

    }
}
