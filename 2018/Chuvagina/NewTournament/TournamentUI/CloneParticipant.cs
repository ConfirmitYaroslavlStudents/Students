using Tournament;

namespace TournamentUI
{
    static class CloneParticipant
    {
        public static UiParticipant Clone(Participant participant)
        {
            var newRoot = new UiParticipant(participant.Name, null);
            DeepToTree(newRoot, participant.Left, participant.Right);
            return newRoot;
        }

        public static void DeepToTree(UiParticipant newParticipant, Participant left, Participant right)
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
                DeepToTree(leftRoot, left.Left, left.Right);
                DeepToTree(rightRoot, right.Left, right.Right);
            }
        }

    }
}
