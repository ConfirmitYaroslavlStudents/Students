using System;

namespace Championship
{
    public class Node
    {
        public Game Match { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Stage { get; set; }

        public Node(int level)
        {
            Match = new Game();
            Left = null;
            Right = null;
            Stage = level;
        }

        public void GetOpponents()
        {
            if (Match.FirstTeam == null)
            {
                Match.FirstTeam = new Team(Left.Match.Winner);
            }

            if (Match.SecondTeam == null)
            {
                Match.SecondTeam = new Team(Right.Match.Winner);
            }
        }
    }
}
