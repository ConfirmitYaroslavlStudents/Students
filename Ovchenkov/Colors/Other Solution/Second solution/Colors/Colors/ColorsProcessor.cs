namespace Colors
{
    public enum TypeOfProcess
    {
        RedAndRed, GreenAndGreen, GreenAndRed, RedAndGreen
    }
    public class ColorsProcessor
    {
        //It is just for tests
        public static TypeOfProcess LastProcess { get; private set; }

        public static void Process(IColor colorOne, IColor colorTwo)
        {
            colorOne.FirstProcess(colorTwo);
        }

        public static void Process(Red colorOne, Red colorTwo)
        {
            LastProcess = TypeOfProcess.RedAndRed;
        }

        public static void Process(Green colorOne, Green colorTwo)
        {
            LastProcess = TypeOfProcess.GreenAndGreen;
        }

        public static void Process(Red colorOne, Green colorTwo)
        {
            LastProcess = TypeOfProcess.RedAndGreen;
        }

        public static void Process(Green colorOne, Red colorTwo)
        {
            LastProcess = TypeOfProcess.GreenAndRed;
        }
    }
}
