using Tournament;

namespace TournamentUI
{
    internal class UiParticipant
    {
        internal string Name;
        internal int HorizontalAlignment;
        internal int VerticalAlignment;
        internal UiParticipant Left;
        internal UiParticipant Right;
        internal UiParticipant Winner;


        public UiParticipant(string name, UiParticipant winner)
        {
            Name = name;
            Winner = winner;
            HorizontalAlignment = 0;
            VerticalAlignment = 0;
        }

        public void SetChildren( UiParticipant left, UiParticipant right)
        {
            Left = left;
            Right = right;
        }

        public void AddAlignment(int horizontal, int vertical)
        {
            HorizontalAlignment = horizontal;
            VerticalAlignment = vertical;
        }

        


    }
}
