namespace Colors
{
    public interface IProcessor
    {
        void Process(Red red, Blue blue);
        //void Process(Blue blue, Red red);
        void Process(Blue blue1, Blue blue2);
        void Process(Red red1, Red red2);
    }

    public class Processor : IProcessor
    {
        public ColorCombination LastProcessed { get; private set; }

        public void Process(Red red, Blue blue)
        {
            LastProcessed = ColorCombination.RedBlue;
        }

        public void Process(Blue blue, Red red)
        {
            LastProcessed = ColorCombination.BlueRed;
        }

        public void Process(Blue blue1, Blue blue2)
        {
            LastProcessed = ColorCombination.BlueBlue;
        }

        public void Process(Red red1, Red red2)
        {
            LastProcessed = ColorCombination.RedRed;
        }
    }
}
