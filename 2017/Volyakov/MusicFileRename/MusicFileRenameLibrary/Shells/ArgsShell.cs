namespace MusicFileRenameLibrary
{
    public class ArgsShell
    {
        public string Pattern { get; set; }
        public bool Recursive { get; set; }
        public IRenamer Renamer { get; set; }

        public ArgsShell()
        {
            Pattern = "";
            Recursive = false;
            Renamer = new FileNameRenamer();
        }

        public ArgsShell(string pattern, bool recursive, IRenamer renamer)
        {
            Pattern = pattern;
            Recursive = recursive;
            Renamer = renamer;
        }
    }
}
