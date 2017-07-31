using System;

namespace MusicFileRenameLibrary
{
    public class ShellMaker
    {
        public FileShell MakeFileShell(string[] parsedFile)
        {
            if (parsedFile.Length < 7)
                throw new ArgumentException("Wrong parsed file");
            var fileShell = new FileShell(parsedFile[0],
                parsedFile[1], parsedFile[2],
                parsedFile[3], parsedFile[4],
                parsedFile[5], parsedFile[6]
                );
            return fileShell;
        }

        public ArgsShell MakeArgsShell (string[] args)
        {
            if (args.Length != 2 && args.Length != 3)
                throw new ArgumentException("Wrong args length");

            var pattern = args[0];

            var recursive = args.Length == 2 ? false : true;
            if (recursive)
                if (args[1] != "-recursive")
                    throw new ArgumentException("Wrong second argument");

            IRenamer renamer;
            string renamerLikeString;

            if (recursive)
                renamerLikeString = args[2];
            else
                renamerLikeString = args[1];

            if (renamerLikeString == "-toFileName")
                renamer = new FileNameRenamer();
            else if (renamerLikeString == "-toTag")
                renamer = new TagRenamer();
            else
                throw new ArgumentException("Unknown rename type");

            return new ArgsShell(pattern, recursive, renamer);
        }
    }
}
