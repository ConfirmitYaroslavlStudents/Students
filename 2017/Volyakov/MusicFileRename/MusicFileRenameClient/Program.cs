using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFileRename
{
    class Program
    {
        //remove self made shell
        static void Main(string[] args)
        {
            string input;

            while( (input=Console.ReadLine()) != "Q" )
            {
                if(input.IndexOf("rename") == 0 && CheckRenameCommand(input))
                {
                    var inputParts = input.Split(' ');
                    var recursive = false;
                    if (inputParts.Length == 4)
                        recursive = true;
                    var pattern = inputParts[1];
                    if (inputParts[inputParts.Length - 1] == "-toFileName")
                        MusicFileRenamer.RenameFileNameByTag(pattern, recursive);
                    else
                        MusicFileRenamer.RenameTagByFileName(pattern, recursive);
                    Console.WriteLine("OK");
                }
                else if(input.IndexOf("clear") == 0)
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
        }

        private static bool CheckRenameCommand(string input)
        {
            var inputParts = input.Split(' ');
            if (inputParts.Length < 3 || inputParts.Length > 4)
                return false;
            if (inputParts[1].Length <= 4)
                return false;
            if (inputParts[1].IndexOf(".mp3") != inputParts[1].Length - 4)
                return false;
            if (inputParts.Length == 4 && inputParts[2] != "-recursive")
                return false;
            if (inputParts[inputParts.Length - 1] != "-toFileName" &&
                inputParts[inputParts.Length - 1] != "-toTag")
                return false;
            return true;
        }
    }
}
