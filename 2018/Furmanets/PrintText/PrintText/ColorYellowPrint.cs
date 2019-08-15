using System;

namespace PrintText
{
    class ColorYellowPrint : IPrintModification
    {
        private readonly IPrintModification _printMessage;

        public ColorYellowPrint(IPrintModification printMessage)
        {
            _printMessage = printMessage;
        }

        public void Do(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            _printMessage.Do(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

}
