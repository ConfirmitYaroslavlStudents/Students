using System;

namespace PrintText
{
    internal class PrintMessage:IPrintModification
    {
        public void Do(string text)
        {
            Console.WriteLine(text);
        }
    }
}
