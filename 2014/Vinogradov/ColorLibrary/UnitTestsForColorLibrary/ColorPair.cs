using ColorLibrary;

namespace UnitTestsForColorLibrary
{
    public class ColorPair
    {
        public IColored FirstColor { get; set; }
        public IColored SecondColor { get; set; }
        public string Answer { get; set; }

        public ColorPair(IColored first, IColored second, string answer)
        {
            FirstColor = first;
            SecondColor = second;
            Answer = answer;
        }

        public delegate string Print(IColored a,IColored b);

        public Print HP1020 { get; set; }
    }
}
