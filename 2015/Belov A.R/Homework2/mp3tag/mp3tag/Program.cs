using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mp3TagLib;
using TagLib;


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
