using System;

namespace Colors.Utils
{
    public class ColorHelper
    {
        private readonly Processor _processor;

        public ColorHelper(Processor processor)
        {
            if (processor == null)
            {
                throw new ArgumentNullException("processor");
            }

            _processor = processor;
        }

        public bool ProcessColored(params IColored[] colored)
        {
            const bool isProcessed = true;

            if (colored.Length == 2)
            {
                if (colored[0].Color == Color.Red && colored[1].Color == Color.Red)
                {
                    _processor.Process(new Red(colored[0]), new Red(colored[1]));
                }
                else if (colored[0].Color == Color.Blue && colored[0].Color == Color.Blue)
                {
                    _processor.Process(new Blue(colored[0]), new Blue(colored[1]));
                }
                else
                {
                    return !isProcessed;
                }
            }
            else if (colored.Length == 1)
            {
                if (colored[0].Color == Color.Red)
                {
                    _processor.Process(new Red(colored[0]));
                }
                else if (colored[0].Color == Color.Blue)
                {
                    _processor.Process(new Blue(colored[0]));
                }
                else
                {
                    return !isProcessed;
                }
            }
            else
            {
                return false;
            }

            return isProcessed;
        }
    }
}
