using System;

namespace PrintText
{
    class Beep:IPrintModification
    {
        private IPrintModification _printModification;
        public Beep(IPrintModification printModification)
        {
            _printModification = printModification;
        }

        public void Do(string text)
        {
            var textSplit = text.Split(' ');
            for (int i = 0; i < textSplit.Length; i++)
            {
                Console.Beep(300, 261);
            }
            _printModification.Do(text);
        }
    }
}
