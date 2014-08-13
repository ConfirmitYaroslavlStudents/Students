using System;

namespace Colors
{
    public interface IColor
    {
        void AcceptProcessor(IColor color, IProcessor processor);
        void RunProcessing(Red red, IProcessor processor);
        void RunProcessing(Blue blue, IProcessor processor);
    }

    public class Blue : IColor
    {
        public void AcceptProcessor(IColor color, IProcessor processor)
        {
            color.RunProcessing(this, processor);
        }

        public void RunProcessing(Red red, IProcessor processor)
        {
            processor.Process(red, this);
        }

        public void RunProcessing(Blue blue, IProcessor processor)
        {
            processor.Process(blue, this);
        }
    }

    public class Red : IColor
    {
        public void AcceptProcessor(IColor color, IProcessor processor)
        {
            color.RunProcessing(this, processor);
        }

        public void RunProcessing(Red red, IProcessor processor)
        {
            processor.Process(red, this);
        }

        public void RunProcessing(Blue blue, IProcessor processor)
        {
            //uncomment if we can process(blue, red)
            //processor.Process(blue, this);
            throw new ArgumentException("Can't process (Blue, Red)");
        }
    }
}
