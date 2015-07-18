using System;

namespace Mp3Lib
{
    //здесь планируется общий хэлп и для каждой команды
    class Helper
    {
        private readonly string[] _helpMessages = {"1", "2", "3"};
        public void ShowInstructions()
        {
            foreach (var message in _helpMessages)
            {
                Console.WriteLine(message);
            }
        }
    }


}
