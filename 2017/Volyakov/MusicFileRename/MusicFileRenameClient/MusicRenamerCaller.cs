using System;
using MusicFileRenameLibrary;
using System.IO;
namespace MusicFileRename
{
    public class MusicRenamerCaller
    {
        public void CallRenamer(string[] args)
        {
            if (args.Length != 3 && args.Length != 4)
                throw new FormatException("Неправильный формат задания команды");

            if (args[0]!="rename")
                throw new FormatException("Неправильный формат задания команды");

            string pattern = args[1];
            bool recursive = args[2] == "-recursive";
            string renameTypeStringFormat = recursive ? args[3] : args[2];

            MusicFileRenamer.RenameType renameType;
            switch(renameTypeStringFormat)
            {
                case "-toFileName": renameType = MusicFileRenamer.RenameType.ToFileName;
                    break;
                case "-toTag": renameType = MusicFileRenamer.RenameType.ToTag;
                    break;
                default: throw new FormatException("Неправильно задан тип переименования");
            }

            var currentDirectory = Directory.GetCurrentDirectory();

            MusicFileRenamer Renamer = new MusicFileRenamer();
            Renamer.RenameMusicFiles(currentDirectory, pattern, recursive, renameType);
        }
    }
}
