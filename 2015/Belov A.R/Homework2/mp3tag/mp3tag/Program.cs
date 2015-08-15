using System;

namespace mp3tager
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                //[TODO] try catch logic in a single place
                try
                {
                    Menu.Show();
                    var currentOperation = Operation.Create(Menu.GetUserInput("\n\nCommand:").ToLower());
                    currentOperation.Call();
                }
                catch (Exception e)
                {

                    Menu.PrintError(e.Message);
                }
            }
        }
    }
}
