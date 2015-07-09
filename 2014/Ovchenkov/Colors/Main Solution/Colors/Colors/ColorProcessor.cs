using Colors.Helper;

namespace Colors
{
    public enum TypeOfProcess
    {
        RedAndRed, GreenAndGreen, GreenAndRed, RedAndGreen
    }
    public class ColorsProcessor : IProcessor
    {
        //It is just for tests
        public TypeOfProcess LastProcess { get; private set; }

        public void Process(IColor colorOne, IColor colorTwo)
        {
            var helper = new ProcessHelper();
            colorOne.Accept(helper);
            colorTwo.Accept(helper);

            helper.Process(this);
            //через конструтор копирования
            helper.ProcessByConstructor(this);
        }

        public void Process(Red colorOne, Red colorTwo)
        {
            LastProcess = TypeOfProcess.RedAndRed;
        }

        public void Process(Green colorOne, Green colorTwo)
        {
            LastProcess = TypeOfProcess.GreenAndGreen;
        }

        public void Process(Red colorOne, Green colorTwo)
        {
            LastProcess = TypeOfProcess.RedAndGreen;
        }

        public void Process(Green colorOne, Red colorTwo)
        {
            LastProcess = TypeOfProcess.GreenAndRed;
        }
    }
}
