using System;
using System.Threading;

namespace PrintText
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = "Hello, world!";
            var textNumbers = "1 2 3 4 5 6 7";
            var printText = new PrintMessage();
            printText.Do(text);

            new Beep(new ColorRedPrint(new PrintMessage())).Do(text);

            Console.ReadKey();
            new Beep(new ColorYellowPrint(new PrintMessage())).Do(textNumbers);

            Console.ReadKey();
            new ColorGreenPrint(new Beep(new PrintMessage())).Do(text);
            
            Console.ReadKey();
        }
    }
}
