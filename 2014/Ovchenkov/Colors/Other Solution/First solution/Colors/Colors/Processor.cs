namespace Colors
{
    public enum TypeOfProcess
    {
        Red, Blue, Green, RedAndRed, RedAndBlue,
        BlueAndRed, BlueAndBlue, GreenAndGreen,
        GreenAndBlue, GreenAndRed, BlueAndGreen, RedAndGreen
    }
    public class Processor : IProcessor
    {
        //It is just for tests
        public TypeOfProcess LastProcess { get; private set; }

        public void Process(Red color)
        {
            LastProcess = TypeOfProcess.Red;
        }

        public void Process(Blue color)
        {
            LastProcess = TypeOfProcess.Blue;
        }

        public void Process(Green color)
        {
            LastProcess = TypeOfProcess.Green;
        }

        public void Process(Red colorOne, Red colorTwo)
        {
            LastProcess = TypeOfProcess.RedAndRed;
        }

        public void Process(Red colorOne, Blue colorTwo)
        {
            LastProcess = TypeOfProcess.RedAndBlue;
        }

        public void Process(Blue colorOne, Red colorTwo)
        {
            LastProcess = TypeOfProcess.BlueAndRed;
        }

        public void Process(Blue colorOne, Blue colorTwo)
        {
            LastProcess = TypeOfProcess.BlueAndBlue;
        }

        public void Process(Green colorOne, Green colorTwo)
        {
            LastProcess = TypeOfProcess.GreenAndGreen;
        }

        public void Process(Green colorOne, Red colorTwo)
        {
            LastProcess = TypeOfProcess.GreenAndRed;
        }

        public void Process(Red colorOne, Green colorTwo)
        {
            LastProcess = TypeOfProcess.RedAndGreen;
        }

        public void Process(Green colorOne, Blue colorTwo)
        {
            LastProcess = TypeOfProcess.GreenAndBlue;
        }

        public void Process(Blue colorOne, Green colorTwo)
        {
            LastProcess = TypeOfProcess.BlueAndGreen;
        }
    }
}
