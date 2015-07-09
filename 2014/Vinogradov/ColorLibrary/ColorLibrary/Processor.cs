namespace ColorLibrary
{
    public class Processor
    {
        public string Mix(Red first, Red second)
        {
            return Submix(first, second);
        }
        public string Mix(Green first, Green second)
        {
            return Submix(first, second);
        }
        public string Mix(Red first, Green second)
        {
            return Submix(first, second);
        }
        public string Mix(Green first, Red second)
        {
            return Submix(first, second);
        }

        private string Submix(IColored a, IColored b)
        {
            return a.Paint() + "_" + b.Paint();
        }
    }
}
