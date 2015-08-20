using System;
using mp3tager.Operations;
using Mp3TagLib;
using Mp3TagLib.Operations;


namespace mp3tager
{
    class Program
    {
        static void Main()
        {
            Processor processor=new Processor(new OperationFactory());
            while (true)
            {
                //[TODO] try catch logic in a single place
                try
                {
                    Menu.Show();
                    processor.CallOperation(processor.CreateOperation(Menu.GetUserInput("\n\nCommand:").ToLower()));
                }
                catch (Exception e)
                {
                    Menu.PrintError(e.Message);
                }
            }
        }
    }
}
