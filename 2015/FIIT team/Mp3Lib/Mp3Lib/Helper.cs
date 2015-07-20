using System;

namespace Mp3Lib
{
    //здесь планируется общий хэлп и для каждой команды
    class Helper
    {
        private readonly string[] _helpMessages =
        {
            "Available commands: help, rename, changeTags\n", 
            "'help' usage:\thelp\n", 
            "'rename' usage:\trename pathToFile pattern\n\tIn pattern you can use key words: {artist}, {title}, {genre}, {album}, {track}\n",
            "'changeTags' usage:\tchangeTags pathToFile tagToChange newTagValue "
        };
        public void ShowInstructions()
        {
            foreach (var message in _helpMessages)
            {
                Console.WriteLine(message);
            }
        }
    }


}
