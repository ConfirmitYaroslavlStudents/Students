using System;
using System.Collections.Generic;
namespace PrintText
{
    class ColorRedPrint:IPrintModification
    {
        private readonly IPrintModification _printMessage;

        public ColorRedPrint(IPrintModification printMessage)
        {
            _printMessage = printMessage;
        }

        public void Do(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            _printMessage.Do(text);
        }
    }
}
