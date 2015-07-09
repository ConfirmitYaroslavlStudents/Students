using ColorLibrary;

namespace UnitTestsForColorLibrary
{
    class ColorPair
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
    }
}
