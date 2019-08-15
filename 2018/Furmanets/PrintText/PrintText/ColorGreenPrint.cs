using System;
namespace PrintText
{
    class ColorGreenPrint:IPrintModification
    {
        private readonly IPrintModification _printMessage;

        public ColorGreenPrint(IPrintModification printMessage)
        {
            _printMessage = printMessage;
        }

        public void Do(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            _printMessage.Do(text);
        }
    }
}
